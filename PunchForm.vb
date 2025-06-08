Imports System.ComponentModel

Public Class PunchForm

    Private myPunches As New List(Of Punch)

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    Public Property Punches As List(Of Punch)
        Get
            Return myPunches
        End Get
        Set(value As List(Of Punch))
            myPunches = value
        End Set
    End Property
    Private Sub PunchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If myPunches.Count = 0 Then
            PunchList.Items.Add("new Punch")
            PunchList.SelectedIndex = 0
            PunchItem.Focus()
        Else
            ParsePunches()
        End If
    End Sub

    Private Sub ParsePunches()
        PunchList.Items.Clear()
        For Each r As Punch In myPunches
            Dim thisItem As String = $"{r.Name}={r.ConnectionString}"
            PunchList.Items.Add(thisItem)
        Next
    End Sub

    Private Function SetPunches() As List(Of Punch)

        Dim newPunchs As New List(Of Punch)
        For Each s As String In PunchList.Items
            Dim myName As String = ""
            Dim myConnection As String = ""
            Dim myDefault As Boolean = False

            Dim sp As String() = s.Split("=")
            myName = sp(0)
            myConnection = sp(1)
            Dim thisPunch As New Punch
            thisPunch.Name = myName
            thisPunch.ConnectionString = myConnection
            newPunchs.Add(thisPunch)
        Next
        Return newPunchs
    End Function

    Private Sub cmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        PunchList.Items(PunchList.SelectedIndex) = PunchItem.Text.Trim
        PunchItem.Text = ""
    End Sub

    Private Sub AddPunch_Click(sender As Object, e As EventArgs) Handles AddPunch.Click
        PunchList.Items.Add("new Punch")
        PunchList.SelectedIndex = PunchList.Items.IndexOf("new Punch")
        PunchItem.Focus()
    End Sub

    Private Sub EditPunch_Click(sender As Object, e As EventArgs) Handles EditPunch.Click
        Dim selectedIndex As Integer = PunchList.SelectedIndex
        If selectedIndex = -1 Then Exit Sub
        PunchItem.Text = PunchList.Items(selectedIndex)
        PunchItem.Focus()
    End Sub

    Private Sub DeletePunch_Click(sender As Object, e As EventArgs) Handles DeletePunch.Click
        Dim SelectedIndex As Integer = PunchList.SelectedIndex
        PunchList.Items.RemoveAt(SelectedIndex)
        If PunchList.Items.Count > 0 Then
            PunchList.SelectedIndex = 0
        Else
            PunchList.SelectedIndex = -1
        End If
    End Sub

    Private Sub PunchForm_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        myPunches = SetPunches()
    End Sub
End Class