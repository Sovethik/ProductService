using Microsoft.EntityFrameworkCore;
using Serilog.Core;
using Serilog.Events;
using System.Data.Common;

namespace ProductService.Infrastructure.Logging.Enricher.Error
{
    public class DatabaseErrorEnricher : ILogEventEnricher
    {
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var ex = logEvent.Exception;
            if (ex is DbException or DbUpdateException)
            {
                logEvent.AddPropertyIfAbsent(
                    propertyFactory.CreateProperty("ErrorType", ex.GetType().Name));
            }
        }
    }
}
