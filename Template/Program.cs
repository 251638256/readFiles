using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RazorEngine;
using RazorEngine.Templating; // For extension methods.
using RazorEngine.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Dynamic;
using RazorEngine.Text;

namespace Template {
    class Program {
        static void Main(string[] args) {


            Test2();



            Console.WriteLine();
        }

        public static DataSet DataSource() {
            SqlConnection con = new SqlConnection(@"Data Source=192.168.0.60\SQLEXPRESS;Initial Catalog=青白江健康证20170605;User ID=sa;password=123;Integrated Security=false");
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from (select  i.PhID, CONVERT(char(12), i.PhysicalDate, 111) as PhysicalDate,i.SysNumber,m.MName,m.MSex,m.MAge,i.WorkType from Physical_Info as i join Physical_MemberInfo as m on i.ArchivesCode = m.ArchivesCode ) as tb where SysNumber = '02017062700001';    select * from (  select PhID,SysNumber,ProName as Name, ResultVal as Value from Physical_MemberProjectInfo union all  select PhID,SysNumber,'PhysicalConclusion' as Name,PhysicalConclusion as Value from Physical_Info union all  select PhID,SysNumber,'ConclusionDate' as Name,CONVERT(char(12), ConclusionDate, 111) as Value from Physical_Info) tb where SysNumber = '02017062700001' ;", con);
            SqlDataAdapter myDataAdapter = new SqlDataAdapter(cmd);
            DataSet myDataSet = new DataSet();
            myDataAdapter.Fill(myDataSet);

            //var table = myDataSet.Tables[0];
            //var keyvalues = myDataSet.Tables[1];
            //keyvalues.TableName = "MemberInfos";
            //var value = keyvalues.Rows[0]["ProName"];
            //int count = keyvalues.Rows.Count;

            return myDataSet;
        }

        public static void Test1() {
            var config = new TemplateServiceConfiguration();
            config.BaseTemplateType = typeof(MyCustomTemplateBase<>);


            using (var service = RazorEngineService.Create(config)) {
                string index_path = Environment.CurrentDirectory + "../../../Test.cshtml";
                string index = System.IO.File.ReadAllText(index_path, System.Text.Encoding.UTF8);

                var lst = new List<Student> { new Student { Age = 18, Height = 10, Name = "Fucking" }, new Student { Name = "CC", Height = 20, Age = 20 } };
                var bag = new DynamicViewBag();
                bag.AddValue("SSSS", "FUcking");

                var result = Engine.Razor.RunCompile(index, "fucking", null, new { Name = "World" }, bag);

                Console.WriteLine(result);
            }
        }

        public static void Test2() {
            DataSet set = DataSource();

            var config = new TemplateServiceConfiguration();
            config.BaseTemplateType = typeof(HtmlSupportTemplateBase<>);

            using (var service = RazorEngineService.Create(config)) {
                string templateFromFile = System.IO.File.ReadAllText(Environment.CurrentDirectory + "../../../Test.cshtml", System.Text.Encoding.UTF8);

                string template = "@Html.Raw(Model.Data)  @Html.toUpper(\"little boy\") @toUpper(\"little boy\") ";
                var model = new { Data = "My raw double quotes appears here \"hello!\" ", Name = "张三",  };

                var bag = new DynamicViewBag();
                bag.AddValue("SSSS", "FUcking");
                bag.AddValue("DataSet", set);

                string result = service.RunCompile(templateFromFile, "htmlRawTemplate", null, model, bag);
                Console.WriteLine("Result: {0}", result);
            }
            Console.WriteLine();
        }

        public class Student {
            public string Name { get; set; }
            public int Age { get; set; }
            public int Height { get; set; }
        }
    }

    public class MyHtmlHelper {
        public IEncodedString Raw(string rawString) {
            return new RawString(rawString);
        }

        public string toUpper(string c) {
            return c.ToUpper();
        }
    }

    public abstract class HtmlSupportTemplateBase<T> : TemplateBase<T> {

        private const string NotFound = "NotFound";
        private DataSet set;
        public HtmlSupportTemplateBase() {
            Html = new MyHtmlHelper();
        }

        public string InitDataSet(object obj) {
            if (obj is DataSet) {
                this.set = obj as DataSet;
            }
            else {
                throw new Exception("没有数据");
            }
            return string.Empty;
        }

        public string toUpper(string c) {
            return c.ToUpper();
        }

        /// <summary>
        /// keyvalue形式
        /// </summary>
        /// <param name="key"></param>
        /// <param name="DataTableIndex"></param>
        /// <returns></returns>
        public string GetValueFromKey(string key,int ?DataTableIndex = null) {
            if (!DataTableIndex.HasValue) {
                for (int i = 0; i < set.Tables.Count; i++) {
                    string result = GetValue(set.Tables[i], key);
                    if (result != NotFound) {
                        return result;
                    }
                }
            } else {
                if (DataTableIndex < 0 || DataTableIndex > set.Tables.Count - 1) {
                    throw new Exception("DataTableIndex越界");
                }
                string result = GetValue(set.Tables[(int)DataTableIndex], key);
                if (result != NotFound) {
                    return result;
                }
            }
            return "";
        }

        private string GetValue(DataTable table, string key) {
            int nameIndex = table.Columns.IndexOf("Name");
            int valueIndex = table.Columns.IndexOf("Value");
            if (nameIndex == -1 || valueIndex == -1)
                return NotFound;

            for (int i = 0; i < table.Rows.Count; i++) {
                DataRow row = table.Rows[i];
                if (row[nameIndex].ToString() == key) {
                    return row[valueIndex].ToString();
                }
            }
            return NotFound;
        }

        /// <summary>
        /// 从列名获取第一行的数据
        /// </summary>
        /// <returns></returns>
        public string GetVaueFromColName(string colName) {
            return "";
        }


        public MyHtmlHelper Html { get; set; }
    }
}
