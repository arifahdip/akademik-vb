Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Public Class Form3
    Dim cnnOLEDB As New OleDbConnection
    Dim cmdOLEDB As New OleDbCommand
    Dim cmdInsert As New OleDbCommand
    Dim strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" &
        System.Environment.CurrentDirectory & "\Akademik.accdb"
    Public ADP As OleDbDataAdapter
    Public DS As New DataSet

    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cnnOLEDB.ConnectionString = strConnectionString
        cnnOLEDB.Open()
        TampilData()
        Bersih()
        TxtNIP.MaxLength = 12
        TxtNama.MaxLength = 30
        TxtLahir.MaxLength = 20
        TxtAlamat.MaxLength = 50
        TxtNoHP.MaxLength = 13
        TxtEmail.MaxLength = 30
    End Sub

    Sub TampilData()
        ADP = New OleDbDataAdapter("SELECT * FROM Master_Dosen ORDER BY NIP", cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Tabel1")
        DataGridView1.DataSource = DS.Tables("Tabel1")
    End Sub

    Sub Bersih()
        TxtNIP.Text = ""
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
        Dim o As System.IO.FileStream
        Dim r As StreamReader
        Dim jpgfile As String = TxtFoto.Text


        If TxtNIP.Text <> "" And TxtNama.Text <> "" And TxtLahir.Text <> "" And TxtAlamat.Text <> "" And CmbKota.Text <> "" And CmbProvinsi.Text <> "" And TxtNoHP.Text <> "" And TxtFoto.Text <> "" Then

            'open image file
            o = New FileStream(jpgfile, FileMode.Open, FileAccess.Read, FileShare.Read)
            r = New StreamReader(o)

            Try
                Dim FileByteArray(o.Length - 1) As Byte

                o.Read(FileByteArray, 0, o.Length)

                cmdInsert.CommandText = "INSERT INTO Master_Dosen (NIP, Nama_Dosen, Tempat_Lahir, Tanggal_Lahir, Alamat, Kota, Provinsi, No_HP, Email, Foto) VALUES(@NIP, @Nama, @TLahir, @Tgl, @Alamat, @Kota, @Provinsi, @NoHP, @Email ,@Gambar)"

                cmdInsert.Parameters.AddWithValue("@NIP", Me.TxtNIP.Text)
                cmdInsert.Parameters.AddWithValue("@Nama", Me.TxtNama.Text)
                cmdInsert.Parameters.AddWithValue("@TLahir", Me.TxtLahir.Text)
                cmdInsert.Parameters.AddWithValue("@Tgl", Me.Tgl.Value.Date.ToString)
                cmdInsert.Parameters.AddWithValue("@Alamat", Me.TxtAlamat.Text)
                cmdInsert.Parameters.AddWithValue("@Kota", Me.CmbKota.Text)
                cmdInsert.Parameters.AddWithValue("@Provinsi", Me.CmbProvinsi.Text)
                cmdInsert.Parameters.AddWithValue("@NoHP", Me.TxtNoHP.Text)
                cmdInsert.Parameters.AddWithValue("@Email", Me.TxtEmail.Text)
                cmdInsert.Parameters.Add("@Gambar", System.Data.OleDb.OleDbType.Binary, o.Length).Value = FileByteArray

                cmdInsert.CommandType = CommandType.Text
                cmdInsert.Connection = cnnOLEDB
                cmdInsert.ExecuteNonQuery()
                MsgBox("Record inserted")

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        Else
            MsgBox("Masukan data Secara Lengkap!", MsgBoxStyle.Exclamation, "Peringatan")
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

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        GetData(e)
    End Sub


    Private Sub DataGridView1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEnter
        GetData(e)
    End Sub

    Sub GetData(ByVal e)
        Dim NIP As Object = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Dim Nama As Object = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        Dim TempatLahir As Object = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        Dim TanggalLahir As Object = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        Dim Alamat As Object = DataGridView1.Rows(e.RowIndex).Cells(4).Value
        Dim Kota As Object = DataGridView1.Rows(e.RowIndex).Cells(5).Value
        Dim Provinsi As Object = DataGridView1.Rows(e.RowIndex).Cells(6).Value
        Dim NoHP As Object = DataGridView1.Rows(e.RowIndex).Cells(7).Value
        Dim Email As Object = DataGridView1.Rows(e.RowIndex).Cells(8).Value

        TxtNIP.Text = CType(NIP, String)
        TxtNama.Text = CType(Nama, String)
        TxtLahir.Text = CType(TempatLahir, String)
        Tgl.Text = CType(TanggalLahir, Date)
        TxtAlamat.Text = CType(Alamat, String)
        TxtNoHP.Text = CType(NoHP, String)
        CmbKota.Text = CType(Kota, String)
        CmbProvinsi.Text = CType(Provinsi, String)
        TxtEmail.Text = CType(Email, String)

        Try
            Dim command As New OleDbCommand("SELECT Foto FROM Master_Dosen WHERE NIP=@NIP", cnnOLEDB)
            command.Parameters.AddWithValue("@NIP", CType(NIP, String))

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

    Private Sub GetData()
        Throw New NotImplementedException
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If TxtNIP.Text <> "" And TxtAlamat.Text <> "" And TxtEmail.Text <> "" And TxtLahir.Text <> "" And TxtNama.Text <> "" And TxtNoHP.Text <> "" Then
            Try
                If TxtFoto.Text = "" Then
                    cmdInsert.CommandText = "UPDATE Master_Dosen SET NIP=@NIP, Nama_Dosen=@Nama, Tempat_Lahir=@TLahir, Tanggal_Lahir=@Tgl, Alamat=@Alamat, Kota=@Kota, Provinsi=@Provinsi, No_HP=@NoHP, Email=@Email WHERE NIP=@NIP"
                    cmdInsert.Parameters.AddWithValue("@NIP", Me.TxtNIP.Text)
                    cmdInsert.Parameters.AddWithValue("@Nama", Me.TxtNama.Text)
                    cmdInsert.Parameters.AddWithValue("@TLahir", Me.TxtLahir.Text)
                    cmdInsert.Parameters.AddWithValue("@Tgl", Me.Tgl.Value.Date.ToString)
                    cmdInsert.Parameters.AddWithValue("@Alamat", Me.TxtAlamat.Text)
                    cmdInsert.Parameters.AddWithValue("@Kota", Me.CmbKota.Text)
                    cmdInsert.Parameters.AddWithValue("@Provinsi", Me.CmbProvinsi.Text)
                    cmdInsert.Parameters.AddWithValue("@NoHP", Me.TxtNoHP.Text)
                    cmdInsert.Parameters.AddWithValue("@Email", Me.TxtEmail.Text)

                Else
                    Dim o As System.IO.FileStream
                    Dim r As StreamReader
                    Dim jpgFile As String = TxtFoto.Text
                    o = New FileStream(jpgFile, FileMode.Open, FileAccess.Read, FileShare.Read)
                    r = New StreamReader(o)
                    Dim FileByteArray(o.Length - 1) As Byte
                    o.Read(FileByteArray, 0, o.Length)

                    cmdInsert.CommandText = "UPDATE Master_Dosen SET NIP=@NIP, Nama_Dosen=@Nama, Tempat_Lahir=@TLahir, Tanggal_Lahir=@Tgl, Alamat=@Alamat, Kota=@Kota, Provinsi=@Provinsi, No_HP=@NoHP, Email=@Email, Foto=@Gambar WHERE NIP=@NIP"
                    cmdInsert.Parameters.AddWithValue("@NIP", Me.TxtNIP.Text)
                    cmdInsert.Parameters.AddWithValue("@Nama", Me.TxtNama.Text)
                    cmdInsert.Parameters.AddWithValue("@TLahir", Me.TxtLahir.Text)
                    cmdInsert.Parameters.AddWithValue("@Tgl", Me.Tgl.Value.Date.ToString)
                    cmdInsert.Parameters.AddWithValue("@Alamat", Me.TxtAlamat.Text)
                    cmdInsert.Parameters.AddWithValue("@Kota", Me.CmbKota.Text)
                    cmdInsert.Parameters.AddWithValue("@Provinsi", Me.CmbProvinsi.Text)
                    cmdInsert.Parameters.AddWithValue("@NoHP", Me.TxtNoHP.Text)
                    cmdInsert.Parameters.AddWithValue("@Email", Me.TxtEmail.Text)
                    cmdInsert.Parameters.Add("@Gambar", System.Data.OleDb.OleDbType.Binary, o.Length).Value = FileByteArray

                End If

                cmdInsert.CommandType = CommandType.Text
                cmdInsert.Connection = cnnOLEDB
                cmdInsert.ExecuteNonQuery()
                MsgBox("Record Updated")

            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try

        Else
            MsgBox("Masukan Data Secara Lengkap")
        End If
        TampilData()
        cmdInsert.Dispose()
    End Sub

    Private Sub btnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHapus.Click
        Try
            cmdInsert.Parameters.AddWithValue("@NIP", Me.TxtNIP.Text)
            cmdInsert.CommandText = "DELETE FROM Master_Dosen WHERE NIP=@NIP"

            cmdInsert.CommandType = CommandType.Text
            cmdInsert.Connection = cnnOLEDB
            cmdInsert.ExecuteNonQuery()
            MsgBox("Record Deleted")
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        TampilData()
        cmdInsert.Dispose()
        Bersih()
    End Sub
End Class