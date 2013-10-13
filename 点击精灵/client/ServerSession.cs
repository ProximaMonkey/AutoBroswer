namespace client
{
    using System;

    public class ServerSession
    {
        private bool _authed;
        private client.ServerSocket _serverSocket = new client.ServerSocket();

        public void DisConnect()
        {
            this._serverSocket.DisConnect();
        }

        public void Init(string IP, int port, uint accountID, string K, uint accountTime)
        {
            this._serverSocket.ConnectToServer(IP, port, K, accountID, accountTime);
        }

        public bool IsConnected()
        {
            return this._serverSocket.IsConnected();
        }

        public bool IsConnecting()
        {
            if (!this._serverSocket.IsConnecting())
            {
                return this._serverSocket.HasNotProcessPacket();
            }
            return true;
        }

        public bool Authed
        {
            get
            {
                return this._authed;
            }
            set
            {
                this._authed = value;
            }
        }

        public client.ServerSocket ServerSocket
        {
            get
            {
                return this._serverSocket;
            }
        }
    }
}

