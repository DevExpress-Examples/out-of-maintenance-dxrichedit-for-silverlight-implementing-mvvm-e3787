using System.Runtime.Serialization;
using System.ServiceModel;

[ServiceContract]
public interface ICarInfoService {
    [OperationContract]
    CarInfo LoadCarInfo();
    [OperationContract]
    void SaveCarInfo(CarInfo carInfo);
}

[DataContract]
public class CarInfo {
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string Description { get; set; }
}
