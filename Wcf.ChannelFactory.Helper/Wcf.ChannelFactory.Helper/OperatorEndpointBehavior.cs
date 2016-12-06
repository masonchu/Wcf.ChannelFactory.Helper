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
    public class OperatorEndpointBehavior : IEndpointBehavior
    {
        private OperatorMessageInspector _OperatorMessageInspector;

        public OperatorMessageInspector OperatorMessageInspector
        {
            get
            {
                if (_OperatorMessageInspector == null)
                {
                    _OperatorMessageInspector = new OperatorMessageInspector();
                }
                return _OperatorMessageInspector;
            }
        }
        public void AddBindingParameters(ServiceEndpoint endpoint, System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
        }
        public void ApplyClientBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(OperatorMessageInspector);
        }
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
        }
        public void Validate(ServiceEndpoint endpoint)
        {
        }
    }
    public class OperatorMessageInspector : IClientMessageInspector
    {
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {

        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            return null;
        }
    }
}
