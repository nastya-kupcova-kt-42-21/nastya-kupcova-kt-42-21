using Microsoft.EntityFrameworkCore;
using NastyaKupcovakt_42_21.Database;
using NastyaKupcovakt_42_21.Filters.SubjectsFilters;
using NastyaKupcovakt_42_21.Models;

namespace NastyaKupcovakt_42_21.Interfaces
{
    public interface ISubjectService
    {
        public Task<Subject[]> GetSubjectsByDescriptionAsync(SubjectDescriptionFilter filter, CancellationToken cancellationToken);
        public Task<Subject[]> GetSubjectsByIsDeletedAsync(SubjectIsDeletedFilter filter, CancellationToken cancellationToken);
    }
    public class SubjectService : ISubjectService
    {
        private readonly StudentDbContext _dbContext;
        public SubjectService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Subject[]> GetSubjectsByDescriptionAsync(SubjectDescriptionFilter filter, CancellationToken cancellationToken = default)
        {
            var subjects = _dbContext.Set<Subject>().Where(w => w.SubjectDescription == filter.SubjectDescription).Where(w => w.IsDeleted == filter.SubjectIsDeleted).ToArrayAsync(cancellationToken);
            return subjects;
        }
        public Task<Subject[]> GetSubjectsByIsDeletedAsync(SubjectIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var subjects = _dbContext.Set<Subject>().Where(w => w.IsDeleted == filter.SubjectIsDeleted).ToArrayAsync(cancellationToken);
            return subjects;
        }
    }
}
