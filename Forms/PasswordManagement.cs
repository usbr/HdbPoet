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
            try
            {
                DateTime tTemp = DateTime.Parse(userInfo.Rows[0]["EXPIRY_DATE"].ToString());
                labelPasswordExpiration.Text += userInfo.Rows[0]["EXPIRY_DATE"];
            }
            catch
            {
                labelPasswordExpiration.Text += "Not enabled...";
            }
            textBoxOldPwd.TextChanged += ValidatePassword;
            textBoxNewPwd1.TextChanged += ValidatePassword;
            textBoxNewPwd2.TextChanged += ValidatePassword;
            textBoxNewPwd1.Text = " "; // force pwd validation box to validate
            textBoxNewPwd1.Text = "";
        }

        private void ValidatePassword(object sender, EventArgs e)
        {
            
            string pwd1 = textBoxNewPwd1.Text;
            string pwd2 = textBoxNewPwd2.Text;
            string pwdOld = textBoxOldPwd.Text;

            bool pwdLength = pwd1.Length < 14;
            bool containsInt = !(pwd1.Count(c => char.IsDigit(c)) >= 2);
            bool containsLCase = !(pwd1.Count(c => char.IsUpper(c)) >= 2);
            bool containsUCase = !(pwd1.Count(c => char.IsLower(c)) >= 2);
            bool containsUName = pwd1.ToLower().Contains(uName.ToLower());
            bool containsSpecialChar = !(pwd1.Count(c => !char.IsLetterOrDigit(c)) >= 2);
            bool pwdsMatch = pwd1 != pwd2 || pwd1 == "";
            bool pwdOldIsBad = true;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"Password=(.*?)\;");
            var match = regex.Match(Hdb.Instance.Server.ConnectionString);
            if (match.Success)
            {
                var pwdOldGood = match.Groups[1].Value;
                if (pwdOld == pwdOldGood)
                {
                    pwdOldIsBad = false;
                }
            }

            if (pwdLength || containsInt || containsUCase || containsLCase || containsUName || 
                containsSpecialChar || pwdsMatch || pwdOldIsBad)
            {
                if (pwdOldIsBad)
                {
                    textBoxPwdCheck.Text = "Type in your current password... ";
                    textBoxPwdCheck.Text += "\r\n ";
                    textBoxPwdCheck.Text += "\r\nNew Password:";
                }
                else
                {
                    textBoxPwdCheck.Text = "New Password:";
                }                
                if (containsUName)
                {
                    textBoxPwdCheck.Text += "\r\n- cannot have your username... ";
                }
                if (pwdLength)
                {
                    textBoxPwdCheck.Text += "\r\n- needs at least 14 characters... ";
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
                if (pwdsMatch)
                {
                    textBoxPwdCheck.Text += "\r\n- new passwords do not match... ";
                }
                buttonUpdatePassword.Enabled = false;
            }
            else
            {
                textBoxPwdCheck.Text = "Password is Ok!";
                buttonUpdatePassword.Enabled = true;
            }
        }

        private void buttonUpdatePassword_Click(object sender, EventArgs e)
        {
            var pwdChg = Hdb.Instance.UpdatePassword(uName, textBoxOldPwd.Text, textBoxNewPwd1.Text);
            textBoxPwdCheck.Text = "Password Changed!";

        }
    }
}
