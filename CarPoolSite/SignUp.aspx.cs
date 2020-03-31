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
        string sql = "INSERT INTO dbo.CARPOOLUSER ([Username],[Password],[FirstName],[Surname],[Gender],[Image],[CourseName],[isDriver]) values (@uname,@pword,@fname,@sname,@gnder,@image,@crse,@drvr)";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", userName);
            cmd.Parameters.AddWithValue("@pword", passWord);
            cmd.Parameters.AddWithValue("@fname", foreName);
            cmd.Parameters.AddWithValue("@sname", surName);
            cmd.Parameters.AddWithValue("@gnder", gender);
            cmd.Parameters.Add("@image", SqlDbType.VarBinary).Value = DBNull.Value;
            cmd.Parameters.AddWithValue("@crse", DBNull.Value);
            cmd.Parameters.AddWithValue("@drvr", 0);
            cmd.ExecuteNonQuery();
        }
    }


    protected void Upload(object sender, EventArgs e)
    {
        Image profileImage = (Image)FindControl("preview");
        
        
        string username = "";
        if (Request.Cookies["cookie"] != null)
        {
            username = Request.Cookies["cookie"].Value;
        }

        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = "+localPath+"; Integrated Security = True";
        conn.Open();
        string sql = "UPDATE CARPOOLUSER SET [Image] = @img WHERE [Username] = @uname";
        using(SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@image", profileImage.);
            cmd.ExecuteNonQuery();
        }
        Response.Redirect("Home.aspx");
    }

    

}