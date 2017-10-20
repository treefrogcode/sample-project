using System.Net.Http;
using System.Threading.Tasks;

namespace Example.Client.Proxy.Interfaces
{
    public interface IClientProxy
    {
        Task<T> Get<T>(string uri);
        Task<string> GetAsJson(string uri);
        Task<T> Post<T>(string uri, T content);
        Task<T> Put<T>(string uri, T content);
        Task<bool> Delete(string uri);
    }
}
