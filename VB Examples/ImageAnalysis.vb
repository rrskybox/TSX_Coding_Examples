Module ImageAnalysis

    ' Windows Visual Basic Sample Console Application: ImageAnalysis
    '
    ' ------------------------------------------------------------------------
    '
    '               Author: R.McAlister (2014)
    '
    ' ------------------------------------------------------------------------
    '
    ' This application demonstrates some of the functionality of the ccdsoftCamera and Image classes
    '
    ' The example takes a 60 second exposure then produces a recommendation window to display computed
    '   optimal exposure length and duration for a one hour shoot, based on average background noise.
    '
    ' The algorithms are based on work by ... 
    '   John Smith: http://www.hiddenloft.com/notes/SubExposures.pdf
    '   Charles Anstey: http://www.cloudynights.com/item.php?item_id=1622
    '   Steve Cannistra: http://www.starrywonders.com/snr.html
    '
    ' Note:  Where the required parameters like "gain" are not supplied through TSX, they are arbitrarily set for
    '   an SBIG STF8300
    '
    Sub Main()

        'Open camera control and connect it to hardware
        Dim tsx_cc = CreateObject("theskyx.ccdsoftcamera")
        Try
            tsx_cc.Connect()
        Catch ex As Exception
            MsgBox("Camera Connect Error: " + ex.Message)
        End Try
        'turn on autosave
        tsx_cc.autosaveon = 1
        'Set for 60 second exposure, light frame with autodark
        tsx_cc.ExposureTime = 60
        tsx_cc.Frame = theskyxLib.ccdsoftImageFrame.cdLight
        tsx_cc.ImageReduction = theskyxLib.ccdsoftImageReduction.cdAutoDark
        tsx_cc.FilterIndexZeroBased = 3     'Assumed Lumescent, but change accordingly
        tsx_cc.Delay = 5                    'Possible filter change = 5 sec delay
        tsx_cc.Asynchronous = True          'Going to do a wait loop
        tsx_cc.TakeImage()
        Do While tsx_cc.state = theskyxLib.ccdsoftCameraState.cdStateTakePicture
            System.Threading.Thread.Sleep(1000)
        Loop

        'Create image object
        Dim tsx_im = CreateObject("TheSkyX.ccdsoftImage")
        Dim imgerr As Integer = 0
        'Open the active image, if any
        Try
            imgerr = tsx_im.AttachToActive()
        Catch ex As Exception
            MsgBox("No Image Available:  " & Str(imgerr))
            tsx_im = Nothing
            Return
        End Try

        Const totalexp = 60  'Minutes for total exposure sequence

        Dim dGain As Double = 0.37       'electrons per ADU for SBIG STF8000M as spec'd
        Dim iPedestal As Integer = 0    'base pedestal
        Dim dRnoise As Double = 9.3     'read out noise in electrons
        Dim dNoiseFac As Double = 0.05  'maximum tolerable contribution of readout noise
        Dim dExpFac As Double = 1      'Exposure reduction factor
        Dim dSLambda As Double = 15      'Faint target ADU minimum
        Dim dSNRMax As Double = 0.9      'fraction of maximum achievable signal to noise ratio (Cannistra)

        'Presumably an Image Link has already been performed
        'Check on this is TBD

        Dim iPX As Integer = tsx_im.FITSKeyword("NAXIS1")
        Dim iPY As Integer = tsx_im.FITSKeyword("NAXIS2")
        Dim iPXBin As Integer = tsx_im.FITSKeyword("XBINNING")
        Dim iPYBin As Integer = tsx_im.FITSKeyword("YBINNING")
        'Dim igain as integer = tsx_im.FITSKeyword("EGAIN")         'TSX doesn't pick this up, yet
        Dim dExpTime As Double = tsx_im.FITSKeyword("EXPTIME")

        iPX = iPX - 1
        iPY = iPY - 1

        Dim dAvgABU As Double = tsx_im.averagePixelValue()
        Dim dEsky As Double = ((dAvgABU - iPedestal) * dGain) / dExpTime
        Dim dTorn As Double = (dRnoise ^ 2) / ((((1 + dNoiseFac) ^ 2) - 1) * dEsky)

        'Smith algorithm
        Dim iExp1 As Integer = Int(dTorn / 2)
        Dim iReps1 As Integer = Int((((totalexp * 60) / dTorn) - 1) * 2)
        'Anstey algorithm
        Dim iExp2 As Integer = Int((dSLambda * Math.Sqrt(totalexp * 60)) / (2 * Math.Sqrt(dAvgABU / dExpTime)))
        Dim iReps2 As Integer = Int((totalexp * 60) / iExp2)
        'Cannestr algorithm
        Dim iExp3 As Integer = Int((dSNRMax ^ 2) * (dRnoise ^ 2)) / ((dEsky) * (1 - (dSNRMax ^ 2)))
        Dim iReps3 As Integer = Int((totalexp * 60) / iExp3)

        'Display Results
        MsgBox("Smith Model (at tolerable noise factor = 0.05):" & vbNewLine &
                     "     " & Str(iExp1) & " second exposure with" & vbNewLine &
                     "     " & Str(iReps1) & " repetitions per hour." & vbNewLine & vbNewLine &
                     "Anstey Model (at faint target minimum = 15):" & vbNewLine &
                     "     " & Str(iExp2) & " second exposure with" & vbNewLine &
                     "     " & Str(iReps2) & " repetitions per hour." & vbNewLine & vbNewLine &
                     "Cannestra Model (at SNR = 90% of maximum):" & vbNewLine &
                     "     " & Str(iExp3) & " second exposure with" & vbNewLine &
                     "     " & Str(iReps3) & " repetitions per hour.")

        'Clean up
        tsx_im = Nothing
        tsx_cc = Nothing
        Return
    End Sub
End Module
