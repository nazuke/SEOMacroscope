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
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;

namespace SEOMacroscope
{

	public partial class MacroscopeDocumentDetails : MacroscopeUserControl
	{
		
		/**************************************************************************/

		MacroscopeUrlLoader UrlLoader;

		/**************************************************************************/
		
		public MacroscopeDocumentDetails ()
		{

			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();

			// Control Properties
			listViewDocumentInfo.Dock = DockStyle.Fill;
			textBoxHttpHeaders.Dock = DockStyle.Fill;
			listViewHrefLang.Dock = DockStyle.Fill;
			listViewLinksIn.Dock = DockStyle.Fill;
			listViewLinksOut.Dock = DockStyle.Fill;
			listViewImages.Dock = DockStyle.Fill;
			listViewStylesheets.Dock = DockStyle.Fill;
			listViewJavascripts.Dock = DockStyle.Fill;

			UrlLoader = new MacroscopeUrlLoader ();
			listViewDocInfo.Dock = DockStyle.Fill;

		}

		/**************************************************************************/
		
		void MacroscopeDocumentDetailsLoad ( object sender, EventArgs e )
		{
		}

		/**************************************************************************/

		public void ClearData ()
		{
			this.listViewDocumentInfo.Items.Clear();
			this.textBoxHttpHeaders.Text = "";
			this.listViewHrefLang.Items.Clear();
			this.listViewImages.Items.Clear();
			this.listViewJavascripts.Items.Clear();
			this.listViewLinksIn.Items.Clear();
			this.listViewLinksOut.Items.Clear();
			this.listViewStylesheets.Items.Clear();
			this.pictureBoxDocumentDetailsImage.Image = null;
			this.listViewDocInfo.Columns.Clear();
		}

		/**************************************************************************/

		public Boolean UpdateDisplay ( MacroscopeJobMaster msJobMaster, string sUrl )
		{

			// TODO: This blows up if the page is from a redirect. Probably need to use the original URL

			MacroscopeDocumentCollection DocCollection = msJobMaster.GetDocCollection();
			MacroscopeDocument msDoc = DocCollection.GetDocument( sUrl );

			if( msDoc != null )
			{
				
				if( this.InvokeRequired )
				{
					this.Invoke(
						new MethodInvoker (
							delegate
							{
								this.UpdateDocumentDetailsDisplay( msJobMaster, msDoc );
							}
						)
					);
				}
				else
				{
					this.UpdateDocumentDetailsDisplay( msJobMaster, msDoc );
				}
				
				return( true );
				
			}
			else
			{
				
				return( false );
			
			}

		}

		/**************************************************************************/

		void UpdateDocumentDetailsDisplay ( MacroscopeJobMaster msJobMaster, MacroscopeDocument msDoc )
		{
			Cursor.Current = Cursors.WaitCursor;
			this.RenderDocumentDetails( msDoc );
			this.RenderDocumentHttpHeaders( msDoc );
			this.RenderDocumentHrefLang( msDoc, msJobMaster.GetLocales(), msJobMaster.GetDocCollection() );
			this.RenderListViewHyperlinksIn( msDoc );
			this.RenderListViewHyperlinksOut( msDoc );
			this.RenderDocumentPreview( msDoc );
			Cursor.Current = Cursors.Default;
		}

		/**************************************************************************/

		void RenderDocumentDetails ( MacroscopeDocument msDoc )
		{

			ListView lvListView = this.listViewDocumentInfo;

			lvListView.Items.Clear();

			List<KeyValuePair<string,string>> lItems = msDoc.DetailDocumentDetails();

			for( int i = 0 ; i < lItems.Count ; i++ )
			{

				KeyValuePair<string,string> kvItem = lItems[ i ];

				//DebugMsg( string.Format( "RenderDocumentDetails: {0} => {1}", kvItem.Key, kvItem.Value ) );
				
				try
				{
					ListViewItem lvItem = new ListViewItem ( kvItem.Key );
					lvItem.Name = kvItem.Key;
					lvItem.SubItems.Add( kvItem.Value );
					lvListView.Items.Add( lvItem );
				}
				catch( Exception ex )
				{
					DebugMsg( string.Format( "RenderDocumentDetails: {0}", ex.Message ) );
				}
				
			}

		}

