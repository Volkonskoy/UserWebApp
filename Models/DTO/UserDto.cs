using System.ComponentModel.DataAnnotations;

namespace UsersProject.Models.DTO
{
    public class UserDto
    {
        [Required] public String Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}
