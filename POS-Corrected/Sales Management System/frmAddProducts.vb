Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmAddProducts
    'declaring database objects
    Dim Barcode As String, Description As String, _
        Itemsperpack As Integer, categoryno As Integer, _
        retail As Double, wholesale As Double, _
        hasSerial As Byte, modelNo As String, _
        VAT_Rated As Integer, deleted As Byte

    Public reorderlevel As Integer

    'imports code from the class level
    Dim items As New ClassProducts

    Private Sub frmAddProducts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''cbo re-order level loop
        'cboreorderlevel.Items.Clear()
        cbocategory.SelectedValue = 1
        cboreorderlevel.SelectedValue = 1
        For i = 0 To 30 Step 5
            'If cboreorderlevel.Items.Add(i) = 40 Then Exit Sub
            cboreorderlevel.Items.Add(i)
        Next

        'populate category combobox
        Dim strsql As String = "SELECT pc.id,pc.categoryname FROM product_category pc WHERE pc.deleted=0 ORDER BY pc.categoryname ASC"
        loadcombo(cbocategory, strsql, "categoryname", "id")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If txtbarcode.Text.Trim = String.Empty Then
            MessageBox.Show("barcode missing", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtbarcode.Focus()
            Exit Sub
        Else
            Barcode = txtbarcode.Text.ToUpper
        End If

        'input validation on txtdescription
        If txtDescription.Text.Trim = String.Empty Then
            MessageBox.Show("Description missing", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtDescription.Focus()
            Exit Sub
        Else
            Description = txtDescription.Text.ToUpper
        End If

        'input validation on txtItemsPerPack
        If txtItemsPerPack.Text.Trim = String.Empty Then
            MessageBox.Show("Number of items missing", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtItemsPerPack.Focus()
            Exit Sub
        Else
            Itemsperpack = txtItemsPerPack.Text.Trim
        End If

        'input validation on cbocategory
        If cbocategory.SelectedIndex = -1 Then
            MessageBox.Show(" Select item category", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cbocategory.Focus()
            Exit Sub
        Else
            categoryno = cbocategory.SelectedValue
        End If

        'input validation on cboreorderlevel
        If cboreorderlevel.SelectedIndex = -1 Then
            MessageBox.Show(" Select item Reorder-level", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            cboreorderlevel.Focus()
            Exit Sub
        Else
            reorderlevel = Integer.Parse(cboreorderlevel.Text)
        End If

        'retail validation
        If txtRetail.Text = String.Empty Then
            retail = 0
        Else
            retail = Double.Parse(txtRetail.Text)
        End If

        'retail validation
        If txtWholesale.Text = String.Empty Then
            wholesale = 0
        Else
            wholesale = Double.Parse(txtWholesale.Text)
        End If

        modelNo = txtmodelNo.Text.Trim.ToUpper

        'VAT Rated Validation
        If (rdbYes.Checked = False) And (rdbNo.Checked = False) Then
            MessageBox.Show("Select if item is V.A.T rated", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            If rdbYes.Checked = True Then
                VAT_Rated = 1
            Else
                VAT_Rated = 0
            End If
        End If
        'INPUT DATA INTO DATABASE
        If btnSave.Text = "Save" Then
            items.insertItems(Barcode, Description, Itemsperpack, categoryno, reorderlevel, retail, wholesale, _
                              modelNo, hasSerial, VAT_Rated, 1)

            MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'reload data
            ''frmproducts.Loadproductstogrid()
            clearcontrols()
            txtbarcode.Focus()
        Else
            items.insertItems(Barcode, Description, Itemsperpack, categoryno, reorderlevel, retail, wholesale, _
                             modelNo, hasSerial, VAT_Rated, 2)
            MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)

            'reload data
            ''frmproducts.Loadproductstogrid()
            clearcontrols()
            Me.Hide()
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearcontrols()
    End Sub

    'CLEARS CONTROLS AFTER THEY HAVE BEEN SUCCESSFULLY SAVED
    Sub clearcontrols()
        txtbarcode.Clear()
        txtDescription.Clear()
        txtItemsPerPack.Text = 1
        txtmodelNo.Clear()
        'cbocategory.SelectedIndex = -1
        txtRetail.Text = 0
        txtWholesale.Text = 0
        lblRetail.Enabled = True
        lblWholesale.Enabled = True
        txtRetail.Enabled = True
        txtWholesale.Enabled = True
        cboreorderlevel.Text = 5
        btnSave.Text = "Save"
        txtbarcode.ReadOnly = False
    End Sub

    Private Sub txtbarcode_LostFocus(sender As Object, e As EventArgs) Handles txtbarcode.LostFocus
        If btnSave.Text = "Update" Then Exit Sub
        Dim itemCode As String, strsql As String
        If txtbarcode.Text.Trim.Length > 0 Then
            itemCode = txtbarcode.Text.Trim
            strsql = "select count(barcode) from products where barcode= '" & itemCode & "' AND deleted!=1"
            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                If datareader(0) > 0 Then
                    MessageBox.Show("Barcode already exists", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtbarcode.Clear()
                    txtbarcode.Focus()
                    Exit Sub
                End If
            End While
            datareader.Dispose()
            closeconn()
        End If
    End Sub

    Private Sub txtDescription_LostFocus(sender As Object, e As EventArgs) Handles txtDescription.LostFocus
        Dim itemName As String, strsql As String
        If txtDescription.Text.Trim.Length > 0 Then
            itemName = txtDescription.Text.Trim
            strsql = "SELECT COUNT(Description) FROM products where Description= '" & itemName & "' AND deleted!=1"
            Dim datareader As MySqlDataReader
            Dim cmd As New MySqlCommand(strsql, Conn)
            cmd.CommandType = CommandType.Text
            datareader = cmd.ExecuteReader
            While datareader.Read
                If datareader(0) > 0 Then
                    MessageBox.Show("Description already exists", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtDescription.Clear()
                    txtDescription.Focus()
                    Exit Sub
                End If
                'Exit Sub
            End While
            datareader.Dispose()
            closeconn()
        End If
    End Sub

    Private Sub txtRetail_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtRetail.KeyPress
        'input validation code for numeric and decimal values only
        Dim c As Char
        c = e.KeyChar
        If Not (Char.IsDigit(c) Or c = "." Or
        Char.IsControl(c)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtWholesale_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWholesale.KeyPress
        'input validation code for numeric and decimal values only
        Dim c As Char
        c = e.KeyChar
        If Not (Char.IsDigit(c) Or c = "." Or
        Char.IsControl(c)) Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnAddCategory_Click(sender As Object, e As EventArgs) Handles btnAddCategory.Click
        'input box code to add products category combo
        Dim addcategory As String = ""
        addcategory = InputBox("Add new item category" & vbNewLine & " eg. Cosmetics", "Strawberry System").ToUpper
        If (addcategory.Length < 4) Then
            Exit Sub
        End If
        Dim strsql As String = "INSERT INTO product_category (categoryname) VALUES ('" & addcategory & "')"
        Try
            Dim dtaName As New MySqlClient.MySqlDataAdapter
            dtaName.InsertCommand = New MySqlClient.MySqlCommand
            With dtaName.InsertCommand
                .CommandTimeout = 60
                .Connection = Conn()
                .CommandType = CommandType.Text
                .CommandText = strsql
                .ExecuteNonQuery()
            End With
            MessageBox.Show("Success", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            closeconn()

            'reload data
            frmproducts.Loadcategory()

        Catch ex As Exception
            If Conn.State = ConnectionState.Open Then Conn.Close()
            MessageBox.Show("An error occured while try to save data. If error persists, inform your administrator for more action", "Add color", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
            closeconn()
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub txtbarcode_TextChanged(sender As Object, e As EventArgs) Handles txtbarcode.TextChanged

    End Sub
End Class