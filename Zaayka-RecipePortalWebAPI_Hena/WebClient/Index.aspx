<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="WebClient.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title></title>
    <style>
        .imgThumbnail
        {
            height:200px;
            width:auto;
        }
        @media only screen and (max-width:750px){
            .card{
                /*max-width: 100%;*/
                display:block;
                clear:both;
            }        
        }
    </style>
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <!-- For ICONS -->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">
</head>
<body>
    <header>
        <h2 class="bg-secondary p-2 text-light">
                <img id="sitelogo" style="height:40px;width:40px" src="/Images/sitelogo.jpg" alt="No image">
                Zaiyka
                <a href="Login.aspx" class="btn btn-light"><i class="fas fa-sign-in-alt"></i> Login</a>
                <a href="Register.aspx" class="btn btn-light"><i class="fas fa-registered"></i> Register</a>
        </h2>
    </header>
    <form id="form1" runat="server">
        <div class="container p-1">
            <h2>All Recipes</h2>
            <asp:Panel ID="Panel1" runat="server" Visible="true" >

            </asp:Panel>
        </div>
    </form>
</body>
</html>
<!--
    For ICONS
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">
    <i class="fas fa-user-alt"></i>---User
    <i class="fas fa-cog"></i> -------settings
    <i class="fas fa-search"></i> -----search
    <i class="fas fa-thumbs-up"></i>----like
    <i class="fas fa-thumbs-down"></i> ---dislike
    <i class="fas fa-comment"></i> ---comment
    <i class="fas fa-heart"></i> --- add to favourite
    <i class="fas fa-registered"></i> ---register
    <i class="fas fa-sign-out-alt"></i> ---logout
    <i class="fas fa-sign-in-alt"></i> ---login
    <i class="fas fa-shopping-cart"></i> ---Cart
    <i class="fas fa-box-open"></i> ---open box
    -->