Module StarChartSetWW
    ' Windows Visual Basic Sample Console Application: StarChartSetWW
    '
    ' ------------------------------------------------------------------------
    '               Adapted from TheSkySetWW.vbs  (Visual Basic Script)
    '               Copyright (C) Software Bisque (2009?)
    '
    '				Converted 2014, R.McAlister
    '
    ' ------------------------------------------------------------------------
    '
    ' This Visual Basic console application exercises the Star Chart class functions for setting 
    '   Timne, Time Zone, Location, etc.
    '
    Sub Main()

        'Welcome Window
        If Not Welcome() Then
            Return
        End If

        Dim dJD As Double = 2452066.0 '=06/05/2001, ignored if UseCompterClock=1
        Dim dTZ As Double = 7         'MST
        Dim dElev As Double = 1000      'meters
        Dim bUseComputerClock = 1         '1=Yes 0=No
        Dim dLat As Double = 39.5
        Dim dLong As Double = 105.5
        Dim szLoc As String = "Location from script"

        'Create Telescope Object and disconnect telescope -- time changes are not enabled with scope connected
        Dim tsx_ts = CreateObject("TheSkyX.Sky6RASCOMTele")
        tsx_ts.Disconnect()

        'Create the Star Chart object
        Dim tsx_sc = CreateObject("TheSkyX.Sky6StarChart")

        'Turn off the Computer Clock
        tsx_sc.SetDocumentProperty(theskyxLib.Sk6DocumentProperty.sk6DocProp_UseComputerClock, 0)

        'Change the julian date
        tsx_sc.SetDocumentProperty(theskyxLib.Sk6DocumentProperty.sk6DocProp_JulianDateNow, dJD)

        'Change the time zone
        tsx_sc.SetDocumentProperty(theskyxLib.Sk6DocumentProperty.sk6DocProp_Time_Zone, dTZ)

        'Change the elevation
        tsx_sc.SetDocumentProperty(theskyxLib.Sk6DocumentProperty.sk6DocProp_ElevationInMeters, dElev)

        'Change the latitude
        tsx_sc.SetDocumentProperty(theskyxLib.Sk6DocumentProperty.sk6DocProp_Latitude, dLat)

        'Change the longitude
        tsx_sc.SetDocumentProperty(theskyxLib.Sk6DocumentProperty.sk6DocProp_Longitude, dLong)

        'Change the location description
        tsx_sc.SetDocumentProperty(theskyxLib.Sk6DocumentProperty.sk6DocProp_LocationDescription, szLoc)

        'Clean Up
        tsx_sc = Nothing

    End Sub

    Function Welcome() As Boolean

        ' Returns True if user wants to continue, False is abort

        Dim intDoIt = MsgBox("This script demonstrates how to set the the Star Chart time and location",
                             vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")

        If intDoIt = vbCancel Then
            Return (False)
        End If

        Return (True)
    End Function
End Module
