namespace PokeApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public List<PokemonName>? Pokemon_V2_Pokemonspeciesnames { get; set; }  // Liste des noms dans différentes langues
    }

    public class PokemonName
    {
        public string? Name { get; set; }
        public Language? Pokemon_V2_Language { get; set; }
    }

    public class Language
    {
        public string? Name { get; set; }
    }
}