using Exam1.Models;
using Exam1.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleTables;
using System.Text;
using System.Threading.Tasks;
using Exam1.Menus;
using Exam1.Models.Data.Base;

namespace Exam1
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();

            menu.MenuStart();

            Console.Write("Ban co muon luu cac thay doi khong? (Y/N): ");
            var str = Console.ReadLine().ToUpper();
            if (str.Equals("Y"))
            {
                menu.Save();
            }
        }
    }
}