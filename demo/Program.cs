using System;
using log4net;
using log4net.Ext;

namespace demo
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			ILog log = LogManager.GetLogger (System.Reflection.MethodBase.GetCurrentMethod().Name);
			int count = 0;
			int debugged = 0;

			for (int i=0; i <= 1000000; i++) {

				log.Info ("InfoStub #" + i.ToString());
				count++;

				if (i % 3 == 0) {
					log.Debug ("DebugStub #" + i.ToString());
					count++;
					debugged++;
				}

				if (i % 5 == 0) {
					log.Warn ("WarnStub #" + i.ToString());
					count++;
				}

				if (i % 7 == 0) {
					log.Trace ("TraceStub #" + i.ToString());
					count++;
				}

				if (i % 35 == 0) {
					log.Fatal ("FatalStub #" + i.ToString());
					count++;
				}

				if (i % 42 == 0) {
					try {
						throw new Exception(".Net exception " + i.ToString());

					} catch (Exception e) {
						log.Error ("ErrorStub #" + i.ToString(), e);
						count++;
					}

				}
			}

			System.Console.WriteLine ("- Total logged messages : " + count.ToString());
			System.Console.WriteLine ("- Total debug messages  : " + debugged.ToString());
			System.Console.WriteLine ("- Messages to be persisted: " + (count -debugged).ToString() );
		}
	}
}