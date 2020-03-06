' Windows VBScript Sample: Filter
'
' ------------------------------------------------------------------------
'               Vaguely adapted from Filter.vbs  (Visual Basic Script)
'               Copyright (C) Software Bisque (2009?)
'
'				Converted 2016, R.McAlister
'
' ------------------------------------------------------------------------
'
' This script demonstrates how to connect, change and disconnect a filter.  Along the way, most o
'  Along the way, most of the basic camera operating methods are also demonstrated.
'
'TSX Enumerations
cdLight = 1
cdAutoDark = 3

'Global Parameters
dExposure = 3.0 'seconds
ifilter  = 3  '4nd filter slot, probably clear/lumenscent

'Create camera object and connect
set tsx_cc = CreateObject("theskyx.ccdsoftcamera")
tsx_cc.Connect()

'Set exposure length
tsx_cc.ExposureTime = dExposure

'Set frame type to Light frame
tsx_cc.Frame = cdLight

'Set filter
tsx_cc.FilterIndexZeroBased = ifilter

'Set preexposure delay
tsx_cc.Delay = 5                                      'Possible filter change = 5 sec delay

'Set method type to Asynchronous (so we can demo the wait process)
tsx_cc.Asynchronous = False

'Set for autodark
tsx_cc.ImageReduction = cdAutoDark

'Take image
tsx_cc.TakeImage()

'Clean up
tsx_cc.Disconnect()





