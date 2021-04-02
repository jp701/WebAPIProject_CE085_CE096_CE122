using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebClient.Models;

namespace WebClient
{
    public partial class MyRecipes : System.Web.UI.Page
    {
        static HttpClient client = new HttpClient();
        protected async void Page_Load(object sender, EventArgs e)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string userId = Request.QueryString["userid"];
            if (userId != null)
            {
                //myrecipes.HRef = "MyRecipes.aspx?userid=" + Request.QueryString["userid"];
                myhome.HRef = "home.aspx?userid=" + userId;
                add.HRef = "AddRecipe.aspx?userid=" + Request.QueryString["userid"];

              
                var recipe_resp = await client.GetAsync("https://localhost:44366/api/recipe/getallrecipes/my/" + Convert.ToInt32(userId));
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
                        img.ImageUrl = "~/Images/" + (recipeList[i].image).ToString();
                        img.Attributes.Add("class", "card-img-top imgThumbnail");
                        img.Attributes.Add("alt", "No Image Available");
                        card_b.Controls.Add(img);
                        card_b.Visible = true;

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
                        try
                        {
                            viewDetails.Click += new EventHandler((s1, e1) => getRecipe(sender, e, rid, Request.QueryString["userid"]));
                            viewDetails.Visible = true;
                        }
                        catch(ThreadAbortException ex)
                        {
                            throw ex;
                        }
                        Button updaterecipe = new Button();                                          //likes button
                        updaterecipe.Attributes.Add("rid", (recipeList[i].id).ToString());
                        updaterecipe.Attributes.Add("class", "btn btn-outline-success btn-sm");
                        //viewDetails.Click += (s2, e2) => getRecipe(s2, e2, s );
                        //viewDetails.Click += delegate (object s1, EventArgs e1) { getRecipe(s1, e1, s ); };
                        updaterecipe.Click += new EventHandler((s1, e1) => updateRecipe(sender, e, rid, Request.QueryString["userid"]));
                        updaterecipe.Text = "Update";
                        updaterecipe.Visible = true;

                        Button deleterecipe = new Button();                                          //dislikes button
                        deleterecipe.Attributes.Add("rid", (recipeList[i].id).ToString());
                        deleterecipe.Attributes.Add("class", "btn btn-outline-danger btn-sm");
                        deleterecipe.Click += new EventHandler((s1, e1) => deleteRecipe(sender, e,rid));
                        /*deleterecipe.Attributes.Add("data-toggle", "modal");
                        deleterecipe.Attributes.Add("data-target", "#deletemodal");*/
                        deleterecipe.Text = "Delete";
                        deleterecipe.Visible = true;
                        //deleteModal.Attributes.Add("rid", (recipeList[i].Id).ToString());

                        /*HtmlGenericControl modal = new HtmlGenericControl("div");           //Delete Modal creation
                        HtmlGenericControl dialog = new HtmlGenericControl("div");
                        HtmlGenericControl content = new HtmlGenericControl("div");
                        HtmlGenericControl modal_header = new HtmlGenericControl("div");
                        HtmlGenericControl modal_body = new HtmlGenericControl("div");
                        HtmlGenericControl modal_footer = new HtmlGenericControl("div");
                        modal.Attributes.Add("class", "modal fade");
                        //modal.Attributes.Add("role", "dialog");
                        modal.Attributes.Add("id", "deletemodal");
                        dialog.Attributes.Add("class", "modal-dialog");
                        content.Attributes.Add("class", "modal-content");
                        modal.Visible = true;
                        dialog.Visible = true;
                        content.Visible = true;
                        modal_header.Visible = true;
                        modal_body.Visible = true;
                        modal_footer.Visible = true;

                        modal_header.Attributes.Add("class", "modal-header");
                        HtmlGenericControl modal_title = new HtmlGenericControl("h4");      //Modal-title
                        modal_title.Attributes.Add("class", "modal-title");
                        modal_title.InnerText = "Confirmation of Deletion of Recipe";
                        Button close = new Button();                                        //Close button
                        close.Attributes.Add("class", "close");
                        close.Attributes.Add("data-dismiss", "modal");
                        close.Text = "&times;";
                        close.Visible = true;
                        modal_header.Controls.Add(modal_title);
                        modal_header.Controls.Add(close);

                        modal_body.Attributes.Add("class", "modal-body");
                        modal_body.InnerHtml = "<h5>Are you sure want to delete this recipe?</h5>";

                        Button yes = new Button();                                          //Yes button
                        yes.Attributes.Add("rid", (recipeList[i].Id).ToString());
                        yes.Attributes.Add("class", "btn btn-outline-danger btn-sm");
                        //viewDetails.Click += (s2, e2) => getRecipe(s2, e2, s );
                        //viewDetails.Click += delegate (object s1, EventArgs e1) { getRecipe(s1, e1, s ); };
                        yes.Click += new EventHandler((s1, e1) => deleteRecipeConfirm(sender, e, (recipeList[i].Id).ToString()));
                        yes.Text = "Yes";
                        yes.Visible = true;
                        Button no = new Button();                                          //No button
                        no.Attributes.Add("data-dismiss", "modal");
                        no.Attributes.Add("class", "btn btn-outline-secondary btn-sm");
                        no.Text = "No";
                        no.Visible = true;

                        modal_footer.Controls.Add(yes);
                        modal_footer.Controls.Add(no);

                        content.Controls.Add(modal_header);
                        content.Controls.Add(modal_body);
                        content.Controls.Add(modal_footer);
                        dialog.Controls.Add(content);
                        modal.Controls.Add(dialog);
                        Panel2.Controls.Add(modal);*/

                        //buttonGroup.Controls.Add(modal);
                        buttonGroup.Controls.Add(viewDetails);
                        buttonGroup.Controls.Add(updaterecipe);
                        buttonGroup.Controls.Add(deleterecipe);
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
        protected void updateRecipe(object sender, EventArgs e, string ID,string userid)
        {
            Response.Redirect("UpdateRecipe.aspx?rid=" + ID + "&b=m&userid=" + userid);
        }
        protected void deleteRecipe(object sender, EventArgs e, string ID)
        {
            /*RecipeServiceClient proxy = new RecipeServiceClient("BasicHttpBinding_IRecipeService");
            Recipe deletingRecipe = proxy.GetRecipe(Convert.ToInt32(ID)) ;
            if(deletingRecipe != null)
            {
                modal_body.InnerHtml = "Are you sure want to delete " + deletingRecipe.Title+" ?";
                modal_title.InnerHtml = "Confirmation of Deletion of Recipe";
                Button delete = (Button)FindControl("modal_yes");
                delete.Attributes.Add("rid",ID);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "deleteModal", "$(document).ready(function(){$('#deleteModal').modal();});", true);
                upmodal.Update();
            }*/
        }

        protected void getRecipe(object sender, EventArgs e, string ID,string userid)
        {
            Response.Redirect("GetRecipe.aspx?rid=" + ID+"&b=m&userid="+userid,false);
            return;
        }

        protected void deleteRecipeConfirm(object sender, EventArgs e)
        {
            /*if (Request.QueryString["userid"] != null)
            {
                string ID = "";
                Button delete = (Button)FindControl("modal_yes");
                foreach (string key in delete.Attributes.Keys)
                {
                    if (key.Equals("rid"))
                    {
                        ID = delete.Attributes["rid"];
                        break;
                    }
                }
                RecipeServiceClient proxy = new RecipeServiceClient("BasicHttpBinding_IRecipeService");
                bool isdeleted = proxy.DeleteRecipe(Convert.ToInt32(ID));
                proxy.Close();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "deleteModal", "$(document).ready(function(){$('#deleteModal').modal('hide');});", true);
                upmodal.Update();
                Response.Redirect("MyRecipes.aspx?userid=" + Request.QueryString["userid"]);
            }*/
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
                /*else
                {
                    Response.Redirect("Login.aspx");
                }*/
    }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}