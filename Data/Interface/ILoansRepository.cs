using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Interface
{
    public interface ILoansRepository
    {
        IEnumerable<Loan> GetAllCurrentlyBorrowed(string memberId);
        List<Loan> GetByBorrowedHistory(string memberIdd);

        Task LoanBook(int bookId, string memberId);
        Task ReturnBook(string id, int bookId);
    }
}
