using BlogSF.DAL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BlogSF.BLL.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TagController : ControllerBase
    {
        private ICommentRepositories _comment;
        private IUserRepositories _user;
        private IBookRepositories _book;
        private ITagRepositories _tag;

        public TagController(IBookRepositories book, ICommentRepositories comment, IUserRepositories user, ITagRepositories tag)
        {
            _comment = comment;
            _user = user;
            _book = book;
            _tag = tag;
        }

        [HttpPost]
        [Route("CreateTag")]
        public async Task<IActionResult> Create(Tag value)
        {
            await _tag.Create(value);
            return StatusCode(200, value);
        }

        [HttpPut]
        [Route("UpdateTag")]
        public async Task<IActionResult> Update(Tag Value)
        {
            try
            {
                await _tag.Update(Value);
                return StatusCode(200, "Tag обновлён");
            }
            catch
            { }
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteTag")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _tag.Delete(id);
                return StatusCode(200, "Tag удалён");
            }
            catch
            { }
            return NotFound();
        }

        [HttpGet]
        [Route("GetAllTags")]
        public async Task<IActionResult> GetAllTags()
        {
            var value = await _tag.GetAll();

            return StatusCode(200, value);
        }

        [HttpGet]
        [Route("GetTagById")]
        public async Task<IActionResult> GetTagById(Guid id)
        {
            try
            {
                var value = await _tag.Get(id);
                if (value == null)
                    return StatusCode(400, "Tag не найден!");

                return StatusCode(200, value);
            }
            catch
            { }
            return NotFound();
        }
    }
}
