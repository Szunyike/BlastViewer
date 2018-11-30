Imports System.ComponentModel
Imports System.IO
Imports Bio.Web.Blast
Imports Szunyi.IO
Imports Szunyi.BLAST.Enums
Imports Szunyi.BLAST
Imports Szunyi.Common
Imports Szunyi.Sequences.Extensions
Imports System.Linq.Dynamic.Core
Imports System.Text
Imports Bio.IO.GenBank
Imports Szunyi.Features.Extensions
Imports Szunyi.Common.Extensions

'Imports System.Linq.Dynamic


Public Class Form1
    Private SortOrders(25) As Integer
    Dim TheBlastResult As New List(Of Bio.Web.Blast.BlastResult)
    ''' <summary>
    ''' This is always containig the original result
    ''' </summary>
    Dim OriginalBlastSearchResults As New List(Of OwnBlastRecord)
    ''' <summary>
    ''' This is contains the filtered Blast Results which can be Reset
    ''' </summary>
    Dim OriginalBlastSearchRecords As New List(Of OwnBlastRecord)
    Dim ClonedAndFilteredBlastSearchRecords As New List(Of OwnBlastRecord)
    Dim OpenedFiles As New List(Of FileInfo)

    Private DisplayMemberofHit As String = "Accession"
    Private DisplayMemberofRecord As String = "IterationQueryDefinition"
    Private DisplayMembersAll As New List(Of String)
    Private currAll As IQueryable
    Private ByFiles As New List(Of List(Of OwnBlastRecord))
    Private Sub ImportFromExternalIDsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportFromExternalIDsToolStripMenuItem.Click

    End Sub

    Private Sub ImportCompressedToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ImportCompressedToolStripMenuItem.Click
        Dim Files = Szunyi.IO.Pick_Up.Files(Szunyi.IO.File_Extensions.Blast, "Select Blast Result", New DirectoryInfo(My.Settings.Result)).ToList
        Dim log As System.Text.StringBuilder
        OpenedFiles = Files
        ClonedAndFilteredBlastSearchRecords.Clear()
        For Each File In Files
            Dim x As New Szunyi.BLAST.Compressed.Common(File)

        Next
        Me.OriginalBlastSearchRecords = ClonedAndFilteredBlastSearchRecords.Clone
        Tblb1.SetIt(ClonedAndFilteredBlastSearchRecords, DisplayMemberofRecord)

    End Sub


    Private Function GetFilteredBlastSearchRecords(Filter As String) As List(Of OwnBlastRecord)
        Dim res = From x In Me.ClonedAndFilteredBlastSearchRecords Where x.IterationQueryDefinition.ToUpper.Contains(Filter.ToUpper)

        If res.Count > 0 Then
            Return res.ToList
        Else
            Return New List(Of OwnBlastRecord)

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
            Dim props = Szunyi.IO.Util_Helpers.Set_Filter_Settings(Of Szunyi.BLAST.Enums.Hsp)(x.Setting)
            Dim c = Szunyi.BLAST.Filter.Filter_Hits(props, Me.ClonedAndFilteredBlastSearchRecords)
            Dim kj As Int16 = 54
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
        Dim Records = Values.InsertBefore("Record.").ToList
        Values = Szunyi.IO.Util_Helpers.Get_All_Enum_Names(Of Szunyi.BLAST.Enums.Hit)(Szunyi.BLAST.Enums.Hit.Accession)
        Records.AddRange(Values.InsertBefore("Hit."))
        Values = Szunyi.IO.Util_Helpers.Get_All_Enum_Names(Of Szunyi.BLAST.Enums.Hsp)(Szunyi.BLAST.Enums.Hsp.AlignmentLength)
        Records.AddRange(Values.InsertBefore("Hsp."))

        Dim x As New Szunyi.IO.CheckBoxForStringsFull(Records, -1, "Set Display member of Record", Me.DisplayMembersAll)
        If x.ShowDialog = DialogResult.OK Then
            Me.DisplayMembersAll = x.SelectedStrings
            Tblb1.SetIt(ClonedAndFilteredBlastSearchRecords, Me.DisplayMemberofRecord)
        End If
    End Sub

    Private Sub tbl1(Item As Object) Handles Tblb1.IndexChanged
        Dim curr As OwnBlastRecord = Item
        Tblb2.SetIt(curr.Hits, DisplayMemberofHit)
        Dim HSPs = Szunyi.BLAST.BlastManipulation.Hsp.All_Own(curr)
        Dim var = From x In HSPs Select x.Record.Hits

        Dim profiles = HSPs.AsQueryable()
        Dim sg = Me.DisplayMembersAll.GetText(",")
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

        Dim HSPs As List(Of extHSP) = Szunyi.BLAST.BlastManipulation.Hsp.All_Own(Tblb1.SelItem, Tblb2.SelItem)
        Dim var = From x In HSPs Select x.Record.Hits

        Dim profiles = HSPs.AsQueryable()
        Dim sg = Me.DisplayMembersAll.GetText(",")
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
                    Yield nFIle
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
    Private Sub FromGenBanksCDSToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromGenBanksCDSToolStripMenuItem.Click
        Dim Files = Szunyi.IO.Pick_Up.GenBank
        Dim Title = InputBox("Enter Database Title")
        If Title = "" Then Exit Sub

        Dim str As New System.Text.StringBuilder
        Using Tax_Writter As New Szunyi.IO.Export.Text_Exporter(New FileInfo(My.Settings.Tax & Title & ".txt"))
            Using x_NA As New Szunyi.IO.Export.Fasta_Exporter(New FileInfo(My.Settings.Fasta & Title & "_NA.fa"))
                Using x_AA As New Szunyi.IO.Export.Fasta_Exporter(New FileInfo(My.Settings.Fasta & Title & "_AA.fa"))
                    Dim Index As Integer = 0
                    For Each File In Files
                        For Each Seq In Szunyi.IO.Import.Sequences.Parse(Files)
                            Dim ls As New List(Of String) ' FileFullName,CommonName,Strain,Accession (SeqID),Length
                            ls.Add(File.FullName)
                            ls.Add(Seq.taxid)
                            ls.Add(Seq.CommonName)
                            ls.Add(Seq.Strain)
                            ls.Add(Seq.Accesion)

                            Tax_Writter.Write(ls.GetText & vbCrLf, False)

                            For Each CDS In Seq.Get_Features(StandardFeatureKeys.CodingSequence)
                                Index += 1
                                Dim NA = CDS.GetSubSequence(Seq)
                                If CDS.Location.IsComplementer = True Then NA = NA.GetReversedSequence
                                NA.ID = CDS.CommonName & "_" & Index

                                Dim AA = NA.Translate
                                AA.ID = NA.ID
                                x_NA.Write(NA)
                                x_AA.Write(AA)

                                Tax_Writter.Write(Seq.ID & vbTab & vbCrLf, False)
                            Next
                        Next
                    Next
                End Using
            End Using

        End Using
    End Sub

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
                str.Append(Seq.ID).Append(vbTab).Append(Seq.TaxId).AppendLine()
                Dim MolType = Seq.MoleculeType
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

            Dim Common_Name = (Seqs.First).CommonName.Replace(" ", "_")
            Dim TaxID = Seqs.First.TaxId
            Dim nnFile = Szunyi.IO.Rename.Append_First(nFIle, TaxID & "_" & Common_Name)
            Szunyi.IO.Export.Fasta(Seqs, nnFile)
            Dim MolType = Seqs.First.MoleculeType
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
                    Dim Quals() = Split("slen qlen qseqid qgi qacc sseqid sallseqid sgi sallgi sacc sallacc qstart qend sstart send qseq sseq evalue bitscore score length pident nident mismatch positive gapopen gaps ppos frames qframe sframe btop staxids sscinames scomnames sblastnames sskingdoms stitle salltitles sstrand qcovs qcovhsp qcovus")
                    Dim t1 As New CheckBoxForStringsFull(Quals.ToList, -1, "Select Qulifiers", Quals.ToList)

                    If t1.ShowDialog = DialogResult.OK Then
                        q = t1.SelectedStrings.GetText(" ")
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
            Dim Header = x.SelectedStrings.GetText(vbTab)
            Filtered.Add(Header)
            Dim Types = Szunyi.Common.Util_Helpers.Get_Enums(Of Szunyi.BLAST.Enums.Record)(x.SelectedStrings)
            '   Filtered.AddRange(Szunyi.BLAST.BlastManipulation.Record.Custom(Me.ClonedAndFilteredBlastSearchRecords, Types))
            '    Dim s = Szunyi.Common.Text.General.GetText(Filtered)
            '    Clipboard.SetText(s)
            Dim kj As Int16 = 54
        End If
    End Sub

    Private Sub ResetToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ResetToolStripMenuItem.Click
        Me.ClonedAndFilteredBlastSearchRecords = Me.OriginalBlastSearchRecords

        Tblb1.SetIt(Me.ClonedAndFilteredBlastSearchRecords, Me.DisplayMemberofRecord)
    End Sub

    Private Sub MathToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MathToolStripMenuItem.Click
        Dim x As New MathEvalution(Me.ClonedAndFilteredBlastSearchRecords)

        If x.ShowDialog = DialogResult.OK Then
            Me.ClonedAndFilteredBlastSearchRecords = x.cOwnRecords
            Tblb1.SetIt(Me.ClonedAndFilteredBlastSearchRecords, Me.DisplayMemberofRecord)
        End If
    End Sub

    Private Sub ByTaxIDToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ByTaxIDToolStripMenuItem.Click
        Dim res As New Dictionary(Of String, Dictionary(Of String, Integer))
        Dim AllTaxID As New List(Of String)
        Dim AllTaxName As New List(Of String)
        For Each Item As OwnBlastRecord In Me.ClonedAndFilteredBlastSearchRecords
            If IsNothing(Item.IterationQueryDefinition) = False Then
                res.Add(Item.IterationQueryDefinition, New Dictionary(Of String, Integer))
                For Each oHit In Item.OwnHits
                    If res(Item.IterationQueryDefinition).ContainsKey(oHit.OwnHsps.First.TaxID) = False Then
                        res(Item.IterationQueryDefinition).Add(oHit.OwnHsps.First.TaxID, 0)
                    End If
                    res(Item.IterationQueryDefinition)(oHit.OwnHsps.First.TaxID) += 1
                    If AllTaxID.Contains(oHit.OwnHsps.First.TaxID) = False Then
                        AllTaxID.Add(oHit.OwnHsps.First.TaxID)
                        AllTaxName.Add(oHit.OwnHsps.First.CommonName)
                    End If
                Next
            End If
        Next
        Dim Log As New System.Text.StringBuilder
        Log.Append(vbTab)
        Log.Append(AllTaxName.GetText)
        For Each Item In res
            Log.Append(Item.Key)
            Dim c As Integer = 0
            For Each cTaxId In AllTaxID

                If Item.Value.ContainsKey(cTaxId) = True Then
                    Log.Append(vbTab)
                    Log.Append(Item.Value(cTaxId))
                    c += 1
                Else
                    Log.Append(vbTab).Append("0")
                End If
            Next
            Log.Append(vbTab & c)
            Log.AppendLine()
        Next
        Dim kj As Integer = 65
    End Sub

    Private Sub ByFileToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ByFileToolStripMenuItem.Click


        Dim AllTaxName = (From x In Me.OpenedFiles Select x.Name).ToList

        Dim Res = Szunyi.BLAST.Analysis.ByFileName(Me.ClonedAndFilteredBlastSearchRecords, AllTaxName)

        Dim Common = Analysis.Get_Common(Res, AllTaxName.Count)
        Dim Uniques = Analysis.Get_Uniques(Res, AllTaxName.Count)
        Dim Common_Uniques = Analysis.Get_Common_Uniques(Res, AllTaxName.Count)
        Dim extHSPs = Szunyi.BLAST.BlastManipulation.Hsp.All(Me.ClonedAndFilteredBlastSearchRecords)
        If Common_Uniques.Count = 0 Then
            MsgBox("No any Query")
        Else
            Dim ForRetrive As Dictionary(Of String, List(Of String)) =
            Szunyi.BLAST.Analysis.Get_HitIDs_w_DbFileNames(Me.ClonedAndFilteredBlastSearchRecords, Common_Uniques) ' DB, HitIDs

            Dim t As New Szunyi.IO.FolderSelectDialog()
            t.Title = ForRetrive.Count & "- e:" & ForRetrive.First.Value.Count
            Dim ByCDS_NA(ForRetrive.First.Value.Count) As List(Of Bio.ISequence)
            Dim ByCDS_AA(ForRetrive.First.Value.Count) As List(Of Bio.ISequence)
            If t.ShowDialog = DialogResult.OK Then
                Dim log As New System.Text.StringBuilder
                Dim cDir As New DirectoryInfo(t.FolderNames.First)
                For Each Item In ForRetrive
                    Dim Seqs = Szunyi.BLAST.Console.Retrive.GetSeqsFromBlastDatabase(New FileInfo(Item.Key), Item.Value, log, My.Settings.BlastPath)
                    Dim nFile As New FileInfo(Item.Key)
                    Dim NA_File = Szunyi.IO.Rename.Append_Before_Extension(nFile, "_NA")
                    Dim AA_File = Szunyi.IO.Rename.Append_Before_Extension(nFile, "_AA")
                    Szunyi.IO.Export.Fasta(Seqs, New FileInfo(cDir.FullName & "\" & NA_File.Name))
                    Szunyi.IO.Export.Fasta(Seqs.TranslateFull(True), AA_File)
                    For i1 = 0 To Seqs.Count - 1
                        If IsNothing(ByCDS_NA(i1)) = True Then
                            ByCDS_NA(i1) = New List(Of Bio.ISequence)
                            ByCDS_AA(i1) = New List(Of Bio.ISequence)
                        End If
                        Seqs(i1).ID = Item.Key
                        ByCDS_NA(i1).Add(Seqs(i1))
                        ByCDS_AA(i1).Add(Seqs(i1).TranslateFull(True))
                    Next
                Next
                For i1 = 0 To ByCDS_NA.Count - 1
                    Szunyi.IO.Export.Fasta(ByCDS_NA(i1), New FileInfo(cDir.FullName & "\" & i1 & "_NA.fa"))
                    Szunyi.IO.Export.Fasta(ByCDS_AA(i1), New FileInfo(cDir.FullName & "\" & i1 & "_AA.fa"))
                Next
            End If

        End If




    End Sub
    ''' <summary>
    ''' Pure
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FromFilesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromFilesToolStripMenuItem.Click
        Me.OpenedFiles = Szunyi.IO.Pick_Up.Files(Szunyi.IO.File_Extensions.Blast, "Select Blast Result", New DirectoryInfo(My.Settings.Result)).ToList
        Dim All_Records = Import_Records()
        Me.ClonedAndFilteredBlastSearchRecords = Group_Records(All_Records)
        Me.OriginalBlastSearchRecords = ClonedAndFilteredBlastSearchRecords.Clone
        Tblb1.SetIt(ClonedAndFilteredBlastSearchRecords, DisplayMemberofRecord)

    End Sub



    ''' <summary>
    ''' Pure
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FromDatabaseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FromDatabaseToolStripMenuItem.Click
        Me.OpenedFiles = Get_DatabaseFiles()
        Dim All_Records = Import_Records()
        Me.ClonedAndFilteredBlastSearchRecords = Group_Records(All_Records)
        Me.OriginalBlastSearchRecords = ClonedAndFilteredBlastSearchRecords.Clone
        Tblb1.SetIt(ClonedAndFilteredBlastSearchRecords, DisplayMemberofRecord)
    End Sub
    ''' <summary>
    ''' With External IDs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FromFilesToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FromFilesToolStripMenuItem1.Click
        Me.OpenedFiles = Szunyi.IO.Pick_Up.Files(Szunyi.IO.File_Extensions.Blast, "Select Blast Result", New DirectoryInfo(My.Settings.Result)).ToList
        Dim All_Records = Import_Records()
        Me.ClonedAndFilteredBlastSearchRecords = Group_Records(All_Records)
        Me.OriginalBlastSearchRecords = ClonedAndFilteredBlastSearchRecords.Clone
        Tblb1.SetIt(ClonedAndFilteredBlastSearchRecords, DisplayMemberofRecord)
    End Sub
    ''' <summary>
    ''' With External IDs
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FromDatabaseToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles FromDatabaseToolStripMenuItem1.Click
        Me.OpenedFiles = Get_DatabaseFiles()
        Dim All_Records = Import_Records()
        Me.ClonedAndFilteredBlastSearchRecords = Group_Records(All_Records)
        Me.OriginalBlastSearchRecords = ClonedAndFilteredBlastSearchRecords.Clone
        Tblb1.SetIt(ClonedAndFilteredBlastSearchRecords, DisplayMemberofRecord)
    End Sub
    Private Function Get_DatabaseFiles() As List(Of FileInfo)
        Dim All_Result_Files = Szunyi.IO.Get_Files.All(New DirectoryInfo(My.Settings.Result))
        Dim All_Result_Files_Names = From x1 In All_Result_Files Select x1.Name
        Dim x As New Szunyi.IO.CheckBoxForStringsFull(All_Result_Files.ToList, -1)
        If x.ShowDialog = DialogResult.OK Then
            Return x.SelectedFiles
        Else
            Return New List(Of FileInfo)
        End If
    End Function
    Private Function Import_Records() As List(Of OwnBlastRecord)
        Dim All_record As New List(Of OwnBlastRecord)
        ClonedAndFilteredBlastSearchRecords.Clear()
        ByFiles.Clear()
        Dim log As New System.Text.StringBuilder
        For Each File In OpenedFiles
            Dim Records = Szunyi.BLAST.Import.From_File(File, log).ToList
            ByFiles.Add(Records)
            All_record.AddRange(Records)
        Next
        Return All_record
    End Function
    Private Function Group_Records(all_Records As List(Of OwnBlastRecord)) As List(Of OwnBlastRecord)
        Dim res As New List(Of OwnBlastRecord)

        Dim gr = From x2 In all_Records Group By x2.IterationQueryDefinition Into Group

        For Each g In gr
            Dim t As OwnBlastRecord
            Dim cHits As New List(Of Bio.Web.Blast.Hit)
            For Each item In g.Group
                If item Is g.Group.First Then
                    t = item.Clone
                Else
                    t.OwnHits.AddRange(item.OwnHits)
                    cHits.AddRange(item.Hits)
                End If
            Next
            For Each c In cHits
                t.Hits.Add(c)
            Next
            t.IterationQueryDefinition = g.IterationQueryDefinition
            res.Add(t)
        Next
        Return res
    End Function

    Private Sub dgv1_RowEnter(sender As Object, e As DataGridViewCellEventArgs) Handles dgv1.RowEnter
        Dim alf As Integer = 43
        Dim x = dgv1.Rows(e.RowIndex).DataBoundItem
        Dim br As SolidBrush
        Select Case x.Score
            Case Is < 40
                br = New SolidBrush(Color.Black)
            Case Is < 50
                br = New SolidBrush(Color.Blue)
            Case Is < 80
                br = New SolidBrush(Color.Green)
            Case Is < 200
                br = New SolidBrush(Color.Pink)
            Case Else
                br = New SolidBrush(Color.Red)
        End Select
        Dim bmp As New Bitmap(pb1.Width, pb1.Height)
        Dim graphics As Graphics = Graphics.FromImage(bmp)

        Dim Range = x.IterationQueryLength \ pb1.Width
        If Range = 0 Then
            Range = pb1.Width \ x.IterationQueryLength
            Dim x1 As Integer = (x.QueryStart * Range)
            Dim x2 As Integer = (x.QueryEnd * Range)
            graphics.DrawLine(New Pen(br, 5), x1, 5, x2, 5)
        Else
            Dim x1 As Integer = (x.QueryStart / Range)
            Dim x2 As Integer = (x.QueryEnd / Range)
            graphics.DrawLine(New Pen(br, 5), x1, 5, x2, 5)
        End If


        Range = x.Length \ pb1.Width
        If Range = 0 Then
            If x.Length > 0 Then
                Range = pb1.Width \ x.Length
                Dim x1 As Integer = (x.HitStart * Range)
                Dim x2 As Integer = (x.HitEnd * Range)
                graphics.DrawLine(New Pen(br, 5), x1, 15, x2, 15)
            End If

        Else
            Dim x1 As Integer = (x.HitStart / Range)
            Dim x2 As Integer = (x.HitEnd / Range)
            graphics.DrawLine(New Pen(br, 5), x1, 15, x2, 15)
        End If


        Dim myFont As Font = tbAlignment.Font

        Dim Fs = graphics.MeasureString("a", myFont)
        DrawTextBox(x, Fs.Width)
        pb1.Image = bmp
        graphics.Dispose()

    End Sub
    Private Sub DrawTextBox(hsp As Object, FontWidth As Double)
        Dim NofCharPerLine As Integer = (Me.tbAlignment.Width) \ FontWidth

        Dim myFont As Font = tbAlignment.Font
        Dim graphics As Graphics = tbAlignment.CreateGraphics
        Dim s As New StringBuilder
        Do
            s.Append("a")
            Dim Fs = graphics.MeasureString(s.ToString, myFont)
            If Fs.Width > tbAlignment.Width - 10 Then
                NofCharPerLine = s.Length - 2
                Exit Do
            End If
        Loop

        Dim str As New StringBuilder
        If IsNothing(hsp.QuerySequence) = True Then Exit Sub
        For i1 = 0 To hsp.QuerySequence.Length Step NofCharPerLine
            If i1 + NofCharPerLine > hsp.QuerySequence.Length Then NofCharPerLine = hsp.QuerySequence.Length - i1
            str.Append(hsp.QuerySequence.Substring(i1, NofCharPerLine)).AppendLine()
            If IsNothing(hsp.Midline) = False Then str.Append(hsp.Midline.Substring(i1, NofCharPerLine)).AppendLine()
            str.Append(hsp.HitSequence.Substring(i1, NofCharPerLine)).AppendLine().AppendLine()
        Next
        Me.tbAlignment.Text = str.ToString
    End Sub

    Private Sub TableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TableToolStripMenuItem.Click
        Dim Files = Szunyi.IO.Pick_Up.GenBank.ToList
        Dim Seqs = Szunyi.IO.Import.Sequences.Parse(Files).ToList
        If Seqs.Count = 0 Then Exit Sub
        Dim str As New System.Text.StringBuilder
        For Each Seq In Seqs
            str.Append(Seq.Convert_To5table).AppendLine()
        Next
        For Each Seq In Seqs
            For Each Intron In Seq.Get_Introns
                If Intron.IsIntronGTAT(Seq) = True Then

                    Dim kj As Int16 = 54
                End If
            Next
        Next
            str.Length -= 2
        Clipboard.SetText(str.ToString)
        Szunyi.IO.Export.Text(str.ToString)
    End Sub

    Private Sub SplitByHitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SplitByHitToolStripMenuItem.Click
        Dim log As New System.Text.StringBuilder

        Dim OutDir = Szunyi.IO.Pick_Up.Directory
        Dim t As New Bio.Sequence(Bio.Alphabets.DNA, "T")
        For Each File In Get_DatabaseFiles()
            Dim Exps As New Dictionary(Of String, List(Of Bio.ISequence))
            Dim OriginalFastaFile = File.QueryFastaFile(My.Settings.Fasta)
            Dim Seqs = Szunyi.IO.Import.Sequences.Parse(OriginalFastaFile).ToList
            Dim c As New Szunyi.Sequences.Sorters.ByID
            Seqs.Sort(c)

            If IsNothing(OriginalFastaFile) = False Then
                For Each record In Szunyi.BLAST.Import.From_File(File, log)
                    For Each ExtHSP In record.ToExtHsps
                        If Exps.ContainsKey(ExtHSP.Hit.Id) = False Then Exps.Add(ExtHSP.Hit.Id, New List(Of Bio.ISequence))

                        t.ID = ExtHSP.Record.IterationQueryDefinition
                        Dim x = Seqs.SearchByID(t)
                        If IsNothing(x) = False Then
                            Exps(ExtHSP.Hit.Id).Add(x)
                        Else
                            log.Append(ExtHSP.Record.IterationQueryDefinition).AppendLine()
                        End If
                    Next
                Next
                Dim AllSeqs As New List(Of Bio.ISequence)
                For Each Item In Exps
                    AllSeqs.AddRange(Item.Value)
                    Dim OneCopy = Item.Value.OneCopyBy_ID
                    Dim Duplicates = Item.Value.DuplicatesBy_ID
                    Szunyi.IO.Export.Fasta(OneCopy, New FileInfo(OutDir.FullName & "\" & File.Name & "," & Item.Key & "_Unique.fa"))
                    Szunyi.IO.Export.Fasta(Duplicates.Firsts, New FileInfo(OutDir.FullName & "\" & File.Name & "," & Item.Key & "_Duplicate.fa"))
                Next
                AllSeqs.Sort(c)
                Dim NotFounded = Seqs.Distinct_ByID(AllSeqs)
                Szunyi.IO.Export.Fasta(NotFounded, New FileInfo(OutDir.FullName & "\" & File.Name & "_NotFounded.fa"))
            End If

        Next

    End Sub

#End Region
End Class
