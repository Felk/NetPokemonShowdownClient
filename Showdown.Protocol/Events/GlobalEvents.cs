using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Showdown.Protocol.Events
{
    public class PopupEventArgs : EventArgs
    {
        public string Message { get; }

        public PopupEventArgs(string message)
        {
            Message = message;
        }
    }

    public class PmEventArgs : EventArgs
    {
        public User Sender { get; }
        public User Receiver { get; }
        public string Message { get; }

        public PmEventArgs(User sender, User receiver, string message)
        {
            Sender = sender;
            Receiver = receiver;
            Message = message;
        }
    }

    public class UserCountEventArgs : EventArgs
    {
        public int Count { get; }

        public UserCountEventArgs(int count)
        {
            Count = count;
        }
    }

    public class NameTakenEventArgs : EventArgs
    {
        public string Username { get; }
        public string Message { get; }

        public NameTakenEventArgs(string username, string message)
        {
            Username = username;
            Message = message;
        }
    }

    public class ChallStrEventArgs : EventArgs
    {
        public string Challstr { get; }

        public ChallStrEventArgs(string challstr)
        {
            Challstr = challstr;
        }
    }

    public class UpdateUserEventArgs : EventArgs
    {
        public User User { get; }
        public bool Named { get; }
        public string Avatar { get; }
        // public IImmutableDictionary<string, object> Settings { get; }
        public string Settings { get; }

        public UpdateUserEventArgs(User user, bool named, string avatar, string settings)
        {
            User = user;
            Named = named;
            Avatar = avatar;
            Settings = settings;
        }
    }

    public class FormatsEventArgs : EventArgs
    {
        public IImmutableList<FormatSection> FormatSections { get; }
        public IEnumerable<Format> AllFormats => FormatSections.SelectMany(f => f.Formats);

        public FormatsEventArgs(IImmutableList<FormatSection> formatSections)
        {
            FormatSections = formatSections;
        }
    }

    public class UpdateSearchEventArgs : EventArgs
    {
        // public IImmutableDictionary<string, object> Data { get; }
        public string Data { get; }

        public UpdateSearchEventArgs(string data)
        {
            Data = data;
        }
    }

    public class UpdateChallengesEventArgs : EventArgs
    {
        // public IImmutableDictionary<string, object> Data { get; }
        public string Data { get; }

        public UpdateChallengesEventArgs(string data)
        {
            Data = data;
        }
    }

    public class QueryResponseEventArgs : EventArgs
    {
        public string Type { get; }
        // public IImmutableDictionary<string, object> Data { get; }
        public string Data { get; }

        public QueryResponseEventArgs(string type, string data)
        {
            Type = type;
            Data = data;
        }
    }
}
