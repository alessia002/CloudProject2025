using System.ComponentModel.DataAnnotations;

namespace CloudProject2025.Models
{
    public class Film
    {
        public int Id { get; set; }
        [Required]
        public string? Title { get; set; }
        public string? Plot { get; set; }
        public int Year { get; set; }
        [Required]
        public string? Genre{ get; set; }
        [Required]
        public string? Director{ get; set; }
        public float? MyRate{ get; set; }



        

    }
}
