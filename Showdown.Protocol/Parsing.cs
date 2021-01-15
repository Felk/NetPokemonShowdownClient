using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Net;
using Showdown.Protocol.Events;

namespace Showdown.Protocol
{
    public static class Parsing
    {
        public static RoomType ParseRoomType(string roomTypeStr)
        {
            return roomTypeStr switch
            {
                "battle" => RoomType.Battle,
                "chat" => RoomType.Chat,
                _ => throw new ProtocolViolationException($"unrecognized room type '{roomTypeStr}'")
            };
        }

        public static User ParseUser(string rawString) =>
            new(rawString[0], rawString.Substring(1));

        public static UserWithStatus ParseUserWithStatus(string rawString)
        {
            char rank = rawString[0];
            string[] split = rawString.Substring(1).Split("@", count: 2);
            bool isAway = false;
            string? status = split.Length == 1 ? null : split[1];
            if (status != null && status.StartsWith("!"))
            {
                isAway = true;
                status = status.Substring(1);
            }

            return new UserWithStatus(new User(rank, split[0]), isAway, status);
        }

        public static Format ParseFormat(string formatStr)
        {
            string[] parts = formatStr.Split(',', count: 2);
            return new Format(parts[0], parts.ElementAtOrDefault(1));
        }

        public static IEnumerable<FormatSection> ParseFormatSections(string sectionsStr)
        {
            var currentFormats = new List<Format>();
            int column = 0;
            string? name = null;
            foreach (var s in sectionsStr.Split('|'))
            {
                if (s.StartsWith(',')) // new column, e.g. ",1"
                {
                    if (currentFormats.Any())
                    {
                        Debug.Assert(name != null, "name can only be null if currentFormats is empty");
                        yield return new FormatSection(column, name, currentFormats.ToImmutableList());
                    }

                    currentFormats.Clear();
                    try
                    {
                        column = int.Parse(s.Substring(1));
                    }
                    catch (FormatException)
                    {
                        // TODO ",LL" = uses local ladder, see https://github.com/smogon/pokemon-showdown-client/blob/master/src/panel-mainmenu.tsx#L103
                    }

                    name = null;
                }
                else if (name == null) // section name, e.g. "Sw/Sh Singles"
                {
                    name = s;
                }
                else // actual format, e.g. "[Gen 8] Anything Goes,c"
                {
                    currentFormats.Add(ParseFormat(s));
                }
            }

            if (currentFormats.Any())
            {
                Debug.Assert(name != null, "name can only be null if currentFormats is empty");
                yield return new FormatSection(column, name, currentFormats.ToImmutableList());
            }
        }

        public static PlayerNum ParsePlayerNum(string playerStr) =>
            Enum.Parse<PlayerNum>(playerStr.ToUpper());

        public static GameType ParseGameType(string gameTypeStr) =>
            gameTypeStr switch
            {
                "singles" => GameType.Singles,
                "doubles" => GameType.Doubles,
                "triples" => GameType.Triples,
                "multi" => GameType.Multi,
                "free-for-all" => GameType.FreeForAll,
                _ => throw new ProtocolViolationException($"unrecognized game type '{gameTypeStr}'")
            };

        public static Details ParseDetails(string detailsStr)
        {
            string[] parts = detailsStr.Split(", ");
            string species = parts[0];
            bool shiny = false;
            Gender? gender = null;
            int level = 100;
            foreach (string moreInfo in parts.Skip(1))
            {
                if (moreInfo == "shiny") shiny = true;
                else if (moreInfo == "M") gender = Gender.Male;
                else if (moreInfo == "F") gender = Gender.Female;
                else if (moreInfo.StartsWith('L')) level = int.Parse(moreInfo.Substring(1));
                else throw new ProtocolViolationException($"Unknown DETAILS appendix '{moreInfo}'");
            }

            // TODO forme support, but that's annoying since I can't just split at "-", which would break things like Ho-oh or Porygon-Z
            return new Details(species, null, shiny, gender, level);
        }

        public static Pokemon ParsePokemon(string pokemon)
        {
            // POSITION: NAME
            // e.g. p1a: Dragonite
            string[] parts = pokemon.Split(": ", count: 2);
            if (parts.Length < 2) throw new ProtocolViolationException("invalid format, expected 'POSITION: NAME'");
            string position = parts[0];
            string pokemonName = parts[1];
            if (position.Length == 0) throw new ProtocolViolationException("position must not be empty");
            char suffix = position[^1];
            if (char.IsDigit(suffix))
            {
                // no position letter, inactive pokemon
                PlayerNum playerNum = ParsePlayerNum(position);
                return new Pokemon(playerNum, null, pokemonName);
            }
            else
            {
                PlayerNum playerNum = ParsePlayerNum(position.Substring(0, position.Length - 1));
                return new Pokemon(playerNum, suffix, pokemonName);
            }
        }
    }
}
