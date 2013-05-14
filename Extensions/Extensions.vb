Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Text
Imports System.Security.Cryptography
Imports System.ComponentModel

	<EditorBrowsable(EditorBrowsableState.Advanced)>
Public Module Extensions

		<EditorBrowsable(EditorBrowsableState.Advanced)>
	Public ReadOnly UnixEpoch As New Date(1970, 1, 1, 0, 0, 0)

	''' <summary>
	''' Date from seconds since <see cref="UnixEpoch"/>
	''' </summary>
	''' <param name="Timestamp">
	''' Seconds since <see cref="UnixEpoch"/>
	''' </param>
	<Extension()>
	 <EditorBrowsable(EditorBrowsableState.Advanced)>
	Public Function EpochToDate(ByVal Timestamp As Integer) As Date
		Return UnixEpoch.AddSeconds(Timestamp)
	End Function

	''' <summary>
	''' Seconds since <see cref="UnixEpoch"/> to <paramref name="StartDate"/>
	''' </summary>
	<Extension()>
	 <EditorBrowsable(EditorBrowsableState.Advanced)>
	Public Function DateToEpoch(ByVal StartDate As Date) As Integer
		Return CInt(StartDate.Subtract(UnixEpoch).TotalSeconds)
	End Function

	''' <summary>
	''' #yyyyMMddHHmmss#
	''' </summary>
	<Extension()>
	 <EditorBrowsable(EditorBrowsableState.Advanced)>
	Public Function ParseDateTime(ByVal S As String) As Date
		Return DateTime.ParseExact(S, "yyyyMMddHHmmss", Nothing)
	End Function

	<Extension()>
	 <EditorBrowsable(EditorBrowsableState.Advanced)>
	Public Function ToText(ByVal Data As Byte()) As String
		Dim StreamReader As New StreamReader(New MemoryStream(Data), Encoding.UTF8)
		Return StreamReader.ReadToEnd
	End Function

	''' <summary>
	''' Serialize to JavaScript Object Annotation (from String).
	''' </summary>
		<EditorBrowsable(EditorBrowsableState.Advanced)>
	<Extension()>
	Public Function ToJson(ByVal Graph As Object) As String
		Return Serialization.Json.Serialize(Graph)
	End Function

	''' <summary>
	''' Deserialize from JavaScript Object Annotation (to Typed object).
	''' </summary>
	<Extension()>
	 <EditorBrowsable(EditorBrowsableState.Advanced)>
	Public Function FromJson(Of T)(ByVal SerializedGraph As String) As T
		Return Serialization.Json.Deserialize(Of T)(SerializedGraph)
	End Function
	
	Private ReadOnly SAditionalEntropy As Byte() = {0, 1, 10, 11, 100, 101}

	<Extension()>
	<EditorBrowsable(EditorBrowsableState.Advanced)>
	Public Function EncryptUserData(ByVal Data As Byte()) As Byte()
		Return ProtectedData.Protect(Data, SAditionalEntropy, DataProtectionScope.CurrentUser)
	End Function

	<Extension()>
	<EditorBrowsable(EditorBrowsableState.Advanced)>
	Public Function DecryptUserData(ByVal Data As Byte()) As Byte()
		Return ProtectedData.Unprotect(Data, SAditionalEntropy, DataProtectionScope.CurrentUser)
	End Function

	''' <summary>
	''' Into PNG file (please, specify .png at the end)
	''' </summary>
	<Extension()>
	 <EditorBrowsable(EditorBrowsableState.Advanced)>
	Public Sub Save(ByVal QRCode As QRCode, ByVal ToFile As String)
		QRCode.Bitmap.Save(ToFile, Drawing.Imaging.ImageFormat.Png)
	End Sub

End Module
