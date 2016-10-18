using ScriptCs.Contracts;
using ScriptCs.Hosting;
using ScriptCs.Engine.Roslyn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCs.Launcher
{
    public class ScriptHost
    {
        private ScriptExecutor Executor { get; set; }

        public void Initialize()
        {
            var console = (IConsole)new ScriptConsole();
            var logProvider = new ColoredConsoleLogProvider(LogLevel.Info, console);

            var builder = new ScriptServicesBuilder(console, logProvider);

            builder.ScriptEngine<RoslynScriptEngine>();
            var services = builder.Build();

            Executor = (ScriptExecutor)services.Executor;
            Executor.Initialize(Enumerable.Empty<string>(), Enumerable.Empty<IScriptPack>());
        }

        public void Execute(string filePath, params string[] args)
        {
            var result = Executor.Execute(filePath, args);
        }
    }
}
