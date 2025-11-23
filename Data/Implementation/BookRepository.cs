using WebApplication1.DbContexts;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;
    public BookRepository(AppDbContext context)
    {
        _context = context;
    }


    public IEnumerable<Book> GetAll()
    {
        return _context.Books.ToList();
    }
    public Book? GetById(int id)
    {
        return _context.Books.Find(id);
    }
    public async Task<Book> Add(Book book)
    {
        try
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;

        }
        catch (Exception ex)
        {
            throw;
        }

    }
    public void Update(Book book)
    {
        _context.SaveChanges();

    }
    public void Delete(int id)
    {
        var book = _context.Books.Find(id);
        if (book != null)
        {
            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}