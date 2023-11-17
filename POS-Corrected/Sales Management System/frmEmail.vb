Imports System.Net.Mail
Imports System.Text.RegularExpressions
Public Class frmEmail
    Dim obj As System.Net.Mail.SmtpClient     ' Variable which will send the mail
    Dim Attachment As System.Net.Mail.Attachment    'Variable to store the attachments
    Dim Mailmsg As New MailMessage()    'Variable to create the message to send
    Public Shared count_file As Integer

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        'Show open dialogue box to select the files to attach
        Dim Counter As Integer
        OFD.CheckFileExists = True
        OFD.Title = "Select file(s) to attach"
        OFD.ShowDialog()
        For Counter = 0 To UBound(OFD.FileNames)
            lstAttachment.Items.Add(OFD.FileNames(Counter))
            count_file = count_file + 1
            btnRemove.Enabled = True
            If count_file > 2 Then
                btnAdd.Enabled = False
            End If
        Next
    End Sub

    Private Sub btnRemove_Click(sender As Object, e As EventArgs) Handles btnRemove.Click
        'Remove the attachments
        If lstAttachment.SelectedIndex > -1 Then
            lstAttachment.Items.RemoveAt(lstAttachment.SelectedIndex)
            count_file = count_file - 1

            If count_file = 0 Then
                btnAdd.Enabled = True
                btnRemove.Enabled = False
            End If
        End If
    End Sub


    '        'Set the properties

    '        'obj.SmtpServer = txtSMTPServer.Text
    '        'Multiple recepients can be specified using ; as the delimeter
    '        'nileshschaudhari1410@gmail.com
    '        Mailmsg.To = txtTo.Text
    '        Mailmsg.From = "\" & txtFromDisplayName.Text & "\ <" & txtFrom.Text & ">"

    '        'Specify the body format
    '        'If chkFormat.Checked = True Then
    '        'Mailmsg.BodyFormat = MailFormat.Html   'Send the mail in HTML Format
    '        'Else
    '        Mailmsg.BodyFormat = MailFormat.Text
    '        'End If

    '        'If you want you can add a reply to header 
    '        'Mailmsg.Headers.Add("Reply-To", "Manoj@geinetech.net")
    '        'custom headersare added like this
    '        'Mailmsg.Headers.Add("Manoj", "TestHeader")
    '        Mailmsg.Subject = txtSubject.Text
    '        For Counter = 0 To lstAttachment.Items.Count - 1
    '            Attachment = New MailAttachment(lstAttachment.Items(Counter))
    '            Mailmsg.Attachments.Add(Attachment)
    '        Next

    '        Mailmsg.Body = txtMessage.Text
    '        'obj.Send(Mailmsg)
    '        Try
    '            SmtpMail.Send(Mailmsg)
    '            MessageBox.Show("Email send successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End Try
    '    End Sub

    ' Code for validating email ID
    Function ChkValidEmail(ByVal Value As String, Optional ByVal MaxLength As Integer = 255, Optional ByVal IsRequired As Boolean = True) As Boolean
        If Value Is Nothing OrElse Value.Length = 0 Then
            ' rule out the null string case        
            Return Not IsRequired
        ElseIf Value.Length > MaxLength Then
            ' rule out values that are longer than allowed        
            Return False
        End If
        ' search invalid chars    
        If Not System.Text.RegularExpressions.Regex.IsMatch(Value, _
        "^[-A-Za-z0-9_@.]+$") Then Return False
        ' search the @ char    
        Dim i As Integer = Value.IndexOf("@"c)
        ' there must be at least three chars after the @    
        If i <= 0 Or i >= Value.Length - 3 Then Return False
        ' ensure there is only one @ char    
        If Value.IndexOf("@"c, i + 1) >= 0 Then Return False
        ' check that the domain portion contains at least one dot   
        Dim j As Integer = Value.LastIndexOf("."c)
        ' it can't be before or immediately after the @ char   
        If j < 0 Or j <= i + 1 Then Return False
        ' if we get here the address if validated    
        Return True
    End Function


    Private Sub btnSend_Click(sender As Object, e As EventArgs) Handles btnSend.Click
        'input validation
        If txtFrom.Text = "" Then
            MsgBox("Enter the From email address.", MsgBoxStyle.Information, "Send Email")
            Exit Sub
        End If

        If txtTo.Text = "" Then
            MsgBox("Enter the Recipient email address.", MsgBoxStyle.Information, "Send Email")
            Exit Sub
        End If

        'check internet connection
        Dim o As New clsCheckInternetConnection()
        Dim flag As Boolean
        flag = o.IsConnected()
        If flag = False Then
            MessageBox.Show("Internet connection is not available to your computer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        Dim mail As New MailMessage()
        Dim SmtpServer As New SmtpClient("smtp.gmail.com")
        mail.From = New MailAddress(txtFrom.Text)
        mail.[To].Add(txtTo.Text)
        mail.Subject = txtSubject.Text
        mail.Body = lstAttachment.Text

        Dim attachment As System.Net.Mail.Attachment
        attachment = New System.Net.Mail.Attachment("your attachment file")

        SmtpServer.Port = 587
        SmtpServer.Credentials = New System.Net.NetworkCredential("username", "password")
        SmtpServer.EnableSsl = True
        SmtpServer.Send(mail)
        MessageBox.Show("mail successfully sent")
    End Sub

    'email regular expression
    Private Sub txtFrom_Leave(sender As Object, e As EventArgs) Handles txtFrom.Leave

    End Sub


End Class