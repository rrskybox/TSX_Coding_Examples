' Windows VBScript Sample: Dome
'
' ------------------------------------------------------------------------
'               Adapted from Dome.vbs  (Visual Basic Script)
'               Copyright (C) Software Bisque (2013)
'
'				Converted 2016, R.McAlister
'
' ------------------------------------------------------------------------
'
' This script demonstrates how to access dome control.
'
'  Note:  The Dome add-on was not available for testing this script. Use at your own risk.
'

   'Create the dome object, connect and get it's targeting
tsx_do = CreateObject("TheSkyX.Sky6Dome")
tsx_do.Connect()
tsx_do.GetAzAl()

DomeAz = tsx_do.dAz
DomeEl = tsx_do.dEl

MsgBox("Dome Azimuth at: " & FormatNumber(DomeAz) & "  Dome Elevation at: " & FormatNumber(DomeEl))
MsgBox("Moving to Azimuth 0 degrees, Elevation 45 Degrees.")

'Target the dome at az = 0, alt = 45
tsx_do.GoToAzEl 0, 45 

'Disconnect the dome
tsx_do.Disconnect()

