using WebApplication1.Data.Interface;
using WebApplication1.Services.Implementation;

namespace WebApplication1.Services.Interface
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repo;

        public MemberService(IMemberRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Member> GetAll() => _repo.GetAll();

        public Member? GetById(string id) => _repo.GetById(id);

        public async Task<Member> Add(Member member)
        {
            return await _repo.Add(member);
        }
        public void Update(Member member) => _repo.Update(member);
        public void Delete(string id) => _repo.Delete(id);
    }
}
