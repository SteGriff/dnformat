using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                    string usage =
@"Usage:
    dnformat format type1:param1,type2:param2...
Examples:
    dnformat ""hello {0}"" string:world
    dnformat ""{0:P} glue"" float:0.05
    dnformat ""{0:X} {0:X08}"" int:37295";

                    Console.WriteLine(usage);
                    return;
                }

                format = args[0];
                paramCollector = args[1];
            }
            else
            {
                Console.Write("Enter format string: ");
                format = Console.ReadLine();

                Console.WriteLine("Enter params with type (enter blank to finish)");
                Console.WriteLine("E.g. int 50");

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
                } while (thisContent != "");
            }

            //Format stage

            IList<object> paramObjects = new List<object>();

            string[] typesAndParams = paramCollector.Split(':', ',');

            int j = 0;
            Type lastContentType = typeof(object);

            foreach(string tp in typesAndParams)
            {
                
                //If even (0, 2...) then its a type
                if (j % 2 == 0)
                {
                    //Get the type
                    try
                    {
                        string typeName = realTypeNameOf(tp);
                        lastContentType = Type.GetType(typeName);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Not a type: {0}", tp);
                    }
                }
                else
                {
                    //Do the cast
                    try
                    {
                        var thing = Convert.ChangeType(tp, lastContentType);
                        paramObjects.Add(thing);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Can't format {0} as {1}", tp, lastContentType);
                    }
                }

                j++;
            }

            try
            {
                string formatted = string.Format(format, paramObjects.ToArray());
                Console.WriteLine(formatted);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("{0}: {1}", ex.GetType().ToString(), ex.Message));
            }
        }

        static string realTypeNameOf(string typeName)
        {
            switch (typeName)
            {
                case "object":
                    return "System.Object";

                case "string":
                    return "System.String";

                case "bool":
                    return "System.Boolean";

                case "byte":
                    return "System.Byte";

                case "sbyte":
                    return "System.SByte";

                case "short":
                    return "System.Int16";

                case "ushort":
                    return "System.UInt16";

                case "int":
                    return "System.Int32";

                case "uint":
                    return "System.UInt32";

                case "long":
                    return "System.Int64";

                case "ulong":
                    return "System.UInt64";

                case "float":
                    return "System.Single";

                case "double":
                    return "System.Double";

                case "decimal":
                    return "System.Decimal";

                case "char":
                    return "System.Char";

                default:
                    return typeName;
            }
        }
    }
}
