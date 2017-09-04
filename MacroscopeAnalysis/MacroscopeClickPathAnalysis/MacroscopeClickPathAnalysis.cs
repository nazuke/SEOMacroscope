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

    List<Guid> Guids;

    SortedDictionary<string,List<int>> ClickPathDepth;

    /**************************************************************************/

    public MacroscopeClickPathAnalysis ( MacroscopeDocumentCollection DocumentCollection )
    {
      
      this.DocCollection = DocumentCollection;
      
      this.RootDoc = null;
      
      this.Guids = null;
      
      this.ClickPathDepth = null;
            
    }

    /**************************************************************************/

    public void Analyze ( MacroscopeDocument RootDoc )
    {

      this.RootDoc = RootDoc;
      this.Guids = new List<Guid> ();
      this.ClickPathDepth = new SortedDictionary<string,List<int>> ();

      if( this.RootDoc != null )
      {

        foreach( MacroscopeHyperlinkOut HyperlinkOut in this.RootDoc.IterateHyperlinksOut() )
        {

          this.DebugMsg( string.Format( "GetTargetUrl: {0}", HyperlinkOut.GetTargetUrl() ) );

          this.Descend(
            Depth: 1,
            ParentDoc: this.RootDoc,
            ParentHyperlinkOut: HyperlinkOut
          );

        }

      }
      
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
            continue;
          }
        
        
          
          
          
          Guid LinkGuid = HyperlinkOut.GetGuid();
          
          if( this.Guids.Contains( LinkGuid ) )
          {
            this.DebugMsg( string.Format( "SEEN: {0}", LinkGuid ) );
            continue;
          }
          else
          {
            this.DebugMsg( string.Format( "UNSEEN: {0}", LinkGuid ) );
          }

          this.DebugMsg( string.Format( "Descend: {0} : {1}", CurrentDepth, HyperlinkOut.GetTargetUrl() ) );

          this.Descend(
            Depth: CurrentDepth,
            ParentDoc: CurrentDoc,
            ParentHyperlinkOut: HyperlinkOut
          );

        }
      
      }

    }

    /**************************************************************************/

  }

}
