<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class includeBrowse
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
        FileBrowserControl1 = New FileBrowserControl()
        Button1 = New Button()
        Button2 = New Button()
        SuspendLayout()
        ' 
        ' FileBrowserControl1
        ' 
        FileBrowserControl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        FileBrowserControl1.Location = New Point(0, 0)
        FileBrowserControl1.Name = "FileBrowserControl1"
        FileBrowserControl1.Size = New Size(795, 456)
        FileBrowserControl1.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(293, 475)
        Button1.Name = "Button1"
        Button1.Size = New Size(94, 29)
        Button1.TabIndex = 1
        Button1.Text = "Select"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(393, 475)
        Button2.Name = "Button2"
        Button2.Size = New Size(94, 29)
        Button2.TabIndex = 2
        Button2.Text = "Cancel"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' includeBrowse
        ' 
        AutoScaleDimensions = New SizeF(8F, 20F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 516)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(FileBrowserControl1)
        Name = "includeBrowse"
        Text = "Choose the file to include"
        ResumeLayout(False)
    End Sub

    Friend WithEvents FileBrowserControl1 As FileBrowserControl
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
End Class
