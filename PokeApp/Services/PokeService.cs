using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IRedisService _redisService;

        public PokeService(IMapper mapper, IRedisService redisService)
        {
            _client = new GraphQLHttpClient("https://beta.pokeapi.co/graphql/v1beta", new NewtonsoftJsonSerializer());
            _mapper = mapper;
            _redisService = redisService;
        }

        public async Task<List<PokemonDto>> GetFirstGenerationPokemonAsync()
        {
            string cacheKey = "gen1_pokemons";

            var cachedData = await _redisService.GetCachedDataAsync<List<PokemonDto>>(cacheKey);
            if (cachedData != null)
            {
                return cachedData;
            }

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

            if (response.Errors != null)
            {
                foreach (var error in response.Errors)
                {
                    Console.WriteLine($"Erreur: {error.Message}");
                }
                return null;
            }

            var pokemonDtos = response.Data?.Gen1Species?.Select(species => _mapper.Map<PokemonDto>(species)).ToList();

            if (pokemonDtos != null && pokemonDtos.Any())
            {
                await _redisService.SetCachedDataAsync(cacheKey, pokemonDtos, TimeSpan.FromMinutes(30));
            }

            return pokemonDtos ?? new List<PokemonDto>();
        }
    }
}