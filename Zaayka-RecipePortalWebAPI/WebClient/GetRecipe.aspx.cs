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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace WebClient
{
    public partial class GetRecipe : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        protected async void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Panel1.Visible = false;
            if (Request.QueryString["userid"] != null)
            {
                if (Request.QueryString["b"] != null)
                {
                    if (Request.QueryString["b"] == "h")
                        back.HRef = "home.aspx?userid=" + Request.QueryString["userid"];
                    if (Request.QueryString["b"] == "m")
                        back.HRef = "MyRecipes.aspx?userid=" + Request.QueryString["userid"];
                }
                //RecipeServiceClient client = new RecipeServiceClient("BasicHttpBinding_IRecipeService");
                TableCell c; TableRow r;
                string madeby = "";
                try
                {
                    var recipeId = Convert.ToInt32(Request.QueryString["rid"]);
                    var recipe_resp = await client.GetAsync("https://localhost:44366/api/recipe/getrecipe/" + recipeId);
                    if (!recipe_resp.IsSuccessStatusCode)
                        Response.Write("<h4>No Recipes available..!!</h4>");

                    var data = await recipe_resp.Content.ReadAsStringAsync();
                    Recipe recipe = JsonConvert.DeserializeObject<Recipe>(data);

                    card_h.InnerText = recipe.title;

                    var response = await client.GetAsync("https://localhost:44366/api/account/getuserdetails/" + recipe.UserId);
                    User user = null;
                    if (response.IsSuccessStatusCode)
                    {
                        user = JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
                    }
                    madeby = user.name;

                    Image img = new Image();
                    img.Attributes.Add("class", "imgThumbnail");
                    img.ImageUrl = "/Images/" + recipe.image;
                    card_b.Controls.Add(img);
                    Table t = new Table();
                    t.Attributes.Add("class", "table table-striped table-hover");

                    r = new TableRow();
                    c = new TableCell(); c.Text = "Made by";
                    r.Cells.Add(c);
                    c = new TableCell(); c.Text = madeby;
                    r.Cells.Add(c);
                    t.Rows.Add(r);

                    r = new TableRow();
                    c = new TableCell(); c.Text = "Ingredients";
                    r.Cells.Add(c);
                    c = new TableCell(); c.Text = recipe.ingredients;
                    r.Cells.Add(c);
                    t.Rows.Add(r);

                    r = new TableRow();
                    c = new TableCell(); c.Text = "Method";
                    r.Cells.Add(c);
                    c = new TableCell(); c.Text = recipe.method;
                    r.Cells.Add(c);
                    t.Rows.Add(r);

                    r = new TableRow();
                    c = new TableCell(); c.Text = "Category";
                    r.Cells.Add(c);
                    c = new TableCell(); c.Text = recipe.category;
                    r.Cells.Add(c);
                    t.Rows.Add(r);

                    r = new TableRow();
                    c = new TableCell(); c.Text = "Other details";
                    r.Cells.Add(c);
                    c = new TableCell(); c.Text = recipe.otherdetails;
                    r.Cells.Add(c);
                    t.Rows.Add(r);

                    card_b.Controls.Add(t);
                }
                catch (Exception ex)
                {
                    Response.Write("<b>An unhandled exception occured: " + ex.Message + "</b>");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        protected async void button2_Click(object sender, EventArgs e) // to Add Comment
        {
            Panel1.Visible = false;
            Comment comment = new Comment();
            string userId = Request.QueryString["userid"].ToString();
            string recipeId = Request.QueryString["rid"].ToString();
            string back = Request.QueryString["b"].ToString();
            comment.RecipeId = Convert.ToInt32(recipeId);
            comment.UserId = Convert.ToInt32(userId);
            comment.commentText = TextArea1.Text;
            comment.datetime = DateTime.Now;

            var serializedUser = JsonConvert.SerializeObject(comment);
            var content = new StringContent(serializedUser, Encoding.UTF8, "application/json");
            var msg = await client.PostAsync("https://localhost:44366/api/recipe/addcomment", content);

            if (msg.IsSuccessStatusCode)
            {
                Response.Redirect("GetRecipe.aspx?rid=" + recipeId + "&b=" + back + "&userid=" + userId);
            }
            else
            {
                Label1.Text = "<h5>Comment Not Added..ERROR!!</h5>";
                Label1.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected async void button3_Click(object sender, EventArgs e) //to Display Comments
        {
            //Console.WriteLine("Inside Button3_click");
            string userId = Request.QueryString["userid"].ToString();
            string recipeId = Request.QueryString["rid"].ToString();
            string back = Request.QueryString["b"].ToString();

            if (button3.Text == "Show Comments")
            {
                Panel1.Visible = true;
                button3.Text = "Hide Comments";
                //CommentServiceClient client = new CommentServiceClient("BasicHttpBinding_ICommentService");
                // AccountServiceClient acc_client = new AccountServiceClient("BasicHttpBinding_IAccountService");
                try
                {
                    var recipeId_int = Convert.ToInt32(recipeId);
                    var response = await client.GetAsync("https://localhost:44366/api/recipe/viewcomment/" + recipeId_int);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        IEnumerable<Comment> comments = JsonConvert.DeserializeObject<IEnumerable<Comment>>(data);
                        foreach (Comment comment in comments)
                        {
                            //Console.WriteLine("Comment Found: (Id:{0})", comment.Id);
                            //get user details of comment writer

                            var user_response = await client.GetAsync("https://localhost:44366/api/account/getuserdetails/" + comment.UserId);
                            if (user_response.IsSuccessStatusCode)
                            {
                                string user_data = await user_response.Content.ReadAsStringAsync();
                                User user = JsonConvert.DeserializeObject<User>(user_data);

                                HtmlGenericControl media = new HtmlGenericControl("div");
                                media.ID = comment.id.ToString();
                                media.Attributes.Add("runat", "server");
                                media.Attributes.Add("class", "media");
                                media.Visible = true;

                                HtmlGenericControl mediaBody = new HtmlGenericControl("div");
                                mediaBody.ID = "comment" + comment.id.ToString();
                                mediaBody.Attributes.Add("runat", "server");
                                mediaBody.Attributes.Add("class", "media-body");

                                HtmlGenericControl h4 = new HtmlGenericControl("h5");
                                h4.Attributes.Add("class", "media-heading");
                                h4.InnerText = user.name + "\t";
                                h4.Visible = true;

                                HtmlGenericControl smallText = new HtmlGenericControl("small");
                                smallText.Visible = true;
                                h4.Controls.Add(smallText);

                                HtmlGenericControl i_datetime = new HtmlGenericControl("i");
                                i_datetime.InnerText = comment.datetime.ToString();
                                i_datetime.Visible = true;
                                smallText.Controls.Add(i_datetime);

                                HtmlGenericControl para = new HtmlGenericControl("p");
                                para.InnerText = comment.commentText;
                                para.Visible = true;

                                mediaBody.Controls.Add(h4);
                                mediaBody.Controls.Add(para);
                                mediaBody.Visible = true;

                                HtmlGenericControl hr = new HtmlGenericControl("hr");
                                hr.Visible = true;
                                Panel1.Controls.Add(hr);

                                media.Controls.Add(mediaBody);
                                Panel1.Controls.Add(media);

                            }
                        }
                    }
                    else
                    {
                        Label2.Text = "<h5>No Comments Available..!!</h5>";
                        Label2.ForeColor = System.Drawing.Color.Red;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("<b>An unhandled exception occured: " + ex.Message + "</b>");
                }
            }
            else if (button3.Text == "Hide Comments")
            {
                Panel1.Visible = false;
                button3.Text = "Show Comments";
            }
        }
    }
}