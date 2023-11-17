Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class ClassStaff

    Public Sub insertstaff(ByVal fullnames As String, _
                           ByVal phonenumber As String, _
                           ByVal nationalidnumber As String, _
                           ByVal username As String, _
                           ByVal paswad As String)
        Try

            Dim strsql As String
            Dim dtaName As New MySqlClient.MySqlDataAdapter
            Try
                dtaName.InsertCommand = New MySqlClient.MySqlCommand
                With dtaName.InsertCommand
                    .CommandTimeout = 60
                    .Connection = Conn()

                    .CommandType = CommandType.Text
                    strsql = "insert into staff(fullnames,phonenumber,nationalidnumber,username,paswad)" _
                                   & " values('" & fullnames & "','" & phonenumber & "','" & nationalidnumber & "'," _
                                              & " '" & username & "','" & paswad & "')"

                    .CommandText = strsql
                    .ExecuteNonQuery()
                    MessageBox.Show("Successfully Saved", "To Database", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End With
                dtaName.Dispose()
                closeconn()
            Catch ex As Exception
                If Conn.State = ConnectionState.Open Then Conn.Close()
                MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
                Exit Sub
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    'update procedure on frmitems
    Public Sub updatestaff(ByVal fullnames As String, _
                           ByVal phonenumber As String, _
                           ByVal nationalidnumber As String, _
                           ByVal username As String, _
                           ByVal staffid As Integer)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                strsql = "update staff set fullnames='" & fullnames & "', " _
                         & " phonenumber='" & phonenumber & "',nationalidnumber='" & nationalidnumber & "', " _
                         & " username='" & username & "' where staffid=" & staffid & ""
                'MsgBox(strsql)
                .CommandText = strsql
                .ExecuteNonQuery()
                MessageBox.Show("Successfully Updated", "Strawberry", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

End Class
