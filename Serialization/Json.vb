Imports Newtonsoft.Json
Imports System.IO
Imports System.Text
imports System.Runtime.Serialization.Formatters

Namespace Serialization

	Friend Class Json

		Private Shared CurrentFormatter As JsonSerializer
		Public Shared ReadOnly Property Formatter() As JsonSerializer
			Get
				If CurrentFormatter Is Nothing Then
					CurrentFormatter = New JsonSerializer
					CurrentFormatter.NullValueHandling = NullValueHandling.Ignore
					CurrentFormatter.DateFormatHandling = DateFormatHandling.IsoDateFormat
					CurrentFormatter.ReferenceLoopHandling = ReferenceLoopHandling.Serialize
					CurrentFormatter.TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
				End If
				Return CurrentFormatter
			End Get
		End Property

		Shared Function Serialize(ByVal Graph As Object) As String

			Dim MemoryStream As New MemoryStream
			Dim StreamWriter As New StreamWriter(MemoryStream, Encoding.UTF8)
			Dim JsonWriter As New JsonTextWriter(StreamWriter)
			JsonWriter.Formatting = Formatting.Indented

			Formatter.Serialize(JsonWriter, Graph)

			JsonWriter.Flush()
			StreamWriter.Flush()

			Dim SerializedGraph As String = MemoryStream.ToArray.ToText

			JsonWriter.Close()
			StreamWriter.Close()
			StreamWriter.Dispose()

			Return SerializedGraph

		End Function

		Shared Function Deserialize(Of T)(ByVal SerializedGraph As String) As T

			Dim StringReader As New StringReader(SerializedGraph)
			Dim JsonReader As New JsonTextReader(StringReader)

			Dim Obj As T = Formatter.Deserialize(Of T)(JsonReader)

			JsonReader.Close()
			StringReader.Close()
			StringReader.Dispose()

			Return Obj

		End Function

	End Class

End Namespace
