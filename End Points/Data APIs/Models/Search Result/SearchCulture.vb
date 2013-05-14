Namespace DataAPIs.Model.CustomSearch

	''' <summary>
	''' Culture search (Title, long ur,
	''' aggregate (short) link and lang (two
	''' letter ISO code))
	''' </summary>
	''' <remarks>
	''' Use the results to perform other
	''' searches see <see
	''' cref="adnmaster.Bitly.DataAPIs.SearchEndPoint.Search">Search</see>
	''' </remarks>
	''' <seealso
	''' cref="adnmaster.Bitly.DataAPIs.SearchEndPoint.Search">Search</seealso>
	Public Class SearchCulture
		Inherits MinSearch

		Public Overloads Shared Widening Operator CType(ByVal ResultItem As SearchResult.Result) As SearchCulture

			If ResultItem IsNot Nothing Then

				Dim C As New SearchCulture
				With C

					.Title = ResultItem.Title
					.LongURL = ResultItem.URL
					.AggregateLink = ResultItem.AggregateLink

					.Lang = ResultItem.Lang
				End With

				Return C

			End If

			Return Nothing

		End Operator

		''' <summary>
		''' The two letter ISO code.
		''' </summary>
		Property Lang As String

	End Class

End Namespace