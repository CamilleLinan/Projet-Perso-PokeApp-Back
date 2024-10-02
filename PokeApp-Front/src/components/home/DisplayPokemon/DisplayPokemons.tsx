import { FC } from 'react';
import "./_DisplayPokemon.scss";
import { Row, Col } from 'antd';
import PokemonCard from '../PokemonCard/PokemonCard';
import useFetchFirstGen from '../../../hooks/fetchFirstGen.hook';
import { Pokemon } from '../../../models/pokemon.model';

const DisplayPokemons: FC = () => {
    const { pokemonsData } = useFetchFirstGen();

    const languages = ['en', 'fr', 'ja'];

    return (
        <Row gutter={[16, 16]} className='pokemon-container'>
            {pokemonsData && pokemonsData.map((pokemon: Pokemon) => (
                <Col key={pokemon.id} xs={8} sm={8} md={6} lg={4}>
                    <PokemonCard pokemon={pokemon} languages={languages} />
                </Col>
            ))}
        </Row>
    );
};

export default DisplayPokemons;