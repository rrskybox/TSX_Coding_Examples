using System.Windows.Forms;
using TheSkyXLib;

public class DataWizard
{

    /// Windows C# Sample Console Application: DataWizard
    ///
    /// ------------------------------------------------------------------------
    ///                          
    ///               Authored:  R.McAlister 2017
    ///           
    ///
    /// ------------------------------------------------------------------------
    ///
    ///This C# console application demonstrates the usage of the Data Wizard and Object Information classes.
    ///
    ///The application opens and runs a built-in Observing List search then accesses a few of the return properties,
    ///    for no particularly good reason.
    ///
    ///The application uses the standard query "Bright objects visible now.dbq"
    ///
    public void DataWizardSample()
    {
        string sname = "";
        string sRA;
        string sDec;
      
        ///Create object information and datawizard objects
        sky6DataWizard tsx_dw = new sky6DataWizard();
        sky6ObjectInformation tsx_oi = new sky6ObjectInformation();

        ///Set query path to a user-defined test query database for this example.  If you don///t have it, make it.  Best if the Messier catalog is selected.
        tsx_dw.Path = "C:\\Users\\Rick\\Documents\\Software Bisque\\TheSkyX Professional Edition\\Database Queries\\Bright objects visible now.dbq";
        tsx_dw.Open();
        tsx_oi = tsx_dw.RunQuery;
        ///
        ///tsx_oi is an array (tsx_oi.Count) of object information indexed by the tsx_oi.Index property
        ///
        ///For each object information in the list, get the name, perform a "Find" and look for the catalog ID.  If there is one, print it.
        for (int i = 0; i <= (tsx_oi.Count - 1); i++ ) 
        {
            tsx_oi.Index = i;
            tsx_oi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1);
            sname = tsx_oi.ObjInfoPropOut;
            tsx_oi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_2000);
            sRA = tsx_oi.ObjInfoPropOut.ToString();
            tsx_oi.Property(Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_2000);
            sDec = tsx_oi.ObjInfoPropOut.ToString();
            MessageBox.Show("Name: " + sname + "   RA: " + sRA + "  Dec:  " + sDec);
        }
     return;
    }
}
