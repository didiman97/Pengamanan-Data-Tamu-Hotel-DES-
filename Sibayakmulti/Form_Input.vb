Imports System.Data.OleDb
Public Class Form_input

    Sub tampil()
        SQL2 = "Select * From tbl_data"
        Perintah = New OleDbCommand(SQL2, Koneksi)
        Pembaca2 = Perintah.ExecuteReader
        Dim x As Integer
        ListView1.Items.Clear()
        While Pembaca2.Read
            ListView1.Items.Add(Pembaca2("id_produk"))
            ListView1.Items(x).SubItems.Add(Pembaca2("nama_produk"))
            ListView1.Items(x).SubItems.Add(Pembaca2("pagi_produksi"))
            ListView1.Items(x).SubItems.Add(Pembaca2("siang_produksi"))
            ListView1.Items(x).SubItems.Add(Pembaca2("malam_produksi"))
            ListView1.Items(x).SubItems.Add(Pembaca2("rata_produksi"))
            ListView1.Items(x).SubItems.Add(Pembaca2("tanggal"))
            x = x + 1
        End While
    End Sub
    Sub kunciteks()
        txtproduksi.Enabled = False
        txtpagi.Enabled = False
        txtsiang.Enabled = False
        txtmalam.Enabled = False
        txtratarata.Enabled = False

    End Sub
    Sub BukaTeks()
        txtproduksi.Enabled = True
        txtpagi.Enabled = True
        txtsiang.Enabled = True
        txtmalam.Enabled = True
        txtratarata.Enabled = True

    End Sub
    Sub bersih()
        txtproduksi.Clear()
        txtsiang.Clear()
        txtpagi.Clear()
        txtmalam.Clear()
        txtratarata.Clear()

    End Sub
    Sub KunciTombol()
        btntambah.Enabled = False
        btntambah.Enabled = False
        BtnUbah.Enabled = False
        BtnHapus.Enabled = False
        BtnBatal.Enabled = False
        BtnKeluar.Enabled = False
    End Sub
    Sub BukaTombol()
        btntambah.Enabled = True
        btntambah.Enabled = True
        BtnUbah.Enabled = True
        BtnHapus.Enabled = True
        BtnBatal.Enabled = True
        BtnKeluar.Enabled = True
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnKeluar.Click
        Me.Close()

    End Sub

    Private Sub Form_input_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DateTimePicker1.Format = DateTimePickerFormat.Custom
        DateTimePicker1.CustomFormat = "dd/MMM/yy"

        Call Bukadatabase()
        Call tampil()
        Call kunciteks()
        SQL = "Select * From tbl_user"
        Perintah = New OleDbCommand(SQL, Koneksi)
        Pembaca = Perintah.ExecuteReader
        Dim x As Integer
        While Pembaca.Read
            Label15.Text = (Pembaca("id"))
            Label16.Text = (Pembaca("nama"))
            x = x + 1
        End While
    End Sub


    Private Sub BtnBatal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBatal.Click
        Call kunciteks()
        Call bersih()
        Call tampil()
        btntambah.Text = "Tambah"
        BtnUbah.Text = "Ubah"
        Call BukaTombol()
    End Sub

    Private Sub btntambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            btntambah.Text = "Simpan"
            Call bersih()
            Call BukaTeks()
            txtidproduk.Focus()
            BtnUbah.Enabled = False
            BtnHapus.Enabled = False
        Else
            Try
                SQL2 = "Insert Into tbl_data Values('" & txtidproduk.Text & "','" & _
                      txtproduksi.Text & _
                      "','" & txtpagi.Text & _
                      "','" & txtsiang.Text & _
                      "','" & txtmalam.Text & _
                      "','" & txtratarata.Text & _
                      "','" & DateTimePicker1.Text & "')"
                Perintah = New OleDbCommand(SQL2, Koneksi)
                Perintah.CommandType = CommandType.Text
                Perintah.ExecuteNonQuery()
                MsgBox("Berhasil Menambah Data", MsgBoxStyle.Information, "Simpan Data")
                Call tampil()
                Call bersih()
                btntambah.Text = "Tambah"
                Call kunciteks()
                BtnUbah.Enabled = True
                BtnHapus.Enabled = True
            Catch ex As Exception
                MsgBox(ex.Message())
            End Try
        End If
    End Sub

    Private Sub BtnUbah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUbah.Click
        If txtidproduk.Text = "" Then
            MsgBox("Tidak ada data yang akan diedit." & Chr(13) & Chr(10) & _
    "Silahkan pilih terlebih dahulu data yang akan diedit.", _
                  MsgBoxStyle.Information, "Info")
        Else
            If BtnUbah.Text = "Ubah" Then
                BtnUbah.Text = "Perbaharui"
                Call BukaTeks()
                txtidproduk.Focus()
                btntambah.Enabled = False
                BtnHapus.Enabled = False
            Else
                Try
                    SQL3 = "UPDATE tbl_data SET id_produk='" & txtidproduk.Text & _
                            "',nama_produk='" & txtproduksi.Text & _
                            "',pagi_produksi='" & txtpagi.Text & _
                            "',siang_produksi='" & txtsiang.Text & _
                            "',malam_produksi='" & txtmalam.Text & _
                            "',rata_produksi='" & txtratarata.Text & _
                            "',tanggal='" & DateTimePicker1.Text & _
                            "' Where id_produk='" & txtidproduk.Text & "'"
                    Perintah3 = New OleDbCommand(SQL3, Koneksi)
                    Perintah3.CommandType = CommandType.Text
                    Perintah3.ExecuteNonQuery()
                    MsgBox("Berhasil Memperbaiki Data", MsgBoxStyle.Information, "Perbaikan Data")
                    Call tampil()
                    Call kunciteks()
                    BtnUbah.Text = "Ubah"
                    btntambah.Enabled = True
                    BtnHapus.Enabled = True
                Catch ex As Exception
                    Call tampil()
                    MsgBox(ex.Message())
                End Try
            End If
        End If
    End Sub

    Private Sub BtnHapus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnHapus.Click
        Dim pil As String
        pil = MsgBox("Anda yakin ingin menghapus data record ini?", _
                     MsgBoxStyle.YesNo, "Konfirmasi")
        If pil = vbYes Then
            Try
                SQL = "Delete * From tbl_data Where id_produk='" & _
                                       txtidproduk.Text & "'"
                Perintah = New OleDbCommand(SQL, Koneksi)
                Perintah.CommandType = CommandType.Text
                Perintah.ExecuteNonQuery()
                MsgBox("Berhasil Menghapus Data", MsgBoxStyle.Information, "Hapus Data")
                Call tampil()
                Call bersih()
            Catch ex As Exception
                MsgBox(ex.Message())
            End Try
        Else
            Exit Sub
        End If
    End Sub

    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        Try
            txtidproduk.Text = ListView1.SelectedItems(0).Text.ToString
            txtproduksi.Text = ListView1.SelectedItems(0).SubItems(1).Text.ToString
            txtpagi.Text = ListView1.SelectedItems(0).SubItems(2).Text.ToString
            txtsiang.Text = ListView1.SelectedItems(0).SubItems(3).Text.ToString
            txtmalam.Text = ListView1.SelectedItems(0).SubItems(4).Text.ToString
            txtratarata.Text = ListView1.SelectedItems(0).SubItems(5).Text.ToString
            DateTimePicker1.Text = ListView1.SelectedItems(0).SubItems(6).Text.ToString
        Catch ex As Exception
        End Try

    End Sub

End Class