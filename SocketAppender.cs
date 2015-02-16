using System;
using System.Text;

using System.Net; 
using System.Net.Sockets; 

using log4net.Core;
using log4net.Appender;

namespace log4net
{
	namespace Appender {

		public class SocketAppender : AppenderSkeleton
		{
			// properties set by log4net xml config file
			public string remoteAddress { get; set;}
			public int    remotePort { get; set;}
			public bool   debugMode { get; set;}

			// this is the socket (you don't say!)
			private Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

			// connect to a remote device
			public override void ActivateOptions() {

				// Establish the remote endpoint for the socket.
				try {
					sender.Connect (remoteAddress, remotePort);
				} catch (ArgumentNullException ane) {
					Console.WriteLine("ArgumentNullException : {0}",ane.ToString());

				} catch (SocketException se) {
					Console.WriteLine("SocketException : {0}",se.ToString());

				} catch (Exception e) {
					Console.WriteLine("Unexpected exception : {0}", e.ToString());
				}

			}

			protected override void Append(LoggingEvent loggingEvent) {
				string rendered = "";

				// check connectivity before send
				if (sender.Connected) {

					// Renderizing event
					rendered = RenderLoggingEvent (loggingEvent);

					// Encode the data string into a byte array.
					byte[] msg = Encoding.UTF8.GetBytes(rendered);

					// Send the data through the socket.
					int bytesSent = sender.Send(msg);

					if (debugMode) {
						Console.WriteLine ("- Bytes sent: " + bytesSent.ToString());
					}
				} else {
					// accumulate?!
					Console.WriteLine ("[SKIPPED]:: " + rendered);
				}
			}

			protected override void OnClose() {
				sender.Shutdown (SocketShutdown.Both);
				sender.Close ();
			}
		}

	}
}