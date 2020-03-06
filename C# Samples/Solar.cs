using System.Windows.Forms;
using TheSkyXLib;

public class Solar
{

    /// Windows C# Sample Console Application: Solar
    ///
    /// ------------------------------------------------------------------------
    ///               Vaguely adapted from Solar.vbs
    ///               Copyright Software Bisque 
    ///           
    ///               Converted by:  R.McAlister 2017
    ///
    /// ------------------------------------------------------------------------
    ///
    ///Very simple demonstration application that targets the telescope on the Sun -- careful now.
    ///
    ///  Note:  this is basically simplification of "ListSearch.vb" where the only target is the Sun.
    ///
    ///


    public void SolarSample()
    {
        ///Target Sun
        string target = "Sun";

        ///Create Objects
        sky6StarChart tsx_sc = new sky6StarChart();
        sky6RASCOMTele tsx_ts = new sky6RASCOMTele();
        sky6ObjectInformation tsx_oi = new sky6ObjectInformation();

        ///Connect telescope
        tsx_ts.Connect();

        tsx_sc.Find(target);
        tsx_oi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_ALT);
        double dAlt = tsx_oi.ObjInfoPropOut;
        tsx_oi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_AZM);
        double dAz = tsx_oi.ObjInfoPropOut;

        try {
            tsx_ts.SlewToAzAlt(dAz, dAlt, target);
        }
        catch {
            MessageBox.Show("An error has occurred running slew");
            return;
        };
        MessageBox.Show("The Sun's location is at: Altitude: " + dAlt.ToString() + "  Azimuth: " + dAz.ToString());

        ///Disconnect telesope
        tsx_ts.Disconnect();
        return;
    }
}