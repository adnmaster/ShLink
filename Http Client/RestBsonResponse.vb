Imports RestSharp

Namespace HttpClient

	Public Class RestBsonResponse
		Inherits RestJsonResponse

		Sub New(ByVal Response As IRestResponse)
			MyBase.New(Response)
			_Content = Response.RawBytes
		End Sub

		Shadows Property Content As Byte()

	End Class

End Namespace