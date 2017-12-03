using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ConsoleTables;
using Exam1.Models.Data;

namespace Exam1.Models.Base
{
    public static class Extension
    {
        public static T GetAttribute<T>(PropertyInfo propertyInfo) where T : Attribute
        {
            return propertyInfo.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
        }

        public static void SaveJson<T>(this List<T> data, string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var fileName = $"{typeof(T).Name}s.json";
            var filePath = Path.Combine(directory, fileName);
            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(filePath, json);
        }

        public static List<T> GetObject<T>(this List<T> data, string directory)
        {
            var fileName = $"{typeof(T).Name}s.json";
            var filePath = Path.Combine(directory, fileName);

            if (File.Exists(filePath)) 
            {
                return (JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath)) ==null) ? new List<T>() : JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(filePath));
            }

            return new List<T>();
        }

        public static List<string> GetMenu<T>(this List<T> data, string filePath)
        {

            if (File.Exists(filePath))
            {
                return (!File.ReadAllText(filePath).Any()) ? new List<string>() : File.ReadAllText(filePath).Split(';').ToList();
            }

            return new List<string>();
        }

        public static string GetPick(this List<string> data)
        {
            data.ForEach(Console.WriteLine);

            Console.Write($"Nhap 1 so trong khoang tu 1 -> {data.Count} de thuc hien cac chuc nang o ben phai: ");
            return Console.ReadLine();
        }

        public static void Menu(this List<string> data, Action<string, int> work)
        {
            var pick = "";

            do
            {
                pick = data.GetPick();
                work(pick, data.Count);
            }
            while (!pick.Equals(data.Count.ToString())); 
        }

        public static int GetId<T>(this List<T> data, bool showList) where T : EasyModel
        {
            if (showList)
            {
                data.ShowConsoleTable();
            }

            Console.Write($"Nhap {typeof(T).Name}Id: ");

            var id = 0;

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Kieu du lieu nhap vao khong dung. Vui long nhap lai hoac nhap 0 de thoat");
                Console.Write($"Nhap {typeof(T).Name}Id: ");
            }

            return id;
        }

        public static void ShowConsoleTable<T>(this IEnumerable<T> data)
        {
            if (data.Any())
            {
                Console.WriteLine(ConsoleTable.From(data));

                return;
            }

            Console.WriteLine("Khong tim thay du lieu");
        }
    }
}
