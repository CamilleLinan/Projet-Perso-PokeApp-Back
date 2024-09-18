using System;
namespace PokeApp.Models
{
	public class PokemonResponse
    {
        [Newtonsoft.Json.JsonProperty("gen1_species")]
        public List<Pokemon>? Gen1Species { get; set; }
    }
}