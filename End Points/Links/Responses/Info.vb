Imports System.ComponentModel
Imports Newtonsoft.Json

Namespace Links.Response

	''' <summary>
	''' This is used to return the page title for a given bitly link.
	''' </summary>
	Public Class Info
		Inherits ResponseBase

		'''<summary>
		''' Page title for a given bitly link
		'''</summary>
		<JsonProperty("data")>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property Data As InfoData

		'''<summary>
		''' <see cref="List(Of Model.PageTitle)"/> of all page titles
		'''</summary>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		ReadOnly Property PageTitles As List(Of Model.PageTitle)
			Get
				Return Data.PageTitles
			End Get
		End Property

		''' <summary>
		''' First or default <see cref="Model.PageTitle"/>
		''' </summary>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		ReadOnly Property PageTitle As Model.PageTitle
			Get
				Return Data.PageTitles.FirstOrDefault
			End Get
		End Property


		''' <summary>
		''' The epoch timestamp when this bitly link was created
		''' </summary>
		ReadOnly Property CreatedAt As Date
			Get
				Return PageTitle.CreatedAt.EpochToDate
			End Get
		End Property

		''' <summary>
		''' The bitly username that originally shortened this link, if the link is public. Otherwise, null.
		''' </summary>
		ReadOnly Property CreatedBy As String
			Get
				Return PageTitle.CreatedBy
			End Get
		End Property

		''' <summary>
		''' The corresponding bitly aggregate identifier.
		''' </summary>
		ReadOnly Property GlobalHash As String
			Get
				Return PageTitle.GlobalHash
			End Get
		End Property

		''' <summary>
		''' This is an echo back of the shortUrl request parameter.
		''' </summary>
		ReadOnly Property ShortUrl As String
			Get
				Return PageTitle.ShortUrl
			End Get
		End Property

		''' <summary>
		''' The HTML page title for the destination page (when available).
		''' </summary>
		ReadOnly Property Title As String
			Get
				Return PageTitle.Title
			End Get
		End Property

		''' <summary>
		''' The corresponding bitly user identifier.
		''' </summary>
		ReadOnly Property UserHash As String
			Get
				Return PageTitle.UserHash
			End Get
		End Property

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Public Class InfoData
			<JsonProperty("info")>
			Property PageTitles As List(Of Model.PageTitle)
		End Class

	End Class

End Namespace
