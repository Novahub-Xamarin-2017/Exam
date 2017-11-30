using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Exam1.Models.Base
{
    public class EasyModel
    {
        private readonly List<PropertyInfo> propertyInfos;

        public EasyModel()
        {
            propertyInfos = GetType().GetProperties().ToList();
        }

        /*[IgnoreInput]
        public int Id { get; set; }

        [PrompDisplay("Ten hoc sinh ")]
        public string Name { get; set; }

        [PrompDisplay("Diem trung binh")]
        public int Score { get; set; }*/


        public void Input()
        {
            foreach (var propertyInfo in propertyInfos.Where(x => x.CanWrite))
            {
                var prompDisplay = Extension.GetAttribute<PrompDisplayAttribute>(propertyInfo);

                var ignoreInput = Extension.GetAttribute<IgnoreInputAttribute>(propertyInfo);

                if (ignoreInput == null)
                {
                    Console.Write($"Nhap {prompDisplay?.Display ?? propertyInfo.Name}: ");

                    switch (Type.GetTypeCode(propertyInfo.PropertyType))
                    {
                        case TypeCode.String:
                            propertyInfo.SetValue(this, Console.ReadLine());
                            break;
                        case TypeCode.Int32:
                            var numberInt = 0;

                            while (!int.TryParse(Console.ReadLine(), out numberInt))
                            {
                                Console.WriteLine("Kieu du lieu nhap vao khong dung. Vui long nhap lai hoac nhap 0 de thoat");
                                Console.Write($"Nhap {prompDisplay?.Display ?? propertyInfo.Name}: ");
                            }

                            propertyInfo.SetValue(this, numberInt);
                            break;
                        case TypeCode.Double:
                            var numberDouble = 0.0;

                            while (!double.TryParse(Console.ReadLine().Replace('.', ','), out numberDouble))
                            {
                                Console.WriteLine("Kieu du lieu nhap vao khong dung. Vui long nhap lai hoac nhap 0 de thoat");
                                Console.Write($"Nhap {prompDisplay?.Display ?? propertyInfo.Name}: ");
                            }

                            if (propertyInfo.Name.Equals("AverageScore") && !(numberDouble >= 0 && numberDouble <= 10))
                            {
                                Console.WriteLine("Kieu du lieu nhap vao khong dung. Du lieu phai nam trong khoan >=0 va <=10. Vui long nhap lai hoac nhap 0 de thoat");
                                Console.Write($"Nhap {prompDisplay?.Display ?? propertyInfo.Name}: ");

                                while (!(double.TryParse(Console.ReadLine().Replace('.', ','), out numberDouble) && numberDouble >= 0 && numberDouble <= 10)) 
                                {
                                    Console.WriteLine("Kieu du lieu nhap vao khong dung. Du lieu phai nam trong khoan >=0 va <=10. Vui long nhap lai hoac nhap 0 de thoat");
                                    Console.Write($"Nhap {prompDisplay?.Display ?? propertyInfo.Name}: ");
                                }
                            }

                            propertyInfo.SetValue(this, numberDouble);
                            break;
                        case TypeCode.DateTime:
                            var datetime = new DateTime();

                            var date = Console.ReadLine();
                            var list = date.Split('/').ToList();

                            while (!(date.Length == 10 && date.Count(x => x == '/') == 2 && DateTime.TryParse(list[2]+"/" + list[1]+"/" + list[0], out datetime)))
                            {
                                Console.WriteLine("Kieu du lieu nhap vao khong dung. Vui long nhap lai hoac nhap 00/00/0000 de thoat");
                                Console.Write($"Nhap {prompDisplay?.Display ?? propertyInfo.Name}(dd/mm/yyyy): ");
                                date = Console.ReadLine();
                                list = date.Split('/').ToList();
                            }

                            propertyInfo.SetValue(this, datetime);
                            break;
                    }
                }
            }
        }

        public override string ToString()
        {
            return string.Join(", ", propertyInfos
                .Where(x => x.CanRead)
                .Select(x => $"{x.Name}: {x.GetValue(this)}"));
        }
    }
}
