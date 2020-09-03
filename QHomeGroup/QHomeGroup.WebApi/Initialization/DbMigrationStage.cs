using Microsoft.EntityFrameworkCore;
using QHomeGroup.Data.EF.Connector;
using QHomeGroup.Utilities.Extensions;
using System.Threading.Tasks;

namespace QHomeGroup.WebApi.Initialization
{
    public class DbMigrationStage : IInitializationStage
    {
        private readonly AppDbContext _dbContext;

        public DbMigrationStage(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Order => 1;
        public async Task ExecuteAsync()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}
