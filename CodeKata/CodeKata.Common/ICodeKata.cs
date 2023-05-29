namespace CodeKata.Common
{
    public interface ICodeKata
    {
        int Index { get; }
        string Name { get; }
        void Execute();
    }
}