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
using System.Collections.Generic;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeHyperlinksIn.
  /// </summary>

  public class MacroscopeHyperlinksIn : Macroscope
  {

    /**************************************************************************/

    private List<MacroscopeHyperlinkIn> Links;

    /**************************************************************************/

    public MacroscopeHyperlinksIn ()
    {
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
      string UrlOrigin,
      string UrlTarget,
      string LinkText, 
      string LinkTitle,
      string AltText
    )
    {

      MacroscopeHyperlinkIn HyperlinkIn = new MacroscopeHyperlinkIn (
                                            LinkType: LinkType,
                                            Method: Method,
                                            UrlOrigin: UrlOrigin,
                                            UrlTarget: UrlTarget,
                                            LinkText: LinkText, 
                                            LinkTitle: LinkTitle,
                                            AltText: AltText
                                          );

      lock( this.Links )
      {
        this.Links.Add( HyperlinkIn );
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

    public Boolean ContainsLink ( string Url )
    {
      Boolean LinkPresent = false;
      lock( this.Links )
      {
        foreach( MacroscopeHyperlinkIn HyperlinkIn in this.Links )
        {
          if( HyperlinkIn.GetUrlTarget() == Url )
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

    public int Count ()
    {
      return( this.Links.Count );
    }

    /**************************************************************************/

  }

}
