using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dtos
{
    public class UserDto
    {
        [Required(ErrorMessage = "{0} é requerido.")]
        [StringLength(15, MinimumLength = 4, 
                        ErrorMessage = "Intervalo de Caractéres permitido é de 4 a 15")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "Insira um {0} válido.")]
        [Required(ErrorMessage = "{0} é requerido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A {0} é requerida.")]
        public string PassWord { get; set; }

        [Required(ErrorMessage = "O {0} é requerido.")]
        [StringLength(25, MinimumLength =3,
                        ErrorMessage = "Intervalo permitido para o {0} é de 3 a 25 caractéres")]
        public string PrimeiroNome { get; set; }

        [Required(ErrorMessage = "O {0} é requerido.")]
        [StringLength(25, MinimumLength =3,
                        ErrorMessage = "Intervalo permitido para o {0} é de 3 a 25 caractéres")]
        public string UltimoNome { get; set; }

        [Required(ErrorMessage = "O {0} é requerido.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "A {0} é requerida.")]
        public string Cidade { get; set; }
    }
}