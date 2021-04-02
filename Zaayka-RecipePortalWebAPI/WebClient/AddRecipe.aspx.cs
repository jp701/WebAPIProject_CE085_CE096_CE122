using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class AddRecipe : System.Web.UI.Page
    {
        static HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            int index = 0;
            string userId = Request.QueryString["userid"];
            if (userId == null)
            {
                Response.Redirect("Login.aspx");
            }

            myrecipes.HRef = "MyRecipes.aspx?userid=" + userId;
            myhome.HRef = "home.aspx?userid=" + userId;
            if (!Page.IsPostBack)
            {
                foreach (string name in Enum.GetNames(typeof(Recipe_Category.Category)))
                {
                    ListItem item = new ListItem(name, (index++).ToString());
                    category.Items.Add(item);
                }
            }
        }

        protected async void button_Click(object sender, EventArgs e)
        {
            string userId = Request.QueryString["userid"];
            /*var response = await client.GetAsync("https://localhost:44366/api/account/getuserdetails/" + Convert.ToInt32(userId));
            User current_user = null;
            if (response.IsSuccessStatusCode)
            {
                current_user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
            }*/

            if (category.SelectedItem.Value != "-1")
            {
                Recipe newrecipe = new Recipe();
                newrecipe.title = title.Value;
                newrecipe.ingredients= ingredients.Value;
                newrecipe.method= method.Value;
                newrecipe.category= category.SelectedItem.Text;
                newrecipe.otherdetails= other.Value;
                newrecipe.image = "no-image.jpg";
                //newrecipe.User = current_user;
                newrecipe.UserId= Convert.ToInt32(userId);
                newrecipe.Date = DateTime.Today;
                if(FileUpload1.HasFile)
                {
                    string fileName = FileUpload1.FileName;
                    string folderPath = Server.MapPath("~\\Images\\");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    folderPath += fileName;
                    FileUpload1.SaveAs(folderPath);
                    newrecipe.image = fileName; //store imagePath to image field
                }

                var serializedUser = JsonConvert.SerializeObject(newrecipe);
                var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
                var msg = await client.PostAsync("https://localhost:44366/api/recipe/addrecipe", content);

                if(msg.IsSuccessStatusCode)
                {
                    Label1.Text = "<h5>Recipe added successfully..</h5>";
                    Label1.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    Label1.Text = "<h5>Recipe not added..ERROR!!</h5>"+msg.StatusCode;
                    Label1.ForeColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "showError",
                    "alert('Please Select Valid Category');", true);
            }
        }

    }
}