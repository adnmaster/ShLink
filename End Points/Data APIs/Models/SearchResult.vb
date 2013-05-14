Imports Newtonsoft.Json
Imports System.ComponentModel

Namespace DataAPIs.Model

	''' <summary>
	''' At bitly, they see about 4 billion clicks a month,
	''' and millions of new links a day. It's a data mine.
	''' Lookking through this enormous set of data to find interesting stuff [...] in order to build cool things!
	''' </summary>
	Public Class SearchResult

		<JsonProperty("results")>
		Property Results As List(Of Result)


		Class Result

			<JsonProperty("domain")>
			Property Domain As String

			<JsonProperty("initial_epoch")>
			<EditorBrowsable(EditorBrowsableState.Advanced)>
			Property InitialEpoch As String

			<JsonProperty("h2")>
			Property H2 As List(Of String)

			<JsonProperty("h3")>
			Property H3 As List(Of String)

			<JsonProperty("site")>
			Property Site As String

			<JsonProperty("lastindexed")>
			<EditorBrowsable(EditorBrowsableState.Advanced)>
			Property LastIndexed As String

			<JsonProperty("keywords")>
			Property Keywords As String

			<JsonProperty("last_indexed_epoch")>
			<EditorBrowsable(EditorBrowsableState.Advanced)>
			Property LastIndexedEpoch As String

			<JsonProperty("title")>
			Property Title As String

			<JsonProperty("initial")>
			<EditorBrowsable(EditorBrowsableState.Advanced)>
			Property Initial As String

			<JsonProperty("summaryText")>
			Property SummaryText As String

			<JsonProperty("content")>
			Property Content As String

			<JsonProperty("score")>
			Property Score As Single

			<JsonProperty("summaryTitle")>
			Property SummaryTitle As String

			<JsonProperty("type")>
			Property Type As String

			<JsonProperty("description")>
			Property Description As String

			<JsonProperty("cities")>
			Property Cities As String

			<JsonProperty("lang")>
			Property Lang As String

			<JsonProperty("url")>
			Property URL As String

			<JsonProperty("referrer")>
			Property Referrer As List(Of String)

			<JsonProperty("aggregate_link")>
			Property AggregateLink As String

			<JsonProperty("lastseen")>
			   <EditorBrowsable(EditorBrowsableState.Advanced)>
			Property LastSeen As String

			<JsonProperty("page")>
			Property Page As String

			<JsonProperty("ogtitle")>
			Property Ogtitle As String

			''' <summary>
			''' <see
			''' cref="P:adnmaster.Bitly.DataAPIs.Model.SearchResult.Result.Initial">See Initial</see>
			''' or 
			''' <see
			''' cref="P:adnmaster.Bitly.DataAPIs.Model.SearchResult.Result.InitialEpoch">InitialDate</see>
			''' </summary>
			ReadOnly Property InitialEpochDate As Date
				Get
					Return CInt(InitialEpoch).EpochToDate
				End Get
			End Property

			''' <summary>
			''' <see
			''' cref="P:adnmaster.Bitly.DataAPIs.Model.SearchResult.Result.LastIndexed">See LastIndexed</see>
			''' or 
			''' <see
			''' cref="P:adnmaster.Bitly.DataAPIs.Model.SearchResult.Result.InitialEpoch">LastIndexedDate</see>
			''' </summary>
			ReadOnly Property LastIndexedEpochDate As Date
				Get
					Return CInt(LastIndexedEpoch).EpochToDate
				End Get
			End Property

			''' <summary>
			''' <see
			''' cref="P:adnmaster.Bitly.DataAPIs.Model.SearchResult.Result.Initial">See Initial</see>
			''' or 
			''' <see
			''' cref="P:adnmaster.Bitly.DataAPIs.Model.SearchResult.Result.InitialEpoch">InitialEpoch</see>
			''' </summary>
			ReadOnly Property InitialDate As Date
				Get
					Return Initial.ParseDateTime
				End Get
			End Property

			''' <summary>
			''' <see
			''' cref="P:adnmaster.Bitly.DataAPIs.Model.SearchResult.Result.LastIndexed">See LastIndexed</see>
			''' or 
			''' <see
			''' cref="P:adnmaster.Bitly.DataAPIs.Model.SearchResult.Result.InitialEpoch">LastIndexedEpoch</see>
			''' </summary>
			ReadOnly Property LastIndexedDate As Date
				Get
					Return LastIndexed.ParseDateTime
				End Get
			End Property

			''' <summary>
			''' <see
			''' cref="P:adnmaster.Bitly.DataAPIs.Model.SearchResult.Result.LastSeen">See LastSeen</see>
			''' </summary>
			ReadOnly Property LastSeenDate As Date
				Get
					Return LastSeen.ParseDateTime
				End Get
			End Property

		End Class

	End Class

End Namespace