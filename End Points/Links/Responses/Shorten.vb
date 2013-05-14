Imports System.ComponentModel
Imports Newtonsoft.Json

Namespace Links.Response

	''' <summary>
	''' Given a long URL, returns a bitly short URL.
	''' </summary>
	Public Class Shorten
		Inherits ResponseBase

		'''<summary>
		''' A bitly short URL
		'''</summary>
		<JsonProperty("data")>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property Data As Model.ShortUrl

		''' <summary>
		''' A bitly identifier for long url
		''' which can be used to track aggregate stats across all bitly links
		''' that point to the same long url.
		''' </summary>
		ReadOnly Property GlobalHash As String
			Get
				Return Data.GlobalHash
			End Get
		End Property

		''' <summary>
		''' A bitly identifier for long url which is unique to the given account.
		''' </summary>
		ReadOnly Property Hash As String
			Get
				Return Data.Hash
			End Get
		End Property

		''' <summary>
		''' An echo back of the long url request parameter.
		''' This may not always be equal to the URL requested,
		''' as some URL normalization may occur (e.g., due to encoding differences, 
		''' or case differences in the domain).
		''' This long url will always be functionally identical to the request parameter. 
		''' </summary>
		ReadOnly Property LongUrl As String
			Get
				Return Data.LongUrl
			End Get
		End Property

		''' <summary>
		''' Designates if this is the first time this long url was shortened by this user.
		''' </summary>
		ReadOnly Property IsNewHash As Boolean
			Get
				Return Data.IsNewHash
			End Get
		End Property

		''' <summary>
		''' The actual link that should be used,
		''' and is a unique value for the given bitly account.
		''' </summary>
		ReadOnly Property ShortUrl As String
			Get
				Return Data.ShortUrl
			End Get
		End Property

	End Class

End Namespace
