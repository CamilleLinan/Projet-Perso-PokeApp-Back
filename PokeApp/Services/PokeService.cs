using GraphQL;
using PokeApp.Services.Interfaces;
using PokeApp.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        public async Task<List<Pokemon>> GetFirstGenerationPokemonAsync()
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

            // Retourne la liste des Pokémon de la première génération
            return response.Data?.Gen1Species ?? new List<Pokemon>();
        }
    }
}