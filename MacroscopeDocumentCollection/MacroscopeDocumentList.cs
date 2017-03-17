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
  /// Description of MacroscopeDocumentList.
  /// </summary>

  public class MacroscopeDocumentList : Macroscope
  {
    /**************************************************************************/

    private Dictionary<string,MacroscopeDocument> DocumentList;

    /**************************************************************************/

    public MacroscopeDocumentList ()
    {
      this.DocumentList = new Dictionary<string,MacroscopeDocument> ( 16 );
    }

    /**************************************************************************/
    
    public void AddDocument ( MacroscopeDocument msDoc )
    {

      lock( this.DocumentList )
      {

        if( this.DocumentList.ContainsKey( msDoc.GetUrl() ) )
        {
          this.DocumentList.Remove( msDoc.GetUrl() );
        }

        this.DocumentList.Add( msDoc.GetUrl(), msDoc );

      }

    }

    /**************************************************************************/

    public void RemoveDocument ( MacroscopeDocument msDoc )
    {

      lock( this.DocumentList )
      {

        if( this.DocumentList.ContainsKey( msDoc.GetUrl() ) )
        {
          this.DocumentList.Remove( msDoc.GetUrl() );
        }

      }

    }

    /**************************************************************************/
    
    public MacroscopeDocument GetDocument ( string Url )
    {

      MacroscopeDocument msDoc = null;
      
      lock( this.DocumentList )
      {

        if( this.DocumentList.ContainsKey( Url ) )
        {
          msDoc = this.DocumentList[ Url ];
        }

      }
      
      return( msDoc );

    }
    
    /**************************************************************************/

    public IEnumerable<MacroscopeDocument> IterateDocuments ()
    {

      lock( this.DocumentList )
      {

        foreach( string Url in this.DocumentList.Keys )
        {
          yield return this.DocumentList[ Url ];
        }

      }

    }

    /**************************************************************************/

  }

}
