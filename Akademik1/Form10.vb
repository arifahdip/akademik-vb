Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Public Class Form10
    Dim cnnOLEDB As New OleDbConnection
    Dim cmdInsert As New OleDbCommand
    Dim cmdUpdate As New OleDbCommand
    Dim cmdDelete As New OleDbCommand
    Dim cmdOLEDB As New OleDbCommand
    Dim strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _
        System.Environment.CurrentDirectory & "\Akademik.accdb"

    Public ADP As OleDbDataAdapter
    Public DS As New DataSet

    Private Sub Form10_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cnnOLEDB.ConnectionString = strConnectionString
        cnnOLEDB.Open()
        TampilData()
        tampilcomboboxkelas()
        tampilcomboboxDosen()
        TampilDataJadwalSesuaiHaridanKelas()
        TampilNIP()
        TampilData()
        autonumber()
        disableTeksbox()
        ListBox1.Visible = False
    End Sub

    Sub TampilDataJadwalSesuaiHaridanKelas()
        ADP = New OleDbDataAdapter("SELECT a.Hari, a.Jam, c.Nama_MK, b.Nama_Dosen, a.Ruang, a.Kelas FROM(Jadwal a LEFT JOIN Master_Dosen b ON b.NIP=a.NIP) LEFT JOIN Master_MK c ON c.Kode_MK=a.Kode_MK WHERE a.Hari='" & CmbHari2.Text & "' AND a.Kelas='" & CmbKelas2.Text & "' ORDER BY a.Jam ASC", cnnOLEDB)

        DS = New DataSet
        ADP.Fill(DS, "Tabel1")
        DataGridView1.DataSource = DS.Tables("Tabel1")
    End Sub

    Sub Bersih()
        TxtTA.Text = ""
        TxtJam.Text = ""
        TxtNIP.Text = ""
        CmbMK.Text = ""
        CmbRuang.Text = ""
        CmbHari.Text = ""
        CmbSemester.Text = ""
        CmbKelas.Text = ""
        CmbDosen.Text = ""
        autonumber()
    End Sub

    Sub disableTeksbox()
        TxtNIP.Enabled = False
        TXTKodeMK.Enabled = False
    End Sub

    Sub TampilData()
        ADP = New OleDbDataAdapter("SELECT * FROM Jadwal ORDER BY Tahun_Akademik", cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Tabel1")
        DataGridView1.DataSource = DS.Tables("Tabel1")
    End Sub

    Sub tampilNama_MK()
        ListBox1.Visible = False
        ADP = New OleDbDataAdapter("SELECT Nama_MK FROM Master_MK WHERE Kode_MK='" & TXTKodeMK.Text & "'", cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Nama")
        Dim Dta As DataTable = DS.Tables("Nama")
        ListBox1.DataSource = Dta
        ListBox1.DisplayMember = "Nama_MK"
        CmbMK.Text = ListBox1.Text
        ListBox1.Text = ""
    End Sub

    Sub tampilcomboboxKode_MK()
        Dim query As String
        Dim rs As OleDbDataReader
        query = "SELECT Nama_MK FROM Master_MK"
        cmdOLEDB = New OleDb.OleDbCommand(query, cnnOLEDB)
        rs = cmdOLEDB.ExecuteReader
        Try
            While rs.Read
                CmbMK.Items.Add(rs("Nama_MK"))
            End While
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        CmbMK.SelectedIndex = 0
        rs.Close()
    End Sub

    Sub tampilcomboboxkelas()
        Dim query As String
        Dim rs As OleDbDataReader
        query = "SELECT DISTINCT Kelas FROM Kelas"
        cmdOLEDB = New OleDb.OleDbCommand(query, cnnOLEDB)
        rs = cmdOLEDB.ExecuteReader
        Try
            While rs.Read
                CmbKelas.Items.Add(rs("Kelas"))
            End While
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        CmbKelas.SelectedIndex = 0
        rs.Close()
    End Sub

    Sub tampilcomboboxDosen()
        Dim query As String
        Dim rs As OleDbDataReader
        query = "SELECT Nama_Dosen FROM Master_Dosen"
        cmdOLEDB = New OleDb.OleDbCommand(query, cnnOLEDB)
        rs = cmdOLEDB.ExecuteReader
        Try
            While rs.Read
                CmbDosen.Items.Add(rs("Nama_Dosen"))
            End While
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        CmbDosen.SelectedIndex = 0
        rs.Close()
    End Sub

    Sub TampilKdMK()
        ListBox1.Visible = False
        ADP = New OleDbDataAdapter("SELECT Kode_MK FROM Master_MK WHERE Nama_MK='" & CmbMK.Text & "'", cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Nama")
        Dim Dta As DataTable = DS.Tables("Nama")
        ListBox1.DataSource = Dta
        ListBox1.DisplayMember = "Kode_MK"
        TXTKodeMK.Text = ListBox1.Text
        ListBox1.Text = ""
    End Sub

    Sub TampilNIP()
        ListBox1.Visible = False
        ADP = New OleDbDataAdapter("SELECT NIP FROM Master_Dosen WHERE Nama_Dosen='" & CmbDosen.Text & "'", cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Nama")
        Dim Dta As DataTable = DS.Tables("Nama")
        ListBox1.DataSource = Dta
        ListBox1.DisplayMember = "NIP"
        TxtNIP.Text = ListBox1.Text
        ListBox1.Text = ""
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        Try
            cmdInsert.CommandText = "INSERT INTO Jadwal(Semester, Tahun_Akademik, Hari, Jam, Kelas, Kode_MK, NIP, Ruang, Id) VALUES(@Semester, @ThnAkad, @Hari, @Jam, @Kelas, @KodeMK, @NIP, @Ruang, @Id)"

            cmdInsert.Parameters.AddWithValue("@Semester", Me.CmbSemester.Text)
            cmdInsert.Parameters.AddWithValue("@ThnAkad", Me.TxtTA.Text)
            cmdInsert.Parameters.AddWithValue("@Hari", Me.CmbHari.Text)
            cmdInsert.Parameters.AddWithValue("@Jam", Me.TxtJam.Text)
            cmdInsert.Parameters.AddWithValue("@Kelas", Me.CmbKelas.Text)
            cmdInsert.Parameters.AddWithValue("@KodeMK", Me.TXTKodeMK.Text)
            cmdInsert.Parameters.AddWithValue("@NIP", Me.TxtNIP.Text)
            cmdInsert.Parameters.AddWithValue("@Ruang", Me.CmbRuang.Text)
            cmdInsert.Parameters.AddWithValue("@Id", Me.Label14.Text)

            cmdInsert.CommandType = CommandType.Text
            cmdInsert.Connection = cnnOLEDB
            cmdInsert.ExecuteNonQuery()
            MsgBox("Record Inserted")
            TampilData()
            cmdInsert.Dispose()
            TxtJam.Focus()
            Bersih()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        cmdInsert.Dispose()
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        Try
            cmdUpdate.Parameters.AddWithValue("@Semester", Me.CmbSemester.Text)
            cmdUpdate.Parameters.AddWithValue("@ThnAkad", Me.TxtTA.Text)
            cmdUpdate.Parameters.AddWithValue("@Hari", Me.CmbHari.Text)
            cmdUpdate.Parameters.AddWithValue("@Jam", Me.TxtJam.Text)
            cmdUpdate.Parameters.AddWithValue("@Kelas", Me.CmbKelas.Text)
            cmdUpdate.Parameters.AddWithValue("@KodeMK", Me.TXTKodeMK.Text)
            cmdUpdate.Parameters.AddWithValue("@NIP", Me.TxtNIP.Text)
            cmdUpdate.Parameters.AddWithValue("@Ruang", Me.CmbRuang.Text)
            cmdUpdate.Parameters.AddWithValue("@Id", Me.Label14.Text)

            cmdUpdate.CommandText = "UPDATE Jadwal SET Semester=@Semester, Tahun_Akademik=@ThnAkad, Hari=@Hari, Jam=@Jam, Kelas=@Kelas, Kode_MK=@KodeMK, NIP=@NIP, Ruang=@Ruang, Id=@Id WHERE Id=@id"

            cmdUpdate.CommandType = CommandType.Text
            cmdUpdate.Connection = cnnOLEDB
            cmdUpdate.ExecuteNonQuery()
            MsgBox("Record Updated")

            TampilData()
            cmdUpdate.Dispose()
            Bersih()
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        cmdUpdate.Dispose()
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Dim konfirmasi
        konfirmasi = MsgBox("Data Akan di Hapus ?", vbOKCancel, "Hapus Data")

        If konfirmasi = vbOK Then
            Try
                cmdDelete.CommandText = "DELETE FROM Jadwal WHERE Id=@id"

                cmdDelete.Parameters.AddWithValue("@id", Me.Label14.Text)
                cmdDelete.CommandType = CommandType.Text
                cmdDelete.Connection = cnnOLEDB
                cmdDelete.ExecuteNonQuery()
                MsgBox("Record Deleted")
                TampilDataJadwalSesuaiHaridanKelas()
                TampilData()
                Bersih()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        ElseIf konfirmasi = vbCancel Then
            MsgBox("Data tidak jadi dihapus", vbInformation, "Batal")
        End If
    End Sub

    Private Sub CmbDosen_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbDosen.SelectedIndexChanged
        TampilNIP()
        tampilcomboboxKode_MK()
    End Sub

    Private Sub CmbMK_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbMK.SelectedIndexChanged
        TampilKdMK()
    End Sub

    Sub IsiNIP()
        Dim query As String
        query = "SELECT Nama_Dosen FROM Master_Dosen WHERE NIP = '" & TxtNIP.Text & "'"
        ADP = New OleDbDataAdapter(query, cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Nama_Dosen")
        For i = 0 To DS.Tables("Nama_Dosen").Rows.Count - 1
            CmbDosen.Text = (DS.Tables("Nama_Dosen").Rows(i).Item("Nama_Dosen").ToString)
        Next
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim semester As Object = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Dim tahunakademik As Object = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        Dim Hari As Object = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        Dim Jam As Object = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        Dim Kelas As Object = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        Dim Ruang As Object = DataGridView1.Rows(e.RowIndex).Cells(7).Value
        Dim KodeMK As Object = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        Dim NIP As Object = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        Dim Id As Object = DataGridView1.Rows(e.RowIndex).Cells(8).Value
        TampilData()
        CmbSemester.Text = CType(semester, String)
        TxtTA.Text = CType(tahunakademik, String)
        CmbHari.Text = CType(Hari, String)
        TxtJam.Text = CType(Jam, String)
        TXTKodeMK.Text = CType(KodeMK, String)
        TxtNIP.Text = CType(NIP, String)
        CmbRuang.Text = CType(Ruang, String)
        CmbKelas.Text = CType(Kelas, String)
        Label14.Text = CType(Id, String)
        tampilNama_MK()
    End Sub

    Private Sub TxtNIP_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNIP.TextChanged
        IsiNIP()
    End Sub

    Sub autonumber()
        Dim reader As OleDbDataReader
        cmdOLEDB = New OleDbCommand("SELECT * FROM Jadwal ORDER BY Id DESC", cnnOLEDB)
        reader = cmdOLEDB.ExecuteReader
        reader.Read()
        If Not reader.HasRows Then
            Label14.Text = "JD" + "0001"

        Else
            Label14.Text = Val(Microsoft.VisualBasic.Mid(reader.Item("Id").ToString, 4, 3)) + 1
            If Len(Label14.Text) = 1 Then
                Label14.Text = "JD000" & Label14.Text & ""
            ElseIf Len(Label14.Text) = 2 Then
                Label14.Text = "JD00" & Label14.Text & ""
            ElseIf Len(Label14.Text) = 3 Then
                Label14.Text = "JD0" & Label14.Text & ""
            End If
        End If
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Bersih()
    End Sub

    Private Sub CmbKelas2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbKelas2.SelectedIndexChanged
        TampilDataJadwalSesuaiHaridanKelas()
    End Sub

    Private Sub TXTKodeMK_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TXTKodeMK.TextChanged
        tampilNama_MK()
    End Sub
End Class