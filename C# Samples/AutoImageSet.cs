using System.Windows.Forms;
using TheSkyXLib;

public class AutoImageSet
{
    /// Windows C# Sample Console Application: AutoImageSet
    ///
    /// ------------------------------------------------------------------------
    ///
    ///               Author: R.McAlister (2017)
    ///
    /// ------------------------------------------------------------------------
    ///
    /// Example program that shows how to read (and by inference) how to set AutomatedImageLinkSettings
    ///
    ///
    public void AutoImageSetSample()
    {
          
        ///Open Automated Image Link Settings object
        AutomatedImageLinkSettings tsx_ails = new AutomatedImageLinkSettings();

        ///This property holds the image scale. 
        double dIScale = tsx_ails.imageScale;

        ///This property holds the position angle.
        double dPA = tsx_ails.positionAngle;

        ///This property holds the exposure time for Closed Loop Slew and T-point runs
        double dExposure = tsx_ails.exposureTimeAILS;

        ///This property holds the number of field of views to search while Image Linking
        int iFOVSearch = tsx_ails.fovsToSearch;

        ///This property holds the number of ImageLink retries upon failure.
        int iRetry = tsx_ails.retries;

        MessageBox.Show("Automated Image Link Settings:" + "\r\n" + "\r\n" +
               "Scale: " + dIScale.ToString() + "\r\n" +
               "Position Angle: " + dPA.ToString() + "\r\n" +
               "Exposure: " + dExposure.ToString() + "\r\n" +
               "FOVs to Search: " + iFOVSearch.ToString() + "\r\n" +
               "Retries: " + iRetry.ToString());
    }

}
