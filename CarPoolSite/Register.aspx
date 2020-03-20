<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="css/loginform.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="loginbuttons">
            <h1 style="font-weight:bold; color:dimgrey; display:block; margin-left:5%;"> Register</h1>
            <div class="login">
                <label class="login"> Username: </label> <input class="login" type="text" name="username" /><br />
                <label class="login"> Forename: </label> <input class="login" type="text" name="forname" /><br />
                <label class="login"> Surname: </label> <input class="login" type="text" name="surname" /><br />
                <label class="login">Gender: </label> 
                <select class="login" id="gender">
                  <option value="male">Male</option>
                  <option value="female">Female</option>
                  <option value="other">Other</option>
                </select> <br />
                <label class="login"> Password: </label> <input class="login" type="password" name="password" /> <br />
            </div>

            <input class="loginbuttons" type="submit" value="Submit" />
        </div>
    </form>
</body>
</html>
