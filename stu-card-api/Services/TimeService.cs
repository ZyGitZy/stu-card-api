using stu_card_api.interfaces;

namespace stu_card_api.Services
{
    public class TimeService : BackgroundService
    {
        Timer timer;
        ILogger<TimeService> logger;
        IServiceProvider Services;
        int count = 0;
        public TimeService(ILogger<TimeService> logger, IServiceProvider services)
        {
            this.logger = logger;
            this.Services = services;
        }
        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("定时任务停止");
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("定时任务开始");

            var scope = Services.CreateScope();
            var scopedProcessingService =
                scope.ServiceProvider
                    .GetRequiredService<IDataAcquisitionService>();

            timer = new Timer(async _ =>
            {
                var result = await scopedProcessingService.PersionCollection();
                if (count > 10)
                {
                    //timer.Change(Timeout.Infinite, 0);
                    //await Task.Delay(1000 * 60 * 60 * 60);
                    //timer.Change(0, 1000 * 60);
                }
                count++;
                await Console.Out.WriteLineAsync("11111111111111111111111111111111");

            }, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            await Task.FromResult(0);
        }
    }
}
