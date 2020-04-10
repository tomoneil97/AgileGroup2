using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;

public partial class ProfilePage : System.Web.UI.Page
{
    public string img;
    protected void Page_Load(object sender, EventArgs e)
    {
        string username = "";
        if (Request.Cookies["user"] != null)
        {
            username = Request.Cookies["user"].Value;
        }
        img = Actions.getProfileImage(username);
        if(Actions.isDriver(username) == "True")
        {
            driverdiv.Visible = false;
        }
    }



    protected void DriverRegBtn(object sender, EventArgs e)
    {
        string username = "";
        if (Request.Cookies["user"] != null)
        {
            username = Request.Cookies["user"].Value;
        }
        string password = Request.Form["psw"];
        if (Actions.Logon(username, password))
        {
            
            Response.Cookies["cookie"].Value = username;
            Response.Cookies["cookie"].Expires = DateTime.Now.AddMinutes(10);

            string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
            conn.Open();
            string sql = "UPDATE dbo.CARPOOLUSER SET [isDriver] = @drv WHERE [Username] = @uname";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                
                cmd.Parameters.AddWithValue("@drv", 1);
                
                cmd.Parameters.AddWithValue("@uname", username);

                cmd.ExecuteNonQuery();
            }
            Response.Redirect("AddVehicle.aspx");
        }
    }

    protected void Update(object sender, EventArgs e)
    {
        string username = "";
        if (Request.Cookies["user"] != null)
        {
            username = Request.Cookies["user"].Value;
        }
        string password = Request.Form["psw"];
        if (Actions.Logon(username, password))
        {
            HttpPostedFile postedFile = Request.Files["filetag"];
            string filePath = "";
            string imageName = "";
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                //Save the File.
                filePath = Server.MapPath("~/images/usericons/") + username + Path.GetExtension(postedFile.FileName);
                postedFile.SaveAs(filePath);
                imageName = "images/usericons/" + username + Path.GetExtension(postedFile.FileName);
            }

            string foreName = Request.Form["forename"];
            string surName = Request.Form["surname"];
            string gender = Request.Form["gender"];
            string course = Request.Form["course"];
           

            string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
            conn.Open();
            string sql = "UPDATE [dbo].[CARPOOLUSER] SET  ";
            if(postedFile != null)
            {
                sql += " [ImageName] = @img,";
            }
            if(foreName != "")
            {
                sql += " [FirstName] = @forename,";
            }
            if(surName != "")
            {
                sql += " [Surname] = @surname,";
            }
            if(gender != "null")
            {
                sql += " [Gender] = @gender,";
            }
            if (course != "null")
            {
                sql += " [CourseName] = @course,";
            }
            sql = sql.Remove(sql.Length - 1);
            sql += " WHERE [Username] = @uname";
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@forename", foreName);
                cmd.Parameters.AddWithValue("@surname", surName);
                cmd.Parameters.AddWithValue("@course", course);
                cmd.Parameters.AddWithValue("@gender", gender);
                cmd.Parameters.AddWithValue("@img", imageName);
                cmd.Parameters.AddWithValue("@uname", username);

                cmd.ExecuteNonQuery();
            }

        }
        Response.Redirect("ProfilePage.aspx");
    }
}