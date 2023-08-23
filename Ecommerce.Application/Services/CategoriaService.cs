using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Repositories.Interfaces;

namespace Ecommerce.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaPersist _categoriaPersist;
        private readonly IMapper _mapper;

        public CategoriaService(ICategoriaPersist categoriaPersist,
                                IMapper mapper)
        {
            _categoriaPersist = categoriaPersist;
            _mapper = mapper;
        }

        public async Task<CategoriaDto> AddCategoria(CategoriaDto categoriaDto)
        {
            try
            {
                var categoria = await _categoriaPersist.GetCategoriaByNameAsync(categoriaDto.Nome);

                if(categoria != null) return null;

                var categoriaParaAdd = _mapper.Map<Categoria>(categoriaDto);

                _categoriaPersist.Add(categoriaParaAdd);
                if(await _categoriaPersist.SaveChangesAsync()){
                    var categoriaRetorno = await _categoriaPersist.GetCategoriaByNameAsync(categoriaDto.Nome);
                    return _mapper.Map<CategoriaDto>(categoriaRetorno);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _categoriaPersist.GetCategoriaByIdAsync(id);
                if(categoria == null) throw new Exception("Categoria para Delete não encontrada.");

                _categoriaPersist.Delete(categoria);
                return await _categoriaPersist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<CategoriaDto> GetCategoriaByIdAsync(int id)
        {
            try
            {
                var categoria = await _categoriaPersist.GetCategoriaByIdAsync(id);
                if(categoria == null) return null;

                var categoriaRetorno = _mapper.Map<CategoriaDto>(categoria);
                return categoriaRetorno;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<CategoriaDto> GetCategoriaByNameAsync(string name)
        {
            try
            {
                var categoria = await _categoriaPersist.GetCategoriaByNameAsync(name);
                if(categoria == null) return null;

                var categoriaRetorno = _mapper.Map<CategoriaDto>(categoria);
                return categoriaRetorno;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<CategoriaDto>> GetCategoriasAsync()
        {
           try
            {
                var categorias = await _categoriaPersist.GetCategoriasAsync();
                if(categorias == null) return null;

                var categoriaRetorno = _mapper.Map<IEnumerable<CategoriaDto>>(categorias);
                return categoriaRetorno;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<CategoriaDto> UpdateCategoria(CategoriaDto model)
        {
            try
            {
                var categoria = await _categoriaPersist.GetCategoriaByIdAsync(model.Id);
                if(categoria == null) return null;

                model.Id = categoria.Id;

                _mapper.Map(model, categoria);

                _categoriaPersist.Update(categoria);

                if(await _categoriaPersist.SaveChangesAsync()){
                    var categoriaRetorno = await _categoriaPersist.GetCategoriaByIdAsync(model.Id);
                    return _mapper.Map<CategoriaDto>(categoriaRetorno);
                }

                return null;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }
    }
}