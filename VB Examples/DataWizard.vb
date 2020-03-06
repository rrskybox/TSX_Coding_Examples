Module DataWizard

    ' Windows Visual Basic Sample Console Application: DataWizard
    '
    ' ------------------------------------------------------------------------
    '                          
    '               Authored:  R.McAlister 2015
    '               Updated: R.McAlister 2016
    '
    ' ------------------------------------------------------------------------
    '
    'This Visual Basic console application demonstrates the usage of the Data Wizard and Object Information classes.
    '
    'The application opens and runs a built-in Observing List search then accesses a few of the return properties,
    '    for no particularly good reason.
    '
    'The application uses the standard query "Bright objects visible now.dbq"
    '
    Sub Main()

        Dim sname As String = Nothing
        Dim sRA As String
        Dim sDec As String
        Dim intDoIt = MsgBox("This script demonstrates how to use the Data Wizard class." + vbCrLf + vbCrLf,
                       vbOKCancel + vbInformation,
                        "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return
        End If

        'Create object information and datawizard objects
        Dim tsx_dw = CreateObject("TheSkyX.Sky6DataWizard")
        Dim tsx_oi = CreateObject("TheSkyX.Sky6ObjectInformation")
        'Set query path to a user-defined test query database for this example.  If you don't have it, make it.  Best if the Messier catalog is selected.
        tsx_dw.Path = "C:\Users\Rick\Documents\Software Bisque\TheSkyX Professional Edition\Database Queries\Bright objects visible now.dbq"
        tsx_dw.Open()
        tsx_oi = tsx_dw.RunQuery()
        '
        'tsx_oi is an array (tsx_oi.Count) of object information indexed by the tsx_oi.Index property
        '
        'For each object information in the list, get the name, perform a "Find" and look for the catalog ID.  If there is one, print it.
        For i = 0 To tsx_oi.Count - 1
            tsx_oi.Index = i
            tsx_oi.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1)
            sname = tsx_oi.ObjInfoPropOut
            tsx_oi.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_2000)
            sRA = tsx_oi.ObjInfoPropOut
            tsx_oi.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_2000)
            sDec = tsx_oi.ObjInfoPropOut
            MsgBox("Name: " + sname + "   RA: " + sRA + "  Dec:  " + sDec)
        Next
        'Clean up and return
        tsx_dw = Nothing
        tsx_oi = Nothing
        Return

    End Sub

End Module
