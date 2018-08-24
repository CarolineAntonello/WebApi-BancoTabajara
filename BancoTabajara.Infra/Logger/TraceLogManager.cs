using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoTabajara.Infra.Logger
{
    public class TraceLogManager
    {
        private static bool _hasBeenConfigurated = false;

        /// Flag que indica se o log4net já foi configurado.
        public static bool HasBeenConfigurated
        {
            get { return _hasBeenConfigurated; }
        }

        /// Retorna o logger (configurado no app.config ou web.config)
        public static ILog CurrentClassLogger
        {
            get
            {
                if (!HasBeenConfigurated)
                    Configure();
                return LogManager.GetLogger
                    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            }
        }

        /// Configura o log4net. (de acordo com configurações do app.config ou web.config)
        public static void Configure()
        {
            log4net.Config.XmlConfigurator.Configure();
            _hasBeenConfigurated = true;
        }

        #region Debug

        /// Loga a mensagem passada. Nível: Debug
        public static void Debug(Func<string> messageFunc)
        {
            CurrentClassLogger.Debug(messageFunc);
        }

        /// Loga a mensagem passada. Nível: Debug
        public static void Debug(string message)
        {
            CurrentClassLogger.Debug(message);
        }

        /// Loga a mensagem e exceção passada. Nível: Debug
        public static void Fatal(string message, Exception exception)
        {
            CurrentClassLogger.Debug(message, exception);
        }

        /// Loga a mensagem passada e formata com os parametros passados (string.format). Nível: Debug
        public static void Debug(string message, params object[] args)
        {
            CurrentClassLogger.DebugFormat(message, args);
        }

        #endregion

        #region Error

        /// Loga a mensagem passada. Nível: Error
        public static void Error(Func<string> messageFunc)
        {
            CurrentClassLogger.Error(messageFunc);
        }

        /// Loga a mensagem passada. Nível: Error
        public static void Error(string message)
        {
            CurrentClassLogger.Error(message);
        }

        /// Loga a mensagem e exceção passada. Nível: Error
        public static void ErrorException(string message, Exception exception)
        {
            CurrentClassLogger.Error(message, exception);
        }

        /// Loga a mensagem passada e formata com os parametros passados (string.format). Nível: Error
        public static void Error(string message, params object[] args)
        {
            CurrentClassLogger.ErrorFormat(message, args);
        }

        #endregion Error

        #region Fatal

        /// Loga a mensagem passada. Nível: Fatal
        public static void Fatal(Func<string> messageFunc)
        {
            CurrentClassLogger.Fatal(messageFunc);
        }

        /// Loga a mensagem passada. Nível: Fatal
        public static void Fatal(string message)
        {
            CurrentClassLogger.Fatal(message);
        }

        /// Loga a mensagem e exceção passada. Nível: Fatal
        public static void FatalException(string message, Exception exception)
        {
            CurrentClassLogger.Fatal(message, exception);
        }

        /// Loga a mensagem passada e formata com os parametros passados (string.format). Nível: Fatal
        public static void Fatal(string message, params object[] args)
        {
            CurrentClassLogger.FatalFormat(message, args);
        }

        #endregion Fatal

        #region Info

        /// Loga a mensagem passada. Nível: Info
        public static void Info(string message)
        {
            CurrentClassLogger.Info(message);
        }

        /// Loga a mensagem passada. Nível: Info
        public static void Info(Func<string> messageFunc)
        {
            CurrentClassLogger.Info(messageFunc);
        }

        /// Loga a mensagem e exceção passada. Nível: Info
        public static void InfoException(string message, Exception exception)
        {
            CurrentClassLogger.Info(message, exception);
        }

        /// Loga a mensagem passada e formata com os parametros passados (string.format). Nível: Fatal
        public static void Info(string message, params object[] args)
        {
            CurrentClassLogger.InfoFormat(message, args);
        }

        #endregion Info

        #region Trace

        /// Loga a mensagem passada. Nível: Info
        public static void Trace(Func<string> messageFunc)
        {
            CurrentClassLogger.Info(messageFunc);
        }

        /// Loga a mensagem passada. Nível: Trace
        public static void Trace(string message)
        {
            CurrentClassLogger.Info(message);
        }

        /// Loga a mensagem e exceção passada. Nível: Trace
        public static void TraceException(string message, Exception exception)
        {
            CurrentClassLogger.Info(message, exception);
        }

        /// Loga a mensagem passada e formata com os parametros passados (string.format). Nível: Trace
        public static void Trace(string message, params object[] args)
        {
            CurrentClassLogger.Info(string.Format(message, args));
        }

        #endregion Trace

        #region Warn

        /// Loga a mensagem passada. Nivel: Warn.
        public static void Warn(Func<string> messageFunc)
        {
            CurrentClassLogger.Warn(messageFunc);
        }

        /// Loga a mensagem passada. Nível: Warn
        public static void Warn(string message)
        {
            CurrentClassLogger.Warn(message);
        }

        /// Loga a mensagem e exceção passada. Nível: Warn
        public static void WarnException(string message, Exception exception)
        {
            CurrentClassLogger.Warn(message, exception);
        }

        /// Loga a mensagem passada e formata com os parametros passados (string.format). Nível: Warn
        public static void Warn(string message, params object[] args)
        {
            CurrentClassLogger.WarnFormat(message, args);
        }

        #endregion Warn

    }
}
