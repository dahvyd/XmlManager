using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlManager {
	public class XmlAttribute : IXmlWriteableAttribute {
		private string _name, _value;
		
		public XmlAttribute(string name, string value) {
			_name = name;
			_value = value;
		}
		
		public string XmlName() {
			return _name;
		}
		
		public string XmlValue() {
			return _value;
		}
		
		public bool Matches(string name) {
			return (_name == name);
		}
	}
}
