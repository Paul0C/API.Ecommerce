using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;

namespace Ecommerce.Application.Services.Interfaces
{
    public interface IProdutoService
    {
        Task<ProdutoDto> AddProduto(ProdutoUpdateDto produtoDto);
        Task<ProdutoDto> UpdateProduto(ProdutoUpdateDto produtoDto);
        Task<bool> DeleteProduto(int id);
        Task<IEnumerable<ProdutoDto>> GetAllProdutosAsync();
        Task<ProdutoDto> GetProdutoByIdAsync(int id);
        Task<ProdutoDto> GetProdutoByNomeAsync(string name, int marcaId);
        Task<IEnumerable<ProdutoDto>> GetProdutosByCategoriaIdAsync(int categoriaId);
        Task<IEnumerable<ProdutoDto>> GetProdutosByMarcaIdAsync(int marcaId);
        Task<IEnumerable<ProdutoDto>> GetProdutosByCategoriaIdAndMarcaIdAsync(int marcaId, int categoriaId);
    }
}