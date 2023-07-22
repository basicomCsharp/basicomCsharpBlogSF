using BlogSF.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

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
        //[HttpGet(Name = "GetUserById")]
        //[Route("")]
        //public async Task<IActionResult> GetById(Guid id)
        //{
        //    try
        //    {
        //        var volume = await _user.Get(id);
        //        if (volume == null)
        //            return StatusCode(400, "Комментарий не найден!");

        //        return StatusCode(200, volume);
        //    }
        //    catch
        //    { }
        //    return NotFound();
        //}
        //[HttpGet(Name = "GetAllUsers")]
        //[Route("")]
        //public async Task<IActionResult> GetAllUsers()
        //{

        //    var volume = await _user.GetAll();
        //    return StatusCode(200);
        //}

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Create(User volume)
        {
            volume.Id= Guid.NewGuid();
            volume.FirstName = "Иван2";
            volume.LastName = "Иванов2";
            volume.Email = "mail2@mail.ru";
            volume.login = "login2";
            volume.password = "password2";
            await _user.Create(volume);
            return StatusCode(200, volume);
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
