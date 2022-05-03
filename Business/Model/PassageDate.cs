namespace Business.Model
{
    public class PassageDate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CheckpointId { get; set; }
        public DateTime Date { get; set; }
    }
}