using System;
using System.IO; // Directory.EnumerateFiles
using System.Collections.Generic; // IEnumerable<>
using System.Xml.Linq; // XDocument
using System.Xml; // XmlWriter 
using System.Text; // StringBuilder

namespace CSharpProjectNullabler
{
    class SetupNullableCommand : ICommand
    {
        public void Run(string[] args)
        {
            foreach (string path in GetProjectFiles(args)) {
                var xml = CreateNullableXDocument(path);
                using (var writer = CreateOmitXmlDeclarationWriter(path)) { xml.Save(writer); }
            }
        }
        private IEnumerable<string> GetProjectFiles(string[] args)
        {
            if (0 < args.Length && "-r" == args[0]) {
                return Directory.EnumerateFiles(System.Environment.CurrentDirectory, "*.csproj", SearchOption.AllDirectories);
            } else {
                return Directory.EnumerateFiles(System.Environment.CurrentDirectory, "*.csproj");
            }
        }
        private XmlWriter CreateOmitXmlDeclarationWriter(string path)
        {
            XmlWriterSettings xws = new XmlWriterSettings();  
            xws.OmitXmlDeclaration = true; // XML宣言を出力しない
            xws.Indent = true;  
            return XmlWriter.Create(path, xws);
        }
        private XDocument CreateNullableXDocument(string path)
        {
            XDocument xml = XDocument.Load(path);
            XElement project = xml.Element("Project");
            XElement propertyGroup = project.Element("PropertyGroup");
            XElement? nullable = propertyGroup.Element("Nullable");
            if (null == nullable) { propertyGroup.Add(new XElement("Nullable", "enable")); }
            return xml;
        }
    }
}
