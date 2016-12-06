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
    public class ServiceHeaderMessageHeader : MessageHeader
    {

        private string _Name;
        private string _Namespace;
        private string _value;

        public ServiceHeaderMessageHeader(string name, string nameSpace, string value)
        {
            _Name = name;
            _Namespace = nameSpace;
            _value = value;
        }

        public override string Name
        {
            get
            {
                return _Name;
            }
        }

        public override string Namespace
        {
            get
            {
                return _Namespace;
            }
        }

        public string UserId { get; set; }

        public string SystemType { get; set; }

        protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
        {
            //writer.WriteStartElement("UserId", "http://schemas.devpro.com/ServiceHeader");
            writer.WriteValue(_value);
        }
    }
}
