Module Filter

    ' Windows Visual Basic Sample Console Application: Filter
    '
    ' ------------------------------------------------------------------------
    '               Vaguely adapted from Filter.vbs  (Visual Basic Script)
    '               Copyright (C) Software Bisque (2009?)
    '
    '				Converted 2014, R.McAlister
    '
    ' ------------------------------------------------------------------------
    '
    ' This Visual Basic console application demonstrates how to connect, change and disconnect a filter.  Along the way, most o
    '  Along the way, most of the basic camera operating methods are also demonstrated.
    '
    'Global Parameters
    Public dExposure As Double = 3.0 'seconds
    Public ifilter As Integer = 3  '4nd filter slot, probably clear/lumenscent

    'Global TSX COM Objects
    Public tsx_pc As Object


    Sub Main()

        If Not Welcome() Then
            Return
        End If

        'Create camera object and connect
        Dim tsx_cc = CreateObject("theskyx.ccdsoftcamera")
        tsx_cc.Connect()

        'Set exposure length
        tsx_cc.ExposureTime = dExposure

        'Set frame type to Light frame
        tsx_cc.Frame = theskyxLib.ccdsoftImageFrame.cdLight

        'Set filter
        tsx_cc.FilterIndexZeroBased = ifilter

        'Set preexposure delay
        tsx_cc.Delay = 5                                      'Possible filter change = 5 sec delay

        'Set method type to Asynchronous (so we can demo the wait process)
        tsx_cc.Asynchronous = False

        'Set for autodark
        tsx_cc.ImageReduction = theskyxLib.ccdsoftImageReduction.cdAutoDark

        'Take image
        tsx_cc.TakeImage()

        'Wait for completion (unnecessary if Asynchronous is set to "False"
        Do While tsx_cc.state = theskyxLib.ccdsoftCameraState.cdStateTakePicture
            System.Threading.Thread.Sleep(1000)
        Loop

        'Clean up
        tsx_cc.Disconnect()
        tsx_cc = Nothing
        Return

    End Sub

    Function Welcome() As Boolean

        ' Returns True if user wants to continue, False is abort

        Dim intDoIt = MsgBox("This script demonstrates how to change a filter." + vbCrLf + vbCrLf,
                            vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return (False)
        End If

        Return (True)
    End Function

End Module


