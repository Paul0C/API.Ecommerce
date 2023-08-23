using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Domain.Models;

namespace Ecommerce.Infrastructure.Repositories.Interfaces
{
    public interface IMarcaPersist : IGeralPersist
    {
        Task<IEnumerable<Marca>> GetMarcasAsync();
        Task<Marca> GetMarcaByIdAsync(int id);
        Task<Marca> GetMarcaByNameAsync(string name);
    }
}