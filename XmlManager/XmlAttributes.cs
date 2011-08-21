using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlManager {
	public class XmlAttributes : List<XmlAttribute> {

		public XmlAttributes() {
		}

		public void Add(string name, string value) {
			this.Add(new XmlAttribute(name, value));
		}

		public XmlAttribute this[string name] {
			get {
				foreach (XmlAttribute a in this) {
					if (a.Matches(name)) {
						return a;
					}
				}
				return null;
			}
			set {
				this[name] = new XmlAttribute(name, value.XmlValue());
			}
		}
	}
}
