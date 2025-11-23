namespace WebApplication1.Services.Interface
{
    public interface ILoansService
    {
        IEnumerable<Loan> GetAllBorrowed(string memberId);
        List<Loan> GetAllHistory(string memberId);
        Task<string> Borrow(int bookId, string memberId);
        Task<string> Return(int bookId, string memberId);
    }
}
