using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateAccount(object sender, EventArgs e)
    {
        User newUser;
        if (File.Exists(Server.MapPath("~/image/") + "jke" + ".*"))
        {
            string jke = "asd";
        }
        string usercourse = Request.Form["course"];
        string Driver = Request.Form["driverCheck"];
        
    }
}