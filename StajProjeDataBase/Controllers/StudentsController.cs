using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StajProjeDataBase.Database;

namespace StajProjeDataBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly MyDatabaseContext db;

        public StudentsController(MyDatabaseContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<List<Students>> Get()
        {
            return await db.Students.ToListAsync();
        }

        [HttpGet("{inputStudentID}")]
        public async Task<Students> Get(int inputStudentID)
        {
            var student = await db.Students.FindAsync(inputStudentID);
            return student;
        }

        [HttpPost]
        public async Task<List<Students>> AddStudent(string inputStudentName, string inputStudentSurname, int inputStudentNo)
        {
            Students newStudent = new Students()
            {
                name = inputStudentName,
                student_no = inputStudentNo,
                surname = inputStudentSurname
            };
            db.Students.Add(newStudent);
            await db.SaveChangesAsync();
            return await db.Students.ToListAsync();
        }

        [HttpDelete("{inputStudentID}")]
        public async Task<List<Students>> DeleteStudent(int inputStudentID)
        {
            var student = db.Students.FirstOrDefault(p => p.id == inputStudentID);
            db.Students.Remove(student);
            await db.SaveChangesAsync();
            return await db.Students.ToListAsync();
        }

        [HttpPut("{inputStudentID}")]
        public async Task<List<Students>> UpdateStudent(int inputStudentID, string inputStudentName, string inputStudentSurname, int inputStudentNo)
        {
            var student = await db.Students.FindAsync(inputStudentID);

            student.name = inputStudentName;
            student.surname = inputStudentSurname;
            student.student_no = inputStudentNo;

            db.Students.Update(student);
            await db.SaveChangesAsync();
            return await db.Students.ToListAsync();
        }
    }
}
