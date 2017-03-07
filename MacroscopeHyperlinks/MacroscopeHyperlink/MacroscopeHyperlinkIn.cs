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

    private Guid LinkGuid;

    private MacroscopeConstants.HyperlinkType HyperlinkType;

    private string Method;
    private string UrlOrigin;
    private string UrlTarget;
    private string LinkText;
    private string LinkTitle;
    private string AltText;

    /**************************************************************************/

    public MacroscopeHyperlinkIn (
      MacroscopeConstants.HyperlinkType LinkType,
      string Method,
      string UrlOrigin,
      string UrlTarget,
      string LinkText, 
      string LinkTitle,
      string AltText
    )
    {

      this.LinkGuid = Guid.NewGuid();

      this.HyperlinkType = LinkType;
      this.Method = Method;
      this.UrlOrigin = UrlOrigin;
      this.UrlTarget = UrlTarget;
      this.LinkText = LinkText;
      this.LinkTitle = LinkTitle;
      this.AltText = AltText;

    }

    /**************************************************************************/

    public Guid GetLinkGuid ()
    {
      return( this.LinkGuid );
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

    public string GetLinkTitle ()
    {
      return( this.LinkTitle );
    }
    
    /**************************************************************************/

    public string GetAltText ()
    {
      return( this.AltText );
    }

    /**************************************************************************/

  }

}
