'
'  VB.NET translation by Ted Ehrich
'  Note: Mode is CBC
'
'  Original license from des.cpp:
'
'  FIPS-46-3 compliant 3DES implementation
'
'  Copyright (C) 2001-2003  Christophe Devine
'
'  This program is free software; you can redistribute it and/or modify
'  it under the terms of the GNU General Public License as published by
'  the Free Software Foundation; either version 2 of the License, or
'  (at your option) any later version.
'
'  This program is distributed in the hope that it will be useful,
'  but WITHOUT ANY WARRANTY; without even the implied warranty of
'  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
'  GNU General Public License for more details.
'
'  You should have received a copy of the GNU General Public License
'  along with this program; if not, write to the Free Software
'  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
'

Public Class DES
    'the eight DES S-boxes
    Dim SB1() As UInt32 = { _
    &H1010400UI, &H0UI, &H10000UI, &H1010404UI, _
    &H1010004UI, &H10404UI, &H4UI, &H10000UI, _
    &H400UI, &H1010400UI, &H1010404UI, &H400UI, _
    &H1000404UI, &H1010004UI, &H1000000UI, &H4UI, _
    &H404UI, &H1000400UI, &H1000400UI, &H10400UI, _
    &H10400UI, &H1010000UI, &H1010000UI, &H1000404UI, _
    &H10004UI, &H1000004UI, &H1000004UI, &H10004UI, _
    &H0UI, &H404UI, &H10404UI, &H1000000UI, _
    &H10000UI, &H1010404UI, &H4UI, &H1010000UI, _
    &H1010400UI, &H1000000UI, &H1000000UI, &H400UI, _
    &H1010004UI, &H10000UI, &H10400UI, &H1000004UI, _
    &H400UI, &H4UI, &H1000404UI, &H10404UI, _
    &H1010404UI, &H10004UI, &H1010000UI, &H1000404UI, _
    &H1000004UI, &H404UI, &H10404UI, &H1010400UI, _
    &H404UI, &H1000400UI, &H1000400UI, &H0UI, _
    &H10004UI, &H10400UI, &H0UI, &H1010004UI _
    }

    Dim SB2() As UInt32 = { _
    &H80108020UI, &H80008000UI, &H8000UI, &H108020UI, _
    &H100000UI, &H20UI, &H80100020UI, &H80008020UI, _
    &H80000020UI, &H80108020UI, &H80108000UI, &H80000000UI, _
    &H80008000UI, &H100000UI, &H20UI, &H80100020UI, _
    &H108000UI, &H100020UI, &H80008020UI, &H0UI, _
    &H80000000UI, &H8000UI, &H108020UI, &H80100000UI, _
    &H100020UI, &H80000020UI, &H0UI, &H108000UI, _
    &H8020UI, &H80108000UI, &H80100000UI, &H8020UI, _
    &H0UI, &H108020UI, &H80100020UI, &H100000UI, _
    &H80008020UI, &H80100000UI, &H80108000UI, &H8000UI, _
    &H80100000UI, &H80008000UI, &H20UI, &H80108020UI, _
    &H108020UI, &H20UI, &H8000UI, &H80000000UI, _
    &H8020UI, &H80108000UI, &H100000UI, &H80000020UI, _
    &H100020UI, &H80008020UI, &H80000020UI, &H100020UI, _
    &H108000UI, &H0UI, &H80008000UI, &H8020UI, _
    &H80000000UI, &H80100020UI, &H80108020UI, &H108000UI _
    }

    Dim SB3() As UInt32 = { _
    &H208UI, &H8020200UI, &H0UI, &H8020008UI, _
    &H8000200UI, &H0UI, &H20208UI, &H8000200UI, _
    &H20008UI, &H8000008UI, &H8000008UI, &H20000UI, _
    &H8020208UI, &H20008UI, &H8020000UI, &H208UI, _
    &H8000000UI, &H8UI, &H8020200UI, &H200UI, _
    &H20200UI, &H8020000UI, &H8020008UI, &H20208UI, _
    &H8000208UI, &H20200UI, &H20000UI, &H8000208UI, _
    &H8UI, &H8020208UI, &H200UI, &H8000000UI, _
    &H8020200UI, &H8000000UI, &H20008UI, &H208UI, _
    &H20000UI, &H8020200UI, &H8000200UI, &H0UI, _
    &H200UI, &H20008UI, &H8020208UI, &H8000200UI, _
    &H8000008UI, &H200UI, &H0UI, &H8020008UI, _
    &H8000208UI, &H20000UI, &H8000000UI, &H8020208UI, _
    &H8UI, &H20208UI, &H20200UI, &H8000008UI, _
    &H8020000UI, &H8000208UI, &H208UI, &H8020000UI, _
    &H20208UI, &H8UI, &H8020008UI, &H20200UI _
    }

    Dim SB4() As UInt32 = { _
    &H802001UI, &H2081UI, &H2081UI, &H80UI, _
    &H802080UI, &H800081UI, &H800001UI, &H2001UI, _
    &H0UI, &H802000UI, &H802000UI, &H802081UI, _
    &H81UI, &H0UI, &H800080UI, &H800001UI, _
    &H1UI, &H2000UI, &H800000UI, &H802001UI, _
    &H80UI, &H800000UI, &H2001UI, &H2080UI, _
    &H800081UI, &H1UI, &H2080UI, &H800080UI, _
    &H2000UI, &H802080UI, &H802081UI, &H81UI, _
    &H800080UI, &H800001UI, &H802000UI, &H802081UI, _
    &H81UI, &H0UI, &H0UI, &H802000UI, _
    &H2080UI, &H800080UI, &H800081UI, &H1UI, _
    &H802001UI, &H2081UI, &H2081UI, &H80UI, _
    &H802081UI, &H81UI, &H1UI, &H2000UI, _
    &H800001UI, &H2001UI, &H802080UI, &H800081UI, _
    &H2001UI, &H2080UI, &H800000UI, &H802001UI, _
    &H80UI, &H800000UI, &H2000UI, &H802080UI _
    }

    Dim SB5() As UInt32 = { _
    &H100UI, &H2080100UI, &H2080000UI, &H42000100UI, _
    &H80000UI, &H100UI, &H40000000UI, &H2080000UI, _
    &H40080100UI, &H80000UI, &H2000100UI, &H40080100UI, _
    &H42000100UI, &H42080000UI, &H80100UI, &H40000000UI, _
    &H2000000UI, &H40080000UI, &H40080000UI, &H0UI, _
    &H40000100UI, &H42080100UI, &H42080100UI, &H2000100UI, _
    &H42080000UI, &H40000100UI, &H0UI, &H42000000UI, _
    &H2080100UI, &H2000000UI, &H42000000UI, &H80100UI, _
    &H80000UI, &H42000100UI, &H100UI, &H2000000UI, _
    &H40000000UI, &H2080000UI, &H42000100UI, &H40080100UI, _
    &H2000100UI, &H40000000UI, &H42080000UI, &H2080100UI, _
    &H40080100UI, &H100UI, &H2000000UI, &H42080000UI, _
    &H42080100UI, &H80100UI, &H42000000UI, &H42080100UI, _
    &H2080000UI, &H0UI, &H40080000UI, &H42000000UI, _
    &H80100UI, &H2000100UI, &H40000100UI, &H80000UI, _
    &H0UI, &H40080000UI, &H2080100UI, &H40000100UI _
    }

    Dim SB6() As UInt32 = { _
    &H20000010UI, &H20400000UI, &H4000UI, &H20404010UI, _
    &H20400000UI, &H10UI, &H20404010UI, &H400000UI, _
    &H20004000UI, &H404010UI, &H400000UI, &H20000010UI, _
    &H400010UI, &H20004000UI, &H20000000UI, &H4010UI, _
    &H0UI, &H400010UI, &H20004010UI, &H4000UI, _
    &H404000UI, &H20004010UI, &H10UI, &H20400010UI, _
    &H20400010UI, &H0UI, &H404010UI, &H20404000UI, _
    &H4010UI, &H404000UI, &H20404000UI, &H20000000UI, _
    &H20004000UI, &H10UI, &H20400010UI, &H404000UI, _
    &H20404010UI, &H400000UI, &H4010UI, &H20000010UI, _
    &H400000UI, &H20004000UI, &H20000000UI, &H4010UI, _
    &H20000010UI, &H20404010UI, &H404000UI, &H20400000UI, _
    &H404010UI, &H20404000UI, &H0UI, &H20400010UI, _
    &H10UI, &H4000UI, &H20400000UI, &H404010UI, _
    &H4000UI, &H400010UI, &H20004010UI, &H0UI, _
    &H20404000UI, &H20000000UI, &H400010UI, &H20004010UI _
    }

    Dim SB7() As UInt32 = { _
    &H200000UI, &H4200002UI, &H4000802UI, &H0UI, _
    &H800UI, &H4000802UI, &H200802UI, &H4200800UI, _
    &H4200802UI, &H200000UI, &H0UI, &H4000002UI, _
    &H2UI, &H4000000UI, &H4200002UI, &H802UI, _
    &H4000800UI, &H200802UI, &H200002UI, &H4000800UI, _
    &H4000002UI, &H4200000UI, &H4200800UI, &H200002UI, _
    &H4200000UI, &H800UI, &H802UI, &H4200802UI, _
    &H200800UI, &H2UI, &H4000000UI, &H200800UI, _
    &H4000000UI, &H200800UI, &H200000UI, &H4000802UI, _
    &H4000802UI, &H4200002UI, &H4200002UI, &H2UI, _
    &H200002UI, &H4000000UI, &H4000800UI, &H200000UI, _
    &H4200800UI, &H802UI, &H200802UI, &H4200800UI, _
    &H802UI, &H4000002UI, &H4200802UI, &H4200000UI, _
    &H200800UI, &H0UI, &H2UI, &H4200802UI, _
    &H0UI, &H200802UI, &H4200000UI, &H800UI, _
    &H4000002UI, &H4000800UI, &H800UI, &H200002UI _
    }

    Dim SB8() As UInt32 = { _
    &H10001040UI, &H1000UI, &H40000UI, &H10041040UI, _
    &H10000000UI, &H10001040UI, &H40UI, &H10000000UI, _
    &H40040UI, &H10040000UI, &H10041040UI, &H41000UI, _
    &H10041000UI, &H41040UI, &H1000UI, &H40UI, _
    &H10040000UI, &H10000040UI, &H10001000UI, &H1040UI, _
    &H41000UI, &H40040UI, &H10040040UI, &H10041000UI, _
    &H1040UI, &H0UI, &H0UI, &H10040040UI, _
    &H10000040UI, &H10001000UI, &H41040UI, &H40000UI, _
    &H41040UI, &H40000UI, &H10041000UI, &H1000UI, _
    &H40UI, &H10040040UI, &H1000UI, &H41040UI, _
    &H10001000UI, &H40UI, &H10000040UI, &H10040000UI, _
    &H10040040UI, &H10000000UI, &H40000UI, &H10001040UI, _
    &H0UI, &H10041040UI, &H40040UI, &H10000040UI, _
    &H10040000UI, &H10001000UI, &H10001040UI, &H0UI, _
    &H10041040UI, &H41000UI, &H41000UI, &H1040UI, _
    &H1040UI, &H40040UI, &H10000000UI, &H10041000UI _
    }

    'PC1: left and right halves bit-swap

    Dim LHs() As UInt32 = { _
    &H0UI, &H1UI, &H100UI, &H101UI, _
    &H10000UI, &H10001UI, &H10100UI, &H10101UI, _
    &H1000000UI, &H1000001UI, &H1000100UI, &H1000101UI, _
    &H1010000UI, &H1010001UI, &H1010100UI, &H1010101UI _
    }

    Dim RHs() As UInt32 = { _
    &H0UI, &H1000000UI, &H10000UI, &H1010000UI, _
    &H100UI, &H1000100UI, &H10100UI, &H1010100UI, _
    &H1UI, &H1000001UI, &H10001UI, &H1010001UI, _
    &H101UI, &H1000101UI, &H10101UI, &H1010101UI _
    }

    'platform-independant 32-bit integer manipulation macros

    Private Sub GET_UINT32(ByRef n As UInt32, ByRef b() As Byte, ByRef i As Integer)
        n = (CUInt(b(i)) << 24) Or _
            (CUInt(b(i + 1)) << 16) Or _
            (CUInt(b(i + 2)) << 8) Or _
            (CUInt(b(i + 3)))
    End Sub

    Private Sub PUT_UINT32(ByRef n As UInt32, ByRef b() As Byte, ByRef i As Integer)
        b(i) = (n >> 24) And &HFFUI
        b(i + 1) = (n >> 16) And &HFFUI
        b(i + 2) = (n >> 8) And &HFFUI
        b(i + 3) = (n) And &HFFUI
    End Sub

    'Initial Permutation macro

    Private Sub DES_IP(ByRef X As UInt32, ByRef Y As UInt32, ByRef T As UInt32)
        T = ((X >> 4) Xor Y) And &HF0F0F0FUI : Y = Y Xor T : X = X Xor (T << 4)
        T = ((X >> 16) Xor Y) And &HFFFFUI : Y = Y Xor T : X = X Xor (T << 16)
        T = ((Y >> 2) Xor X) And &H33333333UI : X = X Xor T : Y = Y Xor (T << 2)
        T = ((Y >> 8) Xor X) And &HFF00FFUI : X = X Xor T : Y = Y Xor (T << 8)
        Y = ((Y << 1) Or (Y >> 31)) And &HFFFFFFFFUI
        T = (X Xor Y) And &HAAAAAAAAUI : Y = Y Xor T : X = X Xor T
        X = ((X << 1) Or (X >> 31)) And &HFFFFFFFFUI
    End Sub

    'Final Permutation macro

    Private Sub DES_FP(ByRef X As UInt32, ByRef Y As UInt32, ByRef T As UInt32)
        X = ((X << 31) Or (X >> 1)) And &HFFFFFFFFUI
        T = (X Xor Y) And &HAAAAAAAAUI : X = X Xor T : Y = Y Xor T
        Y = ((Y << 31) Or (Y >> 1)) And &HFFFFFFFFUI
        T = ((Y >> 8) Xor X) And &HFF00FFUI : X = X Xor T : Y = Y Xor (T << 8)
        T = ((Y >> 2) Xor X) And &H33333333UI : X = X Xor T : Y = Y Xor (T << 2)
        T = ((X >> 16) Xor Y) And &HFFFFUI : Y = Y Xor T : X = X Xor (T << 16)
        T = ((X >> 4) Xor Y) And &HF0F0F0FUI : Y = Y Xor T : X = X Xor (T << 4)
    End Sub

    'DES round macro
    'init nSK as -1
    Private Sub DES_ROUND(ByRef X As UInt32, ByRef Y As UInt32, ByRef T As UInt32, ByRef SK() As UInt32, ByRef nSK As Integer)
        nSK += 1
        T = SK(nSK) Xor X
        Y = Y Xor SB8((T) And &H3FUI) Xor _
                  SB6((T >> 8) And &H3FUI) Xor _
                  SB4((T >> 16) And &H3FUI) Xor _
                  SB2((T >> 24) And &H3FUI)

        nSK += 1
        T = SK(nSK) Xor ((X << 28) Or (X >> 4))
        Y = Y Xor SB7((T) And &H3FUI) Xor _
                  SB5((T >> 8) And &H3FUI) Xor _
                  SB3((T >> 16) And &H3FUI) Xor _
                  SB1((T >> 24) And &H3FUI)
    End Sub

    'DES key schedule
    'nSK will be -1 for DES, 31 for 2nd part of 3DES
    Private Function des_main_ks(ByRef SK() As UInt32, ByRef key() As Byte, ByRef nSK As Integer) As Integer
        Dim i As Integer
        Dim X, Y, T As UInt32

        GET_UINT32(X, key, 0)
        GET_UINT32(Y, key, 4)

        'Permuted Choice 1

        T = ((Y >> 4) Xor X) And &HF0F0F0FUI : X = X Xor T : Y = Y Xor (T << 4)
        T = ((Y) Xor X) And &H10101010UI : X = X Xor T : Y = Y Xor (T)

        X = (LHs((X) And &HFUI) << 3) Or (LHs((X >> 8) And &HFUI) << 2) _
          Or (LHs((X >> 16) And &HFUI) << 1) Or (LHs((X >> 24) And &HFUI)) _
          Or (LHs((X >> 5) And &HFUI) << 7) Or (LHs((X >> 13) And &HFUI) << 6) _
          Or (LHs((X >> 21) And &HFUI) << 5) Or (LHs((X >> 29) And &HFUI) << 4)

        Y = (RHs((Y >> 1) And &HFUI) << 3) Or (RHs((Y >> 9) And &HFUI) << 2) _
          Or (RHs((Y >> 17) And &HFUI) << 1) Or (RHs((Y >> 25) And &HFUI)) _
          Or (RHs((Y >> 4) And &HFUI) << 7) Or (RHs((Y >> 12) And &HFUI) << 6) _
          Or (RHs((Y >> 20) And &HFUI) << 5) Or (RHs((Y >> 28) And &HFUI) << 4)

        X = X And &HFFFFFFFUI
        Y = Y And &HFFFFFFFUI

        'calculate subkeys

        For i = 0 To 15
            If i < 2 OrElse i = 8 OrElse i = 15 Then
                X = ((X << 1) Or (X >> 27)) And &HFFFFFFFUI
                Y = ((Y << 1) Or (Y >> 27)) And &HFFFFFFFUI
            Else
                X = ((X << 2) Or (X >> 26)) And &HFFFFFFFUI
                Y = ((Y << 2) Or (Y >> 26)) And &HFFFFFFFUI
            End If

            nSK += 1
            SK(nSK) = ((X << 4) And &H24000000UI) Or ((X << 28) And &H10000000UI) _
                    Or ((X << 14) And &H8000000UI) Or ((X << 18) And &H2080000UI) _
                    Or ((X << 6) And &H1000000UI) Or ((X << 9) And &H200000UI) _
                    Or ((X >> 1) And &H100000UI) Or ((X << 10) And &H40000UI) _
                    Or ((X << 2) And &H20000UI) Or ((X >> 10) And &H10000UI) _
                    Or ((Y >> 13) And &H2000UI) Or ((Y >> 4) And &H1000UI) _
                    Or ((Y << 6) And &H800UI) Or ((Y >> 1) And &H400UI) _
                    Or ((Y >> 14) And &H200UI) Or ((Y) And &H100UI) _
                    Or ((Y >> 5) And &H20UI) Or ((Y >> 10) And &H10UI) _
                    Or ((Y >> 3) And &H8UI) Or ((Y >> 18) And &H4UI) _
                    Or ((Y >> 26) And &H2UI) Or ((Y >> 24) And &H1UI)

            nSK += 1
            SK(nSK) = ((X << 15) And &H20000000UI) Or ((X << 17) And &H10000000UI) _
                    Or ((X << 10) And &H8000000UI) Or ((X << 22) And &H4000000UI) _
                    Or ((X >> 2) And &H2000000UI) Or ((X << 1) And &H1000000UI) _
                    Or ((X << 16) And &H200000UI) Or ((X << 11) And &H100000UI) _
                    Or ((X << 3) And &H80000UI) Or ((X >> 6) And &H40000UI) _
                    Or ((X << 15) And &H20000UI) Or ((X >> 4) And &H10000UI) _
                    Or ((Y >> 2) And &H2000UI) Or ((Y << 8) And &H1000UI) _
                    Or ((Y >> 14) And &H808UI) Or ((Y >> 9) And &H400UI) _
                    Or ((Y) And &H200UI) Or ((Y << 7) And &H100UI) _
                    Or ((Y >> 7) And &H20UI) Or ((Y >> 3) And &H11UI) _
                    Or ((Y << 2) And &H4UI) Or ((Y >> 21) And &H2UI)
        Next

        Return 0
    End Function

    'DES encryption subkeys 
    'esk(31)
    'dsk(31)
    Private Structure des_context
        Dim esk() As UInt32
        Dim dsk() As UInt32
    End Structure

    'Triple-DES encryption subkeys
    'esk(95)
    'dsk(95)
    Private Structure des3_context
        Dim esk() As UInt32
        Dim dsk() As UInt32
    End Structure

    Private Function des_set_key(ByRef ctx As des_context, ByRef key() As Byte) As Integer
        Dim i As Integer

        'setup encryption subkeys

        des_main_ks(ctx.esk, key, -1)

        'setup decryption subkeys

        For i = 0 To 31 Step 2
            ctx.dsk(i) = ctx.esk(30 - i)
            ctx.dsk(i + 1) = ctx.esk(31 - i)
        Next

        Return 0
    End Function

    'DES 64-bit block encryption/decryption

    Private Sub des_crypt(ByRef SK() As UInt32, ByRef input() As Byte, ByRef output() As Byte)
        Dim X, Y, T As UInt32

        GET_UINT32(X, input, 0)
        GET_UINT32(Y, input, 4)

        DES_IP(X, Y, T)

        Dim nSK As Integer = -1
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)

        DES_FP(Y, X, T)

        PUT_UINT32(Y, output, 0)
        PUT_UINT32(X, output, 4)
    End Sub

    Private Sub des_encrypt(ByRef ctx As des_context, ByRef input() As Byte, ByRef output() As Byte)
        des_crypt(ctx.esk, input, output)
    End Sub

    Private Sub des_decrypt(ByRef ctx As des_context, ByRef input() As Byte, ByRef output() As Byte)
        des_crypt(ctx.dsk, input, output)
    End Sub

    'Triple-DES key schedule 

    Private Function des3_set_2keys(ByRef ctx As des3_context, ByRef key1() As Byte, ByRef key2() As Byte) As Integer
        Dim i As Integer
        des_main_ks(ctx.esk, key1, -1)
        des_main_ks(ctx.dsk, key2, 31)

        For i = 0 To 31 Step 2
            ctx.dsk(i) = ctx.esk(30 - i)
            ctx.dsk(i + 1) = ctx.esk(31 - i)

            ctx.esk(i + 32) = ctx.dsk(62 - i)
            ctx.esk(i + 33) = ctx.dsk(63 - i)

            ctx.esk(i + 64) = ctx.esk(i)
            ctx.esk(i + 65) = ctx.esk(1 + i)

            ctx.dsk(i + 64) = ctx.dsk(i)
            ctx.dsk(i + 65) = ctx.dsk(1 + i)
        Next

        Return 0
    End Function

    'Triple-DES 64-bit block encryption/decryption 

    Private Sub des3_crypt(ByRef SK() As UInt32, ByRef input() As Byte, ByRef output() As Byte)
        Dim X, Y, T As UInt32

        GET_UINT32(X, input, 0)
        GET_UINT32(Y, input, 4)

        DES_IP(X, Y, T)

        Dim nSK As Integer = -1

        'encrypt
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)

        'decrypt
        DES_ROUND(X, Y, T, SK, nSK) : DES_ROUND(Y, X, T, SK, nSK)
        DES_ROUND(X, Y, T, SK, nSK) : DES_ROUND(Y, X, T, SK, nSK)
        DES_ROUND(X, Y, T, SK, nSK) : DES_ROUND(Y, X, T, SK, nSK)
        DES_ROUND(X, Y, T, SK, nSK) : DES_ROUND(Y, X, T, SK, nSK)
        DES_ROUND(X, Y, T, SK, nSK) : DES_ROUND(Y, X, T, SK, nSK)
        DES_ROUND(X, Y, T, SK, nSK) : DES_ROUND(Y, X, T, SK, nSK)
        DES_ROUND(X, Y, T, SK, nSK) : DES_ROUND(Y, X, T, SK, nSK)
        DES_ROUND(X, Y, T, SK, nSK) : DES_ROUND(Y, X, T, SK, nSK)

        'encrypt
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)
        DES_ROUND(Y, X, T, SK, nSK) : DES_ROUND(X, Y, T, SK, nSK)

        DES_FP(Y, X, T)

        PUT_UINT32(Y, output, 0)
        PUT_UINT32(X, output, 4)
    End Sub

    Private Sub des3_encrypt(ByRef ctx As des3_context, ByRef input() As Byte, ByRef output() As Byte)
        des3_crypt(ctx.esk, input, output)
    End Sub

    Private Sub des3_decrypt(ByRef ctx As des3_context, ByRef input() As Byte, ByRef output() As Byte)
        des3_crypt(ctx.dsk, input, output)
    End Sub

    Public Sub encrypt_3des(ByVal key() As Byte, ByVal input() As Byte, ByVal inputLength As Integer, ByRef output() As Byte, ByRef outputLength As Integer)
        Dim in_(7) As Byte
        Dim out(7) As Byte
        Dim ctx As des3_context
        ReDim ctx.dsk(95)
        ReDim ctx.esk(95)
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim temp() As Byte

        temp = output

        'Set encryption keys
        Dim key2(7) As Byte
        Array.Copy(key, 8, key2, 0, 8)
        des3_set_2keys(ctx, key, key2)

        'clear buffers
        '(not necessary)

        'do for each 8 byte block of input
        For i = 0 To (inputLength / 8) - 1
            'copy 8 bytes from input buffer to in
            Array.Copy(input, i * 8, in_, 0, 8)

            'xor with ciphered block -1 
            For j = 0 To 7
                in_(j) = in_(j) Xor out(j)
            Next

            '3DES encryption
            des3_encrypt(ctx, in_, out)

            'copy encrypted block to output
            Array.Copy(out, 0, temp, i * 8, 8)
        Next
        outputLength = inputLength
    End Sub

    Public Sub decrypt_3des(ByVal key() As Byte, ByVal input() As Byte, ByVal inputLength As Integer, ByRef output() As Byte, ByRef outputLength As Integer)
        Dim ctx As des3_context
        ReDim ctx.dsk(95)
        ReDim ctx.esk(95)

        Dim in_(7) As Byte
        Dim out(7) As Byte
        Dim in2(7) As Byte

        Dim temp() As Byte

        Dim i As Integer = 0
        Dim j As Integer = 0

        temp = output

        'Set encryption keys
        Dim key2(7) As Byte
        Array.Copy(key, 8, key2, 0, 8)
        des3_set_2keys(ctx, key, key2)

        'clear buffers
        '(not necessary)

        'do for each 8 byte block of input
        For i = 0 To (inputLength / 8) - 1
            'copy 8 bytes from input buffer to in
            Array.Copy(input, i * 8, in_, 0, 8)

            '3DES encryption
            des3_decrypt(ctx, in_, out)

            'xor with ciphered block -1 
            For j = 0 To 7
                out(j) = out(j) Xor in2(j)
            Next

            Array.Copy(input, i * 8, in2, 0, 8)

            'copy encrypted block to output
            Array.Copy(out, 0, temp, i * 8, 8)
        Next
        outputLength = inputLength
    End Sub

    Public Sub encrypt_des(ByVal key() As Byte, ByVal input() As Byte, ByVal inputLength As Integer, ByRef output() As Byte, ByRef outputLength As Integer)
        Dim in_(7) As Byte
        Dim out(7) As Byte
        Dim ctx As des_context
        ReDim ctx.dsk(31)
        ReDim ctx.esk(31)
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim temp() As Byte

        temp = output

        'Set encryption keys
        des_set_key(ctx, key)

        'clear buffers
        '(not necessary)

        'do for each 8 byte block of input
        For i = 0 To (inputLength / 8) - 1
            'copy 8 bytes from input buffer to in
            Array.Copy(input, i * 8, in_, 0, 8)

            'xor with ciphered block -1 
            For j = 0 To 7
                in_(j) = in_(j) Xor out(j)
            Next

            'DES encryption
            des_encrypt(ctx, in_, out)

            'copy encrypted block to output
            Array.Copy(out, 0, temp, i * 8, 8)
        Next
        outputLength = inputLength
    End Sub

    Public Sub decrypt_des(ByVal key() As Byte, ByVal input() As Byte, ByVal inputLength As Integer, ByRef output() As Byte, ByRef outputLength As Integer)
        Dim ctx As des_context
        ReDim ctx.dsk(31)
        ReDim ctx.esk(31)

        Dim in_(7) As Byte
        Dim out(7) As Byte
        Dim in2(7) As Byte

        Dim temp() As Byte

        Dim i As Integer = 0
        Dim j As Integer = 0

        temp = output

        'Set encryption keys
        des_set_key(ctx, key)

        'clear buffers
        '(not necessary)

        'do for each 8 byte block of input
        For i = 0 To (inputLength / 8) - 1
            'copy 8 bytes from input buffer to in
            Array.Copy(input, i * 8, in_, 0, 8)

            'DES encryption
            des_decrypt(ctx, in_, out)

            'xor with ciphered block -1 
            For j = 0 To 7
                out(j) = out(j) Xor in2(j)
            Next

            Array.Copy(input, i * 8, in2, 0, 8)

            'copy encrypted block to output
            Array.Copy(out, 0, temp, i * 8, 8)
        Next
        outputLength = inputLength
    End Sub
End Class
