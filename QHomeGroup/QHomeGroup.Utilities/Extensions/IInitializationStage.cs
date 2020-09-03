using System.Threading.Tasks;

namespace QHomeGroup.Utilities.Extensions
{
    public interface IInitializationStage
    {
        int Order { get; }
        Task ExecuteAsync();
    }
}
