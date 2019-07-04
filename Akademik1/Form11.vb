Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Public Class Form11
    Dim cnnOLEDB As New OleDbConnection
    Dim cmdInsert As New OleDbCommand
    Dim cmdUpdate As New OleDbCommand
    Dim cmdDelete As New OleDbCommand
    Dim cmdOLEDB As New OleDbCommand
    Dim strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _
        System.Environment.CurrentDirectory & "\Akademik.accdb"

    Public ADP As OleDbDataAdapter
    Public DR As OleDbDataReader
    Public DS As New DataSet
    Dim chk As New DataGridViewCheckBoxColumn()

    Private Sub Form11_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cnnOLEDB.ConnectionString = strConnectionString
        cnnOLEDB.Open()

        CmbKelas.DropDownStyle = ComboBoxStyle.DropDownList
        CmbMK.DropDownStyle = ComboBoxStyle.DropDownList
        CmbSemester.DropDownStyle = ComboBoxStyle.DropDownList

        ButtonDisable()
        GetDataKelas()
        GetDataMataKuliah()
        showKodeMataKuliah()
        TxtKodeMK.Enabled = False
    End Sub

    Sub ButtonEnable()
        BtnSimpan.Enabled = True
        BtnDelete.Enabled = True
        BtnUpdate.Enabled = True
    End Sub

    Sub ButtonDisable()
        BtnSimpan.Enabled = False
        BtnDelete.Enabled = False
        BtnUpdate.Enabled = False
    End Sub
    Sub TampilData()
        Dim Nilai As Integer
        Dim NilaiHuruf As String = ""
        Dim Query As String
        Query = "SELECT m.NIM, m.Nama_Mhs FROM Master_Mahasiswa m LEFT JOIN Kelas k ON m.NIM = k.NIM WHERE k.Kelas='" & CmbKelas.Text & "' ORDER BY m.NIM"
        ADP = New OleDbDataAdapter(Query, cnnOLEDB)
        Dim DS1 As New DataSet
        ADP.Fill(DS1, "Mhs")
        DataGridView1.Columns.Clear()
        Dim Check As New DataGridViewCheckBoxColumn()
        Check.HeaderText = ""
        Check.Name = "ChkCheck"
        DataGridView1.Columns.Add(Check)
        DataGridView1.DataSource = DS1.Tables("Mhs")
        DataGridView1.Columns("NIM").ReadOnly = True
        DataGridView1.Columns("Nama_Mhs").ReadOnly = True
        Dim KolomKodeMK As New DataGridViewTextBoxColumn
        Dim KolomNamaMK As New DataGridViewTextBoxColumn
        Dim Nilai1 As New DataGridViewTextBoxColumn
        Dim Nilai2 As New DataGridViewTextBoxColumn
        Dim Nilai3 As New DataGridViewTextBoxColumn
        KolomKodeMK.HeaderText = "Kode_MK"
        KolomNamaMK.HeaderText = "Nama_MK"
        Nilai1.HeaderText = "N1"
        Nilai2.HeaderText = "N2"
        Nilai3.HeaderText = "N3"
        KolomKodeMK.ReadOnly = True
        KolomNamaMK.ReadOnly = True
        DataGridView1.Columns.Add(KolomKodeMK)
        DataGridView1.Columns.Add(KolomNamaMK)
        DataGridView1.Columns.Add(Nilai1)
        DataGridView1.Columns.Add(Nilai2)
        DataGridView1.Columns.Add(Nilai3)

        Query = "SELECT * FROM Master_MK WHERE Nama_MK='" & CmbMK.Text & "'"
        ADP = New OleDbDataAdapter(Query, cnnOLEDB)
        ADP.Fill(DS1, "MK")
        If DS1.Tables("Mhs").Rows.Count >= 1 Then
            For i = 0 To DS1.Tables("Mhs").Rows.Count - 1
                DataGridView1.Rows(i).Cells(3).Value = DS1.Tables("MK").Rows(0)("Kode_MK").ToString
                DataGridView1.Rows(i).Cells(4).Value = DS1.Tables("MK").Rows(0)("Nama_MK").ToString
            Next
        End If

        Dim TxtColl As New DataGridViewTextBoxColumn
        Dim TxtHuruf As New DataGridViewTextBoxColumn
        TxtColl.HeaderText = "Nilai Akhir"
        TxtHuruf.HeaderText = "Nilai Huruf"
        TxtColl.ReadOnly = True
        TxtHuruf.ReadOnly = True
        DataGridView1.Columns.Add(TxtColl)
        DataGridView1.Columns.Add(TxtHuruf)
        Dim Coll As New DataGridTextBox()
        Dim Txt As New DataGridTextBox()

        If DataGridView1.RowCount >= 1 Then
            Try
                For i = 0 To DataGridView1.RowCount - 1
                    Query = "SELECT N1, N2, N3 FROM Nilai WHERE Semester='" & CmbSemester.Text & "' AND NIM='" & DS1.Tables("Mhs").Rows(i)("NIM").ToString & "' " &
                    "AND Kode_MK='" & DS1.Tables("MK").Rows(0)("Kode_MK").ToString & "' AND Tahun_Akademik='" & TxtTA.Text & "'"
                    ADP = New OleDbDataAdapter(Query, cnnOLEDB)
                    Dim DS2 As New DataSet
                    DS2.Clear()
                    ADP.Fill(DS2, "Nilai")
                    If DS2.Tables("Nilai").Rows.Count > 0 Then
                        Nilai = (Integer.Parse(DS2.Tables("Nilai").Rows(0).Item("N1").ToString) +
                        Integer.Parse(DS2.Tables("Nilai").Rows(0).Item("N2").ToString) +
                        Integer.Parse(DS2.Tables("Nilai").Rows(0).Item("N3").ToString)) / 3
                        If Nilai <= 100 And Nilai > 80 Then
                            NilaiHuruf = "A"
                        ElseIf Nilai <= 80 And Nilai > 60 Then
                            NilaiHuruf = "B"
                        ElseIf Nilai <= 60 And Nilai > 40 Then
                            NilaiHuruf = "C"
                        ElseIf Nilai <= 40 And Nilai > 20 Then
                            NilaiHuruf = "D"
                        ElseIf Nilai <= 20 And Nilai > 0 Then
                            NilaiHuruf = "E"
                        End If
                        Coll.Text = Nilai.ToString
                        Txt.Text = NilaiHuruf
                        DataGridView1.Rows(i).Cells(5).Value = DS2.Tables("Nilai").Rows(0)("N1")
                        DataGridView1.Rows(i).Cells(6).Value = DS2.Tables("Nilai").Rows(0)("N2")
                        DataGridView1.Rows(i).Cells(7).Value = DS2.Tables("Nilai").Rows(0)("N3")
                        DataGridView1.Rows(i).Cells(8).Value = Coll.Text
                        DataGridView1.Rows(i).Cells(9).Value = Txt.Text

                    End If
                Next
            Catch ex As Exception
                'MsgBox(ex.ToString)
            End Try
        End If
        ButtonEnable()
    End Sub


    Sub Bersih()
        TxtTA.Text = ""
        CmbKelas.SelectedIndex = 0
        CmbMK.SelectedIndex = 0
        CmbSemester.SelectedIndex = 0

        DataGridView1.DataSource = ""

        ButtonDisable()
    End Sub

    Sub showKodeMataKuliah()
        If CmbMK.Text = "" Then
            TxtKodeMK.Text = ""
        Else
            Dim query As String

            query = " Select Kode_MK FROM Master_MK WHERE Nama_MK = '" & CmbMK.Text & "'"
            ADP = New OleDbDataAdapter(query, cnnOLEDB)
            DS = New DataSet
            ADP.Fill(DS, "Kode_MK")
            For i = 0 To DS.Tables("Kode_MK").Rows.Count - 1
                TxtKodeMK.Text = (DS.Tables("Kode_MK").Rows(i).Item("Kode_MK").ToString)
            Next
        End If
    End Sub

    Sub GetDataKelas()
        Dim Kelas As String
        Kelas = "SELECT DISTINCT Kelas FROM Kelas ORDER BY Kelas"
        ADP = New OleDbDataAdapter(Kelas, cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Kelas")
        CmbKelas.Items.Clear()
        For i = 0 To DS.Tables("Kelas").Rows.Count - 1
            CmbKelas.Items.Add(DS.Tables("Kelas").Rows(i).Item("Kelas").ToString)
        Next
        If CmbKelas.Items.Count <> 0 Then
            CmbKelas.SelectedIndex = 0
        End If
    End Sub

    Sub GetDataMataKuliah()
        Dim MK As String
        MK = "SELECT Nama_MK FROM Master_MK ORDER BY Nama_MK"
        ADP = New OleDbDataAdapter(MK, cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "MK")
        For i = 0 To DS.Tables("MK").Rows.Count - 1
            CmbMK.Items.Add(DS.Tables("MK").Rows(i).Item("Nama_MK").ToString)
        Next
        If CmbMK.Items.Count <> 0 Then
            CmbMK.SelectedIndex = 0
        End If
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        TampilData()
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Dim x As Integer = DataGridView1.RowCount - 1
        If CheckBox1.Checked Then
            Do
                x = x - 1
                DataGridView1.Rows(x).Cells(0).Value = True
            Loop Until x = 0
        Else
            Do
                x = x - 1
                DataGridView1.Rows(x).Cells(0).Value = False
            Loop Until x = 0
        End If
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        For i = 0 To DataGridView1.RowCount - 1
            If CBool(DataGridView1.Rows(i).Cells(0).Value) = True Then
                Try
                    Dim NIM As String = DataGridView1.Rows(i).Cells(1).Value
                    Dim KodeMK As String = DataGridView1.Rows(i).Cells(3).Value
                    Dim Sem As String = CmbSemester.Text
                    Dim TA As String = TxtTA.Text

                    cmdDelete.Parameters.AddWithValue("@NIM", NIM)
                    cmdDelete.Parameters.AddWithValue("@Kode", KodeMK)
                    cmdDelete.Parameters.AddWithValue("@Sem", Sem)
                    cmdDelete.Parameters.AddWithValue("@TA", TA)

                    cmdDelete.CommandType = CommandType.Text
                    cmdDelete.Connection = cnnOLEDB


                    cmdDelete.CommandText = "DELETE FROM Nilai WHERE NIM=@NIM AND Kode_MK=@Kode AND Semester=@Sem AND Tahun_Akademik=@TA"
                    cmdDelete.ExecuteNonQuery()
                    cmdDelete.Dispose()
                    MsgBox("Berhasil dihapus!", vbInformation, "Informasi")
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If
        Next
        TampilData()
    End Sub

    Private Sub BtnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRefresh.Click
        Bersih()
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        For i = 0 To DataGridView1.RowCount - 1
            If CBool(DataGridView1.Rows(i).Cells(0).Value) = True Then
                Try
                    Dim NIM As String = DataGridView1.Rows(i).Cells(1).Value
                    Dim KodeMK As String = DataGridView1.Rows(i).Cells(3).Value
                    Dim Sem As String = CmbSemester.Text
                    Dim TA As String = TxtTA.Text
                    Dim Kelas As String = CmbKelas.Text
                    Dim N1 As Integer = DataGridView1.Rows(i).Cells(5).Value
                    Dim N2 As Integer = DataGridView1.Rows(i).Cells(6).Value
                    Dim N3 As Integer = DataGridView1.Rows(i).Cells(7).Value
                    Dim Nilai As Integer
                    Nilai = (N1 + N2 + N3) / 3

                    Dim NilaiHuruf As String = ""
                    If Nilai <= 100 And Nilai > 80 Then
                        NilaiHuruf = "A"
                    ElseIf Nilai <= 80 And Nilai > 60 Then
                        NilaiHuruf = "B"
                    ElseIf Nilai <= 60 And Nilai > 40 Then
                        NilaiHuruf = "C"
                    ElseIf Nilai <= 40 And Nilai > 20 Then
                        NilaiHuruf = "D"
                    ElseIf Nilai <= 20 And Nilai > 0 Then
                        NilaiHuruf = "E"
                    End If
                    cmdInsert.Parameters.AddWithValue("@NIM", NIM)
                    cmdInsert.Parameters.AddWithValue("@Kode", KodeMK)
                    cmdInsert.Parameters.AddWithValue("@Sem", Sem)
                    cmdInsert.Parameters.AddWithValue("@TA", TA)
                    cmdInsert.Parameters.AddWithValue("@Kelas", Kelas)
                    cmdInsert.Parameters.AddWithValue("@N1", N1)
                    cmdInsert.Parameters.AddWithValue("@N2", N2)
                    cmdInsert.Parameters.AddWithValue("@N3", N3)
                    cmdInsert.Parameters.AddWithValue("@Nilai", Nilai)
                    cmdInsert.Parameters.AddWithValue("@NH", NilaiHuruf)

                    cmdInsert.CommandType = CommandType.Text
                    cmdInsert.Connection = cnnOLEDB

                    cmdInsert.CommandText = "INSERT INTO Nilai(NIM, Kode_MK, Semester, Tahun_Akademik, Kelas, N1, N2, N3, NA, NH) VALUES(@NIM, @Kode, @Sem, @TA, @Kelas, @N1, @N2, @N3, @Nilai, @NH)"
                    cmdInsert.ExecuteNonQuery()
                    cmdInsert.Dispose()
                    MsgBox("Berhasil disimpan!", vbInformation, "Informasi")
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If
        Next
        TampilData()
    End Sub


    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        For i = 0 To DataGridView1.RowCount - 1
            If CBool(DataGridView1.Rows(i).Cells(0).Value) = True Then
                Dim N1 As String = DataGridView1.Rows(i).Cells(5).Value
                Dim N2 As String = DataGridView1.Rows(i).Cells(6).Value
                Dim N3 As String = DataGridView1.Rows(i).Cells(7).Value

                Dim Nilai1 As Integer = N1
                Dim Nilai2 As Integer = N2
                Dim Nilai3 As Integer = N3
               
                Dim Nilai As Integer
                Nilai = (Nilai1 + Nilai2 + Nilai3) / 3

                Dim NilaiHuruf As String = ""
                If Nilai <= 100 And Nilai > 80 Then
                    NilaiHuruf = "A"
                ElseIf Nilai <= 80 And Nilai > 60 Then
                    NilaiHuruf = "B"
                ElseIf Nilai <= 60 And Nilai > 40 Then
                    NilaiHuruf = "C"
                ElseIf Nilai <= 40 And Nilai > 20 Then
                    NilaiHuruf = "D"
                ElseIf Nilai <= 20 And Nilai > 0 Then
                    NilaiHuruf = "E"
                End If


                Dim NIM As String = DataGridView1.Rows(i).Cells(1).Value
                Dim KodeMK As String = DataGridView1.Rows(i).Cells(3).Value
                Dim Sem As String = CmbSemester.Text
                Dim TA As String = TxtTA.Text

                cmdUpdate.Parameters.AddWithValue("@N1", N1)
                cmdUpdate.Parameters.AddWithValue("@N2", N2)
                cmdUpdate.Parameters.AddWithValue("@N3", N3)
                cmdUpdate.Parameters.AddWithValue("@NA", Nilai)
                cmdUpdate.Parameters.AddWithValue("@NH", NilaiHuruf)
                cmdUpdate.Parameters.AddWithValue("@NIM", NIM)
                cmdUpdate.Parameters.AddWithValue("@Kode", KodeMK)
                cmdUpdate.Parameters.AddWithValue("@Sem", Sem)
                cmdUpdate.Parameters.AddWithValue("@TA", TA)


                cmdUpdate.CommandType = CommandType.Text
                cmdUpdate.Connection = cnnOLEDB

                If N1 <> "" And N2 <> "" And N3 <> "" Then
                    Try
                        cmdUpdate.CommandText = "UPDATE Nilai SET N1=@N1, N2=@N2, N3=@N3, NA=@NA, NH=@NH " &
                            "WHERE NIM=@NIM AND Kode_MK=@Kode AND Semester=@Sem AND Tahun_Akademik=@TA"
                        cmdUpdate.ExecuteNonQuery()
                        cmdUpdate.Dispose()
                        MsgBox("Berhasil diupdate!", vbInformation, "Informasi")
                    Catch ex As Exception
                        MsgBox(ex.ToString)
                    End Try
                Else
                    MsgBox("Masukkan data secara lengkap!", vbInformation, "Informasi")
                End If
            End If
        Next
        TampilData()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        TampilData()
    End Sub

    Private Sub CmbMK_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbMK.SelectedIndexChanged
        showKodeMataKuliah()
    End Sub
End Class