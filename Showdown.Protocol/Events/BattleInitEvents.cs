namespace Showdown.Protocol.Events
{
    public enum PlayerNum
    {
        P1, P2, P3, P4
    }

    public class PlayerEventArgs : RoomEventArgs
    {
        public PlayerNum PlayerNum { get; }
        public string Username { get; }
        public string Avatar { get; }
        public int? Rating { get; }

        public PlayerEventArgs(string? roomId, PlayerNum playerNum, string username, string avatar, int? rating) :
            base(roomId)
        {
            PlayerNum = playerNum;
            Username = username;
            Avatar = avatar;
            Rating = rating;
        }
    }

    public class TeamSizeEventArgs : RoomEventArgs
    {
        public PlayerNum PlayerNum { get; }
        public int Number { get; }

        public TeamSizeEventArgs(string? roomId, PlayerNum playerNum, int number) : base(roomId)
        {
            PlayerNum = playerNum;
            Number = number;
        }
    }

    public enum GameType
    {
        Singles,
        Doubles,
        Triples,
        Multi,
        FreeForAll
    }

    public class GameTypeEventArgs : RoomEventArgs
    {
        public GameType Type { get; }

        public GameTypeEventArgs(string? roomId, GameType type) : base(roomId)
        {
            Type = type;
        }
    }

    public class GenEventArgs : RoomEventArgs
    {
        public int Gen { get; }

        public GenEventArgs(string? roomId, int gen) : base(roomId)
        {
            Gen = gen;
        }
    }

    public class TierEventArgs : RoomEventArgs
    {
        public string FormatName { get; }

        public TierEventArgs(string? roomId, string formatName) : base(roomId)
        {
            FormatName = formatName;
        }
    }

    public class RatedEventArgs : RoomEventArgs
    {
        public RatedEventArgs(string? roomId) : base(roomId)
        {
        }
    }

    public class RatedInfoEventArgs : RoomEventArgs
    {
        public string Message { get; }

        public RatedInfoEventArgs(string? roomId, string message) : base(roomId)
        {
            Message = message;
        }
    }

    public class RuleEventArgs : RoomEventArgs
    {
        public string Rule { get; }
        public string Description { get; }

        public RuleEventArgs(string? roomId, string rule, string description) : base(roomId)
        {
            Rule = rule;
            Description = description;
        }
    }

    public class ClearPokeEventArgs : RoomEventArgs
    {
        public ClearPokeEventArgs(string? roomId) : base(roomId)
        {
        }
    }

    public class PokeEventArgs : RoomEventArgs
    {
        public string Player { get; }
        public Details Details { get; }
        public string Item { get; } // item id?

        public PokeEventArgs(string? roomId, string player, Details details, string item) : base(roomId)
        {
            Player = player;
            Details = details;
            Item = item;
        }
    }

    public class TeamPreviewEventArgs : RoomEventArgs
    {
        public TeamPreviewEventArgs(string? roomId) : base(roomId)
        {
        }
    }

    public class StartEventArgs : RoomEventArgs
    {
        public StartEventArgs(string? roomId) : base(roomId)
        {
        }
    }
}
