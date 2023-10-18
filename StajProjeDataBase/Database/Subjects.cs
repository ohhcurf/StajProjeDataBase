using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace StajProjeDataBase.Database
{
    public class Subjects
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int semester { get; set; }
        public string day { get; set; }
        public string time { get; set; }
    }
}