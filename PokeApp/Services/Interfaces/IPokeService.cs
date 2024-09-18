using PokeApp.Models;

namespace PokeApp.Services.Interfaces
{
	public interface IPokeService
	{
		Task<List<Pokemon>> GetFirstGenerationPokemonAsync();
	}
}