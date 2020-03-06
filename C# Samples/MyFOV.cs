using System.Windows.Forms;
using TheSkyXLib;

public class MyFOV
{
    /// Windows C# Sample Console Application: MyFOV
    ///
    /// ------------------------------------------------------------------------
    ///
    ///               Author: R.McAlister (2017)
    ///
    /// ------------------------------------------------------------------------
    ///
    /// This application demonstrates some of the functionality of the MyFOV class
    ///  
    ///
    ///Globals
    
    public void MyFOVSample()
    {
        ///Which FOV definition by index
        int iFOV = 0;            ///First FOV definition
        
        ///Create FOV object
        sky6MyFOVs tsx_fov = new sky6MyFOVs();
        sky6StarChart tsx_sc = new sky6StarChart();

        ///First FOV definition, additional definitions at successive indicies
        ///Get FOV name and Postition Angle
        tsx_fov.Name(iFOV);
        string fovname = tsx_fov.OutString;
        tsx_fov.Property(fovname, 0, sk6MyFOVProperty.sk6MyFOVProp_PositionAngleDegrees);
        double fovPA  = tsx_fov.OutVar;
        MessageBox.Show("FOV Name: " + fovname + "  FOV PA: " + fovPA.ToString());

        return;
    }
}
