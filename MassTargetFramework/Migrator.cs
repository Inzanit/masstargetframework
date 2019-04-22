using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NETWeasel.Common;

namespace MassTargetFramework
{
    internal class Migrator
    {
        private string _solutionPath;
        private string _rawExcludedProjects;
        private string _targetFramework;
        private string[] _excludedProjects = new string[0];

        internal void Run(IEnumerable<string> args)
        {
            var options = new OptionSet
            {
                {"sln=", "Path to the artifacts/bin directory to package application with", param => _solutionPath = param },
                {"target=", "Name of the target framework to migrate to", param => _targetFramework = param },
                {"exclude=", "Space separated list of names of projects to exclude", param => _rawExcludedProjects = param },
            };

            options.Parse(args);

            if (!File.Exists(_solutionPath))
            {
                throw new InvalidOperationException("Cannot migrate with non existent sln file, ensure path is correct");
            }

            if (!string.IsNullOrWhiteSpace(_rawExcludedProjects))
            {
                _excludedProjects = _rawExcludedProjects.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            }

            var workingDir = Path.GetDirectoryName(_solutionPath);

            var rawSolution = File.ReadAllText(_solutionPath);

            var projectsInSolution = SolutionDeserializer.GetProjects(rawSolution);

            var projectsToMigrate = projectsInSolution
                .Where(x => !_excludedProjects.Contains(x.Name))
                .ToList();

            Console.WriteLine($"Found {projectsToMigrate.Count} projects to migrate...");

            foreach (var projectToMigrate in projectsToMigrate)
            {
                Console.WriteLine($"Opening {projectToMigrate.Name} to migrate");

                var projectPath = Path.Combine(workingDir, projectToMigrate.Path);

                var csProj = XDocument.Load(projectPath);

                var frameworkDescendants = csProj.Descendants()
                    .Where(x => x.Name.LocalName == "TargetFrameworkVersion")
                    .ToList();

                if (!frameworkDescendants.Any())
                {
                    Console.WriteLine($"Failed finding any nodes for Target Framework Version in {projectToMigrate.Name} - is this a .NET Framework project?");
                    continue;
                }

                foreach (var frameworkDescendant in frameworkDescendants)
                {
                    frameworkDescendant.Value = _targetFramework;
                }

                using (var fileStream = new FileStream(projectPath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    csProj.Save(fileStream);
                }

                Console.WriteLine($"Migrated {projectToMigrate.Name}");
            }
        }
    }
}