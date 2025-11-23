namespace WebApplication1.Services.Implementation
{
    public interface IMemberService
    {
        IEnumerable<Member> GetAll();
        Member? GetById(string id);
        Task<Member> Add(Member member);
        void Update(Member member);
        void Delete(string id);
    }
}
