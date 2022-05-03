namespace Business.Model
{
    public class RoomTimeSpent
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int EnterPassageDateId { get; set; }
        public int ExitPassageDateId { get; set; }
    }
}