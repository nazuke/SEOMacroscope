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
using System.Threading.Tasks;

namespace SEOMacroscope
{

  [TestFixture]
  public class TestMacroscopeLevenshteinAnalysis : Macroscope, IMacroscopeTaskController
  {

    /**************************************************************************/

    [Test]
    public async Task TestDuplicate ()
    {

      const string StartUrl = "https://nazuke.github.io/SEOMacroscope/";
      const string DupeUrl = "https://nazuke.github.io/SEOMacroscope/index.html";
      MacroscopeJobMaster JobMaster;
      MacroscopeDocumentCollection DocCollection;
      Dictionary<string, Boolean> CrossCheckList;
      MacroscopeDocument msDoc;
      MacroscopeDocument msDocDifferent;

      JobMaster = new MacroscopeJobMaster(
      JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
      TaskController: this
      );

      DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );

      CrossCheckList = MacroscopeLevenshteinAnalysis.GetCrossCheckList( Capacity: DocCollection.CountDocuments() );

      msDoc = DocCollection.CreateDocument( StartUrl );
      msDocDifferent = DocCollection.CreateDocument( DupeUrl );

      await msDoc.Execute();
      await msDocDifferent.Execute();

      DocCollection.AddDocument( msDoc );
      DocCollection.AddDocument( msDocDifferent );

      DebugMsg( string.Format( "msDoc: {0}", msDoc.GetStatusCode() ) );

      DebugMsg( string.Format( "msDocDifferent: {0}", msDocDifferent.GetStatusCode() ) );

      for( int i = 1 ; i <= 100 ; i++ )
      {

        MacroscopeLevenshteinAnalysis LevenshteinAnalysis;
        Dictionary<MacroscopeDocument, int> DocList;

        LevenshteinAnalysis = new MacroscopeLevenshteinAnalysis(
        msDoc: msDoc,
        SizeDifference: 64,
        Threshold: 16,
        CrossCheckList: CrossCheckList
        );

        DocList = LevenshteinAnalysis.AnalyzeDocCollection( DocCollection: DocCollection );

        DebugMsg( string.Format( "DocList: {0}", DocList.Count ) );

        foreach( MacroscopeDocument msDocAnalyzed in DocList.Keys )
        {

          DebugMsg( string.Format( "msDocAnalyzed: {0} => {1}", DocList[ msDocAnalyzed ], msDocAnalyzed.GetUrl() ) );

          Assert.AreEqual(
          DocList[ msDocAnalyzed ],
          0,
          string.Format( "FAIL: {0} => {1}", DocList[ msDocAnalyzed ], msDocAnalyzed.GetUrl() )
          );

        }

      }

    }

    /**************************************************************************/

    [Test]
    public async Task TestDifferent ()
    {

      const string StartUrl = "https://nazuke.github.io/SEOMacroscope/";
      MacroscopeJobMaster JobMaster;
      MacroscopeDocumentCollection DocCollection;
      Dictionary<string, Boolean> CrossCheckList;
      MacroscopeDocument msDoc;
      MacroscopeLevenshteinAnalysis LevenshteinAnalysis;
      List<string> TargetUrls;

      JobMaster = new MacroscopeJobMaster(
      JobRunTimeMode: MacroscopeConstants.RunTimeMode.LIVE,
      TaskController: this
      );

      DocCollection = new MacroscopeDocumentCollection( JobMaster: JobMaster );

      CrossCheckList = MacroscopeLevenshteinAnalysis.GetCrossCheckList( Capacity: DocCollection.CountDocuments() );

      msDoc = DocCollection.CreateDocument( StartUrl );
      await msDoc.Execute();
      DocCollection.AddDocument( msDoc );

      DebugMsg( string.Format( "msDoc: {0}", msDoc.GetStatusCode() ) );

      LevenshteinAnalysis = new MacroscopeLevenshteinAnalysis(
        msDoc: msDoc,
        SizeDifference: 64,
        Threshold: 16,
        CrossCheckList: CrossCheckList
      );

      TargetUrls = new List<string>();
      TargetUrls.Add( "https://nazuke.github.io/SEOMacroscope/blog/" );
      TargetUrls.Add( "https://nazuke.github.io/SEOMacroscope/downloads/" );
      TargetUrls.Add( "https://nazuke.github.io/SEOMacroscope/manual/" );

      foreach( string TargetUrl in TargetUrls )
      {
        MacroscopeDocument msDocTarget = DocCollection.CreateDocument( TargetUrl );
        await msDocTarget.Execute();
        DocCollection.AddDocument( msDocTarget );
        DebugMsg( string.Format( "msDocTarget: {0}", msDocTarget.GetStatusCode() ) );
      }

      for( int i = 1 ; i <= 10 ; i++ )
      {

        Dictionary<MacroscopeDocument, int> DocList;

        DocList = LevenshteinAnalysis.AnalyzeDocCollection(
        DocCollection: DocCollection
        );

        DebugMsg( string.Format( "DocList: {0}", DocList.Count ) );

        foreach( MacroscopeDocument msDocAnalyzed in DocList.Keys )
        {

          DebugMsg( string.Format( "msDocAnalyzed: {0} => {1}", DocList[ msDocAnalyzed ], msDocAnalyzed.GetUrl() ) );

          Assert.AreNotEqual(
          DocList[ msDocAnalyzed ],
          0,
          string.Format(
          "FAIL: {0} => {1}",
          DocList[ msDocAnalyzed ],
          msDocAnalyzed.GetUrl()
          )
          );

        }

      }

    }

    /**************************************************************************/

    public void ICallbackScanComplete ()
    {
    }

    public MacroscopeCredentialsHttp IGetCredentialsHttp ()
    {
      MacroscopeCredentialsHttp CredentialsHttp = new MacroscopeCredentialsHttp();
      return ( CredentialsHttp );
    }

    public void ICallbackOutOfMemory ()
    {
      DebugMsg( string.Format( "ICallbackOutOfMemory: CALLED" ) );
    }

    /**************************************************************************/

  }

}
