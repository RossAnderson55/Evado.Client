/***************************************************************************************
 * <copyright file="Evado.UniForm.Model\EuArticle.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2013 - 2021 EVADO HOLDING PTY. LTD..  All rights reserved.
 *     
 *      The use and distribution terms for this software are contained in the file
 *      named \license.txt, which can be found in the root of this distribution.
 *      By using this software in any fashion, you are agreeing to be bound by the
 *      terms of this license.
 *     
 *      You must not remove this notice, or any other, from this software.
 *     
 * </copyright>
 * 
 * Description: 
 *  This class contains the EuArticle data object.
 *
 ****************************************************************************************/
using System;
using Newtonsoft.Json;

namespace Evado.UniForm.Model
{
  /// <summary>
  /// This class defines the method parameter object structure.
  /// </summary>
  [Serializable]
  public class EuArticle
  {
    #region class initialisation methods

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameter and value.
    /// </summary>
    //  ---------------------------------------------------------------------------------
    public EuArticle( )
    {
    }

    //  =================================================================================
    /// <summary>
    /// This method initialises the class with parameter and value.
    /// </summary>
    /// <param name="ArticleId">String: article identifier</param>
    /// <param name="Title">String: article title</param>
    /// <param name="Author">String: Author name</param>
    /// <param name="HeaderImageUrl">String:HeaderImageUrl</param>
    /// <param name="Body">String:article body </param>
    //  ---------------------------------------------------------------------------------
    public EuArticle ( 
      String ArticleId, 
      String Title, 
      String Author, 
      String HeaderImageUrl, 
      String Body )
    {
      this.ArticleId = ArticleId;
      this.Title = Title;
      this.Author = Author;
      this.HeaderImageUrl = HeaderImageUrl;
      this.Body = Body;
    }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Class PropertyList

    /// <summary>
    /// This property contains Article identifier
    /// </summary>
    [JsonProperty ( "aid" )]
    public String ArticleId { get; set; }

    /// <summary>
    /// This property contains the article title.
    /// </summary>
    [JsonProperty ( "t" )]
    public String Title { get; set; }

    /// <summary>
    /// This property contains name of the article's author
    /// </summary>
    [JsonProperty ( "an" )]
    public String Author { get; set; }

    /// <summary>
    /// This property contains absoluate URL to the header image.
    /// Null value indicates that there is no image for this article.
    /// </summary>
    [JsonProperty ( "iurl" )]
    public String HeaderImageUrl { get; set; }

    /// <summary>
    /// This Property contains the article body content. Using MarkDown document formatting.
    /// </summary>
    [JsonProperty ( "body" )]
    public String Body { get; set; }

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion
  } 
}//END namespace