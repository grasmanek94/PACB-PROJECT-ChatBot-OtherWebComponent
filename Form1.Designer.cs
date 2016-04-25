namespace ChatBot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btn_AddBot = new System.Windows.Forms.Button();
            this.browsers = new System.Windows.Forms.TabControl();
            this.btn_wzj = new System.Windows.Forms.Button();
            this.btn_lftd = new System.Windows.Forms.Button();
            this.btn_wnplts = new System.Windows.Forms.Button();
            this.btn_mq = new System.Windows.Forms.Button();
            this.btn_ikzk = new System.Windows.Forms.Button();
            this.btn_state = new System.Windows.Forms.Button();
            this.btn_ignore = new System.Windows.Forms.Button();
            this.btn_rst = new System.Windows.Forms.Button();
            this.btn_endcht = new System.Windows.Forms.Button();
            this.btn_kpon = new System.Windows.Forms.Button();
            this.btn_stpstr = new System.Windows.Forms.Button();
            this.btn_zdjw = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_AddBot
            // 
            this.btn_AddBot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_AddBot.Location = new System.Drawing.Point(12, 502);
            this.btn_AddBot.Name = "btn_AddBot";
            this.btn_AddBot.Size = new System.Drawing.Size(90, 21);
            this.btn_AddBot.TabIndex = 0;
            this.btn_AddBot.Text = "New Bot";
            this.btn_AddBot.UseVisualStyleBackColor = true;
            this.btn_AddBot.Click += new System.EventHandler(this.btn_AddBot_Click);
            // 
            // browsers
            // 
            this.browsers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browsers.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.browsers.Font = new System.Drawing.Font("Lucida Console", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.browsers.ItemSize = new System.Drawing.Size(32, 18);
            this.browsers.Location = new System.Drawing.Point(0, 0);
            this.browsers.Multiline = true;
            this.browsers.Name = "browsers";
            this.browsers.Padding = new System.Drawing.Point(3, 3);
            this.browsers.SelectedIndex = 0;
            this.browsers.Size = new System.Drawing.Size(1192, 496);
            this.browsers.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.browsers.TabIndex = 1;
            this.browsers.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.browsers_DrawItem);
            this.browsers.TabIndexChanged += new System.EventHandler(this.browsers_TabIndexChanged);
            // 
            // btn_wzj
            // 
            this.btn_wzj.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_wzj.Location = new System.Drawing.Point(108, 502);
            this.btn_wzj.Name = "btn_wzj";
            this.btn_wzj.Size = new System.Drawing.Size(87, 21);
            this.btn_wzj.TabIndex = 2;
            this.btn_wzj.Text = "Wat zoek je?";
            this.btn_wzj.UseVisualStyleBackColor = true;
            this.btn_wzj.Click += new System.EventHandler(this.btn_wzj_Click);
            // 
            // btn_lftd
            // 
            this.btn_lftd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_lftd.Location = new System.Drawing.Point(201, 502);
            this.btn_lftd.Name = "btn_lftd";
            this.btn_lftd.Size = new System.Drawing.Size(87, 21);
            this.btn_lftd.TabIndex = 3;
            this.btn_lftd.Text = "Leeftijd?";
            this.btn_lftd.UseVisualStyleBackColor = true;
            this.btn_lftd.Click += new System.EventHandler(this.btn_lftd_Click);
            // 
            // btn_wnplts
            // 
            this.btn_wnplts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_wnplts.Location = new System.Drawing.Point(294, 502);
            this.btn_wnplts.Name = "btn_wnplts";
            this.btn_wnplts.Size = new System.Drawing.Size(87, 21);
            this.btn_wnplts.TabIndex = 4;
            this.btn_wnplts.Text = "Woonplaats?";
            this.btn_wnplts.UseVisualStyleBackColor = true;
            this.btn_wnplts.Click += new System.EventHandler(this.btn_wnplts_Click);
            // 
            // btn_mq
            // 
            this.btn_mq.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_mq.Location = new System.Drawing.Point(387, 502);
            this.btn_mq.Name = "btn_mq";
            this.btn_mq.Size = new System.Drawing.Size(87, 21);
            this.btn_mq.TabIndex = 5;
            this.btn_mq.Text = "Meisje?";
            this.btn_mq.UseVisualStyleBackColor = true;
            this.btn_mq.Click += new System.EventHandler(this.btn_mq_Click);
            // 
            // btn_ikzk
            // 
            this.btn_ikzk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ikzk.Location = new System.Drawing.Point(480, 502);
            this.btn_ikzk.Name = "btn_ikzk";
            this.btn_ikzk.Size = new System.Drawing.Size(87, 21);
            this.btn_ikzk.TabIndex = 6;
            this.btn_ikzk.Text = "Ik zoek";
            this.btn_ikzk.UseVisualStyleBackColor = true;
            this.btn_ikzk.Click += new System.EventHandler(this.btn_ikzk_Click);
            // 
            // btn_state
            // 
            this.btn_state.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_state.Location = new System.Drawing.Point(666, 502);
            this.btn_state.Name = "btn_state";
            this.btn_state.Size = new System.Drawing.Size(87, 21);
            this.btn_state.TabIndex = 7;
            this.btn_state.Text = "Pause";
            this.btn_state.UseVisualStyleBackColor = true;
            this.btn_state.Click += new System.EventHandler(this.btn_state_Click);
            // 
            // btn_ignore
            // 
            this.btn_ignore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ignore.Location = new System.Drawing.Point(573, 502);
            this.btn_ignore.Name = "btn_ignore";
            this.btn_ignore.Size = new System.Drawing.Size(87, 21);
            this.btn_ignore.TabIndex = 8;
            this.btn_ignore.Text = "Negeer";
            this.btn_ignore.UseVisualStyleBackColor = true;
            this.btn_ignore.Click += new System.EventHandler(this.btn_ignore_Click);
            // 
            // btn_rst
            // 
            this.btn_rst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_rst.Location = new System.Drawing.Point(1107, 502);
            this.btn_rst.Name = "btn_rst";
            this.btn_rst.Size = new System.Drawing.Size(75, 21);
            this.btn_rst.TabIndex = 9;
            this.btn_rst.Text = "Reset";
            this.btn_rst.UseVisualStyleBackColor = true;
            this.btn_rst.Click += new System.EventHandler(this.btn_rst_Click);
            // 
            // btn_endcht
            // 
            this.btn_endcht.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_endcht.Location = new System.Drawing.Point(759, 502);
            this.btn_endcht.Name = "btn_endcht";
            this.btn_endcht.Size = new System.Drawing.Size(87, 21);
            this.btn_endcht.TabIndex = 10;
            this.btn_endcht.Text = "End Chat";
            this.btn_endcht.UseVisualStyleBackColor = true;
            this.btn_endcht.Click += new System.EventHandler(this.btn_endcht_Click);
            // 
            // btn_kpon
            // 
            this.btn_kpon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_kpon.Location = new System.Drawing.Point(852, 502);
            this.btn_kpon.Name = "btn_kpon";
            this.btn_kpon.Size = new System.Drawing.Size(87, 21);
            this.btn_kpon.TabIndex = 11;
            this.btn_kpon.Text = "Keep On";
            this.btn_kpon.UseVisualStyleBackColor = true;
            this.btn_kpon.Click += new System.EventHandler(this.btn_kpon_Click);
            // 
            // btn_stpstr
            // 
            this.btn_stpstr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_stpstr.Location = new System.Drawing.Point(945, 502);
            this.btn_stpstr.Name = "btn_stpstr";
            this.btn_stpstr.Size = new System.Drawing.Size(75, 21);
            this.btn_stpstr.TabIndex = 12;
            this.btn_stpstr.Text = "Stop Story";
            this.btn_stpstr.UseVisualStyleBackColor = true;
            this.btn_stpstr.Click += new System.EventHandler(this.btn_stpstr_Click);
            // 
            // btn_zdjw
            // 
            this.btn_zdjw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_zdjw.Location = new System.Drawing.Point(1026, 502);
            this.btn_zdjw.Name = "btn_zdjw";
            this.btn_zdjw.Size = new System.Drawing.Size(75, 21);
            this.btn_zdjw.TabIndex = 13;
            this.btn_zdjw.Text = "NGNFNS";
            this.btn_zdjw.UseVisualStyleBackColor = true;
            this.btn_zdjw.Click += new System.EventHandler(this.btn_zdjw_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 526);
            this.Controls.Add(this.btn_zdjw);
            this.Controls.Add(this.btn_stpstr);
            this.Controls.Add(this.btn_kpon);
            this.Controls.Add(this.btn_endcht);
            this.Controls.Add(this.btn_rst);
            this.Controls.Add(this.btn_ignore);
            this.Controls.Add(this.btn_state);
            this.Controls.Add(this.btn_ikzk);
            this.Controls.Add(this.btn_mq);
            this.Controls.Add(this.btn_wnplts);
            this.Controls.Add(this.btn_lftd);
            this.Controls.Add(this.btn_wzj);
            this.Controls.Add(this.browsers);
            this.Controls.Add(this.btn_AddBot);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "ChatBot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_AddBot;
        private System.Windows.Forms.TabControl browsers;
        private System.Windows.Forms.Button btn_wzj;
        private System.Windows.Forms.Button btn_lftd;
        private System.Windows.Forms.Button btn_wnplts;
        private System.Windows.Forms.Button btn_mq;
        private System.Windows.Forms.Button btn_ikzk;
        private System.Windows.Forms.Button btn_state;
        private System.Windows.Forms.Button btn_ignore;
        private System.Windows.Forms.Button btn_rst;
        private System.Windows.Forms.Button btn_endcht;
        private System.Windows.Forms.Button btn_kpon;
        private System.Windows.Forms.Button btn_stpstr;
        private System.Windows.Forms.Button btn_zdjw;
    }
}

