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

  public class MacroscopeDisplayHierarchy : MacroscopeDisplayTreeView
  {

    /**************************************************************************/

    static Boolean ListViewConfigured = false;

    /**************************************************************************/

    public MacroscopeDisplayHierarchy ( MacroscopeMainForm MainFormNew, TreeView tvTreeView )
      : base( MainFormNew, tvTreeView )
    {

      SuppressDebugMsg = false;

      ConfigureListView();

    }

    /**************************************************************************/

    void ConfigureListView ()
    {

      if( !ListViewConfigured )
      {
        ListViewConfigured = true;
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

    void RenderTreeView (
      MacroscopeDocumentCollection DocCollection,
      List<string> UrlList
    )
    {

      this.tvTreeView.BeginUpdate();

      DebugMsg( string.Format( "HIERARCHY: {0}", "BASE" ) );

      foreach( string sUrl in UrlList )
      {
        MacroscopeDocument msDoc = DocCollection.GetDocument( sUrl );
        this.RenderTreeView( msDoc, sUrl );
      }

      this.tvTreeView.ExpandAll();

      this.tvTreeView.EndUpdate();

    }

    /**************************************************************************/

    protected override void RenderTreeView (
      MacroscopeDocument msDoc,
      string Url
    )
    {

      TreeNode nCurrentNode = this.tvTreeView.Nodes[ 0 ];
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

          if( nCurrentNode != null )
          {

            if( nCurrentNode.Nodes.ContainsKey( sElementName ) )
            {

              nCurrentNode = nCurrentNode.Nodes[ sElementName ];

            }
            else
            {

              TreeNode nNewNode = nCurrentNode.Nodes.Add( sElementName );
              nNewNode.Name = sElementName;
              nNewNode.Text = sElementName;
              nNewNode.Tag = Url;

              nCurrentNode = nNewNode;

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
