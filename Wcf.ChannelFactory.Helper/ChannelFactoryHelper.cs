using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Xml;
using System;

namespace Wcf.ChannelFactory.Helper
{
    public class ChannelFactoryHelper
    {
        public ChannelFactory<T> GetOverSeaFactory<T>(string bindingName)
        {
            return GetTcpBindingFactory<T>(bindingName);
            //return GetHttpBindingFactory<IOverSeaService>("BasicHttpBinding_Sinopac", "BasicHttpBinding_OverSeaService");
        }

        public ChannelFactory<T> GetHttpBindingFactory<T>(string endpointName)
        {
            var endpoint = ReadEndPoint(endpointName);
            BasicHttpBinding binding = new BasicHttpBinding(endpoint.BindingName);
            EndpointAddress address = new EndpointAddress(endpoint.AddressName);

            ChannelFactory<T> factory = new ChannelFactory<T>(binding, address);
            factory.Endpoint.Behaviors.Add(new OperatorEndpointBehavior());

            return factory;
        }

        public ChannelFactory<T> GetTcpBindingFactory<T>(string endpointName)
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
                    address = clientSection.Endpoints[i].Address.ToString();
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
