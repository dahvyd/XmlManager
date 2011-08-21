using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlManager {
	/// <summary>
	/// Represents a generic container to hold XML nodes. Can be used where no internal data structures can logically hold
	/// related nodes that should be grouped together in XML.
	/// </summary>
	public class XmlWriteableContainer : XmlWriteable {
		public XmlWriteableContainer(string name, params XmlWriteable[] children) {
			Name = name;
			Children = new List<XmlWriteable>(children);
		}

		public List<XmlWriteable> Children {
			get;
			private set;
		}

		public void Add(XmlWriteable node) {
			Children.Add(node);
		}

		private string Name {
			get;
			set;
		}

		#region XmlWriteable methods

		protected override IXmlWriteableAttribute[] XmlAttributes() {
			return null;
		}

		protected override XmlWriteable[] XmlChildren() {
			return Children.ToArray();
		}

		protected override string XmlText() {
			return "";
		}

		protected override string XmlElementName() {
			return Name;
		}

		#endregion
	}
}
