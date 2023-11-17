Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassCashFlow
    Public Function InsertCashFlow(ByVal FlowDate As Date, _
                                      ByVal CashIn As Double, _
                                      ByVal CashOut As Double, _
                                      ByVal Variance As Double, _
                                      ByVal Registeredby As Integer, _
                                      ByVal Desc As String, _
                                      ByVal Effect As String, _
                                      ByVal Cost As Double, _
                                      ByVal Narration As String, _
                                      ByVal Selector As Integer) As String

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Dim masterno As String = ""
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_cashflow"
                .Parameters.AddWithValue("@intransactiondate", FlowDate)
                .Parameters.AddWithValue("@incashin", CashIn)
                .Parameters.AddWithValue("@incashout", CashOut)
                .Parameters.AddWithValue("@invariance", Variance)
                .Parameters.AddWithValue("@inuser", Registeredby)
                .Parameters.AddWithValue("@indesc", Desc)
                .Parameters.AddWithValue("@ineffect", Effect)
                .Parameters.AddWithValue("@incost", Cost)
                .Parameters.AddWithValue("@innarration", Narration)
                .Parameters.AddWithValue("@inmasterno", "")
                .Parameters("@inmasterno").Direction = ParameterDirection.InputOutput
                .Parameters.AddWithValue("@indeterminer", 1)
                .CommandText = strsql
                .ExecuteScalar()
            End With
            masterno = dtaName.InsertCommand.Parameters("@inmasterno").Value.ToString

            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then closeconn()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
        End Try

        Return masterno

    End Function

End Class
