namespace Showdown.Protocol.Events
{
    // TODO

    public readonly struct FormeChange
    {
        public Pokemon Pokemon { get; }
        public Details Details { get; }
        public int HpDividend { get; }
        public int HpDivisor { get; }
        public string? Status { get; }

        public FormeChange(Pokemon pokemon, Details details, int hpDividend, int hpDivisor, string? status)
        {
            Pokemon = pokemon;
            Details = details;
            HpDividend = hpDividend;
            HpDivisor = hpDivisor;
            Status = status;
        }
    }
}
