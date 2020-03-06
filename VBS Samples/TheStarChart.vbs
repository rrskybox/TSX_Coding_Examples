' Windows VBScript Example: TheStarChart
'
' ------------------------------------------------------------------------
'               Adapted from TheSky.vbs  (Visual Basic Script)
'               Copyright (C) Software Bisque (2009?)
'
'				Converted 2016, R.McAlister
'
' ------------------------------------------------------------------------
'
' This Visual Basic console application exercises the Star Chart class methods.
'    These methods supercede the depreciated TheSky class.
' 

'TSX Enumerations
sk6ObjInfoProp_RA_2000 = 56
sk6ObjInfoProp_DEC_2000 = 57
sk6ObjInfoProp_AZM = 58
sk6ObjInfoProp_ALT = 59

'Global settings
sUserName = "Rick"  'User Directory

'Use TheSky to generate a text file of mapping points
szPathToMapFile = "C:\Users\" & sUserName & "\Documents\Software Bisque\TheSkyX Professional Edition\Exported Data\map.txt"

'Set the exposure time for the image
dExposure  = 1.0

'
'Open Star Chart object 
set tsx_sc = CreateObject("TheSkyX.Sky6StarChart")

'Create Object Information object
set tsx_oi = CreateObject("TheSkyX.Sky6ObjectInformation")

'Find the target 
sname = "M51"
tsx_sc.Find(sname)

'Acquire and display the RA and Dec of the target
tsx_oi.Property(sk6ObjInfoProp_RA_2000)
dRA = tsx_oi.ObjInfoPropOut
tsx_oi.Property(sk6ObjInfoProp_DEC_2000)
dDec = tsx_oi.ObjInfoPropOut
MsgBox(sname & " RA = " & FormatNumber(dRA) & " Declination = " & FormatNumber(dDec))

'Acquire and display the Azimuth and Altitude of the target
tsx_oi.Property(sk6ObjInfoProp_AZM)
dAz = tsx_oi.ObjInfoPropOut
tsx_oi.Property(sk6ObjInfoProp_ALT)
dAlt = tsx_oi.ObjInfoPropOut
MsgBox(sname & " Azimuth = " & FormatNumber(dAz) & " Altitude = " & FormatNumber(dAlt))

'Acquire and display the RA, Dec, FOV and Rotation of the screen
MsgBox("ScreenRa=" & FormatNumber(tsx_sc.RightAscension))
MsgBox("ScreenDec=" & FormatNumber(tsx_sc.Declination))
MsgBox("ScreenFOV =" & FormatNumber(tsx_sc.FieldOfView))
MsgBox("ScreenRotation =" & FormatNumber(tsx_sc.Rotation))

'Set Star Chart FOV to 10
tsx_sc.FieldOfView = 10.0

'Set Star Chart Rotation to 300 degrees
tsx_sc.Rotation = 300.0
