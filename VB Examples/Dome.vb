Module Dome

    ' Windows Visual Basic Sample Console Application: Dome
    '
    ' ------------------------------------------------------------------------
    '               Adapted from Dome.vbs  (Visual Basic Script)
    '               Copyright (C) Software Bisque (2013)
    '
    '				Converted 2015, R.McAlister
    '
    ' ------------------------------------------------------------------------
    '
    ' This Visual Basic console application demonstrates how to access dome control.
    '
    '  Note:  The Dome add-on was not available for testing this script. Use at your own risk.
    '

    Sub main()

        Dim instat = Welcome()
        If Not instat Then
            Return
        End If

        'Create the dome object, connect and get it's targeting
        Dim tsx_do = CreateObject("TheSkyX.Sky6Dome")
        tsx_do.Connect()
        tsx_do.GetAzAl()

        Dim DomeAz = tsx_do.dAz
        Dim DomeEl = tsx_do.dEl

        MsgBox("Dome Azimuth at: " + Str(DomeAz) + "  Dome Elevation at: " + Str(DomeEl))
        MsgBox("Moving to Azimuth 0 degrees, Elevation 0 Degrees.")

        'Target the dome at az = 0, alt = 0
        tsx_do.GoToAzEl(0, 0)

        'Disconnect the dome
        tsx_do.Disconnect()

        'Clean up
        tsx_do = Nothing

        Return

    End Sub


    Function Welcome() As Boolean

        ' Returns True if user wants to continue, False is abort

        Dim intDoIt = MsgBox("This script demonstrates how to access dome control using the Windows Visual Basic.",
                             vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return (False)
        End If
        Return (True)
    End Function


End Module