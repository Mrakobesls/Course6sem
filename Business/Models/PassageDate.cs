namespace Business.Models
{
    public class PassageDate
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CheckpointId { get; set; }
        public DateTime Date { get; set; }

        public static implicit operator PassageDate(Data.Models.PassageDate passageDate)
        {
            return passageDate is null
                ? null
                : new PassageDate
                {
                    Id = passageDate.Id,
                    UserId = passageDate.UserId,
                    CheckpointId = passageDate.CheckpointId,
                    Date = passageDate.Date
                };
        }


        public static implicit operator Data.Models.PassageDate(PassageDate passageDate)
        {
            return passageDate is null
                ? null
                : new Data.Models.PassageDate
                {
                    Id = passageDate.Id,
                    UserId = passageDate.UserId,
                    CheckpointId = passageDate.CheckpointId,
                    Date = passageDate.Date
                };
        }
    }
}