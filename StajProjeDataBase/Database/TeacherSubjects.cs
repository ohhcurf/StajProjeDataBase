using System.ComponentModel.DataAnnotations;

namespace StajProjeDataBase.Database
{
    public class TeacherSubjects
    {
        [Key]
        public int id { get; set; }
        public int teacher_id { get; set; }
        public int subject_id { get; set; }
    }
}
