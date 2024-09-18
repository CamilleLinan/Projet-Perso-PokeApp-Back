using PokeApp.Models.Dto;

namespace PokeApp.Services.Interfaces
{
	public interface IPokeService
	{
		Task<List<PokemonDto>> GetFirstGenerationPokemonAsync();
	}
}