Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmpayment
    Dim inventorystock As New inventory
    Dim sale As New posClass

    'variables declaration
    Dim total As Double, amountpaid As Double, _
        balance As Double, amountdue As Double, _
        salesMode As String, vatPercent As Double

    'SAVE TO PAYMODE
    Dim Paymode As String, TransactionNo As String, PaymodeAmnt As Double = 0

    'get maximum receipt number
    Public Function getMasterNo() As Integer
        Dim MasterNo As Integer = 0
        Dim datareader As MySqlDataReader
        Dim strsql As String = "SELECT MAX(receptno) FROM salesmaster"
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
  
    Private Sub txtCash_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCash.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub


    Private Sub txtCash_TextChanged(sender As Object, e As EventArgs) Handles txtCash.TextChanged
        'input validation on payment form.
        Dim discount As Double = 0
        If txtCash.Text.Trim <> String.Empty Then
            If IsNumeric(txtCash.Text) = True Then
                'if txtcash is not empty then it will take the total and amnt paid from sales form
                total = Double.Parse(lblTotal.Text)
                discount = Double.Parse(lblDiscount.Text)
                amountpaid = Double.Parse(txtCash.Text)
                balance = (amountpaid + discount) - total
                lblChange.Text = balance.ToString
            Else
                lblChange.Text = ""
            End If
        End If
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        Dim Paymode As String = "", _
      TransactionNo As String = "", _
       Amount As Double = 0, _
      TotalAmnt As Double = 0

        'validate paymode cbo
        If cboPaymode.SelectedIndex = -1 Then
            MessageBox.Show("Select paymode to continue", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            Paymode = cboPaymode.Text
        End If
        'add rows to dgv
        dgvPaymode.Rows.Add(Paymode, TransactionNo, 0)
        cboPaymode.SelectedIndex = -1
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        If dgvPaymode.SelectedRows.Count < 1 Then
            MessageBox.Show("Select full record to remove", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            'remove record
            Dim i As Integer = dgvPaymode.CurrentRow.Index
            Dim TotalAmnt As Double = 0
            dgvPaymode.Rows.Remove(dgvPaymode.Rows(i))
            dgvPaymode.Refresh()
            'computeTotal
            For i = 0 To dgvPaymode.RowCount - 1
                TotalAmnt += Double.Parse(dgvPaymode.Rows(i).Cells(2).Value.ToString)
            Next i
            compute_Paymode_dgv()
            txtCash.Text = Double.Parse(TotalAmnt.ToString)
        End If
    End Sub

    Sub compute_Paymode_dgv()
        Dim TotalAmnt As Double = 0, balance As Double = 0
        'computeTotal
        Try
            For i = 0 To dgvPaymode.RowCount - 1
                'validate VAT_Totals
                If dgvPaymode.Rows(i).Cells(2).Value.ToString = String.Empty Then Continue For
                If IsNumeric(dgvPaymode.Rows(i).Cells(2).Value.ToString) Then
                    TotalAmnt += Double.Parse(dgvPaymode.Rows(i).Cells(2).Value.ToString)
                Else
                    dgvPaymode.Rows(i).Cells(2).Value = 0
                End If
            Next i
            txtCash.Text = Double.Parse(TotalAmnt.ToString)
            Dim CustomerPayAmnt As Double = Double.Parse(lblDue.Text.ToString)
            'compute change
            balance = TotalAmnt - CustomerPayAmnt

            lblChange.Text = Double.Parse(balance.ToString)
        Catch ex As Exception
            'do nothing
        End Try
    End Sub

    Private Sub dgvPaymode_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPaymode.CellValueChanged
        compute_Paymode_dgv()
    End Sub

    Private Sub dgvPaymode_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvPaymode.CurrentCellDirtyStateChanged
        If dgvPaymode.IsCurrentCellDirty Then
            dgvPaymode.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub btnProcessPayment_Click(sender As Object, e As EventArgs) Handles btnProcessPayment.Click
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Dim dr As MySqlDataReader = Nothing
        Dim customerid As Integer
        customerid = Integer.Parse(lblcustomerid.Text)
        'input validation on payment form.
        Dim discount As Double = 0
        If txtCash.Text.Trim <> String.Empty Then
            If IsNumeric(txtCash.Text) = True Then
                'if txtcash is not empty then it will take the total and amnt paid from sales form
                total = Double.Parse(lblTotal.Text)
                discount = Double.Parse(lblDiscount.Text)
                amountpaid = Double.Parse(txtCash.Text)
                amountdue = Double.Parse(total - discount)
            End If
        Else
            Exit Sub
        End If
        'check amount entered by cashier
        If (amountpaid < amountdue) Then
            'kama pesa ni kidogo kuliko ile inafaa kulipwa isitoe receipt
            MessageBox.Show("Check amount inserted", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtCash.Focus()
            Exit Sub
        Else
            With frmSales
                .txtchange.Text = lblChange.Text
                .txtamountTOpay.Text = txtCash.Text
                If IsNumeric(txtCash.Text) = True Then
                    If (txtCash.Text.Trim <> String.Empty) Then
                        .total = Double.Parse(lblTotal.Text)
                        .discount = Double.Parse(lblDiscount.Text)
                        '##############################################################
                        'compute VAT 
                        .vat = Double.Parse(lblVAT_Totals.Text.ToString)
                        '##############################################################
                        .purchasemode = "Cash"

                        If Double.Parse(txtCash.Text.Trim) >= Double.Parse(lblTotal.Text.Trim) Then
                            .amountpaid = .total
                        Else
                            .amountpaid = Double.Parse(txtCash.Text)
                        End If

                        If .amountpaid >= .total Then
                            .fullypaid = 1
                        Else
                            .fullypaid = 0
                        End If

                        'sales mode 
                        If .rdbRetailPrice.Checked Then
                            salesMode = "R"
                        ElseIf .rdbWholeSalePrice.Checked Then
                            salesMode = "W"
                        Else
                            salesMode = "ATC"
                        End If
                        'MsgBox(salesMode)
                    End If
                    'SAVE TO MASTER TABLE
                    dtaName.InsertCommand = New MySqlClient.MySqlCommand
                    ' MsgBox(.amountpaid)

                    sale.comitSales(.total, .amountpaid, lblChange.Text, .purchasemode, "Cash", .vat, "Cash", .fullypaid, "", 0, 0, 0, .discount, salesMode, Paymode, TransactionNo, 0, 0, 1)

                    If Double.Parse(txtCash.Text) > 0 Then
                        'INSERT INTO SALE PAYMODE
                        For i = 0 To dgvPaymode.RowCount - 1
                            'If (dgvPaymode.Rows(i).Cells(0).Value <> Nothing) Then Exit Sub
                            If (dgvPaymode.Rows(i).Cells(2).Value = 0) Then Continue For

                            If (dgvPaymode.Rows(i).Cells(0).Value <> Nothing) Then
                                Paymode = dgvPaymode.Rows(i).Cells(0).Value.ToString
                            End If

                            ''validate transaction no
                            'If ((dgvPaymode.Rows(i).Cells(0).Value.ToString) <> "Cash") And (dgvPaymode.Rows(i).Cells(1).Value = Nothing) Then
                            '    MessageBox.Show("Missing transaction number", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                            '    dgvPaymode.CurrentRow.Cells(1).Style.BackColor = Color.Red
                            '    Exit Sub
                            'Else
                            TransactionNo = dgvPaymode.Rows(i).Cells(1).Value.ToString.ToUpper
                            'End If

                            'validate paymode amount
                            If Not IsNumeric(dgvPaymode.Rows(i).Cells(2).Value) OrElse ((dgvPaymode.Rows(i).Cells(2).Value) = 0) Then
                                MessageBox.Show("Invalid pay amount", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                                dgvPaymode.CurrentRow.Cells(2).Style.BackColor = Color.Red
                                Exit Sub
                            Else
                                PaymodeAmnt = Double.Parse(dgvPaymode.Rows(i).Cells(2).Value.ToString)
                            End If

                            'INSERT INTO SALE PAYMODE
                            sale.Insert_To_SalesPaymode(getMasterNo(), Paymode, TransactionNo, PaymodeAmnt)
                        Next
                    End If

                    'INSERT INTO CASH LEDGER
                    If .amountpaid > 0 Then
                        CashLedger(getMasterNo(), .amountpaid, Now(), userid, "Customer cash payment", "+", "Sales", 1)
                    End If
                End If
            End With

            'insert data in sales details
            Dim barcode As String = "", bp As Double, _
                sp As Double, qty As Integer, _
                totalperitem As Double, discountperitem As Double = 0,
                vatamnt As Double

            With frmSales
                For i = 0 To .dgvSales.RowCount - 1
                    If (.dgvSales.Rows(i).Cells(0).Value <> Nothing) Then
                        qty = Integer.Parse(.dgvSales.Rows(i).Cells(1).Value.ToString)
                        sp = Double.Parse(.dgvSales.Rows(i).Cells(2).Value.ToString)
                        totalperitem = Double.Parse(.dgvSales.Rows(i).Cells(3).Value.ToString)
                        barcode = .dgvSales.Rows(i).Cells(4).Value.ToString
                        discountperitem = Double.Parse(.dgvSales.Rows(i).Cells(6).Value.ToString)
                        bp = Double.Parse(.dgvSales.Rows(i).Cells(7).Value.ToString)
                        vatamnt = Double.Parse(.dgvSales.Rows(i).Cells(8).Value.ToString)

                        ''MsgBox(TransactionNo)

                        sale.comitSales(totalperitem, 0, 0, "", "", 0, "", 0, barcode, bp, sp, qty, discountperitem, salesMode, Paymode, TransactionNo, vatamnt, 0, 2)

                        'INSERT INTO Stock LEDGER
                        StockLedger(getMasterNo(), barcode, qty, sp, Now(), userid, "Sales of stock in cash", "-", "Sales", 1)

                        'INSERT INTO STOCK MOVEMENT
                        stockMovement(barcode, "cash sales", "-", qty, sp, bp, Now(), userid, 1)
                    End If

                Next
                '####################################################################
                If rdoYes.Checked Then
                    'print receipt
                    printreceipt()
                End If
                '####################################################################

                'clear controls
                .dgvSales.Rows.Clear()
                .txtBarcode.Clear()
                .txttotalamount.Text = "0"
                .txtDiscount.Text = "0"
            End With
            lblTotal.Text = ""
            lblChange.Text = ""
            lblDiscount.Text = ""
            lblDue.Text = ""
            txtCash.Clear()
            dgvPaymode.Rows.Clear()
            Me.Hide()
            frmSales.txtBarcode.Focus()
            frmSales.txtamountTOpay.Clear()
        End If
    End Sub

    Private Sub printreceipt()
        Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim sp As Double = 0, qty As Double = 0, tcost As Double = 0
        Dim sTotal As Double = 0
        'Dim RcptHeader As String = frmReceiptSettings.txtReceiptHeader.Text

        Try
            With dt
                .Columns.Add("Description")
                .Columns.Add("qty")
                .Columns.Add("sp")
                .Columns.Add("total")
                .Columns.Add("sumtotal")
            End With

            With frmSales
                For i = 0 To .dgvSales.RowCount - 2
                    If .dgvSales.Rows(i).Cells(1).Value = Nothing Then Continue For
                    sp = Double.Parse(.dgvSales.Rows(i).Cells(2).Value.ToString())
                    qty = Double.Parse(.dgvSales.Rows(i).Cells(1).Value.ToString())
                    'VAT = Double.Parse(.vat)
                    tcost = sp * qty
                    sTotal += tcost
                    dt.Rows.Add(.dgvSales.Rows(i).Cells(0).Value.ToString(), qty, sp, tcost, sTotal)
                Next
            End With

            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New salesReceipt
            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("ServedBy", fullname)
            rptDoc.SetParameterValue("receiptNo", receiptNo)
            rptDoc.SetParameterValue("cash", Double.Parse(txtCash.Text))
            rptDoc.SetParameterValue("change", lblChange.Text.ToString)
            rptDoc.SetParameterValue("Total", lblDue.Text.ToString)
            rptDoc.SetParameterValue("Discount", lblDiscount.Text.ToString)
            rptDoc.PrintToPrinter(1, False, 0, 0)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvPaymode_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPaymode.CellContentClick

    End Sub

    Private Sub frmpayment_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class