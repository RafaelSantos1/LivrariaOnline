using LivrariaOnline.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace LivrariaOnline.Domain.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(b => b).NotNull().WithMessage("O objeto livro não pode ser nulo");
            RuleFor(b => b.Autor).NotEmpty().NotNull().WithMessage("É necessário informar o nome do Autor do livro");
            RuleFor(b => b.DataPublicacao).NotNull().WithMessage("É necessário informar a data de publicação");
            RuleFor(b => b.Isbn).NotEmpty().NotNull().WithMessage("É necessário informar o ISBN do livro");
            RuleFor(b => b.Nome).NotEmpty().NotNull().WithMessage("É necessário informar o nome do livro");
            RuleFor(b => b.Preco).GreaterThan(0).WithMessage("É necessário informar o preço do livro");
            RuleFor(b => b.PathImage).NotEmpty().NotNull().WithMessage("É necessário informar o path da imagem do livro");
        }
    }
}
