Imports Newtonsoft.Json

Namespace Links.Model.User

	'''<summary>
	''' Query result for a bitly link shortened by the authenticated user based on a long URL.
	'''</summary>
	Public Class LinkLookup

		'''<summary>
		''' The corresponding bitly aggregate link (global hash)
		'''</summary>
		<JsonProperty("aggregate_link")>
		Property AggregateLink As String

		''' <summary>
		''' The corresponding bitly link (aka short URL).
		''' </summary>
		<JsonProperty("link")>
		Property ShortUrl As String

		''' <summary>
		''' An echo back of the url parameter.
		''' </summary>
		<JsonProperty("url")>
		Property LongUrl As String

	End Class

End Namespace