using System.Diagnostics;
using System.Text.Json.Serialization;

namespace NastyaKupcovakt_42_21.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectDescription { get; set; }

        public bool IsDeleted { get; set; }
        [JsonIgnore]
        // Связь многие ко многим с Group
        public virtual ICollection<Group>? Groups { get; set; }
    }
}