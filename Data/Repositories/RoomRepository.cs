using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class RoomRepository : GenericRepository<Room>
    {
        public RoomRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<Room> DbRead()
        {
            return ReadAll()
                    .Include(c => c.Checkpoints)
                    .ToList();
        }

        public override void DbCrouch(string serializedAccessLevels)
        {
            var entities
                = DbEntities(serializedAccessLevels).Select(e => new Room
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description = e.Description,
                    Checkpoints = e.Checkpoints.Select(с
                                    => AppDbContext.Checkpoints.Find(с.Id))
                                        .ToList(),
                }).ToList();
            DbOverride(entities);
        }
    }
}
