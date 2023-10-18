using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StajProjeDataBase.Database;

namespace StajProjeDataBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SubjectsController : Controller
    {
        private readonly MyDatabaseContext db;

        public SubjectsController(MyDatabaseContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<List<Subjects>> Get()
        {
            return await db.Subjects.ToListAsync();
        }

        [HttpGet("{inputSubjectID}")]
        public async Task<Subjects> Get(int inputSubjectID)
        {
            var subject = await db.Subjects.FindAsync(inputSubjectID);
            return subject;
        }

        [HttpPost]
        public async Task<List<Subjects>> AddSubject(string inputSubjectName, int inputSemester)
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

        [HttpDelete("{inputSubjectID}")]
        public async Task<List<Subjects>> DeleteSubject(int inputSubjectID)
        {
            var subject = db.Subjects.FirstOrDefault(p => p.id == inputSubjectID);
            db.Subjects.Remove(subject);
            await db.SaveChangesAsync();
            return await db.Subjects.ToListAsync();
        }

        [HttpPut("{inputSubjectID}")]
        public async Task<List<Subjects>> UpdateSubject(int inputSubjectID, string inputSubjectName, int inputSemester)
        {
            var subject = await db.Subjects.FindAsync(inputSubjectID);

            subject.name = inputSubjectName;
            subject.semester = inputSemester;

            db.Subjects.Update(subject);
            await db.SaveChangesAsync();
            return await db.Subjects.ToListAsync();
        }
    }
}
