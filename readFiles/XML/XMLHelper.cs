using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace readFiles.XML {
    public class XMLHelper {
        static public void CreatXmlTree(string xmlPath) {
            XElement xElement = new XElement(
                new XElement("BookStore",
                    new XElement("Book",
                        new XElement("Name", "C#入门", new XAttribute("BookName", "C#")),
                        new XElement("Author", "Martin", new XAttribute("Name", "Martin")),
                        new XElement("Adress", "上海"),
                        new XElement("Date", DateTime.Now.ToString("yyyy-MM-dd"))
                        ),
                    new XElement("Book",
                        new XElement("Name", "WCF入门", new XAttribute("BookName", "WCF")),
                        new XElement("Author", "Mary", new XAttribute("Name", "Mary")),
                        new XElement("Adress", "北京"),
                        new XElement("Date", DateTime.Now.ToString("yyyy-MM-dd"))
                        )
                        )
                );

            //需要指定编码格式，否则在读取时会抛：根级别上的数据无效。 第 1 行 位置 1异常

            //XmlWriterSettings settings = new XmlWriterSettings();
            //settings.Encoding = new UTF8Encoding(false);
            //settings.Indent = true;

            XmlWriter xw = XmlWriter.Create(xmlPath,new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 });
            xElement.Save(xw);
            //写入文件
            xw.Flush();
            xw.Close();
        }

        static public void Create(string xmlPath) {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            var root = xmlDoc.DocumentElement;//取到根结点
            XmlNode newNode = xmlDoc.CreateNode("element", "NewBook", "");
            newNode.InnerText = "WPF";

            //添加为根元素的第一层子结点
            root.AppendChild(newNode);
            xmlDoc.Save(xmlPath);
        }

        static public void ParseConfigXML(string path) {


            //XmlDocument doc = new XmlDocument();
            //doc.Load(path);
            //XmlNodeList child = doc.DocumentElement.ChildNodes;

            //for (int i = 0; i < child.Count; i++) {
            //    var node = child[i];
            //    if (node.NodeType == XmlNodeType.Element && node.Name == "add") {
            //        string innerXmlInfo = node.InnerXml.ToString();
            //        string outerXmlInfo = node.OuterXml.ToString();
            //        var key = node.Attributes["key"].Value;
            //        var value = node.Attributes["value"].Value;
            //        Console.WriteLine(node.Name + " " + key + "  " + value);
            //        Console.WriteLine();
            //    }
            //}

            //var element = doc.CreateElement("add");
            //element.SetAttribute("key", "Fucking");
            //element.SetAttribute("value", "FuckingValue");
            //doc.DocumentElement.AppendChild(element);
            //doc.Save(path);

            XDocument doc = XDocument.Load(path);
            var elements = doc.Root.Elements("add");

            XElement node = elements.FirstOrDefault();
            var name = node.Attribute("key").Name;
            var value = node.Attribute("key").Value;

            var t = elements.Where(c => c.Attribute("key").Value == "RegisterMaxCountInADay").FirstOrDefault();
            var str = t.Attributes("value").FirstOrDefault().Value;



            doc.Save(path);


            Console.WriteLine();


        }

        static public void CompareXML(string newPath, string oldPath) {
            var newLst = GetAllAddNode(newPath);
            var oldLst = GetAllAddNode(oldPath);
            List<XElement> diffk = DiffKey(newLst, oldLst);
            List<XElement> diffv = DiffValue(newLst, oldLst);

            Console.WriteLine("旧文件中缺少以下配置属性:");
            if (diffk.Any())
                diffk.ForEach(c => Console.WriteLine(c.Attribute("key").Value));
            else {
                Console.WriteLine("没有节点");
            }
            Console.WriteLine("输入A增加旧文件中缺少的节点:");
            string input = Console.ReadLine();
            if (input.ToLower() == "a") {
                AddXElements(diffk, oldPath);
            }
        }

        private static List<XElement> DiffKey(List<XElement> newLst, List<XElement> oldLst) {
            List<XElement> diff = new List<XElement>();

            for (int i = 0; i < newLst.Count; i++) {

                bool has = false;
                for (int j = 0; j < oldLst.Count; j++) {
                    if (oldLst[j].Attribute("key").Value == newLst[i].Attribute("key").Value) {
                        has = true;
                        break;
                    }
                }

                if (!has) {
                    diff.Add(newLst[i]);
                }

            }
            //
            return diff;
        }

        private static List<XElement> DiffValue(List<XElement> newLst, List<XElement> oldLst) {
            List<XElement> diff = new List<XElement>();

            for (int i = 0; i < newLst.Count; i++) {
                for (int j = 0; j < oldLst.Count; j++) {
                    if (oldLst[j].Attribute("key").Value == newLst[i].Attribute("key").Value && (oldLst[j].Attribute("value").Value != newLst[i].Attribute("value").Value)) {
                        diff.Add(newLst[i]);
                    }
                }
            }

            return diff;
        }

        private static List<XElement> GetAllAddNode(string path) {
            XDocument doc = XDocument.Load(path);
            var elements = doc.Root.Elements("add");
            return elements.ToList();
        }

        private static void AddXElements(List<XElement> elements,string path) {
            XDocument doc = XDocument.Load(path);
            var element = doc.Root.Elements("add").LastOrDefault();
            if (element == null)
                throw new Exception("没有add节点!");

            element.AddAfterSelf(elements.ToArray());
            doc.Save(path);

        }

        private static void UpdateXElements(List<XElement> elements, string path) {

        }

    }



}
