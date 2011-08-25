using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlManager {
	public class XmlNode : IXmlWriteable {
		public XmlNode(string name, XmlAttributes attributes, XmlNode parent, params IXmlWriteable[] children) {
			Name = name;
			Attributes = attributes;
			Parent = parent;
			Children = children;
		}

		public void AddChild(IXmlWriteable child) {
			_children.Add(child);
		}

		#region properties

		public string Name {
			get;
			private set;
		}

		public string Text {
			get;
			set;
		}

		public XmlAttributes Attributes {
			get;
			private set;
		}

		public IXmlWriteable[] Children {
			get {
				return _children != null ? _children.ToArray() : null;
			}
			private set {
				_children = value != null ? new List<IXmlWriteable>(value) : new List<IXmlWriteable>();
			}
		}
		private List<IXmlWriteable> _children;

		public XmlNode Parent {
			get;
			private set;
		}

		#endregion

		#region XmlWriteable methods

		public string XmlElementName() {
			return Name;
		}

		public IXmlWriteableAttribute[] XmlAttributes() {
			return Attributes.ToArray();
		}

		public IXmlWriteable[] XmlChildren() {
			return Children;
		}

		public string XmlText() {
			return Text;
		}

		#endregion
	}
}
