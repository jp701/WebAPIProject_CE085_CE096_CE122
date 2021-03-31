<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebClient.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css"/>        <style>
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
                            <h3>Login Page</h3>
                        </div>
                    </div>
                    
                    <asp:Label ID="Label3" runat="server" style="font-size:1vw;" ForeColor="Red" Font-Bold="true"></asp:Label>
                    <br /><br />

                    <div class="input-group">
                        <asp:Label ID="Label1" type="email" runat="server" style="font-size:1.2vw;" Text="Email:"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="TextBox1" class="form-control" runat="server" style="font-size:1.2vw;" TextMode="Email"></asp:TextBox>
                    </div>
                    <br />
                    <div class="input-group">
                        <asp:Label ID="Label2" runat="server" style="font-size:1.2vw;" type="password" Text="Password:"></asp:Label>
                        <asp:TextBox ID="TextBox2" class ="form-control" runat="server" style="font-size:1.2vw;" TextMode="Password"></asp:TextBox>
                    </div>
         
                    <!--<asp:HyperLink ID="HyperLink1" runat="server" style="float:right;" NavigateUrl="ForgotPassword.aspx">Forgot Password??</asp:HyperLink>
                    <br />-->
                    <br />
                    <asp:Button ID="Button1" class="form-control btn btn-success" runat="server" Text="Login" OnClick="Button1_Click" />
                   
                </form>
                 <br />
                 <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="Register.aspx">Click to Register</asp:HyperLink>
            </div>
        </center>
    </div>
    
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

</body>
</html>
