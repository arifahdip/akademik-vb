Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Public Class Form7
    Dim cnnOLEDB As New OleDbConnection
    Dim cmdInsert As New OleDbCommand
    Dim cmdUpdate As New OleDbCommand
    Dim cmdDelete As New OleDbCommand
    Dim strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _
        System.Environment.CurrentDirectory & "\Akademik.accdb"

    Public ADP As OleDbDataAdapter
    Public DS As New DataSet

    Private Sub Form7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        ListBox1.Visible = False
    End Sub

    Private Sub Form7_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cnnOLEDB.ConnectionString = strConnectionString
        cnnOLEDB.Open()
        TampilData()
        ListBox1.Visible = False
        ButtonEnable()
        MskNIM.Mask = "#,##,##,#,##"
        Label12.Text = ""
    End Sub

    Sub ButtonEnable()
        BtnSimpan.Enabled = True
        BtnDelete.Enabled = False
        BtnUpdate.Enabled = False
        BtnRefresh.Enabled = True
    End Sub
    Sub ButtonDisable()
        BtnSimpan.Enabled = False
        BtnUpdate.Enabled = True
        BtnDelete.Enabled = True
        BtnRefresh.Enabled = False
    End Sub
    Sub TampilData()
        ADP = New OleDbDataAdapter("SELECT * FROM Kehadiran ORDER BY NIM", cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Tabel1")
        DataGridView1.DataSource = DS.Tables("Tabel1")
    End Sub
    Sub IsiList()
        Dim query As String
        query = "SELECT Nama_Mhs FROM Master_Mahasiswa WHERE Nama_Mhs LIKE '" & TxtNama.Text & "%' ORDER BY NIM"
        ADP = New OleDbDataAdapter(query, cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "List")
        ListBox1.Items.Clear()
        For i = 0 To DS.Tables("List").Rows.Count - 1
            ListBox1.Items.Add(DS.Tables("List").Rows(i).Item("Nama_Mhs").ToString)
        Next
    End Sub
    Sub IsiNIM()
        Dim query As String
        query = "SELECT Nama_Mhs FROM Master_Mahasiswa WHERE NIM = '" & MskNIM.Text & "'"
        ADP = New OleDbDataAdapter(query, cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "NIM")
        For i = 0 To DS.Tables("NIM").Rows.Count - 1
            TxtNama.Text = (DS.Tables("NIM").Rows(i).Item("Nama_Mhs").ToString)
        Next
        ListBox1.Visible = False
    End Sub
    Sub IsiKelas()
        Dim query As String
        query = "SELECT Kelas FROM Kelas WHERE NIM = '" & MskNIM.Text & "'"
        ADP = New OleDbDataAdapter(query, cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Kelas")
        For i = 0 To DS.Tables("Kelas").Rows.Count - 1
            TxtKelas.Text = (DS.Tables("Kelas").Rows(i).Item("Kelas").ToString)
        Next
        ListBox1.Visible = False
    End Sub
    Sub Bersih()
        MskNIM.Text = ""
        TxtNama.Text = ""
        TxtKelas.Text = ""
        Tgl.Text = ""
        CmbSemester.Text = ""
        TxtTahunAkd.Text = ""
        TxtIjin.Text = ""
        TxtSakit.Text = ""
        TxtAlpa.Text = ""
        Label12.Text = ""
    End Sub

    Private Sub TxtNama_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TxtNama.TextChanged
        ListBox1.Location = New Point(TxtNama.Location.X, TxtNama.Location.Y + 20)
        ListBox1.BringToFront()
        ListBox1.Visible = True
        IsiList()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        TxtNama.Text = ListBox1.SelectedItem
        ListBox1.Visible = False
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        If MskNIM.Text <> "" And TxtNama.Text <> "" And TxtKelas.Text <> "" And Tgl.Text <> "" And CmbSemester.Text <> "" And TxtTahunAkd.Text <> "" And TxtIjin.Text <> "" And TxtSakit.Text <> "" And TxtAlpa.Text <> "" Then
            Try
                cmdInsert.CommandText = "INSERT INTO Kehadiran (NIM, Semester, Tahun_Akademik, Tanggal, Ijin, Sakit, Alpa) VALUES(@NIM, @Semester, @TA, @Tgl, @Ijin, @Sakit, @Alpa)"
                cmdInsert.Parameters.AddWithValue("@NIM", Me.MskNIM.Text)
                cmdInsert.Parameters.AddWithValue("@Semester", Me.CmbSemester.Text)
                cmdInsert.Parameters.AddWithValue("@TA", Me.TxtTahunAkd.Text)
                cmdInsert.Parameters.AddWithValue("@Tgl", Me.Tgl.Value.Date.ToString)
                cmdInsert.Parameters.AddWithValue("@Ijin", Me.TxtIjin.Text)
                cmdInsert.Parameters.AddWithValue("@Sakit", Me.TxtSakit.Text)
                cmdInsert.Parameters.AddWithValue("@Alpa", Me.TxtAlpa.Text)

                cmdInsert.CommandType = CommandType.Text
                cmdInsert.Connection = cnnOLEDB
                cmdInsert.ExecuteNonQuery()
                MsgBox("Data Berhasil disimpan")
                TampilData()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            MsgBox("Masukan Data Secara Lengkap!")
        End If
        cmdInsert.Dispose()
        Bersih()
    End Sub

    Private Sub MskNIM_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MskNIM.TextChanged
        If MskNIM.Text.Length() = 12 Then
            IsiNIM()
            IsiKelas()
        End If
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        If MskNIM.Text <> "" And TxtNama.Text <> "" And TxtKelas.Text <> "" And Tgl.Text <> "" And CmbSemester.Text <> "" And TxtTahunAkd.Text <> "" And TxtIjin.Text <> "" And TxtSakit.Text <> "" And TxtAlpa.Text <> "" Then

            Try
                cmdInsert.CommandText = "UPDATE Kehadiran SET NIM=@NIM, Semester=@Semester, Tahun_Akademik=@TA, Tanggal=@Tgl, Ijin=@Ijin, Sakit=@Sakit, Alpa=@Alpa WHERE ID=@ID AND NIM=@NIM"
                cmdInsert.Parameters.AddWithValue("@NIM", Me.MskNIM.Text)
                cmdInsert.Parameters.AddWithValue("@Semester", Me.CmbSemester.Text)
                cmdInsert.Parameters.AddWithValue("@TA", Me.TxtTahunAkd.Text)
                cmdInsert.Parameters.AddWithValue("@Tgl", Me.Tgl.Value.Date.ToString)
                cmdInsert.Parameters.AddWithValue("@Ijin", Me.TxtIjin.Text)
                cmdInsert.Parameters.AddWithValue("@Sakit", Me.TxtSakit.Text)
                cmdInsert.Parameters.AddWithValue("@Alpa", Me.TxtAlpa.Text)
                cmdInsert.Parameters.AddWithValue("@ID", Me.Label12.Text)

                cmdInsert.CommandType = CommandType.Text
                cmdInsert.Connection = cnnOLEDB
                cmdInsert.ExecuteNonQuery()
                MsgBox("Data berhasil diupdate")
                TampilData()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            MsgBox("Masukan data secara lengkap")
        End If
        cmdInsert.Dispose()
        Bersih()
        ButtonEnable()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        GetData(e)
        ButtonDisable()
    End Sub

    Sub GetData(ByVal e)
        Dim NIM As Object = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Dim Semester As Object = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        Dim TA As Object = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        Dim Tgl1 As Object = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        Dim Ijin As Object = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        Dim Sakit As Object = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        Dim Alpa As Object = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        Dim ID As Object = DataGridView1.Rows(e.RowIndex).Cells(7).Value

        MskNIM.Text = CType(NIM, String)
        CmbSemester.Text = CType(Semester, String)
        TxtTahunAkd.Text = CType(TA, String)
        Tgl.Text = CType(Tgl1, Date)
        TxtIjin.Text = CType(Ijin, String)
        TxtSakit.Text = CType(Sakit, String)
        TxtAlpa.Text = CType(Alpa, String)
        Label12.Text = CType(ID, String)
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Bersih()
        ButtonEnable()
        ListBox1.Visible = False
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            cmdDelete.CommandText = "DELETE FROM Kehadiran WHERE ID=@ID AND NIM=@NIM"
            cmdDelete.Parameters.AddWithValue("@ID", Me.Label12.Text)
            cmdDelete.Parameters.AddWithValue("@NIM", Me.MskNIM.Text)
            cmdDelete.CommandType = CommandType.Text
            cmdDelete.Connection = cnnOLEDB
            cmdDelete.ExecuteNonQuery()
            MsgBox("Record Deleted")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        cmdDelete.Dispose()
        TampilData()
        Bersih()
    End Sub
End Class