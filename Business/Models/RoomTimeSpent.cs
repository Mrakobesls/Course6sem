namespace Business.Models
{
    public class RoomTimeSpent
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int EnterPassageDateId { get; set; }
        public int ExitPassageDateId { get; set; }

        public static implicit operator RoomTimeSpent(Data.Models.RoomTimeSpent roomTimeSpent)
        {
            return roomTimeSpent is null
                ? null
                : new RoomTimeSpent
                {
                    Id = roomTimeSpent.Id,
                    RoomId = roomTimeSpent.RoomId,
                    EnterPassageDateId = roomTimeSpent.EnterPassageDateId,
                    ExitPassageDateId = roomTimeSpent.ExitPassageDateId
                };
        }


        public static implicit operator Data.Models.RoomTimeSpent(RoomTimeSpent roomTimeSpent)
        {
            return roomTimeSpent is null
                ? null
                : new Data.Models.RoomTimeSpent
                {
                    Id = roomTimeSpent.Id,
                    RoomId = roomTimeSpent.RoomId,
                    EnterPassageDateId = roomTimeSpent.EnterPassageDateId,
                    ExitPassageDateId = roomTimeSpent.ExitPassageDateId
                };
        }
    }
}