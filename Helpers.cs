using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2.ObjektOrienteradProgrammering
{
    public static class Helpers
    {
        public static void ToConsole(this string text, Status status = Status.Neutral)
        {
            switch (status)
            {
                case Status.Neutral:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Status.OK:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Status.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                default:
                    break;
            }
            Console.WriteLine(text);
            Console.ResetColor();
        }
    }
}
