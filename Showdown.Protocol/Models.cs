using System.Collections.Immutable;
using Showdown.Protocol.Events;

namespace Showdown.Protocol
{
    public record User(char Rank, string Name)
    {
        public override string ToString() => $"{Rank}{Name}";
    }
    public record UserWithStatus(User User, bool IsAway, string? Status);
    public record Format(string Name /*, bool UsesRandomTeams, FormatAvailability Availability*/, string? Suffix);
    public record FormatSection(int Column, string Name, IImmutableList<Format> Formats);
    public record Pokemon(PlayerNum Player, char? Position, string Name);
    public enum Gender { Male, Female }
    public record Details(string Species, string? Forme, bool Shiny, Gender? Gender, int Level);
    public record TeamStats(int Hp, int Atk, int Def, int SpA, int SpD, int Spe);

    public record ShowdownPokemon(
        string Name,
        string Species, // omitted if same as name
        string Item,
        string Ability, // 0, 1, H for slot, name for custom
        IImmutableList<string> Moves,
        string? Nature, // omitted if Serious (except Gen1-2, where it means no nature)
        TeamStats Evs, // individual omitted if 0, all omitted if all 0
        Gender? Gender, // TODO apparently null ? species default? and "N" exists
        TeamStats Ivs, // individual omitted if 31, all omitted if all 31
        bool Shiny, // omitted if not
        int Level, // omitted if 100
        int Happiness, // omitted if 255
        string? HpType, // blank of not hyper trained, no effect or represented in a move
        string? Pokeball // blank for regular
    );
}
