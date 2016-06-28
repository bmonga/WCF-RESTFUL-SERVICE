using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace VeraitoServices
{
    
   [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

 
    public class BookService : IBookService
    {
       /// <summary>
       ///  Method to get all the stored books
       /// </summary>
       /// <returns>list of books</returns>
        public List<bookinfo> GetAllBooks()
        {
            using (var db = new BooksEntities())
            {
                return db.bookinfoes.ToList();
            }
        }

      /// <summary>
      ///  Method to get a book on the basis of the Book ID
      /// </summary>
      /// <param name="id"></param>
      /// <returns> the book details of the given book id</returns>
        public bookinfo GetBook(string id)
        {
           int Id  = Convert.ToInt32(id);
            using (var db = new BooksEntities())
            {
                bookinfo book = db.bookinfoes.SingleOrDefault(b => b.Id == Id);
                // If book with given id does not exist throw exception
                if (book == null)
                {
                    ErrorMessages customError = new ErrorMessages("Book not found", "Book with the given Id does not exists");
                    throw new WebFaultException<ErrorMessages>(customError, HttpStatusCode.NotFound);
                }
                return book;
               
            }
          
        }

        /// <summary>
        /// Implementing Method to add the book
        /// </summary>
        /// <param name="book"></param>
        /// <returns> ID of th added book</returns>
        public int AddBook(bookinfo book)
        {

            if ( string.IsNullOrEmpty(book.Id.ToString()))
            {
                ErrorMessages customError = new ErrorMessages("Book can not be added",
                 "The given book data Id is  null or empty");
                throw new WebFaultException<ErrorMessages>(customError, HttpStatusCode.NotFound);
            }
            using (var db = new BooksEntities())
            {
                bookinfo selectbook = db.bookinfoes.SingleOrDefault(b => b.Id == book.Id);
                // check if the book with this id already exists as duplicates are not allowed
                if(selectbook != null)
                {
                    ErrorMessages customError = new ErrorMessages("Book can not be added", "A book with this Id already exists.");
                    throw new WebFaultException<ErrorMessages>(customError, HttpStatusCode.NotFound);

                }
                db.bookinfoes.Add(book);
                db.SaveChanges();
                return book.Id;
            }
        }

      /// <summary>
     ///    Implementing Method to update the book
      /// </summary>
      /// <param name="book"></param>
      /// <returns>bolean value</returns>
        public bool UpdateBook(bookinfo book)
        {
            using (var db = new BooksEntities())
            {
                bookinfo selectbook = db.bookinfoes.SingleOrDefault(b => b.Id == book.Id);
                // If book with given id does not exist throw exception
                if (selectbook == null)
                {
                    ErrorMessages customError = new ErrorMessages("Book can not be updated", "Book with the given Id does not exist");
                    throw new WebFaultException<ErrorMessages>(customError, HttpStatusCode.NotFound);
                }
                selectbook.Id = book.Id;
                selectbook.Title = book.Title;
                selectbook.Description = book.Description;
                db.SaveChanges();
                return true;
            }
        }
       
       
      /// <summary>
        ///   Implementing Method to delete a book on he basis of book ID
      /// </summary>
      /// <param name="id"></param>
      /// <returns>string id</returns>
        public string DeleteBook(string id)
        {
            int Id = Convert.ToInt32(id);
            using (var db = new BooksEntities())
            {
                bookinfo book = db.bookinfoes.SingleOrDefault(b => b.Id == Id);
                // If book with given id does not exist throw exception
                if (book == null)
                {
                    ErrorMessages customError = new ErrorMessages("Book can not be deleted", "Book with the given Id does not exist");
                    throw new WebFaultException<ErrorMessages>(customError, HttpStatusCode.NotFound);
                }
                db.bookinfoes.Remove(book);
                db.SaveChanges();
                return id;
            }
        }

       /// <summary>
       /// handle Cross-Origin Requests
       /// </summary>
        public void GetOptions()
        {
            
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Methods", "POST, GET, PUT, DELETE, OPTIONS");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Accept");
            WebOperationContext.Current.OutgoingResponse.Headers.Add("Access-Control-Max-Age", "1728000");
        }
    }
}
