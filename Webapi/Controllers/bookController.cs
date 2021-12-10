using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace WebApi.AddControllers{
     [ApiController]
     [Route("[controller]s")]
     public class BookController : ControllerBase
     {
         private static List<Book> BookList  = new List<Book>(){
             new Book{
                 Id=1,
                 Title="Lean Startap",
                 GenreId=1, //PersonalGrowth
                 PageCount = 200,
                 PublishDate = new DateTime(2021,12,10)
             },
             new Book{
                 Id=2,
                 Title="Herland",
                 GenreId=2, //Sience Fiction
                 PageCount = 240,
                 PublishDate = new DateTime(2011,5,12)
             },
             new Book{
                 Id=3,
                 Title="Dublinners",
                 GenreId=3, //LoveStory
                 PageCount = 244,
                 PublishDate = new DateTime(2001,1,12)
             },
         };
         [HttpGet]
         public List<Book> GetBooks()
         {
             var bookList = BookList.OrderBy(x=> x.Id).ToList<Book>();
             return bookList;
         }
         [HttpGet("{id}")]
         public Book GetById(int id)
         { 
             var book = BookList.Where(book => book.Id == id).SingleOrDefault();
             return book;
         }
         // [HttpGet] Kullanılması norma get e göre daha az tercih edilir.
        // public Book Get([FromQuery] string id)
        // {
///              var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault(); 
       //      return book;
       //  }
       [HttpPost]
       public IActionResult  AddBook([FromBody] Book newBook)
       {
           var book = BookList.SingleOrDefault(x=> x.Title == newBook.Title);
           if(book is not null )
             return BadRequest();
           BookList.Add(newBook);
           return Ok();  
       }
       [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody] Book updateBook)
        {
            var book = BookList.SingleOrDefault(x=> x.Id == id);
            if(book is null)
                return BadRequest();
            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(x=> x.Id == id);
            if(book is null )
                return BadRequest();
            BookList.Remove(book);
            return Ok();    
        }
     }
}