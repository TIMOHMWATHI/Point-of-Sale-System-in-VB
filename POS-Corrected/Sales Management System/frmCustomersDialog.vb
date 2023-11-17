Imports MySql.Data
Imports MySql.Data.MySqlClient
Public Class frmCustomersDialog
    Dim strsql As String
    'declaring variables
    Dim fullname As String, pinid As String, address As String, town As String, physicallocation As String
    Dim phone As String, email As String, contactperson As String, relationtype As String, supplier_id As Integer

    'calls the classsuppliers
    Dim suppliers As New ClassSuppliers

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'input validation on txtfullname
        Try

            If txtFullname.Text.Trim = String.Empty Then
                MessageBox.Show("input your business or customer name", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            ElseIf IsNumeric(txtFullname.Text) Then
                MessageBox.Show("Fullname input error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            Else
                fullname = txtFullname.Text.ToUpper
            End If

            pinid = txtbusinesspin.Text.ToUpper
            address = txtaddress.Text.ToUpper
            town = txttown.Text.ToUpper
            physicallocation = txtPLocation.Text.ToUpper

            'input validation on phone input
            If txtPhone.Text.Trim = String.Empty Then
                MessageBox.Show("input phone number", "Recommended", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub

            Else
                phone = txtPhone.Text.ToUpper
            End If

                email = txtEmail.Text.ToLower
                contactperson = txtcontactperson.Text.ToUpper

                If cbosupplier.Text = "Supplier" Then
                    relationtype = "S"
                Else
                    relationtype = "C"
            End If
            'INPUT DATA INTO DATABASE
            suppliers.insertsuppliers(fullname, pinid, address, town, physicallocation, phone, email, contactperson, relationtype)
            customerfullname = txtFullname.Text
            customerphone = txtPhone.Text
            retrieveCustomerdata(customerfullname)
            'clear input areas
            clearcontent()
           
            'close form
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
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
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        'If (MsgBox("Are you sure you want to close this page?", MsgBoxStyle.YesNo, "Customers") = MsgBoxResult.Yes) Then
        Me.Close()
        ' End If
    End Sub

    Private Sub frmCustomersDialog_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        With frmCreditSales
            .txtCustomerName.Text = customerfullname
            .txtPhoneNumber.Text = customerphone
        End With
    End Sub

    Sub retrieveCustomerdata(ByVal fullname As String)
   
        Dim strsql As String = "SELECT supplier_id FROM suppliers WHERE fullname ='" & fullname & "'" _
                                & " AND relationtype ='C' order by supplier_id desc limit 1"
        'retreave item using its barcode
        Dim datareader As MySqlDataReader
        Dim cmd As New MySqlCommand(strsql, Conn)
        cmd.CommandType = CommandType.Text
        datareader = cmd.ExecuteReader
        While datareader.Read
            customerno = Integer.Parse(datareader(0).ToString)
        End While
    End Sub
End Class