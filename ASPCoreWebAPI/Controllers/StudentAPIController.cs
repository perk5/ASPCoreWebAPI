using ASPCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAPIController : ControllerBase
    {
        private readonly CodeFirstDbContext context;

        public StudentAPIController(CodeFirstDbContext context)
        {
            this.context = context;
        }   

        [HttpGet]
        public async Task<ActionResult<List<StudentsDb>>> GetStudents()
        {
            var data = await context.StudentsDbs.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentsDb>> GetStudentsById(int id)
        {
            var student = await context.StudentsDbs.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;
        }

        [HttpPost]
        public async Task<ActionResult<StudentsDb>> CreateStudent(StudentsDb std)
        {
            await context.StudentsDbs.AddAsync(std);   
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpPut]
        public async Task<ActionResult<StudentsDb>> UpdateStudent(int id, StudentsDb std)
        {
            if (id != std.Id)
            {
                return BadRequest();
            }
            context.Entry(std).State = EntityState.Modified;
            //var student = await context.StudentsDbs.Where(item => item.Id == id).FirstOrDefaultAsync();
            //if(student != null)
            //{
            //    student.Id = std.Id;
            //    student.StudentName = std.StudentName;
            //    student.StudentGender = std.StudentGender;
            //    student.Age = std.Age;
            //    student.Standard = std.Standard;
            //    student.FatherName = std.FatherName;
            //}
            await context.SaveChangesAsync();
            return Ok(std);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<StudentsDb>> DeleteStudent(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            var student = await context.StudentsDbs.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            context.StudentsDbs.Remove(student);
            await context.SaveChangesAsync();

            return Ok(id);
        }
        }
}
    