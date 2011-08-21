using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlManager {
	public class XmlNode : XmlWriteable {
		public XmlNode(string name, XmlAttributes attributes, XmlNode parent, params XmlWriteable[] children) {
			Name = name;
			Attributes = attributes;
			Parent = parent;
			Children = children;
		}

		public void AddChild(XmlWriteable child) {
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

		public XmlWriteable[] Children {
			get {
				return _children != null ? _children.ToArray() : null;
			}
			private set {
				_children = value != null ? new List<XmlWriteable>(value) : new List<XmlWriteable>();
			}
		}
		private List<XmlWriteable> _children;

		public XmlNode Parent {
			get;
			private set;
		}

		#endregion

		#region XmlWriteable methods

		protected override string XmlElementName() {
			return Name;
		}

		protected override IXmlWriteableAttribute[] XmlAttributes() {
			return Attributes.ToArray();
		}

		protected override XmlWriteable[] XmlChildren() {
			return Children;
		}

		protected override string XmlText() {
			return Text;
		}

		#endregion
	}
}
