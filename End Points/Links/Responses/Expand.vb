Imports Newtonsoft.Json
Imports System.ComponentModel

Namespace Links.Response

	''' <summary>
	''' Given a bitly URL or hash (or multiple), returns the target (long) URL.
	''' </summary>
	Public Class Expand
		Inherits ResponseBase

		'''<summary>
		''' Target (long) URL
		'''</summary>
		<JsonProperty("data")>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property Data As ExpandData

		'''<summary>
		''' A <see cref="IList">List of </see>
		''' <see cref="Model.TargetUrl"/> of all target (long) URLs
		'''</summary>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		ReadOnly Property TargetUrls As List(Of Model.TargetUrl)
			Get
				Return Data.TargetUrls
			End Get
		End Property

		''' <summary>
		''' First or default <see cref="TargetUrl"/>
		''' </summary>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		ReadOnly Property TargetUrl As Model.TargetUrl
			Get
				Return Data.TargetUrls.FirstOrDefault
			End Get
		End Property

		''' <summary>
		''' The corresponding bitly aggregate identifier.
		''' </summary>
		ReadOnly Property GlobalHash As String
			Get
				Return TargetUrl.GlobalHash
			End Get
		End Property

		''' <summary>
		''' The URL that the requested short url or hash points to.
		''' </summary>
		ReadOnly Property LongUrl As String
			Get
				Return TargetUrl.LongUrl
			End Get
		End Property

		''' <summary>
		''' An echo back of the short url request parameter.
		''' </summary>
		ReadOnly Property ShortUrl As String
			Get
				Return TargetUrl.ShortUrl
			End Get
		End Property

		''' <summary>
		''' The corresponding bitly user identifier.
		''' </summary>
		ReadOnly Property UserHash As String
			Get
				Return TargetUrl.UserHash
			End Get
		End Property

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Public Class ExpandData
			<JsonProperty("expand")>
			Property TargetUrls As List(Of Model.TargetUrl)
		End Class


	End Class

End Namespace