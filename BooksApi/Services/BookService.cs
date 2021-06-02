using BooksApi.Models;
using BooksApi.Services.Contracts;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;
        private IScopedService scopedService;
        private ISingletonService singletonService;
        private ITransientService transientService;

        public BookService(
            IBookstoreDatabaseSettings settings,
            IScopedService scopedSvc,
            ISingletonService singletonSvc,
            ITransientService transientSvc
        )
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            scopedService = scopedSvc;
            singletonService = singletonSvc;
            transientService = transientSvc;

            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public object GetGuid()
        {
            var guids = new
            {
                scopeGuid = scopedService.GetGuid(),
                singletonGuid = singletonService.GetGuid(),
                transientGuid = transientService.GetGuid(),
            };
            return guids;
        }

        public List<Book> GetBooks()
        {
           return _books.Find(book => true).ToList();
        }

        public Book Get(string id) =>
            _books.Find<Book>(book => book.Id == id).FirstOrDefault();

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id, Book bookIn) =>
            _books.ReplaceOne(book => book.Id == id, bookIn);

        public void Remove(Book bookIn) =>
            _books.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) =>
            _books.DeleteOne(book => book.Id == id);
    }
}