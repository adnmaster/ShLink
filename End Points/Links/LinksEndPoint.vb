Imports System.Net
Imports adnmaster.Bitly.HttpClient
Imports adnmaster.Bitly.Links.Response

Namespace Links

	''' <summary>
	''' The Links endpoint.
	''' </summary>
	Public NotInheritable Class LinksEndPoint

		''' <summary>
		''' The current configured <see cref="Bitly"/> Client
		''' </summary>
		Private ReadOnly _BitlyClient As BitlyClient
		Friend ReadOnly Property BitlyClient As BitlyClient
			Get
				Return _BitlyClient
			End Get
		End Property

		''' <summary>
		''' Creates the "Links" endpoint from a configured <paramref name="BitlyClient"/>.
		''' </summary>
		Sub New(ByVal BitlyClient As BitlyClient)
			_BitlyClient = BitlyClient
		End Sub

		''' <summary>
		''' Given a bitly URL (or multiple), returns the target (long) URL.
		''' </summary>
		''' <param name="ShortUrl">
		''' Refers to one or more bitly links. e.g.: http://bit.ly/1RmnUT or http://j.mp/1RmnUT
		''' </param>
		Function Expand(ByVal ShortUrl As String) As Expand

			Dim Response As RestJsonResponse =
			 _BitlyClient.ExecuteRequest(RestSharp.Method.GET, "expand",
			  New KeyValuePair(Of String, String)("shortUrl", ShortUrl))

			If Response.OK Then
				Dim ExpandEcho As Expand = Response.Content.FromJson(Of Expand)()
				If ExpandEcho.StatusCode = HttpStatusCode.OK Then
					Return ExpandEcho
				Else
					Throw New Exception(ExpandEcho.StatusDesription)
				End If
			End If

			Throw New RestException(Response)

		End Function

		''' <summary>
		''' Given a bitly hash (or multiple), returns the target (long) URL.
		''' </summary>
		''' <param name="Hash">
		''' Refers to one or more bitly hashes. e.g.: 2bYgqR or a-custom-name
		''' </param>
		Function ExpandByHash(ByVal Hash As String) As Expand

			Dim Response As RestJsonResponse =
			 _BitlyClient.ExecuteRequest(RestSharp.Method.GET, "expand",
			  New KeyValuePair(Of String, String)("hash", Hash))

			If Response.OK Then
				Dim ExpandEcho As Expand = Response.Content.FromJson(Of Expand)()
				If ExpandEcho.StatusCode = HttpStatusCode.OK Then
					Return ExpandEcho
				Else
					Throw New Exception(ExpandEcho.StatusDesription)
				End If
			End If

			Throw New RestException(Response)

		End Function

		''' <summary>
		''' This is used to return the page title for a given bitly link.
		''' </summary>
		''' <param name="ShortUrl">
		''' Refers to one or more bitly links e.g.: http://bit.ly/1RmnUT or http://j.mp/1RmnUT
		''' </param>
		''' <param name="ExpandUser">
		''' Include extra user info in response
		''' </param>
		Function Info(ByVal ShortUrl As String,
		  Optional ByVal ExpandUser As Boolean = False) As Info

			Dim Response As RestJsonResponse =
			 _BitlyClient.ExecuteRequest(RestSharp.Method.GET, "info",
			  New KeyValuePair(Of String, String)("shortUrl", ShortUrl),
			  New KeyValuePair(Of String, String)("expand_user", ExpandUser.ToString.ToLower))

			If Response.OK Then
				Dim InfoEcho As Info = Response.Content.FromJson(Of Info)()
				If InfoEcho.StatusCode = HttpStatusCode.OK Then
					Return InfoEcho
				Else
					Throw New Exception(InfoEcho.StatusDesription)
				End If
			End If

			Throw New RestException(Response)

		End Function

		''' <summary>
		''' This is used to return the page title for a given bitly hash.
		''' </summary>
		''' <param name="Hash">
		''' Refers to one or more bitly hashes. e.g.: 2bYgqR or a-custom-name
		''' </param>
		''' <param name="ExpandUser">
		''' Include extra user info in response
		''' </param>
		Function InfoByHash(ByVal Hash As String,
		  Optional ByVal ExpandUser As Boolean = False) As Info

			Dim Response As RestJsonResponse =
			 _BitlyClient.ExecuteRequest(RestSharp.Method.GET, "info",
			  New KeyValuePair(Of String, String)("hash", Hash),
			  New KeyValuePair(Of String, String)("expand_user", ExpandUser.ToString.ToLower))

			If Response.OK Then
				Dim InfoEcho As Info = Response.Content.FromJson(Of Info)()
				If InfoEcho.StatusCode = HttpStatusCode.OK Then
					Return InfoEcho
				Else
					Throw New Exception(InfoEcho.StatusDesription)
				End If
			End If

			Throw New RestException(Response)

		End Function

		''' <summary>
		''' This is used to query for a bitly link based on a long URL.
		''' </summary>
		''' <param name="Url">
		''' One or more long URLs to lookup
		''' </param>
		Function Lookup(ByVal Url As String) As Lookup

			Dim Response As RestJsonResponse =
			 _BitlyClient.ExecuteRequest(RestSharp.Method.GET, "link/lookup",
			  New KeyValuePair(Of String, String)("url", If(Url.EndsWith("/"), Url, Url & "/")))

			If Response.OK Then
				Dim LookupEcho As Lookup = Response.Content.FromJson(Of Lookup)()
				If LookupEcho.StatusCode = HttpStatusCode.OK Then
					Return LookupEcho
				Else
					Throw New Exception(LookupEcho.StatusDesription)
				End If
			End If

			Throw New RestException(Response)

		End Function

		''' <summary>
		''' This is used to query for a bitly link shortened by the authenticated user based on a long URL.
		''' </summary>
		''' <param name="Url">
		''' One or more long URLs to lookup
		''' </param>
		Function LinkLookup(ByVal Url As String) As User.Lookup

			Dim Response As RestJsonResponse =
			 _BitlyClient.ExecuteRequest(RestSharp.Method.GET, "user/link_lookup",
			  New KeyValuePair(Of String, String)("url", If(Url.EndsWith("/"), Url, Url & "/")))

			If Response.OK Then
				Dim LookupEcho As User.Lookup = Response.Content.FromJson(Of User.Lookup)()
				If LookupEcho.StatusCode = HttpStatusCode.OK Then
					Return LookupEcho
				Else
					Throw New Exception(LookupEcho.StatusDesription)
				End If
			End If

			Throw New RestException(Response)

		End Function

		''' <summary>
		''' Given a long URL, returns a bitly short URL.
		''' </summary>
		''' <param name="Url">
		''' a long URL to be shortened (example: http://betaworks.com/).
		''' </param>
		''' <param name="Domain">
		''' Refers to a preferred domain;
		''' either bit.ly, j.mp, or bitly.com,
		''' for users who do NOT have a custom short domain set up with bitly. This affects the output value of url.
		''' 
		''' The default for this parameter is the short domain selected by each user in his/her bitly account settings.
		''' Passing a specific domain via this parameter
		''' will override the default settings for users who do NOT have a custom short domain set up with bitly.
		''' 
		''' For users who have implemented a custom short domain,
		''' bitly will always return short links according to the user's account-level preference.
		''' </param>
		Function Shorten(ByVal Url As String,
		 Optional ByVal Domain As String = Nothing) As Shorten

			Dim UrlParam As String = Url.Trim

			Dim UrlSegs As New List(Of KeyValuePair(Of String, String))
			UrlSegs.Add(New KeyValuePair(Of String, String)("longUrl", UrlParam))

			If Not String.IsNullOrEmpty(Domain) Then
				UrlSegs.Add(New KeyValuePair(Of String, String)("domain", Domain))
			End If

			Dim Response As RestJsonResponse =
			 _BitlyClient.ExecuteRequest(RestSharp.Method.GET, "shorten", UrlSegs.ToArray)

			If Response.OK Then
				Dim ShortenEcho As Shorten = Response.Content.FromJson(Of Shorten)()
				If ShortenEcho.StatusCode = HttpStatusCode.OK Then
					Return ShortenEcho
				Else
					Throw New Exception(ShortenEcho.StatusDesription)
				End If
			End If

			Throw New RestException(Response)

		End Function

		''' <summary>
		''' Changes link metadata in a user's history.
		''' </summary>
		''' <param name="Link">
		''' The bitly link to be edited.
		''' </param>
		''' <param name="Title">
		''' The title of this bitmark.
		''' </param>
		''' <param name="Note">
		''' A description of, or note about, this bitmark.
		''' </param>
		''' <param name="MakePrivate">
		''' To indicate privacy setting (defaults to user-level setting).
		''' </param>
		''' <param name="Archive">
		''' To indicate whether or not link is to be archived.
		''' </param>
		''' <param name="NewTimestamp">
		''' A timestamp epoch.
		''' </param>
		Function Edit(ByVal Link As String,
		Optional ByVal Title As String = Nothing,
		Optional ByVal Note As String = Nothing,
		 Optional ByVal MakePrivate As TriState = TriState.UseDefault,
		 Optional ByVal Archive As TriState = TriState.UseDefault,
		  Optional ByVal NewTimestamp As Date = Nothing) As User.LinkEdit

			Dim UrlSegs As New List(Of KeyValuePair(Of String, String))
			UrlSegs.Add(New KeyValuePair(Of String, String)("link", Link))

			If Not String.IsNullOrEmpty(Title) Then
				UrlSegs.AddRange(EditParams("title", Title))
			End If

			If Not String.IsNullOrEmpty(Note) Then
				UrlSegs.AddRange(EditParams("note", Note))
			End If

			If MakePrivate <> TriState.UseDefault Then
				UrlSegs.AddRange(EditParams("private", MakePrivate.ToString.ToLower))
			End If

			If Archive <> TriState.UseDefault Then
				UrlSegs.AddRange(EditParams("archive", Archive.ToString.ToLower))
			End If

			If NewTimestamp > Date.MinValue Then
				UrlSegs.AddRange(EditParams("user_ts", NewTimestamp.ToFileTime.ToString))
			End If

			Dim Response As RestJsonResponse =
			 _BitlyClient.ExecuteRequest(RestSharp.Method.GET, "user/link_edit", UrlSegs.ToArray)

			If Response.OK Then
				Dim LinkEditEcho As User.LinkEdit = Response.Content.FromJson(Of User.LinkEdit)()
				If LinkEditEcho.StatusCode = HttpStatusCode.OK Then
					Return LinkEditEcho
				Else
					Throw New Exception(LinkEditEcho.Link)
				End If
			End If

			Throw New RestException(Response)

		End Function

		''' <summary>
		''' Saves a link as a bitmark in a user's history, with optional pre-set metadata.
		''' (Also returns a short URL for that link.)
		''' Note:
		''' Long URLs must have a slash between the domain and the path component.
		''' For example,
		''' http://example.com?query=parameter is invalid,
		''' and instead should be formatted as http://example.com/?query=parameter 
		''' </summary>
		''' <param name="Url">
		''' The (long) URL to be saved as a bitmark.
		''' </param>
		''' <param name="Title">
		''' The title of this bitmark.
		''' </param>
		''' <param name="Note">
		''' A description of, or note about, this bitmark.
		''' </param>
		''' <param name="IsPrivate">
		''' To indicate privacy setting (defaults to user-level setting).
		''' </param>
		''' <param name="UserTimeSpan">
		''' A timestamp epoch.
		''' </param>
		Function Save(ByVal Url As String,
		 Optional ByVal Title As String = Nothing,
		Optional ByVal Note As String = Nothing,
		Optional ByVal IsPrivate As TriState = TriState.UseDefault,
		Optional ByVal UserTimeSpan As Date = Nothing) As User.LinkSave

			Dim UrlSegs As New List(Of KeyValuePair(Of String, String))
			UrlSegs.Add(New KeyValuePair(Of String, String)("longUrl", Url))

			If Not String.IsNullOrEmpty(Title) Then
				UrlSegs.AddRange(EditParams("title", Title))
			End If

			If Not String.IsNullOrEmpty(Note) Then
				UrlSegs.AddRange(EditParams("note", Note))
			End If

			If IsPrivate <> TriState.UseDefault Then
				UrlSegs.AddRange(EditParams("private", IsPrivate.ToString.ToLower))
			End If

			If UserTimeSpan > Date.MinValue Then
				UrlSegs.AddRange(EditParams("user_ts", UserTimeSpan.ToFileTime.ToString))
			End If

			Dim Response As RestJsonResponse =
			 _BitlyClient.ExecuteRequest(RestSharp.Method.GET, "user/link_save", UrlSegs.ToArray)

			If Response.OK Then
				Dim LinkSaveEcho As User.LinkSave = Response.Content.FromJson(Of User.LinkSave)()
				If LinkSaveEcho.StatusCode = HttpStatusCode.OK OrElse
				 LinkSaveEcho.StatusCode = HttpStatusCode.NotModified Then
					Return LinkSaveEcho
				Else
					Throw New Exception(LinkSaveEcho.StatusDesription)
				End If
			End If

			Throw New RestException(Response)

		End Function

		''' <summary>
		''' Build url parameters for edit.
		''' "{edit=<paramref name="ParamName"/>&amp;<paramref name="ParamName"/>=<paramref name="Value"/>"
		''' Ex: 
		'''    edit=note&amp;note=value
		''' </summary>
		''' <param name="ParamName">
		''' The parameter name to edit (ex: title)
		''' </param>
		''' <param name="Value">
		''' The new value for the <paramref name="ParamName">parameter</paramref> specified.
		''' </param>
		''' <returns>
		''' Two (2) <see cref="KeyValuePair(Of String, String)"/> as an
		''' <see cref="IEnumerable"/>
		''' </returns>
		Private Shared Function EditParams(
		 ByVal ParamName As String, ByVal Value As String) As  _
		IEnumerable(Of KeyValuePair(Of String, String))

			Return _
			 {
			 New KeyValuePair(Of String, String)("edit", ParamName),
			   New KeyValuePair(Of String, String)(ParamName, Value)
			  }

		End Function

	End Class

End Namespace