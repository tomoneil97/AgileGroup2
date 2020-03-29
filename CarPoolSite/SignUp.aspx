<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignUp.aspx.cs" Inherits="SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8" />
    <title>Register</title>
    <link rel="stylesheet" href="css/loginform.css">
</head>
<body onload="document.getElementById('id01').style.display='block'">


     <div id="id01" class="modal">
         
        <form class="modal-content animate" runat="server">
            <div class="imgcontainer">
                <span onclick="location.href = 'Index.html'" class="close" title="Return">&times;</span>
                <h1> Register </h1>
                <div>
                     
            </div>
               
            <label><b>Upload Image</b></label>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="Upload" />
            <hr />
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" ShowHeader="false">
                <Columns>
                    <asp:BoundField DataField="Text" />
                    <asp:ImageField DataImageUrlField="Value" ControlStyle-Height="100" ControlStyle-Width="100" />
                </Columns>
            </asp:GridView>
          
            <label class="login">Course: </label>
            <div class="container">
                <select class="login" name="course">
                    <option value="Accounting">Accounting</option>
                    <option value="Biology">Biology</option>
                    <option value="Computer Science">Computer Science</option>
                    <option value="Electronic Engineering">Electronic Engineering</option>
                    <option value="Maths">Maths</option>
                    <option value="Law">Law</option>
                    
                </select>

                <label class="login"> Would you like to register as a driver?: </label>
                <input id="box" type="checkbox" checked="checked" name="driverCheck"/>
               
                
                <asp:Button ID="nextBtn" runat="server" Text="Next" OnClick="CreateAccount" />
                
            </div>

            
        </form>
         
    </div>
   
    
     <script>

        //window.onbeforeunload = function (e) {
        //  return 'Are you sure?';
        // };

         $(document).ready(function() {

    
        var readURL = function(input) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('.profile-pic').attr('src', e.target.result);
                    }
    
                    reader.readAsDataURL(input.files[0]);
                }
            }
    

            $(".file-upload").on('change', function(){
                readURL(this);
            });
    
            $(".upload-button").on('click', function() {
               $(".file-upload").click();
            });
         });


         document.getElementById("submitBtn").addEventListener("click", function(){
    
        }); 

    </script>

</body>
</html>
