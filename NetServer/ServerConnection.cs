﻿using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

namespace GameCore.NetAlpha.Server
{
    public class ServerConnection<T> : Connection<T> where T : struct, Enum
    {
        private BlockingCollection<OwnedMessage<T>> m_qIncomingMessages = new BlockingCollection<OwnedMessage<T>>();

        public ulong UID
        {
            get;
            protected set;
        }

        internal ServerConnection(BlockingCollection<OwnedMessage<T>> queue, Socket socket) : base(socket)
        {
            m_qIncomingMessages = queue;
        }

        public bool ConnectToClient(ulong uid, bool connectionLess = false)
        {
            if (Connected || connectionLess)
            {
                UID = uid;
                OnConnectToClient();
                return true;
            }
            return false;
        }

        protected virtual void OnConnectToClient() { }

        protected override void AddToIncomingMessages()
        {
            m_qIncomingMessages.Add(new OwnedMessage<T>() { message=m_vTmpMessage, UID = UID });
            base.AddToIncomingMessages();
        }
    }
}
