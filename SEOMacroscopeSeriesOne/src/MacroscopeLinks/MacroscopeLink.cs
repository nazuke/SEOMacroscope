/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

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

  [Serializable()]
  public class MacroscopeLink : Macroscope
  {
    
    // TODO: Write unit tests

    /**************************************************************************/
    
    private Guid LinkGuid;
    
    private MacroscopeConstants.InOutLinkType LinkType;

    private string SourceUrl;
    private string TargetUrl;
    
    private bool DoFollow;

    private string Title;
    private string AltText;

    private string RawSourceUrl;
    private string RawTargetUrl;

    /**************************************************************************/

    public MacroscopeLink (
      string SourceUrl,
      string TargetUrl,
      MacroscopeConstants.InOutLinkType LinkType,
      bool Follow
    )
    {
      
      this.LinkGuid = Guid.NewGuid();
            
      this.LinkType = LinkType;

      this.SourceUrl = SourceUrl;
      this.TargetUrl = TargetUrl;      

      this.DoFollow = Follow;

      this.RawSourceUrl = SourceUrl;
      this.RawTargetUrl = TargetUrl;

    }

    /** GUID ******************************************************************/
    
    public Guid GetLinkGuid ()
    {
      return( this.LinkGuid );
    }
    
    /** Source URL ************************************************************/

    public void SetSourceUrl ( string SourceUrl )
    {
      this.SourceUrl = SourceUrl;
    }

    public string GetSourceUrl ()
    {
      return( this.SourceUrl );
    }
    
    /** Target URL ************************************************************/

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

    public bool GetDoFollow ()
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

    /** Raw Source URL ********************************************************/

    public void SetRawSourceUrl ( string SourceUrl )
    {
      this.RawSourceUrl = SourceUrl;
    }

    public string GetRawSourceUrl ()
    {
      return( this.RawSourceUrl );
    }
    
    /** Raw Target URL ********************************************************/

    public void SetRawTargetUrl ( string TargetUrl )
    {
      this.RawTargetUrl = TargetUrl;
    }

    public string GetRawTargetUrl ()
    {
      return( this.RawTargetUrl );
    }

    /**************************************************************************/

  }

}
