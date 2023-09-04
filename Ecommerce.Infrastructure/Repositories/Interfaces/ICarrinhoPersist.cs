using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;

namespace Ecommerce.Infrastructure.Repositories.Interfaces
{
    public interface ICarrinhoPersist : IGeralPersist
    {
        Task<IEnumerable<CarrinhoItem>> GetItensAsync(int userId);
        Task<CarrinhoItem> GetItemByCarrinhoIdByProdutoIdAsync(int carrinhoId, int produtoId);
        Task<CarrinhoItem> GetItemByIdAsync(int id);
    }
}