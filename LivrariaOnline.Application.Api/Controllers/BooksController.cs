using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LivrariaOnline.Application.Api.Dto;
using LivrariaOnline.Domain.Entities;
using LivrariaOnline.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaOnline.Application.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _service;
        private readonly IMapper _mapper;

        public BooksController(IBookService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var listBooks = _mapper.ProjectTo<BookDto>(await _service.GetAsync());
                return Ok(listBooks.OrderBy(b => b.Nome));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Books/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var book = await _service.GetByIdAsync(id);
                if (book != null)
                    return Ok(_mapper.Map<BookDto>(book));
                return NotFound(id);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{nomeAutor}", Name = "FindByAutor")]
        public async Task<IActionResult> FindByAutor(string nomeAutor)
        {
            try
            {
                var books = _mapper.ProjectTo<BookDto>(await _service.GetByAuthor(nomeAutor));
                if (books == null || books.Count() == 0)
                    return NotFound(nomeAutor);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{bookName}", Name = "FindByName")]
        public async Task<IActionResult> FindByName(string bookName)
        {
            try
            {
                var books = _mapper.ProjectTo<BookDto>(await _service.GetByName(bookName));
                if (books == null || books.Count() == 0)
                    return NotFound(bookName);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{Isbn}", Name = "FindByIsbn")]
        public async Task<IActionResult> FindByIsbn(string Isbn)
        {
            try
            {
                var book = _mapper.Map<BookDto>(await _service.GetByIsbn(Isbn));
                if (book == null)
                    return NotFound(Isbn);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{price}", Name = "FindByPrice")]
        public async Task<IActionResult> FindByPrice(decimal price)
        {
            try
            {
                var books = _mapper.ProjectTo<BookDto>(await _service.GetByPrice(price));
                if (books == null || books.Count() == 0)
                    return NotFound(price);
                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{starDate}/{endDate}", Name = "FindBeetwenDatePublish")]
        public async Task<IActionResult> FindBeetwenDatePublish(DateTime starDate, DateTime endDate)
        {
            try
            {
                var books = _mapper.ProjectTo<BookDto>(await _service.GetBeetwenDatePublish(starDate, endDate));
                if (books == null || books.Count() == 0)
                    return NotFound(new { starDate, endDate });

                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Books
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] BookDto bookDto)
        {
            try
            {
                var book = _service.FindAsync(b => b.Isbn.Equals(bookDto.Isbn)).GetAwaiter().GetResult().SingleOrDefault();
                if (book != null)
                    return BadRequest(bookDto);

                book = _mapper.Map<Book>(bookDto);
                if (book.Invalid)
                    return BadRequest(bookDto);

                return Ok(await _service.AddAsync(_mapper.Map<Book>(bookDto)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDto bookDto)
        {
            if (id.ToString() != bookDto.Id)
                return BadRequest();

            var hasBook = await _service.GetByIdAsync(id);
            if (hasBook == null)
                return NotFound(id);

            try
            {
                var book = _mapper.Map<Book>(bookDto);
                if (book.Invalid)
                    return BadRequest(book.ValidationResult.Errors);

                return Ok(await _service.UpdateAsync(book));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var book = await _service.GetByIdAsync(id);
                if (book == null)
                    return NotFound(id);

                await _service.DeleteAsync(id);
                return Ok(book);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
