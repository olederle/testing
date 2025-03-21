using Moq;
using UnitTests.Account;

namespace UnitTests.Tests.Account;

public class AcountServiceTest
{
    [Fact]
    public void GetAllBooksForCategory_returns_list_of_available_books()
    {
        Mock<IBookService> bookServiceStub = new Mock<IBookService>();

        bookServiceStub
            .Setup(x => x.GetBooksForCategory("UnitTesting"))
            .Returns(
                [
                    "The Art of Unit Testing",
                    "Test-Driven Development",
                    "Working Effectively with Legacy Code",
                ]
            );

        Mock<IEmailSender> emalServiceStub = new Mock<IEmailSender>();

        AccountService accountService = new AccountService(
            bookServiceStub.Object,
            emalServiceStub.Object
        );
        IEnumerable<string> result = accountService.GetAllBooksForCategory("UnitTesting");
        Assert.Equal(3, result.Count());
    }

    // TODO 1: add test method for GetBookISBN

    // TODO 2: add a test for a case insensitive check, should fail -> fix code


    // TODO 3: add test method for SendEmail
}
