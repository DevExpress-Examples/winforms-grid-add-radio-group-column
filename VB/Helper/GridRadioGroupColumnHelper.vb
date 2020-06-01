Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraEditors.Repository
Imports DevExpress.XtraGrid.Columns
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraEditors

Namespace WindowsApplication1
	Public Class GridRadioGroupColumnHelper

		Public Event SelectedRowChanged As EventHandler
		Private _GridView As GridView

		Private _RepositoryItem As New RepositoryItemCheckEdit()
		Public Property RadioRepositoryItem() As RepositoryItemCheckEdit
			Get
				Return _RepositoryItem
			End Get
			Set(ByVal value As RepositoryItemCheckEdit)
				_RepositoryItem = value
			End Set
		End Property

		Private _RadioGroupColumn As New GridColumn()
		Public Property RadioGroupColumn() As GridColumn
			Get
				Return _RadioGroupColumn
			End Get
			Set(ByVal value As GridColumn)
				_RadioGroupColumn = value
			End Set
		End Property


		Private _SelectedDataSourceRowIndex As Integer
		Public Property SelectedDataSourceRowIndex() As Integer
			Get
				Return _SelectedDataSourceRowIndex
			End Get
			Set(ByVal value As Integer)
				If _SelectedDataSourceRowIndex <> value Then
					Dim oldRowIndex As Integer = _SelectedDataSourceRowIndex
					_SelectedDataSourceRowIndex = value
					SetRowChecked(oldRowIndex, False)
					OnSelectedRowChanged(oldRowIndex, value)
				End If
			End Set
		End Property

		Private Sub SetRowChecked(ByVal dataSourceRowIndex As Integer, ByVal value As Boolean)
			Dim rowHandle As Integer = _GridView.GetRowHandle(dataSourceRowIndex)
			_GridView.SetRowCellValue(rowHandle, RadioGroupColumn, value)
		End Sub
		Public Sub New(ByVal gridView As GridView)
			_GridView = gridView
			_GridView.BeginUpdate()
			InitGridView()
			InitColumn()
			InitRepositoryItem()
			_GridView.EndUpdate()
		End Sub
		Private Sub InitGridView()
			AddHandler _GridView.CustomUnboundColumnData, AddressOf _GridView_CustomUnboundColumnData
		End Sub

		Private Sub _GridView_CustomUnboundColumnData(ByVal sender As Object, ByVal e As CustomColumnDataEventArgs)
			If e.Column Is RadioGroupColumn Then
				If e.IsGetData Then
					e.Value = e.ListSourceRowIndex = SelectedDataSourceRowIndex
				End If
				If e.IsSetData Then
					If e.Value.Equals(True) Then
						SelectedDataSourceRowIndex = e.ListSourceRowIndex
					End If
				End If
			End If
		End Sub
		Private Sub InitColumn()
			RadioGroupColumn.FieldName = "RadioGroupColumn"
			_GridView.Columns.Add(RadioGroupColumn)
			RadioGroupColumn.Visible = True
			RadioGroupColumn.ColumnEdit = RadioRepositoryItem
			RadioGroupColumn.Caption = "Radio"
			RadioGroupColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean
			RadioGroupColumn.MaxWidth = 50
		End Sub
		Private Sub InitRepositoryItem()
			RadioRepositoryItem.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio
			AddHandler RadioRepositoryItem.EditValueChanged, AddressOf RadioRepositoryItem_EditValueChanged
			_GridView.GridControl.RepositoryItems.Add(RadioRepositoryItem)
		End Sub


		Private Sub RadioRepositoryItem_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs)
			_GridView.PostEditor()
		End Sub

		Private Sub OnSelectedRowChanged(ByVal oldValue As Integer, ByVal newValue As Integer)
			RaiseSelectedRowChanged()
		End Sub
		Protected Overridable Sub RaiseSelectedRowChanged()
			RaiseEvent SelectedRowChanged(_GridView, EventArgs.Empty)
		End Sub

		Public Sub Disable()
			RemoveHandler RadioRepositoryItem.EditValueChanged, AddressOf RadioRepositoryItem_EditValueChanged
			RemoveHandler _GridView.CustomUnboundColumnData, AddressOf _GridView_CustomUnboundColumnData
			_GridView = Nothing
			RadioRepositoryItem = Nothing
		End Sub

	End Class
End Namespace
