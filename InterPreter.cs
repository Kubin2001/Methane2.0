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
        private Dictionary<string, Action> Commands;
        private string fullLine = null;
        private string currentCommand = null;
        private List<string> args = new List<string>();

        public InterPreter(string confPath, string vsPath) {
            if(confPath != null && vsPath != null){
                this.configPath = confPath;
                this.vsPath = vsPath;
                ready = true;
                CreateCommands();
            }
        }

        private void CreateCommands() {
            Commands = new Dictionary<string, Action>();
            Commands["AddIncludeDir"] = AddIncludeDir;
            Commands["AddLibraryDir"] = AddLibraryDir;
            Commands["AddDependencies"] = AddDependencies;

        }

        private void AddIncludeDir() {
            if(args.Count() != 1) {
                Console.WriteLine("Error in AddIncludeDir to many args expected 1");
                return;
            }

            XmlDocument doc = new XmlDocument();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");
            

            try {
                doc.Load(vsPath);
                XmlNodeList clCompileNodes = doc.SelectNodes("//msb:Project/msb:ItemDefinitionGroup/msb:ClCompile", nsmgr);
                foreach (XmlNode node in clCompileNodes) {
                    XmlNode additionalIncludesNode = node.SelectSingleNode("msb:AdditionalIncludeDirectories", nsmgr);
                    if (additionalIncludesNode != null) {
                        additionalIncludesNode.InnerText += $";{args[0]}";
                    }
                    else {
                        XmlElement newElement = doc.CreateElement("AdditionalIncludeDirectories", nsmgr.LookupNamespace("msb"));
                        newElement.InnerText = args[0];
                        node.AppendChild(newElement);
                    }
                }
                doc.Save(vsPath);
            }
            catch (Exception ex) {
                MessageBox.Show("Failed to modify project:" + ex.Message);
            }
        }

        private void AddLibraryDir() {
            if (args.Count() != 1) {
                Console.WriteLine("Error in AddLibraryDir to many args expected 1");
                return;
            }

            XmlDocument doc = new XmlDocument();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");


            try {
                doc.Load(vsPath);
                XmlNodeList linkNodes = doc.SelectNodes("//msb:Project/msb:ItemDefinitionGroup/msb:Link", nsmgr);
                foreach (XmlNode node in linkNodes) {
                    XmlNode additionalLibDirsNode = node.SelectSingleNode("msb:AdditionalLibraryDirectories", nsmgr);
                    if (additionalLibDirsNode != null) {
                        additionalLibDirsNode.InnerText += $";{args[0]}";
                    }
                    else {
                        XmlElement newElement = doc.CreateElement("AdditionalLibraryDirectories", nsmgr.LookupNamespace("msb"));
                        newElement.InnerText = args[0];
                        node.AppendChild(newElement);
                    }
                }
                doc.Save(vsPath);
            }
            catch (Exception ex) {
                MessageBox.Show("Failed to modify project: " + ex.Message);
            }
        }

        private void AddDependencies() {
            string combinedLibs = "";
            foreach (string elem in args) { 
                combinedLibs += elem + ";";
            }

            XmlDocument doc = new XmlDocument();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("msb", "http://schemas.microsoft.com/developer/msbuild/2003");


            try {
                doc.Load(vsPath);

                XmlNodeList linkNodes = doc.SelectNodes("//msb:Project/msb:ItemDefinitionGroup/msb:Link", nsmgr);
                foreach (XmlNode node in linkNodes) {
                    XmlNode additionalDepsNode = node.SelectSingleNode("msb:AdditionalDependencies", nsmgr);
                    if (additionalDepsNode != null) {
                        additionalDepsNode.InnerText += $";{combinedLibs}";
                    }
                    else {
                        XmlElement newElement = doc.CreateElement("AdditionalDependencies", nsmgr.LookupNamespace("msb"));
                        newElement.InnerText = combinedLibs;
                        node.AppendChild(newElement);
                    }
                }
                doc.Save(vsPath);
            }
            catch (Exception ex) {
                MessageBox.Show("Failed to modify project: " + ex.Message);
            }
        }

        public void Run(){
            if (ready) {
                String[] config = File.ReadAllLines(this.configPath);
                foreach (string line in config) {
                    string parsedLine = "";
                    string arg = "";
                    bool collectArg = false;
                    if (line.Contains('(')) {
                        foreach (char elem in line) {
                            if (collectArg) {
                                if (elem == ')') {
                                    break;
                                }
                                if (elem == ',') {
                                    args.Add(arg);
                                    arg = "";
                                    continue;
                                }
                                if (elem == ' ') {
                                    continue;
                                }
                                arg += elem;

                            }
                            else {
                                if (elem == '(') {
                                    collectArg = true;
                                    continue;
                                }
                                parsedLine += elem;
                            }
                        }
                        if(arg != "") {
                            args.Add(arg);
                        }
                    }
                    else { 
                        parsedLine = line;
                    }

                    Console.WriteLine(parsedLine);
                    foreach (string elem in args) { 
                        Console.WriteLine(elem);
                    }


                    if (Commands.ContainsKey(parsedLine)) {
                        fullLine = line;
                        Commands[parsedLine]();
                    }
                    else {
                        Console.WriteLine("Command Does not exist");
                    }

                    parsedLine = "";
                    arg = "";
                    collectArg = false;
                    args.Clear();
                }
            }
        }
    }
}
