Imports System.ComponentModel

Public Class ReaderForm

    Private myReaders As New List(Of Reader)

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Readers As List(Of Reader)
        Get
            Return myReaders
        End Get
        Set(value As List(Of Reader))
            myReaders = value
        End Set
    End Property

    Private Sub ReaderForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If myReaders.Count = 0 Then
            ReaderList.Items.Add("new reader")
            ReaderList.SelectedIndex = 0
            ReaderItem.Focus()
        Else
            ParseReaders()
        End If
    End Sub

    Private Sub ParseReaders()
        ReaderList.Items.Clear()
        For Each r As Reader In myReaders
            Dim thisItem As String = $"{r.Name}={r.ConnectionString}"
            ReaderList.Items.Add(thisItem, r.isDefault)
        Next
    End Sub

    Private Function SetReaders() As List(Of Reader)
        If ReaderList.CheckedItems.Count > 1 Then
            MsgBox("You have more than one default reader checked.  Only the first will be used.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Information, "Note")
        End If
        Dim newReaders As New List(Of Reader)
        For Each s As String In ReaderList.Items
            Dim myName As String = ""
            Dim myConnection As String = ""
            Dim myDefault As Boolean = False
            If ReaderList.CheckedItems.IndexOf(s) >= 0 Then
                myDefault = True
            Else
                myDefault = False
            End If
            Dim sp As String() = s.Split("=")
            myName = sp(0)
            myConnection = sp(1)
            Dim thisReader As New Reader
            thisReader.Name = myName
            thisReader.ConnectionString = myConnection
            thisReader.isDefault = myDefault
            newReaders.Add(thisReader)
        Next
        Return newReaders
    End Function

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        ReaderList.Items(ReaderList.SelectedIndex) = ReaderItem.Text.Trim
        ReaderItem.Text = ""
    End Sub

    Private Sub AddReader_Click(sender As Object, e As EventArgs) Handles AddReader.Click
        ReaderList.Items.Add("new reader")
        ReaderList.SelectedIndex = ReaderList.Items.IndexOf("new reader")
        ReaderItem.Focus()
    End Sub

    Private Sub EditReader_Click(sender As Object, e As EventArgs) Handles EditReader.Click
        Dim selectedIndex As Integer = ReaderList.SelectedIndex
        If selectedIndex = -1 Then Exit Sub
        ReaderItem.Text = ReaderList.Items(selectedIndex)
        ReaderItem.Focus()
    End Sub

    Private Sub DeleteReader_Click(sender As Object, e As EventArgs) Handles DeleteReader.Click
        Dim SelectedIndex As Integer = ReaderList.SelectedIndex
        ReaderList.Items.RemoveAt(SelectedIndex)
        If ReaderList.Items.Count > 0 Then
            ReaderList.SelectedIndex = 0
        Else
            ReaderList.SelectedIndex = -1
        End If
    End Sub

    Private Sub ReaderForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        myReaders = SetReaders()
    End Sub
End Class