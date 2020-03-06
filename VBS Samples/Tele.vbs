
' Windows VBScript Example: Tele
'
' ------------------------------------------------------------------------
'               Adapted from Tele.vbs  (Visual Basic Script)
'               Copyright (C) Software Bisque (2009?)
'
'				Converted 2016, R.McAlister
'
' ------------------------------------------------------------------------
'
' This script exercises the telescope class functions
' 
'Global settings
'
sUserName = "Rick"  'Set for user directory

'Use TheSky to generate a text file of mapping points
szPathToMapFile  = "C:\Users\" & sUserName & "\Documents\Software Bisque\TheSkyX Professional Edition\Exported Data\map.txt"

'Set the exposure time for the image
dExposure  = 1.0

'Create the telescope object
set tsx_ts = CreateObject("TheSkyX.Sky6RASCOMTele")

'Connect to the telescope
tsx_ts.Connect()

'See if connection failed
If (tsx_ts.IsConnected = 0) Then
	MsgBox("Connection failed.")
	Return
End If

'Get and show the current telescope ra, dec
tsx_ts.GetRaDec()
MsgBox("Ra,Dec =" & FormatNumber(tsx_ts.dRa) & ",  " & FormatNumber(tsx_ts.dDec))

'Get and show the current telescope az, alt
tsx_ts.GetAzAlt()
MsgBox("Az,Alt=" & FormatNumber(tsx_ts.dAz) & ",  " & FormatNumber(tsx_ts.dAlt))

'Goto an arbitrary RA and Dec
tsx_ts.SlewToRaDec 2.0, 3.0, "Home" 
MsgBox("GotoComplete")

'Sync on an ra dec
tsx_ts.Sync 3.0, 3.0, "Matt" 

'Disconnect the telscope
tsx_ts.Disconnect()
