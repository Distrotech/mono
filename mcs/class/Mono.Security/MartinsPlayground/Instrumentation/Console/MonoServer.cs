﻿extern alias NewMonoSource;
extern alias MonoSecurity;

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.Security;
using System.Diagnostics;
using System.Collections.Generic;

using MonoSecurity::Mono.Security.Protocol.NewTls;
using SslProtocols = NewMonoSource::System.Security.Authentication.SslProtocols;
using EncryptionPolicy = NewMonoSource::System.Net.Security.EncryptionPolicy;
using MonoSslStreamFactory = NewMonoSource::Mono.Security.NewMonoSource.MonoSslStreamFactory;
using MonoSslStream = NewMonoSource::Mono.Security.NewMonoSource.MonoSslStream;

using SSCX = MonoSecurity::System.Security.Cryptography.X509Certificates;
using MX = MonoSecurity::Mono.Security.X509;

namespace Mono.Security.Instrumentation.Console
{
	using Framework;

	public class MonoServer : MonoConnection, IServer
	{
		public ServerCertificate Certificate {
			get;
			private set;
		}

		new public IServerParameters Parameters {
			get { return (IServerParameters)base.Parameters; }
		}

		public MonoServer (ServerFactory factory, IPEndPoint endpoint, ServerCertificate pfx, IServerParameters parameters)
			: base (factory, endpoint, parameters)
		{
			Certificate = pfx;
		}

		protected override TlsSettings GetSettings ()
		{
			var settings = new TlsSettings ();
			if (Parameters.RequireClientCertificate)
				settings.RequireClientCertificate = true;
			else if (Parameters.AskForClientCertificate)
				settings.AskForClientCertificate = true;
			settings.RequestedCiphers = Parameters.ServerCiphers;
			return settings;
		}

		protected override MonoSslStream Start (Socket socket, TlsSettings settings)
		{
			var monoParams = Parameters as IMonoServerParameters;
			if (monoParams != null)
				settings.Instrumentation = monoParams.ServerInstrumentation;

			settings.ClientCertValidationCallback = ClientCertValidationCallback;

			var stream = new NetworkStream (socket);
			return MonoSslStreamFactory.CreateServer (
				stream, false, null, null, EncryptionPolicy.RequireEncryption, settings,
				Certificate.Certificate, false, SslProtocols.Tls12, false);
		}

		bool ClientCertValidationCallback (ClientCertificateParameters certParams, MX.X509Certificate certificate, MX.X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			return true;
		}
	}
}