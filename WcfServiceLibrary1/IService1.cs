using System;
using System.IO;
using System.ServiceModel;

namespace WcfServiceLibrary1
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string SendData(byte[] data);
    }
}
