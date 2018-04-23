Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.Configuration

Public Class CarInfoService
	Implements ICarInfoService
	Public Function LoadCarInfo() As CarInfo Implements ICarInfoService.LoadCarInfo
		Dim result As New CarInfo()

		Using connection As New SqlConnection(WebConfigurationManager.ConnectionStrings("CarsDBConnectionString").ConnectionString)
			Dim selectCommand As New SqlCommand("SELECT * FROM Cars WHERE ID = @ID", connection)

			connection.Open()
			selectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = 1

			Dim dataReader As SqlDataReader = selectCommand.ExecuteReader()

			If dataReader.HasRows Then
				dataReader.Read()

				result.Name = dataReader.GetString(dataReader.GetOrdinal("Model"))
				result.Description = dataReader.GetString(dataReader.GetOrdinal("RtfContent"))
			End If
		End Using

		Return result
	End Function

	Public Sub SaveCarInfo(ByVal carInfo As CarInfo) Implements ICarInfoService.SaveCarInfo
		Using connection As New SqlConnection(WebConfigurationManager.ConnectionStrings("CarsDBConnectionString").ConnectionString)
			Dim updateCommand As New SqlCommand("UPDATE Cars SET Model = @Model, RtfContent = @RtfContent WHERE ID = @ID", connection)

			connection.Open()
			updateCommand.Parameters.Add("@ID", SqlDbType.Int).Value = 1
			updateCommand.Parameters.Add("@Model", SqlDbType.NVarChar).Value = carInfo.Name
			updateCommand.Parameters.Add("@RtfContent", SqlDbType.VarChar).Value = carInfo.Description

			updateCommand.ExecuteNonQuery()
		End Using
	End Sub
End Class
