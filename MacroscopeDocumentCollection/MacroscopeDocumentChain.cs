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
  /// Description of MacroscopeDocumentChain.
  /// </summary>

  public class MacroscopeDocumentChain : Macroscope
  {
    /**************************************************************************/

    private LinkedList<MacroscopeDocument> DocumentChain;

    /**************************************************************************/

    public MacroscopeDocumentChain ()
    {
      this.DocumentChain = new LinkedList<MacroscopeDocument> ();
    }

    /** Self Destruct Sequence ************************************************/
        
    ~MacroscopeDocumentChain ()
    {
      
      this.DocumentChain = null;
     
    }

    /**************************************************************************/
    
    public void AddDocument ( MacroscopeDocument msDoc )
    {

      lock( this.DocumentChain )
      {



        //this.DocumentChain.Add( Url, msDoc );

      }

    }

    /**************************************************************************/

    public void RemoveDocument ( MacroscopeDocument msDoc )
    {

           
      lock( this.DocumentChain )
      {


        //          this.DocumentChain.Remove( Url );


      }

    }

    /**************************************************************************/
    
    public MacroscopeDocument GetDocument ()
    {

      MacroscopeDocument msDoc = null;

      /*
      if( this.DocumentChain.ContainsKey( Url ) )
      {
        msDoc = this.DocumentChain[ Url ];
      }
      */
     
      return( msDoc );

    }
    
    /**************************************************************************/

    public IEnumerable<MacroscopeDocument> IterateDocuments ()
    {

      lock( this.DocumentChain )
      {

        foreach( MacroscopeDocument msDoc in this.DocumentChain )
        {
          yield return msDoc;
        }

      }

    }

    /**************************************************************************/

    public int CountDocuments ()
    {
      return( this.DocumentChain.Count );
    }
    
    /**************************************************************************/

  }

}
