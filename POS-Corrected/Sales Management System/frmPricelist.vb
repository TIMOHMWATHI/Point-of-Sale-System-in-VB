Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Text.RegularExpressions
Public Class frmPricelist
    Dim p As New ClassPriceSettings

    'variables declaration
    Dim productid As String, sellingprice As Double, dateset As String, strsql As String

    Dim Description As String, Retail As Double, Wholesale As Double, _
         BP As Double, MinimumSP As Double, todaydate As Date, entryid As Integer, category As String

    Dim checkstate As Boolean


    Private Sub frmPricelist_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''LOAD AUTOSEARCH
        Loadproducts(txtItemSearch)
        'load Pricelist data from database
        LoadPriceListToGrid()
        '############
        lblRetailRate.Text = Double.Parse(get_RetailRate())
        lblWholesaleRate.Text = Double.Parse(get_WholesaleRate())
        'Hide controls
        lblBP.Visible = False
        txtBP.Visible = False
    End Sub

    'AUTO SEARCH
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Dim itemname As String
        Dim Barcode As String
        If txtSearch.Text.Trim.Length > 0 Then
            itemname = txtSearch.Text.Trim
            Barcode = txtSearch.Text.Trim
            LoadPriceListAutosearch(dgvpricelist)
        Else
            LoadPriceListToGrid()
        End If
    End Sub

    'Loads AND RELOAD database table FROM Datagridview
    Sub LoadPriceListToGrid()

        'clear controls and grid
        txtSearch.Clear()
        dgvpricelist.Rows.Clear()

        Dim strsql As String = "SELECT q2.b2,q1.Description,q1.categoryname," _
                                & " IFNULL(q2.buyingprice,0) AS BP," _
                                & " IFNULL(q2.retailprice,0) AS Retail," _
                                & " IFNULL(q2.wholesaleprice,0) AS Wholesale," _
                                & " IFNULL(q2.minprice,0) AS MinSP," _
                                & " q2.Setdate" _
                                & " FROM" _
                                & " (SELECT p.Barcode AS b1,p.Description," _
                                & " pc.categoryname FROM products p" _
                                & " INNER JOIN product_category pc ON " _
                                & " pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                                & " Left Join" _
                                & " (SELECT pl.productid AS b2,s.buyingprice,pl.retailprice," _
                                & " pl.wholesaleprice,pl.minprice," _
                                & " DATE_FORMAT(pl.dateset, '%Y %M %d') AS Setdate FROM pricelist pl" _
                                & " INNER JOIN stock s ON s.itemcode=pl.productid WHERE pl.active!=0) AS q2 " _
                                & " ON q2.b2=q1.b1 GROUP BY q2.b2 ORDER BY q1.Description,q1.categoryname ASC"
        'Try
        Dim datareader As MySqlDataReader
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        datareader = cmd.ExecuteReader
        While datareader.Read
            productid = datareader(0).ToString
            Description = datareader(1).ToString
            category = datareader(2).ToString
            BP = Double.Parse(datareader(3).ToString)
            Retail = Double.Parse(datareader(4).ToString)
            Wholesale = Double.Parse(datareader(5).ToString)
            MinimumSP = Double.Parse(datareader(6).ToString)
            dateset = datareader(7).ToString
            'add rows to grid
            dgvpricelist.Rows.Add(checkstate, productid, Description, category, BP, Retail, Wholesale, MinimumSP, 0, 0, 0, 0, dateset)
        End While
        datareader.Dispose()
        closeconn()
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message)
        'End Try
    End Sub

    Private Sub txtItemSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtItemSearch.KeyDown
        Dim itemname As String

        'input validation on txtItemSearch
        If (txtItemSearch.Text = String.Empty) Then
            txtItemSearch.Focus()
            Exit Sub
        Else
            itemname = txtItemSearch.Text.Trim
        End If
        '#################################################################################################
        'AUTO SEARCH FARMER NAMES
        Dim datareader As MySqlDataReader
        If e.KeyCode = Keys.Enter Then    'work if enter button is clicked
            Try
                Dim strsql = "SELECT Description,Barcode FROM products WHERE deleted=0" _
                             & " AND (Description ='" & itemname & "')"

                Dim cmd As New MySqlCommand(strsql, Conn)
                cmd.CommandType = CommandType.Text
                datareader = cmd.ExecuteReader
                While datareader.Read
                    txtItemSearch.Text = datareader(0).ToString
                    lblbarcode.Text = datareader(1).ToString
                End While
                datareader.Dispose()
                closeconn()
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            End Try
        End If
    End Sub

    'Loads Autosearch products
    Sub LoadPriceListAutosearch(ByVal dgv As DataGridView)

        dgv.Rows.Clear()

        Dim strsql As String = "SELECT q2.b2,q1.Description,q1.categoryname," _
                                & " IFNULL(q2.buyingprice,0) AS BP," _
                                & " IFNULL(q2.retailprice,0) AS Retail," _
                                & " IFNULL(q2.wholesaleprice,0) AS Wholesale," _
                                & " IFNULL(q2.minprice,0) AS MinSP," _
                                & " q2.Setdate" _
                                & " FROM" _
                                & " (SELECT p.Barcode AS b1,p.Description," _
                                & " pc.categoryname FROM products p" _
                                & " INNER JOIN product_category pc ON " _
                                & " pc.id=p.categoryno WHERE p.deleted!=1) AS q1" _
                                & " Left Join" _
                                & " (SELECT pl.productid AS b2,s.buyingprice,pl.retailprice," _
                                & " pl.wholesaleprice,pl.minprice," _
                                & " DATE_FORMAT(pl.dateset, '%Y %M %d') AS Setdate FROM pricelist pl" _
                                & " INNER JOIN stock s ON s.itemcode=pl.productid WHERE pl.active!=0) AS q2 " _
                                & " ON q2.b2=q1.b1" _
                                & " WHERE  q1.Description LIKE '" & "%" & txtSearch.Text & "%" & "' OR " _
                                & " q1.categoryname LIKE '" & "%" & txtSearch.Text & "%" & "' " _
                                & " GROUP BY q2.b2 ORDER BY q1.Description,q1.categoryname ASC"

        Try

            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                productid = datareader(0).ToString
                Description = datareader(1).ToString
                category = datareader(2).ToString
                BP = Double.Parse(datareader(3).ToString)
                Retail = Double.Parse(datareader(4).ToString)
                Wholesale = Double.Parse(datareader(5).ToString)
                MinimumSP = Double.Parse(datareader(6).ToString)
                dateset = datareader(7).ToString
                'add rows to grid
                dgvpricelist.Rows.Add(checkstate, productid, Description, category, BP, Retail, Wholesale, MinimumSP, 0, 0, 0, 0, dateset)
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'input validation on pricelist cboitem
        If (txtItemSearch.Text.Trim = String.Empty) Then
            MessageBox.Show("Select an item from the list of products", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            productid = lblbarcode.Text
        End If

        'input validation on item price
        If txtRetailPrice.Text.Trim = String.Empty Then
            MessageBox.Show("Retail price missing", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Retail = Double.Parse(txtRetailPrice.Text.Trim)
        End If

        'input validation on item price
        If txtWholesalePrice.Text.Trim = String.Empty Then
            MessageBox.Show("Wholesale price missing", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            Wholesale = Double.Parse(txtWholesalePrice.Text.Trim)
        End If

        'BP validation
        If txtBP.Text.Trim = String.Empty Then
            BP = 0
        Else
            BP = Double.Parse(txtBP.Text.Trim)
        End If

        'date input validation
        todaydate = dtptodaysdate.Value.ToLongDateString

        Try
            If btnSave.Text = "Save" Then

                'insert data into pricelist database
                p.insertPricelist(productid, 0, Retail, Wholesale, 0, todaydate, 1)

                MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)

                'reload data
                LoadPriceListToGrid()

                'clear content
                clearPricelist()
            Else
                'update data in pricelist database
                p.insertPricelist(productid, BP, Retail, Wholesale, 0, todaydate, 2)

                MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
            'reload data
            LoadPriceListToGrid()
            'clear input areas
            clearPricelist()
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then closeconn()
            MsgBox(ex.Message & "An error occured when trying to save data. If error persists, inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        'LOAD DATA
        LoadPriceListToGrid()
        txtSearch.Clear()
        txtItemSearch.Clear()
    End Sub

    'clear input area
    Sub clearPricelist()
        lblbarcode.Text = 0
        txtItemSearch.Clear()
        txtRetailPrice.Clear()
        txtWholesalePrice.Clear()
        txtBP.Clear()
        dtptodaysdate.Value = Today
        btnSave.Text = "Save"
        'Hide controls
        lblBP.Visible = False
        txtBP.Visible = False
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.adjust_all_item_prices) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&

        Dim RetailRate As Double = 0, WholesaleRate As Double = 0, _
           RetailPrice As Double = 0, WholesalePrice As Double = 0, _
           BuyingPrice As Double = 0, Barcode As String = "", _
           strsql As String

        RetailRate = Double.Parse(lblRetailRate.Text)
        WholesaleRate = Double.Parse(lblWholesaleRate.Text)

        'validate check state

        ''get retail and wholesale prices
        For i = 0 To dgvpricelist.RowCount - 1
            'progress_bar variables
            Dim totalRecords As Double = 0
            totalRecords = dgvpricelist.RowCount - 1
            PB_Progress.Maximum = totalRecords

            Dim checkstate As Boolean = dgvpricelist.CurrentRow.Cells(0).Value
            RetailPrice = Double.Parse(dgvpricelist.Rows(i).Cells(5).Value.ToString)
            WholesalePrice = Double.Parse(dgvpricelist.Rows(i).Cells(6).Value.ToString)
            Barcode = dgvpricelist.Rows(i).Cells(1).Value.ToString
            BuyingPrice = Double.Parse(dgvpricelist.Rows(i).Cells(4).Value.ToString)

            'insert data into pricelist database

            Dim dtaName As New MySqlClient.MySqlDataAdapter
            dtaName.UpdateCommand = New MySqlClient.MySqlCommand
            With dtaName.UpdateCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text

                If checkstate = False Then
                    MessageBox.Show("Select items to update prices", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                Else
                    strsql = "UPDATE stock s SET s.retail=ROUND(((" & RetailRate & "/100) * " & BuyingPrice & ") + " & BuyingPrice & ",0)," _
                             & " s.wholesale=ROUND(((" & WholesaleRate & "/100) * " & BuyingPrice & ") + " & BuyingPrice & ",0) " _
                             & " WHERE s.buyingprice!=0 AND s.itemcode='" & Barcode & "'"

                    .CommandText = strsql
                    .ExecuteNonQuery()

                    strsql = "UPDATE pricelist SET retailprice=ROUND(((" & RetailRate & "/100) * " & BuyingPrice & ") + " & BuyingPrice & ",0)," _
                             & " wholesaleprice=ROUND(((" & WholesaleRate & "/100) * " & BuyingPrice & ") + " & BuyingPrice & ",0)," _
                             & " dateset=NOW(),active=1 WHERE productid='" & Barcode & "' AND active!=0 "

                    .CommandText = strsql
                    .ExecuteNonQuery()

                End If
            End With
            dtaName.Dispose()
            closeconn()

            PB_Progress.Value = i

        Next

        PB_Progress.Value = 0

        MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'reload data
        LoadPriceListToGrid()

        'clear content
        clearPricelist()

    End Sub

    Private Function get_RetailRate() As Double

        Dim datareader As MySqlDataReader
        Dim RetailRate As Double = 0

        Dim strsql As String = "SELECT retailpercent FROM pricelistsettings WHERE pricestatus=1"
        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                If (datareader(0).ToString) = String.Empty Then
                    RetailRate = 0
                Else
                    RetailRate = datareader(0).ToString
                End If
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return RetailRate
    End Function

    Private Function get_WholesaleRate() As Double

        Dim datareader As MySqlDataReader
        Dim WholesaleRate As Double = 0

        Dim strsql As String = "SELECT wholesalepercent FROM pricelistsettings WHERE pricestatus=1"
        Try

            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                If (datareader(0).ToString) = String.Empty Then
                    WholesaleRate = 0
                Else
                    WholesaleRate = datareader(0).ToString
                End If
            End While
            datareader.Dispose()
            closeconn()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return WholesaleRate
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearPricelist()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtRetailPrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetailPrice.KeyPress
        'input validation code for numeric and decimal values only
        Dim c As Char
        c = e.KeyChar
        If Not (Char.IsDigit(c) Or c = "." Or
        Char.IsControl(c)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtWholesalePrice_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWholesalePrice.KeyPress
        'input validation code for numeric and decimal values only
        Dim c As Char
        c = e.KeyChar
        If Not (Char.IsDigit(c) Or c = "." Or
        Char.IsControl(c)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtBP_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtBP.KeyPress
        'input validation code for numeric and decimal values only
        Dim c As Char
        c = e.KeyChar
        If Not (Char.IsDigit(c) Or c = "." Or
        Char.IsControl(c)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.adjust_all_item_prices) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        Dim Barcode As String, NewRetail As Double = 0, _
            NewWholesale As Double = 0, NewBP As Double = 0, _
            SetMinPrice As Double = 0

        Try
            If (MsgBox("Are you sure you want to reconcile prices?", MsgBoxStyle.YesNo, "Strawberry POS") = MsgBoxResult.Yes) Then

                'If dgvpricelist.RowCount > 0 Then

                For Each Item_Row As DataGridViewRow In dgvpricelist.Rows

                    If Item_Row.Cells("ColBarcode").Value.ToString = Nothing Then Continue For
                    If Item_Row.Cells("ColNewBP").Value.ToString = Nothing Then Exit Sub
                    If Item_Row.Cells("ColNewRetail").Value.ToString = Nothing Then Continue For
                    If Item_Row.Cells("ColNewWholeSale").Value.ToString = Nothing Then Continue For

                    Barcode = Item_Row.Cells("ColBarcode").Value.ToString
                    NewRetail = Double.Parse(Item_Row.Cells("ColNewRetail").Value.ToString)
                    NewWholesale = Double.Parse(Item_Row.Cells("ColNewWholeSale").Value.ToString)
                    NewBP = Double.Parse(Item_Row.Cells("ColNewBP").Value.ToString)
                    SetMinPrice = Double.Parse(Item_Row.Cells("ColSetMinPrice").Value.ToString)

                    'MsgBox("Buying Price :" & NewBP & " : " & "Retail Price:" & NewRetail)

                    If (NewRetail > NewBP) Then         'And (NewRetail > 0)

                        'update data in pricelist database
                        p.insertPricelist(Barcode, NewBP, NewRetail, NewWholesale, SetMinPrice, Today(), 2)
                        'MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)

                        'Else

                        '    MessageBox.Show("Invalid prices", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        '    Exit Sub
                    End If
                Next
                MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)

                'clear input areas
                dgvpricelist.Rows.Clear()

                'reload data
                LoadPriceListToGrid()

                'End If

            End If
            Exit Sub
        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then closeconn()
            MsgBox(ex.Message & "An error occured when trying to save data. If error persists, inform your administrator for further action", MsgBoxStyle.Critical)
            Exit Sub
        End Try
    End Sub

    Private Sub dgvpricelist_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvpricelist.CurrentCellDirtyStateChanged
        If dgvpricelist.IsCurrentCellDirty Then
            dgvpricelist.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Sub dgvpricelist_DoubleClick(sender As Object, e As EventArgs) Handles dgvpricelist.DoubleClick
        'update double click event
        txtItemSearch.Text = dgvpricelist.CurrentRow.Cells(2).Value.ToString
        txtRetailPrice.Text = dgvpricelist.CurrentRow.Cells(5).Value.ToString
        txtWholesalePrice.Text = dgvpricelist.CurrentRow.Cells(6).Value.ToString
        txtBP.Text = dgvpricelist.CurrentRow.Cells(4).Value.ToString
        dtptodaysdate.Value = dgvpricelist.CurrentRow.Cells(10).Value.ToString
        lblbarcode.Text = dgvpricelist.CurrentRow.Cells(1).Value.ToString
        'Hide controls
        lblBP.Visible = True
        txtBP.Visible = True
        btnSave.Text = "Update"
    End Sub
End Class