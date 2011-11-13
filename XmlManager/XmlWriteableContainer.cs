using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlManager {
	/// <summary>
	/// Represents a generic container to hold XML nodes. Can be used where no internal data structures can logically hold
	/// related nodes that should be grouped together in XML.
	/// </summary>
	public class XmlWriteableContainer : IXmlWriteable {
		public XmlWriteableContainer(string name, params IXmlWriteable[] children) {
			Name = name;
			Children = new List<IXmlWriteable>(children);
		}

		private List<IXmlWriteable> Children {
			get;
			set;
		}

		public void Add(IXmlWriteable node) {
			Children.Add(node);
		}

		private string Name {
			get;
			set;
		}

		#region XmlWriteable methods

		public IXmlWriteableAttribute[] XmlAttributes() {
			return null;
		}

		public IXmlWriteable[] XmlChildren() {
			return Children.ToArray();
		}

		public string XmlText() {
			return "";
		}

		public string XmlElementName() {
			return Name;
		}

		#endregion
	}
}
