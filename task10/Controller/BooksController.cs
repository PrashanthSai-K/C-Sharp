using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using task10.Contarcts;
using task10.Interface;
using task10.Services;

namespace task10.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepo;

        public BooksController(IBookRepository bookRepo)
        {
            _bookRepo = bookRepo;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooks(CreateBooks book)
        {
            try
            {
                await _bookRepo.Create(book);
                return Ok(new { Message = "Created successfully." });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = $"{ex.Message} : Error durring creation" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            try
            {
                var books = await _bookRepo.GetAllBooks();
                return Ok(new { Message = "ok", data = books });
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var book = await _bookRepo.GetById(id);

                if (book == null)
                {
                    return NotFound(new { Message = "Book fot found for the given id" });
                }

                return Ok(new { Message = "Book found", data = book });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = $"{ex.Message} Error during Find by Id" });
                throw;
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateBooks(int id, UpdateBooks book)
        {
            try
            {
                await _bookRepo.Update(id, book);
                return Ok(new { Message = "Updated successfully" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = $"{ex.Message} Error during updation" });
                throw;
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteBooks(int id)
        {
            try
            {
                await _bookRepo.Delete(id);
                return Ok(new { Message = "Deleted successfully" });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { Message = $"{ex.Message} Error during deletion" });

                throw;
            }
        }
    }
}
