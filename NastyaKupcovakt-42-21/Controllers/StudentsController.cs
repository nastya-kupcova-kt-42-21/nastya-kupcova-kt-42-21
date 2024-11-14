using Microsoft.AspNetCore.Mvc;
using NastyaKupcovakt_42_21.Database;
using NastyaKupcovakt_42_21.Filters.StudentFilters;
using NastyaKupcovakt_42_21.Interfaces;
using NastyaKupcovakt_42_21.Models;
using NLog.Filters;

namespace NastyaKupcovakt_42_21.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentService _studentService;
        private StudentDbContext _context;
        public StudentsController(ILogger<StudentsController> logger, IStudentService studentService, StudentDbContext context)
        {
            _logger = logger;
            _studentService = studentService;
            _context = context;
        }

        [HttpPost("GetStudentsByGroup")]
        public async Task<IActionResult>
            GetStudentsByGroupAsync(StudentGroupFilter filter,
            CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupAsync(filter, cancellationToken);
            return Ok(students);
        }

        [HttpPost("GetStudentsByFIO")]
        public async Task<IActionResult> GetStudentsByFIOAsync(StudentFIOFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByFIOAsync(filter, cancellationToken);
            return Ok(students);
        }
        [HttpPost("GetStudentsByIsDeleted")]
        public async Task<IActionResult> GetStudentsByIsDeletedAsync(StudentIsDeletedFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByIsDeletedAsync(filter, cancellationToken);
            return Ok(students);
        }

        [HttpPost("AddStudent")]
        public IActionResult CreateStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }

        [HttpPut("EditStudent")]
        public IActionResult UpdateStudent(string surname, [FromBody] Student updatedStudent)
        {
            var existingStudent = _context.Students.FirstOrDefault(g => g.Surname == surname);
            if (existingStudent == null)
            {
                return NotFound();
            }
            existingStudent.Surname = updatedStudent.Surname;
            existingStudent.Name = updatedStudent.Name;
            existingStudent.Midname = updatedStudent.Midname;
            existingStudent.GroupId = updatedStudent.GroupId;
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("DeleteStudent")]
        public IActionResult DeleteStudent(string surname, [FromBody] Student deletedStudent)
        {
            var existingStudent = _context.Students.FirstOrDefault(g => g.Surname == surname);
            if (existingStudent == null)
            {
                return NotFound();
            }

            //existingStudent.IsDeleted = true;
            _context.Students.Remove(existingStudent);
            _context.SaveChanges();
            return Ok();
        }

    }
}