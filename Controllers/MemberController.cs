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
            return Ok(users);
        }

        [HttpGet("GetUserById/{id}")]   
        public IActionResult GetUserById(string id)
        {
            var user = _memberService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] Member member)
        {
            if (member == null)
            {
                return BadRequest("Member is null.");
            }
            member.Id = Guid.NewGuid().ToString();
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
            var existing = _memberService.GetById(member.Id);
            if (existing == null) return NotFound();

            existing.Name = member.Name;
            existing.Email = member.Email;
            existing.isActive = member.isActive;


            _memberService.Update(existing);
            return NoContent();

            //if (member == null || string.IsNullOrEmpty(member.Id))
            //{
            //    return BadRequest("Member is null or ID mismatch.");
            //}
            //var existingMember = _memberService.GetById(member.Id);
            //if (existingMember == null)
            //{
            //    return NotFound();
            //}
            //_memberService.Update(member);
            //return NoContent();
        }

        [HttpDelete("DeleteUser/{id}")]
        public IActionResult DeleteUser(string id)
        {
            var existingMember = _memberService.GetById(id);
            if (existingMember == null)
            {
                return NotFound();
            }
            _memberService.Delete(id);
            return NoContent();
        }
    }
}
