using UsersProject.Models.DTO;

namespace UsersProject.Models.RequestResponse
{
    public class RequestUserUpdate
    {
        public int Id { get; set; }
        public UserDto User { get; set; } = new UserDto();
    }
}
