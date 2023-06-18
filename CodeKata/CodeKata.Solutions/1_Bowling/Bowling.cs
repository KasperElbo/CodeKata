using CodeKata.Common;

namespace CodeKata.Solutions._1_Bowling
{
    public class Bowling : ICodeKata
    {
        public int Index => 1;
        public string Name => "Bowling Game";

        public void Execute()
        {
            Console.WriteLine($"{Name} Called");
        }
    }
}