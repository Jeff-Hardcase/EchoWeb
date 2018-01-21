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
        URL = "http://localhost:56783/api/Ultron"
		
		'build data
		data = "testValue=Jeffvalue&testKey=alexaKey"

        'Open the HTTP request and pass the URL to the objRequest object
        objRequest.open "POST", URL , False
		objRequest.setRequestHeader "Content-Type", "application/x-www-form-urlencoded"

        'Send the HTML Request
        objRequest.send data

        'Set the object to nothing
        Set objRequest = Nothing

End Sub