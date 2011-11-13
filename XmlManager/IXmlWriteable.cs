using System;
namespace XmlManager {
	public interface IXmlWriteable {
		string XmlElementName();
		IXmlWriteableAttribute[] XmlAttributes();
		IXmlWriteable[] XmlChildren();
		string XmlText();
	}
}
