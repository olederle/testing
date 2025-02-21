namespace UnitTests.Account;

public class AccountService(IBookService bookService, IEmailSender emailSender)
{
    private readonly IBookService bookService = bookService;
    private readonly IEmailSender emailSender = emailSender;

    public IEnumerable<string> GetAllBooksForCategory(string categoryId)
    {
        IEnumerable<string> allBooks = bookService.GetBooksForCategory(categoryId);
        return allBooks;
    }

    public string GetBookISBN(string categoryId, string searchTerm)
    {
        var allBooks = bookService.GetBooksForCategory(categoryId);
        var foundBook = allBooks
            .Where(x => x.Contains(searchTerm.ToUpper(), StringComparison.Ordinal))
            .FirstOrDefault();
        if (foundBook == null)
        {
            return string.Empty;
        }
        return bookService.GetISBNFor(foundBook);
    }

    public void SendEmail(string emailAddress, string bookTitle)
    {
        string subject = "Awesome Book";
        string body = $"Hi,\n\nThis book is awesome: {bookTitle}.\nCheck it out.";
        emailSender.SendEmail(emailAddress, subject, body);
    }
}
