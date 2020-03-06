using System.Windows.Forms;
using TheSkyXLib;

public class AutoFocus
{
/// Windows C# Sample Console Application: AutoFocus
///
/// ------------------------------------------------------------------------
///
///               Author: R.McAlister (2017)
///
/// ------------------------------------------------------------------------
///
/// This application performs the following steps:
///   Saves the current target and imaging parameters
///   Turns off Autoguiding (if on)
///   Turns on AutoFocus
///   Returns to the current target

/// Note that "Automatically slew telescope to nearest appropriate focus star" must be checked in the @Focus2 start-up window.
///   

    public void AutoFocusSample()
    {
        int iFilter = 3; ///Luminescent, I hope
   
        ///Connect the telescope for some slewing
        sky6RASCOMTele tsx_tt = new sky6RASCOMTele();
        tsx_tt.Connect();

        ///Work around and Run @Focus2
        ///   Save current target name so it can be found again
        ///   Run @Focus2 (which preempts the observating list and object)
        ///   Restore current target, using Name with Find method
        ///   ClosedLoopSlew back to target

        ccdsoftCamera tsx_cc = new ccdsoftCamera();
        tsx_cc.Connect();
        tsx_cc.focConnect();
   
        ///Get current target name so we can return after running @focus2
        sky6ObjectInformation tsx_oi = new sky6ObjectInformation();
        tsx_oi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1);
        string sTargetName = tsx_oi.ObjInfoPropOut;

        ///TBD: Set up parameters for @Focus2, if necessary
        ///   
        ///Run Autofocus
        ///   Create a camera object
        ///   Launch the autofocus watching out for an exception -- which will be posted in TSX

        ///Save current camera delay, exposure and filter
        /// then set the camera delay = 0
        /// set the delay back when done with focusing

        tsx_cc.AutoSaveFocusImages = 0;
        var dCamDelay = tsx_cc.Delay;
        var iCamReduction = tsx_cc.ImageReduction;
        var iCamFilter = tsx_cc.FilterIndexZeroBased;
        var dCamExp = tsx_cc.ExposureTime;
        var iFocStatus = 0;

        tsx_cc.ImageReduction = ccdsoftImageReduction.cdAutoDark;
        tsx_cc.FilterIndexZeroBased = 3; ///Luminance
        tsx_cc.ExposureTime = 10;
        tsx_cc.Delay = 0;
        tsx_cc.FilterIndexZeroBased = iFilter;

        iFocStatus = tsx_cc.AtFocus2();
       
        ///Restore the current target and slew back to it
        ///   Run a Find on the target -- which makes it the "observation"
        ///   Perform Closed Loop Slew to the target

        sky6StarChart tsx_sc = new sky6StarChart();
        tsx_sc.Find(sTargetName);
        ///Run a Closed Loop Slew to return
        ClosedLoopSlew tsx_cls = new ClosedLoopSlew();
        ///Set the exposure, filter to luminance and reduction, set the camera delay to 0 -- any backlash
        /// should be picked up in the mount driver
        tsx_cc.ImageReduction = ccdsoftImageReduction.cdAutoDark;
        tsx_cc.FilterIndexZeroBased = iFilter; ///Luminance, probably
        tsx_cc.ExposureTime = 10;
        tsx_cc.Delay = 0;
        try
        {
            var clsstat = tsx_cls.exec();
        }
        catch
        {
            ///Just close up: TSX will spawn error window
            ///System.Windows.Forms.Show("AutoFocus return failure")
        };
        ///Put back the orginal settings 

        tsx_cc.ImageReduction = iCamReduction;
        tsx_cc.FilterIndexZeroBased = iCamFilter;
        tsx_cc.ExposureTime = dCamExp;
        tsx_cc.Delay = dCamDelay;

        return;
    }
}