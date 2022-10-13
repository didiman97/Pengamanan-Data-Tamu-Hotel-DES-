Imports System.Data.OleDb
Public Class Form_datatamu
    Sub tampil()
        SQL2 = "Select * From tbl_datatamu"
        Perintah = New OleDbCommand(SQL2, Koneksi)
        Pembaca2 = Perintah.ExecuteReader
        Dim x As Integer
        ListView1.Items.Clear()
        While Pembaca2.Read
            ListView1.Items.Add(Pembaca2("kode_tamu"))
            ListView1.Items(x).SubItems.Add(Pembaca2("nik_ktp"))
            ListView1.Items(x).SubItems.Add(Pembaca2("nama_tamu"))
            ListView1.Items(x).SubItems.Add(Pembaca2("alamat"))
            ListView1.Items(x).SubItems.Add(Pembaca2("no_hp"))
            ListView1.Items(x).SubItems.Add(Pembaca2("checkin"))
            ListView1.Items(x).SubItems.Add(Pembaca2("checkout"))
            ListView1.Items(x).SubItems.Add(Pembaca2("data_kamar"))
            x = x + 1
        End While
    End Sub
    Sub kunciteks()
        txtnikktp.Enabled = False
        txtnama.Enabled = False
        txtkamar.Enabled = False
        txtkodetamu.Enabled = False
        txtalamat.Enabled = False
        txtnohp.Enabled = False
    End Sub
    Sub BukaTeks()
        txtnikktp.Enabled = True
        txtnama.Enabled = True
        txtkamar.Enabled = True
        txtnohp.Enabled = True
        txtkodetamu.Enabled = True
        txtalamat.Enabled = True
    End Sub
    Sub bersih()
        txtnikktp.Clear()
        txtkamar.Clear()
        txtnama.Clear()
        txtnohp.Clear()
    End Sub
    Sub KunciTombol()
        btntambah.Enabled = False
        btntambah.Enabled = False
        BtnUbah.Enabled = False
        BtnHapus.Enabled = False
        BtnBatal.Enabled = False
    End Sub
    Sub BukaTombol()
        btntambah.Enabled = True
        btntambah.Enabled = True
        BtnUbah.Enabled = True
        BtnHapus.Enabled = True
        BtnBatal.Enabled = True
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()

    End Sub

    Private Sub Form_Datatamu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call Bukadatabase()
        Call tampil()
        Call kunciteks()
        SQL = "Select * From tbl_user"
        Perintah = New OleDbCommand(SQL, Koneksi)
        Pembaca = Perintah.ExecuteReader
        Dim x As Integer
        While Pembaca.Read
            label16.Text = (Pembaca("id"))
            label15.Text = (Pembaca("nama"))
            x = x + 1
        End While
    End Sub



    Private Sub btntambah_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btntambah.Click
        If btntambah.Text = "Tambah" Then
            btntambah.Text = "Simpan"
            Call bersih()
            Call BukaTeks()
            txtkodetamu.Focus()
            BtnUbah.Enabled = False
            BtnHapus.Enabled = False
        Else
            Try
                SQL2 = "Insert Into tbl_datatamu Values('" & txtkodetamu.Text & "','" & _
                      txtkodetamu.Text & _
                      "','" & txtnama.Text & _
                      "','" & txtalamat.Text & _
                      "','" & txtnohp.Text & _
                      "','" & DateTimePicker1.Text & _
                      "','" & DateTimePicker2.Text & _
                      "','" & txtkamar.Text & "')"
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
        If txtkodetamu.Text = "" Then
            MsgBox("Tidak ada data yang akan diedit." & Chr(13) & Chr(10) & _
    "Silahkan pilih terlebih dahulu data yang akan diedit.", _
                  MsgBoxStyle.Information, "Info")
        Else
            If BtnUbah.Text = "Ubah" Then
                BtnUbah.Text = "Perbaharui"
                Call BukaTeks()
                txtkodetamu.Focus()
                btntambah.Enabled = False
                BtnHapus.Enabled = False
            Else
                Try
                    SQL3 = "UPDATE tbl_datatamu SET kode_tamu='" & txtkodetamu.Text & _
                            "',nik_ktp='" & txtnikktp.Text & _
                            "',nama_tamu='" & txtnama.Text & _
                            "',alamat='" & txtalamat.Text & _
                            "',no_hp='" & txtnohp.Text & _
                            "',checkin='" & DateTimePicker1.Text & _
                            "',checkout='" & DateTimePicker2.Text & _
                            "',data_kamar='" & txtkamar.Text & _
                            "' Where kode_tamu='" & txtkodetamu.Text & "'"
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



    Private Sub ListView1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.Click
        Try
            txtkodetamu.Text = ListView1.SelectedItems(0).Text.ToString
            txtnikktp.Text = ListView1.SelectedItems(0).SubItems(1).Text.ToString
            txtnama.Text = ListView1.SelectedItems(0).SubItems(2).Text.ToString
            txtalamat.Text = ListView1.SelectedItems(0).SubItems(3).Text.ToString
            txtnohp.Text = ListView1.SelectedItems(0).SubItems(4).Text.ToString
            DateTimePicker1.Text = ListView1.SelectedItems(0).SubItems(5).Text.ToString
            DateTimePicker2.Text = ListView1.SelectedItems(0).SubItems(6).Text.ToString
            txtkamar.Text = ListView1.SelectedItems(0).SubItems(7).Text.ToString
        Catch ex As Exception
        End Try

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub BtnHapus_Click(sender As Object, e As EventArgs) Handles BtnHapus.Click
        Dim pil As String
        pil = MsgBox("Anda yakin ingin menghapus data record ini?", _
                     MsgBoxStyle.YesNo, "Konfirmasi")
        If pil = vbYes Then
            Try
                SQL = "Delete * From tbl_datatamu Where kode_tamu='" & _
                                       txtkodetamu.Text & "'"
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

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub BtnBatal_Click(sender As Object, e As EventArgs) Handles BtnBatal.Click
        txtkodetamu.Clear()
        txtnikktp.Clear()
        txtnama.Clear()
        txtnohp.Clear()
        txtkamar.Clear()
    End Sub
End Class