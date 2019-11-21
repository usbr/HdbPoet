using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HdbPoet
{
    public partial class PasswordManagement : Form
    {
        private string uName;
        public PasswordManagement()
        {
            InitializeComponent();
            var userInfo = Hdb.Instance.UserInfo();
            var a = 1;
            textBoxUser.Text = userInfo.Rows[0]["USERNAME"].ToString();
            uName = userInfo.Rows[0]["USERNAME"].ToString();
            labelUserId.Text += userInfo.Rows[0]["USER_ID"];
            labelAccountStatus.Text += userInfo.Rows[0]["ACCOUNT_STATUS"];
            labelAccountCreated.Text += userInfo.Rows[0]["CREATED"];
            labelPasswordExpiration.Text += userInfo.Rows[0]["EXPIRY_DATE"];
            textBoxNewPwd1.TextChanged += ValidatePassword;
            textBoxNewPwd2.TextChanged += ValidatePassword;
            textBoxNewPwd1.Text = " ";
            textBoxNewPwd1.Text = "";
        }

        private void ValidatePassword(object sender, EventArgs e)
        {
            textBoxPwdCheck.Text = "New Password:";
            string pwd1 = textBoxNewPwd1.Text;
            string pwd2 = textBoxNewPwd2.Text;

            bool pwdLength = pwd1.Length < 12;
            bool containsInt = !(pwd1.Count(c => char.IsDigit(c)) >= 2);
            bool containsLCase = !(pwd1.Count(c => char.IsUpper(c)) >= 2);
            bool containsUCase = !(pwd1.Count(c => char.IsLower(c)) >= 2);
            bool containsUName = pwd1.ToLower().Contains(uName.ToLower());
            bool containsSpecialChar = !pwd1.Any(ch => !Char.IsLetterOrDigit(ch));
            containsSpecialChar = !(pwd1.Count(c => !char.IsLetterOrDigit(c)) >= 2);

            if (pwdLength || containsInt || containsUCase || containsLCase || containsUName || containsSpecialChar)
            {
                if (containsUName)
                {
                    textBoxPwdCheck.Text += "\r\n- cannot have your username... ";
                }
                if (pwdLength)
                {
                    textBoxPwdCheck.Text += "\r\n- needs to be at least 12 characters... ";
                }
                if (containsInt)
                {
                    textBoxPwdCheck.Text += "\r\n- needs at least 2 numbers... ";
                }
                if (containsUCase)
                {
                    textBoxPwdCheck.Text += "\r\n- needs at least 2 lower-case characters... ";
                }
                if (containsLCase)
                {
                    textBoxPwdCheck.Text += "\r\n- needs at least 2 upper-case characters... ";
                }
                if (containsSpecialChar)
                {
                    textBoxPwdCheck.Text += "\r\n- needs at least 2 special characters... ";
                }
                buttonUpdatePassword.Enabled = false;
            }
            else
                {
                if (pwd1 != pwd2)
                {
                    textBoxPwdCheck.Text = "New passwords do not match...";
                    buttonUpdatePassword.Enabled = false;
                }
                else
                {
                    textBoxPwdCheck.Text = "Ok!";
                    buttonUpdatePassword.Enabled = true;
                }
            }
        }

        private void buttonUpdatePassword_Click(object sender, EventArgs e)
        {
            var pwdChg = Hdb.Instance.UpdatePassword(uName, textBoxOldPwd.Text, textBoxNewPwd1.Text);
            textBoxPwdCheck.Text = "Password Changed!";

        }
    }
}
