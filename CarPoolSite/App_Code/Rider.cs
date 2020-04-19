using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Rider
/// </summary>
public class Rider
{
    public string name;
    public string lat;
    public string lon;
    public string destination;

    public Rider(string _name, string _location, string _destination)
    {
        this.name = _name;
        string[] latlong = _location.Split(',');
        this.lat =latlong[0];
        this.lon = latlong[1];
        this.destination = _destination;
    }
}