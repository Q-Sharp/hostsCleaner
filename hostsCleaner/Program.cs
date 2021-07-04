using System;
using System.Linq;
using System.IO;

namespace hostsCleaner
{
    public class Program
    {
        //public record HostEntry(string ip, string[] hostsNames)
        //{

        //}

        public static void Main(string[] args)
        {
            Console.WriteLine($"Hosts Cleaner Tool");
            Console.WriteLine($"path to hostfile: ");

            try
            {
                var p = Path.GetFullPath(Console.ReadLine());
                var h = File.ReadAllText(p);

                Console.WriteLine($"file loaded. Reading entries....");
                Console.WriteLine(Environment.NewLine);

                var ha = h.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                var newHa = ha.Distinct();

                var initCount = ha.Length;
                var endCount = newHa.Count();


                if (initCount > endCount)
                {
                    Console.WriteLine($"{initCount - endCount} can be cleaned.");
                    Console.WriteLine($"Do you wanna save the cleaned list? (Y/N)");

                    while (true)
                    {
                        var r = Console.ReadKey(true);

                        if (r.Key == ConsoleKey.Y)
                        {
                            var newh = string.Join(Environment.NewLine, newHa);
                            File.WriteAllText(p, newh);

                            Console.WriteLine(Environment.NewLine);
                            Console.WriteLine("File saved!");
                            break;
                        }

                        if (r.Key == ConsoleKey.N)
                            break;
                    }
                }
                else
                {
                    Console.WriteLine($"nothing to cleanup");
                }
            }
            catch
            {
                Console.WriteLine("Can't find/open/read/write file!");
            }

            Console.ReadKey(true);
        }
    }
}
