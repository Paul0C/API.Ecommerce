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
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoPersist _carrinhoPersist;
        private readonly IMapper _mapper;
        public CarrinhoService(ICarrinhoPersist carrinhoPersist,
                               IMapper mapper)
        {
            _carrinhoPersist = carrinhoPersist;
            _mapper = mapper;
        }

        private CarrinhoItemDto AtualizacaoCarrinhoItemDto(CarrinhoItem carrinhoItem){
            
            var carrinhoItemRetorno = _mapper.Map<CarrinhoItemDto>(carrinhoItem);
            carrinhoItemRetorno.Preco = carrinhoItem.Produto.Preco;
            carrinhoItemRetorno.PrecoTotal = (int)(carrinhoItemRetorno.Preco * carrinhoItem.Quantidade);

            return carrinhoItemRetorno;
        }

        public async Task<CarrinhoItemDto> AddItem(CarrinhoItemAdicionaDto carrinhoItemAdicionaDto)
        {
            try
            {
                var verificacaoCarrinhoItem =  await _carrinhoPersist.GetItemByCarrinhoIdByProdutoIdAsync(carrinhoItemAdicionaDto.CarrinhoId,
                                                                carrinhoItemAdicionaDto.ProdutoId);

                if(verificacaoCarrinhoItem != null) throw new Exception("O Item já existe no carrinho");

                var carrinhoItem = _mapper.Map<CarrinhoItem>(carrinhoItemAdicionaDto);

                _carrinhoPersist.Add(carrinhoItem);

                if(await _carrinhoPersist.SaveChangesAsync()){
                    var carrinhoItemAdicionado = await _carrinhoPersist.GetItemByCarrinhoIdByProdutoIdAsync(
                        carrinhoItemAdicionaDto.CarrinhoId, carrinhoItemAdicionaDto.ProdutoId
                    );

                    return AtualizacaoCarrinhoItemDto(carrinhoItemAdicionado);
                }

                return null;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteCarrinhoItem(int carrinhoItemId)
        {
            try
            {
                var carrinhoItem = _carrinhoPersist.GetItemByIdAsync(carrinhoItemId);
                if(carrinhoItem == null) throw new Exception("Item do Carrinho para Delete não encontrado.");

                _carrinhoPersist.Delete(carrinhoItem);
                
                return await _carrinhoPersist.SaveChangesAsync();
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<IEnumerable<CarrinhoItemDto>> GetItemsAsync(int userId)
        {
            try
            {
                var carrinhoItems = await _carrinhoPersist.GetItensAsync(userId);
                if(carrinhoItems == null) return null;

                var carrinhoItemsParaRetorno = carrinhoItems.Select(c => AtualizacaoCarrinhoItemDto(c));
                return carrinhoItemsParaRetorno;
            }
            catch (Exception e)
            {
                
                throw new Exception(e.Message);
            }
        }

        public async Task<CarrinhoItemDto> UpdateQuantidadeItem(CarrinhoItemAtualizaQuantidadeDto carrinhoItemAtualizaQuantidadeDto)
        {
            try
            {
                var carrinhoItem = await _carrinhoPersist.GetItemByIdAsync(carrinhoItemAtualizaQuantidadeDto.Id);
                if(carrinhoItem == null) return null;

                carrinhoItem.Quantidade = carrinhoItemAtualizaQuantidadeDto.Quantidade;
                _carrinhoPersist.Update(carrinhoItem);

                if(await _carrinhoPersist.SaveChangesAsync()){
                    var carrinhoItemParaRetorno = await _carrinhoPersist.GetItemByIdAsync(carrinhoItem.Id);
                    return AtualizacaoCarrinhoItemDto(carrinhoItemParaRetorno);
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