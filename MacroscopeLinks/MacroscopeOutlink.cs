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
  /// Description of MacroscopeOutlink.
  /// </summary>

  public class MacroscopeOutlink : Macroscope
  {

    /**************************************************************************/

    private MacroscopeConstants.InOutLinkType LinkType;

    private string AbsoluteUrl;
    
    private string SourceUrl;
    
    private Boolean DoFollow;

    private string Title;
    private string AltText;

    /**************************************************************************/

    public MacroscopeOutlink (
      string AbsoluteUrl,
      string SourceUrl,
      MacroscopeConstants.InOutLinkType LinkType,
      Boolean Follow
    )
    {
      this.AbsoluteUrl = AbsoluteUrl;
      this.SourceUrl = SourceUrl;
      this.LinkType = LinkType;
      this.DoFollow = Follow;
    }

    /** Absolute URL **********************************************************/

    public void SetAbsoluteUrl ( string AbsoluteUrl )
    {
      this.AbsoluteUrl = AbsoluteUrl;
    }

    public string GetAbsoluteUrl ()
    {
      return( this.AbsoluteUrl );
    }
    
    /** Source URL **********************************************************/

    public void SetSourceUrl ( string SourceUrl )
    {
      this.SourceUrl = SourceUrl;
    }

    public string GetSourceUrl ()
    {
      return( this.SourceUrl );
    }
    
    /** Link Type *************************************************************/

    public void SetLinkType ( MacroscopeConstants.InOutLinkType LinkType )
    {
      this.LinkType = LinkType;
    }

    public MacroscopeConstants.InOutLinkType GetLinkType ()
    {
      return( this.LinkType );
    }
    
    /** DoFollow **************************************************************/

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

    /** Title *****************************************************************/

    public void SetTitle ( string Title )
    {
      this.Title = Title;
    }

    public string GetTitle ()
    {
      return( this.Title );
    }

    /** AltText ***************************************************************/

    public void SetAltText ( string AltText )
    {
      this.AltText = AltText;
    }

    public string GetAltText ()
    {
      return( this.AltText );
    }

    /**************************************************************************/

  }

}
