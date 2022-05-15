using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class MonthUserRoomTimeSpentRepository : GenericRepository<MonthUserRoomTimeSpent>
    {
        public MonthUserRoomTimeSpentRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public List<MonthUserRoomTimeSpent> DbRead()
        {
            return ReadAll()
                    .Include(murt => murt.User)
                    .Include(murt => murt.Room)
                    .ToList();
        }

        public override void DbCrouch(string serializedAccessLevels)
        {
            var entities
                = DbEntities(serializedAccessLevels).Select(e => new MonthUserRoomTimeSpent
                {
                    Id = e.Id,
                    TotalTime = e.TotalTime,
                    User = AppDbContext.Users.Find(e.User.Id),
                    Room = AppDbContext.Rooms.Find(e.Room.Id),
                }).ToList();
            DbOverride(entities);
        }
    }
}
