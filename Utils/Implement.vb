Namespace Utils

    Public Class Implement

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
        End Sub

        Shared Function Assign(Of T)(ByRef target As T, value As T) As T
            target = value
            Return value
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            Return MyBase.Equals(obj)
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return MyBase.GetHashCode()
        End Function

        Public Overrides Function ToString() As String
            Return MyBase.ToString()
        End Function

    End Class

End Namespace
