﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SparkplugDevice.cs" company="Hämmer Electronics">
// The project is licensed under the MIT license.
// </copyright>
// <summary>
//   Defines the SparkplugDevice type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SparkplugNet.VersionA
{
    using System.Collections.Generic;

    using Serilog;

    using SparkplugNet.Core.Device;

    /// <inheritdoc cref="SparkplugDeviceBase{T}"/>
    public class SparkplugDevice : SparkplugDeviceBase<Payload.KuraMetric>
    {
        /// <inheritdoc cref="SparkplugDeviceBase{T}"/>
        /// <summary>
        /// Initializes a new instance of the <see cref="SparkplugDevice"/> class.
        /// </summary>
        /// <param name="knownMetrics">The known metrics.</param>
        /// <param name="logger">The logger.</param>
        public SparkplugDevice(List<Payload.KuraMetric> knownMetrics, ILogger? logger = null) : base(knownMetrics, logger)
        {
        }
    }
}