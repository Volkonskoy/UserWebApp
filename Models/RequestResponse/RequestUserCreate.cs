using UsersProject.Models.DTO;

namespace UsersProject.Models.RequestResponse
{
    public class RequestUserCreate
    {
        public UserDto User {  get; set; } = new UserDto();
    }
}
