Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmReceiveReturns
    Dim sale As New posClass

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim qty As Double, qtySold As Double
        Dim sp As Double, bp As Double, Narration As String
        Dim totalValueReturns As Double
        Dim barcode As String, recNo As Integer
        Dim vat As Double, feedback As Integer = 0
        Dim disc As Double = 0
        Dim VAT_Rate As Double = Double.Parse(get_VAT_Rate)


        If (txtQtyReturned.Text.Trim.Length < 1) Then Exit Sub
        qtySold = Double.Parse(lblqtysold.Text)
        qty = Double.Parse(txtQtyReturned.Text)
        'VAT_Rate = Double.Parse(get_VAT_Rate)
        '  disc = Double.Parse(frmSales.dgvSales.CurrentRow.Cells(6).Value.ToString)
        If (qty > qtySold) Then
            MessageBox.Show("Error! invalid quantity returned", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        sp = Double.Parse(lblSellingPrice.Text)
        bp = Double.Parse(lblItemBP.Text)

        'compute vat amnt
        If lblVATstatus.Text = "NO" Then
            vat = 0
        Else
            vat = ((sp - bp) * qty) * (VAT_Rate / 100)
        End If

        totalValueReturns = Double.Parse(lblTotalValue.Text)
        barcode = lblBarcode.Text
        recNo = Integer.Parse(lblReceiptNo.Text)

        'narration validation
        If txtNarration.Text = String.Empty Then
            MessageBox.Show("Narration input missing", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtNarration.Focus()
            Exit Sub
        Else
            Narration = txtNarration.Text
        End If

        'process returns
        feedback = sale.setSalesReturns(totalValueReturns, qty, sp, disc, vat, barcode, recNo, "", 5)

        'INSERT INTO CASH LEDGER
        CashLedger(recNo, totalValueReturns, Now(), userid, "Returns of sold items", "-", "SalesReturns", 1)

        'INSERT INTO STOCK LEDGER
        StockLedger(recNo, barcode, qty, sp, Now(), userid, "Stock returns of sold items", "+", "SalesReturns", 1)

        'insert narration
        Dim strsql As String
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        dtaName.InsertCommand = New MySqlClient.MySqlCommand
        With dtaName.InsertCommand
            .CommandTimeout = 60
            .Connection = Conn()
            .CommandType = CommandType.Text
            strsql = "INSERT INTO salereturnsnarration (barcode,qtyreturned,narration,datereturned,receivedby)" _
                     & " VALUES('" & barcode & "'," & qty & ",'" & Narration & "',NOW(),'" & username & "')"
            'MsgBox(strsql)
            .CommandText = strsql
            .ExecuteNonQuery()
        End With

        'INSERT INTO STOCK MOVEMENT
        stockMovement(barcode, "Sale return", "+", qty, sp, 0, Now(), userid, 1)

        If (feedback > 0) Then
            MessageBox.Show("Returns processed successfully", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            'clear inputs
            clearcontrols()
            'close the form
            Me.Close()
            'reload form
            frmSalesReturns.btnLoad.PerformClick()
        End If
    End Sub

    Public Function get_VAT_Rate() As Double
        Dim datareader As MySqlDataReader
        Dim VATRate As Double = 0

        Dim strsql As String = "SELECT v.vatpercent FROM vatsettings v  WHERE v.vatstatus!=0"

        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                If (datareader(0).ToString) = String.Empty Then
                    VATRate = 0
                Else
                    VATRate = datareader(0).ToString
                    'MsgBox(VATRate)
                End If
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
        Return VATRate
    End Function

    Sub computeTotalReturns()
        Dim sp As Double = 0
        Dim qtysold As Integer = 0, _
           qtyreturned As Integer = 0, _
            total As Double = 0

        sp = lblSellingPrice.Text

        'qty return validation
        If (txtQtyReturned.Text.Trim.Length < 1) Then
            qtyreturned = 0
        Else
            qtyreturned = Integer.Parse(txtQtyReturned.Text)
        End If
        'totals validation
        total = (qtyreturned * sp)
        lblTotalValue.Text = total.ToString


        'For i = 0 To frmSalesReturns.dgvSalesReturns.RowCount - 1
        '    If frmSalesReturns.dgvSalesReturns.Rows(i).Cells(5).Value = vbEmpty Then

        '    Else
        '        total -= Double.Parse(frmSalesReturns.dgvSalesReturns.Rows(i).Cells(5).Value.ToString)
        '    End If
        'Next
        'lblTotalValue.Text = total
        'total = 0

    End Sub

    Private Sub txtQtyReturned_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQtyReturned.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtQtyReturned_TextChanged(sender As Object, e As EventArgs) Handles txtQtyReturned.TextChanged
        computeTotalReturns()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearcontrols()
    End Sub

    'clear input areas
    Sub clearcontrols()
        lblBarcode.Text = 0
        lblDescription.Text = ""
        lblFileNo.Text = 0
        lblqtysold.Text = 0
        lblSellingPrice.Text = 0
        lblReceiptNo.Text = 0
        txtQtyReturned.Clear()
        lblTotalValue.Text = ""
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class