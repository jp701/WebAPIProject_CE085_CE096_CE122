using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            /*string email=TextBox1.Text;
            string password = TextBox2.Text;
            ServiceReference2.AccountServiceClient proxy = new ServiceReference2.AccountServiceClient("BasicHttpBinding_IAccountService");
            ServiceReference2.Users loguser=proxy.Login(email,password);
            if (loguser == null)
                Label3.Text = "Invalid Email or password, please enter your registerd email and password only..";
            else
            {
                string url = "home.aspx?";
                url += "userid=" + Server.UrlEncode(loguser.ID.ToString());
                Response.Redirect(url);
            }*/
        }
    }
}