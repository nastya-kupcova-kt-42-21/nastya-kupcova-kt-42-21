using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace NastyaKupcovakt_42_21.Models
{
    public class Group
    {
        [JsonIgnore]
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? GroupJob { get; set; }
        public string? GroupYear { get; set; }
        public int StudentQuantity { get; set; }

        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public List<Student>? Students { get; set; }
        [JsonIgnore]
        // Связь многие ко многим с Subject
        public virtual ICollection<Subject>? Subjects { get; set; }

        public bool IsValidGroupName()
        {
            return Regex.Match(GroupName, @"\D*-\d*-\d\d").Success;
        }
    }
}
