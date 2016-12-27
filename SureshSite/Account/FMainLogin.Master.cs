using System;
using System.Web.Security;
using System.Data.SqlClient;
using SureshSite.DAL;
using System.Web.UI;
using System.Configuration;
using System.Data;
namespace SureshSite
{
    public partial class FMain : System.Web.UI.MasterPage
    {
        Users u = new Users();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin1_Click(object sender, EventArgs e)
        {
         
            SqlDataReader dr = u.AuthenticateUser(txtUserName.Text, txtPassword.Text);
            while (dr.Read())
            {
                int RetryAttempts = Convert.ToInt32(dr["RetryAttempts"]);
                if (Convert.ToBoolean(dr["AccountLocked"]))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Account locked. Please contact administrator')", true);
                    // return "Account locked. Please contact administrator";
                }
                else if (RetryAttempts > 0)
                {
                    int AttemptsLeft = (4 - RetryAttempts);

                    string str = "Invalid user name and/or password. " + AttemptsLeft.ToString() + "attempt(s) left";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('" + str + "')", true);
                }
                else if (Convert.ToBoolean(dr["Authenticated"]))
                {

                    FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, chkRememberme.Checked);

                }




               // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Invalid UserName or Password')", true);


            }

        }

        protected void btnRegister1_Click(object sender, EventArgs e)
        {
            // Read the connection string from web.config.
            // ConfigurationManager class is in System.Configuration namespace
            string CS = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            // SqlConnection is in System.Data.SqlClient namespace
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spRegisterUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter username = new SqlParameter("@UserName", txtRegUser1.Text);
                // FormsAuthentication calss is in System.Web.Security namespace
                string encryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(txtRegpwd1.Text, "SHA1");
                SqlParameter password = new SqlParameter("@Password", txtRegpwd1);
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
                    Response.Redirect("~/Account/Default.aspx");
                }
            }
        }

        
    }
}