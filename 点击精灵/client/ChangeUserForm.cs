namespace client
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class ChangeUserForm : Form
    {
        private ManageForm _parent;
        private Button buttonChangeUserLogin;
        private IContainer components;
        private Label label1;
        private Label label2;
        private Label labelAccountTip;
        private Label labelPasswordTip;
        private TextBox textBoxAccount;
        private TextBox textBoxPassword;

        public ChangeUserForm(ManageForm parent)
        {
            this._parent = parent;
            this.InitializeComponent();
        }

        private void buttonChangeUserLogin_Click(object sender, EventArgs e)
        {
            this.ShowAccountTip("");
            string str = WorldPacket.ResizeUTF8String(this.textBoxAccount.Text.Trim(), 0x40);
            this.textBoxAccount.Text = str;
            string str2 = WorldPacket.ResizeUTF8String(this.textBoxPassword.Text.Trim(), 0x20);
            this.textBoxPassword.Text = str2;
            if (string.IsNullOrEmpty(str))
            {
                this.ShowAccountTip("请输入您的用户名或Email地址");
            }
            else if ((string.IsNullOrEmpty(str2) || (str2.Length < 6)) || (str2.Length > 0x20))
            {
                this.ShowAccountTip("请输入6位-32位密码");
            }
            else
            {
                this._parent.ChangeUserLoginInfo(str, str2);
                base.Close();
            }
        }

        private void ChangeUserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._parent.ChangeUserForm = null;
            this._parent = null;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(ChangeUserForm));
            this.buttonChangeUserLogin = new Button();
            this.label1 = new Label();
            this.textBoxAccount = new TextBox();
            this.label2 = new Label();
            this.textBoxPassword = new TextBox();
            this.labelAccountTip = new Label();
            this.labelPasswordTip = new Label();
            base.SuspendLayout();
            this.buttonChangeUserLogin.Location = new Point(0x71, 130);
            this.buttonChangeUserLogin.Name = "buttonChangeUserLogin";
            this.buttonChangeUserLogin.Size = new Size(0x4b, 0x17);
            this.buttonChangeUserLogin.TabIndex = 3;
            this.buttonChangeUserLogin.Text = "登录";
            this.buttonChangeUserLogin.UseVisualStyleBackColor = true;
            this.buttonChangeUserLogin.Click += new EventHandler(this.buttonChangeUserLogin_Click);
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0x2f, 30);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x35, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "帐户名：";
            this.textBoxAccount.Location = new Point(100, 0x1b);
            this.textBoxAccount.Name = "textBoxAccount";
            this.textBoxAccount.Size = new Size(0x8b, 0x15);
            this.textBoxAccount.TabIndex = 1;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(0x3b, 0x53);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码：";
            this.textBoxPassword.Location = new Point(100, 80);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.Size = new Size(0x8b, 0x15);
            this.textBoxPassword.TabIndex = 2;
            this.textBoxPassword.UseSystemPasswordChar = true;
            this.labelAccountTip.AutoSize = true;
            this.labelAccountTip.ForeColor = Color.Red;
            this.labelAccountTip.Location = new Point(0x3b, 0x39);
            this.labelAccountTip.Name = "labelAccountTip";
            this.labelAccountTip.Size = new Size(0, 12);
            this.labelAccountTip.TabIndex = 4;
            this.labelPasswordTip.AutoSize = true;
            this.labelPasswordTip.ForeColor = Color.Red;
            this.labelPasswordTip.Location = new Point(0x3b, 0x71);
            this.labelPasswordTip.Name = "labelPasswordTip";
            this.labelPasswordTip.Size = new Size(0, 12);
            this.labelPasswordTip.TabIndex = 5;
            base.AcceptButton = this.buttonChangeUserLogin;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x11c, 0xac);
            base.Controls.Add(this.labelPasswordTip);
            base.Controls.Add(this.labelAccountTip);
            base.Controls.Add(this.textBoxPassword);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.textBoxAccount);
            base.Controls.Add(this.label1);
            base.Controls.Add(this.buttonChangeUserLogin);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.Name = "ChangeUserForm";
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "切换用户";
            base.FormClosing += new FormClosingEventHandler(this.ChangeUserForm_FormClosing);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void ShowAccountTip(string s)
        {
            this.labelAccountTip.Text = s;
        }
    }
}

