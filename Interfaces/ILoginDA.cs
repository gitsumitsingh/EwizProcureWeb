using EwizProcureWeb.Models;

namespace EwizProcureWeb.Interfaces
{
    public interface ILoginDA
    {
        List<LoginDetailViewModels> GetUserList(string EmailId);
    }
}
