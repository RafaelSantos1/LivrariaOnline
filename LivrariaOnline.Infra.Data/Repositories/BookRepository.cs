using LivrariaOnline.Domain.Entities;
using LivrariaOnline.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaOnline.Infra.Data.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(DbContext context) : base(context)
        {
        }
    }
}
