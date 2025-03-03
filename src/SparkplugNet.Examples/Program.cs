﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Hämmer Electronics">
// The project is licensed under the MIT license.
// </copyright>
// <summary>
//   The main program.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SparkplugNet.Examples
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Serilog;

    using SparkplugNet.Core.Application;
    using SparkplugNet.Core.Device;
    using SparkplugNet.Core.Node;

    using VersionAPayload = VersionA.Payload;
    using VersionBPayload = VersionB.Payload;

    /// <summary>
    /// The main program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The cancellation token source.
        /// </summary>
        private static readonly CancellationTokenSource CancellationTokenSource = new ();

        /// <summary>
        /// The version A metrics.
        /// </summary>
        private static readonly List<VersionAPayload.KuraMetric> VersionAMetrics = new ()
        {
            new ()
            {
                Name = "Test", Type = VersionAPayload.KuraMetric.ValueType.Double, DoubleValue = 1.20
            },
            new ()
            {
                Name = "Test2", Type = VersionAPayload.KuraMetric.ValueType.Bool, BoolValue = true
            }
        };

        /// <summary>
        /// The version A metrics.
        /// </summary>
        private static readonly List<VersionBPayload.Metric> VersionBMetrics = new ()
        {
            new VersionBPayload.Metric
            {
                Name = "Test", Datatype = (uint)VersionBPayload.Metric.ValueOneofCase.DoubleValue, DoubleValue = 1.20
            },
            new VersionBPayload.Metric
            {
                Name = "Test2", Datatype = (uint)VersionBPayload.Metric.ValueOneofCase.BooleanValue, BooleanValue = true
            }
        };

        /// <summary>
        /// The main method.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing any asynchronous operation.</returns>
        public static async Task Main()
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.Console()
                    .CreateLogger();

                await RunVersionA();
                // await RunVersionB();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Runs a version A simulation.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing any asynchronous operation.</returns>
        private static async Task RunVersionA()
        {
            Console.WriteLine("Starting application...");
            var applicationMetrics = new List<VersionAPayload.KuraMetric>(VersionAMetrics);
            var application = new VersionA.SparkplugApplication(applicationMetrics, Log.Logger);
            var applicationOptions = new SparkplugApplicationOptions("localhost", 1883, "application1", "user", "password", false, "scada1", TimeSpan.FromSeconds(30), true, null, null, CancellationTokenSource.Token);
            await application.Start(applicationOptions);
            Console.WriteLine("Application started...");

            Console.WriteLine("Starting node...");
            var nodeMetrics = new List<VersionAPayload.KuraMetric>(VersionAMetrics);
            var node = new VersionA.SparkplugNode(nodeMetrics, Log.Logger);
            var nodeOptions = new SparkplugNodeOptions("localhost", 1883, "node 1", "user", "password", false, "scada1", "group1", "node1", TimeSpan.FromSeconds(30), null, null, CancellationTokenSource.Token);
            await node.Start(nodeOptions);
            Console.WriteLine("Node started...");

            Console.WriteLine("Starting device...");
            var deviceMetrics = new List<VersionAPayload.KuraMetric>(VersionAMetrics);
            var device = new VersionA.SparkplugDevice(deviceMetrics, Log.Logger);
            var deviceOptions = new SparkplugDeviceOptions("localhost", 1883, "device 1", "user", "password", false, "scada1", "group1", "node1", "device1", TimeSpan.FromSeconds(30), null, null, CancellationTokenSource.Token);
            await device.Start(deviceOptions);
            Console.WriteLine("Device started...");
        }

        /// <summary>
        /// Runs a version B simulation.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing any asynchronous operation.</returns>
        private static async Task RunVersionB()
        {
            Console.WriteLine("Starting application...");
            var applicationMetrics = new List<VersionBPayload.Metric>(VersionBMetrics);
            var application = new VersionB.SparkplugApplication(applicationMetrics, Log.Logger);
            var applicationOptions = new SparkplugApplicationOptions("localhost", 1883, "application1", "user", "password", false, "scada1", TimeSpan.FromSeconds(30), true, null, null, CancellationTokenSource.Token);
            await application.Start(applicationOptions);
            Console.WriteLine("Application started...");

            Console.WriteLine("Starting node...");
            var nodeMetrics = new List<VersionBPayload.Metric>(VersionBMetrics);
            var node = new VersionB.SparkplugNode(nodeMetrics, Log.Logger);
            var nodeOptions = new SparkplugNodeOptions("localhost", 1883, "node 1", "user", "password", false, "scada1", "group1", "node1", TimeSpan.FromSeconds(30), null, null, CancellationTokenSource.Token);
            await node.Start(nodeOptions);
            Console.WriteLine("Node started...");

            Console.WriteLine("Starting device...");
            var deviceMetrics = new List<VersionBPayload.Metric>(VersionBMetrics);
            var device = new VersionB.SparkplugDevice(deviceMetrics, Log.Logger);
            var deviceOptions = new SparkplugDeviceOptions("localhost", 1883, "device 1", "user", "password", false, "scada1", "group1", "node1", "device1", TimeSpan.FromSeconds(30), null, null, CancellationTokenSource.Token);
            await device.Start(deviceOptions);
            Console.WriteLine("Device started...");
        }
    }
}
