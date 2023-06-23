using Prometheus;

namespace prometheus_dotnetcore_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupMetricServer();

            var histogram = Metrics.CreateHistogram("prom_histogram", "This fields indicates the histogram count.",
                                new HistogramConfiguration
                                {
                                    Buckets = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 },
                                }
                            );

            var random = new Random();

            while (true)
            {
                Thread.Sleep(500);
                histogram.Observe(random.NextDouble() * 5.0);
            }
        }

        private static void SetupMetricServer()
        {
            var metricServer = new MetricServer(port: 9184);
            metricServer.Start();
        }
    }
}