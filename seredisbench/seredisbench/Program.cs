using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication115
{
    class Program
    {
        private static int _requests = 0;

        static void Main(string[] args)
        {
            var host = args[0];
            var password = args[1];
            var pendingRequests = Int32.Parse(args[2]);
            var size = Int32.Parse(args[3]);

            Console.WriteLine("Host:\t\t\t{0}", host);
            Console.WriteLine("PendingRequests:\t{0}", pendingRequests);
            Console.WriteLine("Cache Item Size:\t{0} bytes", size);
            Console.WriteLine();

            var cm = ConnectionMultiplexer.Connect(String.Format("{0}, ssl=false, password={1}", host, password));
            var db = cm.GetDatabase();

            // Set test key
            var value = new byte[size];
            (new Random()).NextBytes(value);
            //db.StringSet("test", value);

            for (int i = 0; i < pendingRequests; i++)
            {
                DoRequest(db);
            }

            Console.WriteLine("Time\t\tRPS");

            var last = DateTime.Now;
            while (true)
            {
                Thread.Sleep(1000);

                var now = DateTime.Now;
                var elapsed = (now - last).TotalSeconds;
                last = now;

                var rps = (int)(Interlocked.Exchange(ref _requests, 0) / elapsed);

                Console.WriteLine($"{now.ToString("hh:mm:ss.fff")}\t{rps}");
            }
        }

        static void DoRequest(IDatabase db)
        {
            db.HashIncrementAsync("test", "test", 1).ContinueWith((v) => {
                Interlocked.Increment(ref _requests);
                DoRequest(db);
            });
        }
    }
}
