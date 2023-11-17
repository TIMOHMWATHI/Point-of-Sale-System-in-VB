<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrint
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrint))
        Me.CRPrint = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'CRPrint
        '
        Me.CRPrint.ActiveViewIndex = -1
        Me.CRPrint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRPrint.Cursor = System.Windows.Forms.Cursors.Default
        Me.CRPrint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRPrint.Location = New System.Drawing.Point(0, 0)
        Me.CRPrint.Name = "CRPrint"
        Me.CRPrint.Size = New System.Drawing.Size(834, 501)
        Me.CRPrint.TabIndex = 0
        '
        'frmPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 501)
        Me.Controls.Add(Me.CRPrint)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmPrint"
        Me.Text = "Print"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CRPrint As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
