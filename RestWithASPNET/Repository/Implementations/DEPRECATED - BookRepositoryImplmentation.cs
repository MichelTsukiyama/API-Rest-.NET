
// DEPRECATED

// using System.Collections.Generic;
// using System.Linq;
// using RestWithASPNET.Context;
// using RestWithASPNET.Model;

// namespace RestWithASPNET.Repository.Implementations
// {
//     public class BookRepositoryImplmentation : IBookRepository
//     {
//         private MySqlContext _context;

//         public BookRepositoryImplmentation(MySqlContext context)
//         {
//             _context = context;
//         }

//         public List<Book> FindAll()
//         {
//             return _context.Books.ToList();
//         }

//         public Book FindById(int id)
//         {
//             return _context.Books.SingleOrDefault(b => b.Id.Equals(id));
//         }

//         public Book Create(Book book)
//         {
//             try
//             {
//                 _context.Add(book);
//                 _context.SaveChanges();
//             }
//             catch (System.Exception)
//             {
//                 throw;
//             }
//             return book;
//         }

//         public Book Update(Book book)
//         {
//             if(!Exists(book.Id))
//                 return null;
            
//             var result = _context.Books.SingleOrDefault(b => b.Id.Equals(book.Id));

//             if(result is not null)
//             {
//                 try
//                 {
//                     _context.Entry(result).CurrentValues.SetValues(book);
//                     _context.SaveChanges();
//                 }
//                 catch (System.Exception)
//                 {
//                     throw;
//                 }
//             }

//             return book;
//         }

//         public void Delete(int id)
//         {
//             var result = _context.Books.SingleOrDefault(b => b.Id.Equals(id));

//             if(result is not null)
//             {
//                 try
//                 {
//                     _context.Books.Remove(result);
//                     _context.SaveChanges();
//                 }
//                 catch (System.Exception)
//                 {
//                     throw;
//                 }
//             }
//         }

//         public bool Exists(int id)
//         {
//             return _context.Books.Any(b => b.Id.Equals(id));
//         }
//     }
// }