		/**************************************************************************/
		
		void CallbackDocumentDetailsContextMenuStripCopyRowsClick ( object sender, EventArgs e )
		{
			this.CopyListViewRowsTextToClipboard( this.listViewDocumentInfo );
		}

		void CallbackDocumentDetailsContextMenuStripCopyValuesClick ( object sender, EventArgs e )
		{
			this.CopyListViewValuesTextToClipboard( this.listViewDocumentInfo );
		}

		/**************************************************************************/

		void RenderDocumentHttpHeaders ( MacroscopeDocument msDoc )
		{
			this.textBoxHttpHeaders.Text = string.Join(
				"",
				msDoc.GetHttpStatusLineAsText(),
				msDoc.GetHttpHeadersAsText()
			);
		}

		/**************************************************************************/
				
		void RenderDocumentHrefLang ( MacroscopeDocument msDoc, Dictionary<string,string> htLocales, MacroscopeDocumentCollection DocCollection )
		{

			ListView lvListView = this.listViewHrefLang;

			lvListView.Items.Clear();
			lvListView.Columns.Clear();

			{
				lvListView.Columns.Add( "URL", "URL" );
				lvListView.Columns.Add( "Site Locale", "Site Locale" );
				lvListView.Columns.Add( "Title", "Title" );
			}

			string sKeyUrl = msDoc.GetUrl();

			if( msDoc.GetIsHtml() )
			{

				Dictionary<string,MacroscopeHrefLang> htHrefLangs = msDoc.GetHrefLangs();

				if( htHrefLangs != null )
				{

					{

						ListViewItem lvItem = new ListViewItem ( sKeyUrl );

						lvItem.Name = sKeyUrl;
								
						lvItem.SubItems.Add( "" );
						lvItem.SubItems.Add( "" );
						lvItem.SubItems.Add( "" );

						lvItem.SubItems[ 0 ].Text = msDoc.GetUrl();
						lvItem.SubItems[ 1 ].Text = msDoc.GetLocale();
						lvItem.SubItems[ 2 ].Text = msDoc.GetTitle();

						lvListView.Items.Add( lvItem );

					}

					foreach( string sLocale in htLocales.Keys )
					{


						if( sLocale != null )
						{

							if( sLocale == msDoc.GetLocale() )
							{
								continue;
							}

							string sHrefLangUrl = null;
							string sTitle = "";
							
							ListViewItem lvItem = new ListViewItem ( sLocale );
							
							lvItem.Name = sLocale;
													
							lvItem.SubItems.Add( "" );
							lvItem.SubItems.Add( "" );
							lvItem.SubItems.Add( "" );
							
							if( htHrefLangs.ContainsKey( sLocale ) )
							{

								MacroscopeHrefLang msHrefLang = ( MacroscopeHrefLang )htHrefLangs[ sLocale ];

								if( msHrefLang != null )
								{

									sHrefLangUrl = msHrefLang.GetUrl();

									if( DocCollection.DocumentExists( sHrefLangUrl ) )
									{
										sTitle = DocCollection.GetDocument( sHrefLangUrl ).GetTitle();
									}

								}

							}

							lvItem.SubItems[ 1 ].Text = sLocale;
							lvItem.SubItems[ 2 ].Text = sTitle;

							if( sHrefLangUrl != null )
							{
								lvItem.SubItems[ 0 ].ForeColor = Color.Blue;
								lvItem.SubItems[ 0 ].Text = sHrefLangUrl;
							}
							else
							{
								lvItem.SubItems[ 0 ].ForeColor = Color.Red;
								lvItem.SubItems[ 0 ].Text = "MISSING";

							}

							lvListView.Items.Add( lvItem );
							
						}
								
					}

				}

				lvListView.AutoResizeColumns( ColumnHeaderAutoResizeStyle.ColumnContent );

				lvListView.Columns[ "URL" ].Width = 300;
				lvListView.Columns[ "Site Locale" ].Width = 100;
				lvListView.Columns[ "Title" ].Width = 300;

			}

		}

