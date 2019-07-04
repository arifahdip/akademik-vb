Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Public Class Form8
    Dim cnnOLEDB As New OleDbConnection
    Dim cmdInsert As New OleDbCommand
    Dim cmdUpdate As New OleDbCommand
    Dim cmdDelete As New OleDbCommand
    Dim strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _
        System.Environment.CurrentDirectory & "\Akademik.accdb"

    Public ADP As OleDbDataAdapter
    Public DS As New DataSet

    Private Sub Form8_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cnnOLEDB.ConnectionString = strConnectionString
        cnnOLEDB.Open()
        IsiComboBS()
        TampilData()
        ListBox1.Visible = False
        ButtonEnable()
        MskNIM.Mask = "#,##,##,#,##"
    End Sub

    Sub ButtonEnable()
        BtnSimpan.Enabled = True
        BtnUpdate.Enabled = False
        BtnDelete.Enabled = False
        BtnRefresh.Enabled = True
    End Sub

    Sub ButtonDisable()
        BtnSimpan.Enabled = False
        BtnUpdate.Enabled = True
        BtnDelete.Enabled = True
        BtnRefresh.Enabled = False
    End Sub

    Sub TampilData()
        ADP = New OleDbDataAdapter("SELECT * FROM Beasiswa ORDER BY NIM", cnnOLEDB)
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

    Sub IsiNim()
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

    Sub IsiComboBS()
        Dim query As String
        query = "SELECT Kode_BS From Master_Beasiswa"
        ADP = New OleDbDataAdapter(query, cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Combo")
        For i = 0 To DS.Tables("Combo").Rows.Count - 1
            CmbBeasiswa.Items.Add(DS.Tables("Combo").Rows(i).Item("Kode_BS").ToString)
        Next
    End Sub

    Sub Bersih()
        MskNIM.Text = ""
        TxtNama.Text = ""
        TxtKelas.Text = ""
        TxtTA.Text = ""
        CmbBeasiswa.SelectedText = ""
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

    Private Sub MskNIM_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles MskNIM.TextChanged
        If MskNIM.Text.Length() = 12 Then
            IsiNim()
            IsiKelas()
        End If
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        If MskNIM.Text <> "" And TxtNama.Text <> "" And TxtKelas.Text <> "" And TxtTA.Text <> "" And CmbBeasiswa.Text <> "" Then
            Try
                cmdInsert.CommandText = "INSERT INTO Beasiswa (NIM, Kode_BS, Tahun_Akademik) VALUES (@NIM, @Kode_BS, @TA)"
                cmdInsert.Parameters.AddWithValue("@NIM", Me.MskNIM.Text)
                cmdInsert.Parameters.AddWithValue("@Kode_BS", Me.CmbBeasiswa.Text)
                cmdInsert.Parameters.AddWithValue("@TA", Me.TxtTA.Text)

                cmdInsert.CommandType = CommandType.Text
                cmdInsert.Connection = cnnOLEDB
                cmdInsert.ExecuteNonQuery()
                MsgBox("Record inserted")
                TampilData()
                Bersih()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            MsgBox("Masukkan data secara lengkap")
        End If
        cmdInsert.Dispose()
        Bersih()
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        If MskNIM.Text <> "" And TxtNama.Text <> "" And TxtKelas.Text <> "" And TxtTA.Text <> "" And CmbBeasiswa.Text <> "" Then
            Try
                cmdInsert.CommandText = "UPDATE Beasiswa SET NIM=@NIM , Kode_BS=@Kode_BS , Tahun_Akademik=@TA WHERE NIM=@NIM"
                cmdInsert.Parameters.AddWithValue("@NIM", Me.MskNIM.Text)
                cmdInsert.Parameters.AddWithValue("@Kode_BS", Me.CmbBeasiswa.Text)
                cmdInsert.Parameters.AddWithValue("@TA", Me.TxtTA.Text)

                cmdInsert.CommandType = CommandType.Text
                cmdInsert.Connection = cnnOLEDB
                cmdInsert.ExecuteNonQuery()
                MsgBox("Record inserted")
                TampilData()
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            MsgBox("Masukan Data Secara Lengkap")
        End If
        cmdInsert.Dispose()
        Bersih()
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            cmdDelete.CommandText = "DELETE FROM Beasiswa WHERE NIM=@NIM"
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

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Bersih()
        ButtonEnable()
        ListBox1.Visible = False
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        GetData(e)
        ButtonDisable()
        IsiBeasiswa()
        IsiNim()
        IsiKelas()
    End Sub

    Sub GetData(ByVal e)
        Dim NIM As Object = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Dim Beasiswa As Object = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        Dim TA As Object = DataGridView1.Rows(e.RowIndex).Cells(2).Value

        MskNIM.Text = CType(NIM, String)
        CmbBeasiswa.Text = CType(Beasiswa, String)
        TxtTA.Text = CType(TA, String)
    End Sub
    Sub IsiBeasiswa()
        Dim query As String
        query = "SELECT Nama_BS FROM Master_Beasiswa WHERE Kode_BS = '" & CmbBeasiswa.Text & "'"
        ADP = New OleDbDataAdapter(query, cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Nama_BS")
        For i = 0 To DS.Tables("Nama_BS").Rows.Count - 1
            TxtBeasiswa.Text = (DS.Tables("Nama_BS").Rows(i).Item("Nama_BS").ToString)
        Next
        ListBox1.Visible = False
    End Sub

    Private Sub CmbBeasiswa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbBeasiswa.SelectedIndexChanged
        IsiBeasiswa()
    End Sub
End Class