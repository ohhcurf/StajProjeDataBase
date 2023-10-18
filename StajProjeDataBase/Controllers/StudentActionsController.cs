using Microsoft.AspNetCore.Mvc;
using StajProjeDataBase.Database;

namespace StajProjeDataBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentActionsController : Controller
    {
        private readonly MyDatabaseContext db;

        public StudentActionsController(MyDatabaseContext context)
        {
            db = context;
        }

        //// Bir dönem girerek o dönemin derslerini görüntüle (List<Subjects> şeklinde)
        //[HttpGet]
        //public async Task<List<Subjects>> Get(int inputSemester)
        //{
        //    var subjects = db.Subjects.Where(p => p.semester == inputSemester).ToList();
        //    return subjects;

        //    string result = string.Empty;
        //    foreach(var subject in subjects)
        //    {
        //        result += "Ders: " + subject.name + " - Id: " + subject.id + "\n";
        //    }
        //    return result;
        //}
        // Bir dönem girerek o dönemin derslerini görüntüle (string şeklinde)
        [HttpGet("viewSubjects")]
        public async Task<string> Get(int inputSemester)
        {
            var subjects = db.Subjects.Where(p => p.semester == inputSemester).ToList();

            string result = string.Empty;
            foreach (var subject in subjects)
            {
                result += "Ders: " + subject.name + " - Id: " + subject.id + "\n";
            }
            return result;
        }



        // Okul numaramı ve almak istediğim dersin id sini girerek onay isteği yolla
        [HttpPost("requestSubject")]
        public async Task AddSubject(int inputStudentNo, int inputSubjectID)
        {
            var student = db.Students.FirstOrDefault(p => p.student_no == inputStudentNo);

            StudentSubjects studentToAdd = new StudentSubjects()
            {
                student_id = student.id,
                subject_id = inputSubjectID,
                approved = false
            };

            db.StudentSubjects.Add(studentToAdd);
            await db.SaveChangesAsync();
        }

        //// Approve'lanmış derslerimin notlarını gör (List<StudentSubjects> şeklinde)
        //[HttpGet("{inputStudentNo}")]
        //public async Task<List<StudentSubjects>> GetMySubjects(int inputStudentNo)
        //{
        //    var student = db.Students.FirstOrDefault(p => p.student_no == inputStudentNo);
        //    var mySubjects = db.StudentSubjects.Where(p => p.approved == true && p.student_id == student.id).ToList();
        //    return mySubjects;
        //}
        // Approve'lanmış derslerimin notlarını gör (string şeklinde)
        [HttpGet("viewScores/{inputStudentNo}")]
        public async Task<string> GetMySubjects(int inputStudentNo)
        {
            var student = db.Students.FirstOrDefault(p => p.student_no == inputStudentNo);
            var mySubjects = db.StudentSubjects.Where(p => p.approved == true && p.student_id == student.id).ToList();

            string result = string.Empty;
            foreach (var subject in mySubjects)
            {
                var selectedSubject = db.Subjects.Find(subject.subject_id);
                result += "Ders: " + selectedSubject.name + "\nMidterm: " + subject.midterm + "\nFinal: " + subject.final + "\nScore: " + subject.score + "\n\n";
            }
            return result;
        }



        [HttpGet("viewSchedule/{inputStudentNo}")]
        public async Task<string> GetSchedule(int inputStudentNo)
        {
            var student = db.Students.FirstOrDefault(p => p.student_no == inputStudentNo);
            var mySubjects = db.StudentSubjects.Where(p => p.student_id == student.id).ToList();
            string result = string.Empty;
            foreach (var subject in mySubjects)
            {
                var selectedSubject = db.Subjects.Find(subject.subject_id);
                result += "Ders: " + selectedSubject.name + "\nGün: " + selectedSubject.day + "\nSaat: " + selectedSubject.time + "\n\n";
            }
            return result;
        }
    }
}