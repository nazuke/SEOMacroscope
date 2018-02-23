/*

  This file is part of SEOMacroscope.

  Copyright 2018 Jason Holland.

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
using System.Windows.Forms;
using System.Xml;

namespace SEOMacroscope
{

  public partial class MacroscopeMainForm : Form, IMacroscopeTaskController
  {

    /** Generators Save Dialogue Boxes ****************************************/

    private void CallbackSaveGeneratorSitemapXml ( object sender, EventArgs e )
    {
      
      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "Sitemap XML files (*.xml)|*.xml|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xml";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Sitemap.xml";

      MacroscopeSitemapGenerator SitemapGenerator;
      
      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Pathname = Dialog.FileName;
        
        SitemapGenerator = new MacroscopeSitemapGenerator (
          NewDocCollection: this.JobMaster.GetDocCollection()
        );

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: 256 ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            SitemapGenerator.WriteSitemapXml( NewPath: Pathname );
            Cursor.Current = Cursors.Default;
          }
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Sitemap XML", ex.Message );
          //GC.Collect();
        }
        catch( XmlException ex )
        {
          this.DialogueBoxError( "Error saving Sitemap XML", ex.Message );
          //GC.Collect();
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Sitemap XML", ex.Message );
          //GC.Collect();
        }
        
      }
      
      Dialog.Dispose();

      //GC.Collect();
            
    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveGeneratorSitemapXmlPerHost ( object sender, EventArgs e )
    {
      
      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "Sitemap XML files (*.xml)|*.xml|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "xml";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Sitemap.xml";

      MacroscopeSitemapGenerator SitemapGenerator;
      
      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Pathname = Dialog.FileName;
        
        SitemapGenerator = new MacroscopeSitemapGenerator (
          NewDocCollection: this.JobMaster.GetDocCollection()
        );

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: 256 ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            SitemapGenerator.WriteSitemapXmlPerHost( NewPath: Pathname );
            Cursor.Current = Cursors.Default;
          }
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Sitemap XML", ex.Message );
          //GC.Collect();
        }
        catch( XmlException ex )
        {
          this.DialogueBoxError( "Error saving Sitemap XML", ex.Message );
          //GC.Collect();
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Sitemap XML", ex.Message );
          //GC.Collect();
        }
        
      }

      //GC.Collect();

      Dialog.Dispose();

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveGeneratorSitemapText ( object sender, EventArgs e )
    {
      
      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "Sitemap Text files (*.txt)|*.txt|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "txt";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Sitemap.txt";

      MacroscopeSitemapGenerator SitemapGenerator;
      
      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Pathname = Dialog.FileName;
        
        SitemapGenerator = new MacroscopeSitemapGenerator (
          NewDocCollection: this.JobMaster.GetDocCollection()
        );

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: 256 ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            SitemapGenerator.WriteSitemapText( NewPath: Pathname );
            Cursor.Current = Cursors.Default;
          }
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Sitemap Text", ex.Message );
          //GC.Collect();
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Sitemap Text", ex.Message );
          //GC.Collect();
        }
        
      }

      Dialog.Dispose();

    }

    /** -------------------------------------------------------------------- **/

    private void CallbackSaveGeneratorSitemapTextPerHost ( object sender, EventArgs e )
    {
      
      SaveFileDialog Dialog = new SaveFileDialog ();
      Dialog.Filter = "Sitemap Text files (*.txt)|*.txt|All files (*.*)|*.*";
      Dialog.FilterIndex = 2;
      Dialog.RestoreDirectory = true;
      Dialog.DefaultExt = "txt";
      Dialog.AddExtension = true;
      Dialog.FileName = "Macroscope-Sitemap.txt";

      MacroscopeSitemapGenerator SitemapGenerator;
      
      if( Dialog.ShowDialog() == DialogResult.OK )
      {

        string Pathname = Dialog.FileName;
        
        SitemapGenerator = new MacroscopeSitemapGenerator (
          NewDocCollection: this.JobMaster.GetDocCollection()
        );

        try
        {
          if( Macroscope.MemoryGuard( RequiredMegabytes: 256 ) )
          {
            Cursor.Current = Cursors.WaitCursor;
            SitemapGenerator.WriteSitemapTextPerHost( NewPath: Pathname );
            Cursor.Current = Cursors.Default;
          }
        }
        catch( MacroscopeInsufficientMemoryException ex )
        {
          this.DialogueBoxError( "Error saving Sitemap Text", ex.Message );
          //GC.Collect();
        }
        catch( Exception ex )
        {
          this.DialogueBoxError( "Error saving Sitemap Text", ex.Message );
          //GC.Collect();
        }
        
      }

      Dialog.Dispose();

    }

    /**************************************************************************/

  }

}
