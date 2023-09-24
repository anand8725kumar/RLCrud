using System.ComponentModel.DataAnnotations;

namespace Assignment.Model
{
    public class Crud
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
    }
}
