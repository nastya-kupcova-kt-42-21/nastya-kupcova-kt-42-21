using Microsoft.AspNetCore.Mvc;
using NastyaKupcovakt_42_21.Database;
using NastyaKupcovakt_42_21.Filters.SubjectsFilters;
using NastyaKupcovakt_42_21.Interfaces;
using NastyaKupcovakt_42_21.Models;

namespace NastyaKupcovakt_42_21.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly ILogger<SubjectsController> _logger;
        private readonly ISubjectService _subjectService;
        private StudentDbContext _context;

        public SubjectsController(ILogger<SubjectsController> logger, ISubjectService subjectService, StudentDbContext context)
        {
            _logger = logger;
            _subjectService = subjectService;
            _context = context;
        }

        [HttpPost("GetSubjectsByDescription")]
        public async Task<IActionResult> GetSubjectsByDescriptionAsync(SubjectDescriptionFilter filter, CancellationToken cancellationToken = default)
        {
            var subjects = await _subjectService.GetSubjectsByDescriptionAsync(filter, cancellationToken);
            return Ok(subjects);
        }

        // Новая функция для получения групп по предмету
        [HttpPost("GetGroupsBySubject/{subjectId}")]
        public async Task<IActionResult> GetGroupsBySubjectAsync(int subjectId, CancellationToken cancellationToken = default)
        {
            var groups = await _subjectService.GetGroupsBySubjectIdAsync(subjectId, cancellationToken);
            return Ok(groups);
        }

        [HttpPost("AddSubject")]
        public async Task<IActionResult> CreateSubject([FromBody] SubjectCreationDto subjectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Проверяем, существует ли группа с данным GroupId
            var group = await _context.Groups.FindAsync(subjectDto.GroupId);
            if (group == null)
            {
                return BadRequest("Группа с указанным ID не найдена.");
            }

            var subject = new Subject
            {
                SubjectName = subjectDto.SubjectName,
                SubjectDescription = subjectDto.SubjectDescription,
                IsDeleted = false, // Или любое другое значение по умолчанию
                Groups = new List<Group> { group } // Привязываем группу к предмету
            };

            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync(); // Используйте асинхронное сохранение для избежания блокировок
            return Ok(subject);
        }

        [HttpPut("EditSubject")]
        public IActionResult UpdateSubject(string name, [FromBody] Subject updatedSubject)
        {
            var existingSubject = _context.Subjects.FirstOrDefault(g => g.SubjectName == name);
            if (existingSubject == null)
            {
                return NotFound();
            }
            existingSubject.SubjectName = updatedSubject.SubjectName;
            existingSubject.SubjectDescription = updatedSubject.SubjectDescription;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete("DeleteSubject")]
        public IActionResult DeleteSubject(string name, [FromBody] Subject deletedSubject)
        {
            var existingSubject = _context.Subjects.FirstOrDefault(g => g.SubjectName == name);
            if (existingSubject == null)
            {
                return NotFound();
            }
            _context.Subjects.Remove(existingSubject);
            _context.SaveChanges();
            return Ok();
        }
    }
}