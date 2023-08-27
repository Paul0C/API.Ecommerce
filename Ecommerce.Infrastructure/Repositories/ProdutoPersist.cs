using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.EntityFramework.Contextos;
using Ecommerce.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class ProdutoPersist : GeralPersist, IProdutoPersist
    {
        private readonly DataContext _dataContext;
        public ProdutoPersist(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
        {
            var produtos = await _dataContext.Produtos
                                             .Include(p => p.Categoria)
                                             .Include(p => p.Marca)
                                             .ToListAsync();
                                            
            return produtos;
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            var produto = await _dataContext.Produtos
                                            .Include(p => p.Categoria)
                                            .Include(p => p.Marca)
                                            .SingleOrDefaultAsync(p => p.Id == id);
            
            return produto;
        }

        public async Task<Produto> GetProdutoByNomeAsync(string nomeProduto, int marcaId)
        {
            var produto = await _dataContext.Produtos
                                            .Include(p => p.Categoria)
                                            .Include(p => p.Marca)
                                            .SingleOrDefaultAsync(p => p.Nome == nomeProduto && p.MarcaId == marcaId);
            
            return produto;
        }

        public async Task<IEnumerable<Produto>> GetProdutosByCategoriaIdAndMarcaIdAsync(int marcaId, int categoriaId)
        {
            var produtos = await _dataContext.Produtos
                                             .Include(p => p.Categoria)
                                             .Include(p => p.Marca)
                                             .Where(p => p.MarcaId == marcaId && p.CategoriaId == categoriaId)
                                             .ToListAsync();
            
            return produtos;
        }

        public async Task<IEnumerable<Produto>> GetProdutosByCategoriaIdAsync(int categoriaId)
        {
            var produtos = await _dataContext.Produtos
                                             .Include(p => p.Categoria)
                                             .Include(p => p.Marca)
                                             .Where(p => p.CategoriaId == categoriaId)
                                             .ToListAsync();
            
            return produtos;
        }

        public async Task<IEnumerable<Produto>> GetProdutosByMarcaIdAsync(int marcaId)
        {
            var produtos = await _dataContext.Produtos
                                             .Include(p => p.Categoria)
                                             .Include(p => p.Marca)
                                             .Where(p => p.MarcaId == marcaId)
                                             .ToListAsync();
            
            return produtos;
        }
    }
}