namespace BE.Models
{
    public class Employee : BaseNameEntity
    {
        public string Surname { get; set; } = null!;
        public string Img { get; set; } = null!;

        public string? FaceLink { get; set; }
        public string? TwitLink { get; set; }
        public string? GoogleLink { get; set; }

        public int PositionId { get; set; }
        public Position? Position { get; set; }
    }
}
