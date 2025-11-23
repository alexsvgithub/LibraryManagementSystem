public interface IBookRepository
{
    IEnumerable<Book> GetAll();
    Book? GetById(int id);
    Task<Book> Add(Book book);
    void Update(Book book);
    void Delete(int id);
}