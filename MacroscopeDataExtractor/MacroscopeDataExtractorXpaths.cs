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
using System.Text;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Xml.XPath;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDataExtractorXpaths.cs.
  /// </summary>

  public class MacroscopeDataExtractorXpaths : Macroscope
  {

    /**************************************************************************/

    private Boolean Enabled;

    private int Max;

    private List<MacroscopeConstants.ActiveInactive> ExtractActiveInactive;

    private List<KeyValuePair<string, MacroscopeDataExtractorXpathsExpression>> ExtractXpaths;

    /**************************************************************************/
    
    public MacroscopeDataExtractorXpaths ( int Size )
    {

      this.Disable();
      
      this.Max = Size;

      this.ExtractActiveInactive = new List<MacroscopeConstants.ActiveInactive> ( this.Max );

      this.ExtractXpaths = new List<KeyValuePair<string, MacroscopeDataExtractorXpathsExpression>> ( this.Max );

      for( int Slot = 0 ; Slot < this.Max ; Slot++ )
      {
        
        this.ExtractActiveInactive.Add( MacroscopeConstants.ActiveInactive.INACTIVE );
        
        this.ExtractXpaths.Add(
          new KeyValuePair<string, MacroscopeDataExtractorXpathsExpression> (
            string.Format( "XPathExpression {0}", Slot + 1 ),
            null
          )
        );

      }

    }

    /**************************************************************************/

    public void Disable ()
    {
      this.Enabled = false;
    }
    
    public void SetEnabled ()
    {
      this.Enabled = true;
    }
    
    public Boolean IsEnabled ()
    {
      return( this.Enabled );
    }
    
    /**************************************************************************/

    public int GetSize ()
    {
      return( this.Max );
    }

    /**************************************************************************/

    public void SetActiveInactive ( int Slot, MacroscopeConstants.ActiveInactive State )
    {
      this.ExtractActiveInactive[ Slot ] = State;
    }
    
    public MacroscopeConstants.ActiveInactive GetActiveInactive ( int Slot )
    {
      return( this.ExtractActiveInactive[ Slot ] );
    }

    /**************************************************************************/

    public void SetXpath (
      int Slot,
      string XpathLabel,
      string XpathString,
      MacroscopeConstants.XpathExtractorType ExtractorType
    )
    {

      XPathExpression Expression;
      MacroscopeDataExtractorXpathsExpression DataExtractorXpathsExpression;
      KeyValuePair<string, MacroscopeDataExtractorXpathsExpression> ExpressionSlot;

      if( SyntaxCheckXpath( XpathString: XpathString ) )
      {
        
        Expression = XPathExpression.Compile( XpathString );

        DataExtractorXpathsExpression = new MacroscopeDataExtractorXpathsExpression (
          NewLabel: XpathLabel,
          NewExpression: Expression,
          NewExtractorType: ExtractorType
        );

        ExpressionSlot = new KeyValuePair<string, MacroscopeDataExtractorXpathsExpression> (
          XpathLabel,
          DataExtractorXpathsExpression
        );

        this.ExtractXpaths[ Slot ] = ExpressionSlot;
        
        this.SetEnabled();
        
      }
      else
      {

        throw( new FormatException ( "Invalid XPath expression" ) );
        
      }

    }

    /**************************************************************************/
        
    public string GetLabel ( int Slot )
    {

      return( this.ExtractXpaths[ Slot ].Key );

    }
    
    /**************************************************************************/
        
    public XPathExpression GetXpath ( int Slot )
    {

      XPathExpression Expression = this.ExtractXpaths[ Slot ].Value.Expression;

      return( Expression );

    }
    /**************************************************************************/

    public MacroscopeConstants.XpathExtractorType GetExtractorType ( int Slot )
    {

      MacroscopeConstants.XpathExtractorType ExtractorType = this.ExtractXpaths[ Slot ].Value.ExtractorType;

      return( ExtractorType );

    }

    /**************************************************************************/

    public List<KeyValuePair<string, string>> AnalyzeHtml ( string Html )
    {

      List<KeyValuePair<string, string>> ResultList = null; 
      HtmlDocument HtmlDoc = new HtmlDocument ();

      HtmlDoc.LoadHtml( html: Html );

      try
      {
        ResultList = AnalyzeHtmlDoc( HtmlDoc: HtmlDoc );
      }
      catch( Exception ex )
      {
        this.DebugMsg( string.Format( "AnalyzeHtml: {0}", ex.Message ) );
      }

      return( ResultList );
          
    }

    /** -------------------------------------------------------------------- **/

    public List<KeyValuePair<string, string>> AnalyzeHtmlDoc ( HtmlDocument HtmlDoc )
    {

      List<KeyValuePair<string, string>> ResultList = new List<KeyValuePair<string, string>> ( 8 );

      if( this.IsEnabled() )
      {

        for( int Slot = 0 ; Slot < this.Max ; Slot++ )
        {

          HtmlNodeCollection NodeSet;
          string Label = this.ExtractXpaths[ Slot ].Key;
          string Expression = this.ExtractXpaths[ Slot ].Value.Expression.Expression;
          MacroscopeConstants.XpathExtractorType ExtractorType = this.ExtractXpaths[ Slot ].Value.ExtractorType;
          
          
          this.DebugMsg(
            string.Format(
              "SLOT KEY: {0} => {1} => {2} => {3}",
              Slot,
              Label,
              Expression,
              ExtractorType
            )
          );

          NodeSet = HtmlDoc.DocumentNode.SelectNodes( xpath: Expression );

          if( 
            ( NodeSet != null )
            && ( NodeSet.Count > 0 ) )
          {

            foreach( HtmlNode Node in NodeSet )
            {

              KeyValuePair<string, string> Pair;
              string Text;
              
              switch( ExtractorType )
              {
              
                case MacroscopeConstants.XpathExtractorType.OUTERHTML:
                  Text = Node.OuterHtml;
                  Pair = new KeyValuePair<string, string> ( key: Label, value: Text );
                  ResultList.Add( item: Pair );
                  break;
                  
                case MacroscopeConstants.XpathExtractorType.INNERHTML:
                  Text = Node.InnerHtml;
                  Pair = new KeyValuePair<string, string> ( key: Label, value: Text );
                  ResultList.Add( item: Pair );
                  break;

                case MacroscopeConstants.XpathExtractorType.INNERTEXT:
                  Text = Node.InnerText;
                  Pair = new KeyValuePair<string, string> ( key: Label, value: Text );
                  ResultList.Add( item: Pair );
                  break;

                default:
                  break;

              }

            }

          }

        }

      }

      return( ResultList );

    }

    /**************************************************************************/
    
    public static Boolean SyntaxCheckXpath ( string XpathString )
    {
      
      Boolean IsValid = false;

      try
      {
        
        XPathExpression expression = XPathExpression.Compile( XpathString );

        IsValid = true;

      }
      catch( XPathException ex )
      {

        DebugMsg( ex.Message, true );

        IsValid = false;
        
      }
      catch( Exception ex )
      {

        DebugMsg( ex.Message, true );

        IsValid = false;
        
      }

      return( IsValid );

    }

    /**************************************************************************/

  }

}
