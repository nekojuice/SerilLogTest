﻿using Murmur;
using Serilog.Core;
using Serilog.Events;
using System.Text;

namespace SerilLogTest.Infrastructures.Logging;

public class EventTypeEnricher : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        //var murmur = MurmurHash.Create32();
        //var bytes = Encoding.UTF8.GetBytes(logEvent.MessageTemplate.Text);
        //var hash = murmur.ComputeHash(bytes);
        //var numericHash = BitConverter.ToUInt32(hash, 0);
        //var eventId = propertyFactory.CreateProperty("EventType", numericHash);
        //logEvent.AddPropertyIfAbsent(eventId);

        var level = propertyFactory.CreateProperty("貓咪ErrLevel", logEvent.Level);
        logEvent.AddPropertyIfAbsent(level);
    }
}
