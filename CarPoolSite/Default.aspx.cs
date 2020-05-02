using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
         
    }

    protected void Login(object sender, EventArgs e)
    {
        //gets username and password from form
        string username = Request.Form["uname"];
        string password = Request.Form["psw"];

        //checks the username and password against the database
        if (Actions.Logon(username, password))
        {
            //creates an authentication cookie
            Response.Cookies["user"].Value = username;
            Response.Cookies["user"].Expires = DateTime.Now.AddMinutes(10);
            string isAdmin = Actions.isAdmin(username);

            if (isAdmin == "True")
            {
                Response.Redirect("AdminHome.aspx");
            }
            else Response.Redirect("Home.aspx");
        }
        else
        {
            return;
        }
    }

    
}