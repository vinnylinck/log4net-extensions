using System;

namespace log4net {
	namespace Ext {

		public static class ILogExtentions 
		{
			//private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

			public static void Trace(this ILog log, string message, Exception exception)
			{
				log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType,log4net.Core.Level.Trace, message, exception);
			}

			public static void Trace(this ILog log, string message)
			{
				log.Trace(message, null);
			}
		}
	}
}