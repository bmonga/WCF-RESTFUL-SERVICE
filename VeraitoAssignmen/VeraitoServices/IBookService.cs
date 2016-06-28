using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Services;

namespace VeraitoServices
{
   
    [ServiceContract]
    public interface IBookService
    {
        /// <summary>
        /// get all the books
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "Books", BodyStyle = WebMessageBodyStyle.Wrapped,
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        List<bookinfo> GetAllBooks();

        /// <summary>
        /// get a book on the basis of id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        [WebGet(UriTemplate = "Book/{id}", BodyStyle = WebMessageBodyStyle.Wrapped,
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bookinfo GetBook(string id);

        /// <summary>
        /// add a book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "Book/Add", Method = "POST",BodyStyle = WebMessageBodyStyle.WrappedRequest, 
         ResponseFormat = WebMessageFormat.Json)]        
        int AddBook(bookinfo book);

        /// <summary>
        /// Update a book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "Book/Update", Method = "PUT", BodyStyle = WebMessageBodyStyle.Wrapped,
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        bool UpdateBook(bookinfo book);

        /// <summary>
        /// delete a book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(UriTemplate = "Book/Delete/{id}", Method = "DELETE", BodyStyle = WebMessageBodyStyle.Wrapped,
         RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string DeleteBook(string id);

        /// <summary>
        /// Handle CORS
        /// </summary>
        [OperationContract]
        [WebInvoke(Method = "OPTIONS", UriTemplate = "*")]
        void GetOptions();
    }
}
