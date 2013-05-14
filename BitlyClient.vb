Imports adnmaster.Bitly.HttpClient
Imports adnmaster.Bitly.DataAPIs
Imports RestSharp
Imports adnmaster.Bitly.Links

'TODO : Add localization support

Public Class BitlyClient

#Region " .ctor "

	Private ReadOnly _RootUri As String = "https://api-ssl.bitly.com"

	Public Sub New(ByVal UserName As String, ByVal Password As String,
	   Optional ByVal ClientID As String = Nothing)
		_OAuthClient = New GenericRestClient(_RootUri, UserName, Password)
		Init(ClientID)
		End Sub

	Public Sub New(ByVal ClientID As String)
		_OAuthClient = New GenericRestClient(_RootUri)
		Init(ClientID)
	End Sub

	Private Sub Init(ByVal ClientID As String)

		_WebClient = New GenericRestClient(_RootUri & "/v3")

		If Not String.IsNullOrEmpty(ClientID) Then
			_OAuthClient.AllRequestsWideHeaders.Add(New HttpHeader() With {.Name = "client_id", .Value = ClientID})
		End If

		_Links = New LinksEndPoint(Me)
		_HighValue = New HighValueEndPoint(Me)
		_Search = New SearchEndPoint(Me)

	End Sub

#End Region

#Region " Client "

	Private WithEvents _OAuthClient As GenericRestClient
	Friend ReadOnly Property OAuthClient As GenericRestClient
		Get
			Return _OAuthClient
		End Get
	End Property

	Private WithEvents _WebClient As GenericRestClient
	Friend ReadOnly Property WebClient As GenericRestClient
		Get
			Return _WebClient
		End Get
	End Property

	Private _AccessToken As String
	Friend ReadOnly Property AccessToken As String
		Get
			Return _AccessToken
		End Get
	End Property

	''' <summary>
	''' Sets the OAuth access token for future requests
	''' </summary>
	Sub ApplyAccessToken(ByVal Value As String)
		_AccessToken = Value
	End Sub

	''' <summary>
	''' Requests the OAuth access token for the specified user (set for future requests)
	''' </summary>
	Function OAuthAccessToken() As String

		'curl -u "username:password" -X POST "https://api-ssl.bitly.com/oauth/access_token"

		Dim Response As RestJsonResponse =
		 OAuthClient.ExecuteRequest(Method.POST, "oauth/access_token")

		If Response.OK Then
			_AccessToken = Response.Content
			Return _AccessToken
		Else
			Throw New RestException(Response)
		End If

	End Function

#End Region

#Region " Service EndPoints "

	''' <summary>
	''' The Links endpoint.
	''' </summary>
	Private _Links As LinksEndPoint
	ReadOnly Property Links As LinksEndPoint
		Get
			Return _Links
		End Get
	End Property

	Private _HighValue As HighValueEndPoint
	ReadOnly Property HighValue() As HighValueEndPoint
		Get
			Return _HighValue
		End Get
	End Property

	Private _Search As SearchEndPoint
	ReadOnly Property Search As SearchEndPoint
		Get
			Return _Search
		End Get
	End Property

#End Region

#Region " Execute (RestSharp) "

	Friend Function ExecuteRequest(ByVal Verb As Method, ByVal Resource As String,
	 ByVal ParamArray UrlSegments() As KeyValuePair(Of String, String)) As RestJsonResponse

		Dim Segs As New List(Of Parameter)
		Segs.Add(New Parameter() With {.Name = "access_token", .Type = ParameterType.GetOrPost, .Value = _AccessToken})

		Array.ForEach(UrlSegments,
		 Sub(Seg)
			 Segs.Add(
			  New Parameter() With {.Name = Seg.Key, .Type = ParameterType.GetOrPost, .Value = Seg.Value})
		 End Sub)

		Return _WebClient.ExecuteRequest(Verb, Resource, {}, Segs.ToArray)

	End Function

	'Friend Function ExecuteRequest(ByVal Verb As Method, ByVal Resource As String) As RestJsonResponse
	'	Return _WebClient.ExecuteRequest(Verb, Resource, {},
	'	  New Parameter() With {.Name = "access_token", .Type = ParameterType.GetOrPost, .Value = _AccessToken})
	'End Function

	'Friend Function ExecuteRequest(ByVal Verb As Method, ByVal Resource As String,
	' ByVal Body As Object) As RestJsonResponse
	'	Return _WebClient.ExecuteRequest(Verb, Resource, {},
	'	   New Parameter() With {.Name = "Body", .Type = ParameterType.GetOrPost, .Value = Body},
	'	   New Parameter() With {.Name = "access_token", .Type = ParameterType.GetOrPost, .Value = _AccessToken})
	'End Function

#End Region

#Region " DEBUG "
#If DEBUG Then

	Private Sub _WebClient_RequestSending(ByVal Request As RestRequest) Handles _WebClient.RequestSending
		Debug.WriteLine(Request.Resource)
	End Sub

	Private Sub _WebClient_RequestSent(ByVal Response As IRestResponse) Handles _WebClient.RequestSent

		Dim HttpDebugDir As String = "HTTP_DEBUG"
		If Not Directory.Exists(HttpDebugDir) Then
			Directory.CreateDirectory(HttpDebugDir)
		End If

		Dim Request = Response.Request

		Dim TraceFile As String =
		 String.Format(
		 "{0}\{1};{2};{3}.json",
		 HttpDebugDir,
		 Request.Resource.Replace("/", "-"), Request.Method, Response.StatusCode.ToString)


		File.WriteAllText(
		 TraceFile,
		   New Response() With _
		   {
		   .ResponseURi = Response.ResponseUri.ToString,
		   .Response = Response.Content
		  }.ToString)

	End Sub

	Private Class Response
		Property ResponseURi As String
		Property Response As String
		Public Overrides Function ToString() As String
			Return ToJson()
		End Function
	End Class

#End If
#End Region

End Class
