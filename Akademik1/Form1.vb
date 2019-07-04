Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Public Class Form1
    Dim cnnOLEDB As New OleDbConnection
    Dim cmdOLEDB As New OleDbCommand
    Dim cmdInsert As New OleDbCommand
    Dim cmdUpdate As New OleDbCommand
    Dim cmdDelete As New OleDbCommand
    Dim strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _
        System.Environment.CurrentDirectory & "\Akademik.accdb"
    Public ADP As OleDbDataAdapter
    Public DS As New DataSet

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cnnOLEDB.ConnectionString = strConnectionString
        cnnOLEDB.Open()
        TampilData()
        TxtNIM.MaxLength = 12
        TxtNama.MaxLength = 30
        TxtLahir.MaxLength = 20
        TxtAlamat.MaxLength = 50
        TxtNoHP.MaxLength = 13
        TxtEmail.MaxLength = 30
    End Sub
    Sub TampilData()
        ADP = New OleDbDataAdapter("SELECT * FROM Master_Mahasiswa ORDER BY NIM", cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Tabel1")
        DataGridView1.DataSource = DS.Tables("Tabel1")
    End Sub
    Sub Bersih()
        TxtNIM.Text = ""
        TxtNama.Text = ""
        TxtLahir.Text = ""
        Tgl.Text = ""
        TxtAlamat.Text = ""
        CmbKota.Text = ""
        CmbProvinsi.Text = ""
        TxtNoHP.Text = ""
        TxtFoto.Text = ""
        TxtEmail.Text = ""
        PictureBox1.Image = Nothing
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click

        If TxtNIM.Text <> "" And TxtNama.Text <> "" And TxtLahir.Text <> "" And TxtAlamat.Text <> "" And CmbKota.Text <> "" And CmbProvinsi.Text <> "" And TxtNoHP.Text <> "" And TxtFoto.Text <> "" Then
            Dim o As System.IO.FileStream
            Dim r As StreamReader
            Dim jpgFile As String = TxtFoto.Text
            o = New FileStream(jpgFile, FileMode.Open, FileAccess.Read, FileShare.Read)
            r = New StreamReader(o)

            Try
                Dim FileByteArray(o.Length - 1) As Byte
                o.Read(FileByteArray, 0, o.Length)

                cmdInsert.CommandText = "INSERT INTO Master_Mahasiswa (NIM, Nama_Mhs, Tempat_Lahir, Tanggal_Lahir, Alamat, Kota, Provinsi, No_HP, Email, Foto) VALUES('" & TxtNIM.Text & "', '" & TxtNama.Text & "', '" & TxtLahir.Text & "' ,'" & Tgl.Value.Date.ToString & "','" & TxtAlamat.Text & "','" & CmbKota.Text & "','" & CmbProvinsi.Text & "','" & TxtNoHP.Text & "','" & TxtEmail.Text & "',@Gambar)"
                cmdInsert.Parameters.Add("@Gambar", System.Data.OleDb.OleDbType.Binary, o.Length).Value = FileByteArray

                cmdInsert.CommandType = CommandType.Text
                cmdInsert.Connection = cnnOLEDB
                cmdInsert.ExecuteNonQuery()
                MsgBox("Record inserted")
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        Else
            MsgBox("Masukan Data Secara Lengakp !", MsgBoxStyle.Exclamation, "Peringatan")
        End If
        cmdInsert.Dispose()
        TampilData()
        Bersih()
    End Sub

    Private Sub BtnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBrowse.Click
        Dim OpenFileDialog1 As New OpenFileDialog
        With OpenFileDialog1
            .CheckFileExists = True
            .ShowReadOnly = False
            .Filter = "All Files |*.*|Bitmap Files(*)|*.bmp;*.gif;*.jpg|PNG Files(*)|*.png"
            .FilterIndex = 2

            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                PictureBox1.Image = Image.FromFile(.FileName)
                TxtFoto.Text = .FileName.ToString
            End If
        End With
    End Sub

    Private Sub TxtNoHP_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtNoHP.KeyPress
        If Asc(e.KeyChar) <> 8 And Asc(e.KeyChar) <> 43 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        GetData(e)
    End Sub

    Sub GetData(ByVal e)
        Dim NIM As Object = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Dim Nama As Object = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        Dim TempatLahir As Object = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        Dim TanggalLahir As Object = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        Dim Alamat As Object = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        Dim Kota As Object = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        Dim Provinsi As Object = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        Dim NoHP As Object = DataGridView1.Rows(e.RowIndex).Cells(7).Value
        Dim Email As Object = DataGridView1.Rows(e.RowIndex).Cells(8).Value

        TxtNIM.Text = CType(NIM, String)
        TxtNama.Text = CType(Nama, String)
        TxtLahir.Text = CType(TempatLahir, String)
        Tgl.Text = CType(TanggalLahir, Date)
        TxtAlamat.Text = CType(Alamat, String)
        TxtNoHP.Text = CType(NoHP, String)
        CmbKota.Text = CType(Kota, String)
        CmbProvinsi.Text = CType(Provinsi, String)
        TxtEmail.Text = CType(Email, String)

        Try
            Dim command As New OleDbCommand("SELECT Foto FROM Master_Mahasiswa WHERE NIM=@NIM", cnnOLEDB)
            command.Parameters.AddWithValue("@NIM", CType(NIM, String))

            Dim pictureData As Byte() = DirectCast(command.ExecuteScalar(), Byte())
            command.Dispose()

            'mengubah file byte menjadi foto stream
            Dim Stream As New IO.MemoryStream(pictureData)

            'menampilkan picture pada picturebox
            Me.PictureBox1.Image = Image.FromStream(Stream)
            Stream.Dispose()

        Catch ex As Exception
            MessageBox.Show(ex.ToString)

        End Try

    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        If TxtNIM.Text <> "" And TxtNama.Text <> "" And TxtLahir.Text <> "" And TxtAlamat.Text <> "" And CmbKota.Text <> "" And CmbProvinsi.Text <> "" And TxtNoHP.Text <> "" Then

            Try
                If TxtFoto.Text = "" Then
                    cmdUpdate.CommandText = "UPDATE Master_Mahasiswa SET NIM=@NIM, Nama_Mhs=@Nama, Tempat_Lahir=@TLahir, Tanggal_Lahir=@Tgl, Alamat=@Alamat, Kota=@Kota, Provinsi=@Provinsi, No_HP=@No_Hp, Email=@Email WHERE NIM=@NIM "

                    cmdUpdate.Parameters.AddWithValue("@NIM", Me.TxtNIM.Text)
                    cmdUpdate.Parameters.AddWithValue("@Nama", Me.TxtNama.Text)
                    cmdUpdate.Parameters.AddWithValue("@TLahir", Me.TxtLahir.Text)
                    cmdUpdate.Parameters.AddWithValue("@Tgl", Me.Tgl.Value.Date.ToString)
                    cmdUpdate.Parameters.AddWithValue("@Alamat", Me.TxtAlamat.Text)
                    cmdUpdate.Parameters.AddWithValue("@Kota", Me.CmbKota.Text)
                    cmdUpdate.Parameters.AddWithValue("@Provinsi", Me.CmbProvinsi.Text)
                    cmdUpdate.Parameters.AddWithValue("@No_HP", Me.TxtNoHP.Text)
                    cmdUpdate.Parameters.AddWithValue("@Email", Me.TxtEmail.Text)

                Else

                    Dim o As System.IO.FileStream
                    Dim r As StreamReader
                    Dim jpgFile As String = TxtFoto.Text
                    o = New FileStream(jpgFile, FileMode.Open, FileAccess.Read, FileShare.Read)
                    r = New StreamReader(o)
                    Dim FileByteArray(o.Length - 1) As Byte
                    o.Read(FileByteArray, 0, o.Length)

                    cmdUpdate.CommandText = "UPDATE Master_Mahasiswa SET NIM=@NIM, Nama_Mhs=@Nama, Tempat_Lahir=@TLahir, Tanggal_Lahir=@Tgl, Alamat=@Alamat, Kota=@Kota, Provinsi=@Provinsi, No_HP=@No_Hp, Email=@Email, Foto=@Gambar WHERE NIM=@NIM "

                    cmdUpdate.Parameters.AddWithValue("@NIM", Me.TxtNIM.Text)
                    cmdUpdate.Parameters.AddWithValue("@Nama", Me.TxtNama.Text)
                    cmdUpdate.Parameters.AddWithValue("@TLahir", Me.TxtLahir.Text)
                    cmdUpdate.Parameters.AddWithValue("@Tgl", Me.Tgl.Value.Date.ToString)
                    cmdUpdate.Parameters.AddWithValue("@Alamat", Me.TxtAlamat.Text)
                    cmdUpdate.Parameters.AddWithValue("@Kota", Me.CmbKota.Text)
                    cmdUpdate.Parameters.AddWithValue("@Provinsi", Me.CmbProvinsi.Text)
                    cmdUpdate.Parameters.AddWithValue("@No_HP", Me.TxtNoHP.Text)
                    cmdUpdate.Parameters.AddWithValue("@Email", Me.TxtEmail.Text)
                    cmdUpdate.Parameters.Add("@Gambar", System.Data.OleDb.OleDbType.Binary, o.Length).Value = FileByteArray

                End If

                cmdUpdate.CommandType = CommandType.Text
                cmdUpdate.Connection = cnnOLEDB
                cmdUpdate.ExecuteNonQuery()
                MsgBox("Record Updated")

            Catch ex As Exception
                MsgBox(ex.ToString)

            End Try

        Else
            MsgBox("Masukan Data Secara Lengkap!")
        End If
        cmdUpdate.Dispose()
        TampilData()
        Bersih()
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            cmdDelete.CommandText = "DELETE FROM Master_Mahasiswa WHERE NIM=@NIM"
            cmdDelete.Parameters.AddWithValue("@NIM", Me.TxtNIM.Text)
            cmdDelete.Connection = cnnOLEDB
            cmdDelete.ExecuteNonQuery()
            MsgBox("Record Deleted")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        TampilData()
        Bersih()
    End Sub

    Private Sub CmbCari_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCari.SelectedIndexChanged
        If CmbCari.Text = "NIM" Then
            MskCari.Mask = "#,##,##,#,##"
        Else
            MskCari.Mask = Nothing
            MskCari.Text = ""
        End If
    End Sub

    Private Sub BtnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCari.Click

        Dim query1 As String
        Dim query2 As String

        query1 = "SELECT * FROM Master_Mahasiswa WHERE NIM= '" & MskCari.Text & "'"
        query2 = "SELECT * FROM Master_Mahasiswa WHERE Nama_Mhs LIKE '" & MskCari.Text & "%'"

        If CmbCari.Text = "NIM" Then
            ADP = New OleDbDataAdapter(query1, cnnOLEDB)
            DS = New DataSet
            ADP.Fill(DS, "Tabel1")
            DataGridView1.DataSource = DS.Tables("Tabel1")

            Dim NIM As Object = DataGridView1.Rows(0).Cells(0).Value
            Dim Nama As Object = DataGridView1.Rows(0).Cells(1).Value
            Dim TempatLahir As Object = DataGridView1.Rows(0).Cells(2).Value
            Dim TanggalLahir As Object = DataGridView1.Rows(0).Cells(3).Value
            Dim Alamat As Object = DataGridView1.Rows(0).Cells(4).Value
            Dim Kota As Object = DataGridView1.Rows(0).Cells(5).Value
            Dim Provinsi As Object = DataGridView1.Rows(0).Cells(6).Value
            Dim NoHP As Object = DataGridView1.Rows(0).Cells(7).Value
            Dim Email As Object = DataGridView1.Rows(0).Cells(8).Value

            TxtNIM.Text = CType(NIM, String)
            TxtNama.Text = CType(Nama, String)
            TxtLahir.Text = CType(TempatLahir, String)
            Tgl.Text = CType(TanggalLahir, String)
            TxtAlamat.Text = CType(Alamat, String)
            CmbKota.Text = CType(Kota, String)
            CmbProvinsi.Text = CType(Provinsi, String)
            TxtNoHP.Text = CType(NoHP, String)
            TxtEmail.Text = CType(Email, String)

            Try
                Dim command As New OleDbCommand("SELECT Foto FROM Master_Mahasiswa WHERE NIM=@NIM", cnnOLEDB)
                command.Parameters.AddWithValue("NIM", CType(NIM, String))
                Dim pictureData As Byte() = DirectCast(command.ExecuteScalar(), Byte())
                command.Dispose()
                Dim Stream As New IO.MemoryStream(pictureData)
                Me.PictureBox1.Image = Image.FromStream(Stream)
                Stream.Dispose()

            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        Else
            ADP = New OleDbDataAdapter(query2, cnnOLEDB)
            DS = New DataSet
            ADP.Fill(DS, "Tabel1")
            DataGridView1.DataSource = DS.Tables("Tabel1")
            If DataGridView1.RowCount = 2 Then
                Dim NIM As Object = DataGridView1.Rows(0).Cells(0).Value
                Dim Nama As Object = DataGridView1.Rows(0).Cells(1).Value
                Dim TempatLahir As Object = DataGridView1.Rows(0).Cells(2).Value
                Dim TanggalLahir As Object = DataGridView1.Rows(0).Cells(3).Value
                Dim Alamat As Object = DataGridView1.Rows(0).Cells(4).Value
                Dim Kota As Object = DataGridView1.Rows(0).Cells(5).Value
                Dim Provinsi As Object = DataGridView1.Rows(0).Cells(6).Value
                Dim NoHP As Object = DataGridView1.Rows(0).Cells(7).Value
                Dim Email As Object = DataGridView1.Rows(0).Cells(8).Value

                TxtNIM.Text = CType(NIM, String)
                TxtNama.Text = CType(Nama, String)
                TxtLahir.Text = CType(TempatLahir, String)
                Tgl.Text = CType(TanggalLahir, String)
                TxtAlamat.Text = CType(Alamat, String)
                CmbKota.Text = CType(Kota, String)
                CmbProvinsi.Text = CType(Provinsi, String)
                TxtNoHP.Text = CType(NoHP, String)
                TxtEmail.Text = CType(Email, String)

                Try
                    Dim command As New OleDbCommand("SELECT Foto FROM Master_Mahasiswa WHERE NIM=@NIM", cnnOLEDB)
                    command.Parameters.AddWithValue("@NIM", CType(NIM, String))
                    Dim pictureData As Byte() = DirectCast(command.ExecuteScalar(), Byte())
                    command.Dispose()
                    Dim Stream As New IO.MemoryStream(pictureData)
                    Me.PictureBox1.Image = Image.FromStream(Stream)
                    Stream.Dispose()
                Catch ex As Exception
                    MessageBox.Show(ex.ToString)

                End Try
            End If

        End If

    End Sub
End Class