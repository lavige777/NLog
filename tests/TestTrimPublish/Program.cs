﻿using NLog;

var consoleWriter = new StringWriter();
var orgConsole = System.Console.Out;
try
{
    System.Console.SetOut(consoleWriter);

    var logger = NLog.LogManager.Setup().LoadConfigurationFromXml(@"
    <nlog>
      <targets>
        <target type='console' name='console' layout='${threadid}|${message}' />
      </targets>
      <rules>
        <logger name='*' minLevel='Info' writeTo='console' />
      </rules>
    </nlog>
    ").GetCurrentClassLogger();

    logger.Debug("Almost ready");
    logger.Info("Success");
    NLog.LogManager.Shutdown();
    logger.Debug("Almost done");
}
finally
{
    System.Console.SetOut(orgConsole);
}

var result = consoleWriter.ToString();

Console.WriteLine(result);

if (result == $"{Environment.CurrentManagedThreadId}|Success{System.Environment.NewLine}")
    return 0;
else
    return -1;