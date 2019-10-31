using System;

namespace CSharpProjectNullabler
{
    class ShowHelpCommand : ICommand
    {
        public void Run(string[] args)
        {
            Console.WriteLine(@".csprojファイルに<Nullable>enable</Nullable>を設定する。<Nullable>が既存なら何もしない。引数-rで再帰的に処理する。");
        }
    }
}
