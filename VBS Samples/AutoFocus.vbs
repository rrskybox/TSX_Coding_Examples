' Windows VBScript Example: AutoFocus
'
' ------------------------------------------------------------------------
'
'               Author: R.McAlister (2016)
'
' ------------------------------------------------------------------------
'
' This script performs the following steps:
'   Saves the current target and imaging parameters
'   Turns off Autoguiding (if on)
'   Turns on AutoFocus
'   Returns to the current target

' Note that "Automatically slew telescope to nearest appropriate focus star" must be checked in the @Focus2 start-up window.
'   
'Globals
'
iFilter = 3 
'Connect the telescope for some slewing
set tsx_tele = CreateObject("TheSkyX.Sky6RASCOMTele")
'Work around and Run @Focus2
'   Save current target name so it can be found again
'   Run @Focus2 (which preempts the observating list and object)
'   Restore current target, using Name with Find method
'   ClosedLoopSlew back to target
set tsx_cc = CreateObject("TheSkyX.ccdsoftCamera")
iCamStat = 0
iCamStat = tsx_cc.focConnect()

'Get current target name so we can return after running @focus2
set tsx_oi = CreateObject("TheSkyX.Sky6ObjectInformation")
tsx_oi.Property(0)
sTargetName = tsx_oi.ObjInfoPropOut

'Run Autofocus
'   Create a camera object
'   Launch the autofocus watching out for an exception -- which will be posted in TSX

'Save current camera delay, exposure and filter
' then set the camera delay = 0
' set the delay back when done with focusing

tsx_cc.AutoSaveFocusImages = False
dcamdelay  = tsx_cc.Delay
iCamReduction  = tsx_cc.ImageReduction
iCamFilter  = tsx_cc.FilterIndexZeroBased
dCamExp  = tsx_cc.ExposureTime
iFocStatus  = 0

tsx_cc.ImageReduction = 3 'AutoDark
tsx_cc.FilterIndexZeroBased = 3 'Luminance
tsx_cc.ExposureTime = 10
tsx_cc.Delay = 0
tsx_cc.FilterIndexZeroBased = iFilter

iFocStatus = tsx_cc.AtFocus2()
'
'Restore the current target and slew back to it
'   Run a Find on the target -- which makes it the "observation"
'   Perform Closed Loop Slew to the target

set tsx_sc = CreateObject("TheSkyX.Sky6StarChart")
tsx_sc.Find(sTargetName)
'Run a Closed Loop Slew to return
set tsx_cls = CreateObject("TheSkyX.ClosedLoopSlew")
'Set the exposure, filter to luminance and reduction, set the camera delay to 0 -- any backlash
' should be picked up in the mount driver
tsx_cc.ImageReduction = 3 'AutoDark
tsx_cc.FilterIndexZeroBased = iFilter 'Luminance, probably
tsx_cc.ExposureTime = 10
tsx_cc.Delay = 0
clsstat = tsx_cls.exec()

'Put back the orginal settings            
tsx_cc.ImageReduction = iCamReduction
tsx_cc.FilterIndexZeroBased = iCamFilter
tsx_cc.ExposureTime = dCamExp
tsx_cc.Delay = dCamDelay

