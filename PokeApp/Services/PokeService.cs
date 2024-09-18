using GraphQL;
using PokeApp.Services.Interfaces;
using PokeApp.Models;
using PokeApp.Models.Dto;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace PokeApp.Services
{
    public class PokeService : IPokeService
    {
        private readonly GraphQLHttpClient _client;

        public PokeService()
        {
            _client = new GraphQLHttpClient("https://beta.pokeapi.co/graphql/v1beta", new NewtonsoftJsonSerializer());
        }

        public async Task<List<PokemonDto>> GetFirstGenerationPokemonAsync()
        {
            var request = new GraphQLRequest
            {
                Query = @"
                    query samplePokeAPIquery {
                      gen1_species: pokemon_v2_pokemonspecies(
                        where: {pokemon_v2_generation: {name: {_eq: ""generation-i""}}}, 
                        order_by: {id: asc}) {
                        id
                        pokemon_v2_pokemonspeciesnames(where: {pokemon_v2_language: {name: {_in: [""fr"", ""en"", ""ja""]}}}) {
                          name
                          pokemon_v2_language {
                            name
                          }
                        }
                        pokemon_v2_pokemons {
                          id
                          pokemon_v2_pokemonsprites {
                            sprites
                          }
                          pokemon_v2_pokemontypes {
                            pokemon_v2_type {
                              name
                            }
                          }
                        }
                      }
                    }",
                OperationName = "samplePokeAPIquery",
                Variables = null
            };

            var response = await _client.SendQueryAsync<PokemonResponse>(request);

            // Vérifier les erreurs de la réponse
            if (response.Errors != null)
            {
                foreach (var error in response.Errors)
                {
                    Console.WriteLine($"Erreur: {error.Message}");
                }
                return null;
            }

            var pokemonDtos = response.Data?.Gen1Species?.Select(species =>
            {
                var namesDictionary = species.Pokemon_V2_Pokemonspeciesnames?
                    .ToDictionary(
                        n => n.Pokemon_V2_Language?.Name,
                        n => n.Name
                    );

                var pokemon = species.Pokemon_V2_Pokemons.FirstOrDefault();
                var spriteData = pokemon?.Pokemon_V2_Pokemonsprites.FirstOrDefault()?.Sprites;
                var homeSprite = spriteData?.Other?.Home?.Front_Default;

                var types = pokemon?.Pokemon_V2_Pokemontypes
                    .Select(t => t.Pokemon_V2_Type.Name)
                    .ToArray();

                // Mapper vers PokemonDto
                return new PokemonDto
                {
                    Id = species.Id,
                    Names = namesDictionary,
                    Types = types,
                    Sprite = homeSprite
                };
            }).ToList();

            return pokemonDtos ?? new List<PokemonDto>();
        }
    }
}