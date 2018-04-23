Imports Microsoft.VisualBasic
Imports System.Windows.Input
Imports DevExpress.Xpf.Core.Commands
Imports RichEditMVVMScenarioSL.CarInfoServiceReference

Namespace RichEditMVVMScenarioSL.ViewModel
	Public Class CarInfoViewModel
		Inherits ObservableObject
		Private model As RichEditMVVMScenarioSL.Model.CarInfo

		Public Sub New()
			Me.New(New RichEditMVVMScenarioSL.Model.CarInfo())

			LoadCommand.Execute(Nothing)
		End Sub

		Public Sub New(ByVal model As RichEditMVVMScenarioSL.Model.CarInfo)
			Me.model = model

			name_Renamed = model.Name
			description_Renamed = model.Description

            LoadCommand = New DevExpress.Xpf.Mvvm.DelegateCommand(Of Object)(AddressOf LoadCommandExecute)
            SaveCommand = New DevExpress.Xpf.Mvvm.DelegateCommand(Of Object)(AddressOf SaveCommandExecute)
		End Sub

		Private name_Renamed As String
		Private description_Renamed As String

		Public Property Name() As String
			Get
				Return name_Renamed
			End Get
			Set(ByVal value As String)
				name_Renamed = value
				OnPropertyChanged("Name")
			End Set
		End Property

		Public Property Description() As String
			Get
				Return description_Renamed
			End Get
			Set(ByVal value As String)
				description_Renamed = value
				OnPropertyChanged("Description")
			End Set
		End Property

		Private privateLoadCommand As ICommand
		Public Property LoadCommand() As ICommand
			Get
				Return privateLoadCommand
			End Get
			Private Set(ByVal value As ICommand)
				privateLoadCommand = value
			End Set
		End Property
		Private privateSaveCommand As ICommand
		Public Property SaveCommand() As ICommand
			Get
				Return privateSaveCommand
			End Get
			Private Set(ByVal value As ICommand)
				privateSaveCommand = value
			End Set
		End Property

		Private Sub LoadCommandExecute(ByVal parameter As Object)
			Dim proxy As New CarInfoServiceClient()

			AddHandler proxy.LoadCarInfoCompleted, (Function(s, e) AnonymousMethod1(s, e))

			proxy.LoadCarInfoAsync()
		End Sub
		
		Private Function AnonymousMethod1(ByVal s As Object, ByVal e As LoadCarInfoCompletedEventArgs) As Boolean
			model.Name = e.Result.Name
			model.Description = e.Result.Description
			Me.Name = model.Name
			Me.Description = model.Description
			Return True
		End Function

		Private Sub SaveCommandExecute(ByVal parameter As Object)
			model.Name = name_Renamed
			model.Description = description_Renamed

			Dim proxy As New CarInfoServiceClient()

			proxy.SaveCarInfoAsync(New RichEditMVVMScenarioSL.CarInfoServiceReference.CarInfo() With {.Name = model.Name, .Description = model.Description})
		End Sub
	End Class
End Namespace
