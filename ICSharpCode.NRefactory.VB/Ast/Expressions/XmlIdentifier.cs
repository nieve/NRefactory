﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using System;

namespace ICSharpCode.NRefactory.VB.Ast
{
	/// <summary>
	/// Description of XmlIdentifier.
	/// </summary>
	public class XmlIdentifier : AstNode
	{
		public static readonly new XmlIdentifier Null = new NullXmlIdentifier();
		
		class NullXmlIdentifier : XmlIdentifier
		{
			public override bool IsNull {
				get {
					return true;
				}
			}
			
			public override S AcceptVisitor<T, S> (IAstVisitor<T, S> visitor, T data)
			{
				return default(S);
			}
			
			protected internal override bool DoMatch(AstNode other, PatternMatching.Match match)
			{
				return other == null || other.IsNull;
			}
		}
		
		public string Name { get; set; }
		
		AstLocation startLocation;
		public override AstLocation StartLocation {
			get { return startLocation; }
		}
		
		public override AstLocation EndLocation {
			get { return new AstLocation(startLocation.Line, startLocation.Column + Name.Length); }
		}
		
		private XmlIdentifier()
		{
			this.Name = string.Empty;
		}
		
		public XmlIdentifier(string name, AstLocation startLocation)
		{
			this.Name = name;
			this.startLocation = startLocation;
		}
		
		protected internal override bool DoMatch(AstNode other, ICSharpCode.NRefactory.PatternMatching.Match match)
		{
			var ident = other as XmlIdentifier;
			return ident != null
				&& MatchString(Name, ident.Name)
				&& ident.startLocation == startLocation;
		}
		
		public override S AcceptVisitor<T, S>(IAstVisitor<T, S> visitor, T data)
		{
			return visitor.VisitXmlIdentifier(this, data);
		}
	}
}
