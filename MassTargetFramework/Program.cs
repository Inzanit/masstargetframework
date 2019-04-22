using System;

namespace MassTargetFramework
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                var migrator = new Migrator();

                migrator.Run(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }

            return 0;
        }
    }
}
