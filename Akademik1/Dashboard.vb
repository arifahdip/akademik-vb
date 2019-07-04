Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Data
Imports System.Data.OleDb
Public Class Dashboard
    Dim cnnOLEDB As New OleDbConnection
    Dim strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _
        System.Environment.CurrentDirectory & "\Akademik.accdb"
    Public ADP As OleDbDataAdapter
    Public DS As New DataSet
    Public ADP2 As OleDbDataAdapter
    Public DS2 As New DataSet

    Private Sub Dashboard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        cnnOLEDB.ConnectionString = strConnectionString
        cnnOLEDB.Open()

        Timer1.Enabled = True
        TampilChartIPK()
        TampilChartKehadiran()
        TampilCharBeasiswa()
        TampilChartPrestasi()
    End Sub

    Sub TampilChartIPK()

        Dim Query As String
        Dim DS = New DataSet

        For i = 0 To 3
            'Query TOP IK-2B
            Query = "SELECT TOP 3 Sub.Name AS Nama, ROUND(Sub.IP,2) AS IPB " &
                "FROM (SELECT m.Nama_Mhs AS Name, " &
                "SUM((SWITCH(n.NH='A','4',n.NH='B','3',n.NH='C','2',n.NH='D','1',n.NH='E','0'))*k.SKS)/SUM(k.SKS) AS IP " &
                "FROM Master_MK k INNER JOIN ((Master_Mahasiswa m INNER JOIN Kelas l ON m.NIM = l.NIM) INNER JOIN Nilai n ON m.NIM = n.NIM) ON k.Kode_MK = n.Kode_MK " &
                "WHERE n.Semester='IV' AND n.Tahun_Akademik='2019' AND l.Kelas='IK2B' " &
                "GROUP BY m.Nama_Mhs, n.NIM) Sub " &
                "ORDER BY Sub.IP DESC;"

            ADP = New OleDbDataAdapter(Query, cnnOLEDB)
            ADP.Fill(DS, i, 1, "TOP")

            'Query TOP IK-2A
            Query = "SELECT TOP 3 Sub.Name AS Nama, ROUND(Sub.IP,2) AS IPA " &
             "FROM (SELECT m.Nama_Mhs AS Name, " &
             "SUM((SWITCH(n.NH='A','4',n.NH='B','3',n.NH='C','2',n.NH='D','1',n.NH='E','0'))*k.SKS)/SUM(k.SKS) AS IP " &
             "FROM Master_MK k INNER JOIN ((Master_Mahasiswa m INNER JOIN Kelas l ON m.NIM = l.NIM) INNER JOIN Nilai n ON m.NIM = n.NIM) ON k.Kode_MK = n.Kode_MK " &
             "WHERE n.Semester='IV' AND n.Tahun_Akademik='2019' AND l.Kelas='IK2A' " &
             "GROUP BY m.Nama_Mhs, n.NIM) Sub " &
             "ORDER BY Sub.IP DESC;"

            ADP = New OleDbDataAdapter(Query, cnnOLEDB)
            ADP.Fill(DS, i, 1, "TOP")
        Next

        Chart1.Titles.Clear()
        Chart1.Series.Clear()
        Chart1.Series.Add("IK-2A")
        Chart1.Series.Add("IK-2B")

        Chart1.Series("IK-2A").ChartType = DataVisualization.Charting.SeriesChartType.Column
        Chart1.Series("IK-2B").ChartType = DataVisualization.Charting.SeriesChartType.Column

        Chart1.Series("IK-2B").XValueMember = "Nama"
        Chart1.Series("IK-2A").XValueMember = "Nama"

        Chart1.Series("IK-2B").YValueMembers = "IPB"
        Chart1.Series("IK-2A").YValueMembers = "IPA"
        Chart1.DataSource = DS.Tables("TOP")


    End Sub

    Sub TampilChartKehadiran()
        ADP = New OleDbDataAdapter("SELECT TOP 3 SUM(k.Alpa) as SumAlpa, SUM(k.Ijin) as SumIjin, SUM(k.Sakit) as SumSakit, m.Nama_Mhs as Nama_Mhs FROM Kehadiran k INNER JOIN Master_Mahasiswa m ON k.NIM=m.NIM GROUP BY k.NIM, m.Nama_Mhs ORDER BY SUM(k.Alpa) DESC", cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Tabel1")

        Chart2.Series.Clear()
        Chart2.Series.Add("Ijin")
        Chart2.Series.Add("Sakit")
        Chart2.Series.Add("Alpa")

        Chart2.Series("Ijin").XValueMember = "Nama_Mhs"
        Chart2.Series("Ijin").YValueMembers = "SumIjin"
        Chart2.Series("Sakit").YValueMembers = "SumSakit"
        Chart2.Series("Alpa").YValueMembers = "SumAlpa"

        Chart2.Series("Ijin").ChartType = SeriesChartType.Line
        Chart2.Series("Ijin").BorderWidth = 2
        Chart2.Series("Sakit").ChartType = SeriesChartType.Line
        Chart2.Series("Sakit").BorderWidth = 2
        Chart2.Series("Alpa").ChartType = SeriesChartType.Line
        Chart2.Series("Alpa").BorderWidth = 2

        Chart2.ChartAreas(0).AxisX.Interval = 1
        Chart2.ChartAreas(0).AxisY.Interval = 2
        Chart2.DataSource = DS.Tables("Tabel1")

    End Sub

    Sub TampilCharBeasiswa()
        ADP = New OleDbDataAdapter("SELECT m.Nama_BS AS Nama_BS, COUNT(b.NIM) AS JumlahPenerima FROM Master_Beasiswa m INNER JOIN Beasiswa b ON m.Kode_BS=b.Kode_BS GROUP BY m.Nama_BS", cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Tabel1")
        Chart3.Series.Clear()
        Chart3.Series.Add("Beasiswa")

        Chart3.Series("Beasiswa").XValueMember = "Nama_BS"
        Chart3.Series("Beasiswa").YValueMembers = "JumlahPenerima"

        Chart3.Series("Beasiswa").ChartType = SeriesChartType.Pie
        Chart3.DataSource = DS.Tables("Tabel1")

    End Sub

    Sub TampilChartPrestasi()
        ADP = New OleDbDataAdapter("SELECT TOP 3 p.NIM as NIM, SUM(SWITCH(p.Tingkat='Internasional',4,p.Tingkat='Nasional',3,p.Tingkat='Provinsi',2,p.Tingkat='Kabupaten',1,True,0)) AS Regional, m.Nama_Mhs as Nama_Mhs FROM Prestasi p INNER JOIN Master_Mahasiswa m ON p.NIM=m.NIM GROUP BY p.NIM, m.Nama_Mhs ORDER BY SUM(SWITCH(p.Tingkat='Internasional',4,p.Tingkat='Nasional',3,p.Tingkat='Provinsi',2,p.Tingkat='Kabupaten',1,True,0)) DESC", cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Tabel1")

        Chart4.Series.Clear()
        Chart4.Series.Add("prestasi")

        Chart4.Series("prestasi").ChartType = SeriesChartType.Bar
        Chart4.Series("prestasi").XValueMember = "Nama_Mhs"
        Chart4.Series("prestasi").YValueMembers = "Regional"

        Chart4.DataSource = DS.Tables("Tabel1")
        Chart4.DataBind()

    End Sub


    Private Sub DosenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DosenToolStripMenuItem.Click
        Form3.Show()
    End Sub

    Private Sub BeasiswaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeasiswaToolStripMenuItem.Click
        Form4.Show()
    End Sub

    Private Sub KehadiranToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KehadiranToolStripMenuItem.Click
        Form5.Show()
    End Sub

    Private Sub MahasiswaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MahasiswaToolStripMenuItem1.Click
        Form6.Show()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub KehadiranToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KehadiranToolStripMenuItem1.Click
        Form7.Show()
    End Sub

    Private Sub BeasiswaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BeasiswaToolStripMenuItem1.Click
        Form8.Show()
    End Sub

    Private Sub DataPrestasiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DataPrestasiToolStripMenuItem.Click
        Form9.Show()
    End Sub

    Private Sub JadwalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JadwalToolStripMenuItem.Click
        Form10.Show()
    End Sub

    Private Sub NilaiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NilaiToolStripMenuItem.Click
        Form11.Show()
    End Sub

    Private Sub MahasiswaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MahasiswaToolStripMenuItem.Click
        Form1.Show()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Form2.Show()
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        TampilChartIPK()
        TampilCharBeasiswa()
        TampilChartKehadiran()
        TampilChartPrestasi()
    End Sub
End Class