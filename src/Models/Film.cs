using System.ComponentModel.DataAnnotations;
using System.Web;

namespace TestApp.Models
{
    public class Film
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Год выпуска")]
        public int? Year { get; set; }

        [Required]
        [Display(Name = "Режиссер")]
        public string Producer { get; set; }

        [Display(Name = "Пользователь")]
        public string UserId { get; set; }
                
        [Display(Name = "Постер")]
        public byte[] Poster { get; set; }
    }
}