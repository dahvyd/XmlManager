using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlManager {
	public interface IXmlHandler<DataType> {
		void StartDocument();
		void EndDocument();
		void StartElement(string nsURI, string localName, string qName, XmlAttributes attribs);
		void EndElement(string nsURI, string localName, string qName);
		void Text(string text);
		DataType Get();
	}
}
