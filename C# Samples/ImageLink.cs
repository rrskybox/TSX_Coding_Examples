using System.Windows.Forms;
using TheSkyXLib;

public class ImageLinker

    /// Windows C# Sample Console Application: ImageLink
    ///
    /// ------------------------------------------------------------------------
    ///               Vaguely adapted from ImageLink.vbs  (Visual Basic Script)
    ///               Copyright (C) Software Bisque (2013)
    ///
    ///				Converted 2017, R.McAlister
    ///
    /// ------------------------------------------------------------------------
    ///
    /// This C# console application demonstrates how to perform an image link on a photo.
    ///  The path to the FITS image is set to a user defined location.  In this case, I used
    ///   C:\Users\Rick\Documents\Software Bisque\TheSkyX Professional Edition\Camera AutoSave\Temp
    ///
    /// The application executes an ImageLink on the target file, then prints the RA/Dec coordinates.
    ///
    ///FITS pathname

{
    const string PathName = "C:\\Users\\Rick\\Documents\\Software Bisque\\TheSkyX Professional Edition\\Camera AutoSave\\Temp\\temp.fit";

    ///Global Parameters
    const double dExposure  = 3.0;  ///seconds
    const int ifilter  = 3;  ///4nd filter slot, probably clear/lumenscent

    public void ImageLinkerSample()
    {
        ///Create camera object and connect
        
        ImageLink tsx_il = new ImageLink();
        ImageLinkResults tsx_ilr = new ImageLinkResults();
        
        ///Set path to file
        tsx_il.pathToFITS = PathName;

        ///Run ImageLink
        tsx_il.execute();

        ///Check on result
        if (tsx_ilr.succeeded == 0)
        {
            MessageBox.Show("Error: " + tsx_ilr.errorCode.ToString() + "  " + tsx_ilr.errorText);
            return;
        }

        ///Print the image center location
      
        MessageBox.Show("RA: " + tsx_ilr.imageCenterRAJ2000.ToString() + "  Dec: " + tsx_ilr.imageCenterDecJ2000.ToString());
        return;

        }

  }