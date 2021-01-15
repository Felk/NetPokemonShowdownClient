namespace Showdown.Protocol.Events
{
    public class MoveEventArgs : RoomEventArgs
    {
        public Pokemon Pokemon { get; }
        public string Move { get; }
        public string? Target { get; }
        public bool Miss { get; }

        public bool Still { get; }

        // public bool Silent { get; }
        public string? Anim { get; }
        // public string? FromEffect { get; }
        // public string? OfSource { get; }

        public MoveEventArgs(string? roomId, Pokemon pokemon, string move, string? target, bool miss, bool still,
            string? anim) : base(roomId)
        {
            Pokemon = pokemon;
            Move = move;
            Target = target;
            Miss = miss;
            Still = still;
            // Silent = silent;
            Anim = anim;
            // FromEffect = fromEffect;
            // OfSource = ofSource;
        }
    }

    public abstract class PokemonDataChangedEventArgs : RoomEventArgs
    {
        public Pokemon Pokemon { get; }
        public Details Details { get; }
        public int HpDividend { get; }
        public int HpDivisor { get; }
        public string? Status { get; }

        public PokemonDataChangedEventArgs(string? roomId, Pokemon pokemon, Details details, int hpDividend,
            int hpDivisor, string? status) : base(roomId)
        {
            Pokemon = pokemon;
            Details = details;
            HpDividend = hpDividend;
            HpDivisor = hpDivisor;
            Status = status;
        }
    }

    public class SwitchEventArgs : PokemonDataChangedEventArgs
    {
        public SwitchEventArgs(
            string? roomId, Pokemon pokemon, Details details, int hpDividend, int hpDivisor, string? status)
            : base(roomId, pokemon, details, hpDividend, hpDivisor, status)
        {
        }
    }

    public class DragEventArgs : PokemonDataChangedEventArgs
    {
        public DragEventArgs(
            string? roomId, Pokemon pokemon, Details details, int hpDividend, int hpDivisor, string? status)
            : base(roomId, pokemon, details, hpDividend, hpDivisor, status)
        {
        }
    }

    public class DetailsChangeEventArgs : PokemonDataChangedEventArgs
    {
        public DetailsChangeEventArgs(
            string? roomId, Pokemon pokemon, Details details, int hpDividend, int hpDivisor, string? status)
            : base(roomId, pokemon, details, hpDividend, hpDivisor, status)
        {
        }
    }

    public class ReplaceEventArgs : PokemonDataChangedEventArgs
    {
        public ReplaceEventArgs(
            string? roomId, Pokemon pokemon, Details details, int hpDividend, int hpDivisor, string? status)
            : base(roomId, pokemon, details, hpDividend, hpDivisor, status)
        {
        }
    }

    public class SwapEventArgs : RoomEventArgs
    {
        public Pokemon Pokemon { get; }
        public int Position { get; }

        public SwapEventArgs(string? roomId, Pokemon pokemon, int position) : base(roomId)
        {
            Pokemon = pokemon;
            Position = position;
        }
    }

    public class CantEventArgs : RoomEventArgs
    {
        public Pokemon Pokemon { get; }
        public string Reason { get; }
        public string? Move { get; }

        public CantEventArgs(string? roomId, Pokemon pokemon, string reason, string? move) : base(roomId)
        {
            Pokemon = pokemon;
            Reason = reason;
            Move = move;
        }
    }

    public class FaintEventArgs : RoomEventArgs
    {
        public Pokemon Pokemon { get; }

        public FaintEventArgs(string? roomId, Pokemon pokemon) : base(roomId)
        {
            Pokemon = pokemon;
        }
    }
}
