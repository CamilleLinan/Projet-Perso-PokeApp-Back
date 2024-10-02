import { FC } from 'react';
import "./_PokemonType.scss";
import tinycolor from "tinycolor2";
import { TYPES_COLORS } from '../../../utils/constantes';

const PokemonType: FC<{ pokemonTypes: string[] }> = ({ pokemonTypes }) => {
    const lightenColor = (color: string, amount: number) => {
        return tinycolor(color).lighten(amount).toString();
      };

  return (
    <>
        {pokemonTypes.map((type) => (
            <span
                className="type"
                key={type}
                style={{ 
                    background: `linear-gradient(to bottom, ${TYPES_COLORS[type] || "#777"}, ${lightenColor(TYPES_COLORS[type] || "#777", 10)})`
                }}
            >
              {type.charAt(0).toUpperCase() + type.slice(1).toLowerCase()}
            </span>
        ))}
    </>
  );
};

export default PokemonType;