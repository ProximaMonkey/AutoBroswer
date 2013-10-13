namespace client
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Net.Sockets;
    using System.Threading;

    public class ServerSocket
    {
        private uint _accountID;
        private uint _accountTime;
        private TcpClient _client;
        private bool _connecting;
        private RC5Crypt _Crypt = new RC5Crypt();
        private string _ip = string.Empty;
        private string _K = string.Empty;
        private Thread _netThread;
        private NetworkStream _networkStream;
        private ArrayList _packets = new ArrayList();
        private int _port;
        private BinaryReader _reader;
        private bool _threadRun;
        private BinaryWriter _writer;
        public const int SHA_DIGEST_LENGTH = 20;

        private void ConnectError()
        {
            byte[] destinationArray = new byte[4];
            uint num = 9;
            Array.Copy(BitConverter.GetBytes(num), 0, destinationArray, 0, 4);
            WorldPacket packet = new WorldPacket(4, destinationArray);
            lock (this._packets)
            {
                this._packets.Add(packet);
            }
        }

        public bool ConnectToServer(string ip, int port, string K, uint accountID, uint accountTime)
        {
            try
            {
                if (this._client != null)
                {
                    this.DisConnect();
                }
            }
            catch
            {
                return false;
            }
            this._ip = ip;
            this._port = port;
            this._K = K;
            this._accountID = accountID;
            this._accountTime = accountTime;
            this._Crypt.Close();
            this._connecting = true;
            this._netThread = new Thread(new ThreadStart(this.ThreadFunc));
            this._netThread.Start();
            return true;
        }

        public void DisConnect()
        {
            try
            {
                if (this._threadRun && this._netThread.IsAlive)
                {
                    this._netThread.Abort();
                }
                this._threadRun = false;
            }
            catch
            {
            }
            try
            {
                if ((this._client != null) && this._client.Connected)
                {
                    this._client.Close();
                    this._client = null;
                }
            }
            catch
            {
            }
            this._connecting = false;
        }

        public WorldPacket GetPacket()
        {
            WorldPacket packet = null;
            lock (this._packets)
            {
                if (this._packets.Count > 0)
                {
                    packet = (WorldPacket) this._packets[0];
                    this._packets.RemoveAt(0);
                }
            }
            return packet;
        }

        private int HandleCrypt(WorldPacket recvPacket)
        {
            if (!this._Crypt.IsInitialisedRecv() && (recvPacket != null))
            {
                uint seedRecv = recvPacket.ReadUInt32();
                this.Crypt.InitialiseRecv(seedRecv);
                return 0;
            }
            return -1;
        }

        public bool HasNotProcessPacket()
        {
            return (this._packets.Count > 0);
        }

        public bool IsConnected()
        {
            return this._threadRun;
        }

        public bool IsConnecting()
        {
            return this._connecting;
        }

        private void SendAuthPacket()
        {
            WorldPacket packet = new WorldPacket();
            int a = 0;
            string salt = LoginUtil.GenerateSalt();
            string str2 = LoginUtil.GenerateKey(this._K, salt);
            packet.Initialize(3);
            packet.InputUInt32(this._accountID);
            packet.InputUInt32(this._accountTime);
            if ((this._accountTime == 0) && string.IsNullOrEmpty(this._K))
            {
                a = 1;
            }
            packet.InputInt32(a);
            packet.InputString("1.0.0.37", 0x100);
            packet.InputString(salt, 0x40);
            packet.InputString(str2, 0x40);
            this.SendCorePacket(packet);
        }

        private void SendCorePacket(WorldPacket packet)
        {
            try
            {
                uint[] @in = new uint[2];
                uint num = (uint) (packet.Length % 8L);
                num = (num == 0) ? 0 : (8 - num);
                for (uint i = 0; i < num; i++)
                {
                    packet.InputByte(0);
                }
                uint length = (uint) packet.Length;
                @in[0] = length;
                @in[1] = packet.Opcode;
                this._Crypt.encryptSend(ref @in);
                this._writer.Write(@in[0]);
                this._writer.Write(@in[1]);
                for (uint j = 0; j < packet.Length; j += 8)
                {
                    @in[0] = BitConverter.ToUInt32(packet.ToArray(), (int) j);
                    @in[1] = BitConverter.ToUInt32(packet.ToArray(), ((int) j) + 4);
                    this._Crypt.encryptSend(ref @in);
                    this._writer.Write(@in[0]);
                    this._writer.Write(@in[1]);
                }
                this._writer.Flush();
            }
            catch (Exception)
            {
            }
        }

        private void SendCryptPacket()
        {
            if (!this._Crypt.IsInitialisedSend())
            {
                int a = new Random().Next();
                WorldPacket packet = new WorldPacket();
                packet.Initialize(5);
                packet.InputInt(a);
                this.SendCorePacket(packet);
                this._Crypt.InitialiseSend((uint) a);
            }
        }

        public void SendPacket(WorldPacket packet)
        {
            this.SendCryptPacket();
            this.SendAuthPacket();
            this.SendCorePacket(packet);
        }

        private void ThreadFunc()
        {
            try
            {
                if (!string.IsNullOrEmpty(this._ip))
                {
                    this._client = new TcpClient(AddressFamily.InterNetwork);
                    this._client.Connect(this._ip, this._port);
                    this._networkStream = this._client.GetStream();
                    this._reader = new BinaryReader(this._networkStream);
                    this._writer = new BinaryWriter(this._networkStream);
                    this._threadRun = true;
                }
                else
                {
                    Logger.Trace("连接网络服务器:IP为空");
                    this._threadRun = false;
                    this.ConnectError();
                }
            }
            catch (Exception exception)
            {
                Logger.Error(exception);
                this._threadRun = false;
                this._client = null;
                this.ConnectError();
            }
            while (this._threadRun)
            {
                byte[] destinationArray = null;
                WorldPacket recvPacket = null;
                uint num = 0;
                uint op = 0;
                try
                {
                    num = this._reader.ReadUInt32();
                    op = this._reader.ReadUInt32();
                    uint[] @in = new uint[] { num, op };
                    this._Crypt.decryptRecv(ref @in);
                    num = @in[0];
                    op = @in[1];
                    if ((num >= 0) && (num < 0xa00000))
                    {
                        if (num > 0)
                        {
                            destinationArray = new byte[num];
                            for (uint i = 0; i < num; i += 8)
                            {
                                @in[0] = this._reader.ReadUInt32();
                                @in[1] = this._reader.ReadUInt32();
                                this._Crypt.decryptRecv(ref @in);
                                Array.Copy(BitConverter.GetBytes(@in[0]), 0L, destinationArray, (long) i, 4L);
                                Array.Copy(BitConverter.GetBytes(@in[1]), 0L, destinationArray, (long) (i + 4), 4L);
                            }
                            recvPacket = new WorldPacket(op, destinationArray);
                        }
                        else
                        {
                            recvPacket = new WorldPacket(op);
                        }
                    }
                    else
                    {
                        this._threadRun = false;
                    }
                    if (!this._threadRun)
                    {
                        break;
                    }
                    if (recvPacket != null)
                    {
                        if (recvPacket.Opcode == 6)
                        {
                            this.HandleCrypt(recvPacket);
                        }
                        else
                        {
                            lock (this._packets)
                            {
                                this._packets.Add(recvPacket);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    break;
                }
                if (!this._threadRun)
                {
                    break;
                }
            }
            this._threadRun = false;
            if ((this._client != null) && this._client.Connected)
            {
                this._client.Close();
            }
            this._client = null;
            this._connecting = false;
        }

        public RC5Crypt Crypt
        {
            get
            {
                return this._Crypt;
            }
        }
    }
}

