/*

  This file is part of SEOMacroscope.

  Copyright 2019 Jason Holland.

  The GitHub repository may be found at:

    https://github.com/nazuke/SEOMacroscope

  SEOMacroscope is free software: you can redistribute it and/or modify
  it under the terms of the GNU General Public License as published by
  the Free Software Foundation, either version 3 of the License, or
  (at your option) any later version.

  SEOMacroscope is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with SEOMacroscope.  If not, see <http://www.gnu.org/licenses/>.

*/

using System;
using System.Collections.Generic;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDocumentList.
  /// </summary>

  [Serializable()]
  public class MacroscopeDocumentList : Macroscope
  {
    /**************************************************************************/

    private Dictionary<string, MacroscopeDocument> DocumentList;
    private Dictionary<string, string> DocumentNote;

    /**************************************************************************/

    public MacroscopeDocumentList ()
    {
      this.DocumentList = new Dictionary<string, MacroscopeDocument>( 64 );
      this.DocumentNote = new Dictionary<string, string>( 64 ); // TODO: Possibly expand this to accept a list of notes
    }

    /**************************************************************************/

    public void AddDocument ( MacroscopeDocument msDoc )
    {
      string Url = msDoc.GetUrl();
      lock ( this.DocumentList )
      {
        if ( this.DocumentList.ContainsKey( Url ) )
        {
          this.DocumentList.Remove( Url );
        }
        this.DocumentList.Add( Url, msDoc );
        lock ( this.DocumentNote )
        {
          if ( this.DocumentNote.ContainsKey( Url ) )
          {
            this.DocumentNote.Remove( Url );
          }
          this.DocumentNote.Add( Url, null );
        }
      }
    }

    /**************************************************************************/

    public void RemoveDocument ( MacroscopeDocument msDoc )
    {
      string Url = msDoc.GetUrl();
      lock ( this.DocumentList )
      {
        if ( this.DocumentList.ContainsKey( Url ) )
        {
          this.DocumentList.Remove( Url );
        }
        lock ( this.DocumentNote )
        {
          if ( this.DocumentNote.ContainsKey( Url ) )
          {
            this.DocumentNote.Remove( Url );
          }
        }
      }
    }

    /**************************************************************************/

    public MacroscopeDocument GetDocument ( string Url )
    {
      MacroscopeDocument msDoc = null;
      if ( this.DocumentList.ContainsKey( Url ) )
      {
        msDoc = this.DocumentList[ Url ];
      }
      return ( msDoc );
    }

    /**************************************************************************/

    public IEnumerable<MacroscopeDocument> IterateDocuments ()
    {
      lock ( this.DocumentList )
      {
        foreach ( string Url in this.DocumentList.Keys )
        {
          yield return this.DocumentList[ Url ];
        }
      }
    }

    /**************************************************************************/

    public int CountDocuments ()
    {
      return ( this.DocumentList.Count );
    }

    /**************************************************************************/

    public void AddDocumentNote ( MacroscopeDocument msDoc, string Note )
    {
      string Url = msDoc.GetUrl();
      lock ( this.DocumentNote )
      {
        if ( this.DocumentNote.ContainsKey( Url ) )
        {
          this.DocumentNote[ Url ] = Note;
        }
        else
        {
          this.DocumentNote.Add( Url, Note );

        }
      }
    }

    /**************************************************************************/

    public string GetDocumentNote ( MacroscopeDocument msDoc )
    {
      string Url = msDoc.GetUrl();
      string Note = null;
      lock ( this.DocumentNote )
      {
        if ( this.DocumentNote.ContainsKey( Url ) )
        {
          Note = this.DocumentNote[ Url ];
        }
      }
      return ( Note );
    }

    /**************************************************************************/

  }

}
