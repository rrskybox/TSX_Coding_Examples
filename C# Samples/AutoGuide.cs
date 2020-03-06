using System.Windows.Forms;
using TheSkyXLib;

public class AutoGuide
{

/// Windows C# Sample Console Application: AutoGuide
///
/// ------------------------------------------------------------------------
///
///               Author: R.McAlister (2017)
///
/// ------------------------------------------------------------------------
///
/// This application performs the following steps:
///   Finds a target, M39 in this case
///   Turns off Autoguiding (if on)
///   Performs a Closed Loop Slew to the target
///   Takes an image with the Autoguide camera
///   Turns on Autoguiding
///   
///
 
    public void AutoGuideSample()
        {
      ///Set connection to star chart and perform a find on M39 -- this will set the target for a CLS
    sky6StarChart tsx_sc = new sky6StarChart();
    tsx_sc.Find("M39");

    ///Create connection to autoguide camera and connect
    ccdsoftCamera tsx_ag =new ccdsoftCamera();
    ///Attach this object to the guider camera
    tsx_ag.Autoguider = 1;
    ///Find out with the guide camera is up to.  Abort if autoguiding or calibrating
    if (tsx_ag.Connect() == 0)
        {
        if ((tsx_ag.State == ccdsoftCameraState.cdStateAutoGuide) | (tsx_ag.State == ccdsoftCameraState.cdStateCalibrate))
                {
                    tsx_ag.Abort();
                };
        };
    
    ///Create closed loop slew object
    ClosedLoopSlew tsx_cls = new ClosedLoopSlew();

    ///Set the exposure, filter to luminance and reduction, set the camera delay to 0 -- backlash
    /// should be picked up in the mount driver
    ccdsoftCamera tsx_cc = new ccdsoftCamera();
    tsx_cc.ImageReduction = ccdsoftImageReduction.cdAutoDark;
    tsx_cc.FilterIndexZeroBased = 3; ///Luminance
    tsx_cc.ExposureTime = 10;
    tsx_cc.Delay = 0;
    
    ///run CLS synchronously
    ///tsx_cls.Asynchronous = False  ///-- this setting doesn///t appear to work (member not found) as of DB8458

    ///Execute
    try
    {
        int clsstat = tsx_cls.exec();
    }
    catch
    {        ///Just close up: TSX will spawn error window
       MessageBox.Show("Closed Loop Slew error");
       return;
        ///Let it go for demo purposes
    };

    ///Connect AutoGuide camera in case it isn///t
    try
    {
        tsx_ag.Connect();
    }
    catch
    {
        MessageBox.Show("Guide camera connect error");
        return;
    };
    ///Take an image to use for Autoguiding, run the function synchronously
    tsx_ag.ExposureTime = 2;
    tsx_ag.Asynchronous = 0;
    tsx_ag.Subframe = 0;
    var tstat = tsx_ag.TakeImage(); ///Just assume it works

    ///Turn asynchronous back on to get out of this
    tsx_ag.Asynchronous = 1;
    ///Fire off Autoguiding
    tsx_ag.Autoguide();
        return;
    }
}