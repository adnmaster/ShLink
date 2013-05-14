
Namespace DataAPIs.Model.CustomSearch

	''' <summary>
	''' Minimum search (Title, long url and aggregate (short) link) 
	''' </summary>
	''' <remarks>
	''' Use the results to perform other
	''' searches see
	''' <see
	''' cref="adnmaster.Bitly.DataAPIs.SearchEndPoint.Search">Search</see>
	''' </remarks>
	''' <seealso
	''' cref="adnmaster.Bitly.DataAPIs.SearchEndPoint.Search">Search</seealso>
	Public Class MinSearch

		Public Shared Widening Operator CType(ByVal ResultItem As SearchResult.Result) As MinSearch

			If ResultItem IsNot Nothing Then

				Return New MinSearch With {
				 .Title = ResultItem.Title, .LongURL = ResultItem.URL,
				 .AggregateLink = ResultItem.AggregateLink
				 }
			End If

			Return Nothing

		End Operator

		Property Title As String
		Property LongURL As String
		Property AggregateLink As String

	End Class

End Namespace