using RazorEngine.Templating;
using System.Data;
using System;
using System.IO;

namespace Template {
    public class MyCustomTemplateBase<T> : TemplateBase<T> {
        public MyCustomTemplateBase() {
            StringHelper = new StringHelper();
        }

        public string MyUpper(string name) {
            return name.ToUpper();
        }

        public string KeyValues(string key, string dataSource) {
            
            
            
            return "";
        }

        public string test() {
            int i = 0;
            if (Convert.ToBoolean(i)) {
                return "test fucking";
            }
            return "test success";
            
        }






        public StringHelper StringHelper { get; set; }
    }

    public class MyTemplpateFor<T> : ITemplate<T> {
        public IInternalTemplateService InternalTemplateService {
            set {
                throw new NotImplementedException();
            }
        }

        public T Model {
            get {
                throw new NotImplementedException();
            }

            set {
                throw new NotImplementedException();
            }
        }

        public IRazorEngineService Razor {
            set {
                throw new NotImplementedException();
            }
        }

        public IRazorEngineService RazorEngine {
            set {
                throw new NotImplementedException();
            }
        }

        public ITemplateService TemplateService {
            set {
                throw new NotImplementedException();
            }
        }

        public void Execute() {
            throw new NotImplementedException();
        }

        public void Run(ExecuteContext context, TextWriter writer) {
            throw new NotImplementedException();
        }

        public void SetData(object model, DynamicViewBag viewbag) {
            throw new NotImplementedException();
        }

        public void Write(object value) {
            throw new NotImplementedException();
        }

        public void WriteLiteral(string literal) {
            throw new NotImplementedException();
        }
    }

    public class DataSource {
        DataSet dbSet { get; set; }

    }
}