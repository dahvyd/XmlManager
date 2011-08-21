using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlManager {
	public abstract class XmlWriteable {
		protected abstract string XmlElementName();
		protected abstract IXmlWriteableAttribute[] XmlAttributes();
		protected abstract XmlWriteable[] XmlChildren();
		protected abstract string XmlText();

		public string ToXml() {
			return ToXml(0);
		}

		private string ToXml(int indents) {
			string elementname = XmlElementName();
			string text = XmlText();

			string xml = StartElementOpen(elementname, indents);

			IXmlWriteableAttribute[] attribs = XmlAttributes();
			if (attribs != null) {
				foreach (IXmlWriteableAttribute a in attribs) {
					if (a != null) {
						xml += AddAttribute(a.XmlName(), a.XmlValue());
					}
				}
			}
			XmlWriteable[] children = XmlChildren();
			if (children != null && children.Length > 0) {
				xml += EndElementOpen();
				indents++;
				foreach (XmlWriteable c in children) {
					if (c != null) {
						xml += c.ToXml(indents);
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
