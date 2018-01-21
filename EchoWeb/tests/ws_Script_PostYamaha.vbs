Call LogEntry()

Sub LogEntry()

        'Force the script to finish on an error.
        On Error Resume Next

        'Declare variables
        Dim objRequest
        Dim URL
		Dim data

        Set objRequest = CreateObject("Microsoft.XMLHTTP")

        'Put together the URL link appending the Variables.
        URL = "http://192.168.1.220/YamahaRemoteControl/ctrl"
		
		'build data
		data = "<YAMAHA_AV cmd=" & Chr(34) & "PUT" & Chr(34) + "><Main_Zone><Input><Input_Sel>HDMI3</Input_Sel></Input></Main_Zone></YAMAHA_AV>"

        'Open the HTTP request and pass the URL to the objRequest object
        objRequest.open "POST", URL , False
		objRequest.setRequestHeader "Content-Type", "application/x-www-form-urlencoded"

        'Send the HTML Request
        objRequest.send data

        'Set the object to nothing
        Set objRequest = Nothing

End Sub