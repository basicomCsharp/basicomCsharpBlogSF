using BlogSF.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BlogSF.BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ICommentRepositories _comment;
        private IUserRepositories _user;
        private IBookRepositories _book;
        private ITagRepositories _tag;

        public UserController(IBookRepositories book, ICommentRepositories comment, IUserRepositories user, ITagRepositories tag)
        {
            _comment = comment;
            _user = user;
            _book = book;
            _tag = tag;
        }
        
        [HttpGet]
        [Route("GetUserById")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var value = await _user.Get(id);
                if (value == null)
                    return StatusCode(400, "Пользователь не найден!");

                return StatusCode(200, value);
            }
            catch
            { }
            return NotFound();
        }

        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {

            var value = await _user.GetAll();
            return StatusCode(200, value);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(User value)
        {
            value.Id = Guid.NewGuid();
            value.FirstName = "Иван"+ DateTime.Now.Day.ToString(); ;
            value.LastName = "Иванов" + DateTime.Now.ToString(); ;
            value.Email = DateTime.Now.Hour.ToString() +"@mail.ru";
            value.login = "login";
            value.password = "password";
            await _user.Create(value);
            return StatusCode(200, value);
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(User value)
        {

            value.Id = new Guid("384c43f2-4b37-470b-93fc-947000a3acc9");
            value.FirstName = "Иван";
            value.LastName= "Ивановcкий";
            value.Email = "ivan@mail.ru";
            value.login = "login2";
            value.password = "password3";
            
            try
            {
                await _user.Update(value);
                return StatusCode(200);
            }
            catch
            { }
            return NoContent();
        }
        [HttpDelete]
        [Route("")]
        public async Task<IActionResult> Delite(Guid id)
        {
            try
            {
                await _user.Delete(id);
                return StatusCode(200, "Пользователь удалён");
            }
            catch
            { }
            return NotFound();
        }
    }
}
