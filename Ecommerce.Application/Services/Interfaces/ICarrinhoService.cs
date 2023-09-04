using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface ICarrinhoService
    {
        Task<CarrinhoItemDto> AddItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto);
        Task<CarrinhoItemDto> UpdateQuantidadeItem(CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto);
        Task<bool> DeleteCarrinhoItem(int carrinhoItemId);
        Task<IEnumerable<CarrinhoItemDto>> GetItemsAsync(int userId);
    }
}