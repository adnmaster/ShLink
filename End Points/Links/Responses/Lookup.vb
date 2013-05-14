Imports System.ComponentModel
Imports Newtonsoft.Json

Namespace Links.Response

	''' <summary>
	''' This is used to query for a bitly link based on a long URL.
	''' </summary>
	Public Class Lookup
		Inherits ResponseBase

		'''<summary>
		''' Query result for a bitly link based on a long URL.
		'''</summary>
		<JsonProperty("data")>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property Data As LookupData

		'''<summary>
		''' <see cref="List(Of Model.AggregateLink)"/> of all lookup results
		'''</summary>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		ReadOnly Property LinkLookups As List(Of Model.LinkLookup)
			Get
				Return Data.LinkLookups
			End Get
		End Property

		''' <summary>
		''' First or default <see cref="Model.LinkLookup"/>
		''' </summary>
			<EditorBrowsable(EditorBrowsableState.Advanced)>
		ReadOnly Property LinkLookup As Model.LinkLookup
			Get
				Return Data.LinkLookups.FirstOrDefault
			End Get
		End Property

		''' <summary>
		''' The corresponding bitly aggregate link (global hash).
		''' </summary>
		ReadOnly Property AggregateLink As String
			Get
				Return LinkLookup.AggregateLink
			End Get
		End Property

		''' <summary>
		''' An echo back of the (long) url parameter.
		''' </summary>
		ReadOnly Property Url As String
			Get
				Return LinkLookup.Url
			End Get
		End Property

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Public Class LookupData
			<JsonProperty("link_lookup")>
			Property LinkLookups As List(Of Model.LinkLookup)
		End Class

	End Class

End Namespace