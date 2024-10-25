using Microsoft.AspNetCore.Mvc;
using NastyaKupcovakt_42_21.Database;
using NastyaKupcovakt_42_21.Models;
//using NastyaKupcovakt_42_21.Filters.GroupFilters;
//using NastyaKupcovakt_42_21.Interfaces;
using Microsoft.EntityFrameworkCore;
using NastyaKupcovakt_42_21.Filters.GroupFilters;
using NastyaKupcovakt_42_21.Interfaces;


namespace NastyaKupcovakt_42_21.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly ILogger<GroupsController> _logger;
        private readonly IGroupService _studentService;
        private StudentDbContext _context;
        public GroupsController(ILogger<GroupsController> logger, IGroupService studentService, StudentDbContext context)
        {
            _logger = logger;
            _studentService = studentService;
            _context = context;
        }
        [HttpPost("GetGroupsByJob")]
        public async Task<IActionResult> GetGroupsByGroupAsync(GroupJobFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetGroupsByJobAsync(filter, cancellationToken);
            return Ok(students);
        }
        [HttpPost("GetGroupsByYear")]
        public async Task<IActionResult> GetGroupsByFIOAsync(GroupYearFilter filter, CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetGroupsByYearAsync(filter, cancellationToken);
            return Ok(students);
        }
        //[HttpPost("GetGroupsByIsDeleted")]
        //public async Task<IActionResult> GetGroupsByIsDeletedAsync(GroupIsDeletedFilter filter, CancellationToken cancellationToken = default)
        //{
        //    var students = await _studentService.GetGroupsByIsDeletedAsync(filter, cancellationToken);
        //    return Ok(students);
        //}
        [HttpPost("AddGroup")]
        public IActionResult CreateGroup([FromBody] Group student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Groups.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }
        [HttpPut("EditGroup")]
        public IActionResult UpdateGroup(string name, [FromBody] Group updatedGroup)
        {
            var existingGroup = _context.Groups.FirstOrDefault(g => g.GroupName == name);
            if (existingGroup == null)
            {
                return NotFound();
            }
            existingGroup.GroupName = updatedGroup.GroupName;
            existingGroup.GroupJob = updatedGroup.GroupJob;
            existingGroup.GroupYear = updatedGroup.GroupYear;
            existingGroup.StudentQuantity = updatedGroup.StudentQuantity;
            _context.SaveChanges();
            return Ok();
        }
        [HttpDelete("DeleteGroup")]
        public IActionResult DeleteGroup(string name, [FromBody] Group deletedGroup)
        {
            var existingGroup = _context.Groups.FirstOrDefault(g => g.GroupName == name);
            if (existingGroup == null)
            {
                return NotFound("Группа не найдена.");
            }
            //existingGroup.IsDeleted = true;
            _context.Groups.Remove(existingGroup);
            _context.SaveChanges();
            return Ok();
        }
    }
}

