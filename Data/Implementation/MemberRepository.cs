using WebApplication1.Data.Interface;
using WebApplication1.DbContexts;

namespace WebApplication1.Data.Implementation
{
    public class MemberRepository : IMemberRepository
    {
        private readonly AppDbContext _context;
        public MemberRepository(AppDbContext context) { 
            _context = context;
        }

        public IEnumerable<Member> GetAll()
        {
            return _context.Members.ToList();
        }

        public Member? GetById(string id)
        {
            return _context.Members.Find(id);
        }

        public async Task<Member> Add(Member member)
        {
            try
            {
                await _context.Members.AddAsync(member);
                await _context.SaveChangesAsync();
                return member;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Update(Member member)
        {
            _context.SaveChanges();
        }

        public string Delete(string id)
        {
            var member = _context.Members.Find(id);
            if (member != null)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
                return "Member Deleted Successfully";
            }
            else
            {
                return $"No Member Found with the Id = {id}";
            }
        }
    }
}
