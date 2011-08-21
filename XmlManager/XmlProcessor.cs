using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace XmlManager {
	public static class XmlProcessor {
		public static string XmlDeclaration {
			get {
				return "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + Environment.NewLine;
			}
		}

		/// <summary>
		/// Reads all nodes in an XML file, and calls the methods from the supplied IXmlHandler derivative.
		/// A generic type parameter is required to specify which data type the IXmlHandler will be returning.
		/// This method will return that object as the return value.
		/// </summary>
		public static DataType ReadFromXml<DataType>(string path, IXmlHandler<DataType> handler) {
			XmlTextReader read = new XmlTextReader(new FileStream(path, FileMode.Open));
			while (read.Read()) {
				switch (read.NodeType) {
					case XmlNodeType.Document:
						handler.StartDocument();
						break;
					case XmlNodeType.Element:
						string nsURI = read.NamespaceURI;
						string lName = read.LocalName;
						string name = read.Name;
						XmlAttributes attribs = new XmlAttributes();
						while (read.MoveToNextAttribute()) {
							attribs.Add(read.Name, read.Value);
						}
						handler.StartElement(nsURI, lName, name, attribs);
						break;
					case XmlNodeType.EndElement:
						handler.EndElement(read.NamespaceURI, read.LocalName, read.Name);
						break;
					case XmlNodeType.Text:
						handler.Text(read.Value);
						break;
				}
			}
			handler.EndDocument();
			return handler.Get();
		}

		public static void Write(XmlWriteable root, string path) {
			if (File.Exists(path)) {
				File.Delete(path);
			}
			File.WriteAllText(path, XmlDeclaration);
			File.AppendAllText(path, root.ToXml());
		}
	}
}
