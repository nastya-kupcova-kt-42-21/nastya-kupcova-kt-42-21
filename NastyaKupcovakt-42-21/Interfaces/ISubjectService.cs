using Microsoft.EntityFrameworkCore;
using NastyaKupcovakt_42_21.Database;
using NastyaKupcovakt_42_21.Filters.SubjectsFilters;
using NastyaKupcovakt_42_21.Models;

namespace NastyaKupcovakt_42_21.Interfaces
{
    public interface ISubjectService
    {
        Task<Subject[]> GetSubjectsByDescriptionAsync(SubjectDescriptionFilter filter, CancellationToken cancellationToken);
        Task<Subject[]> GetSubjectsByIsDeletedAsync(SubjectIsDeletedFilter filter, CancellationToken cancellationToken);
        Task<Group[]> GetGroupsBySubjectIdAsync(int subjectId, CancellationToken cancellationToken);
        Task<Subject> AddSubjectAsync(SubjectCreationDto subjectDto, CancellationToken cancellationToken); // Новый метод
    }

    public class SubjectService : ISubjectService
    {
        private readonly StudentDbContext _dbContext;

        public SubjectService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Subject[]> GetSubjectsByDescriptionAsync(SubjectDescriptionFilter filter, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Subject>()
                .Where(w => w.SubjectDescription == filter.SubjectDescription)
                .Where(w => w.IsDeleted == filter.SubjectIsDeleted)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Group[]> GetGroupsBySubjectIdAsync(int subjectId, CancellationToken cancellationToken)
        {
            return await _dbContext.Groups
                .Include(g => g.Subjects)
                .Where(g => g.Subjects.Any(s => s.SubjectId == subjectId) && !g.IsDeleted)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Subject[]> GetSubjectsByIsDeletedAsync(SubjectIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Subject>()
                .Where(w => w.IsDeleted == filter.SubjectIsDeleted)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<Subject> AddSubjectAsync(SubjectCreationDto subjectDto, CancellationToken cancellationToken)
        {
            var group = await _dbContext.Groups.FindAsync(subjectDto.GroupId); // Здесь используем асинхронный вариант
            if (group == null)
            {
                throw new Exception("Group not found");
            }

            var subject = new Subject
            {
                SubjectName = subjectDto.SubjectName,
                SubjectDescription = subjectDto.SubjectDescription,
                IsDeleted = false, // Значение по умолчанию
                Groups = new List<Group> { group } // Установка связи
            };

            _dbContext.Subjects.Add(subject);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return subject;
        }
    }
}
