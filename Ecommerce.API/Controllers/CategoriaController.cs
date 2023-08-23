using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Services;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("categorias")]
        public async Task<IActionResult> Get(){
            try
            {
                var categorias = await _categoriaService.GetCategoriasAsync();
                if(categorias == null) return NoContent();

                return Ok(categorias);
            }
            catch (Exception e)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Não foi possível recuperar as Categorias. Erro: {e.Message}");
            }
        }

        [HttpGet("categoriaNome")]
        public async Task<IActionResult> GetByName([FromQuery] string name){
            try
            {
                var categoria = await _categoriaService.GetCategoriaByNameAsync(name);
                if(categoria == null) return NoContent();

                return Ok(categoria);
            }
            catch (Exception e)
            {               
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Não foi possível recuperar a Categoria {name}. Erro: {e.Message}");
            }
        }

        [HttpGet("categoriaId")]
        public async Task<IActionResult> GetById([FromQuery] int id){
            try
            {
                var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
                if(categoria == null) return NoContent();

                return Ok(categoria);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Não foi possível recuperar a Categoria com o Id {id}. Erro: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoriaDto categoriaDto){
            try
            {
                var categoria = await _categoriaService.AddCategoria(categoriaDto);

                if(categoria == null) return NoContent();

                return Ok(categoria);

            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Não foi possível criar a Categoria. Erro: {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] CategoriaDto categoriaDto){
            try
            {
                var categoria = await _categoriaService.UpdateCategoria(categoriaDto);
                if(categoria == null) return NoContent();

                return Ok(categoria);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Não foi possível atualizar a Categoria. Erro: {e.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id){
            try
            {
                var categoria = await _categoriaService.GetCategoriaByIdAsync(id);
                if(categoria == null) return NoContent();

                return await _categoriaService.DeleteCategoria(id)
                        ? Ok("Deletado")
                        : throw new Exception("Um erro ocorreu ao tentar deletar a Categoria.");

            }   
            catch (Exception e)
            {
               
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                            $"Não foi possível deletar a Categoria. Erro: {e.Message}");
            }
        }
    }
}