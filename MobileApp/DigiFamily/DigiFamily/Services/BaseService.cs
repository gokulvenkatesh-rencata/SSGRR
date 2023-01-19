using DigiFamily.Interfaces;
using Xamarin.Forms;

namespace DigiFamily.Services
{
    public abstract class BaseService
    {
        public IHttpClientService HttpService => DependencyService.Get<IHttpClientService>();
    }
}
