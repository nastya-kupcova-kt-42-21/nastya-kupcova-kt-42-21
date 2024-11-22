using System.Diagnostics;
using System.Text.Json.Serialization;

namespace NastyaKupcovakt_42_21.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        // [JsonIgnore]
        public string? Surname { get; set; }
        public string? Name { get; set; }
        public string? Midname { get; set; }
        public int? GroupId { get; set; }
        [JsonIgnore]
        public Group? Group { get; set; }
        [JsonIgnore]
        public string? Exams { get; set; }
        public int Grades { get; set; }
        public bool IsDeleted { get; set; }

    }
}