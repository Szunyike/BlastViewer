Imports System.ComponentModel
Imports System.IO
Imports Bio.Web.Blast
Imports Szunyi.IO
Imports Szunyi.BLAST.Enums
Imports Szunyi.BLAST
Imports Szunyi.Common
Imports System.Linq.Dynamic.Core
'Imports System.Linq.Dynamic


Public Class Form1
    Private SortOrders(25) As Integer
    Dim TheBlastResult As New List(Of Bio.Web.Blast.BlastResult)
    ''' <summary>
    ''' This is always containig the original result
    ''' </summary>
    Dim OriginalBlastSearchResults As New List(Of Bio.Web.Blast.BlastResult)
    ''' <summary>
    ''' This is contains the filtered Blast Results which can be Reset
    ''' </summary>
    Dim OriginalBlastSearchRecords As New List(Of Bio.Web.Blast.BlastSearchRecord)
    Dim ClonedAndFilteredBlastSearchRecords As New List(Of Bio.Web.Blast.BlastSearchRecord)
    Dim OpenedFiles As New List(Of FileInfo)

    Private DisplayMemberofHit As String = "Accession"
    Private DisplayMemberofRecord As String = "IterationQueryDefinition"
    Private DisplayMembersAll As New List(Of String)
    Private currAll As IQueryable
    Private ByFiles As New List(Of List(Of BlastSearchRecord))
    Private Sub ImportToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportToolStripMenuItem.Click
        Dim Files = Szunyi.IO.Pick_Up.Files(Szunyi.IO.File_Extensions.Blast, "Select Blast Result", New DirectoryInfo(My.Settings.Result)).ToList
        Dim log As System.Text.StringBuilder
        OpenedFiles = Files
        ClonedAndFilteredBlastSearchRecords.Clear()
        For Each File In Files
            Dim Records = Szunyi.BLAST.Import.From_File(File, log).ToList
            ByFiles.Add(Records)
            ClonedAndFilteredBlastSearchRecords.AddRange(Records)
        Next
        Me.OriginalBlastSearchRecords = ClonedAndFilteredBlastSearchRecords.Clone
        Tblb1.SetIt(ClonedAndFilteredBlastSearchRecords, DisplayMemberofRecord)


    End Sub


    Private Function GetFilteredBlastSearchRecords(Filter As String) As List(Of Bio.Web.Blast.BlastSearchRecord)
        Dim res = From x In Me.ClonedAndFilteredBlastSearchRecords Where x.IterationQueryDefinition.ToUpper.Contains(Filter.ToUpper)

        If res.Count > 0 Then
            Return res.ToList
        Else
            Return New List(Of Bio.Web.Blast.BlastSearchRecord)

        End If
    End Function
#Region "dgv1"
    Private Sub ShowAllHsps(cItem As BlastSearchRecord)
        Dim HSPs As New List(Of Bio.Web.Blast.Hsp)
        For Each Hit In cItem.Hits
            HSPs.AddRange(Hit.Hsps)
        Next
        dgv1.DataSource = cItem
    End Sub
    Private Sub dgv1_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles dgv1.ColumnHeaderMouseClick


        Try
            Dim colName = dgv1.Columns(e.ColumnIndex).Name
            Dim SO = SortOrders(e.ColumnIndex)
            If SO = SortOrder.Ascending Then
                Dim t = Me.currAll.OrderBy(colName)

                dgv1.DataSource = Nothing
                dgv1.DataSource = t.ToDynamicList
            Else
                Dim t = Me.currAll.OrderBy(colName & " Desc")

                dgv1.DataSource = Nothing
                dgv1.DataSource = t.ToDynamicList
            End If


        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
        Select Case SortOrders(e.ColumnIndex)
            Case SortOrder.None
                SortOrders(e.ColumnIndex) = SortOrder.Ascending
            Case SortOrder.Ascending
                SortOrders(e.ColumnIndex) = SortOrder.Descending
            Case SortOrder.Descending
                SortOrders(e.ColumnIndex) = SortOrder.Ascending
        End Select
    End Sub

#End Region

#Region "Filter"

