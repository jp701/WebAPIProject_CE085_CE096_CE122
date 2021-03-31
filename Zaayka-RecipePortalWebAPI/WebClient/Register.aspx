<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WebClient.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register User</title>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous"/>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>    
    
    <style>
        body{
            background-color:rgb(240,240,240);
            background-position:center;
            background-repeat:no-repeat;
            background-size:cover;
            background-attachment:fixed;
        }
        .centered{
            top:20%;
        }
    </style>
</head>
<body> 
    <div class="container">
        <center>
        <div class="bg-white centerd p-1 w-25 rounded shadow-lg mt-5">
        <form id="form1" runat="server">
            
            <div class="row p-1">
                <div class="col col-lg-12">
                    <h3>Registration Page</h3>
                </div>
            </div>
            <asp:Label ID="Label4" style="font-size:1vw;" runat="server" Text="" ForeColor="Red" Font-Bold="true"></asp:Label>
            <br /><br />
            <div class="input-group">
                <asp:Label ID="Label1" style="font-size:1.2vw;" runat="server" Text="Name:"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox1" class="form-control" style="font-size:1.2vw;" runat="server" required></asp:TextBox>
            </div>
                       
            <br />
            <div class="input-group">
                <asp:Label ID="Label2" style="font-size:1.2vw;" runat="server" Text="Email:"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="TextBox2"  class="form-control" style="font-size:1.2vw;" runat="server" required></asp:TextBox>
            </div>
            <br />
            <div class="input-group">
                <asp:Label ID="Label3" style="font-size:1.2vw;" runat="server" Text="Password:" required></asp:Label>
                <asp:TextBox ID="TextBox3" type="password"  class="form-control" style="font-size:1.2vw;"  runat="server"></asp:TextBox>
                   
            </div>
            
            <br />
            <asp:Button ID="Button1" class="form-control btn btn-success" runat="server" Text="SignUp" OnClick="Button1_Click" />
        </form>
        <br />Already Registered?
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="Login.aspx">SignIn here</asp:HyperLink>
        <!--<a class="btn btn-primary" href="Login.aspx">Sign In</a>-->
        </div>
        </center>
    </div> 

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</body>
</html>
