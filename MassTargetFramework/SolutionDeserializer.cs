using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MassTargetFramework
{
    internal static class SolutionDeserializer
    {
        private const string PROJECT_PATTERN =
            "^Project\\(\"{(?<TypeId>[A-F0-9-]+)}\"\\) = " +
            "\"(?<Name>.*?)\", " +
            "\"(?<Path>.*?)\", " +
            "\"{(?<Id>[A-F0-9-]+)}\"";

        internal static IEnumerable<SolutionProject> GetProjects(string rawSolution)
        {
            var matches = Regex.Matches(rawSolution, PROJECT_PATTERN, RegexOptions.Multiline);

            return matches
                .Select(x => new SolutionProject(x.Groups[2].Value, x.Groups[3].Value))
                .ToList();
        }
    }
}