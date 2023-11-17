Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassExpenses
    Public Sub ManageExpenseMaster(ByVal ExpenseDate As Date, _
                                   ByVal ExpenseTotal As Double, _
                                   ByVal ExpenseCategory As String, _
                                   ByVal Servedby As Integer)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text

                strsql = "INSERT INTO expensemaster(expensedate,total,expensecategory,servedby)" _
                         & " VALUES('" & ExpenseDate & "'," & ExpenseTotal & ",'" & ExpenseCategory & "','" & Servedby & "')"

                'MsgBox(strsql)
                .CommandText = strsql
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Public Sub ManageExpenseDetails(ByVal MasterNo As Integer, _
                                    ByVal Description As String, _
                                    ByVal Cost As Double, _
                                    ByVal Narration As String)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                strsql = "INSERT INTO expensedetails(masterno,expensedesc,expenseamount,notes)" _
                         & " VALUES(" & MasterNo & ",'" & Description & "'," & Cost & ",'" & Narration & "')"
                'MsgBox(strsql)
                .CommandText = strsql
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub


    'get maximum receipt number
    Public Function getExpenseMasterNo() As Integer
        Dim MasterNo As Integer = 0
        Dim datareader As MySqlDataReader
        Dim strsql As String = "SELECT MAX(entryid) FROM expensemaster"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                MasterNo = Integer.Parse(datareader(0).ToString)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return MasterNo
    End Function
End Class
