using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dtos
{
    public class CarrinhoItemAdicionaDto
    {
        public int Id { get; set; }
        [Required]
        public int CarrinhoId { get; set; }
        [Required]
        public int ProdutoId { get; set; }
        [Required]
        public int Quantidade { get; set; }
    }
}