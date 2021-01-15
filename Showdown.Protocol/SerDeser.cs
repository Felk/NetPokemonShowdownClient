using System.Collections.Generic;
using System.Linq;

namespace Showdown.Protocol
{
    public static class SerDeser
    {
        public static string PackTeam(IEnumerable<ShowdownPokemon> pokemons)
        {
            return string.Join(']', pokemons.Select(PackTeamPokemon));
        }

        /// <summary>
        /// Converts a pokemon into the showdown 'packed' format.
        /// See a reference implementation <c>packTeam</c> here:
        /// https://github.com/smogon/pokemon-showdown/blob/master/sim/dex.ts
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns></returns>
        private static string PackTeamPokemon(ShowdownPokemon pokemon)
        {
            string packed =
                $"{pokemon.Name}" +
                $"|{(pokemon.Name == pokemon.Species ? string.Empty : pokemon.Species)}" +
                $"|{pokemon.Item}" +
                $"|{pokemon.Ability}" +
                $"|{string.Join(',', pokemon.Moves)}" +
                $"|{pokemon.Nature}" +
                $"|{PackStats(pokemon.Evs, omitValue: 0)}" +
                $"|{PackGender(pokemon.Gender)}" +
                $"|{PackStats(pokemon.Ivs, omitValue: 31)}" +
                $"|{(pokemon.Shiny ? "S" : string.Empty)}" +
                $"|{(pokemon.Level == 100 ? string.Empty : pokemon.Level.ToString())}" +
                $"|{(pokemon.Happiness == 255 ? string.Empty : pokemon.Happiness.ToString())}";
            if (pokemon.Pokeball != null || pokemon.HpType != null)
            {
                packed +=
                    $",{pokemon.Pokeball}" +
                    $",{pokemon.HpType}";
            }
            return packed;
        }

        private static string PackStats(TeamStats stats, int omitValue)
        {
            int[] statsOrdered = { stats.Hp, stats.Atk, stats.Def, stats.SpA, stats.SpD, stats.Spe };
            if (statsOrdered.All(stat => stat == omitValue))
            {
                return string.Empty;
            }
            else
            {
                IEnumerable<string> statStrings = statsOrdered
                    .Select(stat => stat == omitValue ? string.Empty : stat.ToString());
                return string.Join(',', statStrings);
            }
        }

        private static string PackGender(Gender? gender) => gender switch
        {
            Gender.Male => "M",
            Gender.Female => "F",
            _ => string.Empty
        };
    }
}
