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
using System.Windows.Forms;

namespace SEOMacroscope
{

  abstract public class MacroscopeDisplayListView : Macroscope
  {

    /**************************************************************************/

    public MacroscopeMainForm MainForm;

    public ListView lvListView;

    protected Boolean ListViewConfigured = false;
        
    /**************************************************************************/

    protected MacroscopeDisplayListView ( MacroscopeMainForm MainForm, ListView lvListView )
    {
      this.MainForm = MainForm;
      this.lvListView = lvListView;
    }

    /**************************************************************************/

    abstract protected void ConfigureListView ();

    /**************************************************************************/

    public void ClearData ()
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              this.lvListView.Items.Clear();
            }
          )
        );
      }
      else
      {
        this.lvListView.Items.Clear();
      }
    }

    /**************************************************************************/

    public void RefreshData ( MacroscopeDocumentCollection DocCollection )
    {

      if( DocCollection.CountDocuments() <= 0 )
      {
        return;
      }

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RenderListView( DocCollection );
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.RenderListView( DocCollection );
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    public void RefreshData (
      MacroscopeDocumentCollection DocCollection,
      List<string> UrlList
    )
    {

      if( DocCollection.CountDocuments() <= 0 )
      {
        return;
      }

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RenderListView( DocCollection, UrlList );
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.RenderListView( DocCollection, UrlList );
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    public void RefreshData (
      MacroscopeDocument msDoc,
      string Url
    )
    {

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RenderListView( msDoc, Url );
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.RenderListView( msDoc, Url );
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    public void RefreshData ( List<MacroscopeDocument> DocList )
    {

      if( DocList.Count <= 0 )
      {
        return;
      }

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RenderListView( DocList: DocList );
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.RenderListView( DocList: DocList );
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    public void RefreshData (
      MacroscopeDocumentCollection DocCollection,
      MacroscopeConstants.DocumentType DocumentType
    )
    {

      if( DocCollection.CountDocuments() <= 0 )
      {
        return;
      }

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RenderListView( DocCollection: DocCollection, DocumentType: DocumentType );
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.RenderListView( DocCollection: DocCollection, DocumentType: DocumentType );
        Cursor.Current = Cursors.Default;
      }

    }

    /**************************************************************************/

    public void RefreshData (
      MacroscopeDocumentCollection DocCollection,
      string UrlFragment
    )
    {

      if( DocCollection.CountDocuments() <= 0 )
      {
        return;
      }

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RenderListView( DocCollection: DocCollection, UrlFragment: UrlFragment );
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.RenderListView( DocCollection: DocCollection, UrlFragment: UrlFragment );
        Cursor.Current = Cursors.Default;
      }

    }

    /** Render Entire DocCollection *******************************************/

    public void RenderListView ( MacroscopeDocumentCollection DocCollection )
    {
      DebugMsg( string.Format( "RenderListView: {0}", "BASE" ) );
      foreach( string Url in DocCollection.DocumentKeys() )
      {
        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        this.RenderListView( msDoc, Url );
      }
    }

    /** Render List ***********************************************************/

    public void RenderListView (
      MacroscopeDocumentCollection DocCollection,
      List<string> UrlList
    )
    {
      foreach( string Url in UrlList )
      {
        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        this.RenderListView( msDoc, Url );
      }
    }

    /** Render Document List **************************************************/

    public void RenderListView ( List<MacroscopeDocument> DocList )
    {
      for( int i = 0 ; i < DocList.Count ; i++ )
      {
        MacroscopeDocument msDoc = DocList[ i ];
        this.RenderListView( msDoc, msDoc.GetUrl() );
      }
    }

    /** Render Filtered DocCollection *******************************************/

    public void RenderListView (
      MacroscopeDocumentCollection DocCollection,
      MacroscopeConstants.DocumentType DocumentType
    )
    {
      DebugMsg( string.Format( "RenderListView: {0}", "BASE" ) );
      foreach( string Url in DocCollection.DocumentKeys() )
      {
        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        if(
          ( msDoc.GetDocumentType() == DocumentType )
          || ( DocumentType == MacroscopeConstants.DocumentType.ALL ) )
        {
          this.RenderListView( msDoc, Url );
        }
      }
    }

    /** Render DocCollection Filtered by URL Fragment *************************/

    public void RenderListView (
      MacroscopeDocumentCollection DocCollection,
      string UrlFragment
    )
    {
      DebugMsg( string.Format( "RenderListView: {0}", "BASE" ) );
      foreach( string Url in DocCollection.DocumentKeys() )
      {
        if( Url.IndexOf( UrlFragment, StringComparison.CurrentCulture ) >= 0 )
        {
          MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
          this.RenderListView( msDoc, Url );
        }
      }
    }

    /** Render One ************************************************************/

    abstract protected void RenderListView ( MacroscopeDocument msDoc, string Url );

    /**************************************************************************/

  }

}
