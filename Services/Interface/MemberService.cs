using WebApplication1.Data.Implementation;
using WebApplication1.Data.Interface;
using WebApplication1.Services.Implementation;

namespace WebApplication1.Services.Interface
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly ILoansRepository _loansRepository;

        public MemberService(IMemberRepository memberRepository, ILoansRepository loansRepository)
        {
            _memberRepository = memberRepository;
            _loansRepository = loansRepository;
        }

        public IEnumerable<Member> GetAll() => _memberRepository.GetAll();

        public Member? GetById(string id)
        {
            var user = _memberRepository.GetById(id);
            if (user == null)
            {
                return null;
            }
            else
            {
                return user;
            }
        }

        public async Task<Member> Add(Member member)
        {
            member.Id = Guid.NewGuid().ToString();
            return await _memberRepository.Add(member);
        }

        public string Update(Member member)
        {

            var existing = _memberRepository.GetById(member?.Id);
            if (existing == null) return null;

            if (member.isActive == false)
            {
                var borrowedBooks = _loansRepository.GetAllCurrentlyBorrowed(member.Id);
                if (borrowedBooks.Any())
                {
                    return "Cannot deactivate member with borrowed books.";
                }
            }

            existing.Name = member.Name;
            existing.Email = member.Email;
            existing.isActive = member.isActive;
            _memberRepository.Update(member);

            return "User Updated Successfully";




        }
        public string Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return "Invalid ID";
            }
            return _memberRepository.Delete(id);
        }
    }
}
