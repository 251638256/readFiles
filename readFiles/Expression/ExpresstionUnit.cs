using System;
using System.Linq;
using System.Linq.Expressions;

namespace readFiles {

    public class ExpresstionUnit {

        public static void TestDyQuery() {

            //Person[] ps = new Person[] {
            //    new Person() { Age = 18, Name = "AA" },
            //    new Person() { Age = 19, Name = "AA" },
            //    new Person() { Age = 20, Name = "AA" },
            //    new Person() { Age = 21, Name = "AA" },
            //};
            //var queryable = ps.AsQueryable();

            //var conditions = new QueryDescriptor();
            //conditions.Conditions = new List<QueryCondition>();
            //conditions.Conditions.Add(
            //    new QueryCondition() { Key = "Age", ValueType = "int", Value = "18,19", Operator = QueryOperator.IN }
            //);
            //var list = queryable.Query(conditions).ToList();
            //Console.WriteLine(string.Join(",", list));

            //var config = TemplateServiceConfiguration();
        }

        /// <summary>
        /// 其他基础测试
        /// </summary>
        public static void Basic() {
            Expression<Func<int, int, int>> exp = (a, b) => a + b;
            //Expression<List<int>> s = Expression.Constant("A");

            var con = Expression.Constant("Hello");
            var callExpression = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), con);
            var loop = Expression.Loop(callExpression);

