using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services.Implementation;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;
        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _memberService.GetAll();
            return users is null ? NotFound() : Ok(users);
        }

        [HttpGet("GetUserById/{id}")]   
        public IActionResult GetUserById(string id)
        {
            var user =  _memberService.GetById(id);
            return user is null ? NotFound() : Ok(user);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] Member member)
        {
            if (member == null)
            {
                return BadRequest("Member is null.");
            }
            var addedMember = await _memberService.Add(member);
            return CreatedAtAction(nameof(GetUserById), new { id = addedMember.Id }, addedMember);
        }

        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser([FromBody] Member member)
        {
            if (member == null || string.IsNullOrEmpty(member.Id))
            {
                return BadRequest("Member is null or ID mismatch.");
            }

            var updatedUser = _memberService.Update(member);
            return updatedUser is null ? NotFound() : Ok(updatedUser);
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(string id)
        {
            
            var status = _memberService.Delete(id);
            return NoContent();
        }
    }
}
