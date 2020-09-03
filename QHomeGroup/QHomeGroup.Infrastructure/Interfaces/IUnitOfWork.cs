using System;
using System.Threading;
using System.Threading.Tasks;

namespace QHomeGroup.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}