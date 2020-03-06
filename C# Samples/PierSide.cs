using System.Windows.Forms;
using TheSkyXLib;

public class PierSide
{

    /// Windows Visual Basic Sample Console Application: PierSide
    ///
    /// ------------------------------------------------------------------------
    ///               Original Example
    ///
    ///			    Developed 2015, R.McAlister
    ///			    Ported to C# 2017
    ///
    /// ------------------------------------------------------------------------
    ///
    /// This Visual Basic console application reads the pier side position of the current
    ///   telescope and shows the value.

    public void PierSideSample()
   {
       sky6RASCOMTele tsx_tele = new sky6RASCOMTele();
        tsx_tele.DoCommand(11, "");
        string pierstring = tsx_tele.DoCommandOutput;

        MessageBox.Show("Pier Side: " + pierstring);
	return;
   }
}