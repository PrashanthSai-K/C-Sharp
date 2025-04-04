using System;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using task10.AppDataContext;
using task10.Contarcts;
using task10.Interface;
using task10.Mappings;
using task10.Models;

namespace task10.Services;

public class BookService : IBookRepository
{

    private readonly ApplicationDbContext _context;
    private readonly ILogger<BookService> _logger;
    private readonly IMapper _mapper;

    public BookService(ApplicationDbContext context, ILogger<BookService> logger, IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task Create(CreateBooks book)
    {
        try
        {
            var Book = _mapper.Map<Book>(book);
            _context.Books.Add(Book);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during creation");
        }
    }

    public async Task Delete(int id)
    {
        try
        {
            var book = await _context.Books.FindAsync(id) ?? throw new Exception("Book not found");
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<List<Book>> GetAllBooks()
    {
        try
        {
            var books = await _context.Books.ToListAsync();
            if (books == null)
            {
                throw new Exception("No Books found");
            }
            return books;
        }
        catch (System.Exception)
        {
            throw new Exception("Error while retreving");
        }
    }

    public async Task<Book> GetById(int id)
    {
        try
        {
            var book = await _context.Books.FindAsync(id) ?? throw new Exception("Book not found");
            return book;
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task Update(int id, UpdateBooks book)
    {
        try
        {
            var oldbook = _mapper.Map<Book>(book);  
            var dbBook = await _context.Books.FindAsync(id) ?? throw new Exception("Book not found");

            if (book.Name != null)
            {
                dbBook.Name = book.Name;
            }

            if (book.Author != null)
            {
                dbBook.Author = book.Author;
            }
            if (book?.Price != null)
            {
                dbBook.Price = book.Price;
            }
            if (book?.IsCompleted != null)
            {
                dbBook.IsCompleted = book.IsCompleted;
            }
            await _context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}
