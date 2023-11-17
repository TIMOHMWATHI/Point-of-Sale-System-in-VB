Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassReceiptHeader
    Public Sub InsertHeader(ByVal BusibessName As String, _
                            ByVal Phone As String, _
                            ByVal Paybill As String, _
                            ByVal Motto As String, _
                            ByVal Entryid As Integer, _
                            ByVal selector As Boolean)

        Entryid = Integer.Parse(frmReceiptSettings.dgvReceiptHeader.CurrentRow.Cells(0).Value.ToString)
        Try
            Dim strsql As String
            Dim dtaName As New MySqlClient.MySqlDataAdapter
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text

                If selector = True Then
                    strsql = "INSERT INTO receiptheader (businessname,phone,paybillno,motto) " _
                              & " VALUES('" & BusibessName & "','" & Phone & "','" & Paybill & "','" & Motto & "')"
                Else
                    strsql = "UPDATE receiptheader SET businessname='" & BusibessName & "'," _
                     & " phone='" & Phone & "',paybillno='" & Paybill & "'," _
                     & " motto='" & Motto & "' WHERE entryid=" & Entryid & ""
                End If

                'MsgBox(strsql)
                .CommandText = strsql
                .ExecuteNonQuery()
                MessageBox.Show("Success", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
