namespace PokeApp.Models
{
    public class PokemonSprite
    {
        public Sprites? Sprites { get; set; }
    }

    public class Sprites
    {
        public Other? Other { get; set; }
    }

    public class Other
    {
        public Home? Home { get; set; }
    }

    public class Home
    {
        public string? Front_Default { get; set; }
    }
}