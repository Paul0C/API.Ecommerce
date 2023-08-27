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
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoPersist _produtoPersist;
        private readonly IMapper _mapper;
        public ProdutoService(IProdutoPersist produtoPersist,
                              IMapper mapper)
        {
            _produtoPersist = produtoPersist;
            _mapper = mapper;
        }

        public async Task<ProdutoDto> AddProduto(ProdutoUpdateDto produtoDto)
        {
            try
            {
                var produto = await _produtoPersist
                                .GetProdutoByNomeAsync(produtoDto.Nome, produtoDto.MarcaId);
                
                if(produto != null) return null;

                var produtoParaAdd = _mapper.Map<Produto>(produtoDto);
                _produtoPersist.Add(produtoParaAdd);

                if(await _produtoPersist.SaveChangesAsync()){
                    var produtoRetorno = await _produtoPersist
                                        .GetProdutoByNomeAsync(produtoDto.Nome, produtoDto.MarcaId);
                    return _mapper.Map<ProdutoDto>(produtoRetorno);
                }

                return null;          
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteProduto(int id)
        {
            try
            {
                var produto = await _produtoPersist.GetProdutoByIdAsync(id);

                if(produto == null) throw new Exception("Produto para delete não encontrado");

                _produtoPersist.Delete(produto);
                return await _produtoPersist.SaveChangesAsync();
            }
            catch (System.Exception)
            {               
                throw;
            }
        }

        public async Task<IEnumerable<ProdutoDto>> GetAllProdutosAsync()
        {
            try
            {
                var produtos = await _produtoPersist.GetAllProdutosAsync();
                if(produtos == null) return null;

                var produtosParaRetorno = produtos.Select(p => _mapper.Map<ProdutoDto>(p));

                return produtosParaRetorno;
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }

        public async Task<ProdutoDto> GetProdutoByIdAsync(int id)
        {
            try
            {
                var produto = await _produtoPersist.GetProdutoByIdAsync(id);
                if(produto == null) return null;

                var produtoParaRetorno = _mapper.Map<ProdutoDto>(produto);
                return produtoParaRetorno; 
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }

        public async Task<ProdutoDto> GetProdutoByNomeAsync(string name, int marcaId)
        {
            try
            {
                var produto = await _produtoPersist.GetProdutoByNomeAsync(name, marcaId);
                if(produto == null) return null;

                var produtoParaRetorno = _mapper.Map<ProdutoDto>(produto);
                return produtoParaRetorno; 
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<ProdutoDto>> GetProdutosByCategoriaIdAndMarcaIdAsync(int marcaId, int categoriaId)
        {
            try
            {
                var produtos = await _produtoPersist.GetProdutosByCategoriaIdAndMarcaIdAsync(marcaId, categoriaId);
                if(produtos == null) return null;

                var produtosParaRetorno = produtos.Select(p => _mapper.Map<ProdutoDto>(p));
                return produtosParaRetorno; 
            }
            catch (Exception e)
            {               
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<ProdutoDto>> GetProdutosByCategoriaIdAsync(int categoriaId)
        {
            try
            {
                var produtos = await _produtoPersist.GetProdutosByCategoriaIdAsync(categoriaId);
                if(produtos == null) return null;

                var produtosParaRetorno = produtos.Select(p => _mapper.Map<ProdutoDto>(p));
                return produtosParaRetorno; 
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<ProdutoDto>> GetProdutosByMarcaIdAsync(int marcaId)
        {
            try
            {
                var produtos = await _produtoPersist.GetProdutosByMarcaIdAsync(marcaId);
                if(produtos == null) return null;

                var produtosParaRetorno = produtos.Select(p => _mapper.Map<ProdutoDto>(p));
                return produtosParaRetorno; 
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }
        }

        public async Task<ProdutoDto> UpdateProduto(ProdutoUpdateDto produtoDto)
        {
            try
            {
                var produto = await _produtoPersist.GetProdutoByIdAsync(produtoDto.Id);
                if(produto == null) return null;

                _mapper.Map(produtoDto, produto);
                _produtoPersist.Update(produto);

                if(await _produtoPersist.SaveChangesAsync()){
                    var produtoParaRetorno = await _produtoPersist.GetProdutoByIdAsync(produto.Id);
                    return _mapper.Map<ProdutoDto>(produtoParaRetorno);
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