<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Validasi
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
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.BtnURL = New System.Windows.Forms.Button()
        Me.BtnEmail = New System.Windows.Forms.Button()
        Me.txtURL = New System.Windows.Forms.TextBox()
        Me.TxtEmail = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Monotype Corsiva", 15.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(144, 36)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(200, 25)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "Validasi Alamat email"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 134)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(82, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Masukkan URL"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(33, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Masukkan Email"
        '
        'BtnURL
        '
        Me.BtnURL.Location = New System.Drawing.Point(414, 131)
        Me.BtnURL.Name = "BtnURL"
        Me.BtnURL.Size = New System.Drawing.Size(75, 23)
        Me.BtnURL.TabIndex = 16
        Me.BtnURL.Text = "Cek URL"
        Me.BtnURL.UseVisualStyleBackColor = True
        '
        'BtnEmail
        '
        Me.BtnEmail.Location = New System.Drawing.Point(414, 91)
        Me.BtnEmail.Name = "BtnEmail"
        Me.BtnEmail.Size = New System.Drawing.Size(75, 23)
        Me.BtnEmail.TabIndex = 15
        Me.BtnEmail.Text = "Cek E-mail"
        Me.BtnEmail.UseVisualStyleBackColor = True
        '
        'txtURL
        '
        Me.txtURL.Location = New System.Drawing.Point(134, 131)
        Me.txtURL.Name = "txtURL"
        Me.txtURL.Size = New System.Drawing.Size(236, 20)
        Me.txtURL.TabIndex = 14
        '
        'TxtEmail
        '
        Me.TxtEmail.Location = New System.Drawing.Point(134, 94)
        Me.TxtEmail.Name = "TxtEmail"
        Me.TxtEmail.Size = New System.Drawing.Size(236, 20)
        Me.TxtEmail.TabIndex = 13
        '
        'Validasi
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(550, 215)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnURL)
        Me.Controls.Add(Me.BtnEmail)
        Me.Controls.Add(Me.txtURL)
        Me.Controls.Add(Me.TxtEmail)
        Me.Name = "Validasi"
        Me.Text = "Validasi"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents BtnURL As System.Windows.Forms.Button
    Friend WithEvents BtnEmail As System.Windows.Forms.Button
    Friend WithEvents txtURL As System.Windows.Forms.TextBox
    Friend WithEvents TxtEmail As System.Windows.Forms.TextBox
End Class
