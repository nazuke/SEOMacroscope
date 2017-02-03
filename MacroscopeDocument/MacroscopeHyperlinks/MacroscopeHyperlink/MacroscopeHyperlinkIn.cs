/*
	
	This file is part of SEOMacroscope.
	
	Copyright 2017 Jason Holland.
	
	The GitHub repository may be found at:
	
		https://github.com/nazuke/SEOMacroscope
	
	Foobar is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License as published by
	the Free Software Foundation, either version 3 of the License, or
	(at your option) any later version.
	
	Foobar is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License for more details.
	
	You should have received a copy of the GNU General Public License
	along with Foobar.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;

namespace SEOMacroscope
{
	
	/// <summary>
	/// Description of MacroscopeHyperlinkIn.
	/// </summary>
	
	public class MacroscopeHyperlinkIn : Macroscope
	{
	
		/**************************************************************************/

		int LinkId;

		MacroscopeConstants.HyperlinkType HyperlinkType;

		string Method;
		string UrlOrigin;
		string UrlTarget;
		string LinkText;
		string AltText;

		/**************************************************************************/
			
		public MacroscopeHyperlinkIn (
			int iLinkId,
			MacroscopeConstants.HyperlinkType hlLinkType,
			string sMethod,
			string sUrlOrigin,
			string sUrlTarget,
			string sLinkText,
			string sAltText
		)
		{
			LinkId = iLinkId;
			HyperlinkType = hlLinkType;
			Method = sMethod;
			UrlOrigin = sUrlOrigin;
			UrlTarget = sUrlTarget;
			LinkText = sLinkText;
			AltText = sAltText;
		}
	
		/**************************************************************************/

		public int GetLinkId ()
		{
			return( this.LinkId );
		}
		
		/**************************************************************************/
	 
		public MacroscopeConstants.HyperlinkType GetHyperlinkType ()
		{
			return( this.HyperlinkType );
		}

		/**************************************************************************/

		public string GetUrlOrigin ()
		{
			return( this.UrlOrigin );
		}
		
		/**************************************************************************/

		public string GetUrlTarget ()
		{
			return( this.UrlTarget );
		}
		
		/**************************************************************************/

		public string GetLinkText ()
		{
			return( this.LinkText );
		}

		/**************************************************************************/

		public string GetAltText ()
		{
			return( this.AltText );
		}

		/**************************************************************************/
				
	}

}
