using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public  class PositionRepository : GenericRepository<Position>
    {
        public PositionRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<Position> DbRead()
        {
            return ReadAll()
                    .ToList();
        }

        public override void DbCrouch(string serializedAccessLevels)
        {
            var entities
                = DbEntities(serializedAccessLevels).Select(e => new Position
                {
                    Id = e.Id,
                    Name = e.Name,
                }).ToList();
            DbOverride(entities);
        }
    }
}
