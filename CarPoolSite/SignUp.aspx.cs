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
using System.Data;
using System.Reflection;

public partial class SignUp :  System.Web.UI.Page
{
    public User newUser;
    protected void Page_Load(object sender, EventArgs e)
    {
        string userName = Request.Form["uname"];
        if(userName == null)
        {
            return;
        }
        else
        {
            Response.Cookies["cookie"].Value = userName;
            Response.Cookies["cookie"].Expires = DateTime.Now.AddMinutes(10);
        }
        
        

        string passWord = Request.Form["psw"];
        string foreName = Request.Form["forename"];
        string surName = Request.Form["surname"];
        string gender = Request.Form["gender"];
        
        
        newUser = new User(userName, passWord, foreName, surName, gender);
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();
        string sql = "INSERT INTO dbo.CARPOOLUSER ([Username],[Password],[FirstName],[Surname],[Gender],[ImageName],[CourseName],[isDriver]) values (@uname,@pword,@fname,@sname,@gnder,@image,@crse,@drvr)";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", userName);
            cmd.Parameters.AddWithValue("@pword", passWord);
            cmd.Parameters.AddWithValue("@fname", foreName);
            cmd.Parameters.AddWithValue("@sname", surName);
            cmd.Parameters.AddWithValue("@gnder", gender);
            cmd.Parameters.AddWithValue("@image", DBNull.Value);
            cmd.Parameters.AddWithValue("@crse", DBNull.Value);
            cmd.Parameters.AddWithValue("@drvr", 0);
            cmd.ExecuteNonQuery();
        }
    }


    protected void Upload(object sender, EventArgs e)
    {
        string username = "";
        if (Request.Cookies["cookie"] != null)
        {
            username = Request.Cookies["cookie"].Value;
        }

        Image profileImage = (Image)FindControl("preview");

        HttpPostedFile postedFile = Request.Files["filetag"];
        string filePath = "";
        string imageName = "";
        if (postedFile != null && postedFile.ContentLength > 0)
        {
            //Save the File.
            filePath = Server.MapPath("~/images/usericons/") + username+ Path.GetExtension(postedFile.FileName);
            postedFile.SaveAs(filePath);
            imageName = "images/usericons/" + username + Path.GetExtension(postedFile.FileName);
        }
        int isDriver = 0;
        string driver = Request.Form["isDriver"];
        if(driver == "on")
        {
            isDriver = 1;
        }

        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();
        string sql = "UPDATE dbo.CARPOOLUSER SET [isDriver] = @drv, [ImageName] = @img WHERE [Username] = @uname";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@drv", isDriver);
            cmd.Parameters.AddWithValue("@img", imageName);
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.ExecuteNonQuery();
        }




            Response.Redirect("Default.aspx");
    }

    

}