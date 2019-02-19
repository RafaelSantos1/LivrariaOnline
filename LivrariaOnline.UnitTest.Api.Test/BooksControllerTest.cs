using AutoMapper;
using LivrariaOnline.Api.Test;
using LivrariaOnline.Application.Api.Controllers;
using LivrariaOnline.Application.Api.Dto;
using LivrariaOnline.Domain.Interfaces.Repositories;
using LivrariaOnline.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Net.Http;

namespace LivrariaOnline.UnitTest.Api.Test
{
    [TestClass]
    public class BooksControllerTest
    {
        private readonly TestServerFixture _fixture = new TestServerFixture();
        private TestServer TestServer => _fixture.Server;
        private HttpClient Client => _fixture.Client;
        private readonly IBookService _service;
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly BooksController _controller;

        public BooksControllerTest()
        {
            var serviceScope = TestServer.Host.Services.GetService<IServiceScopeFactory>().CreateScope();
            _service = serviceScope.ServiceProvider.GetRequiredService<IBookService>();
            _repository = serviceScope.ServiceProvider.GetRequiredService<IBookRepository>();
            _uow = serviceScope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            _mapper = serviceScope.ServiceProvider.GetRequiredService<IMapper>();
            _controller = new BooksController(_service, _mapper);
        }

        [TestMethod]
        public void GetBooksOkResultTest()
        {
            var response = _controller.Get().Result as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsInstanceOfType(response.Value, typeof(IQueryable<BookDto>));
            Assert.AreEqual(9, ((IQueryable<BookDto>)response.Value).ToList().Count);
        }

        [TestMethod]
        public void GetBookByIdOkResultTest()
        {
            var id = 2;
            var response = _controller.Get(id).Result as OkObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsInstanceOfType(response.Value, typeof(BookDto));
            Assert.AreEqual(id.ToString(), ((BookDto)response.Value).Id);
            Assert.AreEqual("978-8501112514", ((BookDto)response.Value).Isbn);
            Assert.AreEqual((28.99m).ToString(), ((BookDto)response.Value).Preco);
        }

