<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ReaderForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        ReaderItem = New TextBox()
        cmdUpdate = New Button()
        ReaderList = New CheckedListBox()
        ToolStrip1 = New ToolStrip()
        AddReader = New ToolStripButton()
        EditReader = New ToolStripButton()
        DeleteReader = New ToolStripButton()
        Label1 = New Label()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ReaderItem
        ' 
        ReaderItem.Location = New Point(21, 397)
        ReaderItem.Name = "ReaderItem"
        ReaderItem.Size = New Size(240, 23)
        ReaderItem.TabIndex = 1
        ' 
        ' cmdUpdate
        ' 
        cmdUpdate.Location = New Point(272, 396)
        cmdUpdate.Name = "cmdUpdate"
        cmdUpdate.Size = New Size(75, 23)
        cmdUpdate.TabIndex = 2
        cmdUpdate.Text = "Update"
        cmdUpdate.UseVisualStyleBackColor = True
        ' 
        ' ReaderList
        ' 
        ReaderList.Font = New Font("Consolas", 11.25F, FontStyle.Regular, GraphicsUnit.Point, CByte(0))
        ReaderList.FormattingEnabled = True
        ReaderList.Location = New Point(12, 87)
        ReaderList.Name = "ReaderList"
        ReaderList.Size = New Size(353, 304)
        ReaderList.TabIndex = 4
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.Items.AddRange(New ToolStripItem() {AddReader, EditReader, DeleteReader})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(377, 38)
        ToolStrip1.TabIndex = 5
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' AddReader
        ' 
        AddReader.Image = My.Resources.Resources.Add
        AddReader.ImageTransparentColor = Color.Magenta
        AddReader.Name = "AddReader"
        AddReader.Size = New Size(33, 35)
        AddReader.Text = "Add"
        AddReader.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' EditReader
        ' 
        EditReader.Image = My.Resources.Resources.ConfigurationEditor
        EditReader.ImageTransparentColor = Color.Magenta
        EditReader.Name = "EditReader"
        EditReader.Size = New Size(31, 35)
        EditReader.Text = "Edit"
        EditReader.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' DeleteReader
        ' 
        DeleteReader.Image = My.Resources.Resources.Delete
        DeleteReader.ImageTransparentColor = Color.Magenta
        DeleteReader.Name = "DeleteReader"
        DeleteReader.Size = New Size(44, 35)
        DeleteReader.Text = "Delete"
        DeleteReader.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(12, 38)
        Label1.Name = "Label1"
        Label1.Size = New Size(285, 45)
        Label1.TabIndex = 6
        Label1.Text = "Place a checkmark beside your default reader" & vbCrLf & "Format: <Display Name>=<hostname.or.ip>:<port>" & vbCrLf & "Hercules=192.168.1.1:3505"
        ' 
        ' ReaderForm
        ' 
        AcceptButton = cmdUpdate
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(377, 434)
        Controls.Add(Label1)
        Controls.Add(ToolStrip1)
        Controls.Add(ReaderList)
        Controls.Add(cmdUpdate)
        Controls.Add(ReaderItem)
        Name = "ReaderForm"
        StartPosition = FormStartPosition.CenterParent
        Text = "Reader Configuration"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents ReaderItem As TextBox
    Friend WithEvents cmdUpdate As Button
    Friend WithEvents ReaderList As CheckedListBox
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents AddReader As ToolStripButton
    Friend WithEvents EditReader As ToolStripButton
    Friend WithEvents DeleteReader As ToolStripButton
    Friend WithEvents Label1 As Label
End Class
