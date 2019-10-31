using System;

namespace CSharpProjectNullabler
{
    class Program
    {
        static void Main(string[] args)
        {
            ICommand cmd = Parse(args);
            cmd.Run(args);
        }
        static ICommand Parse(string[] args)
        {
            if (0 < args.Length && "-r" != args[0]) { return new ShowHelpCommand(); }
            else { return new SetupNullableCommand(); }
        }
    }
}
