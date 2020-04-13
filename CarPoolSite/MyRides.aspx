<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyRides.aspx.cs" Inherits="MyRides" %>

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
        <table>
            <thead>
                <tr>
                    <th> Ride Date</th>
                    <th> Ride Time </th>
                    <th> Manage Ride</th>
                </tr>
            </thead>
            <tbody>

            </tbody>
        </table>
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
    </script>
</body>
</html>
