using AutoMapper;
using PokeApp.Models;
using PokeApp.Models.Dto;

namespace PokeApp.AutoMapperProfile
{
	public class PokemonProfile : Profile
	{
		public PokemonProfile()
		{
            CreateMap<PokemonSpecies, PokemonDto>()
                .ForMember(dest => dest.Names, opt => opt.MapFrom(src => src.Pokemon_V2_Pokemonspeciesnames.ToDictionary(n => n.Pokemon_V2_Language.Name, n => n.Name)))
                .ForMember(dest => dest.Sprite, opt => opt.MapFrom(src => src.Pokemon_V2_Pokemons.FirstOrDefault().Pokemon_V2_Pokemonsprites.FirstOrDefault().Sprites.Other.Home.Front_Default))
                .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Pokemon_V2_Pokemons.FirstOrDefault().Pokemon_V2_Pokemontypes.Select(t => t.Pokemon_V2_Type.Name).ToArray()));
        }
	}
}