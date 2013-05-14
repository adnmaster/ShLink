Imports System.ComponentModel
Imports Newtonsoft.Json

Namespace Links.Response.User

	''' <summary>
	''' Returns the changed link (just an echo of the short link, after an edit)
	''' in a user's history.
	''' </summary>
	Public Class LinkEdit
		Inherits ResponseBase

		'''<summary>
		''' An echo back of the edited bitly link.
		'''</summary>
		<JsonProperty("data")>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property Data As EditedLink

		'''<summary>
		''' An echo back of the edited bitly link.
		'''</summary>
		ReadOnly Property Link As String
			Get
				Return Data.EditedLink.Link
			End Get
		End Property

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Class EditedLink
			<JsonProperty("link_edit")>
			Property EditedLink As Model.User.EditedLink
		End Class

	End Class

End Namespace