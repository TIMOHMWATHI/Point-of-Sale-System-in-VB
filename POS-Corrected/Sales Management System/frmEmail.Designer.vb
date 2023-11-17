<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmail
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmail))
        Me.btnSend = New System.Windows.Forms.Button()
        Me.txtMessage = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtSubject = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lstAttachment = New System.Windows.Forms.ListBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTo = New System.Windows.Forms.TextBox()
        Me.txtFromDisplayName = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.OFD = New System.Windows.Forms.OpenFileDialog()
        Me.txtFrom = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'btnSend
        '
        Me.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnSend.Location = New System.Drawing.Point(389, 380)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(104, 32)
        Me.btnSend.TabIndex = 36
        Me.btnSend.Text = "Send"
        '
        'txtMessage
        '
        Me.txtMessage.Location = New System.Drawing.Point(133, 276)
        Me.txtMessage.Multiline = True
        Me.txtMessage.Name = "txtMessage"
        Me.txtMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtMessage.Size = New System.Drawing.Size(240, 144)
        Me.txtMessage.TabIndex = 35
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(13, 280)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(104, 16)
        Me.Label10.TabIndex = 34
        Me.Label10.Text = "Message"
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(133, 244)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(232, 20)
        Me.txtSubject.TabIndex = 33
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(13, 244)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(104, 16)
        Me.Label9.TabIndex = 32
        Me.Label9.Text = "Subject"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(13, 148)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(72, 20)
        Me.Label8.TabIndex = 31
        Me.Label8.Text = "Attachments"
        '
        'btnRemove
        '
        Me.btnRemove.Enabled = False
        Me.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnRemove.Location = New System.Drawing.Point(381, 196)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(120, 32)
        Me.btnRemove.TabIndex = 30
        Me.btnRemove.Text = "Remove attachment"
        '
        'btnAdd
        '
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.btnAdd.Location = New System.Drawing.Point(381, 148)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(120, 32)
        Me.btnAdd.TabIndex = 29
        Me.btnAdd.Text = "Add attachment"
        '
        'lstAttachment
        '
        Me.lstAttachment.Location = New System.Drawing.Point(133, 148)
        Me.lstAttachment.Name = "lstAttachment"
        Me.lstAttachment.Size = New System.Drawing.Size(232, 82)
        Me.lstAttachment.TabIndex = 28
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(13, 84)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(72, 16)
        Me.Label7.TabIndex = 26
        Me.Label7.Text = "To"
        '
        'txtTo
        '
        Me.txtTo.Location = New System.Drawing.Point(133, 84)
        Me.txtTo.Name = "txtTo"
        Me.txtTo.Size = New System.Drawing.Size(232, 20)
        Me.txtTo.TabIndex = 27
        '
        'txtFromDisplayName
        '
        Me.txtFromDisplayName.Location = New System.Drawing.Point(133, 52)
        Me.txtFromDisplayName.Name = "txtFromDisplayName"
        Me.txtFromDisplayName.Size = New System.Drawing.Size(232, 20)
        Me.txtFromDisplayName.TabIndex = 25
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(13, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(104, 20)
        Me.Label3.TabIndex = 24
        Me.Label3.Text = "From Display name"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(13, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 21
        Me.Label2.Text = "From"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(21, 52)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(104, 32)
        Me.Label4.TabIndex = 23
        Me.Label4.Text = "From Display name"
        '
        'OFD
        '
        Me.OFD.DefaultExt = "*.*"
        Me.OFD.InitialDirectory = "c:\"
        Me.OFD.Multiselect = True
        '
        'txtFrom
        '
        Me.txtFrom.Location = New System.Drawing.Point(133, 20)
        Me.txtFrom.Name = "txtFrom"
        Me.txtFrom.Size = New System.Drawing.Size(232, 20)
        Me.txtFrom.TabIndex = 22
        '
        'frmEmail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.ClientSize = New System.Drawing.Size(514, 429)
        Me.Controls.Add(Me.btnSend)
        Me.Controls.Add(Me.txtMessage)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtSubject)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnRemove)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.lstAttachment)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtTo)
        Me.Controls.Add(Me.txtFromDisplayName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtFrom)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmEmail"
        Me.Text = "Email"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents txtMessage As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lstAttachment As System.Windows.Forms.ListBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtTo As System.Windows.Forms.TextBox
    Friend WithEvents txtFromDisplayName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents OFD As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtFrom As System.Windows.Forms.TextBox
End Class
