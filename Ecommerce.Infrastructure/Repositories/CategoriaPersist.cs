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
    public class CategoriaPersist : GeralPersist, ICategoriaPersist
    {
        private readonly DataContext _dataContext;
        public CategoriaPersist(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Categoria> GetCategoriaByIdAsync(int id)
        {
            return await _dataContext.Categorias.FindAsync(id);
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            return await _dataContext.Categorias.ToListAsync();
        }

        public async Task<Categoria> GetCategoriaByNameAsync(string name){
            return await _dataContext.Categorias.SingleOrDefaultAsync(c => c.Nome == name.ToLower());
        }
    }
}