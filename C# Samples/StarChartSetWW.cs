using System.Windows.Forms;
using TheSkyXLib;

public class StarChartSetWW
{
    /// Windows C# Sample Console Application: StarChartSetWW
    ///
    /// ------------------------------------------------------------------------
    ///               Adapted from TheSkySetWW.vbs  (Visual Basic Script)
    ///               Copyright (C) Software Bisque (2009?)
    ///
    ///				Converted 2014, R.McAlister
    ///
    /// ------------------------------------------------------------------------
    ///
    /// This C# console application exercises the Star Chart class functions for setting 
    ///   Timne, Time Zone, Location, etc.
    ///
    
    public void StarChartSetWWSample()
    {
        double dJD = 2452066.0; ///=06/05/2001, ignored if UseCompterClock=1
        double dTZ = 7;         ///MST
        double dElev = 1000;      ///meters
        double dLat = 39.5;
        double dLong = 105.5;
        string szLoc = "Location from script";

        ///Create Telescope Object and disconnect telescope -- time changes are not enabled with scope connected
        sky6RASCOMTele tsx_ts = new sky6RASCOMTele();
        tsx_ts.Disconnect();

        ///Create the Star Chart object
        sky6StarChart tsx_sc = new sky6StarChart();

        ///Turn off the Computer Clock
        tsx_sc.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_UseComputerClock, 0);

        ///Change the julian date
        tsx_sc.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_JulianDateNow, dJD);

        ///Change the time zone
        tsx_sc.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_Time_Zone, dTZ);

        ///Change the elevation
        tsx_sc.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_ElevationInMeters, dElev);

        ///Change the latitude
        tsx_sc.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_Latitude, dLat);

        ///Change the longitude
        tsx_sc.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_Longitude, dLong);

        ///Change the location description
        tsx_sc.SetDocumentProperty(Sk6DocumentProperty.sk6DocProp_LocationDescription, szLoc);

        return;
    }
}