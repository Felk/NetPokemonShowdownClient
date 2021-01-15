using System;
using Showdown.Protocol.Events;

namespace Showdown.Protocol
{
    // TODO

    public class ShowdownStatefulClient : IDisposable
    {
        public ShowdownProtocolClient Client { get; }

        public User User { get; private set; }

        public ShowdownStatefulClient(ShowdownProtocolClient client)
        {
            Client = client;
            User = null!; // TODO
            SetUpListeners();
        }

        private void SetUpListeners()
        {
            Client.UpdateUser += UpdateUser;
        }

        private void TearDownListeners()
        {
            Client.UpdateUser -= UpdateUser;
        }

        private void UpdateUser(object? sender, UpdateUserEventArgs e)
        {
            User = e.User;
        }

        public void Dispose()
        {
            TearDownListeners();
        }
    }
}
