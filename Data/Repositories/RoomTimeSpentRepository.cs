using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public  class RoomTimeSpentRepository : GenericRepository<RoomTimeSpent>
    {
        public RoomTimeSpentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<RoomTimeSpent> DbRead()
        {
            return ReadAll()
                .Include(rts => rts.EnterPassageDate)
                .Include(rts => rts.ExitPassageDate)
                .Include(rts => rts.Room)
                .ToList();
        }

        public override void DbCrouch(string serializedAccessLevels)
        {
            var entities
                = DbEntities(serializedAccessLevels).Select(e => new RoomTimeSpent
                {
                    Id = e.Id,
                    Room = AppDbContext.Rooms.Find(e.Room.Id),
                    EnterPassageDate = AppDbContext.PassageDates.Find(e.EnterPassageDate.Id),
                    ExitPassageDate = AppDbContext.PassageDates.Find(e.ExitPassageDate.Id),
                }).ToList();
            DbOverride(entities);
        }
    }
}
