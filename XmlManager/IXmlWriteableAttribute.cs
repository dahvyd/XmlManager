using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlManager {
	public interface IXmlWriteableAttribute {
		string XmlName();
		string XmlValue();
	}
}
