using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using TheSkyXLib;

public class AutomatedSearch
{

    ///
    ///
    /// ------------------------------------------------------------------------
    ///               Adapted from AutomatedSearch.vbs  (Visual Basic Script)
    ///               Copyright (C) Software Bisque (2009?)
    ///
    ///				Converted 2017, R.McAlister
    ///
    /// ------------------------------------------------------------------------
    ///
    /// This C# console application reads an Exported Observing List file for targets
    ///   For each target, the telescope is slewed to the target and an image taken.
    /// This example is essentially identical to AutoMap, except that an image is taken and saved
    ///	and no AutoMap performed.
    ///
    ///Use TheSky to generate a text file of mapping points

    ///Set the exposure time for the image
    public const double dExposure  = 1.0;

    public void AutomatedSearchSample()
    {

        /// ********************************************************************************
        /// *
        /// * Below is the flow of program execution
        /// * See the subroutine TargetLoop to see where the real work is done

       ///Create Objects

        sky6RASCOMTele tsx_tele = new sky6RASCOMTele();
        ccdsoftCamera tsx_cam = new ccdsoftCamera();

        ///Connect Objects
        try {
            tsx_tele.Connect();
        }
        catch {
            MessageBox.Show("Telescope Connect Error");
            return;;
        };

        try {
            tsx_cam.Connect();
        }
        catch {
            MessageBox.Show("Camera Connection Error");
            return;;
        }

        ///Run the target loop
        TargetLoop();

        ///Disconnect objects
        tsx_tele.Disconnect();
        tsx_cam.Disconnect();

         }

    /// ********************************************************************************
    /// *
    /// * Below are all the subroutines used in this sample
    /// *

   static void TargetLoop()
    {
        string szPathToMapFile  = "C:\\Users\\Rick\\Documents\\Software Bisque\\TheSkyX Professional Edition\\Exported Data\\map.txt";
      
        string LineFromFile;
        double dAz;
        double dAlt;

        //Connect to TSX TheSky and Uility methods
        sky6Utils tsx_util = new sky6Utils();
        sky6RASCOMTheSky tsx_sky = new sky6RASCOMTheSky();
        sky6RASCOMTele tsx_tele = new sky6RASCOMTele();
        ccdsoftCamera tsx_cam = new ccdsoftCamera();

        ///Open the observing list export file for targets
        StreamReader MyFile = File.OpenText(szPathToMapFile); ///Stream object for Export Data text file

        ///Get the first line -- headers
        ///Exit if the file is empty
        if (MyFile.EndOfStream == true) {
            return;
        };
        LineFromFile = MyFile.ReadLine();

        int iRAindex = LineFromFile.IndexOf("RA");
        int iDecindex = LineFromFile.IndexOf("Dec");

        while (MyFile.EndOfStream == false) {

            LineFromFile = MyFile.ReadLine();
            MessageBox.Show("RA: " + LineFromFile.Substring(iRAindex, 13) + "  Dec: " + LineFromFile.Substring(iDecindex, 13));

            string sname = LineFromFile.Substring((LineFromFile.Length-12), 12);
            tsx_util.ConvertStringToRA(LineFromFile.Substring(iRAindex, 13));
            dAz = tsx_util.dOut0;
            tsx_util.ConvertStringToDec(LineFromFile.Substring(iDecindex, 13));
            dAlt = tsx_util.dOut0;

            ///Slew to object
            tsx_tele.SlewToAzAlt(dAz, dAlt, sname);
         
            ///Set exposure time and try { for image, exit if error
            tsx_cam.ExposureTime = dExposure;
            tsx_cam.TakeImage();
        }; //Loop
    }

    }