using AutoMapper;
using BlogSF.DAL.Models;
using BlogSF.DAL.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Authentication;
using System.Security.Claims;

namespace BlogSF.BLL.Controllers
{
    /*** В контроллере пользователей реализовать логику регистрации, редактирования, удаления пользователя, 
     * а также логику получения всех пользователей и логику получения только одного пользователя по его идентификатору
    ***/
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private ICommentRepositories _comment;
        private IUserRepositories _user;
        private IBookRepositories _book;
        private ITagRepositories _tag;
        private IMapper _mapper;

        public UserController(IBookRepositories book, ICommentRepositories comment, IUserRepositories user, ITagRepositories tag, IMapper mapper)
        {
            _comment = comment;
            _user = user;
            _book = book;
            _tag = tag;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Authenticate")]
        public async Task<UserViewModel> Authenticate(string login, string password)
        {
            if (String.IsNullOrEmpty(login) ||
              String.IsNullOrEmpty(password))
                throw new ArgumentNullException("Запрос не корректен");

            var user = await _user.GetByLogin(login);
            if (user is null)
                throw new AuthenticationException("Пользователь на найден");

            if (user.Password != password)
                throw new AuthenticationException("Введенный пароль не корректен");

            var claims = new List<Claim>() //Подлкючить using дженерики и клаймы using System.Security.Claims; -системный класс
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims,
                "AddCookies",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        
            return _mapper.Map<UserViewModel>(user);
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
        [Route("CreateUser")]
        public async Task<IActionResult> Create(User value)
        {
#if DEBUG
            value.Id = Guid.NewGuid();
            value.FirstName = "Пётр" + DateTime.Now.Day.ToString(); ;
            value.LastName = "Петров" + DateTime.Now.ToString(); ;
            value.Email = DateTime.Now.Hour.ToString() +"@mail.ru";
            value.Login = "login"+ value.FirstName;
            value.Password = "password" + value.LastName;
            value.Role = new Role() { Id = Guid.NewGuid(), Name ="user" };
#endif
            await _user.Create(value);
            return StatusCode(200, value);
        }
        
        [HttpPut]
        [Route("UpdateUser")]
        public async Task<IActionResult> Update(User value)
        {
#if DEBUG
            value.Id = new Guid("384c43f2-4b37-470b-93fc-947000a3acc9");
            value.FirstName = "Иван";
            value.LastName= "Ивановcкий";
            value.Email = "ivan@mail.ru";
            value.Login = "login2";
            value.Password = "password3";
#endif         
            try
            {
                await _user.Update(value);
                return StatusCode(200);
            }
            catch
            { }
            return NoContent();
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        [Route("DeleteUser")]
        public async Task<IActionResult> Delete(Guid id)
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
