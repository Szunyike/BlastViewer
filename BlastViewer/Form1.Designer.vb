<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ImportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SequncesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QueryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.QueryAndHitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.IDsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.QueryToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.HitQueryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Gff3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HitsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RecordsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.BlastToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreateDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OneByOneToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ProteinToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NucleotideToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetBlastPathsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetExecutableFolderToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetWorkingDirectoryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BlastNToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BlastPQprotDBprotToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BlastXQTrNucDBprotToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TBlastNQprotDBTrNuclToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TBlastXQTrNucDBTrNucToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BlatQnucDBnucToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DisplayToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetDisplayMemberOfRecordToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetDisplayMemberOfHitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SetHSPsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FilterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HspsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HitsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RecordsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ResetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.dgv1 = New System.Windows.Forms.DataGridView()
        Me.LoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Tblb1 = New Szunyi.IO.tblb()
        Me.Tblb2 = New Szunyi.IO.tblb()
        Me.MathToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.dgv1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.BlastToolStripMenuItem, Me.DisplayToolStripMenuItem, Me.FilterToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(800, 28)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportToolStripMenuItem, Me.ExportToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(44, 24)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'ImportToolStripMenuItem
        '
        Me.ImportToolStripMenuItem.Name = "ImportToolStripMenuItem"
        Me.ImportToolStripMenuItem.Size = New System.Drawing.Size(129, 26)
        Me.ImportToolStripMenuItem.Text = "Import"
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SequncesToolStripMenuItem, Me.IDsToolStripMenuItem, Me.Gff3ToolStripMenuItem, Me.HitsToolStripMenuItem1, Me.RecordsToolStripMenuItem1})
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(129, 26)
        Me.ExportToolStripMenuItem.Text = "Export"
        '
        'SequncesToolStripMenuItem
        '
        Me.SequncesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.QueryToolStripMenuItem, Me.HitToolStripMenuItem, Me.QueryAndHitToolStripMenuItem})
        Me.SequncesToolStripMenuItem.Name = "SequncesToolStripMenuItem"
        Me.SequncesToolStripMenuItem.Size = New System.Drawing.Size(154, 26)
        Me.SequncesToolStripMenuItem.Text = "Sequences"
        '
        'QueryToolStripMenuItem
        '
        Me.QueryToolStripMenuItem.Name = "QueryToolStripMenuItem"
        Me.QueryToolStripMenuItem.Size = New System.Drawing.Size(176, 26)
        Me.QueryToolStripMenuItem.Text = "Hit"
        '
        'HitToolStripMenuItem
        '
        Me.HitToolStripMenuItem.Name = "HitToolStripMenuItem"
        Me.HitToolStripMenuItem.Size = New System.Drawing.Size(176, 26)
        Me.HitToolStripMenuItem.Text = "Query"
        '
        'QueryAndHitToolStripMenuItem
        '
        Me.QueryAndHitToolStripMenuItem.Name = "QueryAndHitToolStripMenuItem"
        Me.QueryAndHitToolStripMenuItem.Size = New System.Drawing.Size(176, 26)
        Me.QueryAndHitToolStripMenuItem.Text = "Hit and Query"
        '
        'IDsToolStripMenuItem
        '
        Me.IDsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HitToolStripMenuItem1, Me.QueryToolStripMenuItem1, Me.HitQueryToolStripMenuItem})
        Me.IDsToolStripMenuItem.Name = "IDsToolStripMenuItem"
        Me.IDsToolStripMenuItem.Size = New System.Drawing.Size(154, 26)
        Me.IDsToolStripMenuItem.Text = "IDs"
        '
        'HitToolStripMenuItem1
        '
        Me.HitToolStripMenuItem1.Name = "HitToolStripMenuItem1"
        Me.HitToolStripMenuItem1.Size = New System.Drawing.Size(176, 26)
        Me.HitToolStripMenuItem1.Text = "Hit"
        '
        'QueryToolStripMenuItem1
        '
        Me.QueryToolStripMenuItem1.Name = "QueryToolStripMenuItem1"
        Me.QueryToolStripMenuItem1.Size = New System.Drawing.Size(176, 26)
        Me.QueryToolStripMenuItem1.Text = "Query"
        '
        'HitQueryToolStripMenuItem
        '
        Me.HitQueryToolStripMenuItem.Name = "HitQueryToolStripMenuItem"
        Me.HitQueryToolStripMenuItem.Size = New System.Drawing.Size(176, 26)
        Me.HitQueryToolStripMenuItem.Text = "Hit and Query"
        '
        'Gff3ToolStripMenuItem
        '
        Me.Gff3ToolStripMenuItem.Name = "Gff3ToolStripMenuItem"
        Me.Gff3ToolStripMenuItem.Size = New System.Drawing.Size(154, 26)
        Me.Gff3ToolStripMenuItem.Text = "Gff3"
        '
        'HitsToolStripMenuItem1
        '
        Me.HitsToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BestToolStripMenuItem, Me.AllToolStripMenuItem})
        Me.HitsToolStripMenuItem1.Name = "HitsToolStripMenuItem1"
        Me.HitsToolStripMenuItem1.Size = New System.Drawing.Size(154, 26)
        Me.HitsToolStripMenuItem1.Text = "Hits"
        '
        'BestToolStripMenuItem
        '
        Me.BestToolStripMenuItem.Name = "BestToolStripMenuItem"
        Me.BestToolStripMenuItem.Size = New System.Drawing.Size(112, 26)
        Me.BestToolStripMenuItem.Text = "Best"
        '
        'AllToolStripMenuItem
        '
        Me.AllToolStripMenuItem.Name = "AllToolStripMenuItem"
        Me.AllToolStripMenuItem.Size = New System.Drawing.Size(112, 26)
        Me.AllToolStripMenuItem.Text = "All"
        '
        'RecordsToolStripMenuItem1
        '
        Me.RecordsToolStripMenuItem1.Name = "RecordsToolStripMenuItem1"
        Me.RecordsToolStripMenuItem1.Size = New System.Drawing.Size(154, 26)
        Me.RecordsToolStripMenuItem1.Text = "Records"
        '
        'BlastToolStripMenuItem
        '
        Me.BlastToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CreateDatabaseToolStripMenuItem, Me.SetBlastPathsToolStripMenuItem, Me.BlastNToolStripMenuItem, Me.BlastPQprotDBprotToolStripMenuItem, Me.BlastXQTrNucDBprotToolStripMenuItem, Me.TBlastNQprotDBTrNuclToolStripMenuItem, Me.TBlastXQTrNucDBTrNucToolStripMenuItem, Me.BlatQnucDBnucToolStripMenuItem})
        Me.BlastToolStripMenuItem.Name = "BlastToolStripMenuItem"
        Me.BlastToolStripMenuItem.Size = New System.Drawing.Size(53, 24)
        Me.BlastToolStripMenuItem.Text = "Blast"
        '
        'CreateDatabaseToolStripMenuItem
        '
        Me.CreateDatabaseToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OneByOneToolStripMenuItem})
        Me.CreateDatabaseToolStripMenuItem.Name = "CreateDatabaseToolStripMenuItem"
        Me.CreateDatabaseToolStripMenuItem.Size = New System.Drawing.Size(288, 26)
        Me.CreateDatabaseToolStripMenuItem.Text = "Create Database"
        '
        'OneByOneToolStripMenuItem
        '
        Me.OneByOneToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ProteinToolStripMenuItem, Me.NucleotideToolStripMenuItem})
        Me.OneByOneToolStripMenuItem.Name = "OneByOneToolStripMenuItem"
        Me.OneByOneToolStripMenuItem.Size = New System.Drawing.Size(160, 26)
        Me.OneByOneToolStripMenuItem.Text = "One by one"
        '
        'ProteinToolStripMenuItem
        '
        Me.ProteinToolStripMenuItem.Name = "ProteinToolStripMenuItem"
        Me.ProteinToolStripMenuItem.Size = New System.Drawing.Size(157, 26)
        Me.ProteinToolStripMenuItem.Text = "Protein"
        '
        'NucleotideToolStripMenuItem
        '
        Me.NucleotideToolStripMenuItem.Name = "NucleotideToolStripMenuItem"
        Me.NucleotideToolStripMenuItem.Size = New System.Drawing.Size(157, 26)
        Me.NucleotideToolStripMenuItem.Text = "Nucleotide"
        '
        'SetBlastPathsToolStripMenuItem
        '
        Me.SetBlastPathsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetExecutableFolderToolStripMenuItem, Me.SetWorkingDirectoryToolStripMenuItem})
        Me.SetBlastPathsToolStripMenuItem.Name = "SetBlastPathsToolStripMenuItem"
        Me.SetBlastPathsToolStripMenuItem.Size = New System.Drawing.Size(288, 26)
        Me.SetBlastPathsToolStripMenuItem.Text = "Set Blast Paths"
        '
        'SetExecutableFolderToolStripMenuItem
        '
        Me.SetExecutableFolderToolStripMenuItem.Name = "SetExecutableFolderToolStripMenuItem"
        Me.SetExecutableFolderToolStripMenuItem.Size = New System.Drawing.Size(229, 26)
        Me.SetExecutableFolderToolStripMenuItem.Text = "Set Executable Folder"
        '
        'SetWorkingDirectoryToolStripMenuItem
        '
        Me.SetWorkingDirectoryToolStripMenuItem.Name = "SetWorkingDirectoryToolStripMenuItem"
        Me.SetWorkingDirectoryToolStripMenuItem.Size = New System.Drawing.Size(229, 26)
        Me.SetWorkingDirectoryToolStripMenuItem.Text = "Set Working Directory"
        '
        'BlastNToolStripMenuItem
        '
        Me.BlastNToolStripMenuItem.Name = "BlastNToolStripMenuItem"
        Me.BlastNToolStripMenuItem.Size = New System.Drawing.Size(288, 26)
        Me.BlastNToolStripMenuItem.Text = "Blast N Q=nuc DB=nuc"
        '
        'BlastPQprotDBprotToolStripMenuItem
        '
        Me.BlastPQprotDBprotToolStripMenuItem.Name = "BlastPQprotDBprotToolStripMenuItem"
        Me.BlastPQprotDBprotToolStripMenuItem.Size = New System.Drawing.Size(288, 26)
        Me.BlastPQprotDBprotToolStripMenuItem.Text = "Blast P Q=prot DB=prot"
        '
        'BlastXQTrNucDBprotToolStripMenuItem
        '
        Me.BlastXQTrNucDBprotToolStripMenuItem.Name = "BlastXQTrNucDBprotToolStripMenuItem"
        Me.BlastXQTrNucDBprotToolStripMenuItem.Size = New System.Drawing.Size(288, 26)
        Me.BlastXQTrNucDBprotToolStripMenuItem.Text = "Blast X Q=Tr. nuc DB=prot"
        '
        'TBlastNQprotDBTrNuclToolStripMenuItem
        '
        Me.TBlastNQprotDBTrNuclToolStripMenuItem.Name = "TBlastNQprotDBTrNuclToolStripMenuItem"
        Me.TBlastNQprotDBTrNuclToolStripMenuItem.Size = New System.Drawing.Size(288, 26)
        Me.TBlastNQprotDBTrNuclToolStripMenuItem.Text = "T Blast N Q=prot DB=Tr. nucl"
        '
        'TBlastXQTrNucDBTrNucToolStripMenuItem
        '
        Me.TBlastXQTrNucDBTrNucToolStripMenuItem.Name = "TBlastXQTrNucDBTrNucToolStripMenuItem"
        Me.TBlastXQTrNucDBTrNucToolStripMenuItem.Size = New System.Drawing.Size(288, 26)
        Me.TBlastXQTrNucDBTrNucToolStripMenuItem.Text = "T Blast X Q=Tr. nuc DB= Tr. nuc"
        '
        'BlatQnucDBnucToolStripMenuItem
        '
        Me.BlatQnucDBnucToolStripMenuItem.Name = "BlatQnucDBnucToolStripMenuItem"
        Me.BlatQnucDBnucToolStripMenuItem.Size = New System.Drawing.Size(288, 26)
        Me.BlatQnucDBnucToolStripMenuItem.Text = "Blat Q=nuc DB=nuc"
        '
        'DisplayToolStripMenuItem
        '
        Me.DisplayToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SetDisplayMemberOfRecordToolStripMenuItem, Me.SetDisplayMemberOfHitToolStripMenuItem, Me.SetHSPsToolStripMenuItem})
        Me.DisplayToolStripMenuItem.Name = "DisplayToolStripMenuItem"
        Me.DisplayToolStripMenuItem.Size = New System.Drawing.Size(70, 24)
        Me.DisplayToolStripMenuItem.Text = "Display"
        '
        'SetDisplayMemberOfRecordToolStripMenuItem
        '
        Me.SetDisplayMemberOfRecordToolStripMenuItem.Name = "SetDisplayMemberOfRecordToolStripMenuItem"
        Me.SetDisplayMemberOfRecordToolStripMenuItem.Size = New System.Drawing.Size(293, 26)
        Me.SetDisplayMemberOfRecordToolStripMenuItem.Text = "Set Display Member of Records"
        '
        'SetDisplayMemberOfHitToolStripMenuItem
        '
        Me.SetDisplayMemberOfHitToolStripMenuItem.Name = "SetDisplayMemberOfHitToolStripMenuItem"
        Me.SetDisplayMemberOfHitToolStripMenuItem.Size = New System.Drawing.Size(293, 26)
        Me.SetDisplayMemberOfHitToolStripMenuItem.Text = "Set Display Member of Hits"
        '
        'SetHSPsToolStripMenuItem
        '
        Me.SetHSPsToolStripMenuItem.Name = "SetHSPsToolStripMenuItem"
        Me.SetHSPsToolStripMenuItem.Size = New System.Drawing.Size(293, 26)
        Me.SetHSPsToolStripMenuItem.Text = "Set datagridview"
        '
        'FilterToolStripMenuItem
        '
        Me.FilterToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HspsToolStripMenuItem, Me.HitsToolStripMenuItem, Me.RecordsToolStripMenuItem, Me.ResetToolStripMenuItem, Me.LoadToolStripMenuItem, Me.MathToolStripMenuItem})
        Me.FilterToolStripMenuItem.Name = "FilterToolStripMenuItem"
        Me.FilterToolStripMenuItem.Size = New System.Drawing.Size(54, 24)
        Me.FilterToolStripMenuItem.Text = "Filter"
        '
        'HspsToolStripMenuItem
        '
        Me.HspsToolStripMenuItem.Name = "HspsToolStripMenuItem"
        Me.HspsToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.HspsToolStripMenuItem.Text = "Hsps"
        '
        'HitsToolStripMenuItem
        '
        Me.HitsToolStripMenuItem.Name = "HitsToolStripMenuItem"
        Me.HitsToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.HitsToolStripMenuItem.Text = "Hits"
        '
        'RecordsToolStripMenuItem
        '
        Me.RecordsToolStripMenuItem.Name = "RecordsToolStripMenuItem"
        Me.RecordsToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.RecordsToolStripMenuItem.Text = "Records"
        '
        'ResetToolStripMenuItem
        '
        Me.ResetToolStripMenuItem.Name = "ResetToolStripMenuItem"
        Me.ResetToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.ResetToolStripMenuItem.Text = "Reset"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 28)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Tblb1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.dgv1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Tblb2)
        Me.SplitContainer1.Size = New System.Drawing.Size(800, 422)
        Me.SplitContainer1.SplitterDistance = 266
        Me.SplitContainer1.TabIndex = 1
        '
        'dgv1
        '
        Me.dgv1.AllowUserToAddRows = False
        Me.dgv1.AllowUserToDeleteRows = False
        Me.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv1.Dock = System.Windows.Forms.DockStyle.Top
        Me.dgv1.Location = New System.Drawing.Point(206, 0)
        Me.dgv1.Name = "dgv1"
        Me.dgv1.ReadOnly = True
        Me.dgv1.RowTemplate.Height = 24
        Me.dgv1.Size = New System.Drawing.Size(324, 241)
        Me.dgv1.TabIndex = 1
        '
        'LoadToolStripMenuItem
        '
        Me.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem"
        Me.LoadToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.LoadToolStripMenuItem.Text = "Load"
        '
        'Tblb1
        '
        Me.Tblb1.DisplayMember = Nothing
        Me.Tblb1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tblb1.Location = New System.Drawing.Point(0, 0)
        Me.Tblb1.Name = "Tblb1"
        Me.Tblb1.Original = CType(resources.GetObject("Tblb1.Original"), System.Collections.Generic.List(Of Object))
        Me.Tblb1.Selected = CType(resources.GetObject("Tblb1.Selected"), System.Collections.Generic.List(Of Object))
        Me.Tblb1.SelItem = Nothing
        Me.Tblb1.Size = New System.Drawing.Size(266, 422)
        Me.Tblb1.TabIndex = 0
        '
        'Tblb2
        '
        Me.Tblb2.DisplayMember = Nothing
        Me.Tblb2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Tblb2.Location = New System.Drawing.Point(0, 0)
        Me.Tblb2.Name = "Tblb2"
        Me.Tblb2.Original = CType(resources.GetObject("Tblb2.Original"), System.Collections.Generic.List(Of Object))
        Me.Tblb2.Selected = CType(resources.GetObject("Tblb2.Selected"), System.Collections.Generic.List(Of Object))
        Me.Tblb2.SelItem = Nothing
        Me.Tblb2.Size = New System.Drawing.Size(206, 422)
        Me.Tblb2.TabIndex = 0
        '
        'MathToolStripMenuItem
        '
        Me.MathToolStripMenuItem.Name = "MathToolStripMenuItem"
        Me.MathToolStripMenuItem.Size = New System.Drawing.Size(216, 26)
        Me.MathToolStripMenuItem.Text = "Math"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.dgv1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ImportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents FilterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HspsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HitsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RecordsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Tblb1 As Szunyi.IO.tblb
    Friend WithEvents Tblb2 As Szunyi.IO.tblb
    Friend WithEvents BlastToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents DisplayToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetDisplayMemberOfRecordToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetDisplayMemberOfHitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetHSPsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CreateDatabaseToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OneByOneToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProteinToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NucleotideToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetBlastPathsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetExecutableFolderToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SetWorkingDirectoryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BlastNToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BlastPQprotDBprotToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BlastXQTrNucDBprotToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TBlastNQprotDBTrNuclToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TBlastXQTrNucDBTrNucToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents BlatQnucDBnucToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents dgv1 As DataGridView
    Friend WithEvents SequncesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QueryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents QueryAndHitToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents IDsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HitToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents QueryToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents HitQueryToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Gff3ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HitsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents BestToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AllToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RecordsToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents ResetToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents MathToolStripMenuItem As ToolStripMenuItem
End Class
