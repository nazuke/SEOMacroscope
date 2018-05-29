/*

  This file is part of SEOMacroscope.

  Copyright 2018 Jason Holland.

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
  /// Description of MacroscopeClickPathAnalysis.
  /// </summary>

  public class MacroscopeClickPathAnalysis : Macroscope
  {

    /**************************************************************************/
    
    private MacroscopeDocumentCollection DocCollection;

    private MacroscopeDocument RootDoc;

    private Dictionary<MacroscopeDocument,List<MacroscopeHyperlinkOut>> NodeVisited;

    private SortedDictionary<string,List<LinkedList<string>>> PageChains;
    
    /**************************************************************************/

    public MacroscopeClickPathAnalysis ( MacroscopeDocumentCollection DocumentCollection )
    {

      this.SuppressDebugMsg = true;

      this.DocCollection = DocumentCollection;
      
      this.RootDoc = null;

      this.NodeVisited = new Dictionary<MacroscopeDocument,List<MacroscopeHyperlinkOut>> ();
      
      this.PageChains = new SortedDictionary<string,List<LinkedList<string>>> ();

    }

    /**************************************************************************/

    public void Analyze ( MacroscopeDocument RootDoc )
    {

      LinkedList<string> PageChain = new LinkedList<string> ();

      this.RootDoc = RootDoc;
      
      this.NodeVisited.Clear();

      this.PageChains.Clear();

      this.Descend(
        PageChain: PageChain,
        ParentDoc: RootDoc
      );

#if DEBUG
      this.DebugMsg( "######################################################" );
      // TODO: Remove this after debugging:
      foreach( string Url in this.PageChains.Keys )
      {
        this.DebugMsg( string.Format( "PageChains URL: {0}", Url ) );
        int Count = 0;
        foreach( LinkedList<string> Chain in this.PageChains[Url] )
        {
          this.DebugMsg( string.Format( "----{0}: {1}", Count, Url ) );
          foreach( string ChainedUrl in Chain )
          {
            this.DebugMsg( string.Format( "--------ChainedUrl: {0}", ChainedUrl ) );
          }
          Count++;
        }
      }
      this.DebugMsg( "######################################################" );
      /*
      // TODO: Remove this after debugging:
      foreach( string Url in this.PageChains.Keys )
      {
        this.DebugMsg( string.Format( "PageChains URL: {0}", Url ) );
        int Count = 0;
        foreach( LinkedList<string> Chain in this.PageChains[Url] )
        {
          this.DebugMsg( string.Format( "----{0}: {1}", Count, Chain.Count ) );
          Count++;
        }
      }
      */
      this.DebugMsg( "######################################################" );
#endif

      return;

    }

    /**************************************************************************/

    // TODO: Finish this.
    
    private void Descend (
      LinkedList<string> PageChain,
      MacroscopeDocument ParentDoc
    )
    {

      string ParentUrl = ParentDoc.GetUrl();

      PageChain.AddLast( ParentUrl );

      foreach( MacroscopeHyperlinkOut HyperlinkOut in ParentDoc.IterateHyperlinksOut() )
      {

        if( HyperlinkOut.GetTargetUrl().Equals( ParentUrl ) )
        {
          continue;
        }

        if( this.CheckNodeAlreadyVisited( msDoc: ParentDoc, HyperlinkOut: HyperlinkOut ) )
        {
          continue;
        }
        else
        {

          MacroscopeDocument CurrentDoc = this.DocCollection.GetDocument( Url: HyperlinkOut.GetTargetUrl() );

          if( CurrentDoc != null )
          {

            if( CurrentDoc.GetHostAndPort().Equals( ParentDoc.GetHostAndPort() ) )
            {

              this.Descend(
                PageChain: PageChain,
                ParentDoc: CurrentDoc
              );

            }

          }
          
        }

      }

      {
        
        LinkedList<string> PageChainClone = new LinkedList<string> ();

        foreach( string Url in PageChain )
        {

          PageChainClone.AddLast( Url );

          if( ParentDoc.GetUrl().Equals( Url ) )
          {
            break;
          }

        }

        if( !this.PageChains.ContainsKey( PageChain.Last.Value ) )
        {
          this.PageChains.Add( PageChain.Last.Value, new List<LinkedList<string>> () );
        }

        this.PageChains[ PageChain.Last.Value ].Add( PageChainClone );
        
      }

      PageChain.RemoveLast();

      return;

    }

    /**************************************************************************/

    private bool CheckNodeAlreadyVisited (
      MacroscopeDocument msDoc,
      MacroscopeHyperlinkOut HyperlinkOut
    )
    {
      
      bool Result = false;

      if( this.NodeVisited.ContainsKey( msDoc ) )
      {

        if( this.NodeVisited[ msDoc ].Contains( HyperlinkOut ) )
        {
          Result = true;
        }
        else
        {
          this.NodeVisited[ msDoc ].Add( HyperlinkOut );
        }

      }
      else
      {

        this.NodeVisited[ msDoc ] = new List<MacroscopeHyperlinkOut> ();

        this.NodeVisited[ msDoc ].Add( HyperlinkOut );

      }

      return( Result );

    }

    /**************************************************************************/

  }

}
