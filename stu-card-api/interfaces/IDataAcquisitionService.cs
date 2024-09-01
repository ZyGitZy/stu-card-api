namespace stu_card_api.interfaces
{
    public interface IDataAcquisitionService
    {
        Task<bool> BackgroundCollection();

        Task<bool> PersionCollection();
    }
}
