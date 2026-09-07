namespace Infrastructure.Services
{
    public interface IEmbeddingService
    {
        Task<float[]> GenerateEmbeddingAsync(string text);

        Task<string> GetRecommendationsJsonAsync(Guid productId, int count = 4);
    }
}