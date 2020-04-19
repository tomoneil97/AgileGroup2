using System;
using System.Collections.Generic;
using System.Data;
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
    public static string username;
    public string markersString;
    protected void Page_Load(object sender, EventArgs e) //this is the openNav 
    {
       
        if (Request.Cookies["user"] != null)
        {
            username = Request.Cookies["user"].Value;
        }
        img = Actions.getProfileImage(username);
        if (Actions.isDriver(username) == "True")
        {
            riderView.Visible = false;
            AddUserMarkers();
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

    public void AddUserMarkers()
    {
        List<Rider> riderlist = new List<Rider>();
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();
        string sql = "SELECT * FROM [dbo].[RIDERS]";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
           
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    if(reader.IsDBNull(3))
                    {
                        Rider rider = new Rider(reader.GetString(1), reader.GetString(4), reader.GetString(6));
                        riderlist.Add(rider);
                    }
                   
                }
            }
        }
        markersString= "";
        foreach(Rider rider in riderlist)
        {
            markersString += " var " + rider.name + @" = new google.maps.Marker({
              animation: google.maps.Animation.BOUNCE,
              position: { lat: " + rider.lat + @", lng: " + rider.lon + @"},
              map: map,
              title: 'Destination: " + rider.destination + @"'
            });

            
             " + rider.name + @".addListener('click', function(){
                
                document.getElementById('riderDest').innerHTML = '" + rider.name + @" needs a lift !';
                 document.getElementById('riderlocation').innerHTML = 'Location: " + rider.lat + rider.lon + @"';
                 document.getElementById('riderdestination').innerHTML = 'Destination: " + rider.destination + @"';
                 document.getElementById('rideRequest').style.display = 'block';
                });
            ";

        }
        
       
    }

    [WebMethod]
    public static string requestRide(string Dest,string U_Loc)
    {
        string Destination = Dest;
        string Location = U_Loc;
        string sql;
        string usern = username;

        DateTime myDateTime = DateTime.Now;
        string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        if (Location != ""){
          
            sql = "INSERT INTO [dbo].[RIDERS] ([RiderUsername],[Date],[Location],[Destination]) VALUES (@uname,@date,@loc,@dest)";
        }else return null;

        using (SqlCommand cmd = new SqlCommand(sql, conn)){
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@date", sqlFormattedDate);
            cmd.Parameters.AddWithValue("@loc", Location);
            cmd.Parameters.AddWithValue("@dest", Destination);
            cmd.ExecuteNonQuery();
        }
            return "Complete";
    }
    
}