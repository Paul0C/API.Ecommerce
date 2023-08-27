using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;

namespace Ecommerce.Infrastructure.Repositories.Interfaces
{
    public interface IProdutoPersist : IGeralPersist
    {
        Task<IEnumerable<Produto>> GetAllProdutosAsync();
        Task<Produto> GetProdutoByIdAsync(int id);
        Task<Produto> GetProdutoByNomeAsync(string nomeProduto, int marcaId);
        Task<IEnumerable<Produto>> GetProdutosByCategoriaIdAsync(int categoriaId);
        Task<IEnumerable<Produto>> GetProdutosByMarcaIdAsync(int marcaId);
        Task<IEnumerable<Produto>> GetProdutosByCategoriaIdAndMarcaIdAsync(int marcaId, int categoriaId);

    }
}