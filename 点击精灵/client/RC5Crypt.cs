namespace client
{
    using System;

    public class RC5Crypt
    {
        private bool mInitialisedRecv;
        private bool mInitialisedSend;
        private uint[] mRecvKey = new uint[RC5_T];
        private uint[] mRecvTxt = new uint[2];
        private uint[] mSendKey = new uint[RC5_T];
        private uint[] mSendTxt = new uint[2];
        private static uint RC5_B = 0x10;
        private static uint RC5_C = ((RC5_R * 8) / RC5_B);
        private static uint RC5_R = 12;
        private static uint RC5_T = ((2 * RC5_R) + 2);
        private static uint RC5_W = 0x20;

        private void _generateChildKey(ref byte[] KeyK, ref uint[] ChildKeyS)
        {
            int num3;
            uint num6;
            uint num7;
            uint num8;
            uint num = 0xb7e15163;
            uint num2 = 0x9e3779b9;
            uint[] numArray = new uint[RC5_C];
            ChildKeyS[0] = num;
            for (num3 = 1; num3 < RC5_T; num3++)
            {
                ChildKeyS[num3] = ChildKeyS[num3 - 1] + num2;
            }
            for (num3 = 0; num3 < RC5_C; num3++)
            {
                numArray[num3] = 0;
            }
            uint num4 = RC5_W / 8;
            for (num3 = ((int) RC5_B) - 1; num3 != -1; num3--)
            {
                numArray[(int) ((IntPtr) (((long) num3) / ((ulong) num4)))] = (numArray[(int) ((IntPtr) (((long) num3) / ((ulong) num4)))] << 8) + KeyK[num3];
            }
            uint index = num6 = num7 = num8 = 0;
            for (num3 = 0; num3 < (8 * RC5_T); num3++)
            {
                num7 = ChildKeyS[index] = this.ROTL((ChildKeyS[index] + num7) + num8, 3);
                index = (index + 1) % RC5_T;
                num8 = numArray[num6] = this.ROTL((numArray[num6] + num7) + num8, num7 + num8);
                num6 = (num6 + 1) % RC5_C;
            }
        }

        public void Close()
        {
            this.mInitialisedRecv = this.mInitialisedSend = false;
        }

        public void decryptRecv(ref uint[] In)
        {
            uint[] numArray = new uint[2];
            if (this.mInitialisedRecv)
            {
                uint num;
                uint num2;
                numArray[0] = num = In[0];
                numArray[1] = num2 = In[1];
                for (int i = (int) RC5_R; i > 0; i--)
                {
                    num2 = this.ROTR(num2 - this.mRecvKey[(2 * i) + 1], num) ^ num;
                    num = this.ROTR(num - this.mRecvKey[2 * i], num2) ^ num2;
                }
                In[0] = (num - this.mRecvKey[0]) ^ this.mRecvTxt[0];
                In[1] = (num2 - this.mRecvKey[1]) ^ this.mRecvTxt[1];
                this.mRecvTxt[0] = numArray[0];
                this.mRecvTxt[1] = numArray[1];
            }
        }

        public void encryptSend(ref uint[] In)
        {
            if (this.mInitialisedSend)
            {
                uint y = (In[0] ^ this.mSendTxt[0]) + this.mSendKey[0];
                uint num2 = (In[1] ^ this.mSendTxt[1]) + this.mSendKey[1];
                for (int i = 1; i <= RC5_R; i++)
                {
                    y = this.ROTL(y ^ num2, num2) + this.mSendKey[2 * i];
                    num2 = this.ROTL(num2 ^ y, y) + this.mSendKey[(2 * i) + 1];
                }
                this.mSendTxt[0] = In[0] = y;
                this.mSendTxt[1] = In[1] = num2;
            }
        }

        public void InitialiseRecv(uint seedRecv)
        {
            byte[] keyK = new byte[RC5_B];
            this.mInitialisedRecv = true;
            keyK[0] = (byte) seedRecv;
            for (int i = 1; i < RC5_B; i++)
            {
                keyK[i] = (byte) ((keyK[i - 1] * seedRecv) % 0x100);
            }
            this._generateChildKey(ref keyK, ref this.mRecvKey);
            this.mRecvTxt[1] = this.mRecvTxt[0] = seedRecv;
        }

        public void InitialiseSend(uint seedSend)
        {
            byte[] keyK = new byte[RC5_B];
            this.mInitialisedSend = true;
            keyK[0] = (byte) seedSend;
            for (int i = 1; i < RC5_B; i++)
            {
                keyK[i] = (byte) ((keyK[i - 1] * seedSend) % 0x100);
            }
            this._generateChildKey(ref keyK, ref this.mSendKey);
            this.mSendTxt[0] = seedSend;
            this.mSendTxt[1] = seedSend;
        }

        public bool IsInitialisedRecv()
        {
            return this.mInitialisedRecv;
        }

        public bool IsInitialisedSend()
        {
            return this.mInitialisedSend;
        }

        private uint ROTL(uint x, uint y)
        {
            return ((x << (y & (RC5_W - 1))) | (x >> (RC5_W - (y & (RC5_W - 1)))));
        }

        private uint ROTR(uint x, uint y)
        {
            return ((x >> (y & (RC5_W - 1))) | (x << (RC5_W - (y & (RC5_W - 1)))));
        }
    }
}

