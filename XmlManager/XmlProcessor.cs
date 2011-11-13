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

		public static void Write(IXmlWriteable root, string path) {
			if (File.Exists(path)) {
				File.Delete(path);
			}
			File.WriteAllText(path, XmlDeclaration);
			string xml = ToXml(root);
			File.AppendAllText(path, xml);
		}
		
		public static string ToXml(IXmlWriteable root) {
			return ToXml(root, 0);
		}

		private static string ToXml(IXmlWriteable element, int indents) {
			string elementname = element.XmlElementName();
			string text = element.XmlText();
			
			string xml = StartElementOpen(elementname, indents);
			
			IXmlWriteableAttribute[] attribs = element.XmlAttributes();
			if (attribs != null) {
				foreach (IXmlWriteableAttribute a in attribs) {
					if (a != null) {
						xml += AddAttribute(a.XmlName(), a.XmlValue());
					}
				}
			}
			IXmlWriteable[] children = element.XmlChildren();
			if (children != null && children.Length > 0) {
				xml += EndElementOpen();
				indents++;
				foreach (IXmlWriteable c in children) {
					if (c != null) {
						xml += ToXml(c, indents);
					}
				}
				indents--;
				xml += CloseTag(elementname, indents);
			} else if (text != "") {
				xml += AddText(text, elementname);
			} else {
				xml += ShortCloseTag();
			}
			return xml;
		}

		private static string CloseTag(string name, int indents) {
			return Indent(indents) + "</" + name + ">" + Environment.NewLine;
		}

		private static string ShortCloseTag() {
			return " />" + Environment.NewLine;
		}

		private static string AddAttribute(string name, string value) {
			return " " + name + "=\"" + value + "\"";
		}

		private static string StartElementOpen(string name, int indents) {
			return Indent(indents) + "<" + name;
		}

		private static string EndElementOpen() {
			return ">" + Environment.NewLine;
		}

		private static string AddText(string text, string elementname) {
			return ">" + text + CloseTag(elementname, 0);
		}

		private static string Indent(int indents) {
			string s = "";
			for (int i = 0; i < indents; i++) {
				s += '\t';
			}
			return s;
		}
	}
}
