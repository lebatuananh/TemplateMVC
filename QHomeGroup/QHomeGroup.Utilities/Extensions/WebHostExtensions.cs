using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace QHomeGroup.Utilities.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost AutoInit(this IWebHost webHost)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                foreach (var stage in scope.ServiceProvider.GetServices<IInitializationStage>().OrderBy(t => t.Order))
                {
                    stage.ExecuteAsync().Wait();
                }
            }

            return webHost;
        }
    }
}
