namespace Application.Models.Passage
{
    public class PassCheckpointRequest
    {
        public Business.Models.Room Room { get; set; }
        public int CheckpointId { get; set; }
    }
}
