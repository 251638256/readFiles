using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using DynamicQuery;
using System.Diagnostics;
using readFiles.algorithm;
using HtmlAgilityPack;
using readFiles.XML;
using System.Xml;
using System.Security;
using System.Security.Cryptography;
using Autofac;

using System.Web.Compilation;

namespace readFiles {

    public enum DataType {
        SingGe
    }

    public interface IWriter {
        void Write(string content);
    }

    public class ConsoleWriter : IWriter {
        private readonly IDateWriter DateWriter;
        public ConsoleWriter(IDateWriter _dateWriter) {
            DateWriter = _dateWriter; // 构造函数注入
        }
        public void Write(string content) {
            DateWriter.WriteDate();
            Console.WriteLine(content);
        }
    }

    public class OtherWriter : IWriter {
        public void Write(string content) {
            Console.WriteLine("OtherWriter:" + content);
        }
    }

    public interface IDateWriter {
         void WriteDate();
    }


    public class DateWriter : IDateWriter {
        //private readonly IWriter writer;
        //public DateWriter(IWriter _writer) {
        //    writer = _writer;
        //}
        public void WriteDate() {
            //writer.Write("互相依赖??");
            Console.WriteLine(DateTime.Now.ToShortDateString());
        }
    }

    public interface IBaseInterface {

    }

    public interface IRepository<T> : IBaseInterface where T : class  {
        T Add(T obj);
        T Update(T obj);
        T Delete(T obj);
        IQueryable<T> GetAll();
    }

    public class BaseRepository<T> : IRepository<T> where T : class,new() {
        public T Add(T obj) {
            return new T();
        }

        public T Delete(T obj) {
            return new T();
        }

        public IQueryable<T> GetAll() {
            Console.WriteLine("Call GetAll Mothed, The Type is : " + typeof(T).Name);
            return new List<T>().AsQueryable();
        }

        public T Update(T obj) {
            return new T();
        }
    }

    public interface ITaskRepository : IRepository<Task> {
        void Taskmethod();
    }

    public class TaskRepository : BaseRepository<Task>, ITaskRepository {
        public void Taskmethod() {
            Console.WriteLine("Task method");
        }
    }

    public interface ICustomerRepository : IRepository<Customer> {
        void Customermethod();
    }

    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository {
        public void Customermethod() {
            Console.WriteLine("Customer method");
        }
    }


    public class Task {
        public int id { get; set; }
        public string taskName { get; set; }
        public int taskDesc { get; set; }
    }

    public class Customer {
        public int id { get; set; }
        public string Name { get; set; }
    }

    public class TestUseRepository {
        private ITaskRepository _taskRepository;
        private ICustomerRepository _customerRepository;
        private IRepository<Task> _rep;
        private IBaseInterface _baseInterface;

        public TestUseRepository(ITaskRepository taskR, ICustomerRepository customerR, IRepository<Task> task, IBaseInterface baseI) {
            _taskRepository = taskR;
            _customerRepository = customerR;
            _rep = task;
            _baseInterface = baseI;
        }

        public void Test() {
            _taskRepository.Taskmethod();
            _customerRepository.Customermethod();
            _rep.GetAll();
        }
    }

    public class Program {

        private delegate void Test();

        public static IContainer Builder { get; set; }

        private static void Main(string[] args) {

            RegisterType();




            Console.WriteLine("Press any key to ...!");
            Console.ReadKey();
        }
        #region AutoFac
        public static void RegisterType() {
            var builder = new ContainerBuilder();
            builder.RegisterType<OtherWriter>().As<IWriter>();
            builder.RegisterType<ConsoleWriter>().As<IWriter>();
            builder.RegisterType<DateWriter>().As<IDateWriter>();

            //builder.RegisterType<TaskRepository>().As<ITaskRepository>();
            //builder.RegisterType<CustomerRepository>().As<ICustomerRepository>();
            builder.RegisterType<TestUseRepository>();

            //var bType = typeof(IRepository<>);
            var bType = typeof(IBaseInterface);
            Assembly a = Assembly.GetExecutingAssembly();
            Type[] types = a.GetTypes();

            var r = builder.RegisterAssemblyTypes(a);
            var r2 = r.Where(c => bType.IsAssignableFrom(c) && c != bType);
            var r3 = r2.AsImplementedInterfaces();

            Builder = builder.Build();

            TestUseRepository test = Builder.Resolve<TestUseRepository>();
            test.Test();
        }
        #endregion

        public static void hashPassword() {
            //for (int i = 0; i < 10; i++) {
            //    string painPwd = "123";
            //    if (i >= 5)
            //        painPwd = "635";

            //    string v = HashPassword(painPwd);
            //    bool isSucc = VerifyHashedPassword(v, "123");
            //    Console.WriteLine($"密码 123 加密后 : {v}, 验证 : {isSucc}");
            //}
        }

