Module ClosedLoopSlew

    ' Windows Visual Basic Sample Console Application: ClosedLoopSlew
    '
    ' ------------------------------------------------------------------------
    '
    '               Author: R.McAlister (2014)
    '
    ' ------------------------------------------------------------------------
    '
    ' ClosedLoopSlew:  Find a target (M39 in this case), then execute a closed loop slew to it
    '
    '
    Sub Main()

        'Welcome Window
        Dim intDoIt = MsgBox("This script demonstrates how to execute the Closed Loop Slew class." + vbCrLf + vbCrLf,
                            vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return
        End If

        'Set connection to star chart and perform a find on M39
        Dim tsx_sc = CreateObject("TheSkyX.Sky6StarChart")
        tsx_sc.Find("M39")

        'Create connection to camera and connect
        Dim tsx_cc = CreateObject("TheSkyX.ccdsoftCamera")
        tsx_cc.Connect()

        'Create closed loop slew object
        Dim tsx_cls = CreateObject("TheSkyX.ClosedLoopSlew")

        'Set the exposure, filter to luminance and reduction, set the camera delay to 0 -- backlash
        ' should be picked up in the mount driver
        tsx_cc.ImageReduction = theskyxLib.ccdsoftImageReduction.cdAutoDark
        tsx_cc.FilterIndexZeroBased = 3 'Luminance
        tsx_cc.ExposureTime = 10
        tsx_cc.Delay = 0

       'Execute
        Try
            Dim clsstat = tsx_cls.exec()
        Catch ex As Exception
            'Just close up: TSX will spawn error window
            MsgBox("Closed Loop Slew failure: " + Str(tsx_cls.lastError))
        End Try

        Return

    End Sub

End Module
