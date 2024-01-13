using System.ComponentModel.DataAnnotations;

namespace BulkyBookWeb.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
