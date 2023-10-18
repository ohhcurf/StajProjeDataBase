using System.ComponentModel.DataAnnotations;

namespace StajProjeDataBase.Database
{
    public class Teachers
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string teacher_no { get; set; }
    }
}
