using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InfinityLoop
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Generate100Threads().GetAwaiter().GetResult();
        }

        private async Task Generate100Threads()
        {
            const int agentNumber = 1000;
            Task[] tasks = new Task[agentNumber];
            var cts = new CancellationTokenSource();
            foreach (var i in Enumerable.Range(0, agentNumber))
            {
                tasks[i] = printOutAsync(i, cts.Token);
            }
            // Wait for an input then cancel the token.
            Console.WriteLine("Press Any key for cancel");
            Console.ReadLine();
            cts.Cancel();
            await Task.WhenAll(tasks);
            Console.WriteLine("All task gracefully finished.");
            Console.ReadLine();

        }

        private async Task printOutAsync(int i, CancellationToken token) {
           while(true)
            {
                if (token.IsCancellationRequested)
                {
                    break;
                }
               Console.WriteLine($"Thread counter-{i} works");
               await Task.Delay(TimeSpan.FromSeconds(3));
            }
            Console.WriteLine($"Thread counter-{i} has been finished.");
        }
    }
}
