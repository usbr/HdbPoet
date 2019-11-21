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

            bool pwdLength = pwd1.Length < 8 || pwd1.Length > 20;
            bool containsInt = !pwd1.Any(char.IsDigit);
            bool containsUCase = !pwd1.Any(char.IsUpper);
            bool containsLCase = !pwd1.Any(char.IsLower);
            bool containsUName = pwd1.ToLower().Contains(uName.ToLower());
            bool containsSpecialChar = !pwd1.Any(ch => !Char.IsLetterOrDigit(ch));

            if (pwdLength || containsInt || containsUCase || containsLCase || containsUName || containsSpecialChar)
            {
                if (containsUName)
                {
                    textBoxPwdCheck.Text += "\r\n- cannot have your username... ";
                }
                if (pwdLength)
                {
                    textBoxPwdCheck.Text += "\r\n- needs to be between 8 and 20 characters... ";
                }
                if (containsInt)
                {
                    textBoxPwdCheck.Text += "\r\n- needs at least 1 number... ";
                }
                if (containsUCase)
                {
                    textBoxPwdCheck.Text += "\r\n- needs at least 1 lower-case character... ";
                }
                if (containsLCase)
                {
                    textBoxPwdCheck.Text += "\r\n- needs at least 1 upper-case character... ";
                }
                if (containsSpecialChar)
                {
                    textBoxPwdCheck.Text += "\r\n- needs at least 1 special character... ";
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
