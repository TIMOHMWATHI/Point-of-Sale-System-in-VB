Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmCarwashSales
    Dim sv As New ClassManageServices
    Dim employee As String, vehiclereg As String, _
        ownercontacts As String, customerremarks As String

    Dim service As String = "", qty As Integer = 0, _
        unitprice As Double = 0, totalprice As Double = 0, _
        SalesNo As Long

    Private Sub frmCarwashSales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load services to grid
        sv.LoadServices(dgvSalesServices)

        'load employee combo
        Dim strsql As String = "SELECT fullnames,entryid FROM employees WHERE deleted=0"
        loadcombo(cboEmployeeName, strsql, "fullnames", "entryid")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        'employee validation
        If cboEmployeeName.SelectedIndex = -1 Then
            MessageBox.Show("Select employee name", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            cboEmployeeName.Focus()
            Exit Sub
        Else
            employee = cboEmployeeName.SelectedValue
        End If

        'vehicle reg validation
        If txtRegNo.Text.Trim = String.Empty Then
            MessageBox.Show("Invalid vehicle reg number input", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtRegNo.Focus()
            Exit Sub
        Else
            vehiclereg = txtRegNo.Text.Trim.ToUpper
        End If


        'vehicle reg validation
        If txtOwnerContacts.Text.Trim = String.Empty Then
            MessageBox.Show("Invalid customer phone number input", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
            txtOwnerContacts.Focus()
            Exit Sub
        Else
            ownercontacts = txtOwnerContacts.Text.Trim
        End If

        'customer remarks validation
        customerremarks = txtCustomerRemarks.Text.ToUpper

        'save in Carwashmaster
        sv.InsertCarwashSales(employee, vehiclereg, ownercontacts, customerremarks, Double.Parse(lblPriceTotals.Text), Now(), userid, 0, "", 0, 0, 0, 1)
        'save in CarwashDetails
        SalesNo = sv.getNextSalesNo()
        For i = 0 To dgvSales.RowCount - 2
            service = dgvSales.Rows(i).Cells(0).Value.ToString
            qty = Integer.Parse(dgvSales.Rows(i).Cells(1).Value.ToString)
            unitprice = Double.Parse(dgvSales.Rows(i).Cells(2).Value.ToString)
            totalprice = Double.Parse(dgvSales.Rows(i).Cells(3).Value.ToString)
            'MsgBox(totalprice)
            sv.InsertCarwashSales("", "", "", "", 0, Now(), 0, SalesNo, service, qty, unitprice, totalprice, 1)
        Next
        MessageBox.Show("Transaction completed successfully", "Strawberry System", MessageBoxButtons.OK, MessageBoxIcon.Information)
        rub()

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        rub()
    End Sub

    Sub rub()
        cboEmployeeName.SelectedIndex = -1
        txtRegNo.Clear()
        txtOwnerContacts.Clear()
        txtCustomerRemarks.Clear()
        dgvSales.Rows.Clear()
        lblPriceTotals.Text = 0
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dgvSalesServices_DoubleClick(sender As Object, e As EventArgs) Handles dgvSalesServices.DoubleClick
        Dim ServiceName As String, Price As Double = 0
        ServiceName = dgvSalesServices.CurrentRow.Cells(1).Value.ToString
        Price = dgvSalesServices.CurrentRow.Cells(2).Value.ToString
        'adds new row in datagrid sales after doubleclick of items grid
        dgvSales.Rows.Add(ServiceName, 0, Price, 0)
    End Sub

    Private Sub DGVcellvaluechange(ByVal DataGridView1 As DataGridView, _
                                   ByVal lbl As Label)
        'solves calculations on dgvproducts and computes the total price of products on order

        Try
            Dim unitprice As Double, qty As Integer, totalprice As Double
            Dim PriceTotals As Double = 0, i As Integer
            unitprice = Double.Parse(DataGridView1.CurrentRow.Cells(2).Value.ToString)
            qty = Integer.Parse(DataGridView1.CurrentRow.Cells(1).Value.ToString)
            totalprice = qty * unitprice
            DataGridView1.CurrentRow.Cells(3).Value = totalprice

            'compute total
            For i = 0 To DataGridView1.RowCount - 1
                PriceTotals += Double.Parse(DataGridView1.Rows(i).Cells(3).Value.ToString)
            Next i
            lblPriceTotals.Text = PriceTotals.ToString
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvSales_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvSales.CellValueChanged
        DGVcellvaluechange(dgvSales, lblPriceTotals)
    End Sub

    Private Sub txtOwnerContacts_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtOwnerContacts.KeyPress
        'only numbers input validation
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then
            Else
                e.Handled = True
            End If
        End If
    End Sub
End Class