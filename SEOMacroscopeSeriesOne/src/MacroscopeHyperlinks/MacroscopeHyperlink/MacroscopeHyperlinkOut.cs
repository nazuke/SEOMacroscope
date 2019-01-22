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
  /// Description of MacroscopeHyperlinkOut.
  /// </summary>

  [Serializable()]
  public class MacroscopeHyperlinkOut : Macroscope
  {

    // TODO: Write unit tests

    /**************************************************************************/

    private Guid LinkGuid;

    private MacroscopeConstants.HyperlinkType HyperlinkType;

    private string Method;

    private string TargetUrl;
    private string RawTargetUrl;
    
    private bool DoFollow;

    private string LinkTarget;

    private string AnchorText;
    private string Title;
    private string AltText;

    /**************************************************************************/

    public MacroscopeHyperlinkOut ()
    {
      this.LinkGuid = Guid.NewGuid();
      this.HyperlinkType = MacroscopeConstants.HyperlinkType.TEXT;
      this.DoFollow = true;
      this.LinkTarget = "";
      this.AnchorText = "";
      this.Title = "";
      this.AltText = "";
    }

    /**************************************************************************/

    public Guid GetGuid ()
    {
      return( this.LinkGuid );
    }

    /**************************************************************************/

    public void SetHyperlinkType ( MacroscopeConstants.HyperlinkType LinkType )
    {
      this.HyperlinkType = LinkType;
    }

    /** -------------------------------------------------------------------- **/

    public MacroscopeConstants.HyperlinkType GetHyperlinkType ()
    {
      return( this.HyperlinkType );
    }

    /**************************************************************************/

    public void SetMethod ( string Method )
    {
      this.Method = Method;
    }

    /** -------------------------------------------------------------------- **/

    public string GetMethod ()
    {
      return( this.Method );
    }

    /**************************************************************************/

    public void SetTargetUrl ( string Url )
    {
      if( MacroscopePreferencesManager.GetDowncaseLinks() )
      {
        string DowncasedUrl = MacroscopeHttpUrlUtils.DowncaseUrl( Url: Url );
        if( DowncasedUrl != null )
        {
          this.TargetUrl = DowncasedUrl;
        }
        else
        {
          this.TargetUrl = Url;
        }
      }
      else
      {
        this.TargetUrl = Url;
      }
    }

    /** -------------------------------------------------------------------- **/

    public string GetTargetUrl ()
    {
      return( this.TargetUrl );
    }

    /**************************************************************************/

    public void SetDoFollow ()
    {
      this.DoFollow = true;
    }

    /** -------------------------------------------------------------------- **/

    public void UnsetDoFollow ()
    {
      this.DoFollow = false;
    }

    /** -------------------------------------------------------------------- **/

    public bool GetDoFollow ()
    {
      return( this.DoFollow );
    }

    /** Target URL ************************************************************/

    public void SetLinkTarget ( string Text )
    {
      this.LinkTarget = Text;
    }

    /** -------------------------------------------------------------------- **/

    public string GetLinkTarget ()
    {
      string Target = this.LinkTarget;
      if( string.IsNullOrEmpty( Target ) )
      {
        Target = "";
      }
      return( Target );
    }

    /** Raw Target URL ********************************************************/

    public void SetRawTargetUrl ( string TargetUrl )
    {
      this.RawTargetUrl = TargetUrl;
    }

    /** -------------------------------------------------------------------- **/

    public string GetRawTargetUrl ()
    {
      return( this.RawTargetUrl );
    }

    /**************************************************************************/

    public void SetAnchorText ( string Text )
    {
      try
      {
        this.AnchorText = MacroscopeStringTools.CompactWhiteSpace( Text: Text );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        this.AnchorText = Text;
      }
    }

    /** -------------------------------------------------------------------- **/

    public string GetAnchorText ()
    {
      return( this.AnchorText );
    }

    /**************************************************************************/

    public void SetTitle ( string Text )
    {
      try
      {
        this.Title = MacroscopeStringTools.CompactWhiteSpace( Text: Text );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        this.Title = Text;
      }
    }

    /** -------------------------------------------------------------------- **/

    public string GetTitle ()
    {
      return( this.Title );
    }

    /**************************************************************************/

    public void SetAltText ( string Text )
    {
      try
      {
        this.AltText = MacroscopeStringTools.CompactWhiteSpace( Text: Text );
      }
      catch( Exception ex )
      {
        this.DebugMsg( ex.Message );
        this.AltText = Text;
      }
    }

    /** -------------------------------------------------------------------- **/

    public string GetAltText ()
    {
      return( this.AltText );
    }
    
    /**************************************************************************/

  }

}
