using System.Diagnostics;
using System.Reflection;
using CodeKata.Common;

Console.WriteLine("Welcome to The Experimental Society of Codecrafting and Testability\n");

var katas = GetKatas();

while (true)
{
    Console.WriteLine("Please pick a Kata by id to execute:");
    foreach (var (key, kataInfo) in katas.OrderBy(x => x.Key))
    {
        Console.WriteLine($"{key}: {kataInfo.KataName}");
    }
    var input = Console.ReadLine();
    if(int.TryParse(input, out int kataChoice) && katas.TryGetValue(kataChoice, out (string kataName, object kataInstance, MethodInfo executeMethod) kata))
    {
        ExecuteKata(kata);
    }
    else
    {
        Console.WriteLine("Wrong input or no kata with that key. Try again. Input the number of the Kata in the list.\n");
    }
}

Dictionary<int, (string KataName, object KataInstance, MethodInfo ExecuteMethodInfo)> GetKatas()
{
    var katas = new Dictionary<int, (string KataName, object KataInstance, MethodInfo ExecuteMethodInfo)>();

    var assemblies = new List<Assembly>();
    foreach (string dll in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll"))
    {
        assemblies.Add(Assembly.LoadFile(dll));
    }

    foreach (var mytype in assemblies.SelectMany(a => a.GetTypes()).Where(types => types.GetInterfaces().Contains(typeof(ICodeKata))))
    {
        object? obj = Activator.CreateInstance(mytype);
        var idxMethod = mytype.GetMethod("get_Index");
        var nameMethod = mytype.GetMethod("get_Name");
        var executeMethod = mytype.GetMethod("Execute")!;
        var idx = (int)(idxMethod!.Invoke(obj, null))!;
        var name = (string)nameMethod!.Invoke(obj, null)!;
        katas.Add(idx, (name, obj, executeMethod)!);
    }

    return katas;
}

void ExecuteKata((string kataName, object kataInstance, MethodInfo executeMethod) kata)
{
    Console.WriteLine("\n\n======================================================\n\n");
    Stopwatch sw = Stopwatch.StartNew();
    kata.executeMethod.Invoke(kata.kataInstance, null);
    sw.Stop();
    Console.WriteLine("\n\n======================================================");
    Console.WriteLine($"Kata {kata.kataName} executed in {sw.ElapsedMilliseconds}ms.\n");
}
