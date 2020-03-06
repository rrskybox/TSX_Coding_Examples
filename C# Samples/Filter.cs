using System.Windows.Forms;
using TheSkyXLib;

public class Filter
{

    /// Windows C# Sample Console Application: Filter
    ///
    /// ------------------------------------------------------------------------
    ///               Vaguely adapted from Filter.vbs  (Visual Basic Script)
    ///               Copyright (C) Software Bisque (2009?)
    ///
    ///				Converted 2017, R.McAlister
    ///
    /// ------------------------------------------------------------------------
    ///
    /// This C# console application demonstrates how to connect, change and disconnect a filter.  Along the way, most o
    ///  Along the way, most of the basic camera operating methods are also demonstrated.
    ///
   

    public void FilterSample()
   {
   ///Global Parameters
        double dExposure  = 10.0;///seconds
        int ifilter  = 3;  ///4nd filter slot, probably clear/lumenscent      ///Create camera object and connect
        
        ccdsoftCamera tsx_cc = new ccdsoftCamera();
        tsx_cc.Connect();

        ///Set exposure length
        tsx_cc.ExposureTime = dExposure;

        ///Set frame type to Light frame
        tsx_cc.Frame = ccdsoftImageFrame.cdLight;

        ///Set filter
        tsx_cc.FilterIndexZeroBased = ifilter;

        ///Set preexposure delay
        tsx_cc.Delay = 5;  ///Possible filter change = 5 sec delay

        ///Set method type to Asynchronous (so we can demo the wait process)
        tsx_cc.Asynchronous = 0;

        ///Set for autodark
        tsx_cc.ImageReduction = ccdsoftImageReduction.cdAutoDark;

        ///Take image
        tsx_cc.TakeImage();

        ///Wait for completion (unnecessary if Asynchronous is set to "False"
        while (tsx_cc.State == ccdsoftCameraState.cdStateTakePicture)
        {
            System.Threading.Thread.Sleep(1000);
        };
       
               ///Clean up
        tsx_cc.Disconnect();
        return;

        }
}
