Imports System.IO
Imports System.ComponentModel
Imports System.Linq
Imports System.Runtime.InteropServices
Imports System.Drawing

Public Class FileBrowserControl
    Inherits UserControl

    Private treeDirectories As New TreeView()
    Private listFiles As New ListBox()
    Private textPreview As New TextBox()
    Private icons As New ImageList()

    Public Event FileSelected As EventHandler

    Private selectedFilePath As String = ""
    <Browsable(False)>
    Public ReadOnly Property SelectedFile As String
        Get
            Return selectedFilePath
        End Get
    End Property

    Public Sub New()
        InitializeComponents()
        LoadIcons()
        LoadSpecialFoldersAndDrives()
    End Sub

    Private Function GetKnownFolderPath(ByVal knownFolderId As Guid) As String
        Dim pszPath As String = Nothing
        Dim hr = SHGetKnownFolderPath(knownFolderId, 0, IntPtr.Zero, pszPath)
        If hr = 0 Then
            Return pszPath
        Else
            Return Nothing
        End If
    End Function

    Private Sub InitializeComponents()
        treeDirectories.Dock = DockStyle.Left
        treeDirectories.Width = 250
        treeDirectories.ImageList = icons

        listFiles.Dock = DockStyle.Top
        listFiles.Height = 150

        textPreview.Dock = DockStyle.Fill
        textPreview.Multiline = True
        textPreview.ReadOnly = True
        textPreview.ScrollBars = ScrollBars.Both
        textPreview.Font = New Font("Consolas", 9)

        Dim rightPanel As New Panel() With {.Dock = DockStyle.Fill}
        rightPanel.Controls.Add(textPreview)
        rightPanel.Controls.Add(listFiles)

        Controls.Add(rightPanel)
        Controls.Add(treeDirectories)

        AddHandler treeDirectories.AfterSelect, AddressOf OnDirectorySelected
        AddHandler listFiles.SelectedIndexChanged, AddressOf OnFileSelected
        AddHandler treeDirectories.BeforeExpand, AddressOf OnBeforeExpand
    End Sub

    Private Sub LoadIcons()
        icons.Images.Clear()

        Dim folderIcon = GetSystemIcon("folder", True)
        If folderIcon IsNot Nothing Then
            icons.Images.Add(folderIcon)
        Else
            icons.Images.Add(New Bitmap(16, 16))
        End If

        Dim driveIcon = GetSystemIcon("drive", True)
        If driveIcon IsNot Nothing Then
            icons.Images.Add(driveIcon)
        Else
            icons.Images.Add(New Bitmap(16, 16))
        End If

        Dim computerIcon = GetSystemIcon("computer", True)
        If computerIcon IsNot Nothing Then
            icons.Images.Add(computerIcon)
        Else
            icons.Images.Add(New Bitmap(16, 16))
        End If
    End Sub

    Private Function GetSystemIcon(type As String, large As Boolean) As Bitmap
        Dim shinfo As New SHFILEINFO()
        Dim flags As UInteger = SHGFI_ICON Or SHGFI_SMALLICON
        If large Then flags = SHGFI_ICON Or SHGFI_LARGEICON

        Dim path As String = ""
        Select Case type.ToLower()
            Case "folder"
                path = "C:\Windows" ' Any folder path to get folder icon
            Case "drive"
                path = "C:\" ' Root drive to get drive icon
            Case "computer"
                path = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}" ' My Computer shell GUID
            Case Else
                path = "C:\Windows"
        End Select

        Dim res As IntPtr = SHGetFileInfo(path, 0, shinfo, CType(Marshal.SizeOf(shinfo), UInteger), flags)
        If res = IntPtr.Zero Then
            Debug.WriteLine($"Failed to get icon for path: {path}")
            Return Nothing
        End If

        Dim sysIcon = Icon.FromHandle(shinfo.hIcon)
        Dim bmp = sysIcon.ToBitmap()
        sysIcon.Dispose()
        DestroyIcon(shinfo.hIcon)
        Return bmp
    End Function

    Private Sub LoadSpecialFoldersAndDrives()
        treeDirectories.Nodes.Clear()

        ' Create "This PC" root node
        Dim thisPCNode As New TreeNode("This PC") With {
            .ImageIndex = 2,
            .SelectedImageIndex = 2
        }

        ' Add special folders under "This PC"
        Dim docsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        Dim downloadsPath = GetKnownFolderPath(KnownFolderDownloads)
        Dim desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        Dim picturesPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
        Dim musicPath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic)
        Dim videosPath = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos)

        AddSpecialFolderNode(thisPCNode, "Documents", docsPath)
        AddSpecialFolderNode(thisPCNode, "Downloads", downloadsPath)
        AddSpecialFolderNode(thisPCNode, "Desktop", desktopPath)
        AddSpecialFolderNode(thisPCNode, "Pictures", picturesPath)
        AddSpecialFolderNode(thisPCNode, "Music", musicPath)
        AddSpecialFolderNode(thisPCNode, "Videos", videosPath)

        treeDirectories.Nodes.Add(thisPCNode)

        ' Add drives as separate nodes
        For Each drive As DriveInfo In DriveInfo.GetDrives()
            If drive.IsReady Then
                Dim node As New TreeNode(drive.Name) With {
                    .Tag = drive.RootDirectory.FullName,
                    .ImageIndex = 1,
                    .SelectedImageIndex = 1
                }
                node.Nodes.Add("Loading...")
                treeDirectories.Nodes.Add(node)
            End If
        Next
    End Sub

    Private Sub AddSpecialFolderNode(parent As TreeNode, name As String, path As String)
        If Not String.IsNullOrEmpty(path) AndAlso Directory.Exists(path) Then
            Dim node As New TreeNode(name) With {
                .Tag = path,
                .ImageIndex = 0,
                .SelectedImageIndex = 0
            }
            node.Nodes.Add("Loading...")
            parent.Nodes.Add(node)
        End If
    End Sub

    Private Sub OnBeforeExpand(sender As Object, e As TreeViewCancelEventArgs)
        Dim node = e.Node
        If node.Nodes.Count = 1 AndAlso node.Nodes(0).Text = "Loading..." Then
            node.Nodes.Clear()
            Try
                Dim folders = Directory.GetDirectories(CStr(node.Tag))
                For Each folder In folders
                    Dim subNode As New TreeNode(Path.GetFileName(folder)) With {
                        .Tag = folder,
                        .ImageIndex = 0,
                        .SelectedImageIndex = 0
                    }
                    If Directory.GetDirectories(folder).Length > 0 Then
                        subNode.Nodes.Add("Loading...")
                    End If
                    node.Nodes.Add(subNode)
                Next
            Catch ex As UnauthorizedAccessException
                ' Skip folders we can't access
            End Try
        End If
    End Sub

    Private Sub OnDirectorySelected(sender As Object, e As TreeViewEventArgs)
        Dim selectedPath = CStr(e.Node.Tag)
        If selectedPath Is Nothing Then Exit Sub
        listFiles.Items.Clear()

        Try
            Dim files = Directory.GetFiles(selectedPath)
            For Each file In files
                listFiles.Items.Add(Path.GetFileName(file))
            Next
        Catch ex As UnauthorizedAccessException
            ' Skip files we can't access
        End Try

        textPreview.Clear()
    End Sub

    Private Sub OnFileSelected(sender As Object, e As EventArgs)
        If treeDirectories.SelectedNode Is Nothing OrElse listFiles.SelectedItem Is Nothing Then
            Return
        End If

        Dim dir = CStr(treeDirectories.SelectedNode.Tag)
        Dim file = CStr(listFiles.SelectedItem)
        selectedFilePath = Path.Combine(dir, file)

        Try
            Dim previewLines = System.IO.File.ReadLines(selectedFilePath).Take(100)
            textPreview.Text = String.Join(Environment.NewLine, previewLines)
        Catch ex As Exception
            textPreview.Text = $"Unable to read file: {ex.Message}"
        End Try

        RaiseEvent FileSelected(Me, EventArgs.Empty)
    End Sub

    ' P/Invoke and Known Folder constants to get Downloads and other folders by GUID

    Private Shared ReadOnly KnownFolderDownloads As Guid = New Guid("374DE290-123F-4565-9164-39C4925E467B")

    <DllImport("shell32.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function SHGetKnownFolderPath(ByRef rfid As Guid, dwFlags As UInteger, hToken As IntPtr, <MarshalAs(UnmanagedType.LPWStr)> ByRef pszPath As String) As Integer
    End Function

    <StructLayout(LayoutKind.Sequential)>
    Private Structure SHFILEINFO
        Public hIcon As IntPtr
        Public iIcon As Integer
        Public dwAttributes As UInteger
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=260)>
        Public szDisplayName As String
        <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=80)>
        Public szTypeName As String
    End Structure

    Private Const SHGFI_ICON As UInteger = &H100
    Private Const SHGFI_LARGEICON As UInteger = &H0    ' Large icon
    Private Const SHGFI_SMALLICON As UInteger = &H1    ' Small icon

    <DllImport("shell32.dll", CharSet:=CharSet.Unicode)>
    Private Shared Function SHGetFileInfo(pszPath As String, dwFileAttributes As UInteger, ByRef psfi As SHFILEINFO, cbFileInfo As UInteger, uFlags As UInteger) As IntPtr
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function DestroyIcon(hIcon As IntPtr) As Boolean
    End Function

End Class
