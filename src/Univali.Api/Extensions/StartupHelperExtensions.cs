using Microsoft.EntityFrameworkCore;
using Univali.Api.DbContexts;

namespace Univali.Api.Extensions;

internal static class StartupHelperExtensions
{
   public static async Task ResetDatabaseAsync(this WebApplication app)
   {
       using (var scope = app.Services.CreateScope())
       {
           try
           {
               var CustomerContext = scope.ServiceProvider.GetService<CustomerContext>();
               if (CustomerContext != null)
               {
                   await CustomerContext.Database.EnsureDeletedAsync();
                   await CustomerContext.Database.MigrateAsync();
               }

               var PublisherContext = scope.ServiceProvider.GetService<PublisherContext>();
               if(PublisherContext != null) {
                   await PublisherContext.Database.EnsureDeletedAsync();
                   await PublisherContext.Database.MigrateAsync();
               }
           }
           catch (Exception ex)
           {
               var logger = scope.ServiceProvider.GetRequiredService<ILogger<IStartup>>();
               logger.LogError(ex, "An error occurred while migrating the database.");
           }
       }
   }
}
