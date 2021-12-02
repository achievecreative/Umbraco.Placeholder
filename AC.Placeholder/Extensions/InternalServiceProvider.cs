using System;
using Umbraco.Cms.Web.Common.DependencyInjection;

namespace AC.Placeholder.Extensions
{
    internal static class InternalServiceProvider
    {
        public static IServiceProvider Instance { get; internal set; }
    }
}
