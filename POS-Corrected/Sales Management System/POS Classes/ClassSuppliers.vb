Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class ClassSuppliers
    Public Sub insertsuppliers(ByVal fullname As String, ByVal pinid As String, _
                              ByVal address As String, ByVal town As String, _
                              ByVal physicallocation As String, ByVal phone As String, _
                               ByVal email As String, ByVal contactperson As String, _
                               ByVal relationtype As String)

        Dim strsql As String = "insert into suppliers(fullname,pinid, address, town," _
                               & " physicallocation, phone, email,contactperson,relationtype)" _
                               & " values('" & fullname & "','" & pinid & "','" & address & "', " _
                               & " '" & town & "' ,'" & physicallocation & "' ,'" & phone & "'," _
                               & " '" & email & "', '" & contactperson & "', '" & relationtype & "')"

        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                .CommandText = strsql
                .ExecuteNonQuery()
                MessageBox.Show("Successfully Saved", "Strawberry Solution", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End With
            dtaName.Dispose()
            closeconn()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MsgBox(ex.Message & " Error occured while saving data.If error persists, Inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    'Loads database table to Datagridview
    Public Sub selectsuppliers(ByVal datereceived As Date, ByVal gview As DataGridView)
        Dim strsql As String = "SELECT fullname,pinid,address,town,physicallocation," _
                               & " phone,email,contactperson,CASE WHEN relationtype='S'" _
                               & " THEN 'Supplier' ELSE 'Customer' END AS 'Relation Type'," _
                               & " supplier_id FROM suppliers WHERE deleted =0"
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        Dim adapter As New MySqlDataAdapter(cmd)
        Dim table As New DataTable

        adapter.Fill(table)
        'Display the data.
        gview.DataSource = table
        closeconn()
    End Sub

    'update procedure on frmSuppliers
    Public Sub updatesuppliers(ByVal fullname As String, ByVal pinid As String, ByVal address As String, _
                               ByVal town As String, ByVal physicallocation As String, ByVal phone As String, _
                               ByVal email As String, ByVal contactperson As String, ByVal relationtype As String, _
                               ByVal supplier_id As Integer)

        'supplier_id = Integer.Parse(frmSuppliers.dgvSuppliers.CurrentRow.Cells(9).Value.ToString)
        supplier_id = Integer.Parse(frmAddSupplier.lblid.Text)
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Try
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()

                .CommandType = CommandType.Text

                strsql = "update suppliers set fullname='" & fullname & "',pinid='" & pinid & "', address='" & address & "', town='" & town & "' ," _
                    & " physicallocation='" & physicallocation & "',  phone='" & phone & "', email='" & email & "'," _
                        & " contactperson='" & contactperson & "', relationtype='" & relationtype & "' WHERE  supplier_id = " & supplier_id & ""

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

    Public Sub loadcombo(ByVal cbo As ComboBox)
        Try
            Dim ds As New DataSet, strsql As String
            cbo.Items.Clear()
            strsql = "select supplier_id,fullname from suppliers where relationtype = 'S'"
            Dim da = New MySqlDataAdapter(strsql, Conn())
            da.SelectCommand.CommandType = CommandType.Text
            da.Fill(ds, "data")
            With cbo
                .Items.Add(ds.Tables("data").DefaultView)
                .DataSource = ds.Tables("data")
                .DisplayMember = "fullname"
                .ValueMember = "supplier_id"
                .SelectedIndex = -1
            End With
        Catch ex As Exception
        Finally
        End Try
    End Sub

    Sub LoadSupplierInvoices(ByVal dgv As DataGridView, ByVal SupplierNo As Integer)
        Dim CreditsTotal As Double = 0

        Dim strsql As String = "SELECT sp.entryid,DATE_FORMAT(dm.datereceived, '%Y %M %d')" _
                               & " AS DateReceived,sp.transactionno," _
                               & " sp.totalamnt, sp.amountpaid, (sp.totalamnt - sp.amountpaid)" _
                               & " AS Balance,1,sp.masterno FROM supplierpayments sp" _
                               & " INNER JOIN deliverymaster dm ON dm.deliveryid=sp.masterno" _
                               & " INNER JOIN suppliers s ON s.supplier_id=sp.suppliedby" _
                               & " WHERE s.deleted!=1 And sp.paystatus!=1 And " _
                               & " s.supplier_id =" & SupplierNo & " ORDER BY dm.datereceived ASC"
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgv.DataSource = table
            adapter.Dispose()
            closeconn()
            With dgv
                .Columns(0).HeaderText = "#"
                .Columns(1).HeaderText = "Date"
                .Columns(2).HeaderText = "Invoice No"
                .Columns(3).HeaderText = "Total Amnt"
                .Columns(4).HeaderText = "Amount Paid"
                .Columns(5).HeaderText = "Balance"
                .Columns(6).HeaderText = "PayAmnt"
                .Columns(7).HeaderText = "Master No"
                .Columns(7).Visible = False
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(1).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(3).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .Columns(0).Visible = False
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .Columns(3).DefaultCellStyle.Format = "n0"  'set decimals places
                .Columns(4).DefaultCellStyle.Format = "n0"  'set decimals places
                .Columns(5).DefaultCellStyle.Format = "n0"  'set decimals places
                .Columns(6).DefaultCellStyle.Format = "n0"  'set decimals places
            End With
            For i = 0 To dgv.RowCount - 1
                CreditsTotal = CreditsTotal + Double.Parse(dgv.Rows(i).Cells(5).Value)
            Next
            frmSupplierPayments.txtCreditsTotal.Text = CreditsTotal.ToString
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub LoadSupplierAmnts(ByVal dgv As DataGridView)
        Dim CreditsTotal As Double = 0

        Dim strsql As String = "SELECT s.fullname,SUM(sp.totalamnt - sp.amountpaid) " _
                               & " AS 'Unpaid Amnt' FROM supplierpayments sp " _
                               & " INNER JOIN suppliers s ON s.supplier_id=sp.suppliedby" _
                               & " WHERE sp.paystatus!=1 GROUP BY s.fullname ASC "
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            Dim adapter As New MySqlDataAdapter(cmd)
            Dim table As New DataTable
            adapter.Fill(table)
            'Display the data.
            dgv.DataSource = table
            adapter.Dispose()
            closeconn()
            With dgv
                .Columns(0).HeaderText = "Supplier Names"
                .Columns(1).HeaderText = "Unpaid Amnt"
                .AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells
                .Columns(0).DefaultCellStyle.WrapMode = DataGridViewTriState.True
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                '.Columns(3).DefaultCellStyle.Format = "n0"  'set decimals places
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Sub LoadSupplierPayment(ByVal dgv As DataGridView)
        'clear controls and grid
        frmAdvancedDeliveries.txtSearch.Clear()
        dgv.Rows.Clear()

        Dim CheckState As Boolean, InvoiceDate As Date, Supplier As String, _
            InvoiceNo As String, InvoiceTotal As Double, _
            InvoicePaid As Double, Status As String

        Dim strsql As String = "SELECT DATE_FORMAT(dm.datereceived, '%Y %M %d')" _
                                 & " AS DateReceived,s.fullname,sp.transactionno,dm.totalamnt," _
                                 & " sp.amountpaid,CASE WHEN sp.paystatus=0 THEN " _
                                 & "'Not Paid' ELSE 'Partially Paid' END AS 'Pay Status' FROM supplierpayments sp" _
                                 & " INNER JOIN deliverymaster dm ON dm.deliveryid=sp.masterno" _
                                 & " INNER JOIN suppliers s ON s.supplier_id=sp.suppliedby" _
                                 & " WHERE s.deleted!=1 AND sp.paystatus!=1 GROUP BY sp.masterno"
        Try
            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                InvoiceDate = datareader(0).ToString
                Supplier = datareader(1).ToString
                InvoiceNo = datareader(2).ToString
                InvoiceTotal = Double.Parse(datareader(3).ToString)
                InvoicePaid = Double.Parse(datareader(4).ToString)
                Status = datareader(5).ToString
                'add rows to grid
                dgv.Rows.Add(CheckState, InvoiceDate, Supplier, InvoiceNo, InvoiceTotal, InvoicePaid, Status)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub


    'sub load data from database
    Sub LoadSupplierAutosearch(ByVal suppliername As String, _
                               ByVal dgv As DataGridView)
        dgv.Rows.Clear()

        Dim CheckState As Boolean, InvoiceDate As Date, Supplier As String, _
            InvoiceNo As String, InvoiceTotal As Double, _
            InvoicePaid As Double, Status As String

        Dim strsql As String = "SELECT DATE_FORMAT(dm.datereceived, '%Y %M %d')" _
                           & " AS DateReceived,s.fullname,sp.transactionno," _
                           & " sp.totalamnt,sp.amountpaid,CASE WHEN sp.paystatus=0 THEN " _
                           & "'Not Paid' ELSE 'Partially Paid' END AS 'Pay Status' FROM supplierpayments sp" _
                           & " INNER JOIN deliverymaster dm ON dm.deliveryid=sp.masterno" _
                           & " INNER JOIN suppliers s ON s.supplier_id=sp.suppliedby" _
                           & " WHERE s.deleted!=1 AND sp.paystatus!=1 AND s.fullname='" & suppliername & "'"
        Try
            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                InvoiceDate = datareader(0).ToString
                Supplier = datareader(1).ToString
                InvoiceNo = datareader(2).ToString
                InvoiceTotal = Double.Parse(datareader(3).ToString)
                InvoicePaid = Double.Parse(datareader(4).ToString)
                Status = datareader(5).ToString
                'add rows to grid
                dgv.Rows.Add(CheckState, InvoiceDate, Supplier, InvoiceNo, InvoiceTotal, InvoicePaid, Status)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Class
