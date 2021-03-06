using Newtonsoft.Json;
using RecipePortalWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class home : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        protected async void Page_Load(object sender, EventArgs e)
         {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string userId = Request.QueryString["userid"];
            if (userId != null)
            {
                myrecipes.HRef = "MyRecipes.aspx?userid=" + userId;
                //myhome.HRef = "home.aspx?userid=" + userId;
                add.HRef = "AddRecipe.aspx?userid=" + userId;

                var response = await client.GetAsync("https://localhost:44366/api/account/getuserdetails/" + Convert.ToInt32(userId));
                User current_user = null;
                if (response.IsSuccessStatusCode)
                {
                    current_user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                }
                title.InnerText = (current_user == null) ? "Welcome" : "Welcome " + current_user.name;

                var recipe_resp = await client.GetAsync("https://localhost:44366/api/recipe/getallrecipes/all/" + Convert.ToInt32(userId));
                if (!recipe_resp.IsSuccessStatusCode)
                {
                    Label1.Text = "<h4>No Recipes available..!!</h4>";
                    Label1.ForeColor = System.Drawing.Color.Red;
                }

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
                        card_h.Visible = true;

                        HtmlGenericControl card_b = new HtmlGenericControl("div");      //Card-body
                        card_b.Attributes.Add("class", "card-body");
                        System.Web.UI.WebControls.Image img = new System.Web.UI.WebControls.Image();

                        img.ImageUrl = "~/Images/" + (recipeList[i].image);
                        img.Attributes.Add("class", "card-img-top imgThumbnail");
                        img.Attributes.Add("alt", "No Image Available");
                        card_b.Controls.Add(img);
                        card_b.Visible = true;

                        Label likeLabel = new Label();
                        likeLabel.ForeColor = Color.FromArgb(50, 205, 50);
                        likeLabel.Text = "Likes: " + recipeList[i].likes.ToString() + "\t";
                        Label dislikeLabel = new Label();
                        dislikeLabel.Text = "Dislikes: " + recipeList[i].dislikes.ToString();
                        dislikeLabel.ForeColor = Color.FromArgb(255, 0, 56);
                        card_b.Controls.Add(likeLabel);
                        card_b.Controls.Add(dislikeLabel);

                        //< div class="btn-group" role="group" aria-label="Button group example"

                        HtmlGenericControl card_f = new HtmlGenericControl("div");      //Card-footer
                        card_f.Attributes.Add("class", "card-footer text-center");
                        card_f.Visible = true;

                        HtmlGenericControl buttonGroup = new HtmlGenericControl("div");     //Button-Group
                        buttonGroup.Attributes.Add("class", "btn-group");
                        buttonGroup.Attributes.Add("role", "group");

                        Button viewDetails = new Button();                                          //viewDetails button
                        viewDetails.Attributes.Add("rid", (recipeList[i].id).ToString());
                        viewDetails.Text = "View Details";
                        viewDetails.Attributes.Add("class", "btn btn-outline-primary btn-sm");
                        string rid = ((recipeList[i].id).ToString());
                        //viewDetails.Click += (s2, e2) => getRecipe(s2, e2, s );
                        //viewDetails.Click += delegate (object s1, EventArgs e1) { getRecipe(s1, e1, s ); };
                        viewDetails.Click += new EventHandler((s1, e1) => getRecipe(sender, e, rid, Request.QueryString["userid"]));
                        viewDetails.Visible = true;

                        Button like = new Button();                                          //likes button
                        like.Attributes.Add("rid", (recipeList[i].id).ToString());
                        like.Attributes.Add("class", "btn btn-outline-success btn-sm");
                        //HtmlGenericControl itag = new HtmlGenericControl("i");
                        //itag.Attributes.Add("class","fas fa-thumbs-up");
                        //like.Controls.Add(itag);
                        //itag.InnerHtml = "iLike ";
                        //like.Attributes.Add("class", "btn btn-primary align-self-center");
                        //viewDetails.Click += (s2, e2) => getRecipe(s2, e2, s );
                        //viewDetails.Click += delegate (object s1, EventArgs e1) { getRecipe(s1, e1, s ); };
                        like.Click += new EventHandler((s1, e1) => addLike(sender, e, rid));
                        like.Text = "Like";
                        like.Visible = true;

                        Button dislike = new Button();                                          //dislikes button
                        dislike.Attributes.Add("rid", (recipeList[i].id).ToString());
                        dislike.Attributes.Add("class", "btn btn-outline-danger btn-sm");

                        //*HtmlGenericControl itag2 = new HtmlGenericControl("i");
                        //itag2.Attributes.Add("class", "fas fa-thumbs-up");
                        //itag2.InnerHtml = "iDislike ";
                        //dislike.Controls.Add(itag);

                        //like.Attributes.Add("class", "btn btn-primary align-self-center");
                        //viewDetails.Click += (s2, e2) => getRecipe(s2, e2, s );
                        //viewDetails.Click += delegate (object s1, EventArgs e1) { getRecipe(s1, e1, s ); };
                        dislike.Click += new EventHandler((s1, e1) => addDislike(sender, e, rid));
                        dislike.Text = "Dislike";
                        dislike.Visible = true;


                        buttonGroup.Controls.Add(viewDetails);
                        buttonGroup.Controls.Add(like);
                        buttonGroup.Controls.Add(dislike);
                        card_f.Controls.Add(buttonGroup);

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
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected void getRecipe(object sender, EventArgs e, string ID, string userid)
        {
            Response.Redirect("GetRecipe.aspx?rid=" + ID + "&b=h&userid=" + userid, false);
            return;
        }
        protected async void addLike(object sender, EventArgs e, string ID)
        {
            if (Request.QueryString["userid"] != null)
            {
                var data = JsonConvert.SerializeObject(ID);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44366/api/recipe/addlike", content);
                Response.Redirect("home.aspx?userid=" + Request.QueryString["userid"]);
            }
        }
        protected async void addDislike(object sender, EventArgs e, string ID)
        {
            if (Request.QueryString["userid"] != null)
            {
                var data = JsonConvert.SerializeObject(ID);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:44366/api/recipe/adddislike", content);
                Response.Redirect("home.aspx?userid=" + Request.QueryString["userid"]);
            }
        }

        protected async void ViewProfile_Click(object sender, EventArgs e)
        {
                if (Request.QueryString["userid"] != null)
                {
                    var id = Int32.Parse(Request.QueryString["userid"]);
                    var response = await client.GetAsync("https://localhost:44366/api/account/getuserdetails/" + id);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        User user = JsonConvert.DeserializeObject<User>(data);
                        uname.Text = user.name;
                        emailid.Text = user.email;
                        password.Text = user.password;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$(document).ready(function(){$('#myModal').modal();});", true);
                        upmodal.Update();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }

        protected async void Update(object sender, EventArgs e)
        {
            if (Request.QueryString["userid"] != null)
            {
                User update_user = new User();
                update_user.id = Int32.Parse(Request.QueryString["userid"]);
                update_user.name = uname.Text;
                update_user.email = emailid.Text;
                update_user.password = password.Text;

                var serializedObject = JsonConvert.SerializeObject(update_user);
                var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                var response = await client.PutAsync("https://localhost:44366/api/account/updateuserdetails", content);

                if (response.IsSuccessStatusCode)
                {
                    string url = "Home.aspx?";
                    url += "userid=" + Server.UrlEncode(update_user.id.ToString());
                    Response.Redirect(url);
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$(document).ready(function(){$('#myModal').modal('hide');});", true);
                    //upmodal.Update();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }
    }
}