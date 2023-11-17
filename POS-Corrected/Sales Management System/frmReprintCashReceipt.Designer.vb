<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReprintCashReceipt
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReprintCashReceipt))
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.lblChange = New System.Windows.Forms.Label()
        Me.lblpaid = New System.Windows.Forms.Label()
        Me.lblAmntPaid = New System.Windows.Forms.Label()
        Me.lblSoldBy = New System.Windows.Forms.Label()
        Me.lblReceptDate = New System.Windows.Forms.Label()
        Me.lbluser = New System.Windows.Forms.Label()
        Me.lblsdate = New System.Windows.Forms.Label()
        Me.btnReprint = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblReceptDiscount = New System.Windows.Forms.Label()
        Me.txtSearchReceipt = New System.Windows.Forms.TextBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblReceptTotal = New System.Windows.Forms.Label()
        Me.dgvCashItemsSold = New System.Windows.Forms.DataGridView()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.dgvCashItemsSold, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer3
        '
        Me.SplitContainer3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.BackColor = System.Drawing.Color.Gainsboro
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblChange)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblpaid)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblAmntPaid)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblSoldBy)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblReceptDate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lbluser)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblsdate)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnReprint)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label3)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblReceptDiscount)
        Me.SplitContainer3.Panel1.Controls.Add(Me.txtSearchReceipt)
        Me.SplitContainer3.Panel1.Controls.Add(Me.btnRefresh)
        Me.SplitContainer3.Panel1.Controls.Add(Me.Label2)
        Me.SplitContainer3.Panel1.Controls.Add(Me.lblReceptTotal)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.dgvCashItemsSold)
        Me.SplitContainer3.Size = New System.Drawing.Size(755, 431)
        Me.SplitContainer3.SplitterDistance = 89
        Me.SplitContainer3.TabIndex = 1
        '
        'lblChange
        '
        Me.lblChange.AutoSize = True
        Me.lblChange.Location = New System.Drawing.Point(492, 63)
        Me.lblChange.Name = "lblChange"
        Me.lblChange.Size = New System.Drawing.Size(13, 13)
        Me.lblChange.TabIndex = 14
        Me.lblChange.Text = "0"
        Me.lblChange.Visible = False
        '
        'lblpaid
        '
        Me.lblpaid.AutoSize = True
        Me.lblpaid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpaid.Location = New System.Drawing.Point(327, 64)
        Me.lblpaid.Name = "lblpaid"
        Me.lblpaid.Size = New System.Drawing.Size(70, 13)
        Me.lblpaid.TabIndex = 13
        Me.lblpaid.Text = "Amount Paid:"
        Me.lblpaid.Visible = False
        '
        'lblAmntPaid
        '
        Me.lblAmntPaid.AutoSize = True
        Me.lblAmntPaid.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmntPaid.ForeColor = System.Drawing.Color.Red
        Me.lblAmntPaid.Location = New System.Drawing.Point(407, 64)
        Me.lblAmntPaid.Name = "lblAmntPaid"
        Me.lblAmntPaid.Size = New System.Drawing.Size(10, 13)
        Me.lblAmntPaid.TabIndex = 12
        Me.lblAmntPaid.Text = "."
        '
        'lblSoldBy
        '
        Me.lblSoldBy.AutoSize = True
        Me.lblSoldBy.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSoldBy.Location = New System.Drawing.Point(111, 63)
        Me.lblSoldBy.Name = "lblSoldBy"
        Me.lblSoldBy.Size = New System.Drawing.Size(10, 14)
        Me.lblSoldBy.TabIndex = 11
        Me.lblSoldBy.Text = "."
        '
        'lblReceptDate
        '
        Me.lblReceptDate.AutoSize = True
        Me.lblReceptDate.Font = New System.Drawing.Font("Times New Roman", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceptDate.Location = New System.Drawing.Point(110, 37)
        Me.lblReceptDate.Name = "lblReceptDate"
        Me.lblReceptDate.Size = New System.Drawing.Size(10, 14)
        Me.lblReceptDate.TabIndex = 10
        Me.lblReceptDate.Text = "."
        '
        'lbluser
        '
        Me.lbluser.AutoSize = True
        Me.lbluser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbluser.Location = New System.Drawing.Point(58, 64)
        Me.lbluser.Name = "lbluser"
        Me.lbluser.Size = New System.Drawing.Size(46, 13)
        Me.lbluser.TabIndex = 9
        Me.lbluser.Text = "Sold By:"
        Me.lbluser.Visible = False
        '
        'lblsdate
        '
        Me.lblsdate.AutoSize = True
        Me.lblsdate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblsdate.Location = New System.Drawing.Point(42, 38)
        Me.lblsdate.Name = "lblsdate"
        Me.lblsdate.Size = New System.Drawing.Size(62, 13)
        Me.lblsdate.TabIndex = 8
        Me.lblsdate.Text = "Sales Date:"
        Me.lblsdate.Visible = False
        '
        'btnReprint
        '
        Me.btnReprint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReprint.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnReprint.Image = Global.Strawberry.My.Resources.Resources.print
        Me.btnReprint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReprint.Location = New System.Drawing.Point(650, 11)
        Me.btnReprint.Name = "btnReprint"
        Me.btnReprint.Size = New System.Drawing.Size(95, 51)
        Me.btnReprint.TabIndex = 7
        Me.btnReprint.Text = "Print"
        Me.btnReprint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReprint.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(318, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Discount Total:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Search Receipt No:"
        '
        'lblReceptDiscount
        '
        Me.lblReceptDiscount.AutoSize = True
        Me.lblReceptDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceptDiscount.ForeColor = System.Drawing.Color.Red
        Me.lblReceptDiscount.Location = New System.Drawing.Point(407, 36)
        Me.lblReceptDiscount.Name = "lblReceptDiscount"
        Me.lblReceptDiscount.Size = New System.Drawing.Size(13, 13)
        Me.lblReceptDiscount.TabIndex = 5
        Me.lblReceptDiscount.Text = "0"
        '
        'txtSearchReceipt
        '
        Me.txtSearchReceipt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearchReceipt.Location = New System.Drawing.Point(110, 5)
        Me.txtSearchReceipt.Name = "txtSearchReceipt"
        Me.txtSearchReceipt.Size = New System.Drawing.Size(75, 20)
        Me.txtSearchReceipt.TabIndex = 1
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.Image = Global.Strawberry.My.Resources.Resources.refresh
        Me.btnRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRefresh.Location = New System.Drawing.Point(549, 11)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(95, 51)
        Me.btnRefresh.TabIndex = 4
        Me.btnRefresh.Text = "Refresh"
        Me.btnRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(326, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(71, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Receipt Total"
        '
        'lblReceptTotal
        '
        Me.lblReceptTotal.AutoSize = True
        Me.lblReceptTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceptTotal.ForeColor = System.Drawing.Color.Red
        Me.lblReceptTotal.Location = New System.Drawing.Point(407, 8)
        Me.lblReceptTotal.Name = "lblReceptTotal"
        Me.lblReceptTotal.Size = New System.Drawing.Size(13, 13)
        Me.lblReceptTotal.TabIndex = 3
        Me.lblReceptTotal.Text = "0"
        '
        'dgvCashItemsSold
        '
        Me.dgvCashItemsSold.AllowUserToAddRows = False
        Me.dgvCashItemsSold.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.dgvCashItemsSold.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCashItemsSold.BackgroundColor = System.Drawing.Color.MintCream
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCashItemsSold.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.dgvCashItemsSold.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvCashItemsSold.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvCashItemsSold.Location = New System.Drawing.Point(0, 0)
        Me.dgvCashItemsSold.Name = "dgvCashItemsSold"
        Me.dgvCashItemsSold.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dgvCashItemsSold.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvCashItemsSold.Size = New System.Drawing.Size(751, 334)
        Me.dgvCashItemsSold.TabIndex = 1
        '
        'frmReprintCashReceipt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(755, 431)
        Me.Controls.Add(Me.SplitContainer3)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReprintCashReceipt"
        Me.Text = "Reprint Sale Receipt"
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.dgvCashItemsSold, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents lblpaid As System.Windows.Forms.Label
    Friend WithEvents lblAmntPaid As System.Windows.Forms.Label
    Friend WithEvents lblSoldBy As System.Windows.Forms.Label
    Friend WithEvents lblReceptDate As System.Windows.Forms.Label
    Friend WithEvents lbluser As System.Windows.Forms.Label
    Friend WithEvents lblsdate As System.Windows.Forms.Label
    Friend WithEvents btnReprint As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblReceptDiscount As System.Windows.Forms.Label
    Friend WithEvents txtSearchReceipt As System.Windows.Forms.TextBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblReceptTotal As System.Windows.Forms.Label
    Friend WithEvents dgvCashItemsSold As System.Windows.Forms.DataGridView
    Friend WithEvents lblChange As System.Windows.Forms.Label
End Class
