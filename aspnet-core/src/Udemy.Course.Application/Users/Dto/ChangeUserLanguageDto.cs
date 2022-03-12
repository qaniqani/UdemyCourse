using System.ComponentModel.DataAnnotations;

namespace Udemy.Course.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}