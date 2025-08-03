Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing.Imaging.Effects
Imports System.IO
Imports System.Net.NetworkInformation
Imports System.Runtime.CompilerServices
Imports System.Runtime.Serialization
Imports System.Text.RegularExpressions
Imports System.Threading

Public Class Form1
    Private cardDeck As New List(Of String)
    Private cardPos As Integer = 0
    Private currentIndex As Integer = 0
    Private programCardIndex As Integer = -1
    Private Const MaxColumns As Integer = 80
    Private Const ProgramCardPrefix As String = "#PROGRAM:"
    Private lastOpenedFile As String = String.Empty
    Private ReaderList As New List(Of Reader)
    Private PunchList As New List(Of Punch)

    Private Function GetReaderConfig() As List(Of String)
        Dim rlist As New List(Of String)
        If Not File.Exists("readers.cfg") Then
            Dim fs As FileStream = File.Create("readers.cfg")
            fs.Close()
        End If
        Using tr As New StreamReader("readers.cfg")
            While Not tr.EndOfStream
                rlist.Add(tr.ReadLine)
            End While
        End Using
        Return rlist
    End Function
    Private Function GetPunchConfig() As List(Of String)
        Dim rlist As New List(Of String)
        If Not File.Exists("punches.cfg") Then
            Dim fs As FileStream = File.Create("punches.cfg")
            fs.Close()
        End If
        Using tr As New StreamReader("punches.cfg")
            rlist.Add(tr.ReadLine)
        End Using
        Return rlist
    End Function

    Private Function ResetPunches(sl As List(Of String)) As List(Of Punch)
        Dim newPunches As New List(Of Punch)
        If sl.Count = 0 Then Return newPunches
        For Each s As String In sl
            If s IsNot Nothing Then
                Dim myName As String = ""
                Dim myConnection As String = ""
                Dim sp As String() = s.Split("=")
                myName = sp(0)
                myConnection = sp(1)
                Dim thisPunch As New Punch
                thisPunch.Name = myName
                thisPunch.ConnectionString = myConnection
                newPunches.Add(thisPunch)
            End If
        Next
        Return newPunches
    End Function

    Private Function ResetReaders(sl As List(Of String)) As List(Of Reader)
        Dim newReaders As New List(Of Reader)
        For Each s As String In sl
            Dim myName As String = ""
            Dim myConnection As String = ""
            Dim myDefault As Boolean = False
            Dim sp As String() = s.Split("=")
            myName = sp(0)
            myConnection = sp(1)
            If sp(2) = "Y" Then
                myDefault = True
            Else
                myDefault = False
            End If
            Dim thisReader As New Reader
            thisReader.Name = myName
            thisReader.ConnectionString = myConnection
            thisReader.isDefault = myDefault
            newReaders.Add(thisReader)
        Next
        Return newReaders
    End Function

    Private Sub SaveConfig()
        Dim sl As New List(Of String)
        For Each r As Reader In ReaderList
            Dim IsDefault As String = "N"
            If r.isDefault Then IsDefault = "Y" Else IsDefault = "N"
            sl.Add($"{r.Name}={r.ConnectionString}={IsDefault}")
        Next
        Using tr As New StreamWriter("readers.cfg", False)
            For Each s As String In sl
                tr.WriteLine(s)
            Next
        End Using
        sl.Clear()
        For Each p As Punch In PunchList
            sl.Add($"{p.Name}={p.ConnectionString}")
        Next
        Using tr As New StreamWriter("punches.cfg", False)
            For Each s As String In sl
                tr.WriteLine(s)
            Next
        End Using
    End Sub

    Private Sub KeypunchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim RString As List(Of String) = GetReaderConfig()
        ReaderList = ResetReaders(RString)
        PopulateReaders()
        Dim PString As List(Of String) = GetPunchConfig()
        PunchList = ResetPunches(PString)
        'Stop
        ActivatePunches()
        txtCard.MaxLength = MaxColumns
        Try
            Dim fontPath = Path.Combine(Application.StartupPath, "fonts", "Line Printer.ttf")
            If File.Exists(fontPath) Then
                Dim pfc As New Drawing.Text.PrivateFontCollection()
                pfc.AddFontFile(fontPath)
                ListBox1.Font = New Font(pfc.Families(0), 12)
            Else
                ListBox1.Font = New Font("Consolas", 10)
            End If
        Catch
            ListBox1.Font = New Font("Consolas", 10)
        End Try
        sysTimer.Start()
        UpdateDisplay()
        btnNew.PerformClick()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        cardDeck.Clear()
        cardDeck.Add(New String(""))
        currentIndex = 0
        programCardIndex = -1
        lastOpenedFile = String.Empty
        ListBox1.Items.Clear()
        UpdateDisplay()
    End Sub

    Private Sub btnPrev_Click(sender As Object, e As EventArgs) Handles btnPrev.Click
        If currentIndex > 0 Then
            SaveCurrentCard()
            currentIndex -= 1
            UpdateDisplay()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        SaveCurrentCard()

        If currentIndex < cardDeck.Count - 1 Then
            currentIndex += 1
        Else
            cardDeck.Add(New String(""))
            currentIndex = cardDeck.Count - 1
        End If
        UpdateDisplay()
    End Sub

    Private Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        SaveCurrentCard()
        cardDeck.Insert(currentIndex, New String(""))
        ListBox1.Items.Insert(currentIndex, New String(""))
        UpdateDisplay()
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If cardDeck.Count > 1 Then
            cardDeck.RemoveAt(currentIndex)
            ListBox1.Items.RemoveAt(currentIndex)
            If currentIndex >= cardDeck.Count Then currentIndex = cardDeck.Count - 1
            UpdateDisplay()
        End If
    End Sub

    Private Sub btnDuplicate_Click(sender As Object, e As EventArgs) Handles btnDup.Click
        SaveCurrentCard()
        cardDeck.Insert(currentIndex + 1, cardDeck(currentIndex))
        ListBox1.Items.Insert(currentIndex + 1, cardDeck(currentIndex))
        currentIndex += 1
        UpdateDisplay()
    End Sub

    Private Sub SaveCurrentCard()
        If currentIndex >= 0 AndAlso currentIndex < cardDeck.Count Then
            Dim text As String = txtCard.Text
            cardDeck(currentIndex) = text
            Dim lbCount As Integer = ListBox1.Items.Count - 1
            If currentIndex > lbCount Then
                ListBox1.Items.Add(text)
            Else
                ListBox1.Items(currentIndex) = text
            End If
        End If
    End Sub

    Private Sub UpdateDisplay()
        If currentIndex >= 0 AndAlso currentIndex < cardDeck.Count Then
            Dim cardText = cardDeck(currentIndex)
            If cardText.StartsWith(ProgramCardPrefix) Then

            Else
                txtCard.Text = cardText.TrimEnd
                lblStatus.Text = $"Card {currentIndex + 1} / {cardDeck.Count}"
                If (ListBox1.Items.Count - 1) >= currentIndex Then
                    ListBox1.SelectedIndex = currentIndex
                Else
                    ListBox1.Items.Add("")
                    ListBox1.SelectedIndex = currentIndex
                End If
            End If
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveCurrentCard()

        If String.IsNullOrEmpty(lastOpenedFile) Then
            btnSaveAs.PerformClick()
        Else
            File.WriteAllLines(lastOpenedFile, cardDeck)
            Beep()
        End If
    End Sub

    Private Sub btnSaveAs_Click(sender As Object, e As EventArgs) Handles btnSaveAs.Click
        SaveCurrentCard()

        Using sfd As New SaveFileDialog
            sfd.Filter = "JCL Files (*.jcl)|*.jcl|Text Files (*.txt)|*.txt"
            If sfd.ShowDialog = DialogResult.OK Then
                File.WriteAllLines(sfd.FileName, cardDeck)
                lastOpenedFile = sfd.FileName
            End If
        End Using
        Beep()
    End Sub

    Private Sub btnOpen_Click(sender As Object, e As EventArgs) Handles btnOpen.Click
        Using ofd As New OpenFileDialog
            ofd.Filter = "JCL Files (*.jcl)|*.jcl|Text Files (*.txt)|*.txt"
            If ofd.ShowDialog = DialogResult.OK Then
                ' Set current directory to the file's folder
                Environment.CurrentDirectory = Path.GetDirectoryName(ofd.FileName)

                Dim lines = File.ReadAllLines(ofd.FileName).ToList
                cardDeck = lines.Select(Function(l) l).ToList
                currentIndex = 0
                lastOpenedFile = ofd.FileName
                UpdateDisplay()
                ListBox1.Items.Clear()

                For Each s In cardDeck
                    ListBox1.Items.Add(s)
                    ListBox1.SelectedIndex = 0
                    Application.DoEvents()
                Next
            End If
        End Using
    End Sub


    Private Sub txtCard_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCard.KeyPress
        Dim thisKey As Char = e.KeyChar
        If capslock.Checked Then
            thisKey = Char.ToUpper(thisKey)
        End If
        e.KeyChar = thisKey
        cardPos = txtCard.SelectionStart
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCard.KeyDown
        If e.KeyCode = Keys.Tab Then
            e.SuppressKeyPress = True ' prevent focus change

            Dim tb As TextBox = CType(sender, TextBox)
            Dim pos As Integer = tb.SelectionStart

            ' Calculate next 10-character boundary
            Dim nextTabStop As Integer = ((pos \ 10) + 1) * 10
            Dim spacesNeeded As Integer = nextTabStop - pos

            ' Check how many spaces are needed that don't overwrite existing text
            Dim insertSpaces As String = ""

            For i As Integer = 0 To spacesNeeded - 1
                Dim index As Integer = pos + i
                If index >= tb.TextLength OrElse tb.Text(index) = " "c Then
                    insertSpaces &= " "
                Else
                    Exit For ' Stop if non-space character would be overwritten
                End If
            Next

            ' Insert the calculated number of safe spaces
            tb.Text = tb.Text.Insert(pos, insertSpaces)
            tb.SelectionStart = pos + insertSpaces.Length
        End If
    End Sub

    Private Sub sysTimer_Tick(sender As Object, e As EventArgs) Handles sysTimer.Tick
        Dim currentDeck As String = ""
        posDisplay.Text = (txtCard.SelectionStart + 1)
        If lastOpenedFile = "" Then
            currentDeck = "New deck"
        Else
            currentDeck = lastOpenedFile
        End If
        Dim title As String = $"IBM 029 Simulator ({currentDeck})"
        Me.Text = title
        sysTimer.Start()
    End Sub

    Private Sub ListBox1_DoubleClick(sender As Object, e As EventArgs) Handles ListBox1.DoubleClick
        Dim newIndex As Integer = ListBox1.SelectedIndex
        currentIndex = newIndex
        txtCard.Text = cardDeck(currentIndex)
        UpdateDisplay()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If (currentIndex < 0) Or (ListBox1.SelectedIndex < 0) Then Exit Sub
        If ListBox1.SelectedIndex <> currentIndex Then
            currentIndex = ListBox1.SelectedIndex
            txtCard.Text = cardDeck(currentIndex)
            UpdateDisplay()
        End If
    End Sub

    Private Sub ReadersToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ReadersToolStripMenuItem.Click
        Dim rf As New ReaderForm
        rf.Readers = ReaderList
        rf.ShowDialog()
        ReaderList = rf.Readers
        SaveConfig()
        PopulateReaders()
    End Sub

    Private Sub PopulateReaders()
        deviceSelect.Items.Clear()
        For Each r As Reader In ReaderList
            deviceSelect.Items.Add(r.Name)
            If r.isDefault Then
                Dim thisIdx As Integer = deviceSelect.Items.IndexOf(r.Name)
                deviceSelect.SelectedIndex = thisIdx
            End If
        Next
    End Sub

    Private Sub ActivatePunches()
        For Each p As Punch In PunchList
            p.Connect()
            'Stop
        Next
    End Sub

    Private Sub FakeButton_Click(sender As Object, e As EventArgs) Handles FakeButton.Click
        btnNext.PerformClick()
    End Sub

    Private Function ProcessDeck(inDeck As List(Of String)) As List(Of String)
        Dim outDeck As New List(Of String)

        For Each line As String In inDeck
            Dim trimmedLine As String = line.Trim()

            If trimmedLine.StartsWith("#include ", StringComparison.OrdinalIgnoreCase) Then
                Dim includePath As String = trimmedLine.Substring(9).Trim()

                If File.Exists(includePath) Then
                    Try
                        Dim includedLines As List(Of String) =
                        ProcessDeck(File.ReadAllLines(includePath).ToList())

                        outDeck.AddRange(includedLines)
                    Catch ex As Exception
                        Debug.WriteLine($"Error including file '{includePath}': {ex.Message}")
                    End Try
                Else
                    Debug.WriteLine($"Include file not found: {includePath}")
                    ' Optionally: outDeck.Add(line)
                End If
            Else
                outDeck.Add(line)
            End If
        Next

        Return outDeck
    End Function

    Private Function ShowProcessedDeck(processedDeck As List(Of String)) As DialogResult
        Dim outputForm As New Form With {
        .Text = "Processed Deck Output",
        .Width = 800,
        .Height = 600,
        .StartPosition = FormStartPosition.CenterParent,
        .FormBorderStyle = FormBorderStyle.FixedDialog,
        .MinimizeBox = False,
        .MaximizeBox = False
    }

        ' Output TextBox
        Dim txtOutput As New TextBox With {
        .Multiline = True,
        .ScrollBars = ScrollBars.Both,
        .Dock = DockStyle.Fill,
        .Font = New Font("Consolas", 10),
        .ReadOnly = True,
        .WordWrap = False
    }
        txtOutput.Lines = processedDeck.ToArray()

        ' Button panel
        Dim buttonPanel As New Panel With {
        .Dock = DockStyle.Bottom,
        .Height = 50
    }

        ' Submit Button
        Dim btnSubmit As New Button With {
        .Text = "Submit",
        .DialogResult = DialogResult.OK,
        .Width = 100,
        .Height = 30,
        .Top = 10,
        .Left = 560
    }

        ' Cancel Button
        Dim btnCancel As New Button With {
        .Text = "Cancel",
        .DialogResult = DialogResult.Cancel,
        .Width = 100,
        .Height = 30,
        .Top = 10,
        .Left = 670
    }

        ' Add buttons to panel
        buttonPanel.Controls.Add(btnSubmit)
        buttonPanel.Controls.Add(btnCancel)

        ' Add controls to form
        outputForm.Controls.Add(txtOutput)
        outputForm.Controls.Add(buttonPanel)

        ' Default buttons
        outputForm.AcceptButton = btnSubmit
        outputForm.CancelButton = btnCancel

        Return outputForm.ShowDialog(Me)
    End Function




    Private Sub submitButton_Click(sender As Object, e As EventArgs) Handles submitButton.Click
        If cardDeck.Count = 1 Then
            If cardDeck.Item(0).Trim = "" Then
                MsgBox($"There are no cards in this deck.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Critical, "Error")
                Exit Sub
            End If
        End If
        Dim outDeck As List(Of String) = ProcessDeck(cardDeck)
        Dim okToRun As DialogResult = ShowProcessedDeck(outDeck)
        If okToRun = DialogResult.Yes Then
            Dim thisDevice As String = deviceSelect.Text
            Dim devList As IEnumerable(Of Reader)
            devList = (From r As Reader In ReaderList Where r.Name.Trim = thisDevice.Trim Select r)
            Dim thisReader = devList.FirstOrDefault
            thisReader.Submit(outDeck)
            MsgBox($"Deck sent to reader.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Information, "Submit")
        Else
            MsgBox($"Submit cancelled.", MsgBoxStyle.OkOnly Or MsgBoxStyle.Critical, "Cancel job")
        End If
    End Sub

    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        ListBox1.SelectedIndex = 0
    End Sub

    Private Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        ListBox1.SelectedIndex = ListBox1.Items.Count - 1
    End Sub

    Private Sub PunchesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PunchesToolStripMenuItem.Click
        Dim txt As String = $"Using this option will stop any punches" & vbCrLf & $"that are already defined." & vbCrLf &
            $"Are you sure you want to do this?"
        Dim repl As New DialogResult
        repl = MsgBox(txt, MsgBoxStyle.YesNo Or MsgBoxStyle.Question, "Punch")
        If repl = DialogResult.No Then
            Exit Sub
        End If

        Dim pf As New PunchForm
        pf.Punches = PunchList
        pf.ShowDialog()
        PunchList = pf.Punches
        SaveConfig()
        ActivatePunches()

    End Sub
End Class

Public Class TabAwareTextBox
    Inherits TextBox
    Public Sub New()
        Me.AcceptsTab = True
        Me.Multiline = True
        Me.Font = New Font("Consolas", 10) ' Monospaced font helps align tabs
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, keyData As Keys) As Boolean
        If keyData = Keys.Tab Then
            HandleTabKey()
            Return True ' Suppress default Tab behavior
        End If
        Return MyBase.ProcessCmdKey(msg, keyData)
    End Function

    Private Sub HandleTabKey()
        Dim pos As Integer = Me.SelectionStart
        Dim text = Me.Text

        Dim nextTabStop As Integer = ((pos \ 5) + 1) * 5
        Dim spacesNeeded As Integer = nextTabStop - pos

        ' Only insert spaces that won't overwrite non-space characters
        Dim insertSpaces As New Text.StringBuilder()
        For i As Integer = 0 To spacesNeeded - 1
            Dim index As Integer = pos + i
            If index >= text.Length OrElse text(index) = " "c Then
                insertSpaces.Append(" "c)
            Else
                Exit For
            End If
        Next

        If insertSpaces.Length > 0 Then
            Me.Text = text.Insert(pos, insertSpaces.ToString())
            Me.SelectionStart = pos + insertSpaces.Length
        End If
    End Sub


End Class
