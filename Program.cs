using System;
using System.Linq;

namespace dnformat
{
    class Program
    {
        static void Main(string[] args)
        {
            string format;
            string[] helpFlags = {"/?", "--help", "-?"};

            string paramCollector = "";

            //Input retrieval stage

            if (args.Length > 0)
            {
                if (helpFlags.Contains(args[0]))
                {
                    OutputHelp();
                    return;
                }

                format = args[0];
                paramCollector = args[1];
            }
            else
            {
                Console.Write("Enter format string: ");
                format = Console.ReadLine();

                Console.WriteLine("Enter params as type:value (enter blank to finish)");
                Console.WriteLine("E.g. int:50");

                int i = 0;
                string thisContent = "";
                do
                {
                    Console.Write("Param {0}: ", i);
                    thisContent = Console.ReadLine();

                    if (thisContent != "")
                    {
                        paramCollector += thisContent;
                        i++;
                    }
                }
                while (thisContent != "");
            }

            Formatter.FormatOutput(format, paramCollector);

        }

        private static void OutputHelp()
        {
            string usage =
@"Usage:
    dnformat format type1:param1,type2:param2...
Examples:
    dnformat ""hello {0}"" string:world
    dnformat ""{0:P} glue"" float:0.05
    dnformat ""{0:X} {0:X08}"" int:37295";

            Console.WriteLine(usage);
        }

    }
}
