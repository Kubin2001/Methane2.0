using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Methane2._0{
    public class InterPreter{
        string configPath = null;
        string vsPath = null;
        bool ready = false;
        private Dictionary<string, Action<string>> Commands;
        private string fullLine = null;

        public InterPreter(string confPath, string vsPath) {
            if(confPath != null && vsPath != null){
                this.configPath = confPath;
                this.vsPath = vsPath;
                ready = true;
                CreateCommands();
            }
        }

        private void CreateCommands() {
            Commands = new Dictionary<string, Action<string>>();
            Commands["AddIncludeDir"] = AddIncludeDir;
        }

        private void AddIncludeDir(string command) {

            bool pathRegister = false;
            string includePath = "";
            foreach (char elem in fullLine.ToCharArray()) {
                if (elem == '(') {
                    pathRegister = true;
                    continue;
                }
                if (elem == ')') {
                    break;
                }
                if (pathRegister) {
                    includePath += elem;
                }

            }
            Console.WriteLine(includePath);
            includePath += ';';


            XmlDocument doc = new XmlDocument();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");
            

            try {
                doc.Load(vsPath);
                XmlNodeList clCompileNodes = doc.SelectNodes("//msb:Project/msb:ItemDefinitionGroup/msb:ClCompile", nsmgr);
                foreach (XmlNode node in clCompileNodes) {
                    XmlNode additionalIncludesNode = node.SelectSingleNode("msb:AdditionalIncludeDirectories", nsmgr);
                    if (additionalIncludesNode != null) {
                        additionalIncludesNode.InnerText += $";{includePath}";
                    }
                    else {
                        XmlElement newElement = doc.CreateElement("AdditionalIncludeDirectories", nsmgr.LookupNamespace("msb"));
                        newElement.InnerText = includePath;
                        node.AppendChild(newElement);
                    }
                }
                doc.Save(vsPath);
            }
            catch (Exception ex) {
                MessageBox.Show("Failed to modify project:" + ex.Message);
            }
        }

        public void Run(){
            if (ready) {
                String[] config = File.ReadAllLines(this.configPath);
                foreach (string line in config) {
                    string parsedLine = "";
                    if (line.Contains('(')) {
                        foreach (char elem in line) {
                            if (elem == '(') {
                                break;
                            }
                            parsedLine += elem;
                        }
                    }
                    else { 
                        parsedLine = line;
                    }
                    if (Commands.ContainsKey(parsedLine)) {
                        fullLine = line;
                        Commands[parsedLine](parsedLine);
                    }
                    else {
                        Console.WriteLine("Command Does not exist");
                    }
                }
            }

        }
    }
}
