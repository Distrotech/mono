﻿extern alias MonoSecurity;
using MonoSecurity::Mono.Security.Protocol.NewTls;
using MonoSecurity::Mono.Security.Protocol.NewTls.Instrumentation;

namespace Mono.Security.Instrumentation.Framework
{
	using Framework;

	public interface IMonoClientParameters : IClientParameters
	{
		ClientCertificateParameters ClientCertificateParameters {
			get; set;
		}

		InstrumentCollection ClientInstrumentation {
			get;
		}
	}
}
