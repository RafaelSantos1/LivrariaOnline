using LivrariaOnline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrariaOnline.Domain.Interfaces.Services
{
    public interface IBookService : IService<Book>
    {
        Task<IQueryable<Book>> GetBeetwenDatePublish(DateTime startDate, DateTime endDate);
        Task<IQueryable<Book>> GetByAuthor(string nameAuthor);
        Task<IQueryable<Book>> GetByName(string nameBook);
        Task<IQueryable<Book>> GetByPrice(decimal price);
        Task<Book> GetByIsbn(string isbn);        
    }
}
