using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using UsersProject.Data;
using UsersProject.Models;
using UsersProject.Models.RequestResponse;

namespace UsersProject.Controllers
{
    [Route("/api/[controller]")]
    public class UsersController : Controller
    {
        private readonly AppDbContext db;

        public UsersController(AppDbContext context)
        {
            db = context; //Создаём экземпляр дб контекста для операций с бд
        }

        [HttpGet]
        public IEnumerable<User> Get() => db.Users.ToList();

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = db.Users.SingleOrDefault(u => u.Id == id);
            
            if(user == null) { return NotFound(); }
            return Ok(user);
        }

        [HttpPost] //Этот запрос выполняется когда уже известны все атрибуты класса, тоесть они переданы прямо
        public ActionResult<ResponseSuccess> Post([FromBody] RequestUserCreate request)
        {
            if(!ModelState.IsValid) //Проверяет атрибуты Required
            {
                return BadRequest(new ResponseSuccess { Success = false, Message = "Неверные данные юзера" });
            }

            var user = new User { Name = request.User.Name, Age = request.User.Age };

            db.Add(user);
            db.SaveChanges();
            return Ok(new ResponseSuccess { Success = true, Message = "Юзер был успешно добавлен" });
        }

        [HttpPut]
        public ActionResult<ResponseSuccess> Put([FromBody] RequestUserUpdate request)
        {
            if (!ModelState.IsValid) //Проверяет атрибуты Required
            {
                return BadRequest(new ResponseSuccess { Success = false, Message = "Неверные данные юзера" });
            }
            var storedUser = db.Users.SingleOrDefault(u => u.Id == request.Id);
            if(storedUser == null) { return NotFound(); }
            storedUser.Name = request.User.Name;
            storedUser.Age = request.User.Age;
            db.SaveChanges();
            return Ok(new ResponseSuccess { Success = true, Message = "Данные юзера были успешно изменены" });
        }

        [HttpDelete]
        public ActionResult<ResponseSuccess> Delete([FromBody] RequestUserDelete request)
        {
            var user = db.Users.SingleOrDefault(u => u.Id == request.Id);
            if (user == null) 
            { 
                return NotFound(new ResponseSuccess { Success = false, Message = "Неверные данные юзера" }); 
            }
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok(new ResponseSuccess { Success = true, Message = "Юзер был успешно удалён" });
        }
    }
}