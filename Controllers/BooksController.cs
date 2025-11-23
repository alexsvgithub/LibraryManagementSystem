using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IBookService _service;
    public BooksController(IBookService service) 
    { 
        _service = service; 
    }

    [HttpGet]
    public ActionResult<IEnumerable<Book>> Get()
    {
        return Ok(_service.GetAll());
    } 

    [HttpGet("{id}")]
    public ActionResult<Book> Get(int id)
    {
        var book = _service.GetById(id);
        return book is null ? NotFound() : Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Book book)
    {
        var result = await _service.Add(book);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPut]
    public IActionResult Put(Book book)
    {
        if (book.Id == null)  return BadRequest();
        _service.Update(book);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _service.Delete(id);
        return NoContent();
    }
}