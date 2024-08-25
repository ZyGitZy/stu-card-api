namespace stu_card_api.interfaces
{
    public interface IFileService
    {
        Task<int> PostAsync(IFormFile formFile);

        Task<int> PostUrlAsync(string url);
    }
}
