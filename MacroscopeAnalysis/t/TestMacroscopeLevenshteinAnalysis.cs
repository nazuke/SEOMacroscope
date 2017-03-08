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
using NUnit.Framework;
using System.Collections.Generic;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeLevenshteinAnalysis : Macroscope, IMacroscopeTaskController
  {

    /**************************************************************************/

    [Test]
    public void TestDuplicate ()
    {

      const string StartUrl = "https://nazuke.github.io/SEOMacroscope/";                  
      const string DupeUrl = "https://nazuke.github.io/SEOMacroscope/index.html";

      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster (
                                        RunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
                                        TaskController: this
                                      );

      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection ( JobMaster: JobMaster );

      MacroscopeDocument msDoc = DocCollection.CreateDocument( StartUrl );
      MacroscopeDocument msDocDifferent = DocCollection.CreateDocument( DupeUrl );
      
      msDoc.Execute();
      msDocDifferent.Execute();
      
      DocCollection.AddDocument( msDoc );
      DocCollection.AddDocument( msDocDifferent );

      DebugMsg( string.Format( "msDoc: {0}", msDoc.GetStatusCode() ) );
      
      DebugMsg( string.Format( "msDocDifferent: {0}", msDocDifferent.GetStatusCode() ) );

      MacroscopeLevenshteinAnalysis LevenshteinAnalysis = new MacroscopeLevenshteinAnalysis ( msDoc: msDoc, Threshold: 64 );

      Dictionary<MacroscopeDocument,int> DocList = LevenshteinAnalysis.AnalyzeDocCollection( DocCollection: DocCollection );
      
      DebugMsg( string.Format( "DocList: {0}", DocList.Count ) );

      foreach( MacroscopeDocument msDocAnalyzed in DocList.Keys )
      {
        DebugMsg( string.Format( "msDocAnalyzed: {0} => {1}", DocList[ msDocAnalyzed ], msDocAnalyzed.GetUrl() ) );
        Assert.AreEqual( DocList[ msDocAnalyzed ], 0, string.Format( "FAIL: {0} => {1}", DocList[ msDocAnalyzed ], msDocAnalyzed.GetUrl() ) );
      }

      return;
      
    }

    /**************************************************************************/
    
    [Test]
    public void TestDifferent ()
    {

      const string StartUrl = "https://nazuke.github.io/SEOMacroscope/";

      MacroscopeJobMaster JobMaster = new MacroscopeJobMaster (
                                        RunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
                                        TaskController: this
                                      );

      MacroscopeDocumentCollection DocCollection = new MacroscopeDocumentCollection ( JobMaster: JobMaster );
      
      MacroscopeDocument msDoc = DocCollection.CreateDocument( StartUrl );
      msDoc.Execute();
      DocCollection.AddDocument( msDoc );

      DebugMsg( string.Format( "msDoc: {0}", msDoc.GetStatusCode() ) );

      MacroscopeLevenshteinAnalysis LevenshteinAnalysis = new MacroscopeLevenshteinAnalysis ( msDoc: msDoc, Threshold: 64 );

      List<string> TargetUrls = new List<string> () { {
          "https://nazuke.github.io/SEOMacroscope/manual/"
        }
      };

      foreach( string TargetUrl in TargetUrls )
      {
        MacroscopeDocument msDocTarget = DocCollection.CreateDocument( TargetUrl );
        msDocTarget.Execute();
        DocCollection.AddDocument( msDocTarget );
        DebugMsg( string.Format( "msDocTarget: {0}", msDocTarget.GetStatusCode() ) );
      }

      Dictionary<MacroscopeDocument,int> DocList = LevenshteinAnalysis.AnalyzeDocCollection( DocCollection: DocCollection );
      
      DebugMsg( string.Format( "DocList: {0}", DocList.Count ) );

      foreach( MacroscopeDocument msDocAnalyzed in DocList.Keys )
      {
        DebugMsg( string.Format( "msDocAnalyzed: {0} => {1}", DocList[ msDocAnalyzed ], msDocAnalyzed.GetUrl() ) );
        Assert.AreEqual( DocList[ msDocAnalyzed ], 0, string.Format( "FAIL: {0} => {1}", DocList[ msDocAnalyzed ], msDocAnalyzed.GetUrl() ) );
      }

      return;
      
    }

    /**************************************************************************/

    public void ICallbackScanComplete ()
    {
    }

    public MacroscopeCredentialsHttp IGetCredentialsHttp ()
    {
      MacroscopeCredentialsHttp CredentialsHttp = new MacroscopeCredentialsHttp ();
      return( CredentialsHttp );
    }

    /**************************************************************************/
    
  }

}
