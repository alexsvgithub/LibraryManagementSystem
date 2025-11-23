
using WebApplication1.Data.Interface;
using WebApplication1.Services.Interface;

namespace WebApplication1.Services.Implementation
{
    public class LoansService : ILoansService
    {
        private readonly ILoansRepository _loansRepository;
        public LoansService(ILoansRepository loansRepository)
        {
            _loansRepository = loansRepository;
        }
        public IEnumerable<Loan> GetAllBorrowed(string memberId)
        {
            return _loansRepository.GetAllCurrentlyBorrowed(memberId);
        }
        public List<Loan> GetAllHistory(string memberId)
        {
            return _loansRepository.GetByBorrowedHistory(memberId);
        }
        public async Task<string> Borrow(int bookId, string memberId)
        {
            var currentlyBorrowed = _loansRepository.GetAllCurrentlyBorrowed(memberId).Count();
            if (currentlyBorrowed >= 3)
            {
                return "User Exceeds maximum allowed borrowed books Limit";
            }
            var existingLoan = _loansRepository.GetAllCurrentlyBorrowed(memberId)
                .FirstOrDefault(l => l.BookId == bookId);

            if (existingLoan != null)
            {
                return "Book is already borrowed by the member";
            }

            await _loansRepository.LoanBook(bookId, memberId);
            return "Book Issued Successfully";
        }
        public async Task<string> Return(int bookId, string memberId)
        {
            var loan = _loansRepository.GetAllCurrentlyBorrowed(memberId)
                .FirstOrDefault(l => l.BookId == bookId);
            if (loan == null)
            {
                return "No active loan found for this book and member";
            }
            await _loansRepository.ReturnBook(loan.Id);

            return "Book Returned Successfully";
        }


    }
}
