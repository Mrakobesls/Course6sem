namespace Business.Models
{
    public class MonthUserRoomTimeSpent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public static implicit operator MonthUserRoomTimeSpent(Data.Models.MonthUserRoomTimeSpent monthUserRoomTimeSpent)
        {
            return monthUserRoomTimeSpent is null
                ? null
                : new MonthUserRoomTimeSpent
                {
                    Id = monthUserRoomTimeSpent.Id,
                    UserId = monthUserRoomTimeSpent.UserId,
                    RoomId = monthUserRoomTimeSpent.RoomId
                };
        }


        public static implicit operator Data.Models.MonthUserRoomTimeSpent(MonthUserRoomTimeSpent monthUserRoomTimeSpent)
        {
            return monthUserRoomTimeSpent is null
                ? null
                : new Data.Models.MonthUserRoomTimeSpent
                {
                    Id = monthUserRoomTimeSpent.Id,
                    UserId = monthUserRoomTimeSpent.UserId,
                    RoomId = monthUserRoomTimeSpent.RoomId
                };
        }
    }
}