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

        Task<bool> AddSubjectToGroupAsync(int groupId, int subjectId, CancellationToken cancellationToken);

        Task<Subject[]> GetSubjectsByGroupAsync(int groupId, CancellationToken cancellationToken);//поиск предметов по группе(groupid)

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

        public async Task<bool> AddSubjectToGroupAsync(int groupId, int subjectId, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups
                .Include(g => g.Subjects) // Загружаем связанные предметы
                .FirstOrDefaultAsync(g => g.GroupId == groupId, cancellationToken);

            if (group == null)
            {
                return false; // Группа не найдена
            }

            var subject = await _dbContext.Subjects.FindAsync(subjectId); // Поиск предмета
            if (subject == null)
            {
                return false; // Предмет не найден
            }

            // Проверяем, есть ли уже связь
            if (group.Subjects == null)
            {
                group.Subjects = new List<Subject>();
            }

            if (!group.Subjects.Any(s => s.SubjectId == subjectId))
            {
                group.Subjects.Add(subject); // Добавляем связь
                await _dbContext.SaveChangesAsync(cancellationToken);
            }

            return true; // Связь успешно добавлена
        }

        public async Task<Subject[]> GetSubjectsByGroupAsync(int groupId, CancellationToken cancellationToken = default)
        {
            var group = await _dbContext.Groups
                .Include(g => g.Subjects) // Загружаем связанные предметы
                .FirstOrDefaultAsync(g => g.GroupId == groupId && !g.IsDeleted, cancellationToken);

            return group?.Subjects?.ToArray() ?? Array.Empty<Subject>();
        }


    public Task<Group[]> GetGroupsByIsDeletedAsync(GroupIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var students = _dbContext.Set<Group>().Where(w => w.IsDeleted == filter.GroupIsDeleted).ToArrayAsync(cancellationToken);
            return students;
        }
    }

}