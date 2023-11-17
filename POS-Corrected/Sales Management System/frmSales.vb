Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmSales

    Public total As Double = 0, vat As Double, discount As Double = 0
    Public purchasemode As String, amountpaid As Double, receiptno As Long
    Public itemcode As String, sp As Double, qty As Integer, _
           totalcost As Double  'sp refers to the SellingPrice
    Public paymentdetails As String, fullypaid As Byte, StockBal As Integer
    Dim NEWCUSTOMER As Boolean = False
    Dim customerid As Integer = 0

    'Dim VATPercent As Double = Double.Parse(lblVATRate.Text.ToString)

    Private Sub frmSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblServed.Text = fullname
        'CALL THE AUTOSEARCH CODE
        Loadproducts(txtBarcode)
        'LoadSalesSearch(txtBarcode)
        txtBarcode.Focus()
        'load vat rate
        lblVATRate.Text = Double.Parse(get_VATRate())
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs)
        Timer1.Start()
    End Sub

    Private Function get_VATRate() As Double

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
                End If
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return VATRate
    End Function


    Sub computeTotal()
        total = 0
        discount = 0
        Dim VAT_Totals As Double
        For i = 0 To dgvSales.RowCount - 1

            If dgvSales.Rows(i).Cells(3).Value = vbEmpty Then
                'do nothing
            ElseIf ((dgvSales.Rows(i).Cells(6).Value) > (((dgvSales.Rows(i).Cells(1).Value) * (dgvSales.Rows(i).Cells(2).Value)) - ((dgvSales.Rows(i).Cells(1).Value) * (dgvSales.Rows(i).Cells(9).Value)))) Then
                'refuse to proceed with discount
                MessageBox.Show("Invalid discount amnt to complete the sale, " & vbNewLine _
                              & "Discount is soo high and would result to a loss", "Strawberry POS")

                dgvSales.CurrentRow.Cells(6).Value = 0
                Exit Sub

            Else
                total += Double.Parse(dgvSales.Rows(i).Cells(3).Value.ToString)

                'validate discount
                If dgvSales.Rows(i).Cells(6).Value.ToString = String.Empty Then Continue For
                If IsNumeric(dgvSales.Rows(i).Cells(6).Value.ToString) Then
                    discount += Double.Parse(dgvSales.Rows(i).Cells(6).Value.ToString)
                Else
                    dgvSales.Rows(i).Cells(6).Value = 0
                End If

                'validate VAT_Totals
                If dgvSales.Rows(i).Cells(8).Value.ToString = String.Empty Then Continue For
                If IsNumeric(dgvSales.Rows(i).Cells(8).Value.ToString) Then
                    VAT_Totals += Double.Parse(dgvSales.Rows(i).Cells(8).Value.ToString)
                Else
                    dgvSales.Rows(i).Cells(8).Value = 0
                End If
            End If
        Next
        txttotalamount.Text = total
        txtDiscount.Text = discount.ToString
        txtamountTOpay.Text = Double.Parse(total - (discount.ToString))
        lblVATTotals.Text = VAT_Totals.ToString
        total = 0
    End Sub

    Private Sub dgvSales_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSales.CellValueChanged
        'compute total once quantity changes
        Try
            Dim price As Double, qty As Integer, total As Double, _
                Instock As Integer, VAT_Rate As Double, barcode As String, _
                ItemBP As Double

            If dgvSales.RowCount > 1 Then
                qty = Integer.Parse(dgvSales.CurrentRow.Cells(1).Value.ToString)
                price = Double.Parse(dgvSales.CurrentRow.Cells(2).Value.ToString)
                Instock = Integer.Parse(dgvSales.CurrentRow.Cells(5).Value.ToString)
                barcode = dgvSales.CurrentRow.Cells(4).Value.ToString
                ItemBP = Double.Parse(dgvSales.CurrentRow.Cells(7).Value.ToString)
                VAT_Rate = Double.Parse(get_VAT_Rate)

                SELLWHENOUTOFSTOCK = checksetting("outofstock", "sp_checksetting")
                If SELLWHENOUTOFSTOCK = 0 Then

                    'validate qty to be sold
                    If (qty > Instock) Then

                        MessageBox.Show("Invalid sale qty to complete the sale, " & vbNewLine _
                                       & "Qty is higher than what is in stock", "Strawberry POS")

                        dgvSales.CurrentRow.Cells(1).Value = Integer.Parse(Instock)
                        Exit Sub
                    End If
                End If

                'compute total
                total = price * qty

                'compute VAT
                Dim Item_TAX_Rating As Integer = Check_If_Item_Is_VAT_Rated(barcode)

                If Item_TAX_Rating = 0 Then
                    'don't compute
                Else
                    vat = (VAT_Rate / 100) * (total - (qty * ItemBP))
                End If

                dgvSales.CurrentRow.Cells(3).Value = total
                dgvSales.CurrentRow.Cells(8).Value = vat
                computeTotal()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub dgvSales_KeyDown(sender As Object, e As KeyEventArgs) Handles dgvSales.KeyDown
        If ((e.KeyCode = Keys.Enter) Or (e.KeyCode = Keys.Space)) Then
            txtBarcode.Focus()
        End If
    End Sub

    Private Sub RemoveProductToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RemoveProductToolStripMenuItem.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.cancel_remove_from_sale) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'With frmSalesAdminLogin
        '    .StartPosition = FormStartPosition.CenterScreen
        '    .WindowState = FormWindowState.Normal
        '    .TopMost = True
        '    .Show()
        'End With
        Try
            Dim i As Integer = dgvSales.CurrentRow.Index
            dgvSales.Rows.Remove(dgvSales.Rows(i))
            dgvSales.Refresh()
            computeTotal()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnCreditSales_Click(sender As Object, e As EventArgs) Handles btnCreditSales.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.sell_on_credit) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCreditSales
            .WindowState = FormWindowState.Normal
            .StartPosition = FormStartPosition.CenterScreen
            .lblTotal.Text = txttotalamount.Text
            .lblDisc.Text = txtDiscount.Text
            .lblCreditsTotal.Text = (txttotalamount.Text - txtDiscount.Text)
            .lblCreditsBalRemaining.Text = .lblCreditsTotal.Text
            .lblVAT_Totals.Text = lblVATTotals.Text
            .txtCreditsAmntPaid.Text = 0
            .lblOpenningCredit.Text = 0
            .lblClosingCredit.Text = 0
            .Show()
        End With
    End Sub

    Private Sub btnPayCredits_Click(sender As Object, e As EventArgs) Handles btnPayCredits.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.receive_credit_payment) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCreditors
            .MdiParent = mdiSales
            .StartPosition = FormStartPosition.WindowsDefaultLocation
            .WindowState = FormWindowState.Maximized
            .Show()
            .Location = New Point(10, 10)
        End With
    End Sub

    Private Sub btnAddNewCustomer_Click(sender As Object, e As EventArgs) Handles btnAddNewCustomer.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.create_credit_customer) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCustomersDialog
            .StartPosition = FormStartPosition.CenterParent
            .WindowState = FormWindowState.Normal
            .ShowDialog()
        End With
    End Sub

    Private Function checksetting(ByVal settingName As String, _
                                  ByVal sqlQuerry As String) As Integer
        Dim setValue As Integer
        Dim datareader As MySqlDataReader
        Dim cmd As New MySqlCommand(sqlQuerry, Conn)
        cmd.CommandType = CommandType.StoredProcedure
        cmd.Parameters.AddWithValue("@inname", settingName)
        datareader = cmd.ExecuteReader
        While datareader.Read
            setValue = Integer.Parse(datareader(0).ToString)
        End While
        datareader.Dispose()
        closeconn()

        Return setValue
    End Function

    Private Sub txtBarcode_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBarcode.KeyDown
        'retreave item using its barcode
        Dim datareader As MySqlDataReader
        Dim itemname As String = "", price As Double = 0, _
            barcode As String = "", StockBal As Integer = 0, _
            ItemBP As Double = 0, Is_VAT_Rated As Integer, _
            ItemMinSP As Double = 0

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
                '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                'ASSIGN USER PRIVILEDGES CODE
                If Not Authorized(userid, TaskID.retail_sale) Then Exit Sub
                '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                strsql = "SELECT q1.Description," _
                         & " IFNULL(q2.retailprice,0) AS Retail," _
                         & " q2.b2," _
                         & " IFNULL(q2.quantityinstock,0) as InStock," _
                         & " IFNULL(q2.buyingprice,0) AS BP," _
                         & " IFNULL(q1.VAT,0) AS VAT_Rated, " _
                         & " IFNULL(q2.minprice,0) AS MinSP " _
                         & " FROM" _
                         & " (SELECT DISTINCT(p.Barcode) AS b1,p.Description," _
                         & " pc.categoryname,p.vatrated AS VAT FROM products p" _
                         & " INNER JOIN product_category pc ON " _
                         & " pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                         & " Left Join" _
                         & " (SELECT DISTINCT(pl.productid) AS b2,s.buyingprice,pl.retailprice," _
                         & " s.quantityinstock,pl.minprice FROM pricelist pl" _
                         & " INNER JOIN stock s ON s.itemcode=pl.productid WHERE pl.active!=0) AS q2 " _
                         & " ON q2.b2=q1.b1 WHERE (q2.b2='" & itemcode & "'" _
                         & " OR q1.Description='" & itemcode & "') LIMIT 1"

                'INNER JOIN deliverydetails dd ON dd.barcode=pl.productid" _
                '        & " WHERE pl.active!=0 AND DATE(dd.expirydate)>CURDATE() AND" _
                '        & " TIMESTAMPDIFF(DAY,CURDATE(),dd.expirydate)>1

               

            ElseIf rdbWholeSalePrice.Checked Then

                '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                'ASSIGN USER PRIVILEDGES CODE
                If Not Authorized(userid, TaskID.whole_sale) Then Exit Sub
                '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                strsql = "SELECT q1.Description," _
                         & " IFNULL(q2.wholesaleprice,0) AS Wholesale," _
                         & " q2.b2," _
                         & " IFNULL(q2.quantityinstock,0) AS InStock," _
                         & " IFNULL(q2.buyingprice,0) AS BP," _
                         & " IFNULL(q1.VAT,0) AS VAT_Rated," _
                         & " IFNULL(q2.minprice,0) AS MinSP " _
                         & " FROM " _
                         & " (SELECT p.Barcode AS b1,p.Description," _
                         & " pc.categoryname,p.vatrated AS VAT FROM products p" _
                         & " INNER JOIN product_category pc ON " _
                         & " pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                         & " Left Join" _
                         & " (SELECT pl.productid AS b2,s.buyingprice,pl.wholesaleprice," _
                         & " s.quantityinstock,pl.minprice FROM pricelist pl" _
                         & " INNER JOIN stock s ON s.itemcode=pl.productid WHERE pl.active!=0) AS q2 " _
                         & " ON q2.b2=q1.b1 WHERE (q2.b2='" & itemcode & "'" _
                         & " OR q1.Description='" & itemcode & "') LIMIT 1"

                '& " INNER JOIN deliverydetails dd ON dd.barcode=pl.productid WHERE pl.active!=0 " _
                '        & " AND DATE(dd.expirydate)>CURDATE() AND" _
                '        & " TIMESTAMPDIFF(DAY,CURDATE(),dd.expirydate)>1

            Else
                '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
                'ASSIGN USER PRIVILEDGES CODE
                If Not Authorized(userid, TaskID.atc_sale) Then Exit Sub
                '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

                strsql = "SELECT p.Description,s.buyingprice,p.Barcode," _
                         & " s.quantityinstock,s.buyingprice,p.vatrated FROM " _
                         & " products p INNER JOIN pricelist pl ON p.Barcode=pl.productid" _
                         & " INNER JOIN stock s ON s.itemcode=pl.productid WHERE" _
                         & " pl.active=1 AND p.deleted=0 AND (p.Barcode='" & itemcode & "' " _
                         & " OR p.Description = '" & itemcode & "') LIMIT 1"

            End If

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                itemname = datareader(0).ToString
                price = Double.Parse(datareader(1).ToString)
                barcode = datareader(2).ToString
                StockBal = Integer.Parse(datareader(3).ToString)
                ItemBP = Double.Parse(datareader(4).ToString)
                Is_VAT_Rated = Integer.Parse(datareader(5).ToString)
                ItemMinSP = Double.Parse(datareader(6).ToString)
                '++++++++++++++

                If (price <= ItemBP) Then
                    MessageBox.Show("Revisit and adjust your prices" & vbNewLine & _
                                    "Buying price greater then selling" & vbNewLine & _
                                    "Invalid settings that may result to a loss", "Strawberry POS")
                    txtBarcode.Clear()
                    txtBarcode.Focus()
                    Exit Sub
                End If

                If (ItemBP < 1) Then
                    MessageBox.Show("Item buying price is zero" & vbNewLine & _
                                    "Adjust it to continue", "Strawberry POS")
                    txtBarcode.Clear()
                    txtBarcode.Focus()
                    Exit Sub
                End If

                '############
                'Add similar products to one row
                Dim VAT_Rate As Double = Double.Parse(get_VAT_Rate)
                Dim Qty_Count As Decimal = CheckDuplicateExists_Item(itemname, dgvSales, "colDescription")
                Dim Qty As Integer = 1 + Qty_Count

                Dim TotalCost As Double = Qty * price
                computeTotal()

                'compute VAT
                Dim VATAmnt As Double = 0
                Dim Item_TAX_Rating As Integer = Check_If_Item_Is_VAT_Rated(barcode)

                If Item_TAX_Rating = 0 Then
                    'don't compute
                    VATAmnt = 0
                Else
                    VATAmnt = (VAT_Rate / 100) * (TotalCost - (Qty * ItemBP))
                End If

                SELLWHENOUTOFSTOCK = checksetting("outofstock", "sp_checksetting")
                If SELLWHENOUTOFSTOCK = 0 Then

                    If StockBal < 1 Then
                        MessageBox.Show("Item completely out of stock, " & vbNewLine _
                                     & " Add stock inorder to continue", "Strawberry POS")
                        txtBarcode.Clear()
                        txtBarcode.Focus()
                        Exit Sub
                    End If

                    If (Qty > StockBal) Then
                        'qty equals stock bal
                        Qty = StockBal
                        TotalCost = Qty * price
                        computeTotal()
                    End If
                End If

                ''############
                'MsgBox("Item Code:" & barcode & " Is vat rated" & Item_TAX_Rating)
                dgvSales.Rows.Add(itemname, Qty, price, TotalCost, barcode, StockBal, 0, ItemBP, VATAmnt, ItemMinSP)

                '+++++++++++++
            End While
            txtBarcode.Clear()
            computeTotal()
            datareader.Dispose()
            closeconn()
        ElseIf e.KeyCode = Keys.Space Then
            'CALL PAYMENTS FORM
            With frmpayment
                .lblTotal.Text = txttotalamount.Text
                .lblDiscount.Text = txtDiscount.Text
                .lblVAT_Totals.Text = lblVATTotals.Text
                .lblDue.Text = Double.Parse(txttotalamount.Text) - Double.Parse(txtDiscount.Text)
                .lblcustomerid.Text = customerid.ToString
                .TopMost = True
                .StartPosition = FormStartPosition.CenterScreen
                .ShowDialog()
                .txtCash.Focus()
            End With
            ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
        End If
    End Sub

    Function CheckDuplicateExists_Item(ByVal val As String, ByRef DataGridV As DataGridView, ByVal ColName As String)
        Dim DuplicateExists_count As Integer = 0
        For i As Integer = 0 To DataGridV.Rows.Count - 1
            If Not i >= DataGridV.Rows.Count Then
                If val = DataGridV.Rows(i).Cells(ColName).Value Then
                    DuplicateExists_count = DataGridV.Rows(i).Cells("colqtyBought").Value
                    DataGridV.Rows.RemoveAt(i)
                    Exit For
                End If
            End If
        Next
        Return DuplicateExists_count
    End Function

    Private Sub btnCreditReport_Click(sender As Object, e As EventArgs) Handles btnCreditReport.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.view_credit_running_totals) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        With frmCreditRunningTotals
            .StartPosition = FormStartPosition.CenterScreen
            .Show()
        End With
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.cancel_remove_from_sale) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'clear controls
        dgvSales.Rows.Clear()
        txtBarcode.Clear()
        txttotalamount.Text = "0.00"
        txtDiscount.Text = "0.00"
        txtamountTOpay.Text = "0.00"
        txtchange.Clear()
    End Sub

    Private Sub dgvSales_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvSales.CurrentCellDirtyStateChanged
        If dgvSales.IsCurrentCellDirty Then
            dgvSales.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Function Check_If_Item_Is_VAT_Rated(ByVal Barcode As String) As Integer
        Dim datareader As MySqlDataReader

        Dim strsql As String = "SELECT p.vatrated FROM products p WHERE" _
                               & " p.deleted=0 AND p.Barcode='" & Barcode & "'"

        Dim Rating As Integer = 0
        Try
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                Rating = Integer.Parse(datareader(0).ToString)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
        End Try
        Return Rating
    End Function

    Private Function get_VAT_Rate() As Double
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

    Private Sub btnexit_Click(sender As Object, e As EventArgs) Handles btnexit.Click
        'If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Sales") = MsgBoxResult.Yes) Then
        Me.Close()
        'End If
    End Sub

    Private Sub dgvSales_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSales.CellContentClick

    End Sub

    Private Sub txtBarcode_TextChanged(sender As Object, e As EventArgs) Handles txtBarcode.TextChanged

    End Sub
End Class