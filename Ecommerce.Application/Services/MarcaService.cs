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
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaPersist _marcaPersist;
        private readonly IMapper _mapper;

        public MarcaService(IMarcaPersist marcaPersist,
                            IMapper mapper)
        {
            _marcaPersist = marcaPersist;
            _mapper = mapper;
        }

        public async Task<MarcaDto> AddMarca(MarcaDto marcaDto)
        {
             try
            {               
                var marca = await _marcaPersist.GetMarcaByNameAsync(marcaDto.Nome);

                if(marca != null) return null;

                var marcaParaAdd = _mapper.Map<Marca>(marcaDto);

                _marcaPersist.Add(marcaParaAdd);
                if(await _marcaPersist.SaveChangesAsync()){
                    var marcaRetorno = await _marcaPersist.GetMarcaByNameAsync(marcaDto.Nome);
                    return _mapper.Map<MarcaDto>(marcaRetorno);
                }

                return null;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteMarca(int id)
        {
            try
            {
                var marca = await _marcaPersist.GetMarcaByIdAsync(id);
                if(marca == null) throw new Exception("Marca para Delete não encontrada.");

                _marcaPersist.Delete(marca);
                return await _marcaPersist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<MarcaDto> GetMarcaByIdAsync(int id)
        {
             try
            {
                var marca = await _marcaPersist.GetMarcaByIdAsync(id);
                if(marca == null) return null;

                var marcaRetorno = _mapper.Map<MarcaDto>(marca);

                return marcaRetorno;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<MarcaDto> GetMarcaByNameAsync(string name)
        {
            try
            {
                var marca = await _marcaPersist.GetMarcaByNameAsync(name);
                if(marca == null) return null;

                var marcaRetorno = _mapper.Map<MarcaDto>(marca);

                return marcaRetorno;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<MarcaDto>> GetMarcasAsync()
        {
            try
            {
                var marca = await _marcaPersist.GetMarcasAsync();
                if(marca == null) return null;

                var marcaRetorno = _mapper.Map<IEnumerable<MarcaDto>>(marca);

                return marcaRetorno;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<MarcaDto> UpdateMarca(MarcaDto model)
        {
            try
            {
                var marca = await _marcaPersist.GetMarcaByIdAsync(model.Id);
                if(marca == null) return null;

                model.Id = marca.Id;

                _mapper.Map(model, marca);

                _marcaPersist.Update(marca);

                if(await _marcaPersist.SaveChangesAsync()){
                    var marcaRetorno = await _marcaPersist.GetMarcaByIdAsync(model.Id);
                    return _mapper.Map<MarcaDto>(marcaRetorno);
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