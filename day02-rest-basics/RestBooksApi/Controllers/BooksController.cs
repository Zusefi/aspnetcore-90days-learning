using Microsoft.AspNetCore.Mvc;
using RestBooksApi.Models;

namespace RestBooksApi.Controllers;

[ApiController]
[Route("books")]
public class BooksController : ControllerBase
{
    private static readonly List<Book> Books =
    [
        new Book { Id = 1, Title = "Clean Code", Author = "Robert C. Martin" },
        new Book { Id = 2, Title = "The Pragmatic Programmer", Author = "Andrew Hunt" }
    ];

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Books);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);

        if (book is null)
            return NotFound(new { message = $"Book with id {id} not found" });

        return Ok(book);
    }

    [HttpPost]
    public IActionResult Create([FromBody] Book book)
    {
        var newId = Books.Any() ? Books.Max(b => b.Id) + 1 : 1;
        book.Id = newId;
        Books.Add(book);

        return Created($"/books/{book.Id}", book);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);

        if (book is null)
            return NotFound(new { message = $"Book with id {id} not found" });

        Books.Remove(book);
        return NoContent();
    }
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Book updatedBook)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);

        if (book is null)
            return NotFound(new { message = $"Book with id {id} not found" });

        book.Title = updatedBook.Title;
        book.Author = updatedBook.Author;

        return Ok(book);
    }

}
