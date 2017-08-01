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
using NUnit.Framework;

namespace SEOMacroscope
{
  
  [TestFixture]
  public class TestMacroscopeDeepKeywordAnalysis : Macroscope
  {
    
    /**************************************************************************/
	          
    private const string BodyText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Cras commodo, leo et dignissim sodales, sem orci molestie neque, non sollicitudin ligula neque quis urna. Nullam condimentum placerat neque non gravida. Integer at mauris nisi. Vivamus ut auctor enim. Duis sodales enim sem. Nullam ex neque, dignissim congue interdum maximus, dictum elementum risus. Ut faucibus, tellus quis auctor ultricies, elit turpis mattis orci, nec condimentum metus massa pellentesque odio. Nulla pharetra vitae eros eu gravida. Suspendisse ut lacinia elit. Pellentesque id orci nec arcu lobortis dictum. Nulla iaculis augue metus, eget tempus tellus posuere eu. Donec feugiat ligula in dui consectetur consequat id vitae enim. Nullam fermentum, felis faucibus porttitor iaculis, nisl nulla sollicitudin lorem, nec rhoncus orci leo ac purus. Nullam porttitor eleifend mauris, ac egestas leo cursus a. Cras ultricies magna libero, non vestibulum enim laoreet in. Morbi est sapien, iaculis id justo pulvinar, aliquam pulvinar quam. Cras auctor tellus sed purus lobortis, ac faucibus justo blandit. Aliquam eleifend nunc purus, id scelerisque erat ornare sed. Etiam in tincidunt quam. Nullam accumsan maximus placerat. Fusce interdum diam ut magna malesuada blandit vel sit amet dolor. In luctus quam nec pharetra porttitor. Mauris sed elit a tellus laoreet ornare. Etiam tempor aliquam augue vel viverra. Phasellus vehicula dui nibh, vel tempor erat finibus eget. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Pellentesque dolor dui, venenatis quis varius ac, ultricies et erat. Donec eget risus turpis. Maecenas est purus, fermentum et porta ut, rutrum sit amet turpis. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vivamus facilisis dolor nisi, a tincidunt augue pretium at. Maecenas ultricies metus lacus, eget volutpat turpis porta dignissim. Nunc vel ultricies lorem, vitae condimentum odio. Maecenas nec lorem et arcu euismod facilisis. Nunc massa elit, pretium ac venenatis quis, sodales eu ipsum. Morbi erat dui, vulputate vel mauris non, sagittis euismod nulla. Vestibulum vitae pulvinar sapien. Aenean sed sapien sit amet lorem lobortis pretium eu accumsan felis. Suspendisse sollicitudin eu tellus id pretium. Praesent eu egestas lectus. In a ex tristique, tincidunt nisi ut, rhoncus dolor. Maecenas porta tellus nec ex suscipit, ut feugiat sem ultrices. Cras nibh ligula, scelerisque id laoreet vitae, sagittis a mi. Ut eget tortor massa. Suspendisse luctus vestibulum orci, vitae maximus tortor aliquet ut. Donec vitae ligula porttitor, porttitor lorem quis, ullamcorper eros. Fusce porta, purus ut suscipit lacinia, velit elit laoreet neque, sit amet semper est nibh ut quam. Fusce sed auctor est. Donec ac libero erat. Nunc bibendum lacus urna, quis faucibus dui imperdiet sit amet. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi ullamcorper leo est, ut luctus mauris iaculis ut. In sit amet finibus nunc, sed viverra purus. Fusce feugiat metus vel consequat condimentum. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Proin condimentum, diam sit amet posuere ultrices, dolor mi hendrerit neque, vel varius risus diam eu nisi. Proin ultricies consectetur ligula, id finibus mi accumsan id. Nunc mattis semper ornare.";
	  
    /**************************************************************************/
	      
    [Test]
    public void TestAnalyze1 ()
    {
      
      MacroscopeDocument msDoc = new MacroscopeDocument ( "http://localhost/" );
      
      msDoc.SetDocumentText( Text: BodyText );
      
      MacroscopeDeepKeywordAnalysis AnalyzeKeywords = new MacroscopeDeepKeywordAnalysis ();

      Dictionary<string,int> Terms = new Dictionary<string,int> ( 256 );

      AnalyzeKeywords.Analyze( Text: msDoc.GetDocumentTextCleaned(), Terms: Terms, Words: 1 );

      foreach( string Term in Terms.Keys )
      {
        DebugMsg( string.Format( "TOTALS 1: {0} :: {1}", Terms[ Term ], Term ) );
      }
    }
    
    /**************************************************************************/
        
    [Test]
    public void TestAnalyze2 ()
    {
      
      MacroscopeDocument msDoc = new MacroscopeDocument ( "http://localhost/" );
      
      msDoc.SetDocumentText( Text: BodyText );
      
      MacroscopeDeepKeywordAnalysis AnalyzeKeywords = new MacroscopeDeepKeywordAnalysis ();

      Dictionary<string,int> Terms = new Dictionary<string,int> ( 256 );

      AnalyzeKeywords.Analyze( Text: msDoc.GetDocumentTextCleaned(), Terms: Terms, Words: 2 );

      foreach( string Term in Terms.Keys )
      {
        DebugMsg( string.Format( "TOTALS 2: {0} :: {1}", Terms[ Term ], Term ) );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestAnalyze3 ()
    {
      
      MacroscopeDocument msDoc = new MacroscopeDocument ( "http://localhost/" );
      
      msDoc.SetDocumentText( Text: BodyText );
      
      MacroscopeDeepKeywordAnalysis AnalyzeKeywords = new MacroscopeDeepKeywordAnalysis ();

      Dictionary<string,int> Terms = new Dictionary<string,int> ( 256 );

      AnalyzeKeywords.Analyze( Text: msDoc.GetDocumentTextCleaned(), Terms: Terms, Words: 3 );

      foreach( string Term in Terms.Keys )
      {
        DebugMsg( string.Format( "TOTALS 2: {0} :: {1}", Terms[ Term ], Term ) );
      }
    }

    /**************************************************************************/

    [Test]
    public void TestAnalyze4 ()
    {
      
      MacroscopeDocument msDoc = new MacroscopeDocument ( "http://localhost/" );
      
      msDoc.SetDocumentText( Text: BodyText );
      
      MacroscopeDeepKeywordAnalysis AnalyzeKeywords = new MacroscopeDeepKeywordAnalysis ();

      Dictionary<string,int> Terms = new Dictionary<string,int> ( 256 );

      AnalyzeKeywords.Analyze( Text: msDoc.GetDocumentTextCleaned(), Terms: Terms, Words: 4 );

      foreach( string Term in Terms.Keys )
      {
        DebugMsg( string.Format( "TOTALS 2: {0} :: {1}", Terms[ Term ], Term ) );
      }
    }
    
    /**************************************************************************/
		    
  }
	
}
