using System.Reflection;
using UserAccess.Application.Configuration.Commands;

namespace UserAccess.Infrastructure.Configuration
{
    internal static class Assemblies
    {
        public static readonly Assembly Application = typeof(InternalCommandBase).Assembly;
    }
}