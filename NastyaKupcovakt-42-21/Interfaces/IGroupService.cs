using Microsoft.EntityFrameworkCore;
using NastyaKupcovakt_42_21.Database;
using NastyaKupcovakt_42_21.Filters.GroupFilters;
using NastyaKupcovakt_42_21.Models;

namespace NastyaKupcovakt_42_21.Interfaces
{
    public interface IGroupService
    {
        public Task<Group[]> GetGroupsByJobAsync(GroupJobFilter filter, CancellationToken cancellationToken);
        public Task<Group[]> GetGroupsByYearAsync(GroupYearFilter filter, CancellationToken cancellationToken);
        public Task<Group[]> GetGroupsByIsDeletedAsync(GroupIsDeletedFilter filter, CancellationToken cancellationToken);
    }
    public class GroupService : IGroupService
    {
        private readonly StudentDbContext _dbContext;
        public GroupService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<Group[]> GetGroupsByJobAsync(GroupJobFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Group>().Where(w => w.GroupJob == filter.GroupJob).Where(w => w.IsDeleted == filter.GroupIsDeleted).ToArrayAsync(cancellationToken);
            return students;
        }
        public Task<Group[]> GetGroupsByYearAsync(GroupYearFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Group>().Where(w => w.GroupYear == filter.GroupYear).Where(w => w.IsDeleted == filter.GroupIsDeleted).ToArrayAsync(cancellationToken);
            return students;
        }
        public Task<Group[]> GetGroupsByIsDeletedAsync(GroupIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Group>().Where(w => w.IsDeleted == filter.GroupIsDeleted).ToArrayAsync(cancellationToken);
            return students;
        }
    }

}
