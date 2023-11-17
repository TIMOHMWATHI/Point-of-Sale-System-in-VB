<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintStockOrder
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
        Me.CRStockOrder = New CrystalDecisions.Windows.Forms.CrystalReportViewer()
        Me.SuspendLayout()
        '
        'CRStockOrder
        '
        Me.CRStockOrder.ActiveViewIndex = -1
        Me.CRStockOrder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.CRStockOrder.Cursor = System.Windows.Forms.Cursors.Default
        Me.CRStockOrder.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CRStockOrder.Location = New System.Drawing.Point(0, 0)
        Me.CRStockOrder.Name = "CRStockOrder"
        Me.CRStockOrder.Size = New System.Drawing.Size(884, 411)
        Me.CRStockOrder.TabIndex = 0
        '
        'frmPrintStockOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(884, 411)
        Me.Controls.Add(Me.CRStockOrder)
        Me.Name = "frmPrintStockOrder"
        Me.Text = "Print Stock Order"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CRStockOrder As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
