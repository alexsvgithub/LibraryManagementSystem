namespace WebApplication1.Services.Implementation
{
    public interface IMemberService
    {
        IEnumerable<Member> GetAll();
        Member? GetById(string id);
        Task<Member> Add(Member member);
        string Update(Member member);
        string Delete(string id);
    }
}
