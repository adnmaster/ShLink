Imports Newtonsoft.Json

Namespace Links.Model

	'''<summary>
	''' Query result for a bitly link based on a long URL.
	'''</summary>
	Public Class LinkLookup

		''' <summary>
		''' The corresponding bitly aggregate link (global hash).
		''' </summary>
		<JsonProperty("aggregate_link")>
		Property AggregateLink As String

		''' <summary>
		''' An echo back of the (long) url parameter.
		''' </summary>
		<JsonProperty("url")>
		Property Url As String

	End Class

End Namespace