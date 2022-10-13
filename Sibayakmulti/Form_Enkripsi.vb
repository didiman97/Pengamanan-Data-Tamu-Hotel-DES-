Imports System.Data.OleDb
Imports System.Security.Cryptography
Public Class Form_Enkripsi
    Dim outputLength As Integer 'not used here, DES class returns as number of bytes processed

    Dim desProv As New DESCryptoServiceProvider()

    Dim myDes As New DES()
    Dim iv(7) As Byte 'blank, not supported by DES class
    Dim key8() As Byte 'DES key
    Dim input() As Byte
    Dim output() As Byte
    Sub tampil()
        SQL2 = "Select * From tbl_datatamu"
        Perintah = New OleDbCommand(SQL2, Koneksi)
        Pembaca = Perintah.ExecuteReader
        Dim x As Integer
        ListView1.Items.Clear()
        While Pembaca.Read
            ListView1.Items.Add(Pembaca("kode_tamu"))
            ListView1.Items(x).SubItems.Add(Pembaca("nik_ktp"))
            ListView1.Items(x).SubItems.Add(Pembaca("nama_tamu"))
            ListView1.Items(x).SubItems.Add(Pembaca("alamat"))
            ListView1.Items(x).SubItems.Add(Pembaca("no_hp"))
            ListView1.Items(x).SubItems.Add(Pembaca("checkin"))
            ListView1.Items(x).SubItems.Add(Pembaca("checkout"))
            ListView1.Items(x).SubItems.Add(Pembaca("data_kamar"))
            x = x + 1
        End While
    End Sub
    Private Function GetHexString(ByVal bytes() As Byte, Optional ByVal len As Integer = -1, Optional ByVal spaces As Boolean = False) As String
        If len = -1 Then len = bytes.Length
        Dim i As Integer
        Dim s As String = ""
        For i = 0 To len - 1
            s += bytes(i).ToString("x2")
            If spaces Then s += " "
        Next
        If spaces Then s = s.TrimEnd()
        Return s
    End Function
    Function HexStringToBytes(ByVal hexstring As String) As Byte()
        Dim out((hexstring.Length / 2) - 1) As Byte
        For i = 0 To (hexstring.Length / 2) - 1
            out(i) = Convert.ToByte(hexstring.Substring(i * 2, 2), 16)
        Next
        Return out
    End Function
    Public Function StrToHex(ByRef Data As String) As String
        Dim sVal As String
        Dim sHex As String = ""
        While Data.Length > 0
            sVal = Conversion.Hex(Strings.Asc(Data.Substring(0, 1).ToString()))
            Data = Data.Substring(1, Data.Length - 1)
            sHex = sHex & sVal
        End While
        Return sHex
    End Function
    Private Sub SetupBytes()
        'If (InputTextBox.Text.Length / 2) Mod 8 <> 0 Then Throw New Exception("Input length not multiple of 8")
        'If DESKeyTextBox.Text.Length <> (8 * 2) Then Throw New Exception("DES key length not 8")
        'If DES3KeyTextBox.Text.Length <> (16 * 2) Then Throw New Exception("DES3 key length not 16")

        key8 = HexStringToBytes(DES3KeyTextBox.Text)
        'key16 = HexStringToBytes(DES3KeyTextBox.Text)
        'Input = HexStringToBytes(InputTextBox.Text)
        'ReDim output(input.Length - 1)
    End Sub

    Private Sub Form_Enkripsi_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Call Bukadatabase()
        Call tampil()
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


    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim tStr As String '= Me.TextBox4.Text
        Dim tStrA() As String
        Dim tStrD As String = ""
        Dim tStrH As String = ""
        Dim i As Integer
        Dim PanjangAwal As Single
        Dim tIntPenambah As Integer
        Dim tStrPenambah As String
        Dim StrAngkaPenambah As String

        PanjangAwal = Me.TextBox1.TextLength

        If PanjangAwal < 8 Then


            tIntPenambah = 8 - PanjangAwal

            If Len(tIntPenambah) < 2 Then
                StrAngkaPenambah = "0" & tIntPenambah

            Else
                StrAngkaPenambah = tIntPenambah
            End If

            For Ulang = 1 To tIntPenambah
                tStrPenambah = tStrPenambah & "0"
            Next

            tStr = Me.TextBox1.Text & tStrPenambah



            For Each ch As Char In tStr
                i = Asc(ch)
                'tStrD &= i.ToString.PadLeft(2, "0") & " "
                tStrD = tStrD & i.ToString.PadLeft(2, "0") & " "
            Next

            tStrA = tStrD.Split(" ")
            For i = 0 To UBound(tStrA)
                If tStrA(i) <> "" Then

                    If Len(Hex(tStrA(i))) < 2 Then
                        'tStrH &= "0" & Hex(tStrA(i)) & " "
                        tStrH = tStrH & "0" & Hex(tStrA(i)) & " "
                    Else
                        'tStrH &= Hex(tStrA(i)) & " "
                        tStrH = tStrH & Hex(tStrA(i)) & " "
                    End If

                End If

            Next

            Me.DES3KeyTextBox.Text = Replace(tStrH, " ", "")



        Else
            tStr = Me.TextBox1.Text '& tStrPenambah

            For Each ch As Char In tStr
                i = Asc(ch)
                'tStrD &= i.ToString.PadLeft(2, "0") & " "
                tStrD = tStrD & i.ToString.PadLeft(2, "0") & " "
            Next

            tStrA = tStrD.Split(" ")
            For i = 0 To UBound(tStrA)
                If tStrA(i) <> "" Then

                    If Len(Hex(tStrA(i))) < 2 Then
                        'tStrH &= "0" & Hex(tStrA(i)) & " "
                        tStrH = tStrH & "0" & Hex(tStrA(i)) & " "
                    Else
                        'tStrH &= Hex(tStrA(i)) & " "
                        tStrH = tStrH & Hex(tStrA(i)) & " "
                    End If

                End If

            Next

            Me.DES3KeyTextBox.Text = Replace(tStrH, " ", "")
        End If
    End Sub


    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click


        SQL2 = "DELETE * FROM tbl_datatamu"
        Perintah = New OleDbCommand(SQL2, Koneksi)
        Perintah.CommandType = CommandType.Text
        Perintah.ExecuteNonQuery()

        Dim txtkodetamu As String
        Dim txtnikktp As String
        Dim txtnama As String
        Dim txtalamat As String
        Dim txtnohp As String
        Dim katakunci As String
        Dim DateTimePicker1 As String
        Dim DateTimePicker2 As String
        Dim txtkamar As String

        For i = 0 To ListView1.Items.Count - 1
            txtkodetamu = ListView1.Items(i).Text.ToString
            txtnikktp = ListView1.Items(i).SubItems(1).Text.ToString
            txtnama = ListView1.Items(i).SubItems(2).Text.ToString
            txtalamat = ListView1.Items(i).SubItems(3).Text.ToString
            txtnohp = ListView1.Items(i).SubItems(4).Text.ToString
            DateTimePicker1 = ListView1.Items(i).SubItems(5).Text.ToString
            DateTimePicker2 = ListView1.Items(i).SubItems(6).Text.ToString
            txtkamar = ListView1.Items(i).SubItems(7).Text.ToString
            katakunci = TextBox1.Text
            Try
                SQL2 = "Insert Into tbl_datatamu Values('" & txtkodetamu & _
                     "','" & txtnikktp & _
                     "','" & txtnama & _
                     "','" & txtalamat & _
                     "','" & txtnohp & _
                     "','" & DateTimePicker1 & _
                     "','" & DateTimePicker2 & _
                     "','" & txtkamar & "')"
                Perintah = New OleDbCommand(SQL2, Koneksi)
                Perintah.CommandType = CommandType.Text
                Perintah.ExecuteNonQuery()



            Catch ex As Exception
                MsgBox(ex.Message())
            End Try
        Next
        Using Perintah As New OleDbCommand("INSERT INTO tbl_log VALUES('" &
                                               ListView1.Items(0).Text & "','" &
                                               "Berhasil MengDekripsi Data" & "','" &
                                                "-" & "','" &
                                                DateTime.Now & "','" & TextBox1.Text & "')", Koneksi)
            Perintah.ExecuteNonQuery()
        End Using
        MsgBox("Berhasil Menambah Data", MsgBoxStyle.Information, "Simpan Data")
    End Sub

    Private Sub btnenkripsemua_Click(sender As Object, e As EventArgs) Handles btnenkripsemua.Click
        If TextBox1.Text = "" Then
            MessageBox.Show("Isi kata kunci")
        Else

            For i = 0 To ListView1.Items.Count - 1
                For x = 0 To ListView1.Columns.Count - 1
                    'MessageBox.Show(ListView1.Items(i).SubItems(x).Text)
                    Dim teks As String
                    Dim hexateks As String
                    teks = ListView1.Items(i).SubItems(x).Text.ToString
                    hexateks = StrToHex(teks)
                    Dim hitunghasil As Integer
                    hitunghasil = (hexateks.ToString.Length / 2) Mod 8

                    If hitunghasil <> 0 Then
                        Dim tStr As String '= Me.TextBox4.Text
                        Dim tStrD As String = ""
                        Dim tStrH As String = ""
                        'Dim i As Integer
                        Dim PanjangAwal As Single
                        Dim tIntPenambah As Integer
                        Dim inputhex As String
                        PanjangAwal = ListView1.Items(i).SubItems(x).Text.Length
                        If PanjangAwal < 8 Then '1-8
                            tIntPenambah = 8 - PanjangAwal

                            For Ulang = 1 To tIntPenambah
                                tStrD += "0"
                            Next

                            tStr = ListView1.Items(i).SubItems(x).Text & tStrD
                        ElseIf PanjangAwal > 8 And PanjangAwal < 16 Then '9 - 15
                            tIntPenambah = 16 - PanjangAwal

                            For Ulang = 1 To tIntPenambah
                                tStrD += "0"
                            Next

                            tStr = ListView1.Items(i).SubItems(x).Text & tStrD

                        ElseIf PanjangAwal > 16 And PanjangAwal < 24 Then '17 - 23
                            tIntPenambah = 24 - PanjangAwal

                            For Ulang = 1 To tIntPenambah
                                tStrD += "0"
                            Next

                            tStr = ListView1.Items(i).SubItems(x).Text & tStrD
                        ElseIf PanjangAwal > 24 And PanjangAwal < 32 Then '25 - 31
                            tIntPenambah = 32 - PanjangAwal

                            For Ulang = 1 To tIntPenambah
                                tStrD += "0"
                            Next

                            tStr = ListView1.Items(i).SubItems(x).Text & tStrD
                        ElseIf PanjangAwal > 32 And PanjangAwal < 40 Then
                            tIntPenambah = 40 - PanjangAwal

                            For Ulang = 1 To tIntPenambah
                                tStrD += "0"
                            Next

                            tStr = ListView1.Items(i).SubItems(x).Text & tStrD
                        Else
                            MessageBox.Show("Kata terlalu panjang untuk di enkripsi")
                        End If

                        SetupBytes()
                        inputhex = StrToHex(tStr)
                        input = HexStringToBytes(inputhex)
                        ReDim output(input.Length - 1)

                        myDes.encrypt_des(key8, input, input.Length, output, outputLength)
                        Dim hasilfinal As String = GetHexString(output)
                        ListView1.Items(i).SubItems(x).Text = hasilfinal

                    Else

                        SetupBytes()
                        input = HexStringToBytes(hexateks)
                        ReDim output(input.Length - 1)

                        myDes.encrypt_des(key8, input, input.Length, output, outputLength)
                        ListView1.Items(i).SubItems(x).Text = GetHexString(output)
                    End If
                Next

            Next
        End If

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        End
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Form_datatamu.Show()
        Me.Close()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Form_dekripsi.Show()
        Me.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Form_Laporan.Show()
        Me.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs)
        Form_biodata.Show()
        Me.Close()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

End Class