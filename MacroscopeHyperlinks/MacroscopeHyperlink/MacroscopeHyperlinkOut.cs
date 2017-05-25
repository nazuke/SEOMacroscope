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
  /// Description of MacroscopeHyperlinkOut.
  /// </summary>

  public class MacroscopeHyperlinkOut : Macroscope
  {

    /**************************************************************************/

    private Guid LinkGuid;

    private MacroscopeConstants.HyperlinkType HyperlinkType;

    private string Method;

    private string TargetUrl;

    private Boolean DoFollow;

    private string LinkTarget;

    private string LinkText;
    private string LinkTitle;
    private string AltText;

    /**************************************************************************/

    public MacroscopeHyperlinkOut ()
    {
      this.LinkGuid = Guid.NewGuid();
      this.HyperlinkType = MacroscopeConstants.HyperlinkType.TEXT;
      this.DoFollow = true;
      this.LinkTarget = "";   
      this.LinkText = "";
      this.LinkTitle = "";
      this.AltText = "";
    }

    /**************************************************************************/

    public string GetGuid ()
    {
      return( this.LinkGuid.ToString() );
    }

    /**************************************************************************/

    public void SetHyperlinkType ( MacroscopeConstants.HyperlinkType LinkType )
    {
      this.HyperlinkType = LinkType;
    }

    public MacroscopeConstants.HyperlinkType GetHyperlinkType ()
    {
      return( this.HyperlinkType );
    }

    /**************************************************************************/

    public void SetMethod ( string Method )
    {
      this.Method = Method;
    }

    public string GetMethod ()
    {
      return( this.Method );
    }

    /**************************************************************************/

    public void SetTargetUrl ( string Url )
    {
      this.TargetUrl = Url;
    }

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

    public void SetLinkTarget ( string Text )
    {
      this.LinkTarget = Text;
    }

    public string GetLinkTarget ()
    {

      string Target = this.LinkTarget;

      if( string.IsNullOrEmpty( Target ) )
      {
        Target = "";
      }

      return( Target );

    }

    /**************************************************************************/

    public void SetLinkText ( string Text )
    {
      this.LinkText = Text;
    }

    public string GetLinkText ()
    {
      return( this.LinkText );
    }

    /**************************************************************************/

    public void SetLinkTitle ( string Text )
    {
      this.LinkTitle = Text;
    }

    public string GetLinkTitle ()
    {
      return( this.LinkTitle );
    }

    /**************************************************************************/

    public void SetAltText ( string Text )
    {
      this.AltText = Text;
    }

    public string GetAltText ()
    {
      return( this.AltText );
    }

    /**************************************************************************/

  }

}
