Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassBanks
    Public Sub managebanks(ByVal bankname As String, _
                           ByVal entryid As Integer, _
                           ByVal indeterminer As Integer)
        entryid = Integer.Parse(frmBanks.dgvBanks.CurrentRow.Cells(0).Value.ToString)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_managebanks"
                .CommandText = strsql
                .Parameters.AddWithValue("@inbankname", bankname)
                .Parameters.AddWithValue("@inentryid", entryid)
                .Parameters.AddWithValue("@indeterminer", indeterminer)
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

    Public Sub manageBankDeposits(ByVal DepositDate As Date, _
                                  ByVal BankName As String, _
                                  ByVal AmntDeposited As Double, _
                                  ByVal Paymode As String, _
                                  ByVal Memo As String, _
                                  ByVal Registerdby As Integer, _
                                  ByVal indeterminer As Integer)
        'entryid = Integer.Parse(frmBanks.dgvBanks.CurrentRow.Cells(0).Value.ToString)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_bankdeposits"
                .CommandText = strsql
                .Parameters.AddWithValue("@indate", DepositDate)
                .Parameters.AddWithValue("@inbank", BankName)
                .Parameters.AddWithValue("@inamount", AmntDeposited)
                .Parameters.AddWithValue("@inpaymode", Paymode)
                .Parameters.AddWithValue("@inmemo", Memo)
                .Parameters.AddWithValue("@inuser", Registerdby)
                .Parameters.AddWithValue("@indeterminer", indeterminer)
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

    'Loads AND RELOAD database table FROM Datagridview
    Sub LoadBankDetails(ByVal dgv As DataGridView)
        Dim strsql As String = "SELECT entryid,bankname FROM banks WHERE deleted=0 ORDER BY bankname ASC"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            frmBanks.dgvBanks.DataSource = table
            closeconn()
            With dgv
                .Columns(0).HeaderText = " # "
                .Columns(1).HeaderText = "Bank Name"
                .Columns(0).Visible = False
                '.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                '.Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
