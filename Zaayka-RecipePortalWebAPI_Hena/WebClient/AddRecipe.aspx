<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="AddRecipe.aspx.cs" Inherits="WebClient.AddRecipe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add New Recipe</title>
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
    <header>
        <h2 class="bg-secondary p-2 text-light">
                <img id="sitelogo" style="height:40px;width:40px" src="/Images/sitelogo.jpg" alt="No image">
                Zaika
                <a href="" class="btn btn-light" runat="server" id="myhome"><i class="fas fa-home"></i> Home</a>
                <a href="" class="btn btn-light" runat="server" id="myrecipes"><i class="fas fa-box-open"></i> My Recipes</a>
                <a href="" class="btn btn-light" runat="server" id="add"><i class="fas fa-pizza-slice"></i> Add Recipes</a>
                <a href="" class="btn btn-light" runat="server"  id="ViewProfile" onserverclick="ViewProfile_Click"><i class="fas fa-user-alt"></i> Profile</a>
                <a href="Login.aspx" class="btn btn-light" runat="server" id="logout"><i class="fas fa-sign-out-alt"></i> Logout</a>
        </h2>
    </header>

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div class="container w-75 mt-3">
            <h3 style="color:blue;">Add Recipe Details</h3>
            <br />
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            <div class="form-group row">
                <label for="title" class="col-sm-2 col-form-label">Title:</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control" placeholder="Enter recipe name" id="title" runat="server" required="required"/>
                </div>
            </div>
            <div class="form-group row">
                <label for="ingredients" class="col-sm-2 col-form-label">Ingredients:</label>
                <div class="col-sm-10">
                    <textarea rows="3" cols="40" class="form-control" placeholder="Enter ingredients" id="ingredients" runat="server" required=""></textarea>
                </div>
            </div>
            <div class="form-group row">
                <label for="method" class="col-sm-2 col-form-label">Method:</label>
                <div class="col-sm-10">
                    <textarea rows="6" cols="40" class="form-control" placeholder="Enter recipe procedure" id="method" runat="server" required=""></textarea>
                </div>
            </div>
            <!--SouthIndian, Gujarati, Punjabi, Chinese, Diet, Snacks/Breakfast, Lunch, Sweet, Dessert, Other-->
            <div class="form-group row">
                <label for="category" class="col-sm-2 col-form-label">Category:</label>
                <div class="col-sm-10">
                    <asp:DropDownList ID="category" runat="server" class="custom-select" required="">
                        <asp:ListItem Value="-1">Please Select</asp:ListItem>
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
                <label for="FileUpload1" class="col-sm-2 col-form-label">Photo:</label>
                <div class="col-sm-10">
                    <div class="custom-file">
                        <asp:FileUpload ID="FileUpload1" class="form-control custom-file-input" runat="server" />
                        <label class="custom-file-label">Choose Photo..</label>
                    </div>
                </div>
            </div>
            <br />
            <div class="form-group row">
                <div class="col-sm-10">
                    <asp:Button type="submit" class="btn btn-primary" runat="server" text="ADD Recipe" ID="button" OnClick="button_Click"></asp:Button>
                </div>
            </div>
            
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

        <!-- script to reflect filename in photo label-->
        <script>
            $(document).ready(function () {
                $('.custom-file-input').on('change', function () {
                    var filename = $(this).val().split("\\").pop();
                    $(this).next('.custom-file-label').html(filename);
                });
            });
        </script>
    </form>
</body>
</html>
