using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.SqlClient;

public partial class AddVehicle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void CreateCar(object sender, EventArgs e)
    {
        string username = "";
        if (Request.Cookies["cookie"] != null)
        {
            username = Request.Cookies["cookie"].Value;
        }
        string make = Request.Form["make"];
        string regnum = Request.Form["reg"];
        string model = Request.Form["model"];
        int numofseats = Int32.Parse(Request.Form["numOfSeats"]);

        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";
        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();
        string sql = "INSERT INTO dbo.VEHICLE ([Username],[Make],[Model],[RegNum],[NumOfSeats]) values (@uname,@make,@model,@reg,@seats)";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@make", make);
            cmd.Parameters.AddWithValue("@model", model);
            cmd.Parameters.AddWithValue("@reg", regnum);
            cmd.Parameters.AddWithValue("@seats", numofseats);
            cmd.ExecuteNonQuery();
        }

        Response.Cookies["cookie"].Expires = DateTime.Now.AddDays(-10);
        Response.Cookies["cookie"].Value = null;
        Response.Redirect("Default.aspx");
    }
                
}