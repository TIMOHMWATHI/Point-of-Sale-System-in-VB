Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmProformaInvoice

    Private Sub txtBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBarcode.KeyDown
        'retreave item using its barcode
        Dim datareader As MySqlDataReader
        Dim itemname As String = "", price As Double = 0, _
            barcode As String = ""

        If e.KeyCode = Keys.Enter Then    'work if enter button is clicked
            'Try
            'input validation on txtbarcode
            Dim itemcode As String
            If (txtBarcode.Text = String.Empty) Then
                Exit Sub
            Else
                itemcode = txtBarcode.Text.Trim
            End If

            If rdbRetailPrice.Checked Then
                strsql = "SELECT p.Barcode,p.Description,pl.retailprice FROM " _
                         & " products p INNER JOIN pricelist pl ON p.Barcode=pl.productid WHERE" _
                         & " pl.active=1 AND p.deleted= 0 AND (p.Barcode='" & itemcode & "' " _
                         & " OR p.Description= '" & itemcode & "') LIMIT 1"
            Else

                strsql = "SELECT p.Barcode,p.Description,pl.wholesaleprice FROM " _
                        & " products p INNER JOIN pricelist pl ON p.Barcode=pl.productid WHERE" _
                        & " pl.active=1 AND p.deleted= 0 AND (p.Barcode='" & itemcode & "' " _
                        & " OR p.Description= '" & itemcode & "') LIMIT 1"

            End If

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                barcode = datareader(0).ToString
                itemname = datareader(1).ToString
                price = Double.Parse(datareader(2).ToString)

                '++++++++++++++
                dgvProformaInvoice.Rows.Add(barcode, itemname, 1, price, price)
                '+++++++++++++
            End While
            txtBarcode.Clear()
            computeTotal()
            closeconn()
            '''''''''''''''''''''''''''''''''''''''''''''''''''''
        End If
    End Sub

    Sub computeTotal()
        Dim total As Double = 0

        For i = 0 To dgvProformaInvoice.RowCount - 1
            If dgvProformaInvoice.Rows(i).Cells(0).Value = vbEmpty Then

            Else
                If dgvProformaInvoice.Rows(i).Cells(0).Value.ToString = String.Empty Then Continue For
                total += Double.Parse(dgvProformaInvoice.Rows(i).Cells(4).Value.ToString)
            End If
        Next
        txtTotal.Text = total
        total = 0
    End Sub

    Private Sub RemoveProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveProductToolStripMenuItem.Click
        Dim i As Integer = dgvProformaInvoice.CurrentRow.Index
        dgvProformaInvoice.Rows.Remove(dgvProformaInvoice.Rows(i))
        dgvProformaInvoice.Refresh()
        computeTotal()
    End Sub

    Private Sub frmProformaInvoice_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'CALL THE AUTOSEARCH CODE
        Loadproducts(txtBarcode)
        txtBarcode.Focus()

        'load combo
        Dim strsql As String = ""
        strsql = "select supplier_id,fullname from suppliers where relationtype = 'C'"
        loadcombo(cboCustomers, strsql, "fullname", "supplier_id")
    End Sub

    Private Sub dgvProformaInvoice_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvProformaInvoice.CellValueChanged
        'compute total once quantity changes
        Try
            Dim price As Double, qty As Integer, total As Double
            If dgvProformaInvoice.RowCount > 1 Then
                price = Double.Parse(dgvProformaInvoice.CurrentRow.Cells(3).Value.ToString)
                qty = Integer.Parse(dgvProformaInvoice.CurrentRow.Cells(2).Value.ToString)
                total = price * qty
                dgvProformaInvoice.CurrentRow.Cells(4).Value = total
                computeTotal()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        If cboCustomers.SelectedIndex = -1 Then
            MessageBox.Show("Select customer", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            print_Proforma_Invoice()
        End If

    End Sub

    Private Sub print_Proforma_Invoice()
        Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable

        Dim Description As String, Qty As Double, Sp As Double, Total As Double, sTotal As Double
        Dim TotalAmnt As Double = Double.Parse(txtTotal.Text)
        With dt
            .Columns.Add("description")
            .Columns.Add("qty")
            .Columns.Add("unitprice")
            .Columns.Add("total")
        End With

        With dgvProformaInvoice
            For i = 0 To dgvProformaInvoice.RowCount - 1
                If dgvProformaInvoice.Rows(i).Cells(1).Value = Nothing Then Continue For
                Description = dgvProformaInvoice.Rows(i).Cells(1).Value.ToString
                Qty = Double.Parse(dgvProformaInvoice.Rows(i).Cells(2).Value.ToString)
                Sp = Double.Parse(dgvProformaInvoice.Rows(i).Cells(3).Value.ToString)
                Total = Double.Parse(dgvProformaInvoice.Rows(i).Cells(4).Value.ToString)
                sTotal += Double.Parse(dgvProformaInvoice.Rows(i).Cells(4).Value.ToString)
                'add rows
                dt.Rows.Add(Description, Qty, Sp, Total)
            Next
        End With

        Try
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New ProformaInvoice
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("ProformaTotals", sTotal)
            rptDoc.SetParameterValue("ReceiptNo", receiptNo)
            rptDoc.SetParameterValue("ServedBy", fullname)
            'rptDoc.SetParameterValue("Discount", lblDiscounts.Text)
            rptDoc.PrintToPrinter(1, False, 0, 0)
            'frmPrint.CRPrint.ReportSource = rptDoc
            'frmPrint.CRPrint.RefreshReport()
            'frmPrint.TopMost = True
            'frmPrint.StartPosition = FormStartPosition.CenterScreen
            'frmPrint.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Rub()
    End Sub

    'clear controls
    Sub Rub()
        txtBarcode.Clear()
        cboCustomers.SelectedIndex = -1
        dgvProformaInvoice.Rows.Clear()
        dtpDateOrdered.Value = Today
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class