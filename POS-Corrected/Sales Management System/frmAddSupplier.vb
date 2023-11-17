Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmAddSupplier
    Dim strsql As String

    Dim suppliers As New ClassSuppliers

    'declaring variables
    Dim fullname As String, pinid As String, address As String, _
        town As String, physicallocation As String
    Dim phone As String, email As String, contactperson As String, _
        relationtype As String, customerno As Integer, _
         supplier_id As Integer
    Dim deleted As Byte, HasRentals As Byte

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'input validation on txtfullname
        Try

            If txtFullname.Text.Trim = String.Empty Then
                MessageBox.Show("input your business or customer name", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtFullname.Focus()
                Exit Sub
            ElseIf IsNumeric(txtFullname.Text) Then
                MessageBox.Show("Fullname input error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                fullname = txtFullname.Text.ToUpper
            End If

            'input validation on BUSINESS PIN number
            If txtbusinesspin.Text.Trim = String.Empty Then
                MessageBox.Show("input your business pin number", "Recommended for suppliers", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtbusinesspin.Focus()
                Exit Sub
            ElseIf IsNumeric(txtFullname.Text) Then
                MessageBox.Show("Business pin input error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                pinid = txtbusinesspin.Text.ToUpper
            End If

            ''input validation on address
            'If txtaddress.Text.Trim = String.Empty Then
            '    MessageBox.Show("input your adress", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    txtaddress.Focus()
            '    Exit Sub
            'Else
            address = txtaddress.Text.ToUpper
            'End If

            ''input validation on town
            'If txttown.Text.Trim = String.Empty Then
            '    MessageBox.Show("input your town name", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    txttown.Focus()
            '    Exit Sub
            'ElseIf IsNumeric(txttown.Text) Then
            '    MessageBox.Show("town name input error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'Else
            town = txttown.Text.ToUpper
            'End If

            'input validation on physical location
            physicallocation = txtPLocation.Text.ToUpper


            'input validation on phone input
            If txtPhone.Text.Trim = String.Empty Then
                MessageBox.Show("input phone number", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                phone = txtPhone.Text.Trim
            End If

            ''input validation at txtEmail
            'If IsNumeric(txtEmail.Text) Then
            '    MessageBox.Show("Email input error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'Else
            email = txtEmail.Text.ToLower
            'End If

            'contact person input validation
            If txtcontactperson.Text.Trim = String.Empty Then
                MessageBox.Show("input suppliers contact person name", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
                txtcontactperson.Focus()
                Exit Sub
            ElseIf IsNumeric(txtcontactperson.Text) Then
                MessageBox.Show("contact person name input error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                contactperson = txtcontactperson.Text.ToUpper
            End If

            'relationtype input validation
            If cbosupplier.SelectedIndex = -1 Then
                MessageBox.Show("select a relationtype", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            Else
                If cbosupplier.Text = "Supplier" Then
                    relationtype = "S"
                Else
                    relationtype = "C"
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

        'customertype = Label10.Text


        'INPUT DATA INTO DATABASE
        If btnSave.Text = "Save" Then

            suppliers.insertsuppliers(fullname, pinid, address, town, physicallocation, phone, email, contactperson, relationtype)

            'reloa data
            frmSuppliers.LoadSuppliers()

        Else
            suppliers.updatesuppliers(fullname, pinid, address, town, physicallocation, phone, email, contactperson, relationtype, supplier_id)

            'reloa data
            frmSuppliers.LoadSuppliers()

        End If

        closeconn()

        'clear data input area
        clearcontent()
        'Close form
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        clearcontent()
    End Sub

    'clears user's input
    Sub clearcontent()
        txtFullname.Clear()
        txtbusinesspin.Clear()
        txtaddress.Clear()
        txttown.Clear()
        txtPLocation.Clear()
        txtPhone.Clear()
        txtEmail.Clear()
        txtcontactperson.Clear()
        cbosupplier.SelectedIndex = -1
        lblid.Text = "00"
        btnSave.Text = "Save"
    End Sub

   Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

End Class