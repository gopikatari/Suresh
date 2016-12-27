using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using SureshSite.Account;

namespace SureshSite.DAL
{
    public class Users
    {
        
        public SqlDataReader AuthenticateUser(string username, string password)
        {
            // ConfigurationManager class is in System.Configuration namespace
            string CS = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            // SqlConnection is in System.Data.SqlClient namespace
            SqlConnection con = new SqlConnection(CS);

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
            return rdr;
        }

        

    }

}