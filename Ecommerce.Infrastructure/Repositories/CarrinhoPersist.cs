using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.EntityFramework.Contextos;
using Ecommerce.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class CarrinhoPersist : GeralPersist, ICarrinhoPersist
    {
        private readonly DataContext _dataContext;
        public CarrinhoPersist(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<CarrinhoItem> GetItemByCarrinhoIdByProdutoIdAsync(int carrinhoId, int produtoId)
        {
            var carrinhoItem = await _dataContext.CarrinhoItems.SingleOrDefaultAsync(c => c.CarrinhoId == carrinhoId &&
                                                                c.ProdutoId == produtoId);

            IQueryable<CarrinhoItem> query = _dataContext.CarrinhoItems.Include(ci => ci.Produto);

            query = query.Where(c => c.CarrinhoId == carrinhoId &&
                                     c.ProdutoId == produtoId);
            
            return await query.SingleOrDefaultAsync();
        }

        public async Task<CarrinhoItem> GetItemByIdAsync(int id)
        {
            var carrinhoItem = await _dataContext.CarrinhoItems.FindAsync(id);
            return carrinhoItem;
        }

        public async Task<IEnumerable<CarrinhoItem>> GetItensAsync(int userId)
        {
            var carrinhoUser = await _dataContext.Carrinhos
                                            .SingleOrDefaultAsync(c => c.UserId == userId);

            IQueryable<CarrinhoItem> query = _dataContext.CarrinhoItems.Include(ci => ci.Carrinho)
                                                                       .Include(ci => ci.Produto);

            query = query.Where(c => c.CarrinhoId == carrinhoUser.Id);

            return await query.ToListAsync();
        }

        
    }
}