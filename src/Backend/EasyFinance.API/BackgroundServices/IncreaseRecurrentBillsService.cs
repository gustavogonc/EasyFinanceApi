using EasyFinance.Application.UseCases.Expense.RegisterRecurrent;

namespace EasyFinance.API.BackgroundServices
{
    public class IncreaseRecurrentBillsService(IServiceProvider provider) : BackgroundService
    {
        private int TIME_INTERVAL = 1000 * 60 * 60 * 12;
        private DateTime _lastRunTime = DateTime.MinValue;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (DateTime.Now - _lastRunTime < TimeSpan.FromMilliseconds(TIME_INTERVAL))
                    {
                        await Task.Delay(TIME_INTERVAL);
                    }

                    var scope = provider.CreateScope();
                    var registerRecurrentExpense = scope.ServiceProvider.GetRequiredService<IRegisterRecurrentExpenseUseCase>();

                    await registerRecurrentExpense.Execute();

                    _lastRunTime = DateTime.Now;
                }
                catch (Exception)
                {
                    Environment.ExitCode = 1;
                    await Task.Delay(TIME_INTERVAL);
                }
            }
        }
    }
}
