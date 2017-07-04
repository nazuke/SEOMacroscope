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
using System.Collections;
using HtmlAgilityPack;

namespace SEOMacroscope
{

  /// <summary>
  /// Description of MacroscopeDataExtractorCssSelectors.cs.
  /// </summary>

  public class MacroscopeDataExtractorCssSelectors : MacroscopeDataExtractor
  {

    /**************************************************************************/

    private List<KeyValuePair<string, MacroscopeDataExtractorExpression>> ExtractCssSelectors;

    /**************************************************************************/
    
    public MacroscopeDataExtractorCssSelectors ( int Size )
      : base( Size: Size)
    {

      this.ExtractCssSelectors = new List<KeyValuePair<string, MacroscopeDataExtractorExpression>> ( this.GetSize() );

      for( int Slot = 0 ; Slot < this.GetSize() ; Slot++ )
      {
        
        this.ExtractActiveInactive.Add( MacroscopeConstants.ActiveInactive.INACTIVE );
        
        MacroscopeDataExtractorExpression Expression;
          
        Expression = new MacroscopeDataExtractorExpression ( 
          NewLabel: "",
          NewExpression: "",
          NewExtractorType: MacroscopeConstants.DataExtractorType.INNERTEXT
        );

        this.ExtractCssSelectors.Add(
          new KeyValuePair<string, MacroscopeDataExtractorExpression> (
            string.Format( "CssSelector{0}", Slot + 1 ),
            Expression
          )
        );

      }

    }

    /**************************************************************************/

    public void SetCssSelector (
      int Slot,
      string CssSelectorLabel,
      string CssSelectorString,
      MacroscopeConstants.DataExtractorType ExtractorType
    )
    {

      MacroscopeDataExtractorExpression DataExtractorCssSelectorExpression;
      KeyValuePair<string, MacroscopeDataExtractorExpression> ExpressionSlot;

      if( 
        ( !string.IsNullOrEmpty( CssSelectorString ) )
        && ( SyntaxCheckCssSelector( CssSelectorString: CssSelectorString ) ) )
      {

        DataExtractorCssSelectorExpression = new MacroscopeDataExtractorExpression (
          NewLabel: CssSelectorLabel,
          NewExpression: CssSelectorString,
          NewExtractorType: ExtractorType
        );

        ExpressionSlot = new KeyValuePair<string, MacroscopeDataExtractorExpression> (
          CssSelectorLabel,
          DataExtractorCssSelectorExpression
        );

        this.ExtractCssSelectors[ Slot ] = ExpressionSlot;
        
        this.SetEnabled();

      }

    }

    /**************************************************************************/
        
    public string GetLabel ( int Slot )
    {
      return( this.ExtractCssSelectors[ Slot ].Key );
    }
    
    /**************************************************************************/
        
    public string GetCssSelector ( int Slot )
    {

      string Expression = this.ExtractCssSelectors[ Slot ].Value.Expression;
      
      return( Expression );

    }
    
    /**************************************************************************/

    public MacroscopeConstants.DataExtractorType GetExtractorType ( int Slot )
    {

      MacroscopeConstants.DataExtractorType ExtractorType = this.ExtractCssSelectors[ Slot ].Value.ExtractorType;

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

      List<KeyValuePair<string, string>> ResultList = new List<KeyValuePair<string, string>> ( 32 );

      if( this.IsEnabled() )
      {

        for( int Slot = 0 ; Slot < this.GetSize() ; Slot++ )
        {

          IList<HtmlNode> NodeSet;
          string Label = this.ExtractCssSelectors[ Slot ].Key;
          string Expression = this.ExtractCssSelectors[ Slot ].Value.Expression;
          MacroscopeConstants.DataExtractorType ExtractorType = this.ExtractCssSelectors[ Slot ].Value.ExtractorType;

          if( this.GetActiveInactive( Slot ).Equals( MacroscopeConstants.ActiveInactive.INACTIVE ) )
          {
            continue;
          }
          else
          if( !SyntaxCheckCssSelector( CssSelectorString: Expression ) )
          {
            continue;
          }

          NodeSet = HtmlDoc.QuerySelectorAll( Expression );

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
              
                case MacroscopeConstants.DataExtractorType.OUTERHTML:
                  Text = Node.OuterHtml;
                  Pair = new KeyValuePair<string, string> ( key: Label, value: Text );
                  ResultList.Add( item: Pair );
                  break;
                  
                case MacroscopeConstants.DataExtractorType.INNERHTML:
                  Text = Node.InnerHtml;
                  Pair = new KeyValuePair<string, string> ( key: Label, value: Text );
                  ResultList.Add( item: Pair );
                  break;

                case MacroscopeConstants.DataExtractorType.INNERTEXT:

                  Text = Node.InnerText;
                  
                  if( MacroscopePreferencesManager.GetExtractorCleanWhiteSpace() )
                  {
                    Text = this.CleanWhiteSpace( Text: Text );
                  }

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
    
    public static Boolean SyntaxCheckCssSelector ( string CssSelectorString )
    {
      
      Boolean IsValid = false;
      
      try
      {

        HtmlDocument doc = new HtmlDocument ();

        doc.LoadHtml( "<html><head><title>title</title></head><body></body></html>" );

        HtmlNode Node = doc.QuerySelector( CssSelectorString );

        IsValid = true;

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
