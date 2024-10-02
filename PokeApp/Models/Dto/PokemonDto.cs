namespace PokeApp.Models.Dto
{
	public class PokemonDto
	{
		public int Id { get; set; }
		public Dictionary<string, string>? Names { get; set; }
		public IEnumerable<string>? Types { get; set; }
		public string? Sprite { get; set; }
	}
}