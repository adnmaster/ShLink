Imports RestSharp
Imports adnmaster.Bitly.Serialization

Namespace HttpClient

	Friend Class GenericRestClient

		Public Event RequestSending(ByVal Request As RestRequest)
		Public Event RequestSent(ByVal Response As IRestResponse)

		Property RestSharpRestClient As RestClient
		Property AllRequestsWideHeaders As List(Of HttpHeader)

		Sub New()

			_RestSharpRestClient = New RestClient
			AllRequestsWideHeaders = New List(Of HttpHeader)

			SetTimeout()

		End Sub

		''' <summary>
		''' For other Authentication Methods
		''' </summary>
		''' <remarks></remarks>
		Sub New(ByVal BaseURL As String)


			_RestSharpRestClient = New RestClient
			AllRequestsWideHeaders = New List(Of HttpHeader)

			SetBaseURL(BaseURL)
			SetTimeout()

		End Sub

		''' <summary>
		''' Username and Password are for Http Basic Authentication.
		''' Ignore for other Authentication Methods
		''' </summary>
		Sub New(ByVal BaseURL As String, ByVal Username As String, ByVal Password As String)

			_RestSharpRestClient = New RestClient
			AllRequestsWideHeaders = New List(Of HttpHeader)

			SetBaseURL(BaseURL)
			SetBasicAuthenticationCredentials(Username, Password)

			SetTimeout()

		End Sub

		''' <summary>
		''' If Username and Password are Passed In the URL For Http Basic Authentication.
		''' #Ignore for other Authentication Methods
		''' </summary>
		Sub New(ByVal BaseURL As Uri)

			Dim URL As String = BaseURL.GetComponents(UriComponents.SchemeAndServer,
			   UriFormat.SafeUnescaped)

			Dim UserInfo As String() = BaseURL.GetComponents(UriComponents.UserInfo,
				UriFormat.SafeUnescaped).Split(":"c)

			Dim Username As String = String.Empty
			Dim Password As String = String.Empty

			If UserInfo.Length = 2 Then
				Username = UserInfo(0)
				Password = UserInfo(1)
			End If

			_RestSharpRestClient = New RestClient
			AllRequestsWideHeaders = New List(Of HttpHeader)

			SetBaseURL(URL)
			SetBasicAuthenticationCredentials(Username, Password)

			SetTimeout()

		End Sub

		Private Sub SetTimeout()

			Dim TimeoutMinutes As Integer = 10
			Dim TimeoutSeconds As Integer = 1

			Dim TimeoutMilliseconds As Double =
			 New TimeSpan(0, TimeoutMinutes, TimeoutSeconds).TotalMilliseconds

			_RestSharpRestClient.Timeout = CInt(TimeoutMilliseconds)

		End Sub

		Sub SetBaseURL(ByVal BaseURL As String)

			RestSharpRestClient.BaseUrl = BaseURL

		End Sub

		Sub SetBasicAuthenticationCredentials(ByVal Username As String, ByVal Password As String)

			If String.IsNullOrWhiteSpace(Username) OrElse
			String.IsNullOrWhiteSpace(Password) Then
				Exit Sub
			End If

			RestSharpRestClient.Authenticator = New HttpBasicAuthenticator(Username, Password)

		End Sub

		Overridable Function PrepareRequest(ByVal Method As Method, ByVal Resource As String,
		  ByVal ParamArray Headers() As HttpHeader) As RestRequest

			Dim RestRequest As New RestRequest(Method)
			RestRequest.RequestFormat = DataFormat.Json

			'RestRequest.AddHeader("Content-Type", "application/json")

			If String.IsNullOrWhiteSpace(Resource) Then
				Throw New ArgumentException("'Resource' is null, empty or whitespace", "Resource")
			End If

			RestRequest.Resource = Resource

			If Headers IsNot Nothing Then
				For Each Header In Headers
					RestRequest.AddHeader(Header.Name, Header.Value)
				Next
			End If

			If AllRequestsWideHeaders.Count > 0 Then
				AllRequestsWideHeaders.ForEach(
				 Sub(H) RestRequest.AddHeader(H.Name, H.Value))
			End If

			Return RestRequest

		End Function

		Function ExecuteRequest(ByVal Method As Method, ByVal Resource As String,
		ByVal ParamArray Headers() As HttpHeader) As RestJsonResponse

			Dim RestRequest As RestRequest = PrepareRequest(Method, Resource, Headers)

			Dim Response As New RestJsonResponse(Execute(RestRequest))
			Return Response

		End Function

		Function ExecuteRequest(ByVal Method As Method, ByVal Resource As String,
		  ByVal Headers As HttpHeader(),
		  ByVal ParamArray Parameters() As Parameter) As RestJsonResponse

			Dim RestRequest As RestRequest = PrepareRequest(Method, Resource, Headers)
			If Not Parameters Is Nothing Then
				RestRequest.Parameters.AddRange(Parameters)
			End If

			Dim Response As New RestJsonResponse(Execute(RestRequest))
			Return Response

		End Function

		Function ExecuteRequest(ByVal Method As Method, ByVal Resource As String,
		  ByVal Body As String, ByVal ParamArray Headers() As HttpHeader) As RestJsonResponse

			Dim RestRequest As RestRequest = PrepareRequest(Method, Resource, Headers)

			If Not String.IsNullOrWhiteSpace(Body) Then
				RestRequest.AddParameter("application/json", Body, ParameterType.RequestBody)
			End If

			Dim Response As New RestJsonResponse(Execute(RestRequest))
			Return Response

		End Function

		Function ExecuteRequest(ByVal Method As Method, ByVal Resource As String,
		 ByVal Body As Object, ByVal ParamArray Headers() As HttpHeader) As RestJsonResponse

			Dim RestRequest As RestRequest = PrepareRequest(Method, Resource, Headers)

			Dim JsonBody As String = Json.Serialize(Body)
			RestRequest.AddParameter("application/json", JsonBody, ParameterType.RequestBody)

			Dim Response As New RestJsonResponse(Execute(RestRequest))
			Return Response

		End Function

		Protected Function Execute(ByVal Request As RestRequest) As IRestResponse

			RaiseEvent RequestSending(Request)
			Dim Response As IRestResponse = RestSharpRestClient.Execute(Request)
			RaiseEvent RequestSent(Response)

			Return Response

		End Function

		Function GetRawBytes(ByVal Resource As String) As RestBsonResponse

			Dim RestRequest As RestRequest = PrepareRequest(Method.GET, Resource)

			Dim Response As New RestBsonResponse(RestSharpRestClient.Execute(RestRequest))
			Return Response

		End Function

		Function DownloadData(ByVal Resource As String) As Byte()

			Dim RestRequest As RestRequest = PrepareRequest(Method.GET, Resource)
			Return RestSharpRestClient.DownloadData(RestRequest)

		End Function

		Public Shared Function CombineResources(ByVal ParamArray Resources() As String) As String
			Return String.Join("/", Resources)
		End Function

	End Class

End Namespace