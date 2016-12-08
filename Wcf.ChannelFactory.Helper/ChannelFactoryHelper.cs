using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;

namespace Wcf.ChannelFactory.Helper
{
    public class ChannelFactoryHelper
    {
        /// <summary>
        /// get channnel factory instance by connect mode , http , nettcp
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpointName"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public ChannelFactory<T> GetChannelFactory<T>(string endpointName, ConnectMode mode = ConnectMode.Http)
        {
            ChannelFactory<T> factory = null;
            switch (mode)
            {
                case ConnectMode.Http:
                    factory = GetHttpBindingFactory<T>(endpointName);
                    break;
                case ConnectMode.NetTcp:
                    factory = GetTcpBindingFactory<T>(endpointName);
                    break;
                default:
                    break;
            }
            return GetHttpBindingFactory<T>(endpointName);
        }

        private ChannelFactory<T> GetHttpBindingFactory<T>(string endpointName)
        {
            var endpoint = ReadEndPoint(endpointName);
            BasicHttpBinding binding = new BasicHttpBinding(endpoint.BindingName);
            EndpointAddress address = new EndpointAddress(endpoint.AddressName);

            ChannelFactory<T> factory = new ChannelFactory<T>(binding, address);
            factory.Endpoint.Behaviors.Add(new OperatorEndpointBehavior());

            return factory;
        }

        private ChannelFactory<T> GetTcpBindingFactory<T>(string endpointName)
        {
            var endpoint = ReadEndPoint(endpointName);
            NetTcpBinding binding = new NetTcpBinding(endpoint.BindingName);
            EndpointAddress address = new EndpointAddress(endpoint.AddressName);
            //endPoint.Behaviors.Add)


            ChannelFactory<T> factory = new ChannelFactory<T>(binding, address);
            factory.Endpoint.Behaviors.Add(new OperatorEndpointBehavior());
            return factory;
        }

        private EndPointInfo ReadEndPoint(string name)
        {
            ClientSection clientSection = (ClientSection)ConfigurationManager.GetSection("system.serviceModel/client");
            string address = string.Empty;
            string bindingName = string.Empty;
            for (int i = 0; i < clientSection.Endpoints.Count; i++)
            {
                if (clientSection.Endpoints[i].Name == name)
                {
                    bindingName = clientSection.Endpoints[i].BindingConfiguration.ToString();
                    address = clientSection.Endpoints[i].Address.ToString();
                }
            }
            return new EndPointInfo
            {
                AddressName = address,
                BindingName = bindingName
            };
        }

        private class EndPointInfo
        {
            public string BindingName { get; set; }
            public string AddressName { get; set; }
        }
    }

}
