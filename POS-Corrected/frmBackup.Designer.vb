<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBackup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBackup))
        Me.btnBackup = New System.Windows.Forms.Button()
        Me.lblDBName = New System.Windows.Forms.Label()
        Me.lblLocalDisk = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnBackup
        '
        Me.btnBackup.Font = New System.Drawing.Font("Tahoma", 20.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackup.Location = New System.Drawing.Point(13, 13)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(195, 67)
        Me.btnBackup.TabIndex = 0
        Me.btnBackup.Text = "Backup"
        Me.btnBackup.UseVisualStyleBackColor = True
        '
        'lblDBName
        '
        Me.lblDBName.AutoSize = True
        Me.lblDBName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDBName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDBName.Location = New System.Drawing.Point(123, -1)
        Me.lblDBName.Name = "lblDBName"
        Me.lblDBName.Size = New System.Drawing.Size(45, 13)
        Me.lblDBName.TabIndex = 4
        Me.lblDBName.Text = "Label2"
        Me.lblDBName.Visible = False
        '
        'lblLocalDisk
        '
        Me.lblLocalDisk.AutoSize = True
        Me.lblLocalDisk.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLocalDisk.ForeColor = System.Drawing.Color.Red
        Me.lblLocalDisk.Location = New System.Drawing.Point(54, -1)
        Me.lblLocalDisk.Name = "lblLocalDisk"
        Me.lblLocalDisk.Size = New System.Drawing.Size(45, 13)
        Me.lblLocalDisk.TabIndex = 3
        Me.lblLocalDisk.Text = "Label1"
        Me.lblLocalDisk.Visible = False
        '
        'frmBackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(220, 92)
        Me.Controls.Add(Me.lblDBName)
        Me.Controls.Add(Me.lblLocalDisk)
        Me.Controls.Add(Me.btnBackup)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBackup"
        Me.Text = "Backup Data"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBackup As System.Windows.Forms.Button
    Friend WithEvents lblDBName As System.Windows.Forms.Label
    Friend WithEvents lblLocalDisk As System.Windows.Forms.Label
End Class
