Imports Newtonsoft.Json

' URI : /v3/expand
' Given a bitly URL or hash (or multiple), returns the target (long) URL.

Namespace Links.Model

	'''<summary>
	''' Target (long) URL
	'''</summary>
	Public Class TargetUrl

		''' <summary>
		''' The corresponding bitly aggregate identifier.
		''' </summary>
		<JsonProperty("global_hash")>
		Property GlobalHash As String

		''' <summary>
		''' The URL that the requested short url or hash points to.
		''' </summary>
		<JsonProperty("long_url")>
		Property LongUrl As String

		''' <summary>
		''' An echo back of the short url request parameter.
		''' </summary>
		<JsonProperty("short_url")>
		Property ShortUrl As String

		''' <summary>
		''' The corresponding bitly user identifier.
		''' </summary>
		<JsonProperty("user_hash")>
		Property UserHash As String

	End Class

End Namespace