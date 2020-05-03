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
    public string notifications;
    protected void Page_Load(object sender, EventArgs e) //this is the openNav 
    {

        if (Request.Cookies["user"] != null)
        {
            username = Request.Cookies["user"].Value;
        }
        img = Actions.getProfileImage(username);
        if (Actions.isDriver(username) == "True")
        {
            riderView.Style.Add("display", "none");

            AddUserMarkers();
            DrivercheckForRide();
        }
        else
        {
            driverView.Style.Add("display", "none");

            RidercheckForRide();
        }
        Dictionary<string, string> notifs = Actions.Notifications(username);
        if (notifs.Count == 0)
        {
            notifDiv.Visible = false;
        }
        else
        {
            notifNum = notifs.Count;
            foreach(KeyValuePair<string,string> entry in notifs)
            {
                notifications += "<tr> <td>" + entry.Key + "</td> <td> " + entry.Value + "</td> </tr>";
                    
            }
        }
    }

    public void DrivercheckForRide()
    {
        //gets the localpath of the database so it can work on other hosts
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        string sql = "SELECT Id, isActive FROM dbo.RIDE WHERE [DriverName] = @uname";
        string id = "";
        bool active = false;
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    id = reader.GetInt32(0).ToString();
                    active = reader.GetBoolean(1);
                }
            }
        }
        if (active)
        {
            destinationModal.Style.Add("display", "none");
            directionsModal.Style.Add("display", "block");
        }
        if (String.IsNullOrEmpty(id))
        {
            return;
        }
        else
        {
            activeRide.Style.Add("display", "block");
        }

    }

    public void RidercheckForRide()
    {
        //gets the localpath of the database so it can work on other hosts
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        string sql = "SELECT Id, isActive FROM dbo.RIDERS WHERE [RiderUsername] = @uname";
        string id = "";
        bool active = false;

        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    id = reader.GetInt32(0).ToString();
                    active = reader.GetBoolean(1);

                }
            }
        }
        if (active)
        {
            destinationModal.Style.Add("display", "none");
            acceptedRider.Style.Add("display", "block");
        }
        if (String.IsNullOrEmpty(id))
        {
            return;
        }
        else
        {
            riderActive.Style.Add("display", "block");
        }

    }

    public void AddUserMarkers()
    {
        List<Rider> riderlist = new List<Rider>();

        //gets the localpath of the database so it can work on other hosts
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
                    if (reader.IsDBNull(3))
                    {
                        Rider rider = new Rider(reader.GetString(1), reader.GetString(4), reader.GetString(6));
                        riderlist.Add(rider);
                    }

                }
            }
        }

        //creates javascript which is inserted in the home page
        markersString = "";
        foreach (Rider rider in riderlist)
        {
            markersString += " var " + rider.name + @" = new google.maps.Marker({
              animation: google.maps.Animation.BOUNCE,
              position: { lat: " + rider.lat + @", lng: " + rider.lon + @"},
              map: map,
              title: 'Destination: " + rider.destination + @"'
            });

            
             " + rider.name + @".addListener('click', function(){
    
                document.getElementById('riderName').value = '" + rider.name + @"';
                document.getElementById('riderLocation').value = '" + rider.lat + rider.lon + @"';
                document.getElementById('riderDestination').value = '" + rider.destination + @"';




                document.getElementById('riderDestTxt').innerHTML = '" + rider.name + @" needs a lift !';
                 document.getElementById('riderlocationTxt').innerHTML = 'Location: " + rider.lat + rider.lon + @"';
                 document.getElementById('riderdestinationTxt').innerHTML = 'Destination: " + rider.destination + @"';
                 document.getElementById('rideRequest').style.display = 'block';
                });
            ";

        }


    }

    //using WebMethod to run back-end code asynchronously 

    [WebMethod]
    public static string requestRide(string Dest, string U_Loc)
    {
        string Destination = Dest;
        string Location = U_Loc;
        string sql;
        string usern = username;

        DateTime myDateTime = DateTime.Now;
        string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

        //gets the localpath of the database so it can work on other hosts
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        if (Location != "")
        {

            sql = "INSERT INTO [dbo].[RIDERS] ([RiderUsername],[Date],[Location],[Destination]) VALUES (@uname,@date,@loc,@dest)";
        }
        else return null;

        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@date", sqlFormattedDate);
            cmd.Parameters.AddWithValue("@loc", Location);
            cmd.Parameters.AddWithValue("@dest", Destination);
            cmd.ExecuteNonQuery();
        }
        return "Complete";
    }

    [WebMethod]
    public static string confirmRider(string Name, string Location, string Destination)
    {
        DateTime myDateTime = DateTime.Now;
        string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

        //gets the localpath of the database so it can work on other hosts
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        string sql = "SELECT Id FROM dbo.RIDE WHERE [DriverName] = @uname";
        string id;
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);

            id = cmd.ExecuteScalar()?.ToString();
        }
        if (String.IsNullOrEmpty(id))
        {
            sql = "INSERT INTO [dbo].[RIDE] ([Date],[isActive],[isAccepted],[DriverName],[Destination]) VALUES (@date,@activ,@accept,@uname,@dest)";

            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@date", sqlFormattedDate);
                cmd.Parameters.AddWithValue("@activ", 0);
                cmd.Parameters.AddWithValue("@accept", 1);
                cmd.Parameters.AddWithValue("@uname", username);
                cmd.Parameters.AddWithValue("@dest", Destination);
                cmd.ExecuteNonQuery();
            }
        }


        sql = "UPDATE [dbo].[RIDERS] SET [RideID] = (SELECT Id FROM [dbo].[RIDE] WHERE DriverName = @uname), [isActive] = @actv WHERE RiderUsername = @rname";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@actv", 1);
            cmd.Parameters.AddWithValue("@rname", Name);
            cmd.ExecuteNonQuery();
        }

        sql = "INSERT INTO [dbo].[NOTIFICATION] ([Recipient],[Message],[Date]) VALUES (@to,@msg,@date)";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@to", Name);
            cmd.Parameters.AddWithValue("@msg", username + " has accepted your ride!");
            cmd.Parameters.AddWithValue("@date", sqlFormattedDate);
            cmd.ExecuteNonQuery();
        }

        return "Complete";
    }

    [WebMethod]
    public static string finaliseRide()
    {
        //gets the localpath of the database so it can work on other hosts
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();
        string sql = "UPDATE [dbo].[RIDE] SET [isActive] = @actv WHERE DriverName = @uname";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.Parameters.AddWithValue("@actv", 1);
            cmd.ExecuteNonQuery();
        }

        return "True";
    }

    [WebMethod]
    public static string CancelRider()
    {
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        string drivername = "";
        string sql = "SELECT DriverName FROM RIDE WHERE Id = (SELECT RideID FROM RIDERS WHERE RiderUsername = @uname)";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    drivername = reader.GetString(0);
                }
            }
        }
         sql = "DELETE FROM RIDERS WHERE RiderUsername = @uname";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.ExecuteNonQuery();
        }

       

        DateTime myDateTime = DateTime.Now;
        string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

        sql = "INSERT INTO [dbo].[NOTIFICATION] ([Recipient],[Message],[Date]) VALUES(@to, @msg, @date)";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@to", drivername);
            cmd.Parameters.AddWithValue("@msg", username + " has canceled your ride!");
            cmd.Parameters.AddWithValue("@date", sqlFormattedDate);
            cmd.ExecuteNonQuery();
        }

        return "done";
    }

    [WebMethod]
    public static string CancelRide()
    {
        string localPath = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory)) + @"App_Data\Database.mdf";

        SqlConnection conn = new SqlConnection();
        conn.ConnectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = " + localPath + "; Integrated Security = True";
        conn.Open();

        List<string> riders = new List<string>();
        string sql = "SELECT RiderUsername FROM RIDERS WHERE RideID = (SELECT RideID FROM RIDE WHERE DriverName = @dname)";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@dname", username);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    riders.Add(reader.GetString(0));
                }
            }


        }

         sql = "DELETE FROM RIDE WHERE DriverName = @uname";
        using (SqlCommand cmd = new SqlCommand(sql, conn))
        {
            cmd.Parameters.AddWithValue("@uname", username);
            cmd.ExecuteNonQuery();
        }

       
        DateTime myDateTime = DateTime.Now;
        string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
        sql = "INSERT INTO [dbo].[NOTIFICATION] ([Recipient],[Message],[Date]) VALUES(@to, @msg, @date)";
        foreach ( string rider in riders)
        {
            
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@to", rider);
                cmd.Parameters.AddWithValue("@msg", username + " has canceled your ride!");
                cmd.Parameters.AddWithValue("@date", sqlFormattedDate);
                cmd.ExecuteNonQuery();
            }
        }



        return "done";
    }
}