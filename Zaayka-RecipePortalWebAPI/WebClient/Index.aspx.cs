using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebClient.Models;

namespace WebClient
{
    public partial class Index : System.Web.UI.Page
    {
        static HttpClient client = new HttpClient();
        protected async void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var recipe_resp = await client.GetAsync("https://localhost:44366/api/recipe/getallrecipes");
            if (!recipe_resp.IsSuccessStatusCode)
                Response.Write("<h4>No Recipes available..!!</h4>");

            var data = await recipe_resp.Content.ReadAsStringAsync();
            Recipe[] recipeList = JsonConvert.DeserializeObject<Recipe[]>(data);

            int r = 0, c = 0;
            HtmlGenericControl row = new HtmlGenericControl("div");
            row.ID = "row" + (++r).ToString();
            row.Attributes.Add("runat", "server");
            row.Attributes.Add("class", "row");
            row.Visible = true;
            if (recipeList != null)
            {
                int i;
                for (i = 0; i < recipeList.Length; i++)
                {
                    if (recipeList[i].image != null)
                    {
                        //string[] imageParts = recipeList[i].Image.Split(delim);
                        recipeList[i].image = "/Images/" + recipeList[i].image; //extract only image filename
                    }
                    HtmlGenericControl col = new HtmlGenericControl("div");
                    col.ID = "col" + (++c).ToString();
                    col.Attributes.Add("runat", "server");
                    col.Attributes.Add("class", "col-xl-3 p-1");
                    col.Visible = true;

                    HtmlGenericControl card = new HtmlGenericControl("div");
                    card.ID = (recipeList[i].id).ToString();
                    card.Attributes.Add("runat", "server");
                    card.Attributes.Add("class", "card");
                    card.Attributes.Add("style", "width:300px");
                    card.Visible = true;

                    HtmlGenericControl card_h = new HtmlGenericControl("div");      //Card-header
                    card_h.Attributes.Add("class", "card-header bg-light");
                    card_h.InnerText = (recipeList[i].title).ToString();

                    HtmlGenericControl card_b = new HtmlGenericControl("div");      //Card-body
                    card_b.Attributes.Add("class", "card-body");
                    Image img = new Image();
                    img.ImageUrl = (recipeList[i].image).ToString();
                    img.Attributes.Add("class", "card-img-top imgThumbnail");
                    img.Attributes.Add("alt", "No Image Available");
                    card_b.Controls.Add(img);

                    HtmlGenericControl card_f = new HtmlGenericControl("div");      //Card-footer
                    card_f.Attributes.Add("class", "card-footer text-center");

                    card_h.Visible = true; card_b.Visible = true; card_f.Visible = true;

                    var response = await client.GetAsync("https://localhost:44366/api/account/getuserdetails/" + recipeList[i].UserId);
                    User user = null;
                    if (response.IsSuccessStatusCode)
                    {
                        user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                    }
                    //Users user = acc_client.GetUserDetail(recipeList[i].UserID); //get user details of comment writer
                    card_f.InnerText = "By " + user.name;
                    card.Controls.Add(card_h);
                    card.Controls.Add(card_b);
                    card.Controls.Add(card_f);

                    col.Controls.Add(card);
                    row.Controls.Add(col);
                    if ((i + 1) % 4 == 0)
                    {
                        Panel1.Controls.Add(row);
                        row = new HtmlGenericControl("div");
                        row.ID = "row" + (++r).ToString();
                        row.Attributes.Add("runat", "server");
                        row.Attributes.Add("class", "row");
                        row.Visible = true;
                    }
                }
                if (i % 4 != 0)
                    Panel1.Controls.Add(row);
            }
        }
    }
}