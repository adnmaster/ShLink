Imports System.ComponentModel
Imports Newtonsoft.Json

Namespace DataAPIs.Response

	''' <summary>
	''' At bitly, they see about 4 billion clicks a month,
	''' and millions of new links a day. It's a data mine.
	''' Lookking through this enormous set of data to find interesting stuff [...] in order to build cool things!
	''' </summary>
	Public Class SearchResult
		Inherits ResponseBase

		''' <summary>
		''' The Search Result
		''' </summary>
		<JsonProperty("data")>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property SearchResult As Model.SearchResult

	End Class

End Namespace