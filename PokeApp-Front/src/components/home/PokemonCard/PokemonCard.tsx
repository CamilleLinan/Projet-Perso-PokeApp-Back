import { FC, useState } from "react";
import "./_PokemonCard.scss";
import { Card } from "antd";
import { LeftOutlined, RightOutlined } from "@ant-design/icons";
import { Pokemon } from "../../../models/pokemon.model";
import PokemonType from "../PokemonType/PokemonType";

const PokemonCard: FC<{ pokemon: Pokemon; languages: string[] }> = ({
  pokemon,
  languages,
}) => {
  const [currentLanguageIndex, setCurrentLanguageIndex] = useState(0);

  const currentLanguage = languages[currentLanguageIndex];
  const currentName =
    pokemon.names[currentLanguage as keyof typeof pokemon.names];

  return (
    <Card cover={<img alt={pokemon.names.en} src={pokemon.sprite} />}>
      <div className="pokemon-card-content">
        <div className="pokemon-id bold">
          <span>#{pokemon.id.toString().padStart(3, '0')}</span>
        </div>
        <div className="pokemon-name-navigation">
          <LeftOutlined
            onClick={() =>
              setCurrentLanguageIndex((prevIndex) =>
                prevIndex === 0 ? languages.length - 1 : prevIndex - 1
              )
            }
            className="arrow-icon"
          />
          <span className="pokemon-name">{currentName}</span>
          <RightOutlined
            onClick={() =>
              setCurrentLanguageIndex((prevIndex) =>
                prevIndex === languages.length - 1 ? 0 : prevIndex + 1
              )
            }
            className="arrow-icon"
          />
        </div>
        <div className="pokemon-types">
          <PokemonType pokemonTypes={pokemon.types} />
        </div>
      </div>
    </Card>
  );
};

export default PokemonCard;
