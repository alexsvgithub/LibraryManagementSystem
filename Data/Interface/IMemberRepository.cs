using System;

namespace WebApplication1.Data.Interface
{
    public interface IMemberRepository
    {

        IEnumerable<Member> GetAll();
        Member? GetById(string id);
        Task<Member> Add(Member member);
        void Update(Member member);
        string Delete(string id);

    }
}
