using System.Reflection;
using Matches.Application.Configuration.Commands;

namespace Matches.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase).Assembly;
    }
}