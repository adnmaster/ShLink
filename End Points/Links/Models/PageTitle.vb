Imports Newtonsoft.Json

Namespace Links.Model

	'''<summary>
	''' Page title for a given bitly link
	'''</summary>
	Public Class PageTitle

		''' <summary>
		''' The epoch timestamp when this bitly link was created
		''' </summary>
		<JsonProperty("created_at")>
		Property CreatedAt As Integer

		''' <summary>
		''' The bitly username that originally shortened this link, if the link is public. Otherwise, null.
		''' </summary>
		<JsonProperty("created_by")>
		Property CreatedBy As String

		''' <summary>
		''' The corresponding bitly aggregate identifier.
		''' </summary>
		<JsonProperty("global_hash")>
		Property GlobalHash As String

		''' <summary>
		''' This is an echo back of the shortUrl request parameter.
		''' </summary>
		<JsonProperty("short_url")>
		Property ShortUrl As String

		''' <summary>
		''' The HTML page title for the destination page (when available).
		''' </summary>
		<JsonProperty("title")>
		Property Title As String

		''' <summary>
		''' The corresponding bitly user identifier.
		''' </summary>
		<JsonProperty("user_hash")>
		Property UserHash As String

	End Class

End Namespace