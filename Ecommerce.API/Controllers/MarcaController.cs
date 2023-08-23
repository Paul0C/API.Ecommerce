using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Application.Dtos;
using Ecommerce.Application.Services.Interfaces;
using Ecommerce.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;

        public MarcaController(IMarcaService marcaService)
        {
            _marcaService = marcaService;
        }

        [HttpGet("marcas")]
        public async Task<IActionResult> Get(){
            try
            {
                var marcas = await _marcaService.GetMarcasAsync();
                if(marcas == null) return NoContent();

                return Ok(marcas);
            }
            catch (Exception e)
            {
                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                    $"Não foi possível recuperar as Marcas. Erro: {e.Message}");
            }
        }

        [HttpGet("marcaNome")]
        public async Task<IActionResult> GetByName([FromQuery] string name){
            try
            {
                var marca = await _marcaService.GetMarcaByNameAsync(name);
                if(marca == null) return NoContent();

                return Ok(marca);
            }
            catch (Exception e)
            {               
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Não foi possível recuperar a Marca {name}. Erro: {e.Message}");
            }
        }

        [HttpGet("marcaId")]
        public async Task<IActionResult> GetById([FromQuery] int id){
            try
            {
                var marca = await _marcaService.GetMarcaByIdAsync(id);
                if(marca == null) return NoContent();

                return Ok(marca);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Não foi possível recuperar a Marca com o Id {id}. Erro: {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MarcaDto marcaDto){
            try
            {
                var marca = await _marcaService.AddMarca(marcaDto);

                if(marca == null) return NoContent();

                return Ok(marca);

            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError, 
                        $"Não foi possível criar a Marca. Erro: {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] MarcaDto marcaDto){
            try
            {
                var marca = await _marcaService.UpdateMarca(marcaDto);
                if(marca == null) return NoContent();

                return Ok(marca);
            }
            catch (Exception e)
            {                
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                        $"Não foi possível atualizar a Marca. Erro: {e.Message}");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int id){
            try
            {
                var marca = await _marcaService.GetMarcaByIdAsync(id);
                if(marca == null) return NoContent();

                return await _marcaService.DeleteMarca(id)
                        ? Ok("Deletado")
                        : throw new Exception("Um erro ocorreu ao tentar deletar a Marca.");

            }   
            catch (Exception e)
            {
               
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                            $"Não foi possível deletar a Marca. Erro: {e.Message}");
            }
        }
    }
}