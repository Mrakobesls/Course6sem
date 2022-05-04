namespace Business.Models
{
    public class Checkpoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FirstRoomId { get; set; }
        public int SecondRoomId { get; set; }

        public static implicit operator Checkpoint(Data.Models.Checkpoint checkpoint)
        {
            return checkpoint is null
                ? null
                : new Checkpoint
                {
                    Id = checkpoint.Id,
                    Name = checkpoint.Name,
                    Description = checkpoint.Description,
                    FirstRoomId = checkpoint.Rooms.Count() == 0
                                    ? -1 
                                    : checkpoint.Rooms.ToList()[0].Id,
                    SecondRoomId = checkpoint.Rooms.Count() == 0
                                    ? -1
                                    : checkpoint.Rooms.ToList()[1].Id,
                };
        }

        public static implicit operator Data.Models.Checkpoint(Checkpoint checkpoint)
        {
            return checkpoint is null
                ? null
                : new Data.Models.Checkpoint
                {
                    Id = checkpoint.Id,
                    Name = checkpoint.Name,
                    Description = checkpoint.Description
                };
        }
    }
}