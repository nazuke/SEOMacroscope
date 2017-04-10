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

      if( msDoc == null )
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

    public virtual void RenderListView ( MacroscopeDocumentCollection DocCollection )
    {

      if( DocCollection.CountDocuments() == 0 )
      {
        return;
      }
            
      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ();
      decimal Count = 0;
      decimal TotalDocs = ( decimal )DocCollection.CountDocuments();
      decimal MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
      
        ProgressForm.Show();
      
        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing document collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );  

      }
      
      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {

        Application.DoEvents();

        if( msDoc != null )
        {
          this.RenderListView( msDoc: msDoc, Url: msDoc.GetUrl() );
        }

        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {
      
          Count++; 
          MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: MajorPercentage,
            ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
          );
        
        }

      }
     
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.Close();
      }
            
      ProgressForm.Dispose();
      
    }

    /** Render List ***********************************************************/

    public void RenderListView (
      MacroscopeDocumentCollection DocCollection,
      List<string> UrlList
    )
    {
      
      if( DocCollection.CountDocuments() == 0 )
      {
        return;
      }
     
      if( UrlList.Count == 0 )
      {
        return;
      }

      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ();
      decimal Count = 0;
      decimal TotalDocs = ( decimal )UrlList.Count;
      decimal MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
      
        ProgressForm.Show();
      
        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing document collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );
      
      }
            
      foreach( string Url in UrlList )
      {
      
        Application.DoEvents();

        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
      
        if( msDoc != null )
        {
          this.RenderListView( msDoc: msDoc, Url: msDoc.GetUrl() );
        }

        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {
      
          Count++;
          MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: MajorPercentage,
            ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
          );
        
        }
              
      }
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.Close();
      }
            
      ProgressForm.Dispose();
      
    }

    /** Render Document List **************************************************/

    public void RenderListView ( List<MacroscopeDocument> DocList )
    {
      
      if( DocList.Count == 0 )
      {
        return;
      }

      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ();
      decimal Count = 0;
      decimal TotalDocs = ( decimal )DocList.Count;
      decimal MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
      
        ProgressForm.Show();
      
        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing document collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );
              
      }
      
      for( int i = 0 ; i < DocList.Count ; i++ )
      {
        
        MacroscopeDocument msDoc = DocList[ i ];
        
        if( msDoc != null )
        {
          this.RenderListView( msDoc: msDoc, Url: msDoc.GetUrl() );
        }
        
        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {
      
          Count++;
          MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: MajorPercentage,
            ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
          );
                
        }
              
      }
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.Close();
      }
            
      ProgressForm.Dispose();
    
    }

    /** Render Filtered DocCollection *******************************************/

    public void RenderListView (
      MacroscopeDocumentCollection DocCollection,
      MacroscopeConstants.DocumentType DocumentType
    )
    {

      if( DocCollection.CountDocuments() == 0 )
      {
        return;
      }

      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ();
      decimal Count = 0;
      decimal TotalDocs = ( decimal )DocCollection.CountDocuments();
      decimal MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
      
        ProgressForm.Show();
      
        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing document collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );
              
      }

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {
        
        Application.DoEvents();

        if( msDoc != null )
        {
          if(
            ( msDoc.GetDocumentType() == DocumentType )
            || ( DocumentType == MacroscopeConstants.DocumentType.ALL ) )
          {
            this.RenderListView( msDoc: msDoc, Url: msDoc.GetUrl() );
          }
        }
        
        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {
      
          Count++;
          MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: MajorPercentage,
            ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
          );
        
        }
                
      }
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.Close();
      }
            
      ProgressForm.Dispose();
    }

    /** Render DocCollection Filtered by URL Fragment *************************/

    public void RenderListView (
      MacroscopeDocumentCollection DocCollection,
      string UrlFragment
    )
    {

      if( DocCollection.CountDocuments() == 0 )
      {
        return;
      }
     
      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm ();
      decimal Count = 0;
      decimal TotalDocs = ( decimal )DocCollection.CountDocuments();
      decimal MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
      
        ProgressForm.Show();
      
        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing document collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );
              
      }

      foreach( MacroscopeDocument msDoc in DocCollection.IterateDocuments() )
      {
        
        Application.DoEvents();

        if( msDoc != null )
        {
          string Url = msDoc.GetUrl();
          if( Url.IndexOf( UrlFragment, StringComparison.CurrentCulture ) >= 0 )
          {
            this.RenderListView( msDoc: msDoc, Url: Url );
          }
        }
        
        if( MacroscopePreferencesManager.GetShowProgressDialogues() )
        {
      
          Count++;
          MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
          ProgressForm.UpdatePercentages(
            Title: null,
            Message: null,
            MajorPercentage: MajorPercentage,
            ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
          );
        
        }
                
      }
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.Close();
      }
            
      ProgressForm.Dispose();
      
    }

    /** Render One ************************************************************/

    abstract protected void RenderListView ( MacroscopeDocument msDoc, string Url );

    /**************************************************************************/

  }

}
