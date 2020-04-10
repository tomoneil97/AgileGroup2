<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfilePage.aspx.cs" Inherits="ProfilePage" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>My Rides</title>
    <link rel="stylesheet" href="css/site.css">
</head>
<body>
    <div>
        <nav class="topbar">
            <input type="image" src="<%=img %>" alt="icon" class="userIcon" onclick="openNav()">
        </nav>
    </div>
    

    <div id="mySidenav" class="sidenav">
        <a href="Home.aspx">Home</a>
        <a href="ProfilePage.aspx">Profile</a>
        <a href="MyRides.aspx">My Rides</a>
        <a asp-page="./Settings">Settings</a>
        <a asp-page="./Privacy">Privacy</a>
        <a asp-page="./Contact">Contact</a>
        </div>
    
    <div class="menu">
    <form id="form1" runat="server">
        
             <label><b>Change Profile Image</b></label>
            <input type="file" name="filetag" id="filetag" accept="image/x-png,image/jpeg" runat="server"/>
                
                
            <img id="preview" src="<%=img %>" style="border-radius:50%;" height="200" width="200">

            <hr />
            <label class="login">Course: </label>
            <select class="login" name="course">
                    <option value="null"></option>
                    <option value="Computer Science">Computer Science</option>
                    <option value="Maths">Maths</option>
                    <option value="English">English</option>
                </select> <br />

            <label class="login"> Forename: </label>
                <input class="login" type="text" name="forename" placeholder="Enter Forename" />
                <label class="login"> Surname: </label>
                <input class="login" type="text" name="surname" placeholder="Enter Surname" />
                <label class="login">Gender: </label>
                <select class="login" name="gender">
                    <option value="null"></option>
                    <option value="male">Male</option>
                    <option value="female">Female</option>
                    <option value="other">Other</option>
                </select> <br />
        <hr />
        <label class="login" for="psw"><b>Please enter your password to update profile or become a driver</b></label>
                <input class="login" type="password" placeholder="Enter Password" name="psw" required>
         
        <hr />
        <label><b>Update Profile</b></label>
        <asp:Button ID="Button1" runat="server" Text="Update Profile" OnClick="Update" />
        <br />
        <div id="driverdiv" runat="server">
            <label><b>Register as a Driver?</b></label>
            <asp:Button ID="btnDriver" runat="server" Text="Register" OnClick="DriverRegBtn" />
        </div>
            
        
    </form>
     </div>   
        
        
        
     <script>
        function openNav() {
            if (document.getElementById("mySidenav").style.width == "250px") {
                closeNav();
            }
            else {
                document.getElementById("mySidenav").style.width = "250px";
            }


        }

        function closeNav() {
            document.getElementById("mySidenav").style.width = "0";
         }

         var fileTag = document.getElementById("filetag"),
            preview = document.getElementById("preview");
    
        fileTag.addEventListener("change", function() {
          changeImage(this);
        });

        function changeImage(input) {
          var reader;

          if (input.files && input.files[0]) {
            reader = new FileReader();

            reader.onload = function(e) {
              preview.setAttribute('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
          }
        }
    </script>
</body>
</html>
