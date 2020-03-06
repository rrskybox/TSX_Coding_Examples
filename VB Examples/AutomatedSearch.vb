Module AutomatedSearch

    ' Windows Visual Basic Sample Console Application: AutomatedSearch
    '
    ' ------------------------------------------------------------------------
    '               Adapted from AutomatedSearch.vbs  (Visual Basic Script)
    '               Copyright (C) Software Bisque (2009?)
    '
    '				Converted 2015, R.McAlister
    '
    ' ------------------------------------------------------------------------
    '
    ' This Visual Basic console application reads an Exported Observing List file for targets
    '   For each target, the telescope is slewed to the target and an image taken.
    ' This example is essentially identical to AutoMap, except that an image is taken and saved
    '	and no AutoMap performed.
    '
    'Global settings

    'Use TheSky to generate a text file of mapping points
    Public szPathToMapFile As String = "C:\Users\Rick\Documents\Software Bisque\TheSkyX Professional Edition\Exported Data\map.txt"

    'If you want your script to run all night regardless of errors, set bIgnoreErrors = True
    Public bIgnoreErrors As Boolean = False

    'Set the exposure time for the image
    Public dExposure As Double = 1.0

    'Global TSX COM Objects
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
        tsx_tele = CreateObject("TheSkyX.Sky6RASCOMTele")
        tsx_cam = CreateObject("TheSkyX.ccdSoftCamera")
        tsx_util = CreateObject("TheSkyX.Sky6Utils")

        'Connect Objects
        Try
            tsx_tele.Connect()
        Catch ex As Exception
            MsgBox("Telescope Error: " + ex.Message)
            Exit Sub
        End Try
        Try
            tsx_cam.Connect()
        Catch ex As Exception
            MsgBox("Camera Error: " + ex.Message)
            Exit Sub
        End Try

        'Run the target loop
        TargetLoop()

        'Disconnect objects
        tsx_tele.Disconnect()
        tsx_cam.Disconnect()

        'Clean up objects
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
                        Return
                    End If
                End If
            End Try

            'Set exposure time and try for image, exit if error
            tsx_cam.ExposureTime = dExposure
            tsx_cam.AutoSaveOn = True
            Try
                tsx_cam.TakeImage()
            Catch ex As Exception
                If Not bIgnoreErrors Then
                    iError = MsgBox("An error has occurred running image " + ex.Message +
                                      vbCrLf + vbCrLf + "Exit Script?",
                                      vbYesNo + vbInformation)
                    If iError = vbYes Then
                        Return
                    End If
                End If
            End Try

        Loop

    End Sub


    Function Welcome() As Boolean

        ' Returns True if user wants to continue, False is abort

        Dim intDoIt = MsgBox("This script demonstrates how to acquire images from an Observing list textfile." + vbCrLf + vbCrLf +
                            "Use TheSky's data export command to create a Simple List of objects that meet your desired criteria." + vbCrLf +
                            "For example, you can easily generate a list of all Messier objects within an hour of the meridian.  " + vbCrLf +
                            "Or you can generate a list of all PGC galaxies with a magnitude greater than 13 and a size between 10 and 20 arc minutes." + vbCrLf +
                            "See the function InitGlobalUserVariables to set the path to your list of targets, set the exposure time, etc.",
                            vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return (False)
        End If

        Return (True)
    End Function

End Module