        [TestMethod]
        public void GetBookByIdNotFoundTest()
        {
            var id = 35;
            var response = _controller.Get(id).Result as NotFoundObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void FindByAutorOkResultTest()
        {
            var autor = "Dale Carnegie";
            var response = _controller.FindByAutor(autor).Result as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsInstanceOfType(response.Value, typeof(IQueryable<BookDto>));
            Assert.AreEqual("978-8504018028", ((IQueryable<BookDto>)response.Value).FirstOrDefault().Isbn);
            Assert.AreEqual("Como Fazer Amigos e Influenciar Pessoas", ((IQueryable<BookDto>)response.Value).FirstOrDefault().Nome);
        }

        [TestMethod]
        public void FindByAutorNotFoundTest()
        {
            var autor = "Th";
            var response = _controller.FindByAutor(autor).Result as NotFoundObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void FindByNameOkResultTest()
        {
            var bookName = "O Mágico de Oz";
            var response = _controller.FindByName(bookName).Result as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsInstanceOfType(response.Value, typeof(IQueryable<BookDto>));
            Assert.AreEqual("978-8537809662", ((IQueryable<BookDto>)response.Value).FirstOrDefault().Isbn);
        }

        [TestMethod]
        public void FindByNameNotFoundTest()
        {
            var autor = "abc";
            var response = _controller.FindByName(autor).Result as NotFoundObjectResult;

            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void FindByIsbnOkResultTest()
        {
            var isbn = "978-8573255119";
            var response = _controller.FindByIsbn(isbn).Result as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsInstanceOfType(response.Value, typeof(BookDto));
            Assert.AreEqual(isbn, ((BookDto)response.Value).Isbn);
        }

        [TestMethod]
        public void FindByIsbnNotFoundTest()
        {
            var isbn = "854-8573255119";
            var response = _controller.FindByIsbn(isbn).Result as NotFoundObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void FindByPriceOkResultTest()
        {
            var preco = 10.0m;
            var response = _controller.FindByPrice(preco).Result as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsInstanceOfType(response.Value, typeof(IQueryable<BookDto>));
            Assert.AreEqual("978-8573023510", ((IQueryable<BookDto>)response.Value).FirstOrDefault().Isbn);
        }

        [TestMethod]
        public void FindByPriceNotFoundTest()
        {
            var preco = 200.0m;
            var response = _controller.FindByPrice(preco).Result as NotFoundObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void FindBeetwenDatePublishOkResultTest()
        {
            var startDate = new DateTime(2018, 1, 1);
            var endDate = new DateTime(2019, 1, 1);
            var response = _controller.FindBeetwenDatePublish(startDate, endDate).Result as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
            Assert.IsInstanceOfType(response.Value, typeof(IQueryable<BookDto>));
        }

        [TestMethod]
        public void RegisterOkResultTest()
        {
            var bookDto = new BookDto()
            {
                Nome = "Direito Constitucional Esquematizado",
                Autor = "Lenza,Pedro",
                Isbn = "978-8553603398",
                Preco = "211,00",
                DataPublicacao = new DateTime(2019, 2, 5).ToString("yyyy-MM-dd"),
                PathImage = @"ImagesBooks/DireitoConstitucionalEsquematizado.png"
            };

            var response = _controller.Register(bookDto).Result as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public void RegisterBadRequestTest()
        {
            var bookDto = new BookDto()
            {
                Nome = "Direito Constitucional Esquematizado",
                Autor = "Lenza,Pedro",                
                DataPublicacao = new DateTime(2019, 2, 5).ToString("yyyy-MM-dd"),               
            };

            var response = _controller.Register(bookDto).Result as BadRequestObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(400, response.StatusCode);
        }

        [TestMethod]
        public void RegisterDuplicateIsbnBadRequestTest()
        {
            var bookDto = new BookDto()
            {
                Nome = "Direito Constitucional Esquematizado",
                Autor = "Lenza,Pedro",
                Isbn = "978-8573023510", //ISBN Existente na base de dados
                Preco = "211,00",
                DataPublicacao = new DateTime(2019, 2, 5).ToString("yyyy-MM-dd"),
                PathImage = @"ImagesBooks/DireitoConstitucionalEsquematizado.png"
            };

            var response = _controller.Register(bookDto).Result as BadRequestObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(400, response.StatusCode);
        }

        [TestMethod]
        public void RegisterBook_with_Price_LessThan_Zero_Test()
        {
            var bookDto = new BookDto()
            {
                Nome = "Direito Constitucional Esquematizado",
                Autor = "Lenza,Pedro",
                Isbn = "978-8553603398",
                Preco = "-11,00",
                DataPublicacao = new DateTime(2019, 2, 5).ToString("yyyy-MM-dd"),
                PathImage = @"ImagesBooks/DireitoConstitucionalEsquematizado.png"
            };

            var response = _controller.Register(bookDto).Result as BadRequestObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(400, response.StatusCode);
        }

        [TestMethod]
        public void UpdateOkResultTest()
        {
            var id = 3;
            var bookDto = new BookDto()
            {
                Id = id.ToString(),
                Nome = "Livro Atualizado",
                Autor = "Freitas, Thiago",
                Isbn = "978-8573023510", //ISBN Existente na base de dados
                Preco = "25,45",
                DataPublicacao = new DateTime(2019, 2, 5).ToString("yyyy-MM-dd"),
                PathImage = @"ImagesBooks/Operegrino.jpg"
            };

            var response = _controller.Update(id, bookDto).Result as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public void UpdateNotFoundTest()
        {
            var id = 25;
            var bookDto = new BookDto()
            {
                Id = id.ToString(),
                Nome = "Livro Atualizado",
                Autor = "Freitas, Thiago",
                Isbn = "978-8573023510", //ISBN Existente na base de dados
                Preco = "25,45",
                DataPublicacao = new DateTime(2019, 2, 5).ToString("yyyy-MM-dd"),
                PathImage = @"ImagesBooks/Operegrino.jpg"
            };

            var response = _controller.Update(id, bookDto).Result as NotFoundObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
        }

        [TestMethod]
        public void UpdateBook_with_Price_LessThan_Zero_Test()
        {
            var id = 3;
            var bookDto = new BookDto()
            {
                Id = id.ToString(),
                Nome = "Livro Atualizado",
                Autor = "Freitas, Thiago",
                Isbn = "978-8573023510", //ISBN Existente na base de dados
                Preco = "-10,45",
                DataPublicacao = new DateTime(2019, 2, 5).ToString("yyyy-MM-dd"),
                PathImage = @"ImagesBooks/Operegrino.jpg"
            };

            var response = _controller.Update(id, bookDto).Result as BadRequestObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(400, response.StatusCode);
        }

        [TestMethod]
        public void DeleteBookByIdOkResultTest()
        {
            var id = 5;

            var response = _controller.Delete(id).Result as OkObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(200, response.StatusCode);
        }

        [TestMethod]
        public void DeleteBookByIdNotFoundTest()
        {
            var id = 150;
            var response = _controller.Delete(id).Result as NotFoundObjectResult;
            Assert.IsNotNull(response);
            Assert.AreEqual(404, response.StatusCode);
        }
    }
}
