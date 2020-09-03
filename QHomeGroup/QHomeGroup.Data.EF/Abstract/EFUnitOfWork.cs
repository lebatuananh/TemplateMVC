using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using QHomeGroup.Data.EF.Connector;
using QHomeGroup.Data.Interfaces;
using QHomeGroup.Infrastructure.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace QHomeGroup.Data.EF.Abstract
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appContext;
        private readonly ILogger<IUnitOfWork> _logger;

        public EFUnitOfWork(AppDbContext appContext, ILogger<IUnitOfWork> logger)
        {
            _appContext = appContext;
            _logger = logger;
        }

        public void Dispose()
        {
            _appContext.Dispose();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            using (var transaction = await _appContext.Database.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var entityEntries = _appContext.ChangeTracker.Entries().Where(x =>
              x.State == EntityState.Added || x.State == EntityState.Modified).ToList();
                    foreach (var entry in entityEntries)
                    {
                        switch (entry.State)
                        {
                            case EntityState.Added:
                                {
                                    if (entry.Entity is IDateTracking)
                                    {
                                        entry.Property(nameof(IDateTracking.DateCreated)).IsModified = true;
                                        entry.Property(nameof(IDateTracking.DateCreated)).CurrentValue = DateTime.Now;
                                        entry.Property(nameof(IDateTracking.DateModified)).IsModified = true;
                                        entry.Property(nameof(IDateTracking.DateModified)).CurrentValue = DateTime.Now;
                                    }
                                    break;
                                }
                            case EntityState.Modified:
                                {
                                    if (entry.Entity is IDateTracking)
                                    {
                                        entry.Property(nameof(IDateTracking.DateCreated)).IsModified = false;
                                        entry.Property(nameof(IDateTracking.DateModified)).IsModified = true;
                                        entry.Property(nameof(IDateTracking.DateModified)).CurrentValue = DateTime.Now;
                                    }
                                    break;
                                }
                        }
                    }
                    await _appContext.SaveChangesAsync(cancellationToken);
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    _logger.LogError(e, "Unit of work commit failed");
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}