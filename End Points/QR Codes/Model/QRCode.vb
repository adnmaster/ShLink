Imports System.Drawing
Imports System.ComponentModel
Imports System.IO

<EditorBrowsable(EditorBrowsableState.Advanced)>
Public Class QRCode

	Property Bitmap As Bitmap

	Public Shared Widening Operator CType(ByVal QRCode As QRCode) As Bitmap

		If QRCode IsNot Nothing Then Return QRCode.Bitmap
		Return Nothing

	End Operator

	Public Shared Widening Operator CType(ByVal QRCode As QRCode) As Image

		If QRCode IsNot Nothing Then
			Dim PNGBytes As New MemoryStream
			QRCode.Bitmap.Save(PNGBytes, Imaging.ImageFormat.Png)
			Return Image.FromStream(PNGBytes)
		End If
		Return Nothing

	End Operator

End Class
