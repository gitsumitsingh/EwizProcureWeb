using EwizProcure.Models;

namespace EwizProcure.Interfaces
{
    public interface ILoginDA
    {
        List<LoginDetailViewModels> GetUserList(string EmailId);
    }
}
