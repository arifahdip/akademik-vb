Imports System.Text.RegularExpressions
Public Class Validasi
    Private Sub Form2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        TxtEmail.Focus()
    End Sub

    Private Function ValidEmail(ByVal value As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        If Regex.Match(value, pattern).Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function ValidURL(ByVal value As String) As Boolean
        Dim pattern As String = "^(http|https|ftp|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$"
        If Regex.Match(value, pattern).Success Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub BtnEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEmail.Click
        If ValidEmail(TxtEmail.Text) = True Then
            MsgBox("Email Anda Valid")
        Else
            MsgBox("Email Anda tidak Valid")
            TxtEmail.Focus()
        End If
    End Sub

    Private Sub BtnURL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnURL.Click
        If ValidURL(txtURL.Text) = True Then
            MsgBox("URL Valid")
        Else
            MsgBox("URL tidak Valid")
            txtURL.Focus()
        End If
    End Sub

    Private Sub TxtEmail_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtEmail.Validating
        If ValidEmail(TxtEmail.Text) = False Then
            MsgBox("Email tidak valid", MsgBoxStyle.Exclamation, "Peringatan")
            TxtEmail.Text = ""
        End If
    End Sub

    Private Sub txtURL_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtURL.Validating
        If ValidURL(txtURL.Text) = False Then
            MsgBox("Website tidak valid", MsgBoxStyle.Exclamation, "Peringatan")
            txtURL.Text = ""
        End If
    End Sub
End Class