#End Region
    Private Sub HspsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HspsToolStripMenuItem.Click

        Dim t = GetType(Szunyi.BLAST.Enums.Hsp)
        Dim x As New Szunyi.IO.Filter(t, New DirectoryInfo(My.Settings.Filter))
        If x.ShowDialog() = DialogResult.OK Then
            Dim prop = Szunyi.IO.Util_Helpers.Set_Filter_Settings(Of Szunyi.BLAST.Enums.Hsp)(x.Setting)

        End If
    End Sub

    Private Sub HitsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HitsToolStripMenuItem.Click

        Dim t = GetType(Szunyi.BLAST.Enums.Hit)
        Dim x As New Szunyi.IO.Filter(t, New DirectoryInfo(My.Settings.Filter))
        If x.ShowDialog() = DialogResult.OK Then
            Dim props = Szunyi.IO.Util_Helpers.Set_Filter_Settings(Of Szunyi.BLAST.Enums.Hit)(x.Setting)
            Dim c = Szunyi.BLAST.Filter.Filter_Hits(props, Me.ClonedAndFilteredBlastSearchRecords)
            Tblb1.SetIt(c, Me.DisplayMemberofRecord)
        End If
    End Sub

    Private Sub RecordsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RecordsToolStripMenuItem.Click
        Dim t = GetType(Szunyi.BLAST.Enums.Record)
        Dim x As New Szunyi.IO.Filter(t, New DirectoryInfo(My.Settings.Filter))
        If x.ShowDialog() = DialogResult.OK Then
            Dim props = Szunyi.IO.Util_Helpers.Set_Filter_Settings(Of Szunyi.BLAST.Enums.Record)(x.Setting)
            Dim c = Szunyi.BLAST.Filter.Filter_Records(props, Me.ClonedAndFilteredBlastSearchRecords)
            Me.ClonedAndFilteredBlastSearchRecords = c
            Tblb1.SetIt(c, Me.DisplayMemberofRecord)
        End If
    End Sub



