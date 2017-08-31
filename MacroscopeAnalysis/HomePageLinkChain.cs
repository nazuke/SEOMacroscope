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

      MacroscopeDocumentChain LinkChain = new MacroscopeDocumentChain ();
      Boolean Success = false;

      string CurrentPageUrl = this.LeafMsDoc.GetUrl();

      this.DebugMsg( string.Format( "CURRENT PAGE URL: {0}", CurrentPageUrl ) );

      LinkChain.AddDocument( msDoc: this.LeafMsDoc );
      
      this.Descend( LinkChain: LinkChain, CurrentDoc: this.LeafMsDoc );

      
      foreach( MacroscopeDocument doc in  LinkChain.IterateDocuments() )
      {

        this.DebugMsg( string.Format( "DOC: {0}", doc.GetUrl() ) );

      }
      
      
      
      
      
      
      
      
      
      
      return( Success );

    }

    /** -------------------------------------------------------------------- **/

    private void Descend (
      MacroscopeDocumentChain LinkChain,
      MacroscopeDocument CurrentDoc
    )
    {

      string CurrentPageUrl = CurrentDoc.GetUrl();

      foreach( MacroscopeHyperlinkIn HyperLinkIn in CurrentDoc.IterateHyperlinksIn() )
      {

        string SourceUrl = HyperLinkIn.GetSourceUrl();
        string TargetUrl = HyperLinkIn.GetTargetUrl();

        if( CurrentPageUrl.Equals( TargetUrl ) && ( !CurrentPageUrl.Equals( this.LeafMsDoc.GetUrl() ) ) )
        {

          MacroscopeDocument ParentDoc = this.LeafDocCollection.GetDocument( Url: SourceUrl );

          if( ParentDoc != null )
          {

            
            
            
            
            //this.DebugMsg( string.Format( "SourceUrl: {0}", SourceUrl ) );
            //this.DebugMsg( string.Format( "TargetUrl: {0}", TargetUrl ) );

            LinkChain.AddDocument( msDoc: ParentDoc );
                
            //this.Descend( LinkChain: LinkChain, CurrentDoc: ParentDoc );
          
          }
                
        }

      }

      
      
      
      
      return;

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
