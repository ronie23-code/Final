<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.UserControl12 = New Final.UserControl1()
        Me.UserControl32 = New Final.UserControl3()
        Me.SuspendLayout()
        '
        'UserControl12
        '
        Me.UserControl12.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(104, Byte), Integer))
        Me.UserControl12.Location = New System.Drawing.Point(1, -38)
        Me.UserControl12.Name = "UserControl12"
        Me.UserControl12.Size = New System.Drawing.Size(414, 518)
        Me.UserControl12.TabIndex = 1
        '
        'UserControl32
        '
        Me.UserControl32.BackColor = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(209, Byte), Integer), CType(CType(104, Byte), Integer))
        Me.UserControl32.Location = New System.Drawing.Point(-9, -29)
        Me.UserControl32.Name = "UserControl32"
        Me.UserControl32.Size = New System.Drawing.Size(414, 518)
        Me.UserControl32.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(398, 479)
        Me.Controls.Add(Me.UserControl12)
        Me.Controls.Add(Me.UserControl32)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HIMS"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents UserControl31 As UserControl3
    Friend WithEvents UserControl11 As UserControl1
    Friend WithEvents UserControl32 As UserControl3
    Friend WithEvents UserControl12 As UserControl1
End Class