            var con2 = Expression.Constant("World");
            var callExpression2 = Expression.Call(null, typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }), con);
            var loop2 = Expression.Loop(callExpression);

            ParameterExpression parm = Expression.Parameter(typeof(int), "number");
            var myBlock = Expression.Block(new[] { parm },
                Expression.Assign(parm, Expression.Constant(2)),
                Expression.AddAssign(parm, Expression.Constant(10)),
                Expression.DivideAssign(parm, Expression.Constant(2))
            );

            var resultExpresstion = Expression.Lambda<Func<int>>(myBlock);
            var lamda = resultExpresstion.Compile();
        }

        public static void BuildVar() {
            Expression<Func<int>> c = Expression.Lambda<Func<int>>(Expression.Constant(595));
            Single(c);
        }

        public static void Single(Expression<Func<int>> lambda) {
            // 新问题? 怎么解析Expression<TDelegate>
            var constant = lambda.Body as ConstantExpression;
            var r = constant.Value;
            Console.WriteLine();
        }

        public static object NewExpression(Expression<Func<Person, object>> propertyExpression) {
            //MemberInfo[] memberInfos = (propertyExpression.Body as NewExpression).Members.Select(m => m as PropertyInfo).ToArray();
            //NewExpression newE = Expression.New(typeof(object),Expression.Parameter(typeof(Person),"Age"));

            Expression<Func<Person, object>> property = c => new { c.Age };
            //NewExpression newEx = (property.Body as NewExpression);

            //var parm = Expression.Parameter(typeof(Person));

            //Func<Person,object> func = c => new { c.Age };
            //var lnewEx = Expression.Lambda<Expression<Func<Person, object>>>(Expression.New(typeof(object)),Expression.Parameter(typeof(int)));

            //ConstructorInfo ctor = typeof(object).GetConstructor(BindingFlags.Public, null, new Type[] { typeof(object) }, null);

            BinaryExpression b = Expression.GreaterThan(Expression.Constant(5), Expression.Constant(2));

            //Console.WriteLine();
            return null;
        }

        //    public static object NewExpression(Expression<Func<Person, object>> propertyExpression) {
        //    }

        //    public static object NewExpression2Refl(Expression<Func<Person, object>> propertyExpression) {
        //        ParameterExpression expression2;
        //        Expression[] arguments = new Expression[] { Expression.Property(expression2 = Expression.Parameter(typeof(Person), "c"), (MethodInfo)methodof(Person.get_Age)) };
        //        MemberInfo[] members = new MemberInfo[] { (MethodInfo)methodof(<> f__AnonymousType0<int>.get_Age, <> f__AnonymousType0<int>) };
        //        ParameterExpression[] parameters = new ParameterExpression[] { expression2 };
        //        Expression<Func<Person, object>> expression = Expression.Lambda<Func<Person, object>>(Expression.New((ConstructorInfo)methodof(<> f__AnonymousType0<int>..ctor, <> f__AnonymousType0<int>), arguments, members), parameters);
        //        return null;
        //    }

        //    public static object NewExpression2JustDeCompile(Expression<Func<Person, object>> propertyExpression) {
        //        ParameterExpression parameterExpression = Expression.Parameter(typeof(Person), "c");
        //        ConstructorInfo methodFromHandle = (ConstructorInfo)MethodBase.GetMethodFromHandle(typeof(<> f__AnonymousType0<int>).GetMethod(".ctor", new Type[] { typeof(u003cAgeu003ej__TPar) }).MethodHandle, typeof(<> f__AnonymousType0<int>).TypeHandle);
        //        Expression[] expressionArray = new Expression[] { Expression.Property(parameterExpression, (MethodInfo)MethodBase.GetMethodFromHandle(typeof(Person).GetMethod("get_Age").MethodHandle)) };
        //        MemberInfo[] memberInfoArray = new MemberInfo[] { (MethodInfo)MethodBase.GetMethodFromHandle(typeof(<> f__AnonymousType0<int>).GetMethod("get_Age").MethodHandle, typeof(<> f__AnonymousType0<int>).TypeHandle) };
        //    Expression.Lambda<Func<Person, object>>(Expression.New(methodFromHandle, (IEnumerable<Expression>)expressionArray, memberInfoArray), new ParameterExpression[] { parameterExpression
        //});
        //        return null;
        //    }

        /// <summary>
        /// 简单类型构建出一个c => c > 5的表达式
        /// </summary>
        public static void BuildLambdaWithValueType() {
            ParameterExpression parm = Expression.Parameter(typeof(int));
            var birExpr = Expression.GreaterThan(parm, Expression.Constant(5));
            var result = Expression.Lambda<Func<int, bool>>(birExpr, parm);
            var lst = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }.AsQueryable().Where(result);
            Console.WriteLine(string.Join(",", lst.Select(c => c)));
            Console.WriteLine();
        }

        /// <summary>
        /// 构建一个对象 c => c.Property > 10
        /// </summary>
        public static void BuildLambdaWithClass() {
            var data = new Person[] {
                new Person { Age = 18, Name = "AAA", Sex = 1, score = new Score { Csharp = 50, Database = 60, Java = 70 } },
                new Person { Age = 16, Name = "BBB" ,Sex = 1, score = new Score { Csharp = 50, Database = 60, Java = 70 }},
                new Person { Age = 17, Name = "CCC", Sex = 0,score = new Score { Csharp = 50, Database = 60, Java = 70 } },
                new Person { Age = 19, Name = "DDD" ,Sex = 0, score = new Score { Csharp = 50, Database = 60, Java = 70 }},
                new Person { Age = 15, Name = "EEE", Sex = 0, score = new Score { Csharp = 50, Database = 60, Java = 70 } },
            };
            ParameterExpression parm = Expression.Parameter(typeof(Person));
            var left = Expression.Property(parm, "Age"); // lambda的parm和这个参数的parm必须是相同的对象 否则会导致是两个同名参数问题
            var condation = Expression.GreaterThanOrEqual(left, Expression.Constant(18));
            var make = Expression.MakeBinary(ExpressionType.LessThan, left, Expression.Constant(18));
            var lambda = Expression.Lambda<Func<Person, bool>>(make, parm);
            Expression<Func<Person, bool>> self = c => c.Age >= 15;
            var result = data.AsQueryable().Where(lambda).ToList();
            Console.WriteLine();
        }

        /// <summary>
        /// 构建一个对象 c => c.Property == null
        /// </summary>
        public static void BuildLambdaWithNull() {
            var data = new Person[] {
                new Person { Age = 18, Name = "AAA", Sex = 1, score = new Score { Csharp = 50, Database = 60, Java = 70 } },
                new Person { Age = 16, Name = "BBB" ,Sex = 1, score = new Score { Csharp = 50, Database = 60, Java = 70 }},
                new Person { Age = 17, Name = "CCC", Sex = 0,score = new Score { Csharp = 50, Database = 60, Java = 70 } },
                new Person { Age = 19, Name = "DDD" ,Sex = null, score = new Score { Csharp = 50, Database = 60, Java = 70 }},
                new Person { Age = 15, Name = "EEE", Sex = 0, score = new Score { Csharp = 50, Database = 60, Java = 70 } },
                new Person { Age = 105, Name = null, Sex = 0, score = new Score { Csharp = 50, Database = 60, Java = 70 } }
            };
            ParameterExpression parm = Expression.Parameter(typeof(Person));
            var left = Expression.Property(parm, "Sex"); // lambda的parm和这个参数的parm必须是相同的对象 否则会导致是两个同名参数问题
            //var condation = Expression.GreaterThanOrEqual(left, Expression.Constant("BBB"));
            var make = Expression.MakeBinary(ExpressionType.Equal, left, Expression.Constant(null));
            var lambda = Expression.Lambda<Func<Person, bool>>(make, parm);
            Expression<Func<Person, bool>> self = c => c.Age >= 15;
            var result = data.AsQueryable().Where(lambda).ToList();
            Console.WriteLine();
        }

        /// <summary>
        /// 构建一个对象 c => c.Property > 10 && sex = 1
        /// </summary>
        public static void BuildLambdaWithMoreCondations() {
            var data = new Person[] {
                new Person { Age = 18, Name = "AAA", Sex = 1, score = new Score { Csharp = 50, Database = 60, Java = 70 } },
                new Person { Age = 16, Name = "BBB" ,Sex = 1, score = new Score { Csharp = 50, Database = 60, Java = 70 }},
                new Person { Age = 17, Name = "CCC", Sex = 0,score = new Score { Csharp = 50, Database = 60, Java = 70 } },
                new Person { Age = 19, Name = "DDD" ,Sex = 0, score = new Score { Csharp = 50, Database = 60, Java = 70 }},
                new Person { Age = 15, Name = "EEE", Sex = 0, score = new Score { Csharp = 50, Database = 60, Java = 70 } },
            };
            ParameterExpression parm = Expression.Parameter(typeof(Person));
            var left = Expression.Property(parm, "Age"); // lambda的parm和这个参数的parm必须是相同的对象 否则会导致是两个同名参数问题
            var condation = Expression.GreaterThanOrEqual(left, Expression.Constant(18));
            condation = Expression.AndAlso(condation, Expression.Equal(Expression.Property(parm, "Sex"), Expression.Constant(1))); // andalso 实现了 && "短路"运算的特效
            var lambda = Expression.Lambda<Func<Person, bool>>(condation, parm);
            Expression<Func<Person, bool>> self = c => c.Age >= 15;
            var result = data.AsQueryable().Where(lambda).ToList();
            Console.WriteLine();
        }

        /// <summary>
        /// 构建一个对象 c => c.socre.Cshar == 60 && c.sex == 1
        /// </summary>
        public static void BuildLambda() {
            var data = new Person[] {
                new Person { Age = 18, Name = "AAA", Sex = 1, score = new Score { Csharp = 50, Database = 60, Java = 70 } },
                new Person { Age = 16, Name = "BBB" ,Sex = 1, score = new Score { Csharp = 50, Database = 60, Java = 70 }},
                new Person { Age = 17, Name = "CCC", Sex = 1,score = new Score { Csharp = 60, Database = 60, Java = 70 } },
                new Person { Age = 19, Name = "DDD" ,Sex = 0, score = new Score { Csharp = 50, Database = 60, Java = 70 }},
                new Person { Age = 15, Name = "EEE", Sex = 0, score = new Score { Csharp = 50, Database = 60, Java = 70 } },
            };
            ParameterExpression parm = Expression.Parameter(typeof(Person));
            var left = Expression.Property(parm, "score"); // lambda的parm和这个参数的parm必须是相同的对象 否则会导致是两个同名参数问题
            left = Expression.Property(left, "Csharp");
            var condation = Expression.GreaterThanOrEqual(left, Expression.Constant(60));
            condation = Expression.AndAlso(condation, Expression.Equal(Expression.Property(parm, "Sex"), Expression.Constant(1))); // andalso 实现了 &&的"短路"功能
            var lambda = Expression.Lambda<Func<Person, bool>>(condation, parm);
            Expression<Func<Person, bool>> self = c => c.Age >= 15;
            var result = data.AsQueryable().Where(lambda).ToList();
            Console.WriteLine();
        }
    }
}