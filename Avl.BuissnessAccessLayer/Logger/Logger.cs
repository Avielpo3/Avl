using log4net;
using log4net.Appender;
using log4net.Core;
using log4net.Layout;
using log4net.Repository.Hierarchy;
using log4net.Util;
using System.IO;
using System.Text;

namespace Logger
{
    public class Logger
    {
        private static ILog _log;
        
        private const string LogFileName = "log.xml";
        private static readonly string BaseDirectory = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        public static ILog Log
        {
            get
            {
                if (_log == null)
                {
                    Setup();
                    _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); ;
                }

                return _log;
            }
        }

        private Logger()
        {

        }

        private static void Setup()
        {
            LogLog.InternalDebugging = true;

            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();

            PatternString patternString = new PatternString();
            patternString.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternString.ActivateOptions();

            FileAppender fileAppender = new FileAppender
            {
                Name = "XmlAppender",
                AppendToFile = true,
                Layout = new XmlLayoutSchemaLog4j(),
                Encoding = Encoding.UTF8,
                File = BaseDirectory + LogFileName
            };

            fileAppender.ActivateOptions();

            hierarchy.Root.AddAppender(fileAppender);
            hierarchy.Root.Level = Level.Info;
            hierarchy.Configured = true;
        }

    }
}
