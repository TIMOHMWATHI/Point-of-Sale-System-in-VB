<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreditRunningTotals
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCreditRunningTotals))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCreditorName = New System.Windows.Forms.TextBox()
        Me.lblEntryid = New System.Windows.Forms.Label()
        Me.dgvCreditStatement = New System.Windows.Forms.DataGridView()
        Me.btnLoadGrid = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        CType(Me.dgvCreditStatement, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(93, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Filter by Customer:"
        '
        'txtCreditorName
        '
        Me.txtCreditorName.Location = New System.Drawing.Point(113, 17)
        Me.txtCreditorName.Name = "txtCreditorName"
        Me.txtCreditorName.Size = New System.Drawing.Size(267, 20)
        Me.txtCreditorName.TabIndex = 1
        '
        'lblEntryid
        '
        Me.lblEntryid.AutoSize = True
        Me.lblEntryid.Font = New System.Drawing.Font("Wingdings 3", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.lblEntryid.Location = New System.Drawing.Point(117, 47)
        Me.lblEntryid.Name = "lblEntryid"
        Me.lblEntryid.Size = New System.Drawing.Size(11, 12)
        Me.lblEntryid.TabIndex = 2
        Me.lblEntryid.Text = "0"
        '
        'dgvCreditStatement
        '
        Me.dgvCreditStatement.AllowUserToAddRows = False
        Me.dgvCreditStatement.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvCreditStatement.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvCreditStatement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Arial Narrow", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvCreditStatement.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvCreditStatement.Location = New System.Drawing.Point(7, 76)
        Me.dgvCreditStatement.Name = "dgvCreditStatement"
        Me.dgvCreditStatement.ReadOnly = True
        Me.dgvCreditStatement.Size = New System.Drawing.Size(478, 384)
        Me.dgvCreditStatement.TabIndex = 3
        '
        'btnLoadGrid
        '
        Me.btnLoadGrid.Location = New System.Drawing.Point(393, 5)
        Me.btnLoadGrid.Name = "btnLoadGrid"
        Me.btnLoadGrid.Size = New System.Drawing.Size(92, 32)
        Me.btnLoadGrid.TabIndex = 4
        Me.btnLoadGrid.Text = "Get Statement"
        Me.btnLoadGrid.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(393, 43)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(89, 26)
        Me.btnClose.TabIndex = 5
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmCreditRunningTotals
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(494, 466)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnLoadGrid)
        Me.Controls.Add(Me.dgvCreditStatement)
        Me.Controls.Add(Me.lblEntryid)
        Me.Controls.Add(Me.txtCreditorName)
        Me.Controls.Add(Me.Label1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCreditRunningTotals"
        Me.Text = "Credit Statement"
        CType(Me.dgvCreditStatement, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCreditorName As System.Windows.Forms.TextBox
    Friend WithEvents lblEntryid As System.Windows.Forms.Label
    Friend WithEvents dgvCreditStatement As System.Windows.Forms.DataGridView
    Friend WithEvents btnLoadGrid As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class
