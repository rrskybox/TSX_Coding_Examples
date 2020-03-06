Module Cam

    ' Windows Visual Basic Sample Console Application: Cam
    '
    ' ------------------------------------------------------------------------
    '               Adapted from Cam.vbs  (Visual Basic Script)
    '               Copyright (C) Software Bisque (2013)
    '
    '				Converted 2014, R.McAlister
    '
    ' ------------------------------------------------------------------------
    '
    ' This Visual Basic console application demonstrates the key elements of how to take images using the primary camera.

    Sub main()

        Dim iCamStatus As Integer

        Dim instat = Welcome()
        If Not instat Then
            Return
        End If

        'Create the camera object
        Dim tsx_cam = CreateObject("TheSkyX.ccdSoftCamera")

        'Connect TSX to the camera
        Try
            tsx_cam.Connect()
        Catch ex As Exception
            MsgBox("Camera Error: ", ex.Message)
                'Exit
            Exit Sub
        End Try

        'Set the exposure time
        tsx_cam.ExposureTime = 15.0
        'SEt an exposure delay
        tsx_cam.Delay = 5.0
        'Set a frame type
        tsx_cam.Frame = theskyxLib.ccdsoftImageFrame.cdLight
        'Set for autodark
        tsx_cam.ImageReduction = theskyxLib.ccdsoftImageReduction.cdAutoDark
        'Set for synchronous imaging (this app will wait until done or error)
        tsx_cam.Asynchronous = False
        'Take image
        iCamStatus = tsx_cam.TakeImage()
        If iCamStatus <> 0 Then
            MsgBox("Camera Error: ", Str(iCamStatus))
        End If

        'Disconnect Camera
        tsx_cam.Disconnect()

        'Clean up
        tsx_cam = Nothing
        Return
    End Sub


    Function Welcome() As Boolean

        ' Returns True if user wants to continue, False is abort

        Dim intDoIt = MsgBox("This script demonstrates how to access the camera using the Windows Visual Basic.",
                             vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return (False)
        End If
        Return (True)
    End Function


End Module