using System;

namespace testapp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"xuhui: {FnvHash.Hash("xuhui")}");
            Console.WriteLine($"xuhui: {FnvHash.Hash("xuhui")}");
            Console.WriteLine($"minhang: {FnvHash.Hash("minhang")}");
            Console.WriteLine($"aa: {FnvHash.Hash("aa")}");
            Console.WriteLine($"bb: {FnvHash.Hash("bb")}");
            Console.WriteLine($"aa: {FnvHash.Hash("aa")}");
            Console.WriteLine($"bb: {FnvHash.Hash("bb")}");
            Console.ReadLine();
        }
    }
}
