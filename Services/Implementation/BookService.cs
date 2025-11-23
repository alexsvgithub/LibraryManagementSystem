public class BookService : IBookService
{
    private readonly IBookRepository _repo;
    public BookService(IBookRepository repo) => _repo = repo;

    public IEnumerable<Book> GetAll() => _repo.GetAll();
    public Book? GetById(int id) => _repo.GetById(id);
    public async Task<Book> Add(Book book)
    {
        book.Id = null;
        return await _repo.Add(book);
    }
    public string Update(Book book)
    {
        var b = GetById(book.Id ?? int.MaxValue);
        if(b == null) return "Book not found";

        b.Title = book.Title;
        b.Author = book.Author;
        b.NoOfCopiesAvailable = book.NoOfCopiesAvailable;
        _repo.Update(b);
        return "Updated Successfully";
    }
    public void Delete(int id) => _repo.Delete(id);
}