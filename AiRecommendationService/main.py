from fastapi import FastAPI, HTTPException
from fastapi.middleware.cors import CORSMiddleware
from pydantic import BaseModel
import psycopg2
from sentence_transformers import SentenceTransformer
from sklearn.metrics.pairwise import cosine_similarity
import os
from dotenv import load_dotenv
import json

load_dotenv()

app = FastAPI(title="AI Recommendation Service")

# CORS - Allow .NET and React to call this
app.add_middleware(
    CORSMiddleware,
    allow_origins=["*"],  # In production, specify your actual URLs
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)

# Load AI model once at startup
print("Loading AI model...")
model = SentenceTransformer(os.getenv("MODEL_NAME", "sentence-transformers/all-MiniLM-L6-v2"))
print("Model loaded successfully!")

# Database configuration
DATABASE_URL = os.getenv("DATABASE_URL")

def get_db_connection():
    return psycopg2.connect(DATABASE_URL)

# Request/Response models
class EmbeddingRequest(BaseModel):
    text: str

class EmbeddingResponse(BaseModel):
    embedding: list[float]

class ProductRecommendation(BaseModel):
    Id: str
    Name: str
    Description: str | None
    ImageUrl: str | None = None
    Price: float
    SimilarityScore: float

# ==================== ENDPOINTS ====================

@app.post("/generate-embedding")
async def generate_embedding(request: EmbeddingRequest):
    """Generate embedding for a product description"""
    try:
        embedding = model.encode(request.text)
        return EmbeddingResponse(embedding=embedding.tolist())
    except Exception as e:
        raise HTTPException(status_code=500, detail=f"Error generating embedding: {str(e)}")

@app.get("/recommendations/{product_id}")
async def get_recommendations(product_id: str, limit: int = 4):
    """Get product recommendations using cosine similarity"""
    try:
        conn = get_db_connection()
        cursor = conn.cursor()
        
        cursor.execute(
            """SELECT "Id", "Name", "Description", "Price", "Embedding" 
               FROM "Products" WHERE "Id" = %s""",
            (product_id,)
        )
        target = cursor.fetchone()
        
        if not target:
            conn.close()
            print(f"[AI Service] 404: Product {product_id} not found in database.")
            return [] # Return empty instead of crashing frontend
            # raise HTTPException(status_code=404, detail="Product not found")
        
        target_id, target_name, target_desc, target_price, target_embedding_raw = target
        
        if target_embedding_raw is None:
            conn.close()
            print(f"[AI Service] 400: Product '{target_name}' ({product_id}) has NO embedding. Falling back to empty.")
            return [] # This is why seeded products return empty now
            # raise HTTPException(status_code=400, detail="Product has no embedding")
        
        # Parse embedding
        if isinstance(target_embedding_raw, str):
            target_embedding = json.loads(target_embedding_raw)
        else:
            target_embedding = target_embedding_raw
        
        # DEBUG: Print the target embedding 
        print(f"\n=== DEBUG INFO ===")
        print(f"Target Product: {target_name}")
        print(f"Target Embedding Type: {type(target_embedding)}")
        print(f"Target Embedding Length: {len(target_embedding)}")
        print(f"Target Embedding First 5 values: {target_embedding[:5]}")
        print(f"Target Embedding Sample (raw): {str(target_embedding_raw)[:100]}")
        
        cursor.execute(
            """SELECT "Id", "Name", "Description", "Price", "ImageUrl", "Embedding" 
               FROM "Products" 
               WHERE "Embedding" IS NOT NULL AND "Id" != %s""",
            (product_id,)
        )
        candidates = cursor.fetchall()
        conn.close()
        
        if not candidates:
            print(f"[AI Service] 400: No candidates found for product {product_id}. Returning empty list.")
            return []
        
        valid_candidates = []
        candidate_vectors = []
        
        for c in candidates:
            raw_emb = c[5]
            if raw_emb is not None:
                try:
                    if isinstance(raw_emb, str):
                        emb = json.loads(raw_emb)
                    else:
                        emb = raw_emb
                    
                    candidate_vectors.append(emb)
                    valid_candidates.append(c)
                except Exception as parse_error:
                    print(f"Skipping malformed embedding for {c[0]}: {parse_error}")
                    continue
        
        if not candidate_vectors:
            print(f"[AI Service] 400: No valid candidate embeddings found for product {product_id}. Returning empty list.")
            return []
        
        # DEBUG: Print candidate embeddings 
        print(f"\nCandidate Count: {len(candidate_vectors)}")
        if candidate_vectors:
            print(f"First Candidate: {valid_candidates[0][1]}")
            print(f"First Candidate Embedding Length: {len(candidate_vectors[0])}")
            print(f"First Candidate Embedding First 5 values: {candidate_vectors[0][:5]}")
        
        # Calculate cosine similarity
        target_vector = [target_embedding]
        similarities = cosine_similarity(target_vector, candidate_vectors)[0]
        
        # DEBUG: Print similarity scores 
        print(f"\nSimilarity Scores: {similarities}")
        print(f"===================\n")
        
        recommendations = []
        for i, candidate in enumerate(valid_candidates):
            recommendations.append(ProductRecommendation(
                Id=str(candidate[0]),
                Name=candidate[1],
                Description=candidate[2],
                Price=float(candidate[3]),
                ImageUrl=candidate[4],
                SimilarityScore=float(similarities[i])
            ))
        
        recommendations.sort(key=lambda x: x.SimilarityScore, reverse=True)
        
                # Print the final recommendations for DEBUGGING purposes
        print(f"\n[AI Service] Returning {len(recommendations[:limit])} recommendations:")
        for i, rec in enumerate(recommendations[:limit], 1):
            print(f"   {i}. {rec.Name} (Score: {rec.SimilarityScore:.3f}), (image: {rec.ImageUrl})")
        print()
        
        
        return recommendations[:limit]
        
    except HTTPException:
        raise
    except Exception as e:
        # This will print the EXACT red error traceback to your terminal
        print(f"[AI Service] 500: CRITICAL ERROR for product {product_id}")
        print(f"   Reason: {str(e)}")
        import traceback
        traceback.print_exc()
        raise HTTPException(status_code=500, detail=f"Error getting recommendations: {str(e)}")
    
    
@app.get("/health")
async def health_check():
    """Health check endpoint"""
    return {"status": "healthy", "model_loaded": model is not None}

if __name__ == "__main__":
    import uvicorn
    port = int(os.getenv("PORT", 8000))
    uvicorn.run(app, host="0.0.0.0", port=port)