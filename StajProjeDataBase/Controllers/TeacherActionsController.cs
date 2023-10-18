using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StajProjeDataBase.Database;
using System.ComponentModel;

namespace StajProjeDataBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TeacherActionsController : Controller
    {
        private readonly MyDatabaseContext db;

        public TeacherActionsController(MyDatabaseContext context)
        {
            db = context;
        }

        //// Derslerini alan öğrencileri listele (öğretmen, öğretmen numarasını girer) (List<StudentSubjects>)
        //[HttpGet]
        //public async Task<List<StudentSubjects>> Get(string girilenNo)
        //{
        //    var selected = db.Teachers.FirstOrDefault(p => p.teacher_no == girilenNo);
        //    var subjects = db.TeacherSubjects.Where(p => p.teacher_id == selected.id).ToList();
        //    List<StudentSubjects> students = new List<StudentSubjects>();
        //    foreach(var subject in subjects)
        //    {
        //        students.AddRange(db.StudentSubjects.Where(p => p.subject_id == subject.subject_id && p.approved == true).ToList());
        //    }

        //    return students;
        //}
        // Derslerini alan öğrencileri listele (öğretmen, öğretmen numarasını girer) (string)
        [HttpGet("viewMyStudents")]
        public async Task<string> Get(string girilenNo)
        {
            var selectedTeacher = db.Teachers.FirstOrDefault(p => p.teacher_no == girilenNo);
            var subjects = db.TeacherSubjects.Where(p => p.teacher_id == selectedTeacher.id).ToList();
            List<StudentSubjects> students = new List<StudentSubjects>();
            string result = string.Empty;
            foreach (var subject in subjects)
            {
                students.AddRange(db.StudentSubjects.Where(p => p.subject_id == subject.subject_id && p.approved == true).ToList());
            }
            foreach (var student in students)
            {
                var selectedStudent = db.Students.Find(student.student_id);
                var selectedSubject = db.Subjects.Find(student.subject_id);
                result += "Ders: " + selectedSubject.name + " (ID:" + selectedSubject.id + ")" +
                    "\nÖğrenci: (ID:" + selectedStudent.id + ") " + selectedStudent.name + " (NO:" + selectedStudent.student_no + ")" +
                    "\nMidterm: " + student.midterm + "\nFinal: " + student.final + "\nScore: " + student.score + "\n\n";
            }
            return result;
        }

        // student id, subject id ve not girerek StudentSubjects'e güncelleme
        [HttpPut("scoreMyStudent")]
        public async Task<StudentSubjects> Put(int inputStudentID, int inputSubjectID, int midtermScore, int finalScore)
        {
            var selected = db.StudentSubjects.FirstOrDefault(p => p.subject_id == inputSubjectID && p.student_id == inputStudentID && p.approved == true);
            if (midtermScore > 0) selected.midterm = midtermScore;
            if (finalScore > 0)
            {
                selected.final = finalScore;
                selected.score = int.Parse(((selected.midterm * 0.40) + (finalScore * 0.600)).ToString()); // SQL Tablosunda bir attribute'un değeri sonradan değişmiyormuş, float olması lazımdı!
            }
            db.StudentSubjects.Update(selected);
            db.SaveChanges();

            return selected;
        }
    }
}