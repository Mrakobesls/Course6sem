using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class AccessLevelRepository : GenericRepository<AccessLevel>
    {
        public AccessLevelRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<AccessLevel> DbRead()
        {
            return ReadAll().ToList();
        }

        public override void DbCrouch(string serializedAccessLevels)
        {
            var entities
                = DbEntities(serializedAccessLevels).Select(e => new AccessLevel
                {
                    Id = e.Id,
                    Name = e.Name,
                }).ToList();
            DbOverride(entities);
        }
    }
}
