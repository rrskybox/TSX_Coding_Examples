' Windows VBScript Example: AutoImageSet
'
' ------------------------------------------------------------------------
'
'               Author: R.McAlister (2016)
'
' ------------------------------------------------------------------------
'
' Example script that shows how to read (and by inference) how to set AutomatedImageLinkSettings
'
'
'Open Automated Image Link Settings object
set tsx_ails = CreateObject("TheSkyX.AutomatedImageLinkSettings")

'This property holds the image scale. 
dIScale  = tsx_ails.imageScale

'This property holds the position angle.
dPA = tsx_ails.positionAngle

'This property holds the exposure time for Closed Loop Slew and T-point runs
dExposure = tsx_ails.exposureTimeAILS

'This property holds the number of field of views to search while Image Linking
iFOVSearch  = tsx_ails.fovsToSearch

'This property holds the number of ImageLink retries upon failure.
iRetry  = tsx_ails.retries

MsgBox("Automated Image Link Settings:" & vbCrLf & vbCrLf & _
"Scale: " & FormatNumber(dIScale) & vbCrLf & _
"Position Angle: " & FormatNumber(dPA) & vbCrLf & _
"Exposure: " & FormatNumber(dExposure) & vbCrLf & _
"FOVs to Search: " & FormatNumber(iFOVSearch) & vbCrLf & _
"Retries: " & FormatNumber(iRetry))

