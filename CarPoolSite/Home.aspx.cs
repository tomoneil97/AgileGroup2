using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    public string img;
    public int notifNum = 0;
    protected void Page_Load(object sender, EventArgs e) //this is the openNav 
    {
        string username = "";
        if (Request.Cookies["user"] != null)
        {
            username = Request.Cookies["user"].Value;
        }
        img = Actions.getProfileImage(username);
        if (Actions.isDriver(username) == "True")
        {
            riderView.Visible = false;
        }
        List<string> notifs = Actions.Notifications(username);
        if(notifs.Count == 0)
        {
            notifDiv.Visible = false;
        }
        else
        {
            notifNum = notifs.Count;
        }
    }

    [WebMethod]
    public static string requestRide(string Dest,string U_Loc)
    {
        string dLocation = Dest;
        string sLocation = U_Loc;
        string sql;
        string username;
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        if (dLocation != ""){
            //sql = "UPDATE [dbo].[RIDE] SET [Destination] = @dLocation";
            sql = "INSERT INTO [dbo].[RIDE] ([Destination]) VALUES (@dLocation)";
        }else return null;

        using (SqlCommand cmd = new SqlCommand(sql, conn)){
            cmd.Parameters.AddWithValue("@dLocation", dLocation);
            cmd.ExecuteNonQuery();
        }
            return "This string is from Code behind";
    }
    
}