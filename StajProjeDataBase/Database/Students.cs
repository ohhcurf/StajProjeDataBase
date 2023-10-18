using System.ComponentModel.DataAnnotations;

namespace StajProjeDataBase.Database
{
    public class Students
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public int student_no { get; set; }
    }
}
