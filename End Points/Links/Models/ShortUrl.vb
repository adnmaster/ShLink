Imports Newtonsoft.Json

Namespace Links.Model

	'''<summary>
	''' A bitly short URL
	'''</summary>
	Public Class ShortUrl

		''' <summary>
		''' A bitly identifier for long url
		''' which can be used to track aggregate stats across all bitly links
		''' that point to the same long url.
		''' </summary>
		<JsonProperty("global_hash")>
		Property GlobalHash As String

		''' <summary>
		''' A bitly identifier for long url which is unique to the given account.
		''' </summary>
		<JsonProperty("hash")>
		Property Hash As String

		''' <summary>
		''' An echo back of the long url request parameter.
		''' This may not always be equal to the URL requested,
		''' as some URL normalization may occur (e.g., due to encoding differences, 
		''' or case differences in the domain).
		''' This long url will always be functionally identical to the request parameter. 
		''' </summary>
		<JsonProperty("long_url")>
		Property LongUrl As String

		''' <summary>
		''' Designates if this is the first time this long url was shortened by this user.
		''' </summary>
		<JsonProperty("new_hash")>
		Property IsNewHash As Boolean

		''' <summary>
		''' The actual link that should be used,
		''' and is a unique value for the given bitly account.
		''' </summary>
		<JsonProperty("url")>
		Property ShortUrl As String

	End Class


End Namespace