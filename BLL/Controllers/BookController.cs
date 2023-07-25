using AutoMapper;
using BlogSF.DAL.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;

namespace BlogSF.BLL.Controllers
{
    /***
     * В контроллере статей реализовать логику создания, редактирования, удаления статьи, 
     * а также логику получения всех статей и всех статей определённого автора по его идентификатору
     ***/
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private ICommentRepositories _comment;
        private IUserRepositories _user;
        private IBookRepositories _book;
        private ITagRepositories _tag;
        
        public BookController(IBookRepositories book, ICommentRepositories comment, IUserRepositories user, ITagRepositories tag)
        {            
            _comment = comment;
            _user = user;
            _book = book;
            _tag = tag;
        }
        /// <summary>
        /// метод создания статьи
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "user, moderator")]
        [Route("CreateBook")]
        public async Task<IActionResult> Create([FromBody]Book value)
        {
#if DEBUG
            var userIdentity = (ClaimsIdentity)User.Identity;
            value.Id =  Guid.NewGuid();            
            value.Author = userIdentity.Name;
            value.Name = "Статья №" + DateTime.Now.ToString(); 
            value.Content = "abrakadabra";
            value.CreatedData = DateTime.Now;
#endif            
            await _book.Create(value);
            return StatusCode(200, value);
        }
        /// <summary>
        /// метод обновления статьи
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "user, moderator")]
        [Route("UpdateBook")]
        public async Task<IActionResult> Update([FromBody] Book value)
        {
            try
            {
                var userIdentity = (ClaimsIdentity)User.Identity;                
                var claims = userIdentity.Claims;                
                var roles2 = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

                if (roles2[0].Value == "user" && (value.Author != userIdentity.Name))
                {
                    return StatusCode(400, "Только автор и модератор имеет право обновлять статью");
                }                
                await _book.Update(value);
                return StatusCode(200, "Обновление статьи прошло успешно");                                    
            }
            catch
            {
                return StatusCode(400, "Только автор и модератор имеет право обновлять статью");
            }
            
        }
        /// <summary>
        /// метод удаления статьи по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "user, moderator")]
        [Route("DeleteBook")]
        public async Task<IActionResult> DeleteBook(Guid id, Book theBook)
        {
            try
            {
                var userIdentity = (ClaimsIdentity)User.Identity;
                var claims = userIdentity.Claims;
                var roles2 = claims.Where(c => c.Type == ClaimTypes.Role).ToList();

                if (roles2[0].Value == "user" && (theBook.Author != userIdentity.Name))
                {
                    return StatusCode(400, "Только автор и модератор имеет право обновлять статью");
                }
                await _book.Delete(id);
                return StatusCode(200, "Статья удалена!");
            }
            catch
            {
                return NotFound();
            }
            
        }
        /// <summary>
        /// метод получения всех статей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var value = await _book.GetAll();

            return StatusCode(200, value);
        }
        /// <summary>
        /// получение статьи по id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetBookById")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            try
            {  
                var value = await _book.Get(id);
                if (value == null)
                    return StatusCode(400, "Статья не найдена!");

                return StatusCode(200, value);
            }
            catch
            {
                return NotFound();
            }            
        }
    }
}
