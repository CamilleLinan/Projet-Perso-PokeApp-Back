namespace PokeApp.Models
{
    public class PokemonSpecies
    {
        public int Id { get; set; }
        public List<PokemonName>? Pokemon_V2_Pokemonspeciesnames { get; set; }
        public List<Pokemon>? Pokemon_V2_Pokemons { get; set; }
    }

    public class Pokemon
    {
        public int Id { get; set; }
        public List<PokemonType>? Pokemon_V2_Pokemontypes { get; set; }
        public List<PokemonSprite>? Pokemon_V2_Pokemonsprites { get; set; }
    }
}