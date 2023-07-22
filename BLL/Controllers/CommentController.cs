﻿
using BlogSF.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlogSF.Controller
{
    //В контроллере комментариев реализовать логику
    //  создания, редактирования, удаления комментария,
    //  а также логику получения всех комментариев и только одного комментария по его идентификатору
    [ApiController]
    [Route("[controller]")]
    public class CommentController: ControllerBase
    {        
        private ICommentRepositories _comment;
        private IUserRepositories _user;
        private IBookRepositories _book;
        private ITagRepositories _tag;

        public CommentController(ICommentRepositories comment, IUserRepositories user, IBookRepositories book, ITagRepositories tag)
        {           
            _comment = comment;
            _user = user;
            _book= book;
            _tag= tag;
        }
 
        [HttpPost]
        [Route("CreateComment")]
        public async Task<IActionResult> CreateComment(Comment newComment)
        {
            newComment = new Comment();
            newComment.Id = Guid.NewGuid();
            newComment.Title = DateTime.Now.Date.ToString();
            newComment.Text = DateTime.Now.ToString();
            await _comment.Create(newComment);
            return StatusCode(200, newComment);
        }

        [HttpPut]
        [Route("UpdateComment")]
        public async Task<IActionResult> UpdateComment(Comment thisComment)            
        {
            try
            {
                await _comment.Update(thisComment);
                return StatusCode(200,thisComment);
            }
            catch
            { }
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteCommwnt")]
        public async Task<IActionResult> DeliteComment(Guid id)
        {
            try
            {
                await _comment.Delete(id);
                return StatusCode(200, " Комментарий удалён");
            }
            catch
            {}
            return NotFound();
        }

       
        [HttpGet]
        [Route("GetAllComments")]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _comment.GetAll();

            return  StatusCode(200,comments);
        }

        //[HttpGet(Name = "GetCommentById")]
        [HttpGet]
        [Route("GetCommentById")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            try
            {
                var comment = await _comment.Get(id);
                if (comment == null)
                    return StatusCode(400, "Комментарий не найден!");

                return StatusCode(200,comment);
            }
            catch
            { }
            return NotFound();
        }
    }    
}
