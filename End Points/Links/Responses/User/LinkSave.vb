Imports System.ComponentModel
Imports Newtonsoft.Json

Namespace Links.Response.User

	''' <summary>
	''' Returns the saved link as a bitmark (with the short URL) in a user's history,
	''' with optional pre-set metadata.
	''' </summary>
	Public Class LinkSave
		Inherits ResponseBase

		'''<summary>
		''' The saved (short) link
		'''</summary>
		<JsonProperty("data")>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property Data As SavedLink

		'''<summary>
		''' The corresponding bitly aggregate link (global hash)
		'''</summary>
		ReadOnly Property AggregateLink As String
			Get
				Return Data.SavedLink.AggregateLink
			End Get
		End Property

		''' <summary>
		''' The  bitly link (short URL).
		''' </summary>
		ReadOnly Property ShortUrl As String
			Get
				Return Data.SavedLink.ShortUrl
			End Get
		End Property

		''' <summary>
		''' An echo back of the long_url request parameter.
		''' This may not always be equal to the URL requested,
		''' as some URL normalization may occur (e.g., due to encoding differences,
		''' or case differences in the domain).
		''' This long_url will always be functionally identical the the request parameter.
		''' </summary>
		ReadOnly Property LongUrl As String
			Get
				Return Data.SavedLink.LongUrl
			End Get
		End Property

		''' <summary>
		''' Indicates if the authenticated user is saving this link for the first time,
		''' or if the user has previously saved it.
		''' </summary>
		ReadOnly Property IsNewLink As Boolean
			Get
				Return Data.SavedLink.IsNewLink
			End Get
		End Property

		Class SavedLink
			<JsonProperty("link_save")>
		  <EditorBrowsable(EditorBrowsableState.Advanced)>
			Property SavedLink As Model.User.SavedLink
		End Class

	End Class

End Namespace