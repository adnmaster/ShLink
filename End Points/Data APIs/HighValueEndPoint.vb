Imports adnmaster.Bitly.HttpClient
Imports System.Net

Namespace DataAPIs

	''' <summary>
	''' Returns a specified number of "high-value" bitly links
	''' that are popular across bitly at this particular moment
	''' </summary>
	Public NotInheritable Class HighValueEndPoint
		
		Private ReadOnly _BitlyClient As BitlyClient
		''' <summary>
		''' The currently authenticated <see cref="T:adnmaster.Bitly.BitlyClient">BitlyClient</see>
		''' </summary>
		ReadOnly Property BitlyClient As BitlyClient
			Get
				Return _BitlyClient
			End Get
		End Property

		Private _Echo As Response.HighValue

		'''<summary>
		''' gets a specified number of "high-value" bitly links
		''' that are popular across bitly at this particular moment.
		''' </summary>
		''' <param name="Limit">
		''' The maximum number of high-value links to return.
		''' </param>
		Function ExecuteRequest(ByVal Limit As Integer) As Response.HighValue

			Dim Response As RestJsonResponse =
			 _BitlyClient.ExecuteRequest(RestSharp.Method.GET, "highvalue",
			  New KeyValuePair(Of String, String)("limit", Limit.ToString))

			If Response.OK Then
				_Echo = Response.Content.FromJson(Of Response.HighValue)()
				If _Echo.StatusCode <> HttpStatusCode.OK Then
					Throw New Exception(_Echo.StatusDesription)
				End If
			Else
				Throw New RestException(Response)
			End If

			Return _Echo

		End Function

		''' <summary>
		''' Creates the "HighValue" endpoint using
		''' the authenticated <see cref="T:adnmaster.Bitly.BitlyClient">BitlyClient</see>.
		''' </summary>
		Sub New(ByVal BitlyClient As BitlyClient)
			_BitlyClient = BitlyClient
		End Sub

		ReadOnly Property Lang As String
			Get
				Return _Echo.Lang
			End Get
		End Property

		ReadOnly Property Limit As Integer
			Get
				Return _Echo.Limit
			End Get
		End Property

		ReadOnly Property Links As List(Of String)
			Get
				Return _Echo.Links
			End Get
		End Property

		Public Shared Widening Operator CType(ByVal HighValueEndPoint As HighValueEndPoint) As List(Of String)

			If HighValueEndPoint IsNot Nothing Then Return HighValueEndPoint.Links
			Return Nothing

		End Operator

	End Class

End Namespace