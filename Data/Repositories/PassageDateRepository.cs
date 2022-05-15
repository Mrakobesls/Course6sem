using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class PassageDateRepository : GenericRepository<PassageDate>
    {
        public PassageDateRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public List<PassageDate> DbRead()
        {
            return ReadAll()
                    .Include(pd => pd.User)
                    .Include(pd => pd.Checkpoint)
                    .ToList();
        }

        public override void DbCrouch(string serializedAccessLevels)
        {
            var entities
                = DbEntities(serializedAccessLevels).Select(e => new PassageDate
                {
                    Id = e.Id,
                    Date = e.Date,
                    User = AppDbContext.Users.Find(e.User.Id),
                    Checkpoint = AppDbContext.Checkpoints.Find(e.Checkpoint.Id),
                    StartTimeRoomSpent = AppDbContext.RoomTimeSpents.Find(e.StartTimeRoomSpent.Id),
                    EndTimeRoomSpent = AppDbContext.RoomTimeSpents.Find(e.EndTimeRoomSpent.Id),
                }).ToList();
            DbOverride(entities);
        }
    }
}
