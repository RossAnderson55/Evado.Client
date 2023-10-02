using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Evado.Model
{
  public class EvParameters
  {
    /// <summary>
    /// This property contains a list of application parameters.
    /// </summary>
    public List<EvObjectParameter> Parameters { get; set; }

    // ==============================================================================
    /// <summary>
    /// This method returns the parameter object if exists in the parameter list.
    /// </summary>
    /// <param name="Name">The paramater name</param>
    /// <returns>EvApplicationParameter object </returns>
    //  ------------------------------------------------------------------------------
    protected String getParameter ( Object Name )
    {
      string name = Name.ToString ( );
      //
      // If the list is null then return null 
      if ( this.Parameters == null )
      {
        this.Parameters = new List<EvObjectParameter> ( );
        return String.Empty;
      }

      //
      // foreach item in the list return the parameter if the names match.
      //
      foreach ( EvObjectParameter parm in this.Parameters )
      {
        if ( parm.Name.ToLower ( ) == name.ToLower ( ) )
        {
          return parm.Value;
        }
      }

      //
      // return null if the object is not found.
      return String.Empty;
    }//END getParameter method

    // ==============================================================================
    /// <summary>
    /// This method returns the parameter object if exists in the parameter list.
    /// </summary>
    /// <param name="Name">The paramater name</param>
    /// <param name="DataType">EvDataTypes enumeration list value</param>
    /// <param name="Value">String value</param>
    /// <returns>EvApplicationParameter object </returns>
    //  ------------------------------------------------------------------------------

    protected void setParameter ( Object Name, EvDataTypes DataType, String Value )
    {
      EvObjectParameter parameter = new EvObjectParameter ( Name, DataType, Value );
      //
      // If the list is null then return null 
      if ( this.Parameters == null )
      {
        this.Parameters = new List<EvObjectParameter> ( );
      }

      //
      // foreach item in the list return the parameter if the names match.
      //
      foreach ( EvObjectParameter parm in this.Parameters )
      {
        if ( parm.Name == parameter.Name )
        {
          parm.Value = parameter.Value;
          parm.DataType = parameter.DataType;
          return;
        }
      }

      this.Parameters.Add ( parameter );
    }//END getParameter method
  }
}
