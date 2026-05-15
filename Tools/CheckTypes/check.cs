using System;
using System.Reflection;
using System.IO;

class Program {
    static void Main() {
        string path = @"E:\Next\HB_backup\CompiledAssemblies\DefaultRoutine.dll";
        // Since we're likely running as 64-bit, use MetadataReader
        // But the simplest approach: try loading with ReflectionOnly
        try {
            var asm = Assembly.ReflectionOnlyLoadFrom(path);
            var types = asm.GetTypes();
            int simCount = 0;
            int totalCount = types.Length;
            foreach (var t in types) {
                if (t.Name.StartsWith("Sim_")) simCount++;
            }
            Console.WriteLine("Total types: " + totalCount);
            Console.WriteLine("Sim_* types: " + simCount);
        } catch (Exception ex) {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
