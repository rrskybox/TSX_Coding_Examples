using System.Windows.Forms;
using TheSkyXLib;

public class CLS
{

    /// Windows C# Sample Console Application: ClosedLoopSlew
    ///
    /// ------------------------------------------------------------------------
    ///
    ///               Author: R.McAlister (2017)
    ///
    /// ------------------------------------------------------------------------
    ///
    /// ClosedLoopSlew:  Find a target (M39 in this case), then execute a closed loop slew to it
    ///
    ///
    public void CLSSample()
    {
              
        ///Set connection to star chart and perform a find on M39
        sky6StarChart tsx_sc = new sky6StarChart();
        tsx_sc.Find("M39");

        ///Create connection to camera and connect
        ccdsoftCamera tsx_cc = new ccdsoftCamera();
        tsx_cc.Connect();

        ///Create closed loop slew object
        ClosedLoopSlew tsx_cls = new ClosedLoopSlew();

        ///Set the exposure, filter to luminance and reduction, set the camera delay to 0 -- backlash
        /// should be picked up in the mount driver
        tsx_cc.ImageReduction = ccdsoftImageReduction.cdAutoDark;
        tsx_cc.FilterIndexZeroBased = 3; ///Luminance
        tsx_cc.ExposureTime = 10;
        tsx_cc.Delay = 0;

       ///Execute
        try {
            int clsstat = tsx_cls.exec();
        }
        catch {
            ///Just close up: TSX will spawn error window
            MessageBox.Show("Closed Loop Slew failure");
        };

        return;
    }
}
