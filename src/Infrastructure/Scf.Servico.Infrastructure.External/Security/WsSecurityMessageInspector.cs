using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace Scf.Servico.Infrastructure.External.Security
{
    public class WsSecurityMessageInspector : IClientMessageInspector
	{
		private readonly string password;
		private readonly string username;

		public WsSecurityMessageInspector(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		object? IClientMessageInspector.BeforeSendRequest(ref Message request, IClientChannel channel)
		{
			var header = new Security
			{
				UsernameToken =
				{
					Password = new Password
					{
						Value = password,
						Type = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordText"
					},
					Username = username
				}
			};

			request.Headers.Add(header);

			return null;
		}

        void IClientMessageInspector.AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (!reply.IsEmpty)
                reply = TransformReply(reply);
        }

        static Message TransformReply(Message reply)
        {
            Message newReply = reply;

            var stringReply = reply.ToString();

            if (!string.IsNullOrEmpty(stringReply) && stringReply.Contains("xsi:type"))
            {
                MessageBuffer copy = reply.CreateBufferedCopy(int.MaxValue);
                XmlDictionaryReader xdr = copy.CreateMessage().GetReaderAtBodyContents();
                XmlDocument doc = new();

                doc.Load(xdr);
                xdr.Close();

                doc.InnerXml = Regex.Replace(doc.InnerXml, @"\s+xsi:type=""\w+""", "");

                MemoryStream ms = new();
                XmlWriter xw = XmlWriter.Create(ms);

                doc.Save(xw);
                xw.Flush();
                xw.Close();
                ms.Position = 0;

                XmlReader reader = XmlReader.Create(ms);

                newReply = Message.CreateMessage(reply.Version, reply.Headers.Action, reader);
            }

            return newReply;
        }
    }

	public class Password
	{
		[XmlAttribute]
		public string Type { get; set; } = string.Empty;

		[XmlText] 
		public string Value { get; set; } = string.Empty;
	}

	[XmlRoot(Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
	public class UsernameToken
	{
		[XmlElement] 
		public string Username { get; set; } = string.Empty;

		[XmlElement]
		public Password Password { get; set; } 
	}

	public class Security : MessageHeader
	{
		public Security() =>
			UsernameToken = new UsernameToken();

		public UsernameToken UsernameToken { get; set; }

		public override string Name =>
			GetType().Name;

		public override string Namespace =>
			"http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

		public override bool MustUnderstand =>
			true;

		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			var serializer = new XmlSerializer(typeof(UsernameToken));
			serializer.Serialize(writer, UsernameToken);
		}
	}
}
