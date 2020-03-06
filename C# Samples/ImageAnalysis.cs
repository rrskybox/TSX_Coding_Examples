using System.Windows.Forms;
using TheSkyXLib;

public class ImageAnalysis
{
    /// Windows C# Sample Console Application: ImageAnalysis
    ///
    /// ------------------------------------------------------------------------
    ///
    ///               Author: R.McAlister (2017)
    ///
    /// ------------------------------------------------------------------------
    ///
    /// This application demonstrates some of the functionality of the ccdsoftCamera and Image classes
    ///
    /// The example takes a 60 second exposure then produces a recommendation window to display computed
    ///   optimal exposure length and duration for a one hour shoot, based on average background noise.
    ///
    /// The algorithms are based on work by ... 
    ///   John Smith: http://www.hiddenloft.com/notes/SubExposures.pdf
    ///   Charles Anstey: http://www.cloudynights.com/item.php?item_id=1622
    ///   Steve Cannistra: http://www.starrywonders.com/snr.html
    ///
    /// Note:  Where the required parameters like "gain" are not supplied through TSX, they are arbitrarily set for
    ///   an SBIG STF8300
    ///
    public void ImageAnalysisSample()
    {

        ///Open camera control and connect it to hardware
        ccdsoftCamera tsx_cc = new ccdsoftCamera();
        try {
            tsx_cc.Connect();
        }
        catch {
            MessageBox.Show("Camera Connect Error");
        };
       
        ///turn on autosave
        tsx_cc.AutoSaveOn = 1;
        ///Set for 60 second exposure, light frame with autodark
        tsx_cc.ExposureTime = 60;
        tsx_cc.Frame = ccdsoftImageFrame.cdLight;
        tsx_cc.ImageReduction = ccdsoftImageReduction.cdAutoDark;
        tsx_cc.FilterIndexZeroBased = 3;    ///Assumed Lumescent, but change accordingly
        tsx_cc.Delay = 5;                    ///Possible filter change = 5 sec delay
        tsx_cc.Asynchronous = 1;          ///Going to do a wait loop
        tsx_cc.TakeImage();
        while (tsx_cc.State == ccdsoftCameraState.cdStateTakePicture)
        {
            System.Threading.Thread.Sleep(1000);
        };

        ///Create image object
        ccdsoftImage tsx_im = new ccdsoftImage();
        int imgerr = 0;
        ///Open the active image, if any
        try {
            imgerr = tsx_im.AttachToActive();
        }
        catch {        
            MessageBox.Show("No Image Available:  " + imgerr.ToString());
            return;
        };

        const int totalexp = 60;  ///Minutes for total exposure sequence

        double dGain = 0.37;       ///electrons per ADU for SBIG STF8000M as spec///d
        int iPedestal = 0 ;   ///base pedestal
        double dRnoise = 9.3;     ///read out noise in electrons
        double dNoiseFac  = 0.05;  ///maximum tolerable contribution of readout noise
        double dExpFac = 1;      ///Exposure reduction factor
        double dSLambda  = 15;      ///Faint target ADU minimum
        double dSNRMax  = 0.9;      ///fraction of maximum achievable signal to noise ratio (Cannistra)

        ///Presumably an Image Link has already been performed
        ///Check on this is TBD

        int iPX = tsx_im.FITSKeyword("NAXIS1");
        int  iPY  = tsx_im.FITSKeyword("NAXIS2");
        int  iPXBin  = tsx_im.FITSKeyword("XBINNING");
        int  iPYBin  = tsx_im.FITSKeyword("YBINNING");
        ///Dim igain as integer = tsx_im.FITSKeyword("EGAIN");         ///TSX doesn///t pick this up, yet
        double  dExpTime  = tsx_im.FITSKeyword("EXPTIME");

        iPX = iPX - 1;
        iPY = iPY - 1;

        double dAvgABU = tsx_im.averagePixelValue();
        double dEsky = ((dAvgABU - iPedestal) * dGain) / dExpTime;
        double dTorn = (System.Math.Pow(dRnoise,2) / (((System.Math.Pow((1 + dNoiseFac),2) - 1) * dEsky)));

        ///Smith algorithm
        int iExp1  = (int)(dTorn / 2);
        int iReps1  = (int)((((totalexp * 60) / dTorn) - 1) * 2);
        ///Anstey algorithm
        int iExp2  = (int)((dSLambda * System.Math.Sqrt(totalexp * 60)) / (2 * System.Math.Sqrt(dAvgABU / dExpTime)));
        int iReps2  = (int)((totalexp * 60) / iExp2);
        ///Cannestr algorithm
        int iExp3  = (int)((System.Math.Pow(dSNRMax,2) * System.Math.Pow(dRnoise,2)) / ((dEsky) * (1 - System.Math.Pow(dSNRMax,2))));
        int iReps3  = (int)((totalexp * 60) / iExp3);

        ///Display Results
        MessageBox.Show("Smith Model (at tolerable noise factor = 0.05):" + "\r\n" +
                     "     " + iExp1.ToString() + " second exposure with" + "\r\n" +
                     "     " + iReps1.ToString() + " repetitions per hour." + "\r\n" + "\r\n" +
                     "Anstey Model (at faint target minimum = 15):" + "\r\n" +
                     "     " + iExp2.ToString() + " second exposure with" + "\r\n" +
                     "     " + iReps2.ToString() + " repetitions per hour." + "\r\n" + "\r\n" +
                     "Cannestra Model (at SNR = 90% of maximum):" + "\r\n" +
                     "     " + iExp3.ToString() + " second exposure with" + "\r\n" +
                     "     " + iReps3.ToString() + " repetitions per hour.");

       return;
    }
}
