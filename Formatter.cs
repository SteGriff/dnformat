using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dnformat
{
    public static class Formatter
    {

        public static void FormatOutput(string format, string paramCollector)
        {
            //Format stage

            IList<object> paramObjects = new List<object>();

            string[] typesAndParams = paramCollector.Split(':', ',');

            int j = 0;
            Type lastContentType = typeof(object);

            foreach (string tp in typesAndParams)
            {

                //If even (0, 2...) then its a type
                if (j % 2 == 0)
                {
                    //Get the type
                    try
                    {
                        lastContentType = realTypeNameOf(tp); 
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
                        return;
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
                if (ex.GetType() == typeof (FormatException))
                {
                    Console.WriteLine("  Did you mismatch the number of params and placeholders?");
                    Console.WriteLine("  Did you separate params with commas? E.g. int:3,string:hello");
                }
                
            }
        }

        private static Type realTypeNameOf(string typeName)
        {
            switch (typeName.ToLower())
            {
                case "object":
                    return typeof(System.Object);

                case "string":
                    return typeof(System.String);

                case "bool":
                    return typeof(System.Boolean);

                case "byte":
                    return typeof(System.Byte);

                case "sbyte":
                    return typeof(System.SByte);

                case "short":
                    return typeof(System.Int16);

                case "ushort":
                    return typeof(System.UInt16);

                case "int":
                    return typeof(System.Int32);

                case "uint":
                    return typeof(System.UInt32);

                case "long":
                    return typeof(System.Int64);

                case "ulong":
                    return typeof(System.UInt64);

                case "float":
                    return typeof(System.Single);

                case "double":
                    return typeof(System.Double);

                case "decimal":
                    return typeof(System.Decimal);

                case "char":
                    return typeof(System.Char);

                case "datetime":
                    return typeof(System.DateTime);

                case "timespan":
                    return typeof(System.TimeSpan);

                default:
                    throw new NotImplementedException(String.Format("Type {0} is not featured.", typeName));
            }
        }
    }
}
