Imports System.Drawing
Imports System.Net
Imports System.ComponentModel

''' <summary>
''' Generate a QR code, from a bitly short link.
''' </summary>
Public Class QRCodes

	''' <summary>
	''' QR Codes download statuses.
	''' </summary>
	''' <remarks></remarks>
	<EditorBrowsable(EditorBrowsableState.Advanced)>
	Enum Status
		''' <summary>
		''' The link provided is not valid.
		''' </summary>
		Errored
		''' <summary>
		''' Image downloaded.
		''' </summary>
		OK
		''' <summary>
		''' Pending request.
		''' </summary>
		None
		''' <summary>
		''' Transport error (not HTTP)
		''' </summary>
		Unknown
	End Enum

#Region " .ctor "

	Private ReadOnly _Link As String
	Private ReadOnly _Size As Integer

	Private Shared ReadOnly SizeParam As String = "?s={size}"
	Private Shared ReadOnly QRCodeURI As String = ".qrcode"

	''' <summary>
	''' Generate a QR code (Default size).
	''' </summary>
	''' <param name="Link">
	''' The bitly short link.
	''' </param>
	Sub New(ByVal Link As String)
		_Link = Link
		_Size = -1
		PrepareDownload()
	End Sub

	''' <summary>
	''' Generate a QR code,
	''' the closest standard size in pixels (<paramref name="SizeInPixel"/>) will be returned.
	''' </summary>
	''' <param name="Link">
	''' The bitly short link.
	''' </param>
	''' <param name="SizeInPixel">
	''' Closest standard size (in pixels).
	''' </param>
	Sub New(ByVal Link As String, ByVal SizeInPixel As Integer)
		_Link = Link
		_Size = SizeInPixel
		PrepareDownload()
	End Sub

#End Region

#Region " Internal "

	Private Sub PrepareDownload()

		_DownloadStatus = Status.None

		_QRCode = New Lazy(Of QRCode)(
		   Function()

			   Dim WebResp As HttpWebResponse
			   Dim QRCodeModel As New QRCode

			   Try
				   Dim WebReq As HttpWebRequest = CType(HttpWebRequest.Create(GetRequestURL), HttpWebRequest)
				   WebResp = CType(WebReq.GetResponse, HttpWebResponse)
				   Dim RespStatusCode As HttpStatusCode = WebResp.StatusCode

				   If RespStatusCode = HttpStatusCode.OK OrElse
				 RespStatusCode = HttpStatusCode.NotModified Then
					   QRCodeModel.Bitmap = New Bitmap(WebResp.GetResponseStream)
					   _DownloadStatus = Status.OK
				   Else
					   _DownloadStatus = Status.Unknown
				   End If

				   WebResp.Close()

			   Catch Exception As Exception
				   _LastError = Exception
				   _DownloadStatus = Status.Errored
			   End Try

			   Return QRCodeModel

		   End Function)

	End Sub

	Private Function GetRequestURL() As String

		Dim DefaultURL As String = _Link & QRCodeURI

		If _Size = -1 Then
			Return DefaultURL
		Else
			Return DefaultURL & SizeParam.Replace("{size}", _Size.ToString)
		End If

	End Function

	Private _LastError As Exception
	<EditorBrowsable(EditorBrowsableState.Advanced)>
 ReadOnly Property LastError As Exception
		Get
			Return _LastError
		End Get
	End Property

#End Region

	Private _QRCode As Lazy(Of QRCode)
	''' <summary>
	''' The QR Code
	''' </summary>
	<EditorBrowsable(EditorBrowsableState.Advanced)>
	ReadOnly Property QRCode As QRCode
		Get
			Return _QRCode.Value
		End Get
	End Property

	''' <summary>
	''' Returns the <see cref="T:System.Drawing.Bitmap">Bitmap</see> object
	''' </summary>
	ReadOnly Property Bitmap As Bitmap
		Get
			Return _QRCode.Value
		End Get
	End Property

	''' <summary>
	''' Returns the <see cref="T:System.Drawing.Image">Image</see> object (PNG)
	''' </summary>
	ReadOnly Property Image As Image
		Get
			Return _QRCode.Value
		End Get
	End Property

	Private _DownloadStatus As Status
	''' <summary>
	''' Gets the download status.
	''' </summary>
	ReadOnly Property DownloadStatus As Status
		Get
			Return _DownloadStatus
		End Get
	End Property

	''' <summary>
	''' Indicates whether the bitmap has been downloaded.
	''' </summary>
	ReadOnly Property BitmapDownloaded As Boolean
		Get
			Return _QRCode.IsValueCreated
		End Get
	End Property

	''' <summary>
	''' Try download QR Code again.
	''' </summary>
	Function RetryDownload() As QRCode
		PrepareDownload()
		Return _QRCode.Value
	End Function

	''' <summary>
	''' Into PNG file (please, specify .png at the end)
	''' </summary>
	Sub Save(ByVal ToFile As String)
		QRCode.Save(ToFile)
	End Sub


	Public Shared Widening Operator CType(ByVal QRCodeEndPoint As QRCodes) As Bitmap

		If QRCodeEndPoint IsNot Nothing Then Return QRCodeEndPoint.QRCode
		Return Nothing

	End Operator

	Public Shared Widening Operator CType(ByVal QRCodeEndPoint As QRCodes) As Image

		If QRCodeEndPoint IsNot Nothing Then Return QRCodeEndPoint.QRCode
		Return Nothing

	End Operator

End Class
