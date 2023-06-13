namespace DBMS_Projektarbeit.Models
{
    public interface ILibraryService
    {
        IEnumerable<Book> GetBooks();
        void InsertBook(Book employee);
        void UpdateBook(long id, Book employee);
        Book SingleBook(long id);
        void DeleteBook(long id);
    }
}
