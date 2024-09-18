using Microsoft.AspNetCore.Mvc;
using PokeApp.Services;
using PokeApp.Services.Interfaces;

namespace PokeApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokeController : ControllerBase
    {
        private readonly IPokeService _pokeService;

        public PokeController(IPokeService pokeService)
        {
            _pokeService = pokeService;
        }

        [HttpGet("gen1")]
        public async Task<IActionResult> GetFirstGenerationPokemon()
        {
            var pokemons = await _pokeService.GetFirstGenerationPokemonAsync();
            return Ok(pokemons);
        }
    }
}