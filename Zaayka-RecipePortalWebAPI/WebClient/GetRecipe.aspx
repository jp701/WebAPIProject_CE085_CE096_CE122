<%@ Page Async="true" Language="C#" AutoEventWireup="true" CodeBehind="GetRecipe.aspx.cs" Inherits="WebClient.GetRecipe" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>All Recipes</title>
    <style>
        .imgThumbnail
        {
            height:250px;
            width:400px;
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
</head>
<body>
    <header>
        <h2 class="bg-secondary p-2 text-light">
            <img id="sitelogo" style="height:40px;width:40px" src="/Images/sitelogo.jpg"/>
            Zaika
        </h2>
    </header>
    <form id="form1" runat="server">
        <div class="container p-3">
            <div class="row">
                <div class="col-xl-12">
                    <div class="card">
                        <div class="card-header" id="card_h" runat="server">
                        </div>
                        <div class="card-body" id="card_b" runat="server">
                        </div>
                        <div class="card-footer bg-light" id="card_f" runat="server">
                            <center>
                                <a href="#" class="btn btn-outline-primary text-decoration-none btn-sm" id="back" value="Button" runat="server" >Back</a>
                                <asp:Button ID="button3" class="btn btn-outline-primary text-decoration-none btn-sm" Text="Show Comments" OnClick="button3_Click" runat="server" UseSubmitBehavior="false"/>
                                <a href="#" id="button1" class="btn btn-outline-primary text-decoration-none btn-sm" value="Button" runat="server" >Add Comment</a>
                            </center>
                            <!-- Modal -->
                            <div class="modal fade" id="myModal" role="dialog">
                                <div class="modal-dialog">
    
                            <!-- Modal content-->
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h4 class="modal-title">Add Comment</h4>
                                            <button type="button" class="close" data-dismiss="modal">&times;</button>
                                        </div>
                                        <div class="modal-body">
                                                <asp:Textbox id="TextArea1" class="form-control" placeholder="Enter Comment" runat="server" required="required" TextMode="MultiLine" Rows="4"></asp:Textbox>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="button2" class="btn btn-success" Text="ADD" OnClick="button2_Click" runat="server"/>
                                            <button type="button" class="btn btn-danger" data-dismiss="modal">Cancel</button>
                                        </div>
                                    </div>
      
                                   </div>
                            </div>
                            <!--End of Modal-->
                        </div>
                    </div>
                </div>
            </div>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>

        <br/>
        <div class="container p-1">
            <asp:Panel ID="Panel1" runat="server">
                <h2 style="color:blue;">Recipe Comments</h2>
                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
            </asp:Panel>
        </div>
        <script>
            $(document).ready(function(){
                $("#button1").click(function(){
                    $("#myModal").modal();
                });
            });
        </script>
    </form>
</body>
</html>
