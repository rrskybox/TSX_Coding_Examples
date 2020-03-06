
' Windows VBScript: Solar
'
' ------------------------------------------------------------------------
'               Vaguely adapted from Solar.vbs
'               Copyright Software Bisque 
'           
'               Converted by:  R.McAlister 2014
'
' ------------------------------------------------------------------------
'
'Very simple demonstration script that targets the telescope on the Sun -- careful now.
'
'  Note:  this is basically simplification of "ListSearch.vb" where the only target is the Sun.
'
'TSX Enumerations
sk6ObjInfoProp_ALT = 58
sk6ObjInfoProp_AZM = 59
'
'Target Sun

target  = "Sun"

'Create Objects
set tsx_sc = CreateObject("TheSkyX.Sky6StarChart")
set tsx_ts = CreateObject("TheSkyX.Sky6RASCOMTele")
set tsx_oi = CreateObject("TheSkyX.Sky6ObjectInformation")

'Connect telescope
tsx_ts.Connect()

tsx_sc.Find(target)
tsx_oi.Property(sk6ObjInfoProp_ALT)
dAlt  = tsx_oi.ObjInfoPropOut
dAz = tsx_oi.ObjInfoPropOut

tsx_ts.SlewToAzAlt dAz, dAlt, target 

MsgBox("The Sun's location is at: Altitude: " + FormatNumber(dAlt) + "  Azimuth: " + FormatNumber(dAz))

'Disconnect telesope
tsx_ts.Disconnect()



