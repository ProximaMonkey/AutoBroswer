namespace client
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    public class MessageForm : Form
    {
        private ButtonClickDelegate _buttonCancleClick;
        private ButtonClickDelegate _buttonOKClick;
        private DateTime _closeTime = new DateTime();
        private Timer _messageFormTimer = new Timer();
        private ManageForm _parent;
        private Button buttonCancle;
        private Button buttonOK;
        private IContainer components;
        private Label labelText;

        public MessageForm()
        {
            this.InitializeComponent();
        }

        private void buttonCancle_Click(object sender, EventArgs e)
        {
            this._closeTime = DateTime.Now;
            this._messageFormTimer.Enabled = false;
            base.Visible = false;
            if ((this._parent != null) && (this._buttonCancleClick != null))
            {
                this._parent.Invoke(this._buttonCancleClick);
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this._closeTime = DateTime.Now;
            this._messageFormTimer.Enabled = false;
            base.Visible = false;
            if ((this._parent != null) && (this._buttonOKClick != null))
            {
                this._parent.Invoke(this._buttonOKClick);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void Init(ManageForm parent, string tipText, int interval, bool displayNow, ButtonClickDelegate buttonOKClick, ButtonClickDelegate buttonCancleClick)
        {
            TimeSpan span = (TimeSpan) (DateTime.Now - this._closeTime);
            if ((span.TotalSeconds >= 30.0) || displayNow)
            {
                this._closeTime = DateTime.Now;
                this._parent = parent;
                this.labelText.Text = tipText;
                this._buttonOKClick = buttonOKClick;
                this._buttonCancleClick = buttonCancleClick;
                if (interval > 0)
                {
                    this._messageFormTimer.Tick += new EventHandler(this.MessageFormThreadFunc);
                    this._messageFormTimer.Interval = interval * 0x3e8;
                    this._messageFormTimer.Enabled = true;
                }
                base.Visible = true;
            }
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(MessageForm));
            this.buttonCancle = new Button();
            this.buttonOK = new Button();
            this.labelText = new Label();
            base.SuspendLayout();
            this.buttonCancle.Location = new Point(0x9c, 0x41);
            this.buttonCancle.Name = "buttonCancle";
            this.buttonCancle.Size = new Size(0x4b, 0x17);
            this.buttonCancle.TabIndex = 2;
            this.buttonCancle.Text = "取消";
            this.buttonCancle.UseVisualStyleBackColor = true;
            this.buttonCancle.Click += new EventHandler(this.buttonCancle_Click);
            this.buttonOK.Location = new Point(0x30, 0x41);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new Size(0x4b, 0x17);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "确定";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new EventHandler(this.buttonOK_Click);
            this.labelText.AutoSize = true;
            this.labelText.Location = new Point(0x2e, 0x16);
            this.labelText.Name = "labelText";
            this.labelText.Size = new Size(0, 12);
            this.labelText.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x114, 110);
            base.Controls.Add(this.buttonCancle);
            base.Controls.Add(this.buttonOK);
            base.Controls.Add(this.labelText);
            base.Icon = (Icon) manager.GetObject("$this.Icon");
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "MessageForm";
            this.Text = "提示";
            base.FormClosing += new FormClosingEventHandler(this.MessageForm_FormClosing);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private void MessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.buttonCancle_Click(null, null);
            this._parent = null;
        }

        private void MessageFormThreadFunc(object sender, EventArgs ea)
        {
            this.buttonOK_Click(null, null);
        }

        public delegate void ButtonClickDelegate();
    }
}

