using System.Windows.Forms;
using TheSkyXLib;

public class Dome
{
    /// Windows C# Sample Console Application: Dome
    ///
    /// ------------------------------------------------------------------------
    ///               Adapted from Dome.vbs  (Visual Basic Script)
    ///               Copyright (C) Software Bisque (2013)
    ///
    ///				Converted 2017, R.McAlister
    ///
    /// ------------------------------------------------------------------------
    ///
    /// This C# console application demonstrates how to access dome control.
    ///
    ///  Note:  The Dome add-on was not available for testing this script. Use at your own risk.
    ///

    public void DomeSample()
    {
        ///Create the dome object, connect and get it///s targeting
        sky6Dome tsx_do = new sky6Dome();
        tsx_do.Connect();
        tsx_do.GetAzEl();

        double DomeAz = tsx_do.dAz;
        double DomeEl = tsx_do.dEl;

        MessageBox.Show("Dome Azimuth at: " + DomeAz.ToString() + "  Dome Elevation at: " + DomeEl.ToString());
        MessageBox.Show("Moving to Azimuth 0 degrees, Elevation 0 Degrees.");

        ///Target the dome at az = 0, alt = 0
        tsx_do.GotoAzEl(0, 0);

        ///Disconnect the dome
        tsx_do.Disconnect();

        return;

      }
}