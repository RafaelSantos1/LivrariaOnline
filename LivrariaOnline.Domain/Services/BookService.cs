using LivrariaOnline.Domain.Entities;
using LivrariaOnline.Domain.Interfaces.Repositories;
using LivrariaOnline.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaOnline.Domain.Services
{
    public class BookService : Service<Book>, IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _uow;

        public BookService(IBookRepository repository, IUnitOfWork uow) : base(repository, uow)
        {
            _repository = repository;
            _uow = uow;
        }

        public async Task<IQueryable<Book>> GetBeetwenDatePublish(DateTime startDate, DateTime endDate)
        {
            var books = await _repository.GetAsync(b => (b.DataPublicacao.Day >= startDate.Day && b.DataPublicacao.Month >= startDate.Month
            && b.DataPublicacao.Year >= startDate.Year)
            && (b.DataPublicacao.Day <= endDate.Day && b.DataPublicacao.Month <= endDate.Month
            && b.DataPublicacao.Year <= endDate.Year));

            return books.OrderBy(b => b.DataPublicacao.Year).ThenBy(b => b.DataPublicacao.Month).ThenBy(b => b.DataPublicacao.Day);
        }

        public async Task<IQueryable<Book>> GetByAuthor(string nameAuthor)
        {
            var books = await _repository.GetAsync(b => b.Autor.ToLower().Equals(nameAuthor.ToLower()));
            return books.OrderBy(b => b.Autor);
        }

        public async Task<Book> GetByIsbn(string isbn)
        {
            var books = await _repository.GetAsync(b => b.Isbn.Equals(isbn));
            return books.FirstOrDefault();
        }

        public async Task<IQueryable<Book>> GetByName(string nameBook)
        {
            var books = await _repository.GetAsync(b => b.Nome.ToLower().Equals(nameBook.ToLower()));
            return books.OrderBy(b => b.Nome);
        }

        public async Task<IQueryable<Book>> GetByPrice(decimal price)
        {
            var books = await _repository.GetAsync(b => b.Preco.Equals(price));
            return books.OrderBy(b => b.Preco);
        }
    }
}
