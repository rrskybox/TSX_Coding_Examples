' Windows VBScript: StarChartSetWW
'
' ------------------------------------------------------------------------
'               Adapted from TheSkySetWW.vbs  (Visual Basic Script)
'               Copyright (C) Software Bisque (2009?)
'
'				Converted 2016, R.McAlister
'
' ------------------------------------------------------------------------
'
' This script exercises the Star Chart class functions for setting 
'   Timne, Time Zone, Location, etc.
'
'TSX Enumerations
sk6DocProp_UseComputerClock = 5
sk6DocProp_JulianDateNow = 9
sk6DocProp_Time_Zone = 2
sk6DocProp_ElevationInMeters = 3
sk6DocProp_Latitude = 0
sk6DocProp_Longitude = 1
sk6DocProp_LocationDescription = 63

dJD = 2452066.0 '=06/05/2001, ignored if UseCompterClock=1
dTZ = 7         'MST
dElev = 1000      'meters
bUseComputerClock = 1         '1=Yes 0=No
dLat = 39.5
dLong = 105.5
szLoc = "Location from script"

'Create Telescope Object and disconnect telescope -- time changes are not enabled with scope connected
set tsx_ts = CreateObject("TheSkyX.Sky6RASCOMTele")
tsx_ts.Disconnect()

'Create the Star Chart object
set tsx_sc = CreateObject("TheSkyX.Sky6StarChart")

'Turn off the Computer Clock
tsx_sc.SetDocumentProperty sk6DocProp_UseComputerClock, 0 

'Change the julian date
tsx_sc.SetDocumentProperty sk6DocProp_JulianDateNow, dJD

'Change the time zone
tsx_sc.SetDocumentProperty sk6DocProp_Time_Zone, dTZ 

'Change the elevation
tsx_sc.SetDocumentProperty sk6DocProp_ElevationInMeters, dElev 

'Change the latitude
tsx_sc.SetDocumentProperty sk6DocProp_Latitude, dLat 

'Change the longitude
tsx_sc.SetDocumentProperty sk6DocProp_Longitude, dLong 

'Change the location description
tsx_sc.SetDocumentProperty sk6DocProp_LocationDescription, szLoc 


