namespace Business.Models
{
    public class Checkpoint
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static implicit operator Checkpoint(Data.Models.Checkpoint checkpoint)
        {
            return checkpoint is null
                ? null
                : new Checkpoint
                {
                    Id = checkpoint.Id,
                    Name = checkpoint.Name,
                    Description = checkpoint.Description
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