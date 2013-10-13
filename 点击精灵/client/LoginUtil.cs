namespace client
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Xml;

    public class LoginUtil
    {
        private string _account = string.Empty;
        private uint _accountID;
        private uint _accountTime;
        private uint _dayCompleteTask;
        private uint _dayConsumeScore;
        private uint _dayNetTaskCount;
        private uint _dayNetTaskScore;
        private uint _getTotalScore;
        private uint _getTotalTask;
        private string _K = string.Empty;
        private string _password = string.Empty;
        private uint _publishTotalScore;
        private uint _publishTotalTask;
        private uint _score;
        private uint _serverTasksCount;
        private uint _vipRemainDays;
        private const string SETTINGFILE = "account.xml";

        public LoginUtil()
        {
            this.LoadSetting();
        }

        public static string GenerateKey(string M, string salt)
        {
            int destinationIndex = 0;
            byte[] destinationArray = new byte[0x400];
            MD5 md = new MD5CryptoServiceProvider();
            destinationIndex = 0;
            byte[] bytes = Encoding.Default.GetBytes(M);
            byte[] sourceArray = Encoding.Default.GetBytes(salt);
            Array.Copy(bytes, 0, destinationArray, destinationIndex, bytes.Length);
            destinationIndex += bytes.Length;
            Array.Copy(sourceArray, 0, destinationArray, destinationIndex, sourceArray.Length);
            destinationIndex += sourceArray.Length;
            byte[] buffer2 = md.ComputeHash(destinationArray, 0, destinationIndex);
            md.Clear();
            string str = "";
            for (int i = 0; i < buffer2.Length; i++)
            {
                str = str + buffer2[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }

        public static string GenerateKey(string account, string password, string salt)
        {
            int destinationIndex = 0;
            byte[] destinationArray = new byte[0x400];
            byte[] bytes = Encoding.Default.GetBytes("DJJLSEO");
            byte[] sourceArray = Encoding.Default.GetBytes(":");
            byte[] buffer4 = Encoding.Default.GetBytes(password);
            string s = string.Empty;
            MD5 md = new MD5CryptoServiceProvider();
            Array.Copy(bytes, 0, destinationArray, destinationIndex, bytes.Length);
            destinationIndex += bytes.Length;
            Array.Copy(sourceArray, 0, destinationArray, destinationIndex, sourceArray.Length);
            destinationIndex += sourceArray.Length;
            Array.Copy(buffer4, 0, destinationArray, destinationIndex, buffer4.Length);
            destinationIndex += buffer4.Length;
            byte[] buffer5 = md.ComputeHash(destinationArray, 0, destinationIndex);
            s = "";
            for (int i = 0; i < buffer5.Length; i++)
            {
                s = s + buffer5[i].ToString("x").PadLeft(2, '0');
            }
            destinationIndex = 0;
            bytes = Encoding.Default.GetBytes(s);
            sourceArray = Encoding.Default.GetBytes(salt);
            Array.Copy(bytes, 0, destinationArray, destinationIndex, bytes.Length);
            destinationIndex += bytes.Length;
            Array.Copy(sourceArray, 0, destinationArray, destinationIndex, sourceArray.Length);
            destinationIndex += sourceArray.Length;
            buffer5 = md.ComputeHash(destinationArray, 0, destinationIndex);
            md.Clear();
            s = "";
            for (int j = 0; j < buffer5.Length; j++)
            {
                s = s + buffer5[j].ToString("x").PadLeft(2, '0');
            }
            return s;
        }

        public static string GenerateOrginalKey(string account, string password)
        {
            int destinationIndex = 0;
            byte[] destinationArray = new byte[0x400];
            byte[] bytes = Encoding.Default.GetBytes("DJJLSEO");
            byte[] sourceArray = Encoding.Default.GetBytes(":");
            byte[] buffer4 = Encoding.Default.GetBytes(password);
            string str = string.Empty;
            MD5 md = new MD5CryptoServiceProvider();
            Array.Copy(bytes, 0, destinationArray, destinationIndex, bytes.Length);
            destinationIndex += bytes.Length;
            Array.Copy(sourceArray, 0, destinationArray, destinationIndex, sourceArray.Length);
            destinationIndex += sourceArray.Length;
            Array.Copy(buffer4, 0, destinationArray, destinationIndex, buffer4.Length);
            destinationIndex += buffer4.Length;
            byte[] buffer5 = md.ComputeHash(destinationArray, 0, destinationIndex);
            str = "";
            for (int i = 0; i < buffer5.Length; i++)
            {
                str = str + buffer5[i].ToString("x").PadLeft(2, '0');
            }
            return str;
        }

        public static string GenerateSalt()
        {
            int num = 8;
            string[] strArray = new string[] { 
                "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f", 
                "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", 
                "w", "x", "y", "z", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", 
                "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
             };
            string str = "";
            Random random = new Random();
            for (int i = 0; i < num; i++)
            {
                str = str + strArray[random.Next(0, strArray.Length)];
            }
            return str;
        }

        public void InitLoginInfo(uint accountID, string account, uint accountTime, string K)
        {
            this._accountID = accountID;
            this._account = account;
            this._accountTime = accountTime;
            this._K = K;
        }

        public void LoadSetting()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load("account.xml");
                foreach (XmlNode node2 in document.SelectSingleNode("root").ChildNodes)
                {
                    if (node2.Name.Equals("account"))
                    {
                        this._account = node2.InnerText;
                    }
                    else
                    {
                        if (node2.Name.Equals("p1"))
                        {
                            this._K = node2.InnerText;
                            continue;
                        }
                        if (node2.Name.Equals("accountTime"))
                        {
                            this._accountTime = WindowUtil.StringToUint(node2.InnerText);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void LoginSuccess(uint accountID, string account, uint accountTime, string K)
        {
            this.InitLoginInfo(accountID, account, accountTime, K);
            this.SaveSetting();
        }

        public void Reset()
        {
            this._accountID = 0;
            this._account = string.Empty;
            this._password = string.Empty;
            this._K = string.Empty;
            this._accountTime = 0;
            this._score = 0;
            this._dayNetTaskScore = 0;
            this._dayNetTaskCount = 0;
            this._serverTasksCount = 0;
            this._dayCompleteTask = 0;
            this._dayConsumeScore = 0;
        }

        public void SaveSetting()
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlElement newChild = document.CreateElement("root");
                if (!string.IsNullOrEmpty(this._K))
                {
                    XmlElement element2 = document.CreateElement("p1");
                    element2.InnerText = this._K;
                    newChild.AppendChild(element2);
                }
                if (!string.IsNullOrEmpty(this._account))
                {
                    XmlElement element3 = document.CreateElement("account");
                    element3.InnerText = this._account;
                    newChild.AppendChild(element3);
                }
                if (this._accountTime != 0)
                {
                    XmlElement element4 = document.CreateElement("accountTime");
                    element4.InnerText = this._accountTime.ToString();
                    newChild.AppendChild(element4);
                }
                document.AppendChild(newChild);
                document.Save("account.xml");
            }
            catch (Exception)
            {
            }
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

        public uint AccountID
        {
            get
            {
                return this._accountID;
            }
            set
            {
                this._accountID = value;
            }
        }

        public uint AccountTime
        {
            get
            {
                return this._accountTime;
            }
            set
            {
                this._accountTime = value;
            }
        }

        public uint DayCompleteTask
        {
            get
            {
                return this._dayCompleteTask;
            }
            set
            {
                this._dayCompleteTask = value;
            }
        }

        public uint DayConsumeScore
        {
            get
            {
                return this._dayConsumeScore;
            }
            set
            {
                this._dayConsumeScore = value;
            }
        }

        public uint DayNetTaskCount
        {
            get
            {
                return this._dayNetTaskCount;
            }
            set
            {
                this._dayNetTaskCount = value;
            }
        }

        public uint DayNetTaskScore
        {
            get
            {
                return this._dayNetTaskScore;
            }
            set
            {
                this._dayNetTaskScore = value;
            }
        }

        public uint GetTotalScore
        {
            get
            {
                return this._getTotalScore;
            }
            set
            {
                this._getTotalScore = value;
            }
        }

        public uint GetTotalTask
        {
            get
            {
                return this._getTotalTask;
            }
            set
            {
                this._getTotalTask = value;
            }
        }

        public string K
        {
            get
            {
                return this._K;
            }
            set
            {
                this._K = value;
            }
        }

        public string Password
        {
            get
            {
                return this._password;
            }
            set
            {
                this._password = value;
            }
        }

        public uint PublishTotalScore
        {
            get
            {
                return this._publishTotalScore;
            }
            set
            {
                this._publishTotalScore = value;
            }
        }

        public uint PublishTotalTask
        {
            get
            {
                return this._publishTotalTask;
            }
            set
            {
                this._publishTotalTask = value;
            }
        }

        public uint Score
        {
            get
            {
                return this._score;
            }
            set
            {
                this._score = value;
            }
        }

        public uint ServerTasksCount
        {
            get
            {
                return this._serverTasksCount;
            }
            set
            {
                this._serverTasksCount = value;
            }
        }

        public uint VipRemainDays
        {
            get
            {
                return this._vipRemainDays;
            }
            set
            {
                this._vipRemainDays = value;
            }
        }
    }
}

