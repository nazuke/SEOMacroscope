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

  /// <summary>
  /// Description of MacroscopeDisplayHierarchy.
  /// </summary>

  public sealed class MacroscopeDisplayHierarchy : MacroscopeDisplayTreeView
  {

    /**************************************************************************/

    public MacroscopeDisplayHierarchy ( MacroscopeMainForm MainForm, TreeView tvTreeView )
      : base( MainForm, tvTreeView )
    {

      this.SuppressDebugMsg = false;

      this.ConfigureTreeView();

    }

    /**************************************************************************/

    protected override void ConfigureTreeView ()
    {

      if( !this.TreeViewConfigured )
      {
        this.TreeViewConfigured = true;
      }

      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              this.ClearData();
            }
          )
        );
      }
      else
      {
        this.ClearData();
      }

    }

    /**************************************************************************/

    public void ClearData ()
    {
      this.tvTreeView.BeginUpdate();
      this.tvTreeView.Nodes.Clear();
      TreeNode nRoot = this.tvTreeView.Nodes.Add( "/" );
      nRoot.Text = "Websites";
      this.tvTreeView.EndUpdate();
    }

    /**************************************************************************/

    public void RefreshData (
      MacroscopeDocumentCollection DocCollection,
      List<string> UrlList
    )
    {
      if( this.MainForm.InvokeRequired )
      {
        this.MainForm.Invoke(
          new MethodInvoker (
            delegate
            {
              Cursor.Current = Cursors.WaitCursor;
              this.RenderTreeView( DocCollection: DocCollection, UrlList: UrlList );
              Cursor.Current = Cursors.Default;
            }
          )
        );
      }
      else
      {
        Cursor.Current = Cursors.WaitCursor;
        this.RenderTreeView( DocCollection: DocCollection, UrlList: UrlList );
        Cursor.Current = Cursors.Default;
      }
    }

    /**************************************************************************/

    private void RenderTreeView (
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

      MacroscopeSinglePercentageProgressForm ProgressForm = new MacroscopeSinglePercentageProgressForm (this.MainForm);
      decimal Count = 0;
      decimal TotalDocs = ( decimal )UrlList.Count;
      decimal MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
      
      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {

        ProgressForm.UpdatePercentages(
          Title: "Preparing Display",
          Message: "Processing document collection for display:",
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );
      
      }

      this.tvTreeView.BeginUpdate();

      DebugMsg( string.Format( "HIERARCHY: {0}", "BASE" ) );

      foreach( string Url in UrlList )
      {
        
        MacroscopeDocument msDoc = DocCollection.GetDocument( Url );
        
        if( msDoc != null )
        {
          this.RenderTreeView( msDoc, Url );
        }
        
        Count++;
        MajorPercentage = ( ( decimal )100 / TotalDocs ) * Count;
        
        ProgressForm.UpdatePercentages(
          Title: null,
          Message: null,
          MajorPercentage: MajorPercentage,
          ProgressLabelMajor: string.Format( "Document {0} / {1}", Count, TotalDocs )
        );
                
      }

      this.tvTreeView.ExpandAll();

      this.tvTreeView.EndUpdate();

      if( MacroscopePreferencesManager.GetShowProgressDialogues() )
      {
        ProgressForm.DoClose();
      }
      
      ProgressForm.Dispose();
      
    }

    /**************************************************************************/

    protected override void RenderTreeView (
      MacroscopeDocument msDoc,
      string Url
    )
    {

      TreeNode CurrentNode = this.tvTreeView.Nodes[ 0 ];
      string sPath = string.Join( "/", msDoc.GetHostname(), msDoc.GetPath() );

      if( sPath != null )
      {

        DebugMsg( string.Format( "HIERARCHY PATH: {0}", sPath ) );

        List<String> lElements = new List<string> ( sPath.Split( new char[] {
          '/'
        }, StringSplitOptions.RemoveEmptyEntries ) );

        for( int i = 0 ; i < lElements.Count ; i++ )
        {

          string sElementName = lElements[ i ];

          if( CurrentNode != null )
          {

            if( CurrentNode.Nodes.ContainsKey( sElementName ) )
            {

              CurrentNode = CurrentNode.Nodes[ sElementName ];

            }
            else
            {

              TreeNode nNewNode = CurrentNode.Nodes.Add( sElementName );
              nNewNode.Name = sElementName;
              nNewNode.Text = sElementName;
              nNewNode.Tag = Url;

              CurrentNode = nNewNode;

            }

          }

        }

      }
      else
      {
        DebugMsg( string.Format( "HIERARCHY ERROR: {0}", Url ) );
      }

    }

    /**************************************************************************/

  }

}
