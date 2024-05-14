using Serilog.Events;
using Serilog.Formatting.Compact;
using Serilog;

namespace SerilLogTest.Infrastructures.Logging
{
    public static class SerilLogHelper
    {
        public static void ConfigureSerilLogger(IConfiguration configuration)
        {
            // 全域設定
            /*  🔔new CompactJsonFormatter()
             *  由於 Log 的欄位很多，使用 Console Sink 會比較看不出來，改用 Serilog.Formatting.Compact 來記錄 JSON 格式的 Log 訊息會清楚很多！
             */
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information() // 設定最小Log輸出
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information) // 設定 Microsoft.AspNetCore 訊息為 Warning 為最小輸出
                .Enrich.FromLogContext()  // 可以增加Log輸出欄位 https://www.cnblogs.com/wd4j/p/15043489.html
                .Enrich.With<EventTypeEnricher>()
                .Enrich.WithProperty("cat", "123")

                //.WriteTo.Console(new CompactJsonFormatter()) // 寫入Console
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{EventType:x8} {Level:u3}] {Message:lj}貓咪ErrLevel:{貓咪ErrLevel}{NewLine}{Exception} ")
                //.WriteTo.Console()
                //.WriteTo.Seq("http://localhost:5341")

                .WriteTo.Map(   // 寫入txt => 按照 level
                evt => evt.Level,
                (level, wt) => wt.File(
                    new CompactJsonFormatter(),
                    path: String.Format(configuration.GetValue<string>("Path:SerilLogSavePath"), level),
                    restrictedToMinimumLevel: LogEventLevel.Information,
                    rollOnFileSizeLimit: true,
                    rollingInterval: RollingInterval.Day))
                 .CreateLogger();
        }

        public static async void EnrichMethod(IDiagnosticContext diagnosticContext, HttpContext httpContext)
        {
            var request = httpContext.Request;
            var endpoint = httpContext.GetEndpoint();
            

            diagnosticContext.Set("cathi", "貓咪說嗨");
        }
    }
}
