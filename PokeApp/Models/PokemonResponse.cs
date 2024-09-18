using System;
namespace PokeApp.Models
{
	public class PokemonResponse
    {
        [Newtonsoft.Json.JsonProperty("gen1_species")]
        public List<PokemonSpecies>? Gen1Species { get; set; }
    }
}