namespace HdbPoet
{
    partial class PasswordManagement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PasswordManagement));
            this.label1 = new System.Windows.Forms.Label();
            this.labelUserId = new System.Windows.Forms.Label();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.labelAccountStatus = new System.Windows.Forms.Label();
            this.labelAccountCreated = new System.Windows.Forms.Label();
            this.labelPasswordExpiration = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonUpdatePassword = new System.Windows.Forms.Button();
            this.textBoxNewPwd2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxNewPwd1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxOldPwd = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPwdCheck = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "User: ";
            // 
            // labelUserId
            // 
            this.labelUserId.AutoSize = true;
            this.labelUserId.Location = new System.Drawing.Point(12, 49);
            this.labelUserId.Name = "labelUserId";
            this.labelUserId.Size = new System.Drawing.Size(63, 17);
            this.labelUserId.TabIndex = 1;
            this.labelUserId.Text = "User ID: ";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Enabled = false;
            this.textBoxUser.Location = new System.Drawing.Point(54, 12);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(173, 22);
            this.textBoxUser.TabIndex = 2;
            // 
            // labelAccountStatus
            // 
            this.labelAccountStatus.AutoSize = true;
            this.labelAccountStatus.Location = new System.Drawing.Point(12, 81);
            this.labelAccountStatus.Name = "labelAccountStatus";
            this.labelAccountStatus.Size = new System.Drawing.Size(111, 17);
            this.labelAccountStatus.TabIndex = 3;
            this.labelAccountStatus.Text = "Account Status: ";
            // 
            // labelAccountCreated
            // 
            this.labelAccountCreated.AutoSize = true;
            this.labelAccountCreated.Location = new System.Drawing.Point(12, 115);
            this.labelAccountCreated.Name = "labelAccountCreated";
            this.labelAccountCreated.Size = new System.Drawing.Size(121, 17);
            this.labelAccountCreated.TabIndex = 4;
            this.labelAccountCreated.Text = "Account Created: ";
            // 
            // labelPasswordExpiration
            // 
            this.labelPasswordExpiration.AutoSize = true;
            this.labelPasswordExpiration.Location = new System.Drawing.Point(12, 150);
            this.labelPasswordExpiration.Name = "labelPasswordExpiration";
            this.labelPasswordExpiration.Size = new System.Drawing.Size(143, 17);
            this.labelPasswordExpiration.TabIndex = 5;
            this.labelPasswordExpiration.Text = "Password Expiration: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBoxPwdCheck);
            this.groupBox1.Controls.Add(this.buttonUpdatePassword);
            this.groupBox1.Controls.Add(this.textBoxNewPwd2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxNewPwd1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBoxOldPwd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(13, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 318);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Password Management";
            // 
            // buttonUpdatePassword
            // 
            this.buttonUpdatePassword.Enabled = false;
            this.buttonUpdatePassword.Location = new System.Drawing.Point(224, 110);
            this.buttonUpdatePassword.Name = "buttonUpdatePassword";
            this.buttonUpdatePassword.Size = new System.Drawing.Size(145, 34);
            this.buttonUpdatePassword.TabIndex = 13;
            this.buttonUpdatePassword.Text = "Update Password";
            this.buttonUpdatePassword.UseVisualStyleBackColor = true;
            this.buttonUpdatePassword.Click += new System.EventHandler(this.buttonUpdatePassword_Click);
            // 
            // textBoxNewPwd2
            // 
            this.textBoxNewPwd2.Location = new System.Drawing.Point(189, 82);
            this.textBoxNewPwd2.Name = "textBoxNewPwd2";
            this.textBoxNewPwd2.Size = new System.Drawing.Size(180, 22);
            this.textBoxNewPwd2.TabIndex = 11;
            this.textBoxNewPwd2.UseSystemPasswordChar = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(157, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Retype New Password: ";
            // 
            // textBoxNewPwd1
            // 
            this.textBoxNewPwd1.Location = new System.Drawing.Point(189, 53);
            this.textBoxNewPwd1.Name = "textBoxNewPwd1";
            this.textBoxNewPwd1.Size = new System.Drawing.Size(180, 22);
            this.textBoxNewPwd1.TabIndex = 8;
            this.textBoxNewPwd1.UseSystemPasswordChar = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(23, 58);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "New Password: ";
            // 
            // textBoxOldPwd
            // 
            this.textBoxOldPwd.Location = new System.Drawing.Point(189, 25);
            this.textBoxOldPwd.Name = "textBoxOldPwd";
            this.textBoxOldPwd.Size = new System.Drawing.Size(180, 22);
            this.textBoxOldPwd.TabIndex = 7;
            this.textBoxOldPwd.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Old Password: ";
            // 
            // textBoxPwdCheck
            // 
            this.textBoxPwdCheck.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPwdCheck.Enabled = false;
            this.textBoxPwdCheck.Location = new System.Drawing.Point(26, 150);
            this.textBoxPwdCheck.Multiline = true;
            this.textBoxPwdCheck.Name = "textBoxPwdCheck";
            this.textBoxPwdCheck.Size = new System.Drawing.Size(343, 162);
            this.textBoxPwdCheck.TabIndex = 14;
            // 
            // PasswordManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 506);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelPasswordExpiration);
            this.Controls.Add(this.labelAccountCreated);
            this.Controls.Add(this.labelAccountStatus);
            this.Controls.Add(this.textBoxUser);
            this.Controls.Add(this.labelUserId);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(418, 553);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(418, 553);
            this.Name = "PasswordManagement";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Password Management";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelUserId;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.Label labelAccountStatus;
        private System.Windows.Forms.Label labelAccountCreated;
        private System.Windows.Forms.Label labelPasswordExpiration;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonUpdatePassword;
        private System.Windows.Forms.TextBox textBoxNewPwd2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxNewPwd1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxOldPwd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPwdCheck;
    }
}