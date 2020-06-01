Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms

Namespace WindowsApplication1
	Partial Public Class Form1
		Inherits Form

				Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("ID", GetType(Integer))
			tbl.Columns.Add("Number", GetType(Integer))
			tbl.Columns.Add("Date", GetType(DateTime))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i), i, 3 - i, DateTime.Now.AddDays(i) })
			Next i
			Return tbl
				End Function


		Private _Helper As GridRadioGroupColumnHelper
		Public Sub New()
			InitializeComponent()
			gridControl1.DataSource = CreateTable(20)
			_Helper = New GridRadioGroupColumnHelper(gridView1)
			AddHandler _Helper.SelectedRowChanged, AddressOf _Helper_SelectedRowChanged

		End Sub

		Private Sub _Helper_SelectedRowChanged(ByVal sender As Object, ByVal e As EventArgs)
			Text = _Helper.SelectedDataSourceRowIndex.ToString()
		End Sub


	End Class
End Namespace