using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using RecipePortalWebAPI.Models;

namespace WebClient
{
    public partial class Register : System.Web.UI.Page
    {
        static HttpClient client= new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            if(Page.IsValid)
            {
                string name = TextBox1.Text;
                string email = TextBox2.Text;
                string password = TextBox3.Text;

                User newuser = new User();
                newuser.name = name;
                newuser.email = email;
                newuser.password = password;

                var serializedUser = JsonConvert.SerializeObject(newuser);
                var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                var msg = await client.PostAsync("https://localhost:44366/api/account/register",content);
                
                if (msg.IsSuccessStatusCode)
                {
                    string response = JsonConvert.DeserializeObject<String>(await msg.Content.ReadAsStringAsync());
                    if (response == "success")
                        Response.Redirect("~/Login.aspx");
                    else
                        Label4.Text = response;
                }
                else
                    Label4.Text = "*" + "Registration Unsuccessful..!!"+msg.StatusCode;
            }
        }
            
    }
}