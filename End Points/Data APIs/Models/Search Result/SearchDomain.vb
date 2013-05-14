Namespace DataAPIs.Model.CustomSearch

	''' <summary>
	''' Domain search (Title, long url,
	''' aggregate (short) link, domain, site,
	''' score, referrer, last indexed and last
	''' seen)
	''' </summary>
	''' <remarks>
	''' Use the results to perform other
	''' searches see
	''' <see
	''' cref="adnmaster.Bitly.DataAPIs.SearchEndPoint.Search">Search</see>
	''' </remarks>
	''' <seealso
	''' cref="adnmaster.Bitly.DataAPIs.SearchEndPoint.Search">Search</seealso>
	Public Class SearchDomain
		Inherits MinSearch

		Public Overloads Shared Widening Operator CType(ByVal ResultItem As SearchResult.Result) As SearchDomain

			If ResultItem IsNot Nothing Then

				Dim C As New SearchDomain()
				With C

					.Title = ResultItem.Title
					.LongURL = ResultItem.URL
					.AggregateLink = ResultItem.AggregateLink

					.Domain = ResultItem.Domain

					.Site = ResultItem.Site
					.Score = ResultItem.Score
					.Referrer = ResultItem.Referrer

					.Initial = ResultItem.InitialDate
					.LastIndexed = ResultItem.LastIndexedDate
					.LastSeen = ResultItem.LastSeenDate

				End With

				Return C

			End If

			Return Nothing

		End Operator

		Property Domain As String

		Property Site As String
		Property Score As Single
		Property Referrer As List(Of String)

		Property Initial As Date
		Property LastSeen As Date
		Property LastIndexed As Date

		Property KeyWords As New List(Of String)

	End Class

End Namespace