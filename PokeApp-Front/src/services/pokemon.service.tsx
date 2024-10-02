import axios from "axios";
import { Pokemon } from "../models/pokemon.model";

const clientHTTP = axios.create({
    baseURL: "https://localhost:7134",
});

const getFirstGen = async (): Promise<Pokemon[] | []> => {
    try {
        const response = await clientHTTP.get(`/api/Poke/gen1`);
        return response.data;
    } catch (error) {
        console.error('Error to fetch first gen:', error);
        throw error;
    }
}

export default { getFirstGen };