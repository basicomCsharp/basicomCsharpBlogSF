﻿using BlogSF.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BlogSF.BLL.Controllers
{
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

        [HttpPost]
        [Route("CreateBook")]
        public async Task<IActionResult> Create(Book value)
        {
            value.Id = Guid.NewGuid();
            value.Author = "Иван" + DateTime.Now.Day.ToString();
            value.Name = "Иванов" + DateTime.Now.ToString(); 
            value.Content = "abrakadabra";
            value.CreatedData = DateTime.Now;
            
            await _book.Create(value);
            return StatusCode(200, value);
        }

        [HttpPut]
        [Route("UpdateBook")]
        public async Task<IActionResult> Update(Book value)
        {
            try
            {
                await _book.Update(value);
                return StatusCode(200,"Обновление статьи прошло успешно");
            }
            catch
            { }
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteBook")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                await _book.Delete(id);
                return StatusCode(200, "Статья удалена!");
            }
            catch
            { }
            return NotFound();
        }

        [HttpGet]
        [Route("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            var value = await _book.GetAll();

            return StatusCode(200, value);
        }

        [HttpGet]
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
            { }
            return NotFound();
        }
    }
}