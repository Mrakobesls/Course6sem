using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CheckpointRepository : GenericRepository<Checkpoint>
    {
        public CheckpointRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<Checkpoint> DbRead()
        {
            return ReadAll()
                .Include(c => c.AccessLevels)
                .ToList();
        }

        public override void DbCrouch(string serializedAccessLevels)
        {
            var entities
                = DbEntities(serializedAccessLevels).Select(e => new Checkpoint
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    AccessLevels = e.AccessLevels.Select(al 
                                    => AppDbContext.AccessLevels.Find(al.Id))
                                        .ToList(),
                }).ToList();
            DbOverride(entities);
        }
    }
}
