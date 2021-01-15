namespace Showdown.Protocol.Events
{
    public class ClearMessageBarEventArgs : RoomEventArgs
    {
        public ClearMessageBarEventArgs(string? roomId) : base(roomId)
        {
        }
    }

    public class RequestEventArgs : RoomEventArgs
    {
        public string Request { get; }

        public RequestEventArgs(string? roomId, string request) : base(roomId)
        {
            Request = request;
        }
    }

    public class InactiveEventArgs : RoomEventArgs
    {
        public string Message { get; }

        public InactiveEventArgs(string? roomId, string message) : base(roomId)
        {
            Message = message;
        }
    }

    public class InactiveOffEventArgs : RoomEventArgs
    {
        public string Message { get; }

        public InactiveOffEventArgs(string? roomId, string message) : base(roomId)
        {
            Message = message;
        }
    }

    public class UpkeepEventArgs : RoomEventArgs
    {
        public UpkeepEventArgs(string? roomId) : base(roomId)
        {
        }
    }

    public class TurnEventArgs : RoomEventArgs
    {
        public int Number { get; }

        public TurnEventArgs(string? roomId, int number) : base(roomId)
        {
            Number = number;
        }
    }

    public class WinEventArgs : RoomEventArgs
    {
        public string User { get; }

        public WinEventArgs(string? roomId, string user) : base(roomId)
        {
            User = user;
        }
    }

    public class TieEventArgs : RoomEventArgs
    {
        public TieEventArgs(string? roomId) : base(roomId)
        {
        }
    }
}
