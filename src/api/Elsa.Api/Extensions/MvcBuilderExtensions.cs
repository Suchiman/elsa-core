using Elsa.Api.Endpoints.ActivityDescriptors;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace Elsa.Api.Extensions;

public static class MvcBuilderExtensions
{
    /// <summary>
    /// Add all API controllers from this assembly.
    /// </summary>
    public static IMvcBuilder AddElsaApiControllers(this IMvcBuilder mvcBuilder)
    {
        return mvcBuilder.ConfigureApplicationPartManager(partManager =>
        {
            // Add controllers from Elsa.Api:
            partManager.ApplicationParts.Add(new AssemblyPart(typeof(List).Assembly));
        });
    }
}