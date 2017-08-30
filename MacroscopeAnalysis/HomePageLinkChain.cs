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
  /// Description of HomePageLinkChain.
  /// </summary>

  public class HomePageLinkChain : Macroscope
  {

    /**************************************************************************/

    SortedDictionary<string,MacroscopeDocumentChain> LinkChains;
    
    MacroscopeDocumentCollection LeafDocCollection;
    
    MacroscopeDocument LeafMsDoc;
        
    /**************************************************************************/

    public HomePageLinkChain (
      MacroscopeDocumentCollection DocCollection,
      MacroscopeDocument msDoc
    )
    {

      this.SuppressDebugMsg = false;
      
      this.LinkChains = new SortedDictionary<string,MacroscopeDocumentChain> ();

      this.LeafDocCollection = DocCollection;
      
      this.LeafMsDoc = msDoc;

    }

    /**************************************************************************/

    public Boolean Compute ()
    {

      Boolean Success = false;
      



      foreach( MacroscopeHyperlinkIn HyperLinkIn in this.LeafMsDoc.IterateHyperlinksIn() )
      {
        
        string SourceUrl = HyperLinkIn.GetSourceUrl();
        string TargetUrl = HyperLinkIn.GetTargetUrl();
        
        this.DebugMsg( string.Format( "SourceUrl: {0}", SourceUrl ) );
        this.DebugMsg( string.Format( "TargetUrl: {0}", TargetUrl ) );
        
        
      }
      
           
      
      
      
      
      
      
      
      
      return( Success );

    }

    /**************************************************************************/

    public IEnumerable<MacroscopeDocumentChain> IterateChainsList ()
    {
      
      lock( this.LinkChains )
      {
        foreach( string Key in this.LinkChains.Keys )
        {
          yield return( this.LinkChains[ Key ] );
        
        }
      }
    }

    /**************************************************************************/
	      
  }

}
