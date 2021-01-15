using System;
using System.Collections.Immutable;

namespace Showdown.Protocol.Events
{
    public abstract class RoomEventArgs : EventArgs
    {
        public string? RoomId { get; }

        protected RoomEventArgs(string? roomId)
        {
            RoomId = roomId;
        }
    }

    #region room init events

    public enum RoomType { Chat, Battle }

    public class InitEventArgs : RoomEventArgs
    {
        public RoomType RoomType { get; }

        public InitEventArgs(string? roomId, RoomType roomType) : base(roomId)
        {
            RoomType = roomType;
        }
    }

    public class TitleEventArgs : RoomEventArgs
    {
        public string Title { get; }

        public TitleEventArgs(string? roomId, string title) : base(roomId)
        {
            Title = title;
        }
    }

    public class UsersEventArgs : RoomEventArgs
    {
        public IImmutableList<UserWithStatus> Users { get; }

        public UsersEventArgs(string? roomId, IImmutableList<UserWithStatus> users) : base(roomId)
        {
            Users = users;
        }
    }

    #endregion

    #region regular room events

    public class MessageEventArgs : RoomEventArgs
    {
        public string Message { get; }

        public MessageEventArgs(string? roomId, string message) : base(roomId)
        {
            Message = message;
        }
    }

    public class HtmlEventArgs : RoomEventArgs
    {
        public string Html { get; }

        public HtmlEventArgs(string? roomId, string html) : base(roomId)
        {
            Html = html;
        }
    }

    public class UHtmlEventArgs : RoomEventArgs
    {
        public string Name { get; }
        public string Html { get; }

        public UHtmlEventArgs(string? roomId, string name, string html) : base(roomId)
        {
            Name = name;
            Html = html;
        }
    }

    public class UHtmlChangeEventArgs : RoomEventArgs
    {
        public string Name { get; }
        public string Html { get; }

        public UHtmlChangeEventArgs(string? roomId, string name, string html) : base(roomId)
        {
            Name = name;
            Html = html;
        }
    }

    public class JoinEventArgs : RoomEventArgs
    {
        public User User { get; }
        public bool ShouldDisplay { get; }

        public JoinEventArgs(string? roomId, User user, bool shouldDisplay) : base(roomId)
        {
            User = user;
            ShouldDisplay = shouldDisplay;
        }
    }

    public class LeaveEventArgs : RoomEventArgs
    {
        public User User { get; }
        public bool ShouldDisplay { get; }

        public LeaveEventArgs(string? roomId, User user, bool shouldDisplay) : base(roomId)
        {
            User = user;
            ShouldDisplay = shouldDisplay;
        }
    }

    public class NameEventArgs : RoomEventArgs
    {
        public string OldName { get; }
        public UserWithStatus User { get; }
        public bool ShouldDisplay { get; }

        public NameEventArgs(string? roomId, string oldName, UserWithStatus user, bool shouldDisplay) : base(roomId)
        {
            OldName = oldName;
            User = user;
            ShouldDisplay = shouldDisplay;
        }
    }

    public class ChatEventArgs : RoomEventArgs
    {
        public User User { get; }
        public string Message { get; }

        public ChatEventArgs(string? roomId, User user, string message) : base(roomId)
        {
            User = user;
            Message = message;
        }
    }

    public class NotifyEventArgs : RoomEventArgs
    {
        public string Title { get; }
        public string Message { get; }
        public string? HighlightToken { get; }

        public NotifyEventArgs(string? roomId, string title, string message, string? highlightToken) : base(roomId)
        {
            Title = title;
            Message = message;
            HighlightToken = highlightToken;
        }
    }

    public class ChatWithTimestampEventArgs : RoomEventArgs
    {
        public DateTime Timestamp { get; }
        public User User { get; }
        public string Message { get; }

        public ChatWithTimestampEventArgs(string? roomId, DateTime timestamp, User user, string message) : base(roomId)
        {
            Timestamp = timestamp;
            User = user;
            Message = message;
        }
    }

    public class TimestampEventArgs : EventArgs
    {
        public DateTime Timestamp { get; }

        public TimestampEventArgs(DateTime timestamp)
        {
            Timestamp = timestamp;
        }
    }

    public class BattleEventArgs : EventArgs
    {
        public string BattleRoomId { get; }
        public User User1 { get; }
        public User User2 { get; }
        public bool ShouldDisplay { get; }

        public BattleEventArgs(string battleRoomId, User user1, User user2, bool shouldDisplay)
        {
            BattleRoomId = battleRoomId;
            User1 = user1;
            User2 = user2;
            ShouldDisplay = shouldDisplay;
        }
    }

    #endregion

}
