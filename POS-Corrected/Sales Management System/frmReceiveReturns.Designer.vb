<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReceiveReturns
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReceiveReturns))
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.lblFileNo = New System.Windows.Forms.Label()
        Me.lblReceiptNo = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblSellingPrice = New System.Windows.Forms.Label()
        Me.txtQtyReturned = New System.Windows.Forms.TextBox()
        Me.lblTotalValue = New System.Windows.Forms.Label()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblqtysold = New System.Windows.Forms.Label()
        Me.lblBarcode = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtNarration = New System.Windows.Forms.TextBox()
        Me.lblVATstatus = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblItemBP = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblDescription
        '
        Me.lblDescription.AutoSize = True
        Me.lblDescription.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription.Location = New System.Drawing.Point(71, 5)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(71, 13)
        Me.lblDescription.TabIndex = 0
        Me.lblDescription.Text = "Description"
        '
        'lblFileNo
        '
        Me.lblFileNo.AutoSize = True
        Me.lblFileNo.Font = New System.Drawing.Font("Wingdings 2", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.lblFileNo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblFileNo.Location = New System.Drawing.Point(112, 34)
        Me.lblFileNo.Name = "lblFileNo"
        Me.lblFileNo.Size = New System.Drawing.Size(13, 11)
        Me.lblFileNo.TabIndex = 1
        Me.lblFileNo.Text = "0"
        '
        'lblReceiptNo
        '
        Me.lblReceiptNo.AutoSize = True
        Me.lblReceiptNo.Font = New System.Drawing.Font("Wingdings 2", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.lblReceiptNo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblReceiptNo.Location = New System.Drawing.Point(24, 34)
        Me.lblReceiptNo.Name = "lblReceiptNo"
        Me.lblReceiptNo.Size = New System.Drawing.Size(13, 11)
        Me.lblReceiptNo.TabIndex = 2
        Me.lblReceiptNo.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 153)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Quantity Returned:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(55, 93)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Selling Price:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(59, 182)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Total Value:"
        '
        'lblSellingPrice
        '
        Me.lblSellingPrice.AutoSize = True
        Me.lblSellingPrice.Location = New System.Drawing.Point(164, 93)
        Me.lblSellingPrice.Name = "lblSellingPrice"
        Me.lblSellingPrice.Size = New System.Drawing.Size(13, 13)
        Me.lblSellingPrice.TabIndex = 6
        Me.lblSellingPrice.Text = "0"
        '
        'txtQtyReturned
        '
        Me.txtQtyReturned.Location = New System.Drawing.Point(164, 149)
        Me.txtQtyReturned.Name = "txtQtyReturned"
        Me.txtQtyReturned.Size = New System.Drawing.Size(100, 20)
        Me.txtQtyReturned.TabIndex = 7
        '
        'lblTotalValue
        '
        Me.lblTotalValue.AutoSize = True
        Me.lblTotalValue.Location = New System.Drawing.Point(164, 182)
        Me.lblTotalValue.Name = "lblTotalValue"
        Me.lblTotalValue.Size = New System.Drawing.Size(13, 13)
        Me.lblTotalValue.TabIndex = 8
        Me.lblTotalValue.Text = "0"
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(10, 256)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(103, 33)
        Me.btnSave.TabIndex = 9
        Me.btnSave.Text = "Process Returns"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(124, 256)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(73, 33)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(50, 124)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(73, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Quantity Sold:"
        '
        'lblqtysold
        '
        Me.lblqtysold.AutoSize = True
        Me.lblqtysold.Location = New System.Drawing.Point(164, 124)
        Me.lblqtysold.Name = "lblqtysold"
        Me.lblqtysold.Size = New System.Drawing.Size(13, 13)
        Me.lblqtysold.TabIndex = 12
        Me.lblqtysold.Text = "0"
        '
        'lblBarcode
        '
        Me.lblBarcode.AutoSize = True
        Me.lblBarcode.Font = New System.Drawing.Font("Wingdings 2", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.lblBarcode.Location = New System.Drawing.Point(217, 34)
        Me.lblBarcode.Name = "lblBarcode"
        Me.lblBarcode.Size = New System.Drawing.Size(13, 11)
        Me.lblBarcode.TabIndex = 13
        Me.lblBarcode.Text = "0"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(205, 256)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(73, 33)
        Me.btnClose.TabIndex = 14
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(69, 211)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Narration:"
        '
        'txtNarration
        '
        Me.txtNarration.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.txtNarration.Location = New System.Drawing.Point(129, 211)
        Me.txtNarration.Multiline = True
        Me.txtNarration.Name = "txtNarration"
        Me.txtNarration.Size = New System.Drawing.Size(149, 39)
        Me.txtNarration.TabIndex = 16
        '
        'lblVATstatus
        '
        Me.lblVATstatus.AutoSize = True
        Me.lblVATstatus.Location = New System.Drawing.Point(164, 65)
        Me.lblVATstatus.Name = "lblVATstatus"
        Me.lblVATstatus.Size = New System.Drawing.Size(10, 13)
        Me.lblVATstatus.TabIndex = 18
        Me.lblVATstatus.Text = "."
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(55, 65)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 13)
        Me.Label7.TabIndex = 17
        Me.Label7.Text = "V.A.T Rated:"
        '
        'lblItemBP
        '
        Me.lblItemBP.AutoSize = True
        Me.lblItemBP.Location = New System.Drawing.Point(236, 65)
        Me.lblItemBP.Name = "lblItemBP"
        Me.lblItemBP.Size = New System.Drawing.Size(13, 13)
        Me.lblItemBP.TabIndex = 19
        Me.lblItemBP.Text = "0"
        '
        'frmReceiveReturns
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(285, 298)
        Me.Controls.Add(Me.lblItemBP)
        Me.Controls.Add(Me.lblVATstatus)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtNarration)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblBarcode)
        Me.Controls.Add(Me.lblqtysold)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblTotalValue)
        Me.Controls.Add(Me.txtQtyReturned)
        Me.Controls.Add(Me.lblSellingPrice)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblReceiptNo)
        Me.Controls.Add(Me.lblFileNo)
        Me.Controls.Add(Me.lblDescription)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReceiveReturns"
        Me.Text = "Returns"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents lblFileNo As System.Windows.Forms.Label
    Friend WithEvents lblReceiptNo As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblSellingPrice As System.Windows.Forms.Label
    Friend WithEvents txtQtyReturned As System.Windows.Forms.TextBox
    Friend WithEvents lblTotalValue As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblqtysold As System.Windows.Forms.Label
    Friend WithEvents lblBarcode As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtNarration As System.Windows.Forms.TextBox
    Friend WithEvents lblVATstatus As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblItemBP As System.Windows.Forms.Label
End Class
