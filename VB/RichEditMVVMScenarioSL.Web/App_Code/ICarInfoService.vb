Imports Microsoft.VisualBasic
Imports System.Runtime.Serialization
Imports System.ServiceModel

<ServiceContract> _
Public Interface ICarInfoService
	<OperationContract> _
	Function LoadCarInfo() As CarInfo
	<OperationContract> _
	Sub SaveCarInfo(ByVal carInfo As CarInfo)
End Interface

<DataContract> _
Public Class CarInfo
	Private privateName As String
	<DataMember> _
	Public Property Name() As String
		Get
			Return privateName
		End Get
		Set(ByVal value As String)
			privateName = value
		End Set
	End Property
	Private privateDescription As String
	<DataMember> _
	Public Property Description() As String
		Get
			Return privateDescription
		End Get
		Set(ByVal value As String)
			privateDescription = value
		End Set
	End Property
End Class
