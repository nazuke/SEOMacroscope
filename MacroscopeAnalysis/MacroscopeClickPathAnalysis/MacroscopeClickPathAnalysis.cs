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
  /// Description of MacroscopeClickPathAnalysis.
  /// </summary>

  public class MacroscopeClickPathAnalysis : Macroscope
  {

    /**************************************************************************/
    
    MacroscopeDocumentCollection DocCollection;

    MacroscopeDocument RootDoc;

    SortedDictionary<string,List<int>> ClickPathDepth;

    Dictionary<MacroscopeDocument,List<MacroscopeHyperlinkOut>> NodeVisited;

    /**************************************************************************/

    public MacroscopeClickPathAnalysis ( MacroscopeDocumentCollection DocumentCollection )
    {
      
      this.DocCollection = DocumentCollection;
      
      this.RootDoc = null;

      this.ClickPathDepth = null;

      this.NodeVisited = null;

    }

    /**************************************************************************/

    public void Analyze ( MacroscopeDocument RootDoc )
    {

      this.RootDoc = RootDoc;
      this.ClickPathDepth = new SortedDictionary<string,List<int>> ();

      this.NodeVisited = new Dictionary<MacroscopeDocument,List<MacroscopeHyperlinkOut>> ();

      if( this.RootDoc != null )
      {

        foreach( MacroscopeHyperlinkOut HyperlinkOut in this.RootDoc.IterateHyperlinksOut() )
        {

          this.DebugMsg( string.Format( "GetTargetUrl: {0}", HyperlinkOut.GetTargetUrl() ) );

          if( this.CheckNodeAlreadyVisited( msDoc: this.RootDoc, HyperlinkOut: HyperlinkOut ) )
          {
            continue;
          }
          else
          {

            this.Descend(
              Depth: 1,
              ParentDoc: this.RootDoc,
              ParentHyperlinkOut: HyperlinkOut
            );

          }
          
        }

      }

      foreach( string Url in this.ClickPathDepth.Keys )
      {
        foreach( int Depth in this.ClickPathDepth[Url] )
        {
          this.DebugMsg( string.Format( "DEPTH: {0} :: {1}", Depth, Url ) );
        }
      }

      return;

    }

    /** -------------------------------------------------------------------- **/

    private void Descend (
      int Depth,
      MacroscopeDocument ParentDoc,
      MacroscopeHyperlinkOut ParentHyperlinkOut
    )
    {

      MacroscopeDocument CurrentDoc = this.DocCollection.GetDocument( Url: ParentHyperlinkOut.GetTargetUrl() );
      int CurrentDepth = Depth + 1;

      if( CurrentDoc != null )
      {

        
        if( CurrentDoc.GetUrl().Equals( ParentDoc.GetUrl() ) )
        {
          return;
        }

        foreach( MacroscopeHyperlinkOut HyperlinkOut in CurrentDoc.IterateHyperlinksOut() )
        {

          if( CurrentDoc.GetUrl().Equals( HyperlinkOut.GetTargetUrl() ) )
          {

            string CurrentDocUrl = CurrentDoc.GetUrl();
            
            if( this.ClickPathDepth.ContainsKey( CurrentDocUrl ) )
            {
              if( !this.ClickPathDepth[ CurrentDocUrl ].Contains( Depth ) )
              {
                this.ClickPathDepth[ CurrentDocUrl ].Add( Depth );
              }
            }
            else
            {
              this.ClickPathDepth[ CurrentDocUrl ] = new List<int> ();
              this.ClickPathDepth[ CurrentDocUrl ].Add( Depth );
            }

          }
          else
          {

            if( this.CheckNodeAlreadyVisited( msDoc: CurrentDoc, HyperlinkOut: HyperlinkOut ) )
            {
              continue;
            }

            this.Descend(
              Depth: CurrentDepth,
              ParentDoc: CurrentDoc,
              ParentHyperlinkOut: HyperlinkOut
            );

          }
          
        }
      
      }

    }

    /**************************************************************************/

    private Boolean CheckNodeAlreadyVisited (
      MacroscopeDocument msDoc,
      MacroscopeHyperlinkOut HyperlinkOut
    )
    {
      
      Boolean Result = false;

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
