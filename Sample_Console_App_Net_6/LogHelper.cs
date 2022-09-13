using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample_Console_App_Net_6
{
    internal class LogHelper
    {
        private readonly ILogger<LogHelper> _logHelper;

        public LogHelper(ILogger<LogHelper> logHelper)
        {
            _logHelper = logHelper;
        }

        public void LogDebug(string text)
        {
            _logHelper.LogDebug(20, "Sample Console App (Debug) - {Action}", text);
        }

        public void LogInformation(string text)
        {
            _logHelper.LogInformation(20, "Sample Console App (Information) - {Action}", text);
        }
    }
}
