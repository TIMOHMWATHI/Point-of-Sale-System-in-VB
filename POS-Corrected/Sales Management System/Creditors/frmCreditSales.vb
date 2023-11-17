Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Public Class frmCreditSales
    Dim inventorystock As New inventory
    'Dim ledger As New ClassMilkDeliveryRegistry
    Dim sale As New posClass

    'variables
    Dim customerid As Integer, phonenumber As String 'customerid was string before change
    Dim receiptno As Long, totalamnt As Double, amntpaid As Double, discnt As Double, _
        datedue As Date, creditstatus As Integer, supplier_id As Integer, _
        othernotes As String, strsql As String
    Dim balance As Double, NEWCUSTOMER As Boolean = False, salesMode As String

    Dim receiptNumber As Long
    'On formload event
    Dim FORMLOADING As Boolean = True

    'SAVE TO PAYMODE
    Dim Paymode As String, TransactionNo As String, PaymodeAmnt As Double = 0

    Private Sub frmCreditSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        computechange()
        FORMLOADING = False
        'Auto search creditor names
        LoadCreditorNames(txtCreditorName)
        txtCreditorName.Focus()
        lblOpenningCredit.Text = "0"
    End Sub

    Private Sub lblCreditsTotal_TextChanged1(sender As Object, e As EventArgs) Handles lblCreditsTotal.TextChanged
        Dim total As Double, amountpaid As Double, balance As Double
        Try
            If lblTotal.Text <> String.Empty Then
                Exit Sub
            Else
                total = Double.Parse(lblCreditsTotal.Text)
                amountpaid = Double.Parse(txtCreditsAmntPaid.Text)

                balance = Double.Parse(total - amountpaid)

                lblCreditsBalRemaining.Text = Double.Parse(balance)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnAddNew_Click(sender As Object, e As EventArgs)
        txtCustomerName.Clear()
        txtPhoneNumber.Clear()
        With frmCustomersDialog
            .StartPosition = FormStartPosition.CenterParent
            .WindowState = FormWindowState.Normal
            .ShowDialog()
        End With
    End Sub

    'FIND OPEN RECEIPT
    Private Function findOpenReceipt(ByVal customerid As Integer) As Long
        Dim sqlQuery As String
        sqlQuery = "SELECT sm.receptno FROM salesmaster sm WHERE sm.customerid = " & customerid & " " _
                   & "  AND DATE(sm.receptdatetime) = CURDATE() And sm.fullypaid = 0 LIMIT 1"

        Dim datareader As MySqlDataReader
        Dim receiptNo As Long = 0
        Dim cmd As New MySqlCommand(sqlQuery, Conn)
        cmd.CommandType = CommandType.Text
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While datareader.Read
                receiptNo = Long.Parse(datareader(0).ToString)
            End While
        Else
            receiptNo = -1
        End If
        datareader.Dispose()
        closeconn()
        Return receiptNo
    End Function

    Private Sub txtCreditorName_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCreditorName.KeyDown
        'input validation on txtfarmernames
        If e.KeyCode = Keys.Enter Then    'work if enter button is clicked
            'Dim OpeningBalance As Double = Double.Parse(lblOpenningCredit.Text)

            If (txtCreditorName.Text = String.Empty) Then
                txtCreditorName.Focus()
                Exit Sub
            End If

            Dim datareader As MySqlDataReader
            'Try
            Dim strsql = "SELECT fullname,phone,supplier_id FROM suppliers" _
                         & " WHERE (fullname='" & txtCreditorName.Text.Trim & "') " _
                         & " AND deleted= 0 "

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                txtCreditorName.Text = datareader(0).ToString
                txtCustomerName.Text = datareader(0).ToString
                txtPhoneNumber.Text = datareader(1).ToString
                lblEntryid.Text = datareader(2).ToString
                'validate Opening credit
                'If Not IsNumeric(lblOpenningCredit.Text) Then
                '    lblOpenningCredit.Text = 0
                '    MsgBox(lblOpenningCredit.Text)
                'Else
                getCustomerCredit(datareader(2).ToString, lblOpenningCredit)
                'End If

            End While
            datareader.Dispose()
            closeconn()
        End If
    End Sub

    Private Sub btnCreditsSearch_Click(sender As Object, e As EventArgs) Handles btnCreditsSearch.Click
        'retreave item using its phone number
        Dim datareader As MySqlDataReader
        'Dim customername As String = "", phonenumber As String, strsql As String
        Dim fullnames As String = "", telephone As String, strsql As String
        Dim customerID As Integer = 0
        'assign phone number
        phonenumber = txtcreditssearch.Text.Trim
        strsql = "select fullname,phone,supplier_id from suppliers where phone ='" & phonenumber & "' and relationtype = 'C'"
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        datareader = cmd.ExecuteReader
        If datareader.HasRows Then
            While datareader.Read
                fullnames = datareader(0).ToString
                telephone = datareader(1).ToString
                customerID = Integer.Parse(datareader(2).ToString)
            End While
            txtCustomerName.Text = fullnames
            txtPhoneNumber.Text = phonenumber
            txtCustomerID.Text = customerID.ToString
            datareader.Dispose()
            closeconn()
        Else
            txtCustomerName.Clear()
            txtPhoneNumber.Clear()
            txtCustomerID.Clear()
            NEWCUSTOMER = True
            With frmCustomersDialog
                .StartPosition = FormStartPosition.CenterParent
                .WindowState = FormWindowState.Normal
                .ShowDialog()
            End With
        End If
        closeconn()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim strsql As String = Nothing
        Dim dtaName As New MySqlClient.MySqlDataAdapter
        Dim dr As MySqlDataReader = Nothing
        'customer name input validation
        If NEWCUSTOMER = True Then
            customerid = customerno
        Else
            If (txtCreditorName.Text = String.Empty) Then
                txtCreditorName.Focus()
                Exit Sub
            Else
                customerid = Integer.Parse(lblEntryid.Text.ToString)
            End If

        End If

        'Get receipt number if it exists

        receiptNumber = findOpenReceipt(customerid)
        '==========================================
        With frmSales
            'kama hakuna pesa imewekwa then isifanye kitu
            If (txtCreditsAmntPaid.Text.Trim <> String.Empty) Then

                'save data to sales master
                .total = Double.Parse(lblTotal.Text)
                totalamnt = .total

                '##############################################################
                'compute VAT 
                .vat = Double.Parse(lblVAT_Totals.Text.ToString)
                '##############################################################
                .purchasemode = "Credit"
                .amountpaid = Double.Parse(txtCreditsAmntPaid.Text)
                discnt = Double.Parse(lblDisc.Text)
                amntpaid = .amountpaid
                If .amountpaid >= .total Then
                    .fullypaid = 1
                Else
                    .fullypaid = 0
                End If

                If frmSales.rdbRetailPrice.Checked Then
                    salesMode = "R"
                Else
                    salesMode = "W"
                End If

                ' MsgBox(Now())
                If (receiptNumber < 0) Then

                    strsql = "INSERT INTO salesmaster(receptdatetime,totalamount,amountpaid,discount,amountreturned," _
                             & " purchasemode,soldby,VATamount,paymode,transactionno,fullypaid,customerid,salesmode)" _
                             & " VALUES(NOW()," & .total & "," & .amountpaid & "," & .discount & ",0," _
                             & " '" & .purchasemode & "','" & userid & "'," & .vat & "," _
                             & " '" & Paymode & "','" & TransactionNo & "'," & .fullypaid & "," & customerid & ", " _
                             & " '" & salesMode & "')"

                Else

                    strsql = "UPDATE salesmaster SET totalamount = totalamount + " & .total & " , " _
                             & " VATamount = VATamount + " & .vat & ", amountpaid = amountpaid + " & .amountpaid & ", " _
                             & " discount = discount + " & .discount & " WHERE receptno = " & receiptNumber & ""

                End If

                dtaName.InsertCommand = New MySqlClient.MySqlCommand
                With dtaName.InsertCommand
                    .CommandTimeout = 60
                    .Connection = Conn()
                    .CommandType = CommandType.Text
                    .CommandText = strsql
                    .ExecuteNonQuery()
                End With
            End If
            dtaName.Dispose()
            closeconn()
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.StoredProcedure
                strsql = "sp_managecreditaccounts"
                .CommandText = strsql
                .Parameters.AddWithValue("@incustomerid", customerid)
                .Parameters.AddWithValue("@intransactiondate", Today())
                .Parameters.AddWithValue("@increditamount", totalamnt)
                .Parameters.AddWithValue("@inamountpaid", amntpaid)
                .Parameters.AddWithValue("@indiscount", discnt)
                .Parameters.AddWithValue("@indeterminer", 1)
                .ExecuteNonQuery()
            End With
            dtaName.Dispose()
            closeconn()
        End With
        'sales details
        'retrieve receiptno from sales master
        If (receiptNumber < 0) Then
            strsql = "select max(receptno) from salesmaster"
            Dim cmd As New MySqlCommand(strsql, Conn())
            cmd.CommandType = CommandType.Text
            dr = cmd.ExecuteReader
            If (dr.HasRows) Then
                While dr.Read
                    frmSales.receiptno = Long.Parse(dr(0).ToString)
                    receiptno = Long.Parse(dr(0).ToString)
                End While
            End If
            dr.Dispose()
            closeconn()
        Else
            receiptno = receiptNumber
        End If

        'SAVE TO PAYMODE TABLE
        If Double.Parse(txtCreditsAmntPaid.Text) > 0 Then
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
                sale.Insert_To_SalesPaymode(receiptno, Paymode, TransactionNo, PaymodeAmnt)
            Next
        End If
        'insert data in sales details
        Dim barcode As String = "", bp As Double, sp As Double, _
            qty As Integer, totalperitem As Double, _
            discountperitem As Double = 0, item_VAT As Double

        With frmSales
            For i = 0 To .dgvSales.RowCount - 1
                If (.dgvSales.Rows(i).Cells(0).Value <> Nothing) Then
                    qty = Integer.Parse(.dgvSales.Rows(i).Cells(1).Value.ToString)
                    sp = Double.Parse(.dgvSales.Rows(i).Cells(2).Value.ToString)
                    totalperitem = Double.Parse(.dgvSales.Rows(i).Cells(3).Value.ToString)
                    barcode = .dgvSales.Rows(i).Cells(4).Value.ToString
                    discountperitem = Double.Parse(.dgvSales.Rows(i).Cells(6).Value.ToString)
                    bp = Double.Parse(.dgvSales.Rows(i).Cells(7).Value.ToString)
                    item_VAT = Double.Parse(.dgvSales.Rows(i).Cells(8).Value.ToString)
                    'MsgBox(bp)
                    sale.comitSales(totalperitem, 0, 0, "", "", 0, "", 0, barcode, bp, sp, qty, discountperitem, "", "", "", item_VAT, 0, 2)

                    'INSERT INTO STOCK LEDGER
                    StockLedger(receiptno, barcode, qty, sp, Now(), userid, "Sales of stock on credit", "-", "Credit_Sales", 1)

                    'INSERT INTO STOCK MOVEMENT
                    stockMovement(barcode, "Invoice sale", "-", qty, sp, bp, Now(), userid, 1)
                End If
            Next
        End With
        Me.Hide()

        '0==============
        'input validation on dtp datedue
        datedue = dtpDateDue.Value.Date

        'other notes input text area
        othernotes = txtOtherNotes.Text.Trim

        'credits status validation
        If (txtCreditsAmntPaid.Text.Trim.Length < 1) Then
            amntpaid = 0
        Else
            amntpaid = Double.Parse(txtCreditsAmntPaid.Text.Trim)
        End If

        balance = Double.Parse(totalamnt - amntpaid)
        'payment ststus
        If totalamnt = amntpaid Then
            creditstatus = 1
        ElseIf amntpaid > 0 And amntpaid < totalamnt Then
            creditstatus = 2
        Else
            creditstatus = 0
        End If

        Try
            'save data into data base
            strsql = "INSERT INTO creditsales (receiptno,totalamount,amountpaid,discounts,datedue,creditstatus,supplier_id)" _
                      & " VALUES(" & receiptno & "," & totalamnt & "," & amntpaid & "," _
                      & " " & discnt & ",'" & datedue & "', " & creditstatus & ",'" & customerid & "')"

            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                .CommandText = strsql
                .ExecuteNonQuery()
            End With

            'update credit narration report
            If (amntpaid > 0) Then
                sale.creditupdationdetails(receiptno, customerid, amntpaid, Now())
                'INSERT INTO CASH LEDGER
                CashLedger(receiptno, amntpaid, Now(), userid, "Customer invoice payment", "+", "Sales", 1)
            End If

            ''####################################################################
            If rdoYes.Checked Then
                ''****print invoice receipt****'
                PrintInvoice()
            End If
            ''####################################################################
            closeconn()
            'clear input areas
            frmSales.dgvSales.Rows.Clear()
            clearcontent()

        Catch ex As Exception
            closeconn()
            MsgBox(ex.Message & " Error occured while saving data.If error persists," _
                   & " Inform your administrator for further action", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub lblCreditsBalRemaining_TextChanged1(sender As Object, e As EventArgs) Handles lblCreditsBalRemaining.TextChanged
        computeClosingTotal()
    End Sub

    Private Sub lblOpenningCredit_TextChanged1(sender As Object, e As EventArgs) Handles lblOpenningCredit.TextChanged
        computeClosingTotal()
    End Sub

    Private Sub lblClosingCredit_TextChanged(sender As Object, e As EventArgs) Handles lblClosingCredit.TextChanged
        computeClosingTotal()
    End Sub

    Sub computechange()
        Try
            'computes credit sales
            If FORMLOADING = False Then
                Dim creditsTotal As Double, amntpaid As Double, balanceremaining As Double
                creditsTotal = Double.Parse(lblCreditsTotal.Text)

                If txtCreditsAmntPaid.Text.Trim = String.Empty Then
                    amntpaid = 0
                Else
                    amntpaid = Double.Parse(txtCreditsAmntPaid.Text)
                End If
                balanceremaining = creditsTotal - amntpaid
                lblCreditsBalRemaining.Text = Double.Parse(balanceremaining.ToString)
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub computeClosingTotal()
        Dim openingcredit As Double = 0, _
            credits As Double = 0, _
            closingcredit As Double = 0
        If IsNumeric(lblOpenningCredit.Text) Then
            openingcredit = Double.Parse(lblOpenningCredit.Text.ToString)
        Else
            openingcredit = 0
        End If
        If IsNumeric(lblCreditsBalRemaining.Text) Then
            credits = Double.Parse(lblCreditsBalRemaining.Text.ToString)
        Else
            credits = 0
        End If
        'compute
        closingcredit = (openingcredit + credits)
        lblClosingCredit.Text = Double.Parse(closingcredit.ToString)
    End Sub

    Private Sub dgvPaymode_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvPaymode.CellValueChanged
        compute_Paymode_dgv()
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
            txtCreditsAmntPaid.Text = Double.Parse(TotalAmnt.ToString)
            'Dim CustomerPayAmnt As Double = Double.Parse(lblDue.Text.ToString)
            ''compute change
            'balance = TotalAmnt - CustomerPayAmnt

            'lblChange.Text = Double.Parse(balance.ToString)
        Catch ex As Exception
            'do nothing
        End Try
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
            txtCreditsAmntPaid.Text = Double.Parse(TotalAmnt.ToString)
        End If
    End Sub

    Private Sub dgvPaymode_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvPaymode.CurrentCellDirtyStateChanged
        If dgvPaymode.IsCurrentCellDirty Then
            dgvPaymode.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub PrintInvoice()
        Dim receiptNo As Long = Long.Parse(Now().Date.Day & Now().Date.Month & Now().Date.Year & Now().TimeOfDay.Hours & Now().TimeOfDay.Minutes)
        Dim dt As New DataTable
        Dim sp As Double = 0, qty As Double = 0, tcost As Double = 0, VAT As Double = 0
        Dim sTotal As Double = 0
        Dim OpeningCredit As Double = 0

        Try
            With dt
                .Columns.Add("Description")
                .Columns.Add("qty")
                .Columns.Add("sp")
                .Columns.Add("total")
                .Columns.Add("sumtotal")
            End With

            With frmSales
                For i = 0 To .dgvSales.RowCount - 1
                    If .dgvSales.Rows(i).Cells(1).Value = Nothing Then Continue For
                    sp = Double.Parse(.dgvSales.Rows(i).Cells(2).Value.ToString())
                    qty = Double.Parse(.dgvSales.Rows(i).Cells(1).Value.ToString())
                    VAT = Double.Parse(lblVAT_Totals.Text.ToString)
                    tcost = sp * qty
                    sTotal += tcost
                    dt.Rows.Add(.dgvSales.Rows(i).Cells(0).Value.ToString(), qty, sp, tcost, sTotal)
                Next
            End With
            Dim rptDoc As CrystalDecisions.CrystalReports.Engine.ReportDocument

            rptDoc = New SalesInvoiceReceipt

            rptDoc.SetDataSource(dt)
            rptDoc.SetParameterValue("ServedBy", fullname)
            rptDoc.SetParameterValue("receiptNo", receiptNo)
            rptDoc.SetParameterValue("cash", Double.Parse(txtCreditsAmntPaid.Text))
            rptDoc.SetParameterValue("Balance", Double.Parse(lblCreditsBalRemaining.Text.ToString))
            rptDoc.SetParameterValue("Discount", Double.Parse(lblDisc.Text.ToString))
            rptDoc.SetParameterValue("AmntDue", Double.Parse(lblCreditsTotal.Text.ToString))
            rptDoc.SetParameterValue("OpeningCredit", Double.Parse(lblOpenningCredit.Text.ToString))
            rptDoc.SetParameterValue("customer", txtCreditorName.Text)
            rptDoc.SetParameterValue("ClosingCredit", Double.Parse(lblClosingCredit.Text.ToString))
            rptDoc.SetParameterValue("V.A.T", Double.Parse(VAT))

            'Print directly to printer
            rptDoc.PrintToPrinter(1, False, 0, 0)
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub txtCreditsAmntPaid_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCreditsAmntPaid.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub txtCreditsAmntPaid_TextChanged(sender As Object, e As EventArgs) Handles txtCreditsAmntPaid.TextChanged
        computechange()
    End Sub

    Sub clearcontent()
        lblEntryid.Text = "0"
        txtCreditorName.Clear()
        txtCustomerName.Clear()
        txtPhoneNumber.Clear()
        dtpDateDue.Value = Today
        txtOtherNotes.Clear()
        lblTotal.Text = ""
        lblDisc.Text = ""
        txtCreditsAmntPaid.Clear()
        lblCreditsBalRemaining.Text = ""
        lblOpenningCredit.Text = ""
        lblClosingCredit.Text = ""
        btnSave.Text = "Save"
        NEWCUSTOMER = False
        rdoYes.Checked = True
    End Sub

    Private Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        txtcreditssearch.Clear()
        txtCustomerName.Clear()
        txtPhoneNumber.Clear()
        dtpDateDue.Value = Today
        txtOtherNotes.Clear()
    End Sub

    Private Sub txtcreditssearch_KeyPress1(sender As Object, e As KeyPressEventArgs) Handles txtcreditssearch.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Strawberry Solution") = MsgBoxResult.Yes) Then
        Me.Close()
        'End If
    End Sub
End Class