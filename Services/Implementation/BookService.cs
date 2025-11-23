public class BookService : IBookService
{
    private readonly IBookRepository _repo;
    public BookService(IBookRepository repo) => _repo = repo;

    public IEnumerable<Book> GetAll() => _repo.GetAll();
    public Book? GetById(int id) => _repo.GetById(id);
    public async Task<Book> Add(Book book)
    {
        return await _repo.Add(book);
    }
    public void Update(Book book) => _repo.Update(book);
    public void Delete(int id) => _repo.Delete(id);
}