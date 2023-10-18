using System.ComponentModel.DataAnnotations;

namespace StajProjeDataBase.Database
{
    public class StudentSubjects
    {
        [Key]
        public int id { get; set; }
        public int student_id { get; set; }
        public int subject_id { get; set; }
        public int midterm { get; set; }
        public int final { get; set; }
        public int score { get; set; }
        public bool approved { get; set; }
    }
}
