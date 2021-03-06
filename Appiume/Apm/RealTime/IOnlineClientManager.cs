﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appiume.Apm.RealTime
{
    /// <summary>
    /// Used to manage online clients those are connected to the application..
    /// </summary>
    public interface IOnlineClientManager
    {
        event EventHandler<OnlineClientEventArgs> ClientConnected;

        event EventHandler<OnlineClientEventArgs> ClientDisconnected;

        event EventHandler<OnlineUserEventArgs> UserConnected;

        event EventHandler<OnlineUserEventArgs> UserDisconnected;

        /// <summary>
        /// Adds a client.
        /// </summary>
        /// <param name="client">The client.</param>
        void Add(IOnlineClient client);

        /// <summary>
        /// Removes a client by connection id.
        /// </summary>
        /// <param name="connectionId">The connection id.</param>
        /// <returns>True, if a client is removed</returns>
        bool Remove(string connectionId);

        /// <summary>
        /// Tries to find a client by connection id.
        /// Returns null if not found.
        /// </summary>
        /// <param name="connectionId">connection id</param>
        IOnlineClient GetByConnectionIdOrNull(string connectionId);

        /// <summary>
        /// Gets all online clients.
        /// </summary>
        IReadOnlyList<IOnlineClient> GetAllClients();
    }

    public class OnlineClientEventArgs : EventArgs
    {
        public IOnlineClient Client { get; }

        public OnlineClientEventArgs(IOnlineClient client)
        {
            Client = client;
        }
    }

    public class OnlineUserEventArgs : OnlineClientEventArgs
    {
        public UserIdentifier User { get; }

        public OnlineUserEventArgs(UserIdentifier user, IOnlineClient client)
            : base(client)
        {
            User = user;
        }
    }
}
