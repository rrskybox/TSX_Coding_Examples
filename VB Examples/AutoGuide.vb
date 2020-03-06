Module AutoGuide

    ' Windows Visual Basic Sample Console Application: AutoGuide
    '
    ' ------------------------------------------------------------------------
    '
    '               Author: R.McAlister (2014)
    '
    ' ------------------------------------------------------------------------
    '
    ' This application performs the following steps:
    '   Finds a target, M39 in this case
    '   Turns off Autoguiding (if on)
    '   Performs a Closed Loop Slew to the target
    '   Takes an image with the Autoguide camera
    '   Turns on Autoguiding
    '   
    '
    Sub Main()

        'Welcome Window
        Dim intDoIt = MsgBox("This script demonstrates how to execute AutoGuiding with the Camera class." + vbCrLf + vbCrLf,
                            vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return
        End If

        'Set connection to star chart and perform a find on M39 -- this will set the target for a CLS
        Dim tsx_sc = CreateObject("TheSkyX.Sky6StarChart")
        tsx_sc.Find("M39")

        'Create connection to autoguide camera and connect
        Dim tsx_ag = CreateObject("TheSkyX.ccdsoftCamera")
        'Attach this object to the guider camera
        tsx_ag.Autoguider = 1
        'Find out with the guide camera is up to.  Abort if autoguiding or calibrating
        If tsx_ag.Connect() = 0 Then
            If (tsx_ag.state = theskyxLib.ccdsoftCameraState.cdStateAutoGuide) Or (tsx_ag.state = theskyxLib.ccdsoftCameraState.cdStateCalibrate) Then
                tsx_ag.abort()
            End If
        End If

        'Create closed loop slew object
        Dim tsx_cls = CreateObject("TheSkyX.ClosedLoopSlew")

        'Set the exposure, filter to luminance and reduction, set the camera delay to 0 -- backlash
        ' should be picked up in the mount driver
        Dim tsx_cc = CreateObject("TheSkyX.ccdsoftCamera")
        tsx_cc.ImageReduction = theskyxLib.ccdsoftImageReduction.cdAutoDark
        tsx_cc.FilterIndexZeroBased = 3 'Luminance
        tsx_cc.ExposureTime = 10
        tsx_cc.Delay = 0
        tsx_cc = Nothing

        'run CLS synchronously
        'tsx_cls.Asynchronous = False  '-- this setting doesn't appear to work (member not found) as of DB8458

        'Execute
        Try
            Dim clsstat = tsx_cls.exec()
        Catch ex As Exception
            'Just close up: TSX will spawn error window
            MsgBox("Closed Loop Slew failure: " + Str(tsx_cls.lastError))
            'Let it go for demo purposes
        End Try

        'Connect AutoGuide camera in case it isn't
        Try
            tsx_ag.Connect()
        Catch ex As Exception
            MsgBox("Guide camera connect error: " + ex.Message)
            Exit Sub
        End Try
        'Take an image to use for Autoguiding, run the function synchronously
        tsx_ag.ExposureTime = 2
        tsx_ag.Asynchronous = False
        tsx_ag.Subframe = False
        Dim tstat = tsx_ag.TakeImage()  'Just assume it works

        'Turn asynchronous back on to get out of this
        tsx_ag.Asynchronous = True
        'Fire off Autoguiding
        tsx_ag.AutoGuide()

        'Clean up
        tsx_cls = Nothing
        tsx_ag = Nothing
        Return

    End Sub

End Module
