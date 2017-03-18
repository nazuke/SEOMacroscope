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
  /// Description of MacroscopeLink.
  /// </summary>

  public class MacroscopeLink : Macroscope
  {

    /**************************************************************************/

    private MacroscopeConstants.InOutLinkType LinkType;

    private string SourceUrl;
    private string TargetUrl;
    
    private Boolean DoFollow;

    private string Title;
    private string AltText;

    /**************************************************************************/

    public MacroscopeLink (
      string SourceUrl,
      string TargetUrl,
      MacroscopeConstants.InOutLinkType LinkType,
      Boolean Follow
    )
    {
      this.SourceUrl = SourceUrl;     
      this.TargetUrl = TargetUrl;      
      this.LinkType = LinkType;
      this.DoFollow = Follow;
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
    
    /** Target URL **********************************************************/

    public void SetTargetUrl ( string TargetUrl )
    {
      this.TargetUrl = TargetUrl;
    }

    public string GetTargetUrl ()
    {
      return( this.TargetUrl );
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
