Module Tele

    ' Windows Visual Basic Sample Console Application: Tele
    '
    ' ------------------------------------------------------------------------
    '               Adapted from Tele.vbs  (Visual Basic Script)
    '               Copyright (C) Software Bisque (2009?)
    '
    '				Converted 2014, R.McAlister
    '
    ' ------------------------------------------------------------------------
    '
    ' This Visual Basic console application exercises the telescope class functions
    ' 
    'Global settings

    'Use TheSky to generate a text file of mapping points
    Public szPathToMapFile As String = "C:\Users\Rick\Documents\Software Bisque\TheSkyX Professional Edition\Exported Data\map.txt"

    'If you want your script to run all night regardless of errors, set bIgnoreErrors = True
    Public bIgnoreErrors As Boolean = False

    'Set the exposure time for the image
    Public dExposure As Double = 1.0

    'Global TSX COM Objects
    Public objTele As Object

    Sub Main()

        'Welcome Window
        If Not Welcome() Then
            Return
        End If

        'Create the telescope object
        Dim tsx_ts = CreateObject("TheSkyX.Sky6RASCOMTele")

        'Connect to the telescope
        tsx_ts.Connect()

        'See if connection failed
        If (tsx_ts.IsConnected = 0) Then
            MsgBox("Connection failed.")
            Return
        End If

        'Get and show the current telescope ra, dec
        tsx_ts.GetRaDec()
        MsgBox("Ra,Dec =" + Str(tsx_ts.dRa) + ",  " + Str(tsx_ts.dDec))

        'Get and show the current telescope az, alt
        tsx_ts.GetAzAlt()
        MsgBox("Az,Alt=" + CStr(tsx_ts.dAz) + ",  " + CStr(tsx_ts.dAlt))

        'Goto an arbitrary RA and Dec
        tsx_ts.SlewToRaDec(2.0, 3.0, "Home")
        MsgBox("GotoComplete")

        'Sync on an ra dec
        Call tsx_ts.Sync(3.0, 3.0, "Matt")

        'Disconnect the telscope
        tsx_ts.Disconnect()

        'Clean up
        tsx_ts = Nothing
    End Sub

    Function Welcome() As Boolean

        ' Returns True if user wants to continue, False is abort

        Dim intDoIt = MsgBox("This script demonstrates how to automate the telescope",
                             vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")

        If intDoIt = vbCancel Then
            Return (False)
        End If

        Return (True)
    End Function
End Module
