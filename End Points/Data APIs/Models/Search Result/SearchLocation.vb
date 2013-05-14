Imports System.ComponentModel

Namespace DataAPIs.Model.CustomSearch

	''' <summary>
	''' Location search (Title, long ur, aggregate (short) link and cities) 
	''' </summary>
	''' <remarks>
	''' Use the results to perform other
	''' searches see
	''' <see
	''' cref="adnmaster.Bitly.DataAPIs.SearchEndPoint.Search">Search</see>
	''' </remarks>
	''' <seealso
	''' cref="adnmaster.Bitly.DataAPIs.SearchEndPoint.Search">Search</seealso>
	Public Class SearchLocation
		Inherits MinSearch

		Public Overloads Shared Widening Operator CType(ByVal ResultItem As SearchResult.Result) As SearchLocation

			If ResultItem IsNot Nothing Then

				Dim C As New SearchLocation()
				With C

					.Title = ResultItem.Title
					.LongURL = ResultItem.URL
					.AggregateLink = ResultItem.AggregateLink

					.Cities = ResultItem.Cities
				End With

				Return C

			End If

			Return Nothing

		End Operator


		<EditorBrowsable(EditorBrowsableState.Advanced)>
		Property Cities As String

		Private _ListOfCities As SortedList(Of Integer, String)
		ReadOnly Property ListOfCities As SortedList(Of Integer, String)
			Get

				If _ListOfCities IsNot Nothing Then
					Return _ListOfCities
				Else
					_ListOfCities = New SortedList(Of Integer, String)
				End If

				Dim Order As Integer = 1
				Dim Cts = Cities.Split("-"c)
				Array.ForEach(Cts,
				  Sub(City)
					  _ListOfCities.Add(Order, City)
					  Order += 1
				  End Sub)

				Return _ListOfCities

			End Get
		End Property

	End Class

End Namespace