#Region "Display"
    Private Sub SetDisplayMemberOfRecordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetDisplayMemberOfRecordToolStripMenuItem.Click
        Dim Values = Szunyi.IO.Util_Helpers.Get_All_Enum_Names(Of Szunyi.BLAST.Enums.Record)(Szunyi.BLAST.Enums.Record.IterationMessage)
        Dim x As New Szunyi.IO.CheckBoxForStringsFull(Values, 1, "Set Display member of Record", "IterationQueryDefinition")
        If x.ShowDialog = DialogResult.OK Then
            Tblb1.SetIt(ClonedAndFilteredBlastSearchRecords, x.SelectedStrings.First)
        End If
    End Sub

    Private Sub SetDisplayMemberOfHitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetDisplayMemberOfHitToolStripMenuItem.Click
        Dim Values = Szunyi.IO.Util_Helpers.Get_All_Enum_Names(Of Szunyi.BLAST.Enums.Hit)(Szunyi.BLAST.Enums.Record.IterationMessage)
        Dim x As New Szunyi.IO.CheckBoxForStringsFull(Values, 1, "Set Display member of Record", "IterationQueryDefinition")
        If x.ShowDialog = DialogResult.OK Then
            Tblb2.SetDisplayMember(x.SelectedStrings.First)
            DisplayMemberofHit = x.SelectedStrings.First
        End If
    End Sub

    Private Sub SetHSPsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetHSPsToolStripMenuItem.Click
        Dim Values = Szunyi.IO.Util_Helpers.Get_All_Enum_Names(Of Szunyi.BLAST.Enums.Record)(Szunyi.BLAST.Enums.Record.IterationMessage)
        Dim Records = Szunyi.Common.Text.Lists.Insert_Text_in_Every_LineStarts(Values.ToList, "Record.")
        Values = Szunyi.IO.Util_Helpers.Get_All_Enum_Names(Of Szunyi.BLAST.Enums.Hit)(Szunyi.BLAST.Enums.Hit.Accession)
        Records.AddRange(Szunyi.Common.Text.Lists.Insert_Text_in_Every_LineStarts(Values.ToList, "Hit."))
        Values = Szunyi.IO.Util_Helpers.Get_All_Enum_Names(Of Szunyi.BLAST.Enums.Hsp)(Szunyi.BLAST.Enums.Hsp.AlignmentLength)
        Records.AddRange(Szunyi.Common.Text.Lists.Insert_Text_in_Every_LineStarts(Values.ToList, "Hsp."))

        Dim x As New Szunyi.IO.CheckBoxForStringsFull(Records, -1, "Set Display member of Record", Me.DisplayMembersAll)
        If x.ShowDialog = DialogResult.OK Then
            Me.DisplayMembersAll = x.SelectedStrings
            Tblb1.SetIt(ClonedAndFilteredBlastSearchRecords, Me.DisplayMemberofRecord)
        End If
    End Sub

    Private Sub tbl1(Item As Object) Handles Tblb1.IndexChanged
        Dim curr As Bio.Web.Blast.BlastSearchRecord = Item
        Tblb2.SetIt(curr.Hits, DisplayMemberofHit)
        Dim HSPs = Szunyi.BLAST.BlastManipulation.Hsp.All(curr)
        Dim var = From x In HSPs Select x.Record.Hits

        Dim profiles = HSPs.AsQueryable()
        Dim sg = Szunyi.Common.Text.General.GetText(Me.DisplayMembersAll, ",")
        If sg <> "" Then
            Try
                Dim var1 = profiles.Select("new(" & sg & ")")
                dgv1.DataSource = Nothing '

                Dim f = var1.OrderBy("Id")
                currAll = f
                dgv1.DataSource = f.ToDynamicList
                dgv1.Update()
            Catch ex As Exception
                Dim kj As Int16 = 54
            End Try


        End If



    End Sub

    Private Sub SetExecutableFolderToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetExecutableFolderToolStripMenuItem.Click
        Dim FB As New Szunyi.IO.FolderSelectDialog()
        FB.Title = "Select Blast Programs Directory (bin)"
        If FB.ShowDialog = DialogResult.OK Then
            My.Settings.BlastPath = FB.FolderNames.First & "\"
            My.Settings.Save()
        End If
    End Sub

    Private Sub SetWorkingDirectoryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SetWorkingDirectoryToolStripMenuItem.Click
        Dim FB As New Szunyi.IO.FolderSelectDialog()
        FB.Title = "Select Working Directory "
        If FB.ShowDialog = DialogResult.OK Then
            My.Settings.DataPath = FB.FolderNames.First
            My.Settings.Db = My.Settings.DataPath & "\Db\"
            My.Settings.Result = My.Settings.DataPath & "\Result\"
            My.Settings.Fasta = My.Settings.DataPath & "\Fasta\"
            My.Settings.Filter = My.Settings.DataPath & "\Filter\"
            My.Settings.Tax = My.Settings.DataPath & "\Tax\"
            System.IO.Directory.CreateDirectory(My.Settings.DataPath)
            System.IO.Directory.CreateDirectory(My.Settings.Db)
            System.IO.Directory.CreateDirectory(My.Settings.Result)
            System.IO.Directory.CreateDirectory(My.Settings.Fasta)
            System.IO.Directory.CreateDirectory(My.Settings.Filter)
            System.IO.Directory.CreateDirectory(My.Settings.Tax)
            My.Settings.Save()
        End If
    End Sub

    Private Sub Tblb2_IndexChanged(Item As Object) Handles Tblb2.D_Click

        Dim HSPs = Szunyi.BLAST.BlastManipulation.Hsp.All(Tblb1.SelItem, Tblb2.SelItem)
        Dim var = From x In HSPs Select x.Record.Hits

        Dim profiles = HSPs.AsQueryable()
        Dim sg = Szunyi.Common.Text.General.GetText(Me.DisplayMembersAll, ",")
        If sg <> "" Then
            Try
                Dim var1 = profiles.Select("new(" & sg & ")")
                dgv1.DataSource = Nothing '

                Dim f = var1.OrderBy("Id")
                currAll = f
                dgv1.DataSource = f.ToDynamicList
                dgv1.Update()
            Catch ex As Exception
                Dim kj As Int16 = 54
            End Try


        End If

    End Sub

