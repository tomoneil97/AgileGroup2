using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;

public partial class SignUp :  System.Web.UI.Page
{
    public User newUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Request.Form["uname"];
        string passWord = Request.Form["psw"];
        string foreName = Request.Form["forename"];
        string surName = Request.Form["surname"];
        string gender = Request.Form["gender"];

        
        newUser = new User(userName, passWord, foreName, surName, gender);
        
    }


    protected void Upload(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string fileName = newUser.ID.ToString() + Path.GetExtension(FileUpload1.PostedFile.FileName);

            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/images/") + fileName);
            //Response.Redirect(Request.Url.AbsoluteUri);
        }
    }

    protected void CreateAccount(object sender, EventArgs e)
    {
        if (File.Exists(Server.MapPath("~/image/") + newUser.ID +".*"))
        {
            string jke = "asd";
        }
        string usercourse = Request.Form["course"];
        string Driver = Request.Form["driverCheck"];
        newUser.COURSE = usercourse;
    }

}