Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Public Class Form6
    Dim cnnOLEDB As New OleDbConnection
    Dim cmdInsert As New OleDbCommand
    Dim cmdUpdate As New OleDbCommand
    Dim cmdDelete As New OleDbCommand
    Dim strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _
        System.Environment.CurrentDirectory & "\Akademik.accdb"

    Public ADP As OleDbDataAdapter
    Public DS As New DataSet
    Public ADP2 As OleDbDataAdapter
    Public DS2 As New DataSet

    Private Sub Form6_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cnnOLEDB.ConnectionString = strConnectionString
        cnnOLEDB.Open()
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False
        txtKelasSebelum.Visible = False
        txtKelasBaru.Visible = False
        MaskedTextBox1.Visible = False
        BtnEditOK1.Visible = False
        BtnEditOK2.Visible = False

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        Label3.Visible = True
        Label4.Visible = True
        Label5.Visible = False
        txtKelasSebelum.Visible = True
        txtKelasBaru.Visible = True
        MaskedTextBox1.Visible = False
        BtnEditOK1.Visible = True
        BtnEditOK2.Visible = False
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = True
        txtKelasSebelum.Visible = False
        txtKelasBaru.Visible = False
        MaskedTextBox1.Visible = True
        BtnEditOK1.Visible = False
        BtnEditOK2.Visible = True
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click
        Dim query As String
        query = "SELECT m.NIM, m.Nama_Mhs, k.Kelas FROM Master_Mahasiswa m LEFT JOIN Kelas k ON m.NIM = k.NIM WHERE k.Kelas IS NULL AND MID(m.NIM,6,2)='" & TxtTahunMasuk.Text.Substring(2, 2) & "' ORDER BY m.NIM"
        ADP = New OleDbDataAdapter(query, cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Tabel1")

        DataGridView1.Columns.Clear()
        Dim chk As New DataGridViewCheckBoxColumn() With {.Width = 25}
        DataGridView1.Columns.Add(chk)
        chk.HeaderText = ""
        chk.Name = "chk"


        DataGridView1.DataSource = DS.Tables("Tabel1")

        Dim cmb As New DataGridViewComboBoxColumn
        cmb.HeaderText = "Status"
        cmb.Name = "cmbstatus"
        cmb.MaxDropDownItems = 3
        cmb.Items.Add("Aktif")
        cmb.Items.Add("Cuti")
        cmb.Items.Add("DO")

        DataGridView1.Columns.Add(cmb)

        Dim X As Integer = DataGridView1.RowCount
        Do
            X = X - 1
            DataGridView1.Rows(X).Cells(4).Value = "Aktif"

        Loop Until X = 0
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Dim X As Integer = DataGridView1.RowCount
        If CheckBox1.Checked Then
            Do
                X = X - 1
                DataGridView1.Rows(X).Cells(0).Value = True
            Loop Until X = 0

        Else
            Do
                X = X - 1
                DataGridView1.Rows(X).Cells(0).Value = False
            Loop Until X = 0
        End If
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        For i = 0 To DataGridView1.RowCount - 2

            Dim NIM As Object = DataGridView1.Rows(i).Cells(1).Value
            Dim Status As Object = DataGridView1.Rows(i).Cells(4).Value
            cmdInsert.Parameters.AddWithValue("@NIM", CType(NIM, String))
            cmdInsert.Parameters.AddWithValue("@Kelas", CType(Me.TxtKelas.Text, String))
            cmdInsert.Parameters.AddWithValue("@Status", CType(Status, String))

            If DataGridView1.Item(0, i).Value = True Then
                Try
                    cmdInsert.CommandText = "INSERT INTO Kelas(NIM, Kelas, Status) VALUES(@NIM, @Kelas, @Status)"
                    cmdInsert.CommandType = CommandType.Text
                    cmdInsert.Connection = cnnOLEDB
                    cmdInsert.ExecuteNonQuery()
                    cmdInsert.Dispose()
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If

        Next
        MsgBox("All Records Inserted")
        DS.Tables("Tabel1").Clear()

        DataGridView1.DataSource = DS.Tables("Tabel1")
    End Sub

    Private Sub BtnEditOK1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEditOK1.Click
        Dim query As String
        query = "SELECT m.NIM, m.Nama_Mhs, k.Kelas FROM Master_Mahasiswa m LEFT JOIN Kelas k ON m.NIM = k.NIM WHERE k.Kelas='" & txtKelasSebelum.Text & "' ORDER BY m.NIM"

        ADP = New OleDbDataAdapter(query, cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Tabel1")

        DataGridView1.Columns.Clear()

        Dim chk As New DataGridViewCheckBoxColumn() With {.Width = 25}
        DataGridView1.Columns.Add(chk)
        chk.HeaderText = ""
        chk.Name = "chk"

        DataGridView1.DataSource = DS.Tables("Tabel1")

        Dim cmb As New DataGridViewComboBoxColumn
        cmb.HeaderText = "Status"
        cmb.Name = "cmbstatus"
        cmb.MaxDropDownItems = 3
        cmb.Items.Add("Aktif")
        cmb.Items.Add("Cuti")
        cmb.Items.Add("DO")

        DataGridView1.Columns.Add(cmb)

        Dim Status As String
        Status = "SELECT Status FROM Kelas"
        ADP2 = New OleDbDataAdapter(Status, cnnOLEDB)
        DS2 = New DataSet
        ADP2.Fill(DS2, "Tabel2")

        For i = 0 To DataGridView1.RowCount - 2
            DataGridView1.Rows(i).Cells(4).Value = DS2.Tables("Tabel2").Rows(i)("Status")
        Next

    End Sub

    Private Sub BtnEditOK2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEditOK2.Click
        Dim query As String
        query = "SELECT m.NIM, m.Nama_Mhs, k.Kelas FROM Master_Mahasiswa m LEFT JOIN Kelas k " & _
                "ON m.NIM = k.NIM WHERE m.NIM = '" & MaskedTextBox1.Text & "'ORDER BY m.NIM"
        ADP = New OleDbDataAdapter(query, cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Tabel1")

        DataGridView1.Columns.Clear()
        Dim chk As New DataGridViewCheckBoxColumn() With {.Width = 25}
        DataGridView1.Columns.Add(chk)
        chk.HeaderText = ""
        chk.Name = "chk"

        DataGridView1.DataSource = DS.Tables("Tabel1")

        Dim cmb As New DataGridViewComboBoxColumn
        cmb.HeaderText = "Status"
        cmb.Name = "cmbStatus"
        cmb.MaxDropDownItems = 3
        cmb.Items.Add("Aktif")
        cmb.Items.Add("Cuti")
        cmb.Items.Add("DO")
        DataGridView1.Columns.Add(cmb)

        Dim Status As String
        Status = "SELECT Status FROM Kelas WHERE NIM ='" & MaskedTextBox1.Text & "' ORDER BY NIM"
        ADP2 = New OleDbDataAdapter(Status, cnnOLEDB)
        DS2 = New DataSet
        ADP2.Fill(DS2, "Tabel2")

        For i = 0 To DataGridView1.RowCount - 2
            DataGridView1.Rows(i).Cells(4).Value = DS2.Tables("Tabel2").Rows(i)("Status")
        Next
    End Sub

    Private Sub BtnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEdit.Click
        For i = 0 To DataGridView1.RowCount - 2
            If DataGridView1.Item(0, i).Value = True Then
                Try
                    If RadioButton1.Enabled = True Then
                        cmdUpdate.CommandText = "UPDATE Kelas SET NIM=@NIM, Kelas=@KelasBaru, Status=@Status WHERE NIM=@NIM AND Kelas=@KelasSebelum"

                        Dim NIM As Object = DataGridView1.Rows(i).Cells(1).Value
                        Dim KelasSebelum As Object = DataGridView1.Rows(i).Cells(3).Value
                        Dim Status As Object = DataGridView1.Rows(i).Cells(4).Value
                        cmdUpdate.Parameters.AddWithValue("@NIM", CType(NIM, String))
                        cmdUpdate.Parameters.AddWithValue("@KelasBaru", Me.txtKelasBaru.Text)
                        cmdUpdate.Parameters.AddWithValue("@Status", CType(Status, String))
                        cmdUpdate.Parameters.AddWithValue("@KelasSebelum", CType(KelasSebelum, String))

                    Else
                        cmdUpdate.CommandText = "UPDATE Kelas SET Status = @Status WHERE NIM = @NIM"
                        Dim Status As Object = DataGridView1.Rows(i).Cells(4).Value
                        cmdUpdate.Parameters.AddWithValue("@Status", CType(Status, String))
                        Dim NIM As Object = DataGridView1.Rows(i).Cells(1).Value
                        cmdUpdate.Parameters.AddWithValue("@NIM", CType(NIM, String))
                    End If
                    cmdUpdate.CommandType = CommandType.Text
                    cmdUpdate.Connection = cnnOLEDB
                    cmdUpdate.ExecuteNonQuery()
                    MsgBox("All Records Updated")
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If

        Next
        DS.Tables("Tabel1").Clear()
        DataGridView1.DataSource = DS.Tables("Tabel1")
        cmdUpdate.Dispose()
        cmdUpdate.Dispose()
    End Sub

    Private Sub BtnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHapus.Click
        For i = 0 To DataGridView1.RowCount - 2
            If DataGridView1.Item(0, i).Value = True Then
                Try
                    cmdInsert.CommandText = "INSERT INTO Alumni(NIM, Nama, Kelas, TahunKeluar, Tempat_Lahir, Tanggal_Lahir, Alamat, Kota, Provinsi, No_HP, Email) VALUES(@NIM, @Nama, @Kelas, '" & Format(Now, "yyyy") & "', @Tlahir, @Tgl, @Alamat, @Kota, @Provinsi, @NoHp, @Email)"

                    Dim NIM As Object = DataGridView1.Rows(i).Cells(1).Value
                    cmdInsert.Parameters.AddWithValue("@NIM", CType(NIM, String))
                    Dim Nama As Object = DataGridView1.Rows(i).Cells(2).Value
                    cmdInsert.Parameters.AddWithValue("@Nama", CType(Nama, String))
                    Dim Kelas As Object = DataGridView1.Rows(i).Cells(3).Value
                    cmdInsert.Parameters.AddWithValue("@Kelas", CType(Kelas, String))
                    Dim Tlahir As Object = DataGridView1.Rows(i).Cells(5).Value
                    cmdInsert.Parameters.AddWithValue("@Tlahir", CType(Tlahir, String))
                    Dim Tgl As Object = DataGridView1.Rows(i).Cells(6).Value
                    cmdInsert.Parameters.AddWithValue("@Tgl", CType(Tgl, String))
                    Dim Alamat As Object = DataGridView1.Rows(i).Cells(7).Value
                    cmdInsert.Parameters.AddWithValue("@Alamat", CType(Alamat, String))
                    Dim Kota As Object = DataGridView1.Rows(i).Cells(8).Value
                    cmdInsert.Parameters.AddWithValue("@Kota", CType(Kota, String))
                    Dim Provinsi As Object = DataGridView1.Rows(i).Cells(9).Value
                    cmdInsert.Parameters.AddWithValue("@Provinsi", CType(Provinsi, String))
                    Dim NoHp As Object = DataGridView1.Rows(i).Cells(10).Value
                    cmdInsert.Parameters.AddWithValue("@NoHp", CType(NoHp, String))
                    Dim Email As Object = DataGridView1.Rows(i).Cells(11).Value
                    cmdInsert.Parameters.AddWithValue("@Email", CType(Email, String))


                    cmdInsert.CommandType = CommandType.Text
                    cmdInsert.Connection = cnnOLEDB
                    cmdInsert.ExecuteNonQuery()
                    cmdInsert.Dispose()

                    cmdDelete.CommandText = "DELETE * FROM Kelas WHERE NIM=@NIM1"

                    Dim NIM1 As Object = DataGridView1.Rows(i).Cells(1).Value
                    cmdDelete.Parameters.AddWithValue("@NIM1", CType(NIM1, String))
                    Dim Kelas1 As Object = DataGridView1.Rows(i).Cells(3).Value
                    cmdDelete.Parameters.AddWithValue("@Kelas1", CType(Kelas1, String))

                    cmdDelete.CommandType = CommandType.Text
                    cmdDelete.Connection = cnnOLEDB
                    cmdDelete.ExecuteNonQuery()
                    cmdDelete.Dispose()
                Catch ex As Exception
                    MsgBox(ex.ToString)
                End Try
            End If
        Next
        MsgBox("All Records Deleted")
        DS.Tables("Tabel1").Clear()
        DataGridView1.DataSource = DS.Tables("Tabel1")
        TextBox4.Text = ""
    End Sub

    Private Sub BtnLihat_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLihat.Click
        If TextBox4.Text.Substring(2, 1) = "3" Or TextBox4.Text.Substring(3, 1) = "3" Then
            Dim query As String
            query = "SELECT m.NIM, m.Nama_Mhs, k.Kelas, m.Tempat_Lahir, m.Tanggal_Lahir, m.Alamat, m.Kota, m.Provinsi, m.No_HP, m.Email FROM Master_Mahasiswa m LEFT JOIN Kelas k ON m.NIM = k.NIM WHERE k.Kelas='" & TextBox4.Text & "' ORDER BY m.NIM"

            ADP = New OleDbDataAdapter(query, cnnOLEDB)
            DS = New DataSet
            ADP.Fill(DS, "Tabel1")
            DataGridView1.Columns.Clear()

            Dim chk As New DataGridViewCheckBoxColumn() With {.Width = 25}
            DataGridView1.Columns.Add(chk)
            chk.HeaderText = ""
            chk.Name = "chk"

            DataGridView1.DataSource = DS.Tables("Tabel1")

            Dim cmb As New DataGridViewComboBoxColumn
            cmb.HeaderText = "Status"
            cmb.Name = "cmbstatus"
            cmb.MaxDropDownItems = 3
            cmb.Items.Add("Aktif")
            cmb.Items.Add("Cuti")
            cmb.Items.Add("DO")
            DataGridView1.Columns.Add(cmb)

            Dim Status As String
            Status = "SELECT Status FROM Kelas WHERE Kelas='" & TextBox4.Text & "' ORDER BY NIM"

            ADP2 = New OleDbDataAdapter(Status, cnnOLEDB)
            DS2 = New DataSet
            ADP2.Fill(DS2, "Tabel2")

            For i = 0 To DataGridView1.RowCount - 2
                DataGridView1.Rows(i).Cells(11).Value = DS2.Tables("Tabel2").Rows(i)("Status")
            Next
        End If
    End Sub

    Private Sub btnAlumni_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAlumni.Click
        If cmbCari.Text = "Nama" Then
            Dim query As String
            query = "SELECT * FROM Alumni WHERE Nama LIKE '" & TextAlumni.Text & "%' ORDER BY NIM"
            ADP = New OleDbDataAdapter(query, cnnOLEDB)
            DS = New DataSet
            ADP.Fill(DS, "Tabel1")
            DataGridView1.Columns.Clear()
            DataGridView1.DataSource = DS.Tables("Tabel1")

        ElseIf cmbCari.Text = "Tahun" Then
            Dim query1 As String
            query1 = "SELECT * FROM Alumni WHERE TahunKeluar='" & TextAlumni.Text & "' ORDER BY NIM"
            ADP = New OleDbDataAdapter(query1, cnnOLEDB)
            DS = New DataSet
            ADP.Fill(DS, "Tabel1")
            DataGridView1.Columns.Clear()
            DataGridView1.DataSource = DS.Tables("Tabel1")

        Else
            Dim query2 As String
            query2 = "SELECT * FROM Alumni WHERE Kelas='" & TextAlumni.Text & "' ORDER BY NIM"

            ADP = New OleDbDataAdapter(query2, cnnOLEDB)
            DS = New DataSet
            ADP.Fill(DS, "Tabel1")
            DataGridView1.Columns.Clear()
            DataGridView1.DataSource = DS.Tables("Tabel1")

        End If

    End Sub
End Class