#End Region
#Region "Create Db"
    Private Iterator Function Get_GenBank(File As FileInfo) As IEnumerable(Of Bio.ISequence)

    End Function
    Private Iterator Function Get_Fasta() As IEnumerable(Of FileInfo)
        Dim Files = Szunyi.IO.Pick_Up.Fasta
        Dim wFIles As New List(Of FileInfo)
        For Each File In Files
            Dim fDir As New DirectoryInfo(My.Settings.Fasta)
            Dim nFIle = Szunyi.IO.Rename.Change_Directory(File, fDir)
            If nFIle.FullName <> File.FullName Then
                If nFIle.Exists = True Then
                    MsgBox("File is already in database. We used the stored one")
                Else
                    File.CopyTo(nFIle.FullName)
                    Yield nFIle
                End If
            Else
                Yield nFIle
            End If
        Next
    End Function
    Private Sub One_by_one_ProteinToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ProteinToolStripMenuItem.Click
        For Each File In Get_Fasta()
            Dim x As New Szunyi.BLAST.Console.CreateDatabase(File,
                                                               False,
                                                               New DirectoryInfo(My.Settings.BlastPath),
                                                                New DirectoryInfo(My.Settings.Db))
            x.DoIt()
        Next
    End Sub


#End Region
#Region "DoBlast"
#Region "MenuItems"
    Private Sub FromMixedGenBanksToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromMixedGenBanksToolStripMenuItem.Click
        Dim Files = Szunyi.IO.Pick_Up.GenBank
        Dim Title = InputBox("Enter Database Title")
        If Title = "" Then Exit Sub
        Dim nFile As New FileInfo(My.Settings.Fasta & Title & ".fa")
        Dim str As New System.Text.StringBuilder
        Using x As New Szunyi.IO.Export.Fasta_Exporter(nFile)

            Dim isNucle As Boolean = True
            For Each Seq In Szunyi.IO.Import.Sequences.Parse(Files)
                x.Write(Seq)
                str.Append(Seq.ID).Append(vbTab).Append(Szunyi.Features.GenBankMetaDataManipulation.Get_TaxID(Seq)).AppendLine()
                Dim MolType = Szunyi.Features.GenBankMetaDataManipulation.Get_Mol_Type(Seq)

                If MolType = Bio.IO.GenBank.MoleculeType.Protein Then isNucle = False
            Next
        End Using
        If str.Length > 0 Then str.Length -= 2
            Dim TaxFIle As New FileInfo(My.Settings.Tax & Title)
            Szunyi.IO.Export.Text(str.ToString, TaxFIle)
            Dim x1 As New Szunyi.BLAST.Console.CreateDatabase(nFile,
                                                               True,
                                                               New DirectoryInfo(My.Settings.BlastPath),
                                                                New DirectoryInfo(My.Settings.Db), TaxFIle)
            x1.DoIt()




    End Sub
    Private Sub FromGenBankToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromGenBankToolStripMenuItem.Click
        Dim Files = Szunyi.IO.Pick_Up.GenBank
        For Each File In Files
            Dim nFIle = Szunyi.IO.Rename.Change_Directory(File, New DirectoryInfo(My.Settings.Fasta))
            nFIle = Szunyi.IO.Rename.ChangeExtension(nFIle, Szunyi.IO.File_Extension.fasta)


            Dim Seqs = Szunyi.IO.Import.Sequences.Parse(File).ToList

            Dim Common_Name = Szunyi.Features.GenBankMetaDataManipulation.Get_Common_Name(Seqs.First).Replace(" ", "_")
            Dim TaxID = Szunyi.Features.GenBankMetaDataManipulation.Get_TaxID(Seqs.First)
            Dim nnFile = Szunyi.IO.Rename.Append_First(nFIle, TaxID & "_" & Common_Name)
            Szunyi.IO.Export.Fasta(Seqs, nnFile)
            Dim MolType = Szunyi.Features.GenBankMetaDataManipulation.Get_Mol_Type(Seqs.First)
            Dim isNucle As Boolean = True
            If MolType = Bio.IO.GenBank.MoleculeType.Protein Then isNucle = False
            Dim x As New Szunyi.BLAST.Console.CreateDatabase(nnFile,
                                                               True,
                                                               New DirectoryInfo(My.Settings.BlastPath),
                                                                New DirectoryInfo(My.Settings.Db), TaxID)
            x.DoIt()
            Dim kj As Int16 = 54

        Next



    End Sub
    Private Sub NucleotideToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NucleotideToolStripMenuItem.Click
        For Each File In Get_Fasta()
            Dim x As New Szunyi.BLAST.Console.CreateDatabase(File,
                                                               True,
                                                               New DirectoryInfo(My.Settings.BlastPath),
                                                                New DirectoryInfo(My.Settings.Db))
            x.DoIt()
        Next

    End Sub

    Private Sub BlastPQprotDBprotToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BlastPQprotDBprotToolStripMenuItem.Click
        Dim Files = GetDbFiles(False)
        If Files.Count = 0 Then Exit Sub

        Dim f1 As New CheckBoxForStringsFull(Files, -1)
        If f1.ShowDialog = DialogResult.Cancel Then Exit Sub
        StartApps(f1.SelectedFiles, "blastp")
    End Sub

    Private Sub BlastNToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BlastNToolStripMenuItem.Click
        Dim Files = GetDbFiles(True)
        If Files.Count = 0 Then Exit Sub

        Dim f1 As New Szunyi.IO.CheckBoxForStringsFull(Files, -1)
        If f1.ShowDialog = DialogResult.Cancel Then Exit Sub

        StartApps(f1.SelectedFiles, "blastn")
    End Sub

    Private Sub BlastXQTrNucDBprotToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BlastXQTrNucDBprotToolStripMenuItem.Click
        Dim Files = GetDbFiles(False)
        If Files.Count = 0 Then Exit Sub

        Dim f1 As New CheckBoxForStringsFull(Files, -1)
        If f1.ShowDialog = DialogResult.Cancel Then Exit Sub

        StartApps(f1.SelectedFiles, "blastx")
    End Sub

    Private Sub TBlastNQprotDBTrNuclToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TBlastNQprotDBTrNuclToolStripMenuItem.Click
        Dim Files = GetDbFiles(True)
        If Files.Count = 0 Then Exit Sub

        Dim f1 As New CheckBoxForStringsFull(Files, -1)
        If f1.ShowDialog = DialogResult.Cancel Then Exit Sub

        StartApps(f1.SelectedFiles, "tblastn")
    End Sub

    Private Sub TBlastXQTrNucDBTrNucToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TBlastXQTrNucDBTrNucToolStripMenuItem.Click
        Dim Files = GetDbFiles(True)
        If Files.Count = 0 Then Exit Sub

        Dim f1 As New CheckBoxForStringsFull(Files, -1)
        If f1.ShowDialog = DialogResult.Cancel Then Exit Sub

        StartApps(f1.SelectedFiles, "tblastx")
    End Sub


