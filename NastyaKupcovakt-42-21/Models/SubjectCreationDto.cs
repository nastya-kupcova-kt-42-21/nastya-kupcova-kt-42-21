namespace NastyaKupcovakt_42_21.Models
{
    public class SubjectCreationDto
    {
        public string? SubjectName { get; set; }
        public string? SubjectDescription { get; set; }
        public int GroupId { get; set; } // ID группы для связи
    }
}
