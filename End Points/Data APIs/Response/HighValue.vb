Imports System.ComponentModel
Imports Newtonsoft.Json

Namespace DataAPIs.Response

	Public Class HighValue
		Inherits ResponseBase

		'''<summary>
		''' Target (long) URL
		'''</summary>
		<JsonProperty("data")>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property HighValue As Model.HighValue

		ReadOnly Property Lang As String
			Get
				Return HighValue.Params.Lang
			End Get
		End Property

		ReadOnly Property Limit As Integer
			Get
				Return HighValue.Params.Limit
			End Get
		End Property

		ReadOnly Property Links As List(Of String)
			Get
				Return HighValue.Values
			End Get
		End Property

	End Class

End Namespace