#End Region
    Private Function GetDbFiles(IsDNA As Boolean) As List(Of FileInfo)
        Dim res As New List(Of FileInfo)
        Dim files = Szunyi.IO.Get_Files.All(New DirectoryInfo(My.Settings.Db))

        If IsDNA = True Then
            Dim t = From x In files Where x.Extension = ".nhr"

            If t.Count > 0 Then Return t.ToList

            Return New List(Of FileInfo)
        Else
            Dim t = From x In files Where x.Extension = ".phr"

            If t.Count > 0 Then Return t.ToList

            Return New List(Of FileInfo)
        End If
        Return res
    End Function

    Private Sub StartApps(DbFiles As List(Of FileInfo), SelectedProgram As String)
        Dim QueryFiles = Get_Fasta().ToList

        If IsNothing(DbFiles) = True Or IsNothing(QueryFiles) = True Then Exit Sub
        If DbFiles.Count = 0 AndAlso QueryFiles.Count = 0 Then Exit Sub


        Dim Helper As New Szunyi.BLAST.Helper
        Dim t As New CheckBoxForStringsFull(Helper.Get_FileTypes, -1, "Select Output Type", "A")
        If t.ShowDialog = DialogResult.OK Then
            For Each Item In t.SelectedStrings
                Dim outfmt = Helper.Get_outfmt_Value(Item)
                Dim q As String = ""
                If outfmt = 7 Then
                    Dim Quals() = Split("qseqid qgi qacc sseqid sallseqid sgi sallgi sacc sallacc qstart qend sstart send qseq sseq evalue bitscore score length pident nident mismatch positive gapopen gaps ppos frames qframe sframe btop staxids sscinames scomnames sblastnames sskingdoms stitle salltitles sstrand qcovs qcovhsp qcovus")
                    Dim t1 As New CheckBoxForStringsFull(Quals.ToList, -1, "Select Qulifiers", Quals.ToList)

                    If t1.ShowDialog = DialogResult.OK Then
                        q = Szunyi.Common.Text.General.GetText(t1.SelectedStrings, " ")
                    End If
                End If
                Dim x As New Szunyi.BLAST.Console.DoBlast(QueryFiles,
                                                           DbFiles,
                                                           SelectedProgram,
                                                           outfmt, New DirectoryInfo(My.Settings.BlastPath),
                                                           New DirectoryInfo(My.Settings.Result), q)

                Me.CreateBgWork(x.ToString, x)
            Next
        End If


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

