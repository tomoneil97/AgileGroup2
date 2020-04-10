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
        string username = Request.Form["uname"];
        string password = Request.Form["psw"];
        if (Actions.Logon(username, password))
        {
            Response.Cookies["user"].Value = username;
            Response.Cookies["user"].Expires = DateTime.Now.AddMinutes(10);
            Response.Redirect("Home.aspx");
        }
        else
        {
            return;
        }
    }

    
}