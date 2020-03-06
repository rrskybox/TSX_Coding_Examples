Module AutoFocus

    ' Windows Visual Basic Sample Console Application: AutoFocus
    '
    ' ------------------------------------------------------------------------
    '
    '               Author: R.McAlister (2014)
    '
    ' ------------------------------------------------------------------------
    '
    ' This application performs the following steps:
    '   Saves the current target and imaging parameters
    '   Turns off Autoguiding (if on)
    '   Turns on AutoFocus
    '   Returns to the current target

    ' Note that "Automatically slew telescope to nearest appropriate focus star" must be checked in the @Focus2 start-up window.
    '   
    'Globals
    '
    Dim iFilter As Integer = 3 'Luminescent, I hope

    Sub Main()

        'Welcome Window
        Dim intDoIt = MsgBox("This script demonstrates how to execute AutoFocus with the Camera class." + vbCrLf + vbCrLf,
                            vbOKCancel + vbInformation,
                             "Windows Visual Basic Sample")
        If intDoIt = vbCancel Then
            Return
        End If

        'Connect the telescope for some slewing
        Dim tsx_tele = CreateObject("TheSkyX.Sky6RASCOMTele")

        'Work around and Run @Focus2
        '   Save current target name so it can be found again
        '   Run @Focus2 (which preempts the observating list and object)
        '   Restore current target, using Name with Find method
        '   ClosedLoopSlew back to target
        Dim tsx_cc = CreateObject("TheSkyX.ccdsoftCamera")
        Dim iCamStat As Integer = 0
        Try
            iCamStat = tsx_cc.focConnect()
        Catch ex As Exception
            'Just close up, TSX will spawn error window
            'MsgBox("AutoFocus unavailable:  " & Str(tstat))
            tsx_cc = Nothing
            Return
        End Try

        'Get current target name so we can return after running @focus2
        Dim tsx_oi = CreateObject("TheSkyX.Sky6ObjectInformation")
        tsx_oi.Property(theskyxLib.Sk6ObjectInformationProperty.sk6ObjInfoProp_NAME1)
        Dim sTargetName As String = tsx_oi.ObjInfoPropOut
        'TBD: Set up parameters for @Focus2, if necessary
        '   
        'Run Autofocus
        '   Create a camera object
        '   Launch the autofocus watching out for an exception -- which will be posted in TSX

        'Save current camera delay, exposure and filter
        ' then set the camera delay = 0
        ' set the delay back when done with focusing

        tsx_cc.AutoSaveFocusImages = False
        Dim dCamDelay As Double = tsx_cc.Delay
        Dim iCamReduction As Integer = tsx_cc.ImageReduction
        Dim iCamFilter As Integer = tsx_cc.FilterIndexZeroBased
        Dim dCamExp As Double = tsx_cc.ExposureTime
        Dim iFocStatus As Integer = 0

        tsx_cc.ImageReduction = theskyxLib.ccdsoftImageReduction.cdAutoDark
        tsx_cc.FilterIndexZeroBased = 3 'Luminance
        tsx_cc.ExposureTime = 10
        tsx_cc.Delay = 0
        tsx_cc.FilterIndexZeroBased = iFilter

        Try
            iFocStatus = tsx_cc.AtFocus2()
        Catch ex As Exception
            'Just close up, TSX will spawn error window
            'MsgBox("AutoFocus Error = " & Str(focstat))
            'ANd don't do anything for this example code
        End Try

        'Restore the current target and slew back to it
        '   Run a Find on the target -- which makes it the "observation"
        '   Perform Closed Loop Slew to the target

        Dim tsx_sc = CreateObject("TheSkyX.Sky6StarChart")
        tsx_sc.Find(sTargetName)
        'Run a Closed Loop Slew to return
        Dim tsx_cls = CreateObject("TheSkyX.ClosedLoopSlew")
        'Set the exposure, filter to luminance and reduction, set the camera delay to 0 -- any backlash
        ' should be picked up in the mount driver
        tsx_cc.ImageReduction = theskyxLib.ccdsoftImageReduction.cdAutoDark
        tsx_cc.FilterIndexZeroBased = iFilter 'Luminance, probably
        tsx_cc.ExposureTime = 10
        tsx_cc.Delay = 0
        Try
            Dim clsstat = tsx_cls.exec()
        Catch ex As Exception
            'Just close up: TSX will spawn error window
            'MsgBox("AutoFocus return failure")
        End Try
        'Put back the orginal settings            
        tsx_cc.ImageReduction = iCamReduction
        tsx_cc.FilterIndexZeroBased = iCamFilter
        tsx_cc.ExposureTime = dCamExp
        tsx_cc.Delay = dCamDelay
        'Clean Up
        tsx_cc = Nothing
        tsx_cls = Nothing
        tsx_oi = Nothing
        tsx_sc = Nothing
        tsx_tele = Nothing
        Return


    End Sub

End Module
