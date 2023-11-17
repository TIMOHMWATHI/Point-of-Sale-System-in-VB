Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmVATSettings
    Dim vat As New ClassVATSetting
    Dim VATPercent As Double


    Private Sub frmVATSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Reload data
        vat.LoadToGrid(dgvVAT)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'ASSIGN USER PRIVILEDGES CODE
        If Not Authorized(userid, TaskID.change_vat_rates) Then Exit Sub
        '&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&&
        'validate vat setting
        If (NUDPercent.Value < 1) Then
            MessageBox.Show("Invalid VAT percent value", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        Else
            VATPercent = Double.Parse(NUDPercent.Value)
        End If
        'SAVE DATA TO DB
        vat.Set_VAT_Percent(VATPercent, Today(), userid, 1)
        'Reload data
        vat.LoadToGrid(dgvVAT)
        'clear control
        NUDPercent.Value = 1

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        NUDPercent.Value = 1
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class