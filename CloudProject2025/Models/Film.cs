using System.ComponentModel.DataAnnotations;

namespace CloudProject2025.Models
{
    public class Film
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Plot { get; set; }
        public string? Released { get; set; }
        [Required]
        public string? Genre{ get; set; }
        [Required]
        public string? Director{ get; set; }
        public string? Awards { get; set; }
        public float? MyRate{ get; set; }



        

    }
}
