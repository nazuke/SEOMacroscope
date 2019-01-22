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
using System.Collections.Generic;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeHyperlinksIn.
  /// </summary>

  [Serializable()]
  public class MacroscopeHyperlinksIn : Macroscope
  {

    // TODO: Write unit tests

    /**************************************************************************/

    private List<MacroscopeHyperlinkIn> Links;

    /**************************************************************************/

    public MacroscopeHyperlinksIn ()
    {
      this.SuppressDebugMsg = true;
      this.Links = new List<MacroscopeHyperlinkIn> ( 256 );
    }

    /**************************************************************************/

    public void Clear ()
    {
      lock( this.Links )
      {
        this.Links.Clear();
      }
    }

    /**************************************************************************/

    public MacroscopeHyperlinkIn Add (
      MacroscopeConstants.HyperlinkType LinkType,
      string Method,
      string SourceUrl,
      string TargetUrl,
      string AnchorText, 
      string Title,
      string AltText,
      Guid ExtantGuid
    )
    {

      MacroscopeHyperlinkIn HyperlinkIn;

      if( this.ContainsGuid( ExtantGuid: ExtantGuid ) )
      {
        HyperlinkIn = this.GetLinkByGuid( ExtantGuid: ExtantGuid );
      }
      else
      {

        HyperlinkIn = new MacroscopeHyperlinkIn (
          LinkType: LinkType,
          Method: Method,
          SourceUrl: SourceUrl,
          TargetUrl: TargetUrl,
          AnchorText: AnchorText, 
          Title: Title,
          AltText: AltText,
          ExtantGuid: ExtantGuid
        );

        lock( this.Links )
        {
          this.Links.Add( HyperlinkIn );
        }

      }
      
      return( HyperlinkIn );
      
    }

    /**************************************************************************/

    public void Remove ( MacroscopeHyperlinkIn HyperlinkIn )
    {
      lock( this.Links )
      {
        foreach( MacroscopeHyperlinkIn HyperlinkInOld in this.Links )
        {
          if( HyperlinkInOld.Equals( HyperlinkIn ) )
          {
            this.Links.Remove( HyperlinkInOld );
          }
        }
      }
    }

    /**************************************************************************/

    private bool ContainsGuid ( Guid ExtantGuid )
    {
      bool LinkPresent = false;
      lock( this.Links )
      {
        foreach( MacroscopeHyperlinkIn HyperlinkIn in this.Links )
        {
          if( HyperlinkIn.GetLinkGuid() == ExtantGuid )
          {
            LinkPresent = true;
          }
        }
      }
      return( LinkPresent );
    }
    
    /**************************************************************************/

    public bool ContainsLink ( MacroscopeHyperlinkIn Link )
    {
      bool LinkPresent = false;
      lock( this.Links )
      {
        if( this.Links.Contains( Link ) )
        {
          LinkPresent = true;
        }
      }
      return( LinkPresent );
    }

    /**************************************************************************/

    public bool ContainsHyperlinkIn ( string Url )
    {
      bool LinkPresent = false;
      lock( this.Links )
      {
        foreach( MacroscopeHyperlinkIn HyperlinkIn in this.Links )
        {
          if( HyperlinkIn.GetTargetUrl() == Url )
          {
            LinkPresent = true;
          }
        }
      }
      return( LinkPresent );
    }

    /**************************************************************************/

    public IEnumerable<MacroscopeHyperlinkIn> IterateLinks ()
    {
      lock( this.Links )
      {
        foreach( MacroscopeHyperlinkIn HyperlinkIn in this.Links )
        {
          yield return HyperlinkIn;
        }
      }
    }

    /**************************************************************************/

    private MacroscopeHyperlinkIn GetLinkByGuid ( Guid ExtantGuid )
    {
      MacroscopeHyperlinkIn Link = null;
      lock( this.Links )
      {
        foreach( MacroscopeHyperlinkIn HyperlinkIn in this.Links )
        {
          if( HyperlinkIn.GetLinkGuid() == ExtantGuid )
          {
            Link = HyperlinkIn;
            break;
          }
        }
      }
      return( Link );
    }

    /**************************************************************************/

    public int Count ()
    {
      return( this.Links.Count );
    }

    /**************************************************************************/

  }

}
