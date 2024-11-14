namespace NastyaKupcovakt_42_21.Filters.StudentFilters
{
    public class StudentGroupFilter
    {
        public string? GroupName { get; set; }
        public string? GroupJob { get; set; }
        public string? GroupYear { get; set; }

        public bool StudentIsDeleted = false;
    }
}
