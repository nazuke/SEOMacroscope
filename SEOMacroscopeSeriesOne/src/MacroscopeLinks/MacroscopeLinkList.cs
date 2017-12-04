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
  /// Description of MacroscopeLinkList.
  /// </summary>

  public class MacroscopeLinkList : Macroscope
  {

    /**************************************************************************/

    private List<MacroscopeLink> Links;

    /**************************************************************************/

    public MacroscopeLinkList ()
    {

      this.SuppressDebugMsg = true;
      
      this.Links = new List<MacroscopeLink> ( 256 );

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

    public void Add ( MacroscopeLink Link )
    {
      
      lock( this.Links )
      {

        if( !this.ContainsLink( Link: Link ) )
        {
          this.Links.Add( Link );
        }
        
      }
      
    }

    /**************************************************************************/

    public void Remove ( MacroscopeLink Link )
    {
      lock( this.Links )
      {
        foreach( MacroscopeLink LinkOld in this.Links )
        {
          if( LinkOld.Equals( Link ) )
          {
            this.Links.Remove( LinkOld );
          }
        }
      }
    }

    /**************************************************************************/

    public Boolean ContainsLink ( MacroscopeLink Link )
    {

      Boolean LinkPresent = false;

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

    public Boolean ContainsSourceLink ( string Url )
    {
      Boolean LinkPresent = false;
      lock( this.Links )
      {
        foreach( MacroscopeLink Link in this.Links )
        {
          if( Link.GetSourceUrl() == Url )
          {
            LinkPresent = true;
          }
        }
      }
      return( LinkPresent );
    }

    /**************************************************************************/
    
    public Boolean ContainsTargetLink ( string Url )
    {
      Boolean LinkPresent = false;
      lock( this.Links )
      {
        foreach( MacroscopeLink Link in this.Links )
        {
          if( Link.GetTargetUrl() == Url )
          {
            LinkPresent = true;
          }
        }
      }
      return( LinkPresent );
    }

    /**************************************************************************/

    public IEnumerable<MacroscopeLink> IterateLinks ()
    {
      lock( this.Links )
      {
        foreach( MacroscopeLink Link in this.Links )
        {
          yield return Link;
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