#End Region

#Region "BgWork"
    Private Sub CreateBgWork(Type As String, t As Object)
        Dim w = New BackgroundWorker
        w.WorkerReportsProgress = True
        w.WorkerSupportsCancellation = True
        AddHandler w.DoWork, AddressOf WorkerDoWork
        AddHandler w.ProgressChanged, AddressOf WorkerProgressChanged
        AddHandler w.RunWorkerCompleted, AddressOf WorkerCompleted

        w.RunWorkerAsync(t)

    End Sub
    Private Sub WorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs)
        Try
            Select Case e.Result.Type

            End Select
        Catch ex As Exception
            Dim alf As Int16 = 54
        End Try
    End Sub

    Private Sub WorkerProgressChanged(sender As Object, e As ProgressChangedEventArgs)
        'Throw New NotImplementedException()
    End Sub

    Private Sub WorkerDoWork(sender As Object, e As DoWorkEventArgs)

        e.Result = e.Argument
        Try
            e.Argument.DoIt
        Catch ex As Exception
            Dim alf As Int16 = 43

        End Try


    End Sub


#End Region

#Region "Export"
    Private Sub QueryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QueryToolStripMenuItem.Click
        'Export.Query_Sequences(Me.ClonedAndFilteredBlastSearchRecords, Me.OpenedFiles)
    End Sub

    Private Sub HitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HitToolStripMenuItem.Click

    End Sub

    Private Sub QueryAndHitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles QueryAndHitToolStripMenuItem.Click

    End Sub

    Private Sub HitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles HitToolStripMenuItem1.Click

    End Sub

    Private Sub QueryToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles QueryToolStripMenuItem1.Click

    End Sub

    Private Sub HitQueryToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HitQueryToolStripMenuItem.Click

    End Sub

    Private Sub Gff3ToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles Gff3ToolStripMenuItem.Click

    End Sub

    Private Sub BestToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles BestToolStripMenuItem.Click

    End Sub

    Private Sub AllToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AllToolStripMenuItem.Click

    End Sub

    Private Sub RecordsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RecordsToolStripMenuItem1.Click
        Dim Values = Szunyi.IO.Util_Helpers.Get_All_Enum_Names(Of Szunyi.BLAST.Enums.Record)(Szunyi.BLAST.Enums.Record.IterationMessage)
        Dim x As New Szunyi.IO.CheckBoxForStringsFull(Values, -1, "Set export member of Record", "IterationQueryDefinition")
        If x.ShowDialog = DialogResult.OK Then
            Dim Filtered As New List(Of String)
            Dim Header = Szunyi.Common.Text.General.GetText(x.SelectedStrings, vbTab)
            Filtered.Add(Header)
            Dim Types = Szunyi.Common.Util_Helpers.Get_Enums(Of Szunyi.BLAST.Enums.Record)(x.SelectedStrings)
            Filtered.AddRange(Szunyi.BLAST.BlastManipulation.Record.Custom(Me.ClonedAndFilteredBlastSearchRecords, Types))
            Dim s = Szunyi.Common.Text.General.GetText(Filtered)
            Clipboard.SetText(s)
            Dim kj As Int16 = 54
        End If
    End Sub

    Private Sub ResetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem.Click
        Me.ClonedAndFilteredBlastSearchRecords = OriginalBlastSearchRecords.Clone
        Tblb1.SetIt(Me.ClonedAndFilteredBlastSearchRecords, Me.DisplayMemberofRecord)
    End Sub

    Private Sub MathToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MathToolStripMenuItem.Click
        Dim x As New MathEvalution(Me.ClonedAndFilteredBlastSearchRecords)

        If x.ShowDialog = DialogResult.OK Then
            Me.ClonedAndFilteredBlastSearchRecords = x.clonedAndFilteredBlastSearchRecords
            Tblb1.SetIt(Me.ClonedAndFilteredBlastSearchRecords, Me.DisplayMemberofRecord)
        End If
    End Sub




#End Region
End Class
