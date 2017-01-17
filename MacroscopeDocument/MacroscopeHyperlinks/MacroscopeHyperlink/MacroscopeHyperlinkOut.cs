﻿/*
	
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
	/// Description of MacroscopeHyperlinkOut.
	/// </summary>
	
	public class MacroscopeHyperlinkOut : Macroscope
	{
	
		/**************************************************************************/

		Guid LinkGuid;

		MacroscopeHyperlinkClass LinkClass;

		string Method;

		string UrlOrigin;
		string UrlTarget;
		string LinkText;
		string AltText;

		Boolean Follow;
		
		/**************************************************************************/

		public MacroscopeHyperlinkOut ()
		{
			LinkGuid = Guid.NewGuid();
			LinkClass = new MacroscopeHyperlinkClass ( "TEXT" );
			Follow = true;
		}
	
		/**************************************************************************/

		public string GetGuid ()
		{
			return( this.LinkGuid.ToString() );
		}

		/**************************************************************************/
		
		public string GetLinkClass ()
		{
			return( this.LinkClass.Get() );
		}

		/**************************************************************************/

		public void SetMethod ( string sMethod )
		{
			this.Method = sMethod;
		}

		public string GetMethod ()
		{
			return( this.Method );
		}
		
		/**************************************************************************/

		public void SetUrlOrigin ( string sUrl )
		{
			this.UrlOrigin = sUrl;
		}

		public string GetUrlOrigin ()
		{
			return( this.UrlOrigin );
		}

		/**************************************************************************/
				
		public void SetUrlTarget ( string sUrl )
		{
			this.UrlTarget = sUrl;
		}

		public string GetUrlTarget ()
		{
			return( this.UrlTarget );
		}
				
		/**************************************************************************/

		public void SetLinkText ( string sText )
		{
			this.LinkText = sText;
		}

		public string GetLinkText ()
		{
			return( this.LinkText );
		}
				
		/**************************************************************************/

		public void SetAltText ( string sText )
		{
			this.AltText = sText;
		}

		public string GetAltText ()
		{
			return( this.AltText );
		}

		/**************************************************************************/

		public void SetFollow ( Boolean bFollow )
		{
			this.Follow = bFollow;
		}

		public Boolean GetFollow ()
		{
			return( this.Follow );
		}

		/**************************************************************************/

	}

}
