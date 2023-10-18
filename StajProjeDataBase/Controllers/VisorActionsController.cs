using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StajProjeDataBase.Database;
using System.ComponentModel;

namespace StajProjeDataBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VisorActionsController : Controller
    {
        private readonly MyDatabaseContext db;

        public VisorActionsController(MyDatabaseContext context)
        {
            db = context;
        }

        //// Onay bekleyen dersleri gör (List<StudentSubjects>)
        //[HttpGet]
        //public async Task<List<StudentSubjects>> Get()
        //{
        //    var subjects = db.StudentSubjects.Where(p => p.approved == false).ToList();
        //    return subjects;
        //}
        // Onay bekleyen dersleri gör (string)
        [HttpGet("viewApprovalRequests")]
        public async Task<string> Get()
        {
            var subjects = db.StudentSubjects.Where(p => p.approved == false).ToList();
            string result = string.Empty;
            foreach (var subject in subjects)
            {
                var selectedStudent = db.Students.Find(subject.student_id);
                var selectedSubject = db.Subjects.Find(subject.subject_id);
                result += "İsteğin ID'si: " + subject.id + "\nÖğrenci: (ID:" + subject.student_id + ") " + selectedStudent.name + " (NO:" + selectedStudent.student_no + ")\nOnay Bekleyen Ders: (ID:" + subject.subject_id + ") " + selectedSubject.name + "\n\n";
            }
            return result;
        }



        // ID ile onaylama işlemi yap
        [HttpPut("approve")]
        public async Task Approve(int girilenId)
        {
            var selected = db.StudentSubjects.Find(girilenId);
            selected.approved = true;
            db.StudentSubjects.Update(selected);
            db.SaveChanges();
        }

        // ID ile onay bekleyen isteği sil
        [HttpDelete("disapprove")]
        public async Task Disapprove(int girilenId)
        {
            var selected = db.StudentSubjects.Find(girilenId);
            db.StudentSubjects.Remove(selected);
            db.SaveChanges();
        }
    }
}