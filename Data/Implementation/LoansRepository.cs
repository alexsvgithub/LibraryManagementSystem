using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication1.Data.Interface;
using WebApplication1.DbContexts;

namespace WebApplication1.Data.Implementation
{
    public class LoansRepository : ILoansRepository
    {
        private readonly AppDbContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly int MaxAllowedBorrowWithouReturn = 3;

        public LoansRepository(AppDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        public IEnumerable<Loan> GetAllCurrentlyBorrowed(string memberId)
        {
            var loans = _context.Loans
                .Where(l => l.MemberId == memberId && l.isReturned == false)
                .AsNoTracking()
                .ToList();

            return loans;
        }
        public List<Loan> GetByBorrowedHistory(string memberIdd)
        {
            return _context.Loans.Where(x=>x.MemberId == memberIdd).OrderByDescending(x=>x.BorrowedAt).AsNoTracking().ToList();
        }

        public async Task LoanBook(int bookId, string memberId)
        {
            try
            {
                Loan loan = new Loan
                {
                    BookId = bookId,
                    MemberId = memberId,
                    BorrowedAt = DateTime.Now,
                    isReturned = false
                };

                _context.Loans.Add(loan);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task ReturnBook(string id)
        {   
            var loan = new Loan { Id = id };
            _context.Loans.Attach(loan);

            loan.isReturned = true;
            loan.ReturnedAt = DateTime.Now;

            _context.Entry(loan).Property(x => x.isReturned).IsModified = true;
            _context.Entry(loan).Property(x => x.ReturnedAt).IsModified = true;

            await _context.SaveChangesAsync();

        }

    }
}
