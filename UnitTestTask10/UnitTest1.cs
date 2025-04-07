
namespace UnitTestTask10;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using task10.AppDataContext;
using task10.Contarcts;
using task10.Controller;
using task10.Mappings;
using task10.Services;

public class BooksControllerTest
{

    private readonly BooksController _bookController;
    private readonly ApplicationDbContext _context;

    public BooksControllerTest()
    {
        var db = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "testdb")
            .Options;

        _context = new ApplicationDbContext(db);

        var logger = new LoggerFactory().CreateLogger<BookService>();

        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile<MappingProfile>();
        }).CreateMapper();

        var Bookservice = new BookService(_context, logger, mapper);

        _bookController = new BooksController(Bookservice);

    }

    [Fact]
    public async Task GetBooksTest()
    {
        var result = await _bookController.GetBooks();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }


    [Fact]
    public async Task CreateBooksTest()
    {
        var book = new CreateBooks
        {
            Name = "Sample Book",
            Author = "Sample author",
            Price = 1234.5,
            IsCompleted = false
        };

        var result = await _bookController.CreateBooks(book);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }

    [Fact]
    public async Task GetByIdTest()
    {

        var result = await _bookController.GetById(1);
        var notFoundResult = await _bookController.GetById(2);

        // Then
        var okResult = Assert.IsType<OkObjectResult>(result);
        var notOkResult = Assert.IsType<ObjectResult>(notFoundResult);

        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(1, _context.Books.Count());

        Assert.Equal(500, notOkResult.StatusCode);

    }

}
