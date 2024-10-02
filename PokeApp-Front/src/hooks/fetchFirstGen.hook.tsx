import { useEffect, useState } from "react"
import { Pokemon } from "../models/pokemon.model"
import pokemonService from "../services/pokemon.service";

const useFetchFirstGen = () => {
    const [ pokemonsData, setPokemonsData ] = useState<Pokemon[]>([]);
    const [ error, setError ] = useState<string>('');
    const [ isLoading, setIsLoading ] = useState<boolean>(true);

    useEffect(() => {
        const fetchFirstGen = async () => {
            try {
                const pokemons = await pokemonService.getFirstGen();
                setPokemonsData(pokemons);
            } catch {
                setError("Une erreur est survenue lors de la récupération des produits, veuillez réessayer plus tard.");
            } setIsLoading(false);
        }

        fetchFirstGen();
    }, []);

    return { pokemonsData, error, isLoading };
};

export default useFetchFirstGen;