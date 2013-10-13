namespace client
{
    using System;
    using System.IO;
    using System.Text;

    public class WorldPacket : MemoryStream
    {
        private BinaryReader _binaryReader;
        private uint _opcode;
        public static uint PACKAGE_MAX_LENGTH = 0x100000;

        public WorldPacket() : base(0x2800)
        {
        }

        public WorldPacket(uint op) : base(0x2800)
        {
            this._opcode = op;
        }

        public WorldPacket(uint op, byte[] bytes) : base(bytes)
        {
            this._opcode = op;
            if (bytes != null)
            {
                this._binaryReader = new BinaryReader(this);
            }
        }

        public void Initialize(uint op)
        {
            this._opcode = op;
        }

        public void InputByte(byte a)
        {
            this.WriteByte(a);
        }

        public void InputBytes(byte[] str)
        {
            this.Write(str, 0, str.Length);
        }

        public void InputInt(int a)
        {
            byte[] bytes = BitConverter.GetBytes(a);
            this.Write(bytes, 0, bytes.Length);
        }

        public void InputInt32(int a)
        {
            byte[] bytes = BitConverter.GetBytes(a);
            this.Write(bytes, 0, bytes.Length);
        }

        public void InputString(string str2, uint maxSize)
        {
            byte[] bytes;
            while (true)
            {
                bytes = Encoding.UTF8.GetBytes(str2);
                if (bytes.Length < maxSize)
                {
                    break;
                }
                str2 = str2.Substring(0, str2.Length - 1);
            }
            this.Write(bytes, 0, bytes.Length);
            this.WriteByte(0);
        }

        public void InputUInt32(uint a)
        {
            byte[] bytes = BitConverter.GetBytes(a);
            this.Write(bytes, 0, bytes.Length);
        }

        public byte ReadByte()
        {
            return this._binaryReader.ReadByte();
        }

        public byte[] ReadBytes(int count)
        {
            return this._binaryReader.ReadBytes(count);
        }

        public int ReadInt32()
        {
            return this._binaryReader.ReadInt32();
        }

        public string ReadString()
        {
            uint num = this._binaryReader.ReadUInt32();
            if (num > 0)
            {
                byte[] buffer = new byte[num];
                this._binaryReader.Read(buffer, 0, buffer.Length);
                return Encoding.UTF8.GetString(buffer);
            }
            return string.Empty;
        }

        public uint ReadUInt32()
        {
            return this._binaryReader.ReadUInt32();
        }

        public static string ResizeUTF8String(string str2, uint maxSize)
        {
            string s = str2;
            while (true)
            {
                Encoding.UTF8.GetBytes(s);
                if (s.Length < maxSize)
                {
                    return s;
                }
                s = s.Substring(0, s.Length - 1);
            }
        }

        public uint Opcode
        {
            get
            {
                return this._opcode;
            }
        }
    }
}

