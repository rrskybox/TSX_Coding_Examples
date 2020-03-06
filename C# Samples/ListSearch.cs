using System.Windows.Forms;
using TheSkyXLib;

public class ListSearch
{
    /// Windows C# Sample Console Application: ListSearch
    ///
    /// ------------------------------------------------------------------------
    ///               Vaguely adapted from ErrorHandling.vbs  (Visual Basic Script)
    ///               Copyright (C) Software Bisque (2013)
    ///
    ///				Converted 2015, R.McAlister
    ///
    /// ------------------------------------------------------------------------
    ///
    /// This C# console application demonstrates how to run the telescope through a list of targets as defined
    ///   by a list of names.
    ///
    ///  Note:  The gist of the orginal VBS script was entitled "ErrorHandling.vbs".  However, that
    ///  script, however labeled, performed the functions as adapted to VB herein.
    ///


    public void ListSearchSample()
    {
        ///Set the exposure time for the image
        double dExposure = 1.0;
        ///Target List
        string[] targetlist = new string[] {
                  "NGC1348",
                  "NGC1491",
                  "NGC1708",
                  "NGC179",
                  "NGC1798",
                  "NGC2165",
                  "NGC2334",
                  "NGC2436",
                  "NGC2519",
                  "NGC2605",
                  "NGC2689",
                  "NGC2666",
                  "NGC4381",
                  "NGC5785",
                  "NGC5804",
                  "NGC6895",
                  "NGC6991",
                  "NGC7011",
                  "NGC7058",
                  "M39",
                  "NGC7071",
                  "NGC7150",
                  "NGC7295",
                  "NGC7394",
                  "NGC7686",
                  "NGC7801"};

        ///Create objects 

        sky6StarChart objChrt = new sky6StarChart();
        sky6RASCOMTele objTele = new sky6RASCOMTele();
        ccdsoftCamera objCam = new ccdsoftCamera();
        sky6Utils objUtil = new sky6Utils();
        sky6ObjectInformation objInfo = new sky6ObjectInformation();

        ///Connect Objects
        objTele.Connect();
        objCam.Connect();

        ///Run loop over array of target names
        double dAlt;
        double dAz;
        bool iError;

        foreach (string target in targetlist)
        {
            objChrt.Find(target);
            objInfo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_ALT);
            dAlt = objInfo.ObjInfoPropOut;
            objInfo.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_AZM);
            dAz = objInfo.ObjInfoPropOut;

            try
            {
                objTele.SlewToAzAlt(dAz, dAlt, target);
            }
            catch
            {
                MessageBox.Show("An error has occurred running slew");
                return;
            };

            ///Set exposure time and try for image, exit if error
            objCam.ExposureTime = dExposure;
            try
            {
                objCam.TakeImage();
            }
            catch
            {
                MessageBox.Show("An error has occurred running image");
            };
        }

        ///Disconnect telescope and camera
        objTele.Disconnect();
        objCam.Disconnect();
        return;
    }
}
            