namespace MassTargetFramework
{
    internal class SolutionProject
    {
        internal SolutionProject(string name, string path)
        {
            Name = name;
            Path = path;
        }

        internal string Name { get; }
        internal string Path { get; }
    }
}