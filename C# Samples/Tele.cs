using System.Windows.Forms;
using TheSkyXLib;

public class Tele
{

    /// Windows C# Sample Console Application: Tele
    ///
    /// ------------------------------------------------------------------------
    ///               Adapted from Tele.vbs  (Visual Basic Script)
    ///               Copyright (C) Software Bisque (2009?)
    ///
    ///				Converted 2017, R.McAlister
    ///
    /// ------------------------------------------------------------------------
    ///
    /// This C# console application exercises the telescope class functions
    /// 

    public void TeleSample()
    {
        ///Use TheSky to generate a text file of mapping points
        string szPathToMapFile = "C:\\Users\\Rick\\Documents\\Software Bisque\\TheSkyX Professional Edition\\Exported Data\\map.txt";

        ///Set the exposure time for the image
        double dExposure = 1.0;
;
        ///Create the telescope object
        sky6RASCOMTele tsx_ts = new sky6RASCOMTele();

        ///Connect to the telescope
        tsx_ts.Connect();

        ///See if connection failed
        if (tsx_ts.IsConnected == 0) {
            MessageBox.Show("Connection failed.");
            return;
        }
       
        ///Get and show the current telescope ra, dec
        tsx_ts.GetRaDec();
        MessageBox.Show("Ra,Dec =" + tsx_ts.dRa.ToString() + ",  " + tsx_ts.dDec.ToString());

        ///Get and show the current telescope az, alt
        tsx_ts.GetAzAlt();
        MessageBox.Show("Az,Alt=" + tsx_ts.dAz.ToString() + ",  " + tsx_ts.dAlt.ToString());

        ///Goto an arbitrary RA and Dec
        tsx_ts.SlewToRaDec(2.0, 3.0, "Home");
        MessageBox.Show("GotoComplete");

        ///Sync on an ra dec
        tsx_ts.Sync(3.0, 3.0, "Matt");

        ///Disconnect the telscope
        tsx_ts.Disconnect();
        return;
    }
}