        public static string HashPassword(string password) {
            byte[] salt;
            byte[] bytes;
            if (password == null) {
                throw new ArgumentNullException("password");
            }
            using (System.Security.Cryptography.Rfc2898DeriveBytes rfc2898DeriveByte = new System.Security.Cryptography.Rfc2898DeriveBytes(password, 16, 1000)) {
                salt = rfc2898DeriveByte.Salt;
                bytes = rfc2898DeriveByte.GetBytes(32);
            }
            byte[] numArray = new byte[49];
            Buffer.BlockCopy(salt, 0, numArray, 1, 16);
            Buffer.BlockCopy(bytes, 0, numArray, 17, 32);
            return Convert.ToBase64String(numArray);
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password) {
            byte[] bytes;
            if (hashedPassword == null) {
                throw new ArgumentNullException("hashedPassword");
            }
            if (password == null) {
                throw new ArgumentNullException("password");
            }
            byte[] numArray = Convert.FromBase64String(hashedPassword);
            if ((int)numArray.Length != 49 || numArray[0] != 0) {
                return false;
            }
            byte[] numArray1 = new byte[16];
            Buffer.BlockCopy(numArray, 1, numArray1, 0, 16);
            byte[] numArray2 = new byte[32];
            Buffer.BlockCopy(numArray, 17, numArray2, 0, 32);
            using (Rfc2898DeriveBytes rfc2898DeriveByte = new Rfc2898DeriveBytes(password, numArray1, 1000)) {
                bytes = rfc2898DeriveByte.GetBytes(32);
            }
            return ByteArraysEqual(numArray2, bytes);
        }

        private static bool ByteArraysEqual(byte[] a, byte[] b) {
            if (object.ReferenceEquals(a, b)) {
                return true;
            }
            if (a == null || b == null || (int)a.Length != (int)b.Length) {
                return false;
            }
            bool flag = true;
            for (int i = 0; i < (int)a.Length; i++) {
                flag = flag & a[i] == b[i];
            }
            return flag;
        }

        /// <summary>
        /// XML PARSE 
        /// </summary>
        public static void XMLParse() {
            string path = Environment.CurrentDirectory + "/system-new.config";
            string oldPath = Environment.CurrentDirectory + "/system-old.config";
            Console.WriteLine(path);
            Console.WriteLine(oldPath);

            if (!File.Exists(path)) {
                Console.WriteLine("新文件不存在");
            }

            if (!File.Exists(oldPath)) {
                Console.WriteLine("旧文件不存在");
            }

            if (File.Exists(oldPath) && File.Exists(path))
                XMLHelper.CompareXML(path, oldPath);
        }

        public static void HTMLParse() {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(@"C:\Users\work\Desktop\新建文件夹\郫县体检表 - 新.html", System.Text.Encoding.UTF8);
            var s = doc.DocumentNode.ChildNodes;

            var pattern = @"//*[starts-with(@class,'TempIndex')]";
            var result = doc.DocumentNode.SelectNodes(pattern);
        }

        public static void TestAlgo() {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var data = Sort1(GenerateRandomNumbers(10));
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds / 1000.0);
            Console.WriteLine(string.Join(",", data));
        }

        public static List<int> GenerateRandomNumbers(int count) {
            Random r = new Random();
            List<int> lst = new List<int>(100000);
            for (int i = 0; i < count; i++) {
                int v = r.Next() % 1000;
                lst.Add(v);
            }
            return lst;
        }

        public static List<int> Sort1(List<int> arr){
            for (int i = 0; i < arr.Count(); i++) {

                int maxIndex = i;
                for (int j = i + 1; j < arr.Count(); j++) {
                    if (arr[j] < arr[maxIndex]) {
                        maxIndex = j; // 记位置
                    }
                }

                int temp = arr[i];
                arr[i] = arr[maxIndex];
                arr[maxIndex] = temp;
            }

            return arr;
        }

        public static int MaxNumber(int[] arr) {
            if (arr == null || !arr.Any())
                throw new Exception("Null");

            int max = arr[0];
            for (int i = 0; i < arr.Length; i++) {
                if (arr[i] > max) {
                    max = arr[i]; // 记录数字
                }
            }

            return max;
        }

        public static int[]fuck = new int[50];
        public static int totl = 0;
        public static int lineCount = 8;
        public static void Search(int n) {
            if (n == lineCount) {
                totl++;
            } else {
                for (int i = 0; i < lineCount; i++) {
                    fuck[n] = i;// 在第N行的I位置
                    bool isok = true;
                    for (int j = 0; j < n; j++) {
                        if (fuck[n] == fuck[j] || n - fuck[n] == j - fuck[j] || n + fuck[n] == j + fuck[j]) {
                            isok = false;
                            break;
                        }
                    }

                    if (isok)
                        Search(n + 1);
                }
            }
        }

        static bool[,] data = new bool[8, 8];
        static void Searh2(int n) {
            if (n == lineCount) {
                totl++;
            } else {
                for (int i = 0; i < lineCount; i++) {
                    for (int j = 0; j < i; j++) {
                        data[i,j] = true;

                        bool ok = true;
                        // 判断下数据是否合法
                        for (int k = 0; k < i; k++) {
                            
                        }

                        if (ok)
                            Searh2(n + 1);
                    }
                }

            }
        }


    }
}
