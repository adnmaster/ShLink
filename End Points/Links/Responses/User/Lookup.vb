Imports System.ComponentModel
Imports Newtonsoft.Json

Namespace Links.Response.User

	''' <summary>
	''' This is used to query for a bitly link shortened by the authenticated user based on a long URL.
	''' </summary>
	Public Class Lookup
		Inherits ResponseBase

		'''<summary>
		''' Query result for a bitly link shortened by the authenticated user based on a long URL.
		'''</summary>
		<JsonProperty("data")>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property Data As LookupData

		''' <summary>
		''' <see cref="T:System.Collections.Generic.List`1">List</see> of all <see
		''' cref="T:adnmaster.Bitly.Links.Model.LinkLookup">Model.LinkLookup</see> results
		''' </summary>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		ReadOnly Property LinkLookups As List(Of Model.User.LinkLookup)
			Get
				Return Data.LinkLookups
			End Get
		End Property

		''' <summary>
		''' First or default <see cref="Model.User.LinkLookup"/>
		''' </summary>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		ReadOnly Property LinkLookup As Model.User.LinkLookup
			Get
				Return Data.LinkLookups.FirstOrDefault
			End Get
		End Property

		'''<summary>
		''' The corresponding bitly aggregate link (global hash)
		'''</summary>
		ReadOnly Property AggregateLink As String
			Get
				Return LinkLookup.AggregateLink
			End Get
		End Property

		''' <summary>
		''' The corresponding bitly link (aka short URL).
		''' </summary>
		ReadOnly Property ShortUrl As String
			Get
				Return LinkLookup.ShortUrl
			End Get
		End Property

		''' <summary>
		''' An echo back of the url parameter.
		''' </summary>
		ReadOnly Property LongUrl As String
			Get
				Return LinkLookup.LongUrl
			End Get
		End Property

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Public Class LookupData
			<JsonProperty("link_lookup")>
			Property LinkLookups As List(Of Model.User.LinkLookup)
		End Class

	End Class

End Namespace