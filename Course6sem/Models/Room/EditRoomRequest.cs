namespace Application.Models.Room
{
    public class EditRoomRequest
    {
        public int Id { set; get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Business.Models.Checkpoint> Checkpoints { get; set; }
    }
}
