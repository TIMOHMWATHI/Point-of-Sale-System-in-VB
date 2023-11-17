Imports MySql.Data
Imports MySql.Data.MySqlClient

Public Class frmBankDeposits
    Dim bank As New ClassBanks

    Dim BankName As String, Amntpaid As Double, Paymode As String, _
        Memo As String, paydate As String, registeredby As Integer


    Private Sub frmBankStatements_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load combo
        Dim strsql As String = "SELECT bankname,entryid FROM banks WHERE deleted!=1 ORDER BY bankname ASC"
        loadcombo(cboBank, strsql, "bankname", "entryid")
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If cboBank.SelectedIndex = -1 Then
            MessageBox.Show("Select bank...compulsory", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            BankName = cboBank.SelectedValue
        End If

        'amount validation
        If txtAmount.Text = String.Empty Then
            MessageBox.Show("Invalid amount input", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtAmount.Focus()
            Exit Sub
        Else
            Amntpaid = Double.Parse(txtAmount.Text.Trim)
        End If

        'paymode validation
        If cboPaymode.SelectedIndex = -1 Then
            MessageBox.Show("Select paymode to continue", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        Else
            Paymode = cboPaymode.Text.ToUpper
        End If

        'memo validation
        memo = txtMemo.Text.Trim.ToUpper

        'save to db
        bank.manageBankDeposits(Now(), BankName, Amntpaid, Paymode, Memo, userid, 1)


        ''INSERT INTO CASH LEDGER
        'CashLedger("Deposit", "Bank Deposit", 1, Amntpaid, Now(), userid, "Payment To :" & cboBank.Text, "-", "Paymode via:" & Paymode, 1)


        MessageBox.Show("Success", "Strawberry POS", MessageBoxButtons.OK, MessageBoxIcon.Information)
        'clear controls
        rub()
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        'input validation code for numeric and decimal values only
        Dim c As Char
        c = e.KeyChar
        If Not (Char.IsDigit(c) Or c = "." Or
        Char.IsControl(c)) Then
            e.Handled = True
        End If
    End Sub


    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        rub()
    End Sub

    'rub inputs
    Sub rub()
        cboBank.SelectedIndex = -1
        txtAmount.Clear()
        cboPaymode.SelectedIndex = -1
        txtMemo.Clear()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class