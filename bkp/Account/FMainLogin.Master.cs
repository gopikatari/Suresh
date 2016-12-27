using System;
using System.Web.Security;
using System.Data.SqlClient;
using SureshSite.DAL;
namespace SureshSite
{
    public partial class FMain : System.Web.UI.MasterPage
    {
        Users u = new Users();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            

        }

    }
}