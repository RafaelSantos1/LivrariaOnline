using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LivrariaOnline.Application.Api.Dto
{
    public class BookDto
    {
        public string Id { get; set; }
        public string Isbn { get; set; }
        public string Autor { get; set; }
        public string Nome { get; set; }
        public string Preco { get; set; }
        public string DataPublicacao { get; set; }
        public string PathImage { get; set; }
    }
}
