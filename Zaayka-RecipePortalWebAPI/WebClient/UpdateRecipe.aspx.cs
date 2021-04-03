using Newtonsoft.Json;
using RecipePortalWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class UpdateRecipe : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        protected async void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            int index = 0;
            char[] delim = { '\\', '/' };
            if (!Page.IsPostBack)
            {
                string userId = Request.QueryString["userid"];
                if (userId == null)
                {
                    Response.Redirect("Login.aspx");
                }
                back.HRef = "MyRecipes.aspx?userid=" + userId;
                //ServiceReference1.RecipeServiceClient client = new ServiceReference1.RecipeServiceClient("BasicHttpBinding_IRecipeService");
                int rid=Int32.Parse(Request.QueryString["rid"]);
                var response = await client.GetAsync("https://localhost:44366/api/recipe/getrecipe/" + rid);
                if(response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    Recipe fetchrecipe = JsonConvert.DeserializeObject<Recipe>(data);
                    if(fetchrecipe.image!=null)
                    {
                        fetchrecipe.image = "/Images/" + fetchrecipe.image; //extract only image filename
                    }
                    recipeId.Value = fetchrecipe.id.ToString();
                    title.Value = fetchrecipe.title;
                    ingredients.Value = fetchrecipe.ingredients;
                    method.Value = fetchrecipe.method;
                    other.Value = fetchrecipe.otherdetails;
                    photo.Src = fetchrecipe.image;
                    foreach (string name in Enum.GetNames(typeof(Recipe_Category.Category)))
                    {
                        ListItem item = new ListItem(name, (index++).ToString());
                        if (fetchrecipe.category == name)
                        {
                            item.Selected = true;
                        }
                        category.Items.Add(item);
                    }
                }
                else
                {
                    Label1.Text = "<h5>Recipe Not Found..!!</h5>";
                    Label1.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected async void button_Click(object sender, EventArgs e)
        {
            Recipe updaterecipe = new Recipe();
            updaterecipe.id = Int32.Parse(recipeId.Value);
            updaterecipe.title = title.Value;
            updaterecipe.ingredients = ingredients.Value;
            updaterecipe.method = method.Value;
            updaterecipe.category = category.SelectedItem.Text;
            updaterecipe.otherdetails = other.Value;
            updaterecipe.UserId = Int32.Parse(Request.QueryString["userid"]);
            updaterecipe.image = photo.Src.Remove(0, 8);
            var serializedObject = JsonConvert.SerializeObject(updaterecipe);
            var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("https://localhost:44366/api/recipe/updaterecipedetails", content);

            if (response.IsSuccessStatusCode)
            {
                string url = "Home.aspx?";
                url += "userid=" + Server.UrlEncode(updaterecipe.UserId.ToString());
                Response.Redirect(url);
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$(document).ready(function(){$('#myModal').modal('hide');});", true);
                //upmodal.Update();
            }
            else
            {
                Label1.Text = "<h5>Recipe not updated..ERROR!!</h5>"+response.StatusCode;
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}