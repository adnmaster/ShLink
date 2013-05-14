Imports Newtonsoft.Json

Namespace Links.Model.User

	'''<summary>
	''' Saved link as a bitmark in the user's history,
	''' with optional pre-set metadata and the short URL for that link.
	'''</summary>
	Public Class SavedLink

		'''<summary>
		''' The corresponding bitly aggregate link (global hash)
		'''</summary>
		<JsonProperty("aggregate_link")>
		Property AggregateLink As String

		''' <summary>
		''' The  bitly link (short URL).
		''' </summary>
		<JsonProperty("link")>
		Property ShortUrl As String

		''' <summary>
		''' An echo back of the long url request parameter.
		''' This may not always be equal to the URL requested,
		''' as some URL normalization may occur (e.g., due to encoding differences,
		''' or case differences in the domain).
		''' This long url will always be functionally identical the the request parameter.
		''' </summary>
		<JsonProperty("long_url")>
		Property LongUrl As String

		''' <summary>
		''' Indicates if the authenticated user is saving this link for the first time,
		''' or if the user has previously saved it.
		''' (Previously|Newly Saved Link)
		''' </summary>
		<JsonProperty("new_link")>
		Property IsNewLink As Boolean

	End Class

End Namespace
