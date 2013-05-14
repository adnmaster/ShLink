Imports Newtonsoft.Json

Namespace DataAPIs.Model

	Public Class HighValue

		<JsonProperty("params")>
		Property Params As HighValueParams

		<JsonProperty("values")>
		Property Values As List(Of String)


		Class HighValueParams

			<JsonProperty("lang")>
			Property Lang As String

			<JsonProperty("limit")>
			Property Limit As Integer

		End Class

	End Class

End Namespace