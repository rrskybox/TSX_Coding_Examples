' Windows VBScript Sample: ImageAnalysis
'
' ------------------------------------------------------------------------
'
'               Author: R.McAlister (2016)
'
' ------------------------------------------------------------------------
'
' This script demonstrates some of the functionality of the ccdsoftCamera and Image classes
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
'TSX enumerations
cdLight = 1
cdAutoDark = 3

'Open camera control and connect it to hardware
set tsx_cc = CreateObject("theskyx.ccdsoftcamera")
tsx_cc.Connect()

'turn on autosave
tsx_cc.autosaveon = 1
'Set for 60 second exposure, light frame with autodark
tsx_cc.ExposureTime = 60
tsx_cc.Frame = cdLight
tsx_cc.ImageReduction = cdAutoDark
tsx_cc.FilterIndexZeroBased = 3     'Assumed Lumescent, but change accordingly
tsx_cc.Delay = 5                    'Possible filter change = 5 sec delay
tsx_cc.Asynchronous = false          'Going to do a wait loop
tsx_cc.TakeImage()

'Create image object
set tsx_im = CreateObject("TheSkyX.ccdsoftImage")
imgerr  = 0
'Open the active image, if any

imgerr = tsx_im.AttachToActive()

totalexp = 6  'Seconds for total exposure sequence

dGain  = 0.37       'electrons per ADU for SBIG STF8000M as spec'd
iPedestal = 0    'base pedestal
dRnoise  = 9.3     'read out noise in electrons
dNoiseFac  = 0.05  'maximum tolerable contribution of readout noise
dExpFac  = 1      'Exposure reduction factor
dSLambda  = 15      'Faint target ADU minimum
dSNRMax  = 0.9      'fraction of maximum achievable signal to noise ratio (Cannistra)

'Presumably an Image Link has already been performed
'Check on this is TBD

iPX = tsx_im.FITSKeyword("NAXIS1")
iPY = tsx_im.FITSKeyword("NAXIS2")
iPXBin = tsx_im.FITSKeyword("XBINNING")
iPYBin = tsx_im.FITSKeyword("YBINNING")
'igain = tsx_im.FITSKeyword("EGAIN")         'TSX doesn't pick this up, yet
dExpTime  = tsx_im.FITSKeyword("EXPTIME")

iPX = iPX - 1
iPY = iPY - 1

dAvgABU  = tsx_im.averagePixelValue()
dEsky  = ((dAvgABU - iPedestal) * dGain) / dExpTime
dTorn  = (dRnoise ^ 2) / ((((1 + dNoiseFac) ^ 2) - 1) * dEsky)

'Smith algorithm
iExp1 = Int(dTorn / 2)
iReps1 = Int((((totalexp * 60) / dTorn) - 1) * 2)
'Anstey algorithm
iExp2 = Int((dSLambda * Sqr(totalexp * 60)) / (2 * Sqr(dAvgABU / dExpTime)))
iReps2 = Int((totalexp * 60) / iExp2)
'Cannestr algorithm
iExp3 = Int((dSNRMax ^ 2) * (dRnoise ^ 2)) / ((dEsky) * (1 - (dSNRMax ^ 2)))
iReps3 = Int((totalexp * 60) / iExp3)

'Display Results
MsgBox("Smith Model (at tolerable noise factor = 0.05):" & vbNewLine & _
"     " & FormatNumber(iExp1) & " second exposure with" & vbNewLine & _
"     " & FormatNumber(iReps1) & " repetitions per hour." & vbNewLine & vbNewLine & _
"Anstey Model (at faint target minimum = 15):" & vbNewLine & _
"     " & FormatNumber(iExp2) & " second exposure with" & vbNewLine & _
"     " & FormatNumber(iReps2) & " repetitions per hour." & vbNewLine & vbNewLine & _
"Cannestra Model (at SNR = 90% of maximum):" & vbNewLine & _
"     " & FormatNumber(iExp3) & " second exposure with" & vbNewLine & _
"     " & FormatNumber(iReps3) & " repetitions per hour.")

