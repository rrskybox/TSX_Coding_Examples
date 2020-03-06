using System.Windows.Forms;
using TheSkyXLib;

public class Camera
{

    /// Windows C# Sample Console Application: Cam
    ///
    /// ------------------------------------------------------------------------
    ///               Adapted from Cam.vbs  (Visual Basic Script)
    ///               Copyright (C) Software Bisque (2013)
    ///
    ///				Converted 2017, R.McAlister
    ///
    /// ------------------------------------------------------------------------
    ///
    /// This C# console application demonstrates the key elements of how to take images using the primary camera.

    public void CameraSample()
    {
        int iCamStatus;

        ///Create a TSX camera object
        ccdsoftCamera tsx_cam = new ccdsoftCamera();

        ///Connect TSX to the camera
        try {
            tsx_cam.Connect();
        }
        catch {
            MessageBox.Show("Camera Error");
            return;
        };


        ///Set the exposure time
        tsx_cam.ExposureTime = 15.0;
        ///SEt an exposure delay
        tsx_cam.Delay = 5.0;
        ///Set a frame type
        tsx_cam.Frame = ccdsoftImageFrame.cdLight;
        ///Set for autodark
        tsx_cam.ImageReduction = ccdsoftImageReduction.cdAutoDark;
        ///Set for synchronous imaging (this app will wait until done or error)
        tsx_cam.Asynchronous = 0;
        ///Take image
        iCamStatus = tsx_cam.TakeImage();
        if (iCamStatus != 0) {
            MessageBox.Show("Camera Error: " + iCamStatus.ToString());
        };

        ///Disconnect Camera
        tsx_cam.Disconnect();

    }


}