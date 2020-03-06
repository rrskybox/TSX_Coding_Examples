Module ListSearch
    ' Windows Visual Basic Sample Console Application: ListSearch
    '
    ' ------------------------------------------------------------------------
    '               Vaguely adapted from ErrorHandling.vbs  (Visual Basic Script)
    '               Copyright (C) Software Bisque (2013)
    '
    '				Converted 2015, R.McAlister
    '
    ' ------------------------------------------------------------------------
    '
    ' This Visual Basic console application demonstrates how to run the telescope through a list of targets as defined
    '   by a list of names.
    '
    '  Note:  The gist of the orginal VBS script was entitled "ErrorHandling.vbs".  However, that
    '  script, however labeled, performed the functions as adapted to VB herein.
    '

    'If you want your script to run all night regardless of errors, set bIgnoreErrors = True
    Public bIgnoreErrors As Boolean = False

    'Set the exposure time for the image
    Public dExposure As Double = 1.0

    'Global TSX COM Objects
    Public objChrt As Object
    Public objTele As Object
    Public objCam As Object
    Public objUtil As Object
    Public objInfo As Object

    'Target List

    Public targetlist() = {
                  "NGC1348",
                  "NGC1491",
                  "NGC1708",
                  "NGC179",
                  "NGC1798",
                  "NGC2165",
                  "NGC2334",
                  "NGC2436",
                  "NGC2519",
                  "NGC2605",
                  "NGC2689",
                  "NGC2666",
                  "NGC4381",
                  "NGC5785",
                  "NGC5804",
                  "NGC6895",
                  "NGC6991",
                  "NGC7011",
                  "NGC7058",
                  "M39",
                  "NGC7071",
                  "NGC7150",
                  "NGC7295",
                  "NGC7394",
                  "NGC7686",
                  "NGC7801"}

    Sub Main()

        'Welcome Window
        If Not Welcome() Then
            Return
        End If

        'Create objects
        objChrt = CreateObject("TheSkyX.Sky6StarChart")
        objTele = CreateObject("TheSkyX.Sky6RASCOMTele")
        objCam = CreateObject("TheSkyX.ccdSoftCamera")
        objUtil = CreateObject("TheSkyX.Sky6Utils")
        objInfo = CreateObject("TheSkyX.Sky6ObjectInformation")

        'Connect Objects
        objTele.Connect()
        objCam.Connect()


        'Run loop over array of target names
        Dim dAz As Double
        Dim dAlt As Double
        Dim iError As Boolean

        For Each target In targetlist
            objChrt.Find(target)
            objInfo.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_ALT)
            dAlt = objInfo.ObjInfoPropOut
            objInfo.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_AZM)
            dAz = objInfo.ObjInfoPropOut
            Try
                objTele.SlewToAzAlt(dAz, dAlt, target)
            Catch ex As Exception
                If Not bIgnoreErrors Then
                    iError = MsgBox("An error has occurred running slew: " + ex.Message +
                                          vbCrLf + vbCrLf + "Exit Script?",
                                          vbYesNo + vbInformation)
                    If iError = vbYes Then
                        Exit For
                    End If
                End If
            End Try
            'Set exposure time and try for image, exit if error
            objCam.ExposureTime = dExposure
            Try
                objCam.TakeImage()
            Catch ex As Exception
                If Not bIgnoreErrors Then
                    iError = MsgBox("An error has occurred running image:  " + ex.Message +
                                      vbCrLf + vbCrLf + "Exit Script?",
                                      vbYesNo + vbInformation)
                    If iError = vbYes Then
                        Exit For
                    End If
                End If
            End Try

        Next


        'Disconnect telescope and camera
        objTele.Disconnect()
        objCam.Disconnect()

        'Clean up
        objChrt = Nothing
        objTele = Nothing
        objCam = Nothing
        objUtil = Nothing

        Return

    End Sub

    Function Welcome() As Boolean

        ' Returns True if user wants to continue, False is abort

        Dim intDoIt = MsgBox("This script demonstrates how to acquire images from a list of target names." + vbCrLf + vbCrLf,
                            vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return (False)
        End If

        Return (True)
    End Function

End Module

