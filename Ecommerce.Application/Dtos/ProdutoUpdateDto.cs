using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dtos
{
    public class ProdutoUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [MaxLength(30, ErrorMessage = "O tamanho máximo do campo {0} é 30")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido]")]
        [MaxLength(60, ErrorMessage = "O tamanho máximo do campo {0} é 60")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "O campo {0} é requerido")]
        public int MarcaId { get; set; }
    }
}