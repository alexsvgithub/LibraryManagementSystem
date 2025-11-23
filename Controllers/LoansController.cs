using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services.Interface;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private readonly ILoansService _service;
    public LoansController(ILoansService service)
    {
        _service = service;
    }

    [HttpGet("borrowed/{memberId}")]
    public IActionResult GetAllBorrowed(string memberId)
    {
        var loans = _service.GetAllBorrowed(memberId);
        return Ok(loans);
    }

    [HttpGet("history/{memberId}")]
    public IActionResult GetAllHistory(string memberId)
    {
        var loans = _service.GetAllHistory(memberId);
        return Ok(loans);
    }

    [HttpPost("borrow")]
    public async Task<IActionResult> Borrow([FromQuery] int bookId, [FromQuery] string memberId)
    {
        var result = await _service.Borrow(bookId, memberId);
        return Ok(result);
    }

    [HttpPost("return")]
    public async Task<IActionResult> Return([FromQuery] int bookId, [FromQuery] string memberId)
    {
        var result = await _service.Return(bookId, memberId);
        return Ok(result);
    }



}