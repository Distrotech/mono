//
// Mono-specific additions to Microsoft's SslStream.cs
//
namespace System.Net.Security {
    using System.Net.Sockets;

    partial class SslStream
    {
        internal bool IsClosed
        {
            get { return _SslState.IsClosed; }
        }

        internal Exception LastError
        {
            get { return _SslState.LastError; }
        }

        internal IAsyncResult BeginShutdown(bool waitForReply, AsyncCallback asyncCallback, object asyncState)
        {
            return _SslState.BeginShutdown(waitForReply, asyncCallback, asyncState);
        }

        internal void EndShutdown(IAsyncResult asyncResult)
        {
            _SslState.EndShutdown(asyncResult);
        }
    }
}