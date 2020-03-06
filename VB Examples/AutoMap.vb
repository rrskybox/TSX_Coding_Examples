Module AutoMap

    ' Windows Visual Basic Sample Console Application: AutoMap
    '
    ' ------------------------------------------------------------------------
    '               Adapted from AutoMap.vbs  (Visual Basic Script)
    '               Copyright (C) Software Bisque (2009?)
    '
    '				Converted 2014, R.McAlister
    '
    ' ------------------------------------------------------------------------
    '
    ' This Visual Basic console application reads an Exported Observing List file for targets
    '   For each target, the telescope is slewed to the target and an image taken
    '	The target is then AutoMap'd into the T-Point Model.
    '
    'Global settings

    'Use TheSky to generate a text file of mapping points
    Public szPathToMapFile As String = "C:\Users\Rick\Documents\Software Bisque\TheSkyX Professional Edition\Exported Data\map.txt"

    'If you want your script to run all night regardless of errors, set bIgnoreErrors = True
    Public bIgnoreErrors As Boolean = False

    'Set the exposure time for the image
    Public dExposure As Double = 1.0

    'Global TSX COM Objects
    Public tsx_sky As Object
    Public tsx_tele As Object
    Public tsx_cam As Object
    Public tsx_util As Object

    Sub Main()

        ' ********************************************************************************
        ' *
        ' * Below is the flow of program execution
        ' * See the subroutine TargetLoop to see where the real work is done

        If Not welcome() Then
            Return
        End If

        'Create Objects

        tsx_sky = CreateObject("TheSkyX.Sky6RASCOMTheSky")
        tsx_tele = CreateObject("TheSkyX.Sky6RASCOMTele")
        tsx_cam = CreateObject("TheSkyX.ccdSoftCamera")
        tsx_util = CreateObject("TheSkyX.Sky6Utils")

        'Connect Objects
        Try
            tsx_sky.Connect()
        Catch ex As Exception
            MsgBox("Sky Connect Error: " + ex.Message)
            Exit Sub
        End Try
        Try
            tsx_tele.Connect()
        Catch ex As Exception
            MsgBox("Telescope Connect Error" + ex.Message)
            Exit Sub
        End Try
        Try
            tsx_cam.Connect()
        Catch ex As Exception
            MsgBox("Camera Connection Error: " + ex.Message)
            Exit Sub
        End Try

        'Run the target loop
        TargetLoop()

        'Disconnect objects
        tsx_sky.Disconnect()
        tsx_tele.Disconnect()
        tsx_cam.Disconnect()

        'Clean up objects
        tsx_sky = Nothing
        tsx_tele = Nothing
        tsx_cam = Nothing
        tsx_util = Nothing

    End Sub

    ' ********************************************************************************
    ' *
    ' * Below are all the subroutines used in this sample
    ' *

    Sub TargetLoop()

        Dim LineFromFile As String
        Dim sname As String
        Dim dAz As Double
        Dim dAlt As Double
        Dim iError As Boolean

        'Open the observing list export file for targets
        Dim MyFile = My.Computer.FileSystem.OpenTextFileReader(szPathToMapFile)  'Stream object for Export Data text file

        'Get the first line -- headers
        LineFromFile = MyFile.ReadLine
        'Exit if the file is empty
        If LineFromFile Is Nothing Then
            Return
        End If

        Dim iRAindex = InStr(LineFromFile, "RA")
        Dim iDecindex = InStr(LineFromFile, "Dec")

        Do

            LineFromFile = MyFile.ReadLine
            MsgBox("RA: " + Mid(LineFromFile, iRAindex, 13) + "  Dec: " + Mid(LineFromFile, iDecindex, 13))

            sname = Left(LineFromFile, 12)
            tsx_util.ConvertStringToRA(Mid(LineFromFile, iRAindex, 13))
            dAz = tsx_util.dOut0
            tsx_util.ConvertStringToDec(Mid(LineFromFile, iDecindex, 13))
            dAlt = tsx_util.dOut0

            'Slew to object
            Try
                tsx_tele.SlewToAzAlt(dAz, dAlt, sname)
            Catch ex As Exception
                If Not bIgnoreErrors Then
                    iError = MsgBox("An error has occurred running slew: " + ex.Message +
                                          vbCrLf + vbCrLf + "Exit Script?",
                                          vbYesNo + vbInformation)
                    If iError = vbYes Then
                        Exit Do
                    End If
                End If
            End Try

            'Set exposure time and try for image, exit if error
            tsx_cam.ExposureTime = dExposure
            Try
                tsx_cam.TakeImage()
            Catch ex As Exception
                If Not bIgnoreErrors Then
                    iError = MsgBox("An error has occurred running image " + ex.Message +
                                      vbCrLf + vbCrLf + "Exit Script?",
                                      vbYesNo + vbInformation)
                    If iError = vbYes Then
                        Exit Do
                    End If
                End If
            End Try

            'Add to T-point Model
            Try
                tsx_sky.AutoMap()
            Catch ex As Exception
                If Not bIgnoreErrors Then
                    iError = MsgBox("An error has occurred running AutoMap " + ex.Message +
                                    vbCrLf + vbCrLf + "Exit Script?",
                                    vbYesNo + vbInformation)
                    If iError = vbYes Then
                        Exit Do
                    End If
                End If
            End Try

        Loop

    End Sub


    Function Welcome() As Boolean

        ' Returns True if user wants to continue, False is abort

        Dim intDoIt = MsgBox("This script demonstrates how to map your telescope in an automated fashion." +
                             vbCrLf + vbCrLf +
                             "See the function InitGlobalUserVariables() to set the path to the map list text file.",
                             vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")

        If intDoIt = vbCancel Then
            Return (False)
        End If

        Return (True)
    End Function

End Module