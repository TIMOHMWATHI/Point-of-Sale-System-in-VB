Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassInvoices
    Public Sub insertInvoiceMaster(ByVal InvoiceNo As String, ByVal BranchNo As Integer, _
                                   ByVal TotalAmnt As Double, ByVal DateReceived As Date, _
                                   ByVal Servedby As Integer, ByVal MasterNo As Integer, _
                                   ByVal Barcode As String, ByVal Qty As Integer, _
                                   ByVal UnitPrice As Double, ByVal Amnt As Double, _
                                   ByVal Selector As Integer)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_manageinvoices"
                .CommandText = strsql
                .Parameters.AddWithValue("@inselector", Selector)
                .Parameters.AddWithValue("@invoiceno", InvoiceNo)
                .Parameters.AddWithValue("@inbranchid", BranchNo)
                .Parameters.AddWithValue("@ininvoicetotals", TotalAmnt)
                .Parameters.AddWithValue("@indateinvoiced", DateReceived)
                .Parameters.AddWithValue("@inuser", Servedby)
                .Parameters.AddWithValue("@inmasterno", MasterNo)
                .Parameters.AddWithValue("@initemcode", Barcode)
                .Parameters.AddWithValue("@inqty", Qty)
                .Parameters.AddWithValue("@inunitprice", UnitPrice)
                .Parameters.AddWithValue("@inamnt", Amnt)
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

    Public Function getMasterNo() As Integer
        Dim datareader As MySqlDataReader
        Dim strsql As String = "SELECT MAX(entryno) FROM invoicemaster"
        Dim InvMaxNo As Integer = 0
        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                InvMaxNo = Integer.Parse(datareader(0).ToString)
            End While
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Return InvMaxNo
    End Function

    Public Sub ManageBranches(ByVal BranchName As String, _
                              ByVal Id As Integer, _
                              ByVal Determiner As Integer)

        Id = Integer.Parse(frmInvoices.dgvBranches.CurrentRow.Cells(0).Value.ToString)

        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_managebranches"
                .CommandText = strsql
                .Parameters.AddWithValue("@inname", BranchName)
                .Parameters.AddWithValue("@inid", Id)
                .Parameters.AddWithValue("@indeterminer", Determiner)
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
    Sub LoadBranches(ByVal dgv As DataGridView)
        Dim strsql As String = "SELECT sb.entryno,sb.branchname FROM salesbranches sb" _
                               & " WHERE sb.deleted=0 ORDER BY sb.branchname ASC"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            frmInvoices.dgvBranches.DataSource = table
            closeconn()
            With dgv
                .Columns(0).HeaderText = " # "
                .Columns(1).HeaderText = "Branch Name"
                .Columns(0).Visible = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

End Class
