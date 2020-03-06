Module TheStarChart

    ' Windows Visual Basic Sample Console Application: TheStarChart
    '
    ' ------------------------------------------------------------------------
    '               Adapted from TheSky.vbs  (Visual Basic Script)
    '               Copyright (C) Software Bisque (2009?)
    '
    '				Converted 2014, R.McAlister
    '
    ' ------------------------------------------------------------------------
    '
    ' This Visual Basic console application exercises the Star Chart class methods.
    '    These methods supercede the depreciated TheSky class.
    ' 
    'Global settings

    'Use TheSky to generate a text file of mapping points
    Public szPathToMapFile As String = "C:\Users\Rick\Documents\Software Bisque\TheSkyX Professional Edition\Exported Data\map.txt"

    'If you want your script to run all night regardless of errors, set bIgnoreErrors = True
    Public bIgnoreErrors As Boolean = False

    'Set the exposure time for the image
    Public dExposure As Double = 1.0

    'Global TSX COM Objects
    Public tsx_sc As Object
    Public tsx_oi As Object

    Sub Main()

        'Welcome Window
        If Not Welcome() Then
            Return
        End If

        'Open Star Chart object 
        tsx_sc = CreateObject("TheSkyX.Sky6StarChart")

        'Create Object Information object
        tsx_oi = CreateObject("TheSkyX.Sky6ObjectInformation")

        'Find the target 
        Dim sname As String = "M51"
        tsx_sc.Find(sname)

        'Acquire and display the RA and Dec of the target
        tsx_oi.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_RA_2000)
        Dim dRA As Double = tsx_oi.ObjInfoPropOut
        tsx_oi.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_DEC_2000)
        Dim dDec As Double = tsx_oi.ObjInfoPropOut
        MsgBox(sname + " RA = " + Str(dRA) + " Declination = " + Str(dDec))

        'Acquire and display the Azimuth and Altitude of the target
        tsx_oi.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_AZM)
        Dim dAz As Double = tsx_oi.ObjInfoPropOut
        tsx_oi.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_ALT)
        Dim dAlt As Double = tsx_oi.ObjInfoPropOut
        MsgBox(sname + " Azimuth = " + Str(dAz) + " Altitude = " + Str(dAlt))

        'Acquire and display the RA, Dec, FOV and Rotation of the screen
        MsgBox("ScreenRa=" + Str(tsx_sc.RightAscension))
        MsgBox("ScreenDec=" + Str(tsx_sc.Declination))
        MsgBox("ScreenFOV =" + Str(tsx_sc.FieldOfView))
        MsgBox("ScreenRotation =" + Str(tsx_sc.Rotation))

        'Set Star Chart FOV to 10
        tsx_sc.FieldOfView = 10.0

        'Set Star Chart Rotation to 300 degrees
        tsx_sc.Rotation = 300.0

        'Clean Up
        tsx_sc = Nothing
        tsx_oi = Nothing

	Return

    End Sub

    Function Welcome() As Boolean

        ' Returns True if user wants to continue, False is abort

        Dim intDoIt = MsgBox("This script demonstrates some Star Chart methods",
                             vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")

        If intDoIt = vbCancel Then
            Return (False)
        End If

        Return (True)
    End Function
End Module
