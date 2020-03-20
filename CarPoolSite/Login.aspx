<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link rel="stylesheet" href="css/loginform.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loginbuttons">
            <h1 style="font-weight:bold; color:dimgrey; display:block; margin-left:5%;">Login</h1>
            <div class="login">
                <label class="login"> Username: </label> <input class="login" type="text" name="username" /><br />
                <label class="login"> Password: </label> <input class="login" type="password" name="password" /> <br />
            </div>

            <input class="loginbuttons" type="submit" value="Submit" />
        </div>
    </form>
</body>
</html>
