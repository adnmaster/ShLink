Imports RestSharp

Namespace HttpClient

	Public Class RestJsonResponse

		Sub New(ByVal Response As IRestResponse)

			If Response.StatusCode = Net.HttpStatusCode.OK Then
				_OK = True
			Else
				_OK = False
			End If

			_RestResponse = Response
			_Content = Response.Content

		End Sub

		Property OK As Boolean
		Property Content As String
		Property RestResponse As IRestResponse

	End Class

End Namespace