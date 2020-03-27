using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data.SqlClient;
using System.Configuration;

public partial class SignUp :  System.Web.UI.Page
{
    public int newUserId;
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Request.Form["uname"];
        string passWord = Request.Form["psw"];
        string foreName = Request.Form["forename"];
        string surName = Request.Form["surname"];
        string gender = Request.Form["gender"];

        User newUser = new User(userName, passWord, foreName, surName, gender);
        newUserId = newUser.ID;
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        
    }
}