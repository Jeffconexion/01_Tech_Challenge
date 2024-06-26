using LocalFriendzApi.Core.Responses;



namespace LocalFriendzApi.Core.IIntegration
{
    public interface IInfoDDDIntegration
    {
        Task<bool> GetDDDInfo(int ddd);
    }

}
