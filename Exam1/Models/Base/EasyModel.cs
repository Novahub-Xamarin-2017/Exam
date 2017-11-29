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
                        case TypeCode.Int16:
                            propertyInfo.SetValue(this, Convert.ToInt16(Console.ReadLine()));
                            break;
                        case TypeCode.Int32:
                            propertyInfo.SetValue(this, Convert.ToInt32(Console.ReadLine()));
                            break;
                        case TypeCode.Double:
                            propertyInfo.SetValue(this, Convert.ToDouble(Console.ReadLine()));
                            break;
                        case TypeCode.DateTime:
                            var date = Console.ReadLine();
                            var list = date.Split('/').ToList();
                            var year = int.Parse(list[2]);
                            var month = int.Parse(list[1]);
                            var day = int.Parse(list[0]);
                            var birthday = new DateTime(year, month, day);
                            propertyInfo.SetValue(this, birthday);
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
