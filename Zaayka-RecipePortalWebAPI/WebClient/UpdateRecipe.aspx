<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="UpdateRecipe.aspx.cs" Inherits="WebClient.UpdateRecipe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Recipe Details</title>
    <style>
        .imgThumbnail{
            width: 400px;
            height: 250px;
        }
    </style>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</head>
<body>
    <header>
        <h2 class="bg-secondary p-2 text-light">
            <img id="sitelogo" style="height:40px;width:40px" src="/Images/sitelogo.jpg"/>
            Zaika
        </h2>
    </header>
    <form id="form1" runat="server">
        <div class="container w-75 mt-3">
            <h3 style="color:blue;">Update Recipe Details</h3>
            <br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <div class="form-group row">
                <label for="recipeId" class="col-sm-2 col-form-label">Recipe Id:</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control" placeholder="" id="recipeId" runat="server" readonly="true"/>
                </div>
            </div>
            <div class="form-group row">
                <label for="title" class="col-sm-2 col-form-label">Title:</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control" placeholder="Enter recipe name" id="title" runat="server"/>
                </div>
            </div>
            <div class="form-group row">
                <label for="ingredients" class="col-sm-2 col-form-label">Ingredients:</label>
                <div class="col-sm-10">
                    <textarea rows="3" cols="40" class="form-control" placeholder="Enter ingredients" id="ingredients" runat="server"></textarea>
                </div>
            </div>
            <div class="form-group row">
                <label for="method" class="col-sm-2 col-form-label">Method:</label>
                <div class="col-sm-10">
                    <textarea rows="6" cols="40" class="form-control" placeholder="Enter recipe procedure" id="method" runat="server"></textarea>
                </div>
            </div>
            <!--SouthIndian, Gujarati, Punjabi, Chinese, Italian, Diet, Breakfast, Sweet, Other-->
            <div class="form-group row">
                <label for="category" class="col-sm-2 col-form-label">Category:</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="category" runat="server" class="custom-select">
                        <asp:ListItem Value="">Please Select Category</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="form-group row">
                <label for="other" class="col-sm-2 col-form-label">Other Details:</label>
                <div class="col-sm-10">
                    <textarea rows="2" cols="40" class="form-control" id="other" placeholder="" runat="server"></textarea>
                </div>
            </div>
            <div class="form-group row">
                <label for="photo" class="col-sm-2 col-form-label">Photo:</label>
                <div class="col-sm-10">
                    <img id="photo" class="imgThumbnail" src="" alt="No Image Available" runat="server"/>
                </div>
            </div>
            <br />
            <div class="form-group row">
                <div class="col-sm-10">
                    <asp:Button type="submit" class="btn btn-primary" runat="server" text="Update" ID="button" OnClick="button_Click"></asp:Button>
                    | <a href="" id="back" class="btn btn-primary" runat="server">Back To List</a>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
