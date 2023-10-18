using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StajProjeDataBase.Database;

namespace StajProjeDataBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeachersController : Controller
    {
        private readonly MyDatabaseContext db;

        public TeachersController(MyDatabaseContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<List<Teachers>> Get()
        {
            return await db.Teachers.ToListAsync();
        }

        [HttpGet("{inputTeacherID}")]
        public async Task<Teachers> Get(int inputTeacherID)
        {
            var teachers = await db.Teachers.FindAsync(inputTeacherID);
            return teachers;
        }

        [HttpPost] //
        public async Task<List<Subjects>> AddTeacher(string inputSubjectName, int inputSemester)
        {
            Subjects subjectToCreate = new Subjects()
            {
                name = inputSubjectName,
                semester = inputSemester
            };

            db.Subjects.Add(subjectToCreate);
            await db.SaveChangesAsync();
            return await db.Subjects.ToListAsync();
        }

        [HttpDelete("{inputTeacherID}")]
        public async Task<List<Teachers>> DeleteTeacher(int inputTeacherID)
        {
            var teacher = db.Teachers.FirstOrDefault(p => p.id == inputTeacherID);
            db.Teachers.Remove(teacher);
            await db.SaveChangesAsync();
            return await db.Teachers.ToListAsync();
        }

        [HttpPut("{inputTeacherID}")]
        public async Task<List<Teachers>> UpdateTeacher(int inputTeacherID, string inputTeacherName, string inputTeacherNo)
        {
            var teacher = await db.Teachers.FindAsync(inputTeacherID);

            teacher.name = inputTeacherName;
            teacher.teacher_no = inputTeacherNo;

            db.Teachers.Update(teacher);
            await db.SaveChangesAsync();
            return await db.Teachers.ToListAsync();
        }
    }
}