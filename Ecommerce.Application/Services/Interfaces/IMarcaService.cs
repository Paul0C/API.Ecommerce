using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface IMarcaService
    {
        Task<MarcaDto> GetMarcaByIdAsync(int id);
        Task<MarcaDto> GetMarcaByNameAsync(string name);
        Task<IEnumerable<MarcaDto>> GetMarcasAsync();
        Task<MarcaDto> AddMarca(MarcaDto marcaDto);
        Task<MarcaDto> UpdateMarca(MarcaDto model);
        Task<bool> DeleteMarca(int id);
    }
}