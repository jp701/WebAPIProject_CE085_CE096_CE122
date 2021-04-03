<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="WebClient.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
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

    <script type="text/javascript">
        $(document).ready(function () {
            $('#show_password').hover(function show() {
                //Change the attribute to text  
                $('#password').attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
            function () {
                //Change the attribute back to password  
                $('#password').attr('type', 'password');
                $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
            });
        });
    </script>
    <!-- For ICONS -->
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.7.0/css/all.css" integrity="sha384-lZN37f5QGtY3VHgisS14W3ExzMWZxybE1SJSEsQp9S+oqd12jhcu+A56Ebc1zFSJ" crossorigin="anonymous"/>
</head>
<body>
    
    <form id="form1" runat="server">
    <header>
        <h2 class="bg-secondary p-2 text-light">
                <img id="sitelogo" style="height:40px;width:40px" src="/Images/sitelogo.jpg" alt="No image">
                Zaika
                <a href="#" class="btn btn-light" runat="server" id="myhome"><i class="fas fa-home"></i> Home</a>
                <a href="" class="btn btn-light" runat="server" id="myrecipes"><i class="fas fa-box-open"></i> My Recipes</a>
                <a href="" class="btn btn-light" runat="server" id="add"><i class="fas fa-pizza-slice"></i> Add Recipes</a>
                <a href="" class="btn btn-light" runat="server"  id="ViewProfile" onserverclick="ViewProfile_Click"><i class="fas fa-user-alt"></i> Profile</a>
                <a href="Login.aspx" class="btn btn-light" runat="server" id="logout"><i class="fas fa-sign-out-alt"></i> Logout</a>
        </h2>
    </header>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
        <div class="container p-1">
            <h3 style="color:blue;" runat="server" id="title"></h3>
            <br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <asp:Panel ID="Panel1" runat="server">
            </asp:Panel>
        </div>
         <!--Profile modal -->
        <div class="modal fade" id="myModal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <asp:UpdatePanel ID="upmodal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title"><asp:Label ID="UserProfile" runat="server" Text="Profile"></asp:Label></h4>
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    
                            </div>
                            <div class="modal-body">
                                <div class="row p-1">
                                    <div class="col col-lg-2">
                                        <asp:Label ID="email" class="col-form-label" runat="server" Text="Email:"></asp:Label>
                                    </div>
                                    <div class="col col-lg-10">
                                        <asp:TextBox ID="emailid" class="form-control" runat="server" type="email" readonly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row p-1">
                                    <div class="col col-lg-2">
                                        <asp:Label ID="name" class="col-form-label" runat="server" Text="Name:"></asp:Label>
                                    </div>
                                    <div class="col col-lg-10">
                                        <asp:TextBox ID="uname" class="form-control" runat="server" type="Text" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="row p-1">
                                    <div class="col col-lg-2">
                                        <asp:Label ID="pwd" class="col-form-label" runat="server" Text="Password:"></asp:Label>
                                    </div>
                                    <div class="col col-lg-10">
                                        <div class="input-group">
                                            <asp:TextBox ID="password" type="password" runat="server" class="form-control"></asp:TextBox>  
                                            <div class="input-group-append">  
                                                <button id="show_password" class="btn btn-primary" type="button">
                                                    <span class="fa fa-eye-slash icon"></span>  
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="update" class="btn btn-success" runat="server" Text="Update" OnClick="Update" />
                                <button class="btn btn-danger" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>    
           </div>
    </form>
</body>
</html>