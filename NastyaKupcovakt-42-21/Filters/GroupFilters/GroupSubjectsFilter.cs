namespace NastyaKupcovakt_42_21.Filters.GroupFilters
{
    public class GroupSubjectsFilter
    {
        public string? GroupName { get; set; }

        public string? GroupJob { get; set; }

        public string? GroupYear { get; set; }

        public bool GroupIsDeleted = false;
    }
}
