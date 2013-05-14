Imports Newtonsoft.Json
Imports System.ComponentModel

<EditorBrowsable(EditorBrowsableState.Advanced)>
Public MustInherit Class ResponseBase

	''' <summary>
	''' HTTP status code (from bitly API).
	''' </summary>
	<JsonProperty("status_code")>
	Property StatusCode As Net.HttpStatusCode

	''' <summary>
	''' Status description of <see cref="StatusCode"> StatusCode (from bitly API) </see>.
	''' </summary>
	<JsonProperty("status_txt")>
	Property StatusDesription As String

End Class
