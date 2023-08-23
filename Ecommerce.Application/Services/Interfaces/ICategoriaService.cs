using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface ICategoriaService
    {
        Task<CategoriaDto> GetCategoriaByIdAsync(int id);
        Task<CategoriaDto> GetCategoriaByNameAsync(string name);
        Task<IEnumerable<CategoriaDto>> GetCategoriasAsync();
        Task<CategoriaDto> AddCategoria(CategoriaDto categoriaDto);
        Task<CategoriaDto> UpdateCategoria(CategoriaDto model);
        Task<bool> DeleteCategoria(int id);

    }
}