Imports adnmaster.Bitly.HttpClient
Imports System.Net
Imports adnmaster.Bitly.DataAPIs.Model.CustomSearch
Imports RestSharp

Namespace DataAPIs

	''' <summary>
	''' Search links receiving clicks across bitly by content, language, location, and more.
	''' </summary>
	''' <remarks>
	''' At bitly, they see about 4 billion clicks a month,
	''' and millions of new links a day. It's a data mine.
	''' Lookking through this enormous set of data to find interesting stuff [...] just build cool things!
	''' </remarks>
	Public NotInheritable Class SearchEndPoint

		Private ReadOnly _BitlyClient As BitlyClient
		''' <summary>
		''' The currently authenticated <see cref="T:adnmaster.Bitly.BitlyClient">BitlyClient</see>
		''' </summary>
		ReadOnly Property BitlyClient As BitlyClient
			Get
				Return _BitlyClient
			End Get
		End Property

		''' <summary>
		''' Creates the "Search" endpoint using
		''' the authenticated <see cref="T:adnmaster.Bitly.BitlyClient">BitlyClient</see>.
		''' </summary>
		Sub New(ByVal BitlyClient As BitlyClient)
			_BitlyClient = BitlyClient
		End Sub

		Private _Echo As Response.SearchResult

		''' <summary>
		''' At bitly, they see about 4 billion clicks a month,
		''' and millions of new links a day. It's a data mine.
		''' 
		''' Lookking through this enormous set of data to find interesting stuff [...] in order to build cool things!
		''' </summary>
		''' <param name="Query">
		''' String to query for.
		''' </param>
		''' <param name="Limit">
		''' The maximum number of links to return.
		''' </param>
		''' <param name="Lang">
		''' Favor results in this language (two letter ISO code).
		''' </param>
		''' <param name="Offset">
		''' Which result to start with (defaults to 0).
		''' </param>
		''' <param name="Domain">
		''' Restrict results to this web domain.
		''' </param>
		''' <param name="Cities">
		''' show links active in this city (ordered like country-state-city, e.g. us-il-chicago).
		''' </param>
		''' <param name="Fields">
		''' fields - 
		''' which fields to return in the response (comma-separated).
		''' May be any of: domain, initial_epoch, h2, h3, site,
		''' lastindexed, keywords, last_indexed_epoch, title, initial,
		''' summaryText, content, score, summaryTitle, type, description, cities,
		''' lang, url, referrer, aggregate_link, lastseen, page, ogtitle.
		''' 
		''' By default, all will be returned.
		''' </param>
		Function Search(ByVal Query As String, ByVal Limit As Integer,
		 Optional ByVal Lang As String = Nothing,
		 Optional ByVal Offset As Integer = 0,
		 Optional ByVal Domain As String = Nothing,
		 Optional ByVal Cities As String() = Nothing,
		 Optional ByVal Fields As SearchField() = Nothing) As Response.SearchResult

			Dim UrlSegs As New List(Of KeyValuePair(Of String, String))
			UrlSegs.AddRange({
			  New KeyValuePair(Of String, String)("query", Query),
			   New KeyValuePair(Of String, String)("limit", Limit.ToString)
			  })

			If (Not String.IsNullOrEmpty(Lang)) AndAlso Lang.Length = 2 Then
				UrlSegs.Add(New KeyValuePair(Of String, String)("lang", Lang))
			End If

			If Offset > 0 Then
				UrlSegs.Add(New KeyValuePair(Of String, String)("offset", Offset.ToString))
			End If

			If Not String.IsNullOrEmpty(Domain) Then
				UrlSegs.Add(New KeyValuePair(Of String, String)("domain", Domain))
			End If

			If Cities IsNot Nothing AndAlso Cities.Count > 0 Then
				UrlSegs.Add(New KeyValuePair(Of String, String)("cities", String.Join("-", Cities)))
			End If

			If Fields IsNot Nothing AndAlso Fields.Count > 0 Then
				If (Fields.Count = 1 AndAlso Fields.Single <> SearchField.Default) OrElse
				 Fields.Count <> 1 Then
					UrlSegs.Add(New KeyValuePair(Of String, String)("fields",
					String.Join(",", Fields.Select(Function(F) F.ToString))))
				End If
			End If

			Dim Response As RestJsonResponse =
			 _BitlyClient.ExecuteRequest(Method.GET, "search", UrlSegs.ToArray)

			If Response.OK Then
				_Echo = Response.Content.FromJson(Of Response.SearchResult)()
				If _Echo.StatusCode <> HttpStatusCode.OK Then
					Throw New Exception(_Echo.StatusDesription)
				End If
			Else
				Throw New RestException(Response)
			End If

			Return _Echo

		End Function

		''' <summary>
		''' Minimum search (Title, long url and aggregate (short) link) 
		''' </summary>
		Function MinSearch(ByVal Query As String,
		 ByVal Limit As Integer, ByVal Offset As Integer) As IEnumerable(Of MinSearch)

			Dim List As New List(Of MinSearch)

			Search(Query, Limit, , Offset, , ,
			  GetMinSearchFields).SearchResult.Results.ForEach(
			  Sub(R)
				  List.Add(R)
			  End Sub)

			Return List

		End Function
		''' <summary>
		''' Domain search (Title, long url, aggregate (short) link,
		''' domain, site, score, referrer, last indexed and last seen) 
		''' </summary>
		Function SearchDomain(ByVal Query As String,
		 ByVal Limit As Integer, ByVal Offset As Integer) As List(Of SearchDomain)

			Dim List As New List(Of SearchDomain)

			Search(Query, Limit, , Offset, , ,
			  GetDomainSpecificFields).SearchResult.Results.ForEach(
			  Sub(R)
				  List.Add(R)
			  End Sub)

			Return List

		End Function
		''' <summary>
		''' Location search (Title, long ur, aggregate (short) link and cities) 
		''' </summary>
		Function SearchLocation(ByVal Query As String,
		 ByVal Limit As Integer, ByVal Offset As Integer) As IEnumerable(Of SearchLocation)

			Dim List As New List(Of SearchLocation)

			Search(Query, Limit, , Offset, , ,
		   GetLocationSpecificFields).SearchResult.Results.ForEach(
			  Sub(R)
				  List.Add(R)
			  End Sub)

			Return List

		End Function
		''' <summary>
		''' Culture search (Title, long ur, aggregate (short) link and lang (two letter ISO code)) 
		''' </summary>
		Function SearchCulture(ByVal Query As String,
		 ByVal Limit As Integer, ByVal Offset As Integer) As IEnumerable(Of SearchCulture)

			Dim List As New List(Of SearchCulture)

			Search(Query, Limit, , Offset, , ,
			  GetCultureSpecificFields).SearchResult.Results.ForEach(
			  Sub(R)
				  List.Add(R)
			  End Sub)

			Return List

		End Function

		Function GetMinSearchFields() As SearchField()
			Return GetFieldsArray(SearchField.title, SearchField.aggregate_link, SearchField.url)
		End Function

		Function GetDomainSpecificFields() As SearchField()

			Dim List As New List(Of SearchField)(GetMinSearchFields)

			List.AddRange(
			 GetFieldsArray(
			   SearchField.domain, SearchField.site, SearchField.score,
			 SearchField.referrer, SearchField.initial, SearchField.lastseen, SearchField.lastindexed)
			 )

			Return List.ToArray

		End Function

		Function GetCultureSpecificFields() As SearchField()

			Dim List As New List(Of SearchField)(GetMinSearchFields)
			List.Add(SearchField.lang)

			Return List.ToArray

		End Function

		Function GetLocationSpecificFields() As SearchField()

			Dim List As New List(Of SearchField)(GetMinSearchFields)
			List.Add(SearchField.cities)

			Return List.ToArray

		End Function

		Function GetAllFieldsParam() As SearchField()
			Return {SearchField.Default}
		End Function

		Function GetFieldsArray(ByVal ParamArray Fields As SearchField()) As SearchField()
			Return Fields
		End Function

	End Class

End Namespace