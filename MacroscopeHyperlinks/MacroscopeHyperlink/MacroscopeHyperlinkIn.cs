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
    private string SourceUrl;
    private string TargetUrl;
    private Boolean DoFollow;
    private string LinkText;
    private string LinkTitle;
    private string AltText;

    /**************************************************************************/

    public MacroscopeHyperlinkIn (
      MacroscopeConstants.HyperlinkType LinkType,
      string Method,
      string SourceUrl,
      string TargetUrl,
      string LinkText, 
      string LinkTitle,
      string AltText
    )
    {

      this.LinkGuid = Guid.NewGuid();

      this.HyperlinkType = LinkType;
      this.Method = Method;
      this.SourceUrl = SourceUrl;
      this.TargetUrl = TargetUrl;
      this.LinkText = LinkText;
      this.LinkTitle = LinkTitle;
      this.AltText = AltText;

      this.DoFollow = true;

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

    public string GetSourceUrl ()
    {
      return( this.SourceUrl );
    }

    /**************************************************************************/

    public string GetTargetUrl ()
    {
      return( this.TargetUrl );
    }

    /**************************************************************************/

    public void SetDoFollow ()
    {
      this.DoFollow = true;
    }
    
    public void UnsetDoFollow ()
    {
      this.DoFollow = false;
    }

    public Boolean GetDoFollow ()
    {
      return( this.DoFollow );
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
