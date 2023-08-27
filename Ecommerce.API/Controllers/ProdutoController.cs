using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _produtoService;
        public ProdutoController(IProdutoService produtoService){
            _produtoService = produtoService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(){
            try
            {
                var produtos = await _produtoService.GetAllProdutosAsync();
                if(produtos == null) return NoContent();

                return Ok(produtos);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                $"Erro ao tentar recuperar os produtos. Erro: {e.Message}");
            }
        }

        [HttpGet("getByNome")]
        public async Task<IActionResult> GetByNome([FromQuery] string nome, int marcaId){
            try
            {
                var produto = await _produtoService.GetProdutoByNomeAsync(nome, marcaId);
                if(produto == null) return NoContent();

                return Ok(produto);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                $"Erro ao tentar recuperar o produto pelo nome. Erro: {e.Message}");
            }
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetById([FromQuery] int id){
            try
            {
                var produto = await _produtoService.GetProdutoByIdAsync(id);
                if(produto == null) return NoContent();

                return Ok(produto);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                $"Erro ao tentar recuperar o produto pelo Id{id}. Erro: {e.Message}");
            }
        }

        [HttpGet("getByCategoria")]
        public async Task<IActionResult> GetByCategoriaId([FromQuery] int categoriaId){
            try
            {
                var produtos = await _produtoService.GetProdutosByCategoriaIdAsync(categoriaId);
                if(produtos == null) return NoContent();

                return Ok(produtos);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                $"Erro ao tentar recuperar os produtos pela Categoria. Erro: {e.Message}");
            }
        }

        [HttpGet("getByMarca")]
        public async Task<IActionResult> GetByMarcaId([FromQuery] int marcaId){
            try
            {
                var produtos = await _produtoService.GetProdutosByMarcaIdAsync(marcaId);
                if(produtos == null) return NoContent();

                return Ok(produtos);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                $"Erro ao tentar recuperar os produtos pela Marca. Erro: {e.Message}");
            }
        }

        [HttpGet("getByCategoriaAndMarca")]
        public async Task<IActionResult> GetByCategoriaIdAndMarcaId([FromQuery] int marcaId, int categoriaId){
            try
            {
                var produtos = await _produtoService.GetProdutosByCategoriaIdAndMarcaIdAsync(marcaId, categoriaId);
                if(produtos == null) return NoContent();

                return Ok(produtos);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                                $"Erro ao tentar recuperar os produtos pela Marca e Categoria. Erro: {e.Message}");
            }
        }

        [HttpPost("AddProduto")]
        public async Task<IActionResult> AddProduto(ProdutoUpdateDto produtoUpdateDto){
            try
            {
                var produto = await _produtoService.AddProduto(produtoUpdateDto);
                if(produto == null) return NoContent();

                return Ok(produto);
            }
            catch (Exception e)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao adicionar o Produto. Erro: {e.Message}");
            }
        }

        [HttpPut("AttProduto")]
        public async Task<IActionResult> PutProduto([FromBody]ProdutoUpdateDto produtoUpdateDto){
            try
            {
                var produto = await _produtoService.UpdateProduto(produtoUpdateDto);
                if(produto == null) return NoContent();

                return Ok(produto);
            }
            catch (Exception e)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao atualizar o Produto. Erro: {e.Message}");
            }
        }

        [HttpDelete("DeleteProduto")]
        public async Task<IActionResult> DeleteProduto([FromQuery] int id){
            try
            {
                var produto = await _produtoService.GetProdutoByIdAsync(id);
                if(produto == null) return NoContent();

                return await _produtoService.DeleteProduto(id)
                        ? Ok("Produto deletado com sucesso!")
                        : throw new Exception("Ocorreu um problema ao tentar deletar Evento.");
            }
            catch (Exception e)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                            $"Erro ao deletar o Produto. Erro: {e.Message}");
            }
        }
    }
}