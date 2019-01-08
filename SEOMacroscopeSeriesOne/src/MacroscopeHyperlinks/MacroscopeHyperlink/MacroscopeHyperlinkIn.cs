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
  /// Description of MacroscopeHyperlinkIn.
  /// </summary>

  [Serializable()]
  public class MacroscopeHyperlinkIn : Macroscope
  {

    // TODO: Write unit tests

    /**************************************************************************/

    private Guid LinkGuid;

    private MacroscopeConstants.HyperlinkType HyperlinkType;

    private string Method;
    
    private string SourceUrl;
    private string TargetUrl;
    
    private bool DoFollow;
    
    private string LinkText;
    private string LinkTitle;
    private string AltText;

    private string RawSourceUrl;
    private string RawTargetUrl;
    
    /**************************************************************************/

    public MacroscopeHyperlinkIn (
      MacroscopeConstants.HyperlinkType LinkType,
      string Method,
      string SourceUrl,
      string TargetUrl,
      string LinkText, 
      string LinkTitle,
      string AltText,
      Guid ExtantGuid
    )
    {
      this.LinkGuid = ExtantGuid;
      this.HyperlinkType = LinkType;
      this.Method = Method;
      this.SourceUrl = SourceUrl;
      this.TargetUrl = TargetUrl;
      this.LinkText = LinkText;
      this.LinkTitle = LinkTitle;
      this.AltText = AltText;
      this.DoFollow = true;
      this.RawSourceUrl = "";
      this.RawTargetUrl = "";
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

    public bool GetDoFollow ()
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
