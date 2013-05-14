Imports System.Net
Imports System.ComponentModel

Namespace HttpClient

	Public Class RestException
		Inherits Exception

#Region " .ctor "

		Sub New(ByVal Response As RestJsonResponse)

			MyBase.New(Response.Content)

			_StatusDescription = Response.RestResponse.StatusDescription
			_HttpStatusCode = Response.RestResponse.StatusCode

			Try
				_RestRequest = Response.RestResponse.Request
				_RequestVerb = _RestRequest.Method.ToString
				_RequestUri = _RestRequest.Resource

				_ResponseUri = Response.RestResponse.ResponseUri

			Catch
			End Try

		End Sub

		Sub New(ByVal Response As RestJsonResponse, ByVal InnerException As Exception)

			MyBase.New(Response.Content, InnerException)

			_StatusDescription = Response.RestResponse.StatusDescription
			_HttpStatusCode = Response.RestResponse.StatusCode

			_ResponseUri = Response.RestResponse.ResponseUri

		End Sub

		Sub New(ByVal Message As String, ByVal StatusDescription As String, ByVal HttpStatusCode As HttpStatusCode,
		Optional ByVal ResponseUri As Uri = Nothing)

			MyBase.New(Message)

			_StatusDescription = StatusDescription
			_HttpStatusCode = HttpStatusCode

			_ResponseUri = ResponseUri

		End Sub

		Sub New(ByVal Message As String, ByVal InnerException As Exception,
		  ByVal StatusDescription As String, ByVal HttpStatusCode As HttpStatusCode,
		  Optional ByVal ResponseUri As Uri = Nothing)

			MyBase.New(Message, InnerException)

			_StatusDescription = StatusDescription
			_HttpStatusCode = HttpStatusCode

			_ResponseUri = ResponseUri

		End Sub
#End Region

		Property HttpStatusCode As HttpStatusCode
		Property StatusDescription As String

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property RequestVerb As String

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property RequestUri As String

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property RestRequest As RestSharp.IRestRequest

		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property ResponseUri As Uri

	End Class

End Namespace