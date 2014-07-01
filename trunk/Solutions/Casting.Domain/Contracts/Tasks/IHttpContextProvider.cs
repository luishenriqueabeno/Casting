using System.Web;

namespace Casting.Domain.Contracts.Tasks
{
    public interface IHttpContextProvider
    {
        HttpContextBase GetCurrentHttpContext(); 
    }
}