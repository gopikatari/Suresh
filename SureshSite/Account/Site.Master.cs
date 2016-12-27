using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SureshSite.Account
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
         private void AuthenticateUser(string username, string password)
        {
            // ConfigurationManager class is in System.Configuration namespace
            string CS = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            // SqlConnection is in System.Data.SqlClient namespace
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spAuthenticateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //Formsauthentication is in system.web.security
                //string encryptedpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

                //sqlparameter is in System.Data namespace
                SqlParameter paramUsername = new SqlParameter("@UserName", username);
                SqlParameter paramPassword = new SqlParameter("@Password", password);

                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPassword);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int RetryAttempts = Convert.ToInt32(rdr["RetryAttempts"]);
                    if (Convert.ToBoolean(rdr["AccountLocked"]))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Account locked. Please contact administrator')", true);
                       // lblMessage.Text = "Account locked. Please contact administrator";
                    }
                    else if (RetryAttempts > 0)
                    {
                        int AttemptsLeft = (4 - RetryAttempts);
                     string str = "Invalid user name and/or password. " +
                            AttemptsLeft.ToString() + "attempt(s) left";
                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('"+str+"')", true);
                    }
                    else if (Convert.ToBoolean(rdr["Authenticated"]))
                    {
                        Session["user"] = txtUserName.Text;
                        FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, chkRememberme.Checked);
                    }
                }
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            AuthenticateUser(txtUserName.Text, txtPassword.Text);


            }
        

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            // Read the connection string from web.config.
            // ConfigurationManager class is in System.Configuration namespace
            string CS = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            // SqlConnection is in System.Data.SqlClient namespace
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spRegisterUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter username = new SqlParameter("@UserName", txtRegUser.Text);
                // FormsAuthentication calss is in System.Web.Security namespace
               // string encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
                SqlParameter password = new SqlParameter("@Password", txtRegpwd1.Text);
                SqlParameter email = new SqlParameter("@Email", txtRegEmail.Text);

                cmd.Parameters.Add(username);
                cmd.Parameters.Add(password);
                cmd.Parameters.Add(email);

                con.Open();
                int ReturnCode = (int)cmd.ExecuteScalar();
                if (ReturnCode == -1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('User Name already in use, please choose another user name')", true);
                    //lblMessage.Text = "User Name already in use, please choose another user name";
                }
                else
                {
                    Response.Redirect("~/Home.aspx");
                }
            }
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('AtesssssssPlease contact administrator')", true);
        }

       
        private void SendPasswordResetEmail(string ToEmail, string UserName, string UniqueId)
        {
            // MailMessage class is present is System.Net.Mail namespace
            MailMessage mailMessage = new MailMessage("katarigopi@gmail.com", ToEmail);


            // StringBuilder class is present in System.Text namespace
            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("Dear " + UserName + ",<br/><br/>");
            sbEmailBody.Append("Please click on the following link to reset your password");
            sbEmailBody.Append("<br/>"); sbEmailBody.Append("http://localhost:50309/Account/Changepassword.aspx?uid=" + UniqueId);
            sbEmailBody.Append("<br/><br/>");
            sbEmailBody.Append("<b>Change Password</b>");

            mailMessage.IsBodyHtml = true;

            mailMessage.Body = sbEmailBody.ToString();
            mailMessage.Subject = "Reset Your Password";
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "katarigopi@gmail.com",
                Password = "Balaji_1992"
            };

            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }

        protected void btnForgot_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spResetPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUsername = new SqlParameter("@UserName", txtForgotUsername.Text);

                cmd.Parameters.Add(paramUsername);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (Convert.ToBoolean(rdr["ReturnCode"]))
                    {

                        SendPasswordResetEmail(rdr["Email"].ToString(), txtForgotUsername.Text, rdr["UniqueId"].ToString());
                        
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('An email with instructions to reset your password is sent to your registered email')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('UserName Not Found')", true);
                        
                    }
                }
            }
        }
    }
}