public interface IBookService
{
    IEnumerable<Book> GetAll();
    Book? GetById(int id);
    Task<Book> Add(Book book);
    string Update(Book book);
    void Delete(int id);
}