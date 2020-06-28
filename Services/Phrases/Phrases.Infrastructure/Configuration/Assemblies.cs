using System.Reflection;
using Phrases.Application.Configuration.Commands;

namespace Phrases.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase).Assembly;
    }
}