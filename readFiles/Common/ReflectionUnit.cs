using System;
using System.Reflection;
using System.Linq;

namespace readFiles {
    public class ReflectionUnit {
        public static string GetPropertyValue<T>(string value) where T : class {
            //var data = new Person[] {
            //    new Person { Age = 18, Name = "AAA", Sex = 1, score = new Score { Csharp = 50, Database = 60, Java = 70 } },
            //    new Person { Age = 16, Name = "BBB" ,Sex = 1, score = new Score { Csharp = 50, Database = 60, Java = 70 }},
            //    new Person { Age = 17, Name = "CCC", Sex = 0,score = new Score { Csharp = 50, Database = 60, Java = 70 } },
            //    new Person { Age = 19, Name = "DDD" ,Sex = 0, score = new Score { Csharp = 50, Database = 60, Java = 70 }},
            //    new Person { Age = 15, Name = "EEE", Sex = 0, score = new Score { Csharp = 50, Database = 60, Java = 70 } },
            //};

            Person p = new Person { Age = 18, Name = "AAA", Sex = 1, score = new Score { Csharp = 50, Database = 60, Java = 70 } };
            //PropertyInfo propertyAge = typeof(Person).GetProperty("Age");
            //PropertyInfo propertyScore = typeof(Person).GetProperty("score");
            //string age = propertyAge.GetValue(p).ToString();
            //Score score = propertyScore.GetValue(p) as Score;

            //PropertyInfo csharp = typeof(Person).GetProperty("score.Csharp");
            //string v = csharp.GetValue(p).ToString();

            string patt = "score.Csharp";

            string []names = patt.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            Type type = typeof(Person);
            for (int i = 0; i < names.Length; i++) {
                PropertyInfo info = type.GetProperty(names[i]);
                Type propertyType = info.PropertyType;
                object vv = info.GetValue(p);
                if (propertyType.IsInstanceOfType(vv)) {
                    //propertyType.
                    object t = propertyType.Assembly.CreateInstance(propertyType.FullName);
                    T vvvv = t as T;
                }
            }

            return "";
        }

        public static T GetPropertyValue<T>(string p,object obj) {
            string []names = p.Split(new char[] { '.' });
            object result = obj;
            for (int i = 0; i < names.Length; i++) {
                if (result.GetType().GetProperties().Select(c => c.Name).Contains(names[i])) {
                    result = result.GetType().GetProperty(names[i]).GetValue(result);
                } else {
                    throw new Exception($"类型{result.GetType()}没有{names[i]}属性");
                }
            }
            return (T)result;
        }
    }
}