		/**************************************************************************/

		void RenderListViewHyperlinksIn ( MacroscopeDocument msDoc )
		{

			ListView lvListView = this.listViewLinksIn;
			MacroscopeHyperlinksIn hlHyperlinksIn = ( MacroscopeHyperlinksIn )msDoc.GetHyperlinksIn();

			lvListView.Items.Clear();

			foreach( string sUrlOrigin in hlHyperlinksIn.IterateKeys() )
			{

				foreach( MacroscopeHyperlinkIn hlHyperlinkIn in hlHyperlinksIn.IterateLinks( sUrlOrigin ) )
				{

					string sPairKey = string.Join( "::", sUrlOrigin, hlHyperlinkIn.GetLinkId().ToString() );

					if( lvListView.Items.ContainsKey( sPairKey ) )
					{
							
						try
						{

							ListViewItem lvItem = lvListView.Items[ sPairKey ];
							lvItem.SubItems[ 0 ].Text = hlHyperlinkIn.GetHyperlinkType().ToString();
							lvItem.SubItems[ 1 ].Text = hlHyperlinkIn.GetUrlOrigin();
							lvItem.SubItems[ 2 ].Text = hlHyperlinkIn.GetUrlTarget();
							lvItem.SubItems[ 3 ].Text = hlHyperlinkIn.GetLinkText();
							lvItem.SubItems[ 4 ].Text = hlHyperlinkIn.GetAltText();

						}
						catch( Exception ex )
						{
							DebugMsg( string.Format( "RenderListViewHyperlinksIn 1: {0}", ex.Message ) );
						}

					}
					else
					{
							
						try
						{

							ListViewItem lvItem = new ListViewItem ( sPairKey );

							lvItem.Name = sPairKey;

							lvItem.SubItems[ 0 ].Text = hlHyperlinkIn.GetHyperlinkType().ToString();
							lvItem.SubItems.Add( hlHyperlinkIn.GetUrlOrigin() );						
							lvItem.SubItems.Add( hlHyperlinkIn.GetUrlTarget() );
							lvItem.SubItems.Add( hlHyperlinkIn.GetLinkText() );
							lvItem.SubItems.Add( hlHyperlinkIn.GetAltText() );

							lvListView.Items.Add( lvItem );

						}
						catch( Exception ex )
						{
							DebugMsg( string.Format( "RenderListViewHyperlinksIn 2: {0}", ex.Message ) );
						}

					}

				}

			}

		}

		/**************************************************************************/

		void RenderListViewHyperlinksOut ( MacroscopeDocument msDoc )
		{

			ListView lvListView = this.listViewLinksOut;
			MacroscopeHyperlinksOut hlHyperlinksOut = ( MacroscopeHyperlinksOut )msDoc.GetHyperlinksOut();

			lvListView.Items.Clear();

			lock( hlHyperlinksOut )
			{
				
				foreach( string sUrlOrigin in hlHyperlinksOut.IterateKeys() )
				{

					//foreach( MacroscopeHyperlinkOut hlHyperlinkOut in hlHyperlinksOut.GetLinks( sUrlOrigin ) ) {

						
					foreach( MacroscopeHyperlinkOut hlHyperlinkOut in hlHyperlinksOut.IterateLinks( sUrlOrigin ) )
					{
						
						
						
						
						
						string sKey = hlHyperlinkOut.GetGuid();

						//DebugMsg( string.Format( "RenderListViewHyperlinksOut sKey: {0} :: {1}", sKey, hlHyperlinkOut.GetUrlTarget() ) );

						if( lvListView.Items.ContainsKey( sKey ) )
						{
							
							try
							{

								ListViewItem lvItem = lvListView.Items[ sKey ];
								lvItem.SubItems[ 0 ].Text = hlHyperlinkOut.GetHyperlinkType().ToString();
								lvItem.SubItems[ 1 ].Text = hlHyperlinkOut.GetUrlOrigin();
								lvItem.SubItems[ 2 ].Text = hlHyperlinkOut.GetUrlTarget();
								lvItem.SubItems[ 3 ].Text = hlHyperlinkOut.GetLinkText();
								lvItem.SubItems[ 4 ].Text = hlHyperlinkOut.GetAltText();
								lvItem.SubItems[ 5 ].Text = hlHyperlinkOut.GetFollow().ToString();

							}
							catch( Exception ex )
							{
								DebugMsg( string.Format( "RenderListViewHyperlinksOut 1: {0}", ex.Message ) );
							}

						}
						else
						{
							
							try
							{

								ListViewItem lvItem = new ListViewItem ( sKey );

								lvItem.Name = sKey;

								lvItem.SubItems[ 0 ].Text = hlHyperlinkOut.GetHyperlinkType().ToString();
								lvItem.SubItems.Add( hlHyperlinkOut.GetUrlOrigin() );						
								lvItem.SubItems.Add( hlHyperlinkOut.GetUrlTarget() );
								lvItem.SubItems.Add( hlHyperlinkOut.GetLinkText() );
								lvItem.SubItems.Add( hlHyperlinkOut.GetAltText() );						
								lvItem.SubItems.Add( hlHyperlinkOut.GetFollow().ToString() );

								lvListView.Items.Add( lvItem );

							}
							catch( Exception ex )
							{
								DebugMsg( string.Format( "RenderListViewHyperlinksOut 2: {0}", ex.Message ) );
							}

						}

					}

				}
				
			}
		
		}

		/** Document Preview ******************************************************/

		void RenderDocumentPreview ( MacroscopeDocument msDoc )
		{
			
			if( msDoc.GetIsImage() )
			{
				this.RenderImagePreview( msDoc );
			}
			else
			{
				ClearDocumentPreviewListView();
			}

		}
		
		/** Clear Document Preview ListView ***************************************/

		void ClearDocumentPreviewListView ()
		{
			ListView lvListView = this.listViewDocInfo;
			lvListView.Clear();
			this.pictureBoxDocumentDetailsImage.Image = null;
		}

		/** Image Preview *********************************************************/

		void RenderImagePreview ( MacroscopeDocument msDoc )
		{
			if( msDoc.GetIsImage() )
			{
				MemoryStream msStream = this.UrlLoader.LoadMemoryStreamFromUrl( msDoc.GetUrl() );
				if( msStream != null )
				{
					this.pictureBoxDocumentDetailsImage.Image = Image.FromStream( msStream );
					this.RenderImagePreviewListView( msDoc, this.pictureBoxDocumentDetailsImage.Image );
				}
			}
			else
			{
				try
				{
					this.pictureBoxDocumentDetailsImage.Image = null;
				}
				catch( Exception ex )
				{
					MessageBox.Show( ex.Message );
				}
			}
		}

		void RenderImagePreviewListView ( MacroscopeDocument msDoc, Image iImage )
		{

			ListView lvListView = this.listViewDocInfo;
			
			lvListView.Clear();

			lvListView.Columns.Add( "Property" );
			lvListView.Columns.Add( "Value" );

			{
				ListViewItem lvItem = new ListViewItem ( "Format" );
				lvItem.SubItems[ 0 ].Text = "PixelFormat";
				lvItem.SubItems.Add( msDoc.GetMimeType() );
				lvListView.Items.Add( lvItem );
			}

			{
				ListViewItem lvItem = new ListViewItem ( "WIDTH" );
				lvItem.SubItems[ 0 ].Text = "Width";
				lvItem.SubItems.Add( iImage.Width.ToString() );
				lvListView.Items.Add( lvItem );
			}

			{
				ListViewItem lvItem = new ListViewItem ( "HEIGHT" );
				lvItem.SubItems[ 0 ].Text = "Height";
				lvItem.SubItems.Add( iImage.Height.ToString() );
				lvListView.Items.Add( lvItem );
			}

			lvListView.Columns[ 0 ].Width = 150;
			lvListView.Columns[ 1 ].Width = 150;
			
		}

		/**************************************************************************/
	}

}
