<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PunchForm
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
        ToolStrip1 = New ToolStrip()
        AddPunch = New ToolStripButton()
        EditPunch = New ToolStripButton()
        DeletePunch = New ToolStripButton()
        cmdUpdate = New Button()
        PunchItem = New TextBox()
        Label1 = New Label()
        PunchList = New ListBox()
        ToolStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' ToolStrip1
        ' 
        ToolStrip1.ImageScalingSize = New Size(20, 20)
        ToolStrip1.Items.AddRange(New ToolStripItem() {AddPunch, EditPunch, DeletePunch})
        ToolStrip1.Location = New Point(0, 0)
        ToolStrip1.Name = "ToolStrip1"
        ToolStrip1.Size = New Size(433, 47)
        ToolStrip1.TabIndex = 6
        ToolStrip1.Text = "ToolStrip1"
        ' 
        ' AddPunch
        ' 
        AddPunch.Image = My.Resources.Resources.Add
        AddPunch.ImageTransparentColor = Color.Magenta
        AddPunch.Name = "AddPunch"
        AddPunch.Size = New Size(41, 44)
        AddPunch.Text = "Add"
        AddPunch.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' EditPunch
        ' 
        EditPunch.Image = My.Resources.Resources.ConfigurationEditor
        EditPunch.ImageTransparentColor = Color.Magenta
        EditPunch.Name = "EditPunch"
        EditPunch.Size = New Size(39, 44)
        EditPunch.Text = "Edit"
        EditPunch.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' DeletePunch
        ' 
        DeletePunch.Image = My.Resources.Resources.Delete
        DeletePunch.ImageTransparentColor = Color.Magenta
        DeletePunch.Name = "DeletePunch"
        DeletePunch.Size = New Size(57, 44)
        DeletePunch.Text = "Delete"
        DeletePunch.TextImageRelation = TextImageRelation.ImageAboveText
        ' 
        ' cmdUpdate
        ' 
        cmdUpdate.Location = New Point(310, 540)
        cmdUpdate.Margin = New Padding(3, 4, 3, 4)
        cmdUpdate.Name = "cmdUpdate"
        cmdUpdate.Size = New Size(86, 31)
        cmdUpdate.TabIndex = 8
        cmdUpdate.Text = "Update"
        cmdUpdate.UseVisualStyleBackColor = True
        ' 
        ' PunchItem
        ' 
        PunchItem.Location = New Point(14, 540)
        PunchItem.Margin = New Padding(3, 4, 3, 4)
        PunchItem.Name = "PunchItem"
        PunchItem.Size = New Size(274, 27)
        PunchItem.TabIndex = 7
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(14, 51)
        Label1.Name = "Label1"
        Label1.Size = New Size(355, 40)
        Label1.TabIndex = 10
        Label1.Text = "Format: <Display Name>=<hostname.or.ip>:<port>" & vbCrLf & "Hercules=192.168.1.1:3525"
        ' 
        ' PunchList
        ' 
        PunchList.FormattingEnabled = True
        PunchList.Location = New Point(14, 115)
        PunchList.Margin = New Padding(3, 4, 3, 4)
        PunchList.Name = "PunchList"
        PunchList.Size = New Size(405, 404)
        PunchList.TabIndex = 11
        ' 
        ' PunchForm
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(433, 591)
        Controls.Add(PunchList)
        Controls.Add(Label1)
        Controls.Add(cmdUpdate)
        Controls.Add(PunchItem)
        Controls.Add(ToolStrip1)
        Margin = New Padding(3, 4, 3, 4)
        Name = "PunchForm"
        Text = "Punch Configuration"
        ToolStrip1.ResumeLayout(False)
        ToolStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents AddPunch As ToolStripButton
    Friend WithEvents EditPunch As ToolStripButton
    Friend WithEvents DeletePunch As ToolStripButton
    Friend WithEvents cmdUpdate As Button
    Friend WithEvents PunchItem As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PunchList As ListBox
End Class
