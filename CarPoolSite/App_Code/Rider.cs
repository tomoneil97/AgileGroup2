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
    public string location;
    public string destination;

    public Rider(string _name, string _location, string _destination)
    {
        this.name = _name;
        this.location = _location;
        this.destination = _destination;
    }
}