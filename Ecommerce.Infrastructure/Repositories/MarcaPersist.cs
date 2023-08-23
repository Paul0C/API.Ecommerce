using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Identity;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.EntityFramework.Contextos;
using Ecommerce.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repositories
{
    public class MarcaPersist : GeralPersist, IMarcaPersist
    {
        private readonly DataContext _dataContext;
        public MarcaPersist(DataContext dataContext) : base(dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Marca> GetMarcaByIdAsync(int id)
        {
            return await _dataContext.Marcas.FindAsync(id);
        }

        public async Task<Marca> GetMarcaByNameAsync(string name)
        {
            return await _dataContext.Marcas.SingleOrDefaultAsync(m => m.Nome == name.ToLower());
        }

        public async Task<IEnumerable<Marca>> GetMarcasAsync()
        {
            return await _dataContext.Marcas.ToListAsync();
        }
    }
}