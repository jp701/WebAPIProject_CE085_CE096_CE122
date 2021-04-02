using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebClient.Models;

namespace WebClient
{
    public partial class Login : System.Web.UI.Page
    {
        static HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected async void Button1_Click(object sender, EventArgs e)
        {
            User user = new User();
            user.email = TextBox1.Text;
            user.password = TextBox2.Text;

            var userSerialized = JsonConvert.SerializeObject(user);
            var content = new StringContent(userSerialized, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://localhost:44366/api/account/login",content);

           /*var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://localhost:44366/api/account/login"),
                Content = content,
            };

            var response = await client.SendAsync(request).ConfigureAwait(false);*/
            
            if (response.IsSuccessStatusCode) //then retrive User object
            {
                User get_user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                string url = "home.aspx?";
                url += "userid=" + Server.UrlEncode(get_user.id.ToString());
                Response.Redirect(url);
            }
            else
            { 
                Label3.Text = "Invalid Email or Password";
            }
            
        }
    }
}