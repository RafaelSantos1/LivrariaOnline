using LivrariaOnline.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaOnline.Domain.Entities
{
    public class Book : EntityBase
    {
        public string Isbn { get; private set; }
        public string Autor { get; private set; }
        public string Nome { get; private set; }
        public decimal Preco { get; private set; }
        public DateTime DataPublicacao { get; private set; }
        public string PathImage { get; private set; }

        public Book(string isbn, string autor, string nome, decimal preco, DateTime dataPublicacao, string pathImage)
        {
            Isbn = isbn;
            Autor = autor;
            Nome = nome;
            Preco = preco;
            DataPublicacao = dataPublicacao;
            PathImage = pathImage;
            Validate(this, new BookValidator());
        }
    }
}
