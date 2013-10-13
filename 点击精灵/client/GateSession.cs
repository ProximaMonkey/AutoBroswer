namespace client
{
    using System;

    public class GateSession
    {
        private string _account;
        private client.GateSocket _gateSocket = new client.GateSocket();
        public static int SHA_DIGEST_LENGTH = 20;

        public void DisConnect()
        {
            this._gateSocket.DisConnect();
        }

        public void Init(string account, string password, string K, uint accountTime, string IP)
        {
            this._account = account;
            this._gateSocket.ConnectToGate(K, account, password, accountTime, IP);
        }

        public bool IsConnecting()
        {
            if (!this._gateSocket.IsConnecting())
            {
                return this._gateSocket.HasNotProcessPacket();
            }
            return true;
        }

        public string Account
        {
            get
            {
                return this._account;
            }
            set
            {
                this._account = value;
            }
        }

        public client.GateSocket GateSocket
        {
            get
            {
                return this._gateSocket;
            }
        }
    }
}

