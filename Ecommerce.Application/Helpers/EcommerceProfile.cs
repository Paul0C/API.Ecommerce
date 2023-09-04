using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Dtos;
using Ecommerce.Domain.Identity;
using Ecommerce.Domain.Models;

namespace Ecommerce.Application.Helpers
{
    public class EcommerceProfile : Profile
    {
        public EcommerceProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();

            CreateMap<Categoria, CategoriaDto>().ReverseMap();

            CreateMap<Marca, MarcaDto>().ReverseMap();

            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Produto, ProdutoUpdateDto>().ReverseMap();

            CreateMap<CarrinhoItem, CarrinhoItemDto>().ReverseMap();
            CreateMap<CarrinhoItem, CarrinhoItemAdicionaDto>().ReverseMap();
            CreateMap<CarrinhoItem, CarrinhoItemAtualizaQuantidadeDto>().ReverseMap();
        }
    }
}