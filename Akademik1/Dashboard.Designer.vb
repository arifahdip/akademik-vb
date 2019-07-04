<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Dashboard
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
        Me.components = New System.ComponentModel.Container()
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series3 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series4 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim Series5 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea3 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend3 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series6 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Dim ChartArea4 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New System.Windows.Forms.DataVisualization.Charting.ChartArea()
        Dim Legend4 As System.Windows.Forms.DataVisualization.Charting.Legend = New System.Windows.Forms.DataVisualization.Charting.Legend()
        Dim Series7 As System.Windows.Forms.DataVisualization.Charting.Series = New System.Windows.Forms.DataVisualization.Charting.Series()
        Me.Chart1 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.DataPrestasiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.DataIndukToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MahasiswaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DosenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeasiswaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.KehadiranToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataTransakasiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MahasiswaToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.KehadiranToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BeasiswaToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.JadwalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NilaiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Chart2 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart3 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Chart4 = New System.Windows.Forms.DataVisualization.Charting.Chart()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Chart4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Chart1
        '
        Me.Chart1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        ChartArea1.Name = "ChartArea1"
        Me.Chart1.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        Me.Chart1.Legends.Add(Legend1)
        Me.Chart1.Location = New System.Drawing.Point(150, 197)
        Me.Chart1.Name = "Chart1"
        Me.Chart1.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry
        Series1.ChartArea = "ChartArea1"
        Series1.Color = System.Drawing.Color.Gold
        Series1.Legend = "Legend1"
        Series1.Name = "IK2A"
        Series2.ChartArea = "ChartArea1"
        Series2.Color = System.Drawing.Color.Blue
        Series2.Legend = "Legend1"
        Series2.Name = "IK2B"
        Me.Chart1.Series.Add(Series1)
        Me.Chart1.Series.Add(Series2)
        Me.Chart1.Size = New System.Drawing.Size(442, 224)
        Me.Chart1.TabIndex = 0
        Me.Chart1.Text = "Chart1"
        '
        'DataPrestasiToolStripMenuItem
        '
        Me.DataPrestasiToolStripMenuItem.Name = "DataPrestasiToolStripMenuItem"
        Me.DataPrestasiToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.DataPrestasiToolStripMenuItem.Text = "Data Prestasi"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DataIndukToolStripMenuItem, Me.DataTransakasiToolStripMenuItem, Me.AboutToolStripMenuItem, Me.ExitToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1370, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'DataIndukToolStripMenuItem
        '
        Me.DataIndukToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MahasiswaToolStripMenuItem, Me.DosenToolStripMenuItem, Me.BeasiswaToolStripMenuItem, Me.KehadiranToolStripMenuItem})
        Me.DataIndukToolStripMenuItem.Name = "DataIndukToolStripMenuItem"
        Me.DataIndukToolStripMenuItem.Size = New System.Drawing.Size(76, 20)
        Me.DataIndukToolStripMenuItem.Text = "Data Induk"
        '
        'MahasiswaToolStripMenuItem
        '
        Me.MahasiswaToolStripMenuItem.Name = "MahasiswaToolStripMenuItem"
        Me.MahasiswaToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.MahasiswaToolStripMenuItem.Text = "Mahasiswa"
        '
        'DosenToolStripMenuItem
        '
        Me.DosenToolStripMenuItem.Name = "DosenToolStripMenuItem"
        Me.DosenToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.DosenToolStripMenuItem.Text = "Dosen"
        '
        'BeasiswaToolStripMenuItem
        '
        Me.BeasiswaToolStripMenuItem.Name = "BeasiswaToolStripMenuItem"
        Me.BeasiswaToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.BeasiswaToolStripMenuItem.Text = "Beasiswa"
        '
        'KehadiranToolStripMenuItem
        '
        Me.KehadiranToolStripMenuItem.Name = "KehadiranToolStripMenuItem"
        Me.KehadiranToolStripMenuItem.Size = New System.Drawing.Size(137, 22)
        Me.KehadiranToolStripMenuItem.Text = "Mata Kuliah"
        '
        'DataTransakasiToolStripMenuItem
        '
        Me.DataTransakasiToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MahasiswaToolStripMenuItem1, Me.KehadiranToolStripMenuItem1, Me.BeasiswaToolStripMenuItem1, Me.DataPrestasiToolStripMenuItem, Me.JadwalToolStripMenuItem, Me.NilaiToolStripMenuItem})
        Me.DataTransakasiToolStripMenuItem.Name = "DataTransakasiToolStripMenuItem"
        Me.DataTransakasiToolStripMenuItem.Size = New System.Drawing.Size(100, 20)
        Me.DataTransakasiToolStripMenuItem.Text = "Data Transakasi"
        '
        'MahasiswaToolStripMenuItem1
        '
        Me.MahasiswaToolStripMenuItem1.Name = "MahasiswaToolStripMenuItem1"
        Me.MahasiswaToolStripMenuItem1.Size = New System.Drawing.Size(141, 22)
        Me.MahasiswaToolStripMenuItem1.Text = "Mahasiswa"
        '
        'KehadiranToolStripMenuItem1
        '
        Me.KehadiranToolStripMenuItem1.Name = "KehadiranToolStripMenuItem1"
        Me.KehadiranToolStripMenuItem1.Size = New System.Drawing.Size(141, 22)
        Me.KehadiranToolStripMenuItem1.Text = "Kehadiran"
        '
        'BeasiswaToolStripMenuItem1
        '
        Me.BeasiswaToolStripMenuItem1.Name = "BeasiswaToolStripMenuItem1"
        Me.BeasiswaToolStripMenuItem1.Size = New System.Drawing.Size(141, 22)
        Me.BeasiswaToolStripMenuItem1.Text = "Beasiswa"
        '
        'JadwalToolStripMenuItem
        '
        Me.JadwalToolStripMenuItem.Name = "JadwalToolStripMenuItem"
        Me.JadwalToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.JadwalToolStripMenuItem.Text = "Jadwal"
        '
        'NilaiToolStripMenuItem
        '
        Me.NilaiToolStripMenuItem.Name = "NilaiToolStripMenuItem"
        Me.NilaiToolStripMenuItem.Size = New System.Drawing.Size(141, 22)
        Me.NilaiToolStripMenuItem.Text = "Nilai"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'Timer1
        '
        '
        'Chart2
        '
        Me.Chart2.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        ChartArea2.Name = "ChartArea1"
        Me.Chart2.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        Me.Chart2.Legends.Add(Legend2)
        Me.Chart2.Location = New System.Drawing.Point(150, 446)
        Me.Chart2.Name = "Chart2"
        Me.Chart2.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry
        Series3.ChartArea = "ChartArea1"
        Series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series3.Legend = "Legend1"
        Series3.Name = "Sakit"
        Series4.ChartArea = "ChartArea1"
        Series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series4.Legend = "Legend1"
        Series4.Name = "Ijin"
        Series5.ChartArea = "ChartArea1"
        Series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line
        Series5.Legend = "Legend1"
        Series5.Name = "Alpa"
        Me.Chart2.Series.Add(Series3)
        Me.Chart2.Series.Add(Series4)
        Me.Chart2.Series.Add(Series5)
        Me.Chart2.Size = New System.Drawing.Size(442, 224)
        Me.Chart2.TabIndex = 2
        Me.Chart2.Text = "Chart2"
        '
        'Chart3
        '
        Me.Chart3.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        ChartArea3.Name = "ChartArea1"
        Me.Chart3.ChartAreas.Add(ChartArea3)
        Legend3.Name = "Legend1"
        Me.Chart3.Legends.Add(Legend3)
        Me.Chart3.Location = New System.Drawing.Point(683, 197)
        Me.Chart3.Name = "Chart3"
        Me.Chart3.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry
        Series6.ChartArea = "ChartArea1"
        Series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
        Series6.Color = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Series6.Legend = "Legend1"
        Series6.Name = "beasiswa"
        Me.Chart3.Series.Add(Series6)
        Me.Chart3.Size = New System.Drawing.Size(442, 224)
        Me.Chart3.TabIndex = 3
        Me.Chart3.Text = "Chart3"
        '
        'Chart4
        '
        Me.Chart4.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        ChartArea4.Name = "ChartArea1"
        Me.Chart4.ChartAreas.Add(ChartArea4)
        Legend4.Name = "Legend1"
        Me.Chart4.Legends.Add(Legend4)
        Me.Chart4.Location = New System.Drawing.Point(683, 446)
        Me.Chart4.Name = "Chart4"
        Me.Chart4.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry
        Series7.ChartArea = "ChartArea1"
        Series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Bar
        Series7.Legend = "Legend1"
        Series7.Name = "prestasi"
        Me.Chart4.Series.Add(Series7)
        Me.Chart4.Size = New System.Drawing.Size(442, 224)
        Me.Chart4.TabIndex = 4
        Me.Chart4.Text = "Chart4"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(0, 24)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1416, 121)
        Me.Panel1.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Monotype Corsiva", 21.75!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(574, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(255, 36)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Dashboard Akademik"
        '
        'Dashboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1370, 590)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Chart4)
        Me.Controls.Add(Me.Chart3)
        Me.Controls.Add(Me.Chart2)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.Chart1)
        Me.Name = "Dashboard"
        Me.Text = "Dashboard"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.Chart1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.Chart2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Chart4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Chart1 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents DataPrestasiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents DataIndukToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MahasiswaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DosenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeasiswaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KehadiranToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DataTransakasiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MahasiswaToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents KehadiranToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BeasiswaToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JadwalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents NilaiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Chart2 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart3 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Chart4 As System.Windows.Forms.DataVisualization.Charting.Chart
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
