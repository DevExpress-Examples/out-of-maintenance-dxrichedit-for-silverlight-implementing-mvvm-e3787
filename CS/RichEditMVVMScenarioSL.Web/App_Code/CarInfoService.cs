using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

public class CarInfoService : ICarInfoService {
    public CarInfo LoadCarInfo() {
        CarInfo result = new CarInfo();

        using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CarsDBConnectionString"].ConnectionString)) {
            SqlCommand selectCommand = new SqlCommand("SELECT * FROM Cars WHERE ID = @ID", connection);
            
            connection.Open();
            selectCommand.Parameters.Add("@ID", SqlDbType.Int).Value = 1;

            SqlDataReader dataReader = selectCommand.ExecuteReader();
            
            if (dataReader.HasRows) {
                dataReader.Read();

                result.Name = dataReader.GetString(dataReader.GetOrdinal("Model"));
                result.Description = dataReader.GetString(dataReader.GetOrdinal("RtfContent"));
            }
        }

        return result;
    }

    public void SaveCarInfo(CarInfo carInfo) {
        using (SqlConnection connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["CarsDBConnectionString"].ConnectionString)) {
            SqlCommand updateCommand = new SqlCommand("UPDATE Cars SET Model = @Model, RtfContent = @RtfContent WHERE ID = @ID", connection);

            connection.Open();
            updateCommand.Parameters.Add("@ID", SqlDbType.Int).Value = 1;
            updateCommand.Parameters.Add("@Model", SqlDbType.NVarChar).Value = carInfo.Name;
            updateCommand.Parameters.Add("@RtfContent", SqlDbType.VarChar).Value = carInfo.Description;

            updateCommand.ExecuteNonQuery();
        }
    }
}
