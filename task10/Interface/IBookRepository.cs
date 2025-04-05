using System;
using task10.Contarcts;
using task10.Models;

namespace task10.Interface;

public interface IBookRepository
{
    Task<List<Book>> GetAllBooks();
    Task<Book> GetById(int id);
    Task Create(CreateBooks book);
    Task Update(int id, UpdateBooks book);
    Task Delete(int id);
}
