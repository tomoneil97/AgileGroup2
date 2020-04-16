using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ride
/// </summary>
public class Ride
{
    //User ID = new User(UserName);
    //public int riderID 
    private int id;
    private string username;
    private string sLocation;
    private string destination;
    private string actual_arrivalTime;
    private string predicted_arrivalTime;

    public int rID
    {
        get { return id; }

        set { id = value; }
    }
    public string rUsername
    {
        get { return username; }

        set { username = value; }
    }
    public string rStart
    {
        get { return sLocation; }

        set { sLocation = value; }
    }
    public string rDestination
    {
        get { return destination; }

        set { destination = value; }
    }
    public string r_aTime
    {
        get { return actual_arrivalTime; }

        set { actual_arrivalTime = value; }
    }
    public string r_pTime
    {
        get { return predicted_arrivalTime; }

        set { predicted_arrivalTime = value; }
    }

    public Ride(string username, string sLocation, string destination, string date, string time)
    {
        
    }
}