
using WebApplication1.Data.Interface;
using WebApplication1.Services.Interface;

namespace WebApplication1.Services.Implementation
{
    public class LoansService : ILoansService
    {
        private readonly ILoansRepository _loansRepository;
        private readonly IBookRepository _bookRepository;
        public LoansService(ILoansRepository loansRepository, IBookRepository bookRepository)
        {
            _loansRepository = loansRepository;
            _bookRepository = bookRepository;
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

            var bookLoaned = _bookRepository.GetById(bookId);
            if (bookLoaned == null)
            {
                return "Book not found";
            }

            if(bookLoaned.NoOfCopiesAvailable == 0 )
            {
                return "No Books Available To Borrow.";
            }

            bookLoaned.NoOfCopiesAvailable -= 1;
            _bookRepository.Update(bookLoaned);

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
            await _loansRepository.ReturnBook(loan.Id,bookId);

            return "Book Returned Successfully";
        }


    }
}
