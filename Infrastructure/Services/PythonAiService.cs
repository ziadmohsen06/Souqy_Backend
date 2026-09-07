using System.Net.Http.Json;

namespace Infrastructure.Services
{
    public class PythonAiService : IEmbeddingService
    {
        // Just create a static, reusable HttpClient. No DI needed!
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<float[]> GenerateEmbeddingAsync(string text)
        {
            var response = await _httpClient.PostAsJsonAsync(
                "http://localhost:8000/generate-embedding", 
                new { text }
            );
            
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<EmbeddingResponse>();
            return result?.Embedding ?? throw new Exception("Failed to get embedding from Python service");
        }

        public async Task<string> GetRecommendationsJsonAsync(Guid productId, int count = 4)
        {
            var response = await _httpClient.GetAsync($"http://localhost:8000/recommendations/{productId}?limit={count}");
            
            if (!response.IsSuccessStatusCode) return "[]"; // Return empty JSON array on failure
            
            // Just return the raw string!
            return await response.Content.ReadAsStringAsync();
        }
    }


    internal class EmbeddingResponse
    {
        public float[] Embedding { get; set; } = Array.Empty<float>();
    }
}