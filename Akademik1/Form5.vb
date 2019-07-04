Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Public Class Form5
    Dim cnnOLEDB As New OleDbConnection
    Dim cmdInsert As New OleDbCommand
    Dim cmdUpdate As New OleDbCommand
    Dim cmdDelete As New OleDbCommand
    Dim strConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & _
        System.Environment.CurrentDirectory & "\Akademik.accdb"

    Public ADP As OleDbDataAdapter
    Public DS As New DataSet

    Private Sub Form5_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cnnOLEDB.ConnectionString = strConnectionString
        cnnOLEDB.Open()
        TampilData()
        TxtKMK.MaxLength = 7
        TxtNMK.MaxLength = 50
        TxtSKS.MaxLength = 5
        TxtJT.MaxLength = 20
        TxtJP.MaxLength = 20
        ButtonEnable()
    End Sub

    Sub TampilData()
        ButtonEnable()
        ADP = New OleDbDataAdapter("SELECT * FROM Master_MK ORDER BY Kode_MK", cnnOLEDB)
        DS = New DataSet
        ADP.Fill(DS, "Tabel1")
        DataGridView1.DataSource = DS.Tables("Tabel1")
    End Sub

    Sub Bersih()
        TxtKMK.Text = ""
        TxtNMK.Text = ""
        TxtSKS.Text = ""
        TxtJP.Text = ""
        TxtJT.Text = ""
    End Sub

    Private Sub BtnSimpan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSimpan.Click
        If TxtKMK.Text <> "" And TxtNMK.Text <> "" And TxtSKS.Text <> "" And TxtJT.Text <> "" And TxtJP.Text <> "" Then

            Try
                cmdInsert.CommandText = "INSERT INTO Master_MK (Kode_MK, Nama_MK, SKS, Jam_Teori, Jam_Praktek) VALUES('" & TxtKMK.Text & "','" & TxtNMK.Text & "','" & TxtSKS.Text & "','" & TxtJT.Text & "','" & TxtJP.Text & "')"

                cmdInsert.CommandType = CommandType.Text
                cmdInsert.Connection = cnnOLEDB
                cmdInsert.ExecuteNonQuery()
                MsgBox("Record Inserted")
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            MsgBox("Masukan Data Secara Lengkap!")
        End If
        cmdInsert.Dispose()
        TampilData()
        Bersih()
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        GetData(e)
    End Sub

    Private Sub DataGridView1_CellContextMenuStripChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContextMenuStripChanged
        GetData(e)
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        GetData(e)
    End Sub

    Sub GetData(ByVal e)
        ButtonDisable()
        Dim KMK As Object = DataGridView1.Rows(e.RowIndex).Cells(0).Value
        Dim NMK As Object = DataGridView1.Rows(e.RowIndex).Cells(1).Value
        Dim SKS As Object = DataGridView1.Rows(e.RowIndex).Cells(2).Value
        Dim JT As Object = DataGridView1.Rows(e.RowIndex).Cells(3).Value
        Dim JP As Object = DataGridView1.Rows(e.RowIndex).Cells(4).Value

        TxtKMK.Text = CType(KMK, String)
        TxtNMK.Text = CType(NMK, String)
        TxtSKS.Text = CType(SKS, String)
        TxtJT.Text = CType(JT, String)
        TxtJP.Text = CType(JP, String)
    End Sub

    Sub ButtonEnable()
        BtnSimpan.Enabled = True
        BtnUpdate.Enabled = False
        BtnDelete.Enabled = False
        TxtKMK.Enabled = True
    End Sub
    Sub ButtonDisable()
        BtnSimpan.Enabled = False
        BtnUpdate.Enabled = True
        BtnDelete.Enabled = True
        TxtKMK.Enabled = False
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        If TxtKMK.Text <> "" And TxtNMK.Text <> "" And TxtSKS.Text <> "" And TxtJT.Text <> "" And TxtJP.Text <> "" Then
            Try
                cmdUpdate.CommandText = "UPDATE Master_MK SET Kode_MK=@KMK, Nama_MK=@NMK, SKS=@SKS, Jam_Teori=@JT, Jam_Praktek=@JP WHERE Kode_MK=@KMK"

                cmdUpdate.Parameters.AddWithValue("@KMK", Me.TxtKMK.Text)
                cmdUpdate.Parameters.AddWithValue("@NMK", Me.TxtNMK.Text)
                cmdUpdate.Parameters.AddWithValue("@SKS", Me.TxtSKS.Text)
                cmdUpdate.Parameters.AddWithValue("@JT", Me.TxtJT.Text)
                cmdUpdate.Parameters.AddWithValue("@JP", Me.TxtJP.Text)

                cmdUpdate.CommandType = CommandType.Text
                cmdUpdate.Connection = cnnOLEDB
                cmdUpdate.ExecuteNonQuery()
                MsgBox("Record Updated")
            Catch ex As Exception
                MsgBox(ex.ToString)
            End Try
        Else
            MsgBox("Masukan data secara lengkap!")
        End If
        cmdUpdate.Dispose()
        TampilData()
        Bersih()
    End Sub

    Private Sub BtnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDelete.Click
        Try
            cmdDelete.CommandText = "DELETE FROM Master_MK WHERE Kode_MK=@KMK"

            cmdDelete.Parameters.AddWithValue("@KMK", Me.TxtKMK.Text)
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


    Private Sub BtnCari_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCari.Click
        ButtonDisable()
        Dim query1 As String
        Dim query2 As String

        query1 = "SELECT * FROM Master_MK WHERE Kode_MK='" & MskCari.Text & "'"
        query2 = "SELECT * FROM Master_MK WHERE Nama_MK LIKE '" & MskCari.Text & "%'"

        If CmbCari.Text = "Kode Mata Kuliah" Then
            ADP = New OleDbDataAdapter(query1, cnnOLEDB)
            DS = New DataSet
            ADP.Fill(DS, "Tabel1")
            DataGridView1.DataSource = DS.Tables("Tabel1")

            Dim KMK As Object = DataGridView1.Rows(0).Cells(0).Value
            Dim NMK As Object = DataGridView1.Rows(0).Cells(1).Value
            Dim SKS As Object = DataGridView1.Rows(0).Cells(2).Value
            Dim JT As Object = DataGridView1.Rows(0).Cells(3).Value
            Dim JP As Object = DataGridView1.Rows(0).Cells(4).Value

            TxtKMK.Text = CType(KMK, String)
            TxtNMK.Text = CType(NMK, String)
            TxtSKS.Text = CType(SKS, String)
            TxtJT.Text = CType(JT, String)
            TxtJP.Text = CType(JP, String)

        Else
            ADP = New OleDbDataAdapter(query2, cnnOLEDB)
            DS = New DataSet
            ADP.Fill(DS, "Tabel1")
            DataGridView1.DataSource = DS.Tables("Tabel1")
            If DataGridView1.RowCount = 2 Then

                Dim KMK As Object = DataGridView1.Rows(0).Cells(0).Value
                Dim NMK As Object = DataGridView1.Rows(0).Cells(1).Value
                Dim SKS As Object = DataGridView1.Rows(0).Cells(2).Value
                Dim JT As Object = DataGridView1.Rows(0).Cells(3).Value
                Dim JP As Object = DataGridView1.Rows(0).Cells(4).Value

                TxtKMK.Text = CType(KMK, String)
                TxtNMK.Text = CType(NMK, String)
                TxtSKS.Text = CType(SKS, String)
                TxtJT.Text = CType(JT, String)
                TxtJP.Text = CType(JP, String)

            End If
        End If
    End Sub
End Class