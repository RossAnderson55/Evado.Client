/***************************************************************************************
 * <copyright file="EventCodes.cs" company="EVADO HOLDING PTY. LTD.">
 *     
 *      Copyright (c) 2002 - 2023 EVADO HOLDING PTY. LTD.  All rights reserved.
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
 *  This class contains the EventCodes data object.
 *
 ****************************************************************************************/

using System;
using System.Configuration;
using System.Diagnostics;

namespace Evado.Model
{
  /// <summary>
  /// The application EvEvent GroupCodes ennumeration
  /// </summary>
  [Serializable]
  public enum EvEventCodes
  {
    /// <summary>
    /// The enumeration indicates that an enumeration is empty.
    /// </summary>
    Null = 0,

    /// <summary>
    /// Note that the first part of the event code is it's category, which is used to group the
    /// event codes and may be removed when displaying the code to a log or use.
    /// this enumeration indicates that no error event was raised.
    /// </summary>
    Ok = -1,

    /// <summary>
    /// this event code is a general error.
    /// </summary>
    General_Error = -2,

    /// <summary>
    /// this event code is a general error.
    /// </summary>
    Exception_Raised_Error = -3,

    #region User  events
    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a session start error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    User_Action_Event = -10,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a session start error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    User_Authenticated = -11,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a session start error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    User_Session_Start_Completed = -12,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a session end up error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    User_Session_End_Completed = -13,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that user access error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    User_Access_Granted = -14,

    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    #endregion

    #region Application events
    // 
    // ==================================================================================
    /// <summary>
    /// This enumeration defines that an application start up error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Application_Startup_Error = -101,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that an application up error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Application_End_Error = -102,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a session start error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    User_Session_Start_Error = -103,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a session end up error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    User_Session_End_Error = -104,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that start up error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Application_UserId_Error = -105,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a licencing error error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Application_Project_License_Error = -106,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that an application error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    User_Authentication_Error = -107,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that user access error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    User_Access_Error = -108,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that an application up error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Application_General_Error = -109,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that an application up error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Application_Exception_Error = -110,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that SAE notification error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Sae_Notification_Error = -120,
    #endregion

    #region Database events

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that general database error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_General_Error = -200,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that database communications error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_Communications_Error = -201,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that database query error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_Query_Error = -202,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a database update error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_Record_Update_Error = -203,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a database lock error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_Record_Lock_Error = -204,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a database unlock error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_Record_UnLock_Error = -205,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that database record delete error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_Record_Delete_Error = -206,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a database record withdrawal error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_Record_Withdrawal = -207,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a database record retrieval error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_Create_Record_Error = -208,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a database record retrieval error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_Record_Retrieval_Error = -209,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a database record retrieval error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_Query_List_Empty = -210,
    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a database update error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_Update_Error = -211,

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a database delete error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Database_0_records_deleted = -212, 
    #endregion

    #region Object events

    // ==================================================================================
    /// <summary>
    /// This enumeration defines that a general application object error event occured.
    /// </summary>
    /// // ------------------------------------------------------------------------------
    Object_General_Object_Error = -300,

    /// <summary>
    /// This enumeration defines that an application object error event occured.
    /// </summary>
    Object_Application_Error = -301,

    /// <summary>
    /// This enumeration defines that a session object error event occured.
    /// </summary>
    Object_Session_Error = -302,

    /// <summary>
    /// This enumeration defines that a general project object error event occured.
    /// </summary>
    Object_General_Project_Error = -303,

    /// <summary>
    /// This enumeration defines that a record form object error event occured.
    /// </summary>
    Object_Record_Form_Error = -304,

    /// <summary>
    /// This enumeration defines that a project record object error event occured.
    /// </summary>
    Object_Project_Record_Error = -305,

    /// <summary>
    /// This enumeration defines that a case report form object error event occured.  
    /// </summary>
    Object_Case_Report_Form_Error = -306,

    /// <summary>
    /// This enumeration defines that a subject object error event occured.
    /// </summary>
    Object_Subject_Error = -307,

    /// <summary>
    /// This enumeration defines that an activity object error event occured.
    /// </summary>
    Object_Activity_Error = -308,

    /// <summary>
    /// This enumeration defines that a patient object error event occured.
    /// </summary>
    Object_Patient_Error = -309,

    /// <summary>
    /// This enumeration defines that a user profile object error event occured.
    /// </summary>
    Object_UserProfile_Error = -310,

    /// <summary>
    /// This enumeration defines that a record form object error event occured.
    /// </summary>
    Object_Common_Record_Error = -311,

    /// <summary>
    /// This enumeration defines that a record form object error event occured.
    /// </summary>
    Object_Consent_Record_Error = -312,

    /// <summary>
    /// This enumeration defines that a record form object is null or empty.
    /// </summary>
    Object_Common_Record_Empty = -313,
    /// <summary>
    /// This enumeration defines that trial organisatin error occured.
    /// </summary>
    Trial_Organisation_Error = -314,
    /// <summary>
    /// This enumeration defines that a project organisation error event occured.
    /// </summary>
    Trial_Site_Email_Address_Empty = -315,
    /// <summary>
    /// This enumeration defines that a project organisation error event occured.
    /// </summary>
    Subject_Email_Address_Empty = -316,
    #endregion

    #region Identifier error events

    /// <summary>
    /// This enumeration defines that a general identifier error event occured.
    /// </summary>
    Identifier_General_ID_Error = -400,

    /// <summary>
    /// This enumeration defines that a user identifier error event occured.
    /// </summary>
    Identifier_User_Id_Error = -401,

    /// <summary>
    /// This enumeration defines that a project identifier error event occured.
    /// </summary>
    Identifier_Trial_Id_Error = -402,

    /// <summary>
    /// This enumeration defines that a organization identifier error event occured.
    /// </summary>
    Identifier_Org_Id_Error = -403,

    /// <summary>
    /// This enumeration defines that a primary identifier error event occured.
    /// </summary>
    Identifier_Primary_Id_Error = -404,

    /// <summary>
    /// This enumeration defines that a subject identifier error event occured.
    /// </summary>
    Identifier_Subject_Id_Error = -405,

    /// <summary>
    /// This enumeration defines that a form identifier error event occured.
    /// </summary>
    Identifier_Form_Id_Error = -406,

    /// <summary>
    /// This enumeration defines that a record identifier error event occured.
    /// </summary>
    Identifier_Record_Id_Error = -407,

    /// <summary>
    /// This enumeration defines that a adverse event identifier error event occured.
    /// </summary>
    Identifier_Adverse_Event_Id_Error = -408,

    /// <summary>
    /// This enumeration defines that a visit identifier error event occured.
    /// </summary>
    Identifier_Visit_Id_Error = -409,

    /// <summary>
    /// This enumeration defines that a test identifier error event occured.
    /// </summary>
    Identifier_Test_Id_Error = -410,

    /// <summary>
    /// This enumeration defines that a sample identifier error event occured.
    /// </summary>
    Identifier_Sample_Id_Error = -411,

    /// <summary>
    /// This enumeration defines that a role identifier error event occured.
    /// </summary>
    Identifier_Role_Id_Error = -412,

    /// <summary>
    /// This enumeration defines that a form field identifier error event occured.
    /// </summary>
    Identifier_Form_Field_Id_Error = -413,

    /// <summary>
    /// This enumeration defines that a serious adverse event identifier error event occured.
    /// </summary>
    Identifier_Serious_Adverse_Event_Id_Error = -414,

    /// <summary>
    /// This enumeration defines that a source identifier error event occured.
    /// </summary>
    Identifier_Source_Id_Error = -416,

    /// <summary>
    /// This enumeration defines that a participant identifier error event occured.
    /// </summary>
    Identifier_Patient_Id_Error = -417,

    /// <summary>
    /// This enumeration defines that a record unique identifier error event occured.
    /// </summary>
    Identifier_Record_Unique_Identifier_Error = -418,

    /// <summary>
    /// This enumeration defines that a form field unque identifier error event occured.
    /// </summary>
    Identifier_Form_Field_Unique_Identifier_Error = -419,

    /// <summary>
    /// This enumeration defines that a milestone identifier error event occured.
    /// </summary>
    Identifier_Milestone_Id_Error = -420,

    /// <summary>
    /// This enumeration defines that a activity identifier error event occured.
    /// </summary>
    Identifier_Activity_Id_Error = -421,

    /// <summary>
    /// This enumeration defines that a letter identifier error event occured.
    /// </summary>
    Identifier_External_Id_Error = -422,

    /// <summary>
    /// This enumeration defines that a test item identifier error event occured.
    /// </summary>
    Identifier_TestItem_Id_Error = -423,

    /// <summary>
    /// This enumeration defines that a user common name identifier error event occured.
    /// </summary>
    Identifier_User_Common_Name_Error = -424,

    /// <summary>
    /// This enumeration defines that a service identifier error event occured.
    /// </summary>
    Identifier_Service_Id_Error = -425,

    /// <summary>
    /// This enumeration defines that a customer identifier error event occured.
    /// </summary>
    Identifier_Customer_Id_Error = -426,

    /// <summary>
    /// This enumeration defines that a device identifier error event occured.
    /// </summary>
    Identifier_Device_Id_Error = -427,

    /// <summary>
    /// This enumeration defines that a page EuCommand identifier error event occured.
    /// </summary>
    Identifier_Page_Command_Id_Error = -428,

    /// <summary>
    /// This enumeration defines that a file identifier error event occured.
    /// </summary>
    Identifier_File_Id_Error = -430,

    /// <summary>
    /// This enumeration defines that a file archive identifier error event occured.
    /// </summary>
    Identifier_File_Archive_Id_Error = -431,

    /// <summary>
    /// This enumeration defines that a category identifier error event occured.
    /// </summary>
    Identifier_Category_Error = -432,

    /// <summary>
    /// This enumeration defines that a list identifier error event occured.
    /// </summary>
    Identifier_List_Id_Error = -433,

    /// <summary>
    /// This enumeration defines that a schedule unique identifier error event occured.
    /// </summary>
    Identifier_Schedule_Id_Error = -434,

    /// <summary>
    /// This enumeration defines that a budget unique identifier error event occured.
    /// </summary>
    Identifier_Record_Source_Id_Error = -435,

    /// <summary>
    /// This enumeration defines that a budget unique identifier error event occured.
    /// </summary>
    Identifier_Visit_Source_Id_Error = -436,

    /// <summary>
    /// This enumeration defines that a budget unique identifier error event occured.
    /// </summary>
    Identifier_Billing_Record_Item_Id_Error = -437,

    /// <summary>
    /// This enumeration defines that a budget unique identifier error event occured.
    /// </summary>
    Identifier_Billing_Record_Id_Error = -438,
    /// <summary>
    /// This enumeration defines that a participant GUID error event occured.
    /// </summary>
    Identifier_Patient_Guid_Error = -439,

    /// <summary>
    /// This enumeration defines that a user identifier error event occured.
    /// </summary>
    Identifier_Email_Address_Error = -440,

    /// <summary>
    /// This enumeration defines that a user identifier error event occured.
    /// </summary>
    Identifier_Password_Error = -440,

    /// <summary>
    /// This enumeration defines that a subject identifier error event occured.
    /// </summary>
    Identifier_Group_Id_Error = -441,

    /// <summary>
    /// This enumeration defines that a subject identifier error event occured.
    /// </summary>
    Identifier_Sub_Group_Id_Error = -442,

    /// <summary>
    /// This enumeration defines that a report identifier error event occured.
    /// </summary>
    Identifier_Schedule_Identifier_Error = -443,

    /// <summary>
    /// This enumeration defines that a schedule identifier error event occured.
    /// </summary>
    Identifier_Report_Identifier_Error = -444,
    /// <summary>
    /// This enumeration defines that a activity identifier error event occured.
    /// </summary>
    Identifier_Entity_Id_Error = -445,

    /// <summary>
    /// This enumeration defines that a analysis project identifier error event occured.
    /// </summary>
    Identifier_Analysis_Project_Id_Error = -446,

    /// <summary>
    /// This enumeration defines that a common form type error occured.
    /// </summary>
    Identifier_Common_Form_Type_Error = -447,

    /// <summary>
    /// This enumeration defines that a global unique identifier error event occured.
    /// </summary>
    Identifier_Global_Unique_Identifier_Error = -499,
    #endregion

    #region Data error events

    /// <summary>
    /// This enumeration defines that a general data error event occured.
    /// </summary>
    Data_General_Data_Error = -500,

    /// <summary>
    /// This enumeration defines that a null identifier data error event occured.
    /// </summary>
    Data_Null_Id_Error = -501,

    /// <summary>
    /// This enumeration defines that a duplicate identifier data error event occured.
    /// </summary>
    Data_Duplicate_Id_Error = -502,

    /// <summary>
    /// This enumeration defines that a invalid data error event occured.
    /// </summary>
    Data_InvalidId_Error = -503,

    /// <summary>
    /// This enumeration defines that a general value casting data error event occured.
    /// </summary>
    Data_General_Value_Casting_Error = -504,

    /// <summary>
    /// This enumeration defines that a date casting data error event occured.
    /// </summary>
    Data_Date_Casting_Error = -505,

    /// <summary>
    /// This enumeration defines that a integer casting data error event occured.
    /// </summary>
    Data_Integer_Casting_Error = -506,

    /// <summary>
    /// This enumeration defines that a floating casting data error event occured.
    /// </summary>
    Data_Float_Casting_Error = -507,

    /// <summary>
    /// This enumeration defines that a null data error event occured.
    /// </summary>
    Data_Null_Data_Error = -508,

    /// <summary>
    /// This enumeration defines that a minimum value range data error event occured.
    /// </summary>
    Data_Minimum_Value_Range_Error = -509,

    /// <summary>
    /// This enumeration defines that a maximum value range data error event occured.
    /// </summary>
    Data_Maximum_Value_Range_Error = -510,

    /// <summary>
    /// This enumeration defines that a duplicate field data error event occured.
    /// </summary>
    Data_Duplicate_Field_Error = -511,

    /// <summary>
    /// This enumeration defines that a enumeration casting data error event occured.
    /// </summary>
    Data_Enumeration_Casting_Error = -513,

    /// <summary>
    /// This enumeration defines that a list empty data error event occured.
    /// </summary>
    Data_List_Empty = -514,

    /// <summary>
    /// This enumeration defines that a null date data error event occured.
    /// </summary>
    Data_Null_Date_Error = -515,

    /// <summary>
    /// This enumeration defines that a null date data error event occured.
    /// </summary>
    Data_Empty_Error = -516,

    /// <summary>
    /// This enumeration defines that a null date data error event occured.
    /// </summary>
    Data_Parameter_Error = -517,

    /// <summary>
    /// This enumeration defines that a duplicate object data error event occured.
    /// </summary>
    Data_Duplicate_Object_Error = -518,

    /// <summary>
    /// This enumeration defines that a duplicate identifier data error event occured.
    /// </summary>
    Data_Duplicate_External_Id_Error = -519,

    /// <summary>
    /// This enumeration defines that a duplicate identifier data error event occured.
    /// </summary>
    Data_Duplicate_Primary_Id_Error = -520,

    /// <summary>
    /// This enumeration defines that adata validation error event occured.
    /// </summary>
    Data_Validation_Error = -521,

    #endregion

    #region Process events
    /// <summary>
    /// This enumeration defines that a general business logic error event occured.
    /// </summary>
    Business_Logic_General_Process_Error = -120,

    /// <summary>
    /// This enumeration defines that a object state update business logic error event occured.
    /// </summary>
    Business_Logic_Object_State_Update_Error = -601,

    /// <summary>
    /// This enumeration defines that a project alert business logic error event occured.
    /// </summary>
    Business_Logic_Alert_Error = -602,

    /// <summary>
    /// This enumeration defines that a alert message business logic error event occured.
    /// </summary>
    Business_Logic_Alert_Message_Error = -603,

    /// <summary>
    /// This enumeration defines that a form used business logic error event occured.
    /// </summary>
    Business_Logic_Form_Used_Error = -604,

    /// <summary>
    /// This enumeration defines that a object in use business logic error event occured.
    /// </summary>
    Business_Logic_Object_In_Use_Error = -605,

    /// <summary>
    /// This enumeration defines that page value update processing error.
    /// </summary>
    Value_Update_Processing_Error = -606,

    /// <summary>
    /// This enumeration defines that the questionnaire URL has not be defined or loaded
    /// </summary>
    Questionnaire_Url_Not_Defines = -607,

    /// <summary>
    /// This enumeration defines no subjects have been selected for the quetionnaire.
    /// </summary>
    Questionnaire_No_Subject_Selected = -608,

    /// <summary>
    /// This enumeration defines that no milestones have been selected for the questionnaire.
    /// </summary>
    Questionnaire_No_Milestones_Selected = -609,

    /// <summary>
    /// This enumeration defines Token user profile update encountered a general error.
    /// </summary>
    Token_User_Profile_Update_Error = -610,

    /// <summary>
    /// This enumeration defines that token user profile encountered a token error.
    /// </summary>
    Token_User_Profile_Token_Error = -611,

    /// <summary>
    /// This enumeration defines that token user profile encountered a userid error.
    /// </summary>
    Token_User_Profile_UserId_Error = -612,

    /// <summary>
    /// This enumeration defines that token user profile encountered a userid error.
    /// </summary>
    Token_User_Profile_UserId_Empty = -613,

    /// <summary>
    /// This enumeration defines that a object state update business logic error event occured.
    /// </summary>
    Business_Logic_Activity_List_Empty = -614,
    #endregion

    #region Process Email events
    /// <summary>
    /// This enumeration defines that an alert business logic email error event occured.
    /// </summary>
    Email_Alert_Error = -650,

    /// <summary>
    /// This enumeration defines that a SMTP business logic email error event occured.
    /// </summary>
    Email_SMTP_Error = -651,

    /// <summary>
    /// This enumeration defines that an authentication business logic email error event occured.
    /// </summary>
    Email_Authentication_Error = -652,

    /// <summary>
    /// This enumeration defines that an authentication business logic email send event
    /// </summary>
    Email_Sent = -653,

    /// <summary>
    /// This enumeration defines that a SMTP business logic email error event occured.
    /// </summary>
    Email_Failure_Error = -654,
    
    /// <summary>
    /// This enumeration defines that a SMTP business logic email no receiver address error event occured.
    /// </summary>
    Email_No_Reciever_Addresses = -655,
    
    /// <summary>
    /// This enumeration defines that a SMTP business logic email configuratin error event occured.
    /// </summary>
    Email_Configuration_Error_Message = -656,

    /// <summary>
    /// The style sheet path is empty 
    /// </summary>
    Xsl_Style_Sheet_Path_Empty = -657,

    /// <summary>
    /// The regulatory report is empty or null
    /// </summary>
    Regulatory_Report_Empty = -658,

    /// <summary>
    /// No SMTP server
    /// </summary>
    Email_SMTP_Server_Url_Empty = -659,

    /// <summary>
    /// No email addresses for sender.
    /// </summary>
    Email_Sender_Address_Empty  = -660,

    /// <summary>
    /// No email addresses for recievers.
    /// </summary> 
    Email_Distribution_List_Empty  = -661,

    /// <summary>
    /// Email send request failed.
    /// </summary>
    Email_Send_Request_Failed  = -662,

    /// <summary>
    /// Email send request failed.
    /// </summary>
    Email_Event_Raised  = -663,

    /// <summary>
    /// Email send request failed.
    /// </summary>
    Parameters_Missing_Error  = -664,

    /// <summary>
    /// Email send request failed.
    /// </summary>
    Smtp_Protocol_Error = -665,

    /// <summary>
    /// Support Email value is empty.
    /// </summary>
    Support_Email_Empty_Error = -666,

    /// <summary>
    /// User Email value is empty.
    /// </summary>
    User_Email_Empty_Error = -667,

    /// <summary>
    /// Application URL  value is empty.
    /// </summary>
    Application_Url_Empty_Error = -667,

    #endregion

    #region Page error events
    /// <summary>
    /// This enumeration defines that a general page error event occured.
    /// </summary>
    Page_Loading_General_Error = -700,

    /// <summary>
    /// This enumeration defines that a loading page error event occured.
    /// </summary>
    Page_Loading_Page_Error = -701,

    /// <summary>
    /// This enumeration defines that a Filling data grid page error event occured.
    /// </summary>
    Page_Filling_Data_Grid_Error = -702,

    /// <summary>
    /// This enumeration defines that a filling selection list page error event occured.
    /// </summary>
    Page_Filling_Selection_List_Error = -703,

    /// <summary>
    /// This enumeration defines that a filling project list page error event occured.
    /// </summary>
    Page_Filling_Project_List_Error = -704,

    /// <summary>
    /// This enumeration defines that a filling organization page error event occured.
    /// </summary>
    Page_Filling_Organisation_List_Error = -705,

    /// <summary>
    /// This enumeration defines that a filling form list page error event occured.
    /// </summary>
    Page_Filling_Form_List_Error = -706,

    /// <summary>
    /// This enumeration defines that a filling visit list page error event occured.
    /// </summary>
    Page_Filling_Visit_List_Error = -707,

    /// <summary>
    /// This enumeration defines that a filling state list page error event occured.
    /// </summary>
    Page_Filling_State_List_Error = -708,

    /// <summary>
    /// This enumeration defines that a filling appointment list page error event occured.
    /// </summary>
    Page_Filling_Appointment_List_Error = -709,

    /// <summary>
    /// This enumeration defines that a retrieving record page error event occured.
    /// </summary>
    Page_Retrieval_Record_Error = -7011,

    /// <summary>
    /// This enumeration defines that a saving record page error event occured.
    /// </summary>
    Page_Saving_Record_Error = -7012,

    /// <summary>
    /// This enumeration defines that a updating selection page error event occured.
    /// </summary>
    Page_Updating_Selection_Error = -7013,

    /// <summary>
    /// This enumeration defines that a saving object page error event occured.
    /// </summary>
    Page_Saving_Object_Error = -7050,
    #endregion

    #region State events
    /// <summary>
    /// This enumeration defines that a general state error event occured.
    /// </summary>
    State_General_Error = -800,

    /// <summary>
    /// This enumeration defines that a field not reviewed state error event occured.
    /// </summary>
    State_Field_Not_Reviewed_Error = -801,
    #endregion

    #region Xml events
    /// <summary>
    /// This enumeration defines that a general xml error event occured.
    /// </summary>
    Xml_General_Error = -900,

    /// <summary>
    /// This enumeration defines that a data xml error event occured.
    /// </summary>
    Xml_Data_Error = -901,

    /// <summary>
    /// This enumeration defines that a style sheet xml error event occured.
    /// </summary>
    Xml_Style_Sheet_Error = -902,

    /// <summary>
    /// This enumeration defines that a xPath query xml error event occured.
    /// </summary>
    Xml_xPath_Query_Error = -903,

    /// <summary>
    /// This enumeration defines that a node xml error event occured.
    /// </summary>
    Xml_Node_Error = -904,

    /// <summary>
    /// This enumeration defines that a initialization xml error event occured.
    /// </summary>
    Xml_Initiaisation_Error = -905,

    /// <summary>
    /// This enumeration defines that a state xml error event occured.
    /// </summary>
    Xml_State_Error = -906,

    /// <summary>
    /// This enumeration defines that a automatication xml error event occured.
    /// </summary>
    Xml_Automation_Error = -907,

    /// <summary>
    /// This enumeration defines that a update xml error event occured.
    /// </summary>
    Xml_Update_Error = -908,

    /// <summary>
    /// This enumeration defines that a deserialization xml error event occured.
    /// </summary>
    Xml_DeSerialisation_Error = -909,

    /// <summary>
    /// This enumeration defines that a deserialization xml error event occured.
    /// </summary>
    Xsl_Transformation_Error = -910,

    /// <summary>
    /// This enumeration defines that a deserialization xml error event occured.
    /// </summary>
    Xsl_Path_Empty_Error = -910,

    /// <summary>
    /// This enumeration defines the general record export error
    /// </summary>
    Data_Export_Exception_Event = -911,

    /// <summary>
    /// This enumeration defines the  data export parmeter missing error.
    /// </summary>
    Data_Export_Parameter_Error = -912,

    /// <summary>
    /// This enumeration defines the empty record list error.
    /// </summary>
    Data_Export_Empty_Record_List = -913,

    /// <summary>
    /// This enumeration defines the import data object is empty.
    /// </summary>
    Data_Import_Object_Missing_error = -914,

    #endregion

    #region Data point events
    /// <summary>
    /// This enumeration defines that a general data point error event occured.
    /// </summary>
    Data_Point_General_Error = -1000,

    /// <summary>
    /// This enumeration defines that a form field data point error event occured.
    /// </summary>
    Data_Point_Form_Field_Error = -1001,

    /// <summary>
    /// This enumeration defines that a text data point error event occured.
    /// </summary>
    Data_Point_Text_Data_Error = -1002,

    /// <summary>
    /// This enumeration defines that a selection data point error event occured.
    /// </summary>
    Data_Point_Selection_Data_Error = -1003,

    /// <summary>
    /// This enumeration defines that a numeric data data point error event occured.
    /// </summary>
    Data_Point_Numeric_Data_Error = -1004,

    /// <summary>
    /// This enumeration defines that a date data data point error event occured.
    /// </summary>
    Data_Point_Date_Data_Error = -1005,

    /// <summary>
    /// This enumeration defines that a table row data data point error event occured.
    /// </summary>
    Data_Point_Table_Row_Data_Error = -1006,

    /// <summary>
    /// This enumeration defines that a table column data data point error event occured.
    /// </summary>
    Data_Point_Table_Column_Data_Error = -1007,

    /// <summary>
    /// This enumeration defines that a table value data point error event occured.
    /// </summary>
    Data_Point_Table_Value_Error = -1008,

    #endregion

    #region Web services events
    /// <summary>
    /// This enumeration defines that a general webservices error event occured.
    /// </summary>
    WebServices_General_Failure_Error = -1100,

    /// <summary>
    /// This enumeration defines that a user authentication webservices error event occured.
    /// </summary>
    WebServices_JSON_Empty_Error = -1101,

    /// <summary>
    /// This enumeration defines that a data deserialization failed webservices error event occured.
    /// </summary>
    WebServices_JSON_Deserialisation_Failed_Error = -1102,

    /// <summary>
    /// This enumeration defines that a global unique identifier webservices error event occured.
    /// </summary>
    WebServices_User_Authentication_Error = -1103,

    /// <summary>
    /// This enumeration defines that a common item object null webservices error event occured.
    /// </summary>
    WebServices_Common_Item_Object_Null_Error = -1104,

    /// <summary>
    /// This enumeration defines that a project object null webservices error event occured.
    /// </summary>
    WebServices_Trial_Object_Null_Error = -1105,

    /// <summary>
    /// This enumeration defines that a subject object null webservices error event occured.
    /// </summary>
    WebServices_Subject_Object_Null_Error = -1106,

    /// <summary>
    /// This enumeration defines that a subject data upload webservices error event occured.
    /// </summary>
    WebServices_Subject_Data_Upload_Error = -1107,

    /// <summary>
    /// This enumeration defines that an web servces internal server event occured.
    /// </summary>
    WebServices_Internal_Server_Error = -1108,

    /// <summary>
    /// This enumeration defines that an not found webservices error event occured.
    /// </summary>
    WebServices_Not_Found_Error = -1109,

    /// <summary>
    /// This enumeration defines that an Bad request webservices error event occured.
    /// </summary>
    WebServices_Bad_Request_Error = -1110,

    /// <summary>
    /// This enumeration defines that a conflict webservices error event occured.
    /// </summary>
    WebServices_Conflict_Error = -1111,

    /// <summary>
    /// This enumeration defines that an Request Entity too large webservices error event occured.
    /// </summary>
    WebServices_Request_Entity_Too_Large_Error = -1112,

    /// <summary>
    /// This enumeration defines that an Method not allowe webservices error event occured.
    /// </summary>
    WebServices_Method_Not_Allowed_Error = -1113,

    /// <summary>
    /// This enumeration defines that anNo content  webservices error event occured.
    /// </summary>
    WebServices_No_Content_Error = -1114,

    /// <summary>
    /// This enumeration defines the Adapter trusted client validation failure
    /// </summary>
    WhereBy_Service_UrL_Empty_Error = -1124,

    /// <summary>
    /// This enumeration defines the Adapter trusted client validation failure
    /// </summary>
    WhereBy_Token_Empty_Error = -1125,

    /// <summary>
    /// This enumeration defiens the Adapter Token not foud error
    /// </summary>
    WhereBy_Base_Url_Empty_Error = -1126,

    /// <summary>
    /// This enumeration defines the Adapter trusted client validation failure
    /// </summary>
    WhereBy_Web_Api_Error = -1127,

    /// <summary>
    /// This enumeration defines the Adapter trusted client validation failure
    /// </summary>
    WhereBy_endDate_Not_In_Future_Error = -1128,

    #endregion

    #region Form Script events
    /// <summary>
    /// This enumeration defines that a form script error event occured.
    /// </summary>
    CsFormScript_Error = -1200,

    /// <summary>
    /// This enumeration defines that a method return value form script error event occured.
    /// </summary>
    CsFormScript_Form_Script_Enabled_False = -1201,

    /// <summary>
    /// This enumeration defines that a file name null form script error event occured.
    /// </summary>
    CsFormScript_File_Name_Null_Error = -1202,

    /// <summary>
    /// This enumeration defines that a script not found form script error event occured.
    /// </summary>
    CsFormScript_Script_Not_Found_Error = -1203,

    /// <summary>
    /// This enumeration defines that a method exception form script error event occured.
    /// </summary>
    CsFormScript_Method_Exception_Error = -1204,

    /// <summary>
    /// This enumeration defines that a method return value form script error event occured.
    /// </summary>
    CsFormScript_Method_Return_Value_Error = -1205,
    #endregion

    #region Encryption events
    /// <summary>
    /// This enumeration defines that a general encryption error event occured.
    /// </summary>
    Encryption_General_Error = -1300,

    /// <summary>
    /// This enumeration defines that a global unique identifier 1 null encryption error event occured.
    /// </summary>
    Encryption_Guid_1_Null_Error = -1301,

    /// <summary>
    /// This enumeration defines that a global unique identifier 2 null encryption error event occured.
    /// </summary>
    Encryption_Guid_2_Null_Error = -1302,

    /// <summary>
    /// This enumeration defines that a decryption bad data encryption error event occured.
    /// </summary>
    Encryption_Encryption_Bad_Data_Error = -1303,

    /// <summary>
    /// This enumeration defines that a decryption bad data encryption error event occured.
    /// </summary>
    Encryption_Decryption_Bad_Data_Error = -1304,
    #endregion

    #region File events
    /// <summary>
    /// This enumeration defines that a general access file error event occured.
    /// </summary>
    File_General_Access_Error = -1400,

    /// <summary>
    /// This enumeration defines that a save file error event occured.
    /// </summary>
    File_Save_Error = -1401,

    /// <summary>
    /// This enumeration defines that a read file error event occured.
    /// </summary>
    File_Read_Error = -1402,

    /// <summary>
    /// This enumeration defines that a read file error event occured.
    /// </summary>
    File_Directory_Error = -1403,

    /// <summary>
    /// This enumeration defines that a read file error event occured.
    /// </summary>
    File_Directory_Path_Empty = -1404,

    /// <summary>
    /// This enumeration defines that a read file error event occured.
    /// </summary>
    File_File_Name_Empty = -1405,

    /// <summary>
    /// This enumeration defines that a read file error event occured.
    /// </summary>
    File_File_Content_Empty = -1406,

    /// <summary>
    /// This enumeration defines Binary File path is empty.
    /// </summary>
    Uniform_Binary_File_Path_Empty = -1407,

    /// <summary>
    /// This enumeration defines that binary file URL is empty.
    /// </summary>
    Uniform_Binary_File_Url_Empty = -1408,

    /// <summary>
    /// This enumeration defines that binary file service error.
    /// </summary>
    Uniform_File_Service_Error = -1409,

    /// <summary>
    /// This enumeration defines that binary file service error.
    /// </summary>
    Uniform_File_Segement_Processed = -1410,

    /// <summary>
    /// This enumeration defines that binary file service error.
    /// </summary>
    Uniform_File_Service_Returned_Null = -1411,


    /// <summary>
    /// This enumeration defines that a read file error event occured.
    /// </summary>
    File_Directory_Path_Create_Error = -1412,
    #endregion

    #region Integration Import events
    /// <summary>
    /// This enumeration defines that a general data import error.
    /// </summary>
    Integration_Import_General_Error = -1500,

    /// <summary>
    /// This enumeration defines the import object contains query parameters.
    /// </summary>
    Integration_Import_Parameter_Error = -1501,

    /// <summary>
    /// This enumeration defines the import object contains query parameters.
    /// </summary>
    Integration_Import_Column_Data_Error = -1502,

    /// <summary>
    /// This enumeration defines that a data import validation error.
    /// </summary>
    Integration_Import_Multiple_Index_Error = -1503,

    /// <summary>
    /// This enumeration defines that a data import validation error.
    /// </summary>
    Integration_Import_Data_Validation_Error = -1504,

    /// <summary>
    /// This enumeration defines that a data import validation aborted due to too many validation errors.
    /// </summary>
    Integration_Import_Data_Validation_Aborted = -1505,

    /// <summary>
    /// This enumeration defines that a data import project identifier error.
    /// </summary>
    Integration_Import_Project_Id_Error = -1506,

    /// <summary>
    /// This enumeration defines that a data import organisation identifier error.
    /// </summary>
    Integration_Import_Organisation_Id_Error = -1507,

    /// <summary>
    /// This enumeration defines that a data import  identifier error.
    /// </summary>
    Integration_Import_External_Identifier_Error = -1508,

    /// <summary>
    /// This enumeration defines that a data import subject identifier error.
    /// </summary>
    Integration_Import_Subject_Id_Error = -1509,

    /// <summary>
    /// This enumeration defines that a data import subject identifier error.
    /// </summary>
    Integration_Import_Milestone_Id_Error = -15109,

    /// <summary>
    /// This enumeration defines that a data import activity identifier error.
    /// </summary>
    Integration_Import_Activity_Id_Error = -15111,

    /// <summary>
    /// This enumeration defines that a data import activity identifier error.
    /// </summary>
    Integration_Import_Visit_Id_Error = -15112,

    /// <summary>
    /// This enumeration defines that a data import mandatory record error.
    /// </summary>
    Integration_Import_Mandatory_Record_Error = -15113,

    /// <summary>
    /// This enumeration defines that a data import mandatory activity error.
    /// </summary>
    Integration_Import_Mandatory_Activity_Error = -15114,

    #endregion

    #region Active Directory  events
    /// <summary>
    /// This enumeration defines that a general data import error.
    /// </summary>
    Active_Directory_General_Error = -1600,

    /// <summary>
    /// The value of arguments is invalid
    /// </summary>
    Active_Directory_Invalid_Argument = -1601,

    /// <summary>
    /// There was either no User or no Group matched Which was requested.
    /// </summary>
    Active_Directory_Object_Not_Found = -1602,

    /// <summary>
    /// The password is not matched.
    /// </summary>
    Active_Directory_Invalid_Credentials = -1603,

    /// <summary>
    /// Error occurred in the operation with ADS
    /// </summary>
    Active_Directory_Operation_Failure = -1604,

    /// <summary>
    /// Error occurred in the operation with ADS
    /// </summary>
    Active_Directory_Group_Not_Found = -1605,

    /// <summary>
    /// Error occurred in the operation with ADS
    /// </summary>
    Active_Directory_User_Not_Found = -1606,

    /// <summary>
    /// Error occurred in the operation with ADS
    /// </summary>
    Active_Directory_Save_User_Failed = -1607,

    /// <summary>
    /// Active Directory is no enabled.
    /// </summary>
    Active_Directory_Not_Enabled,

    #endregion

    #region Data Export events
    /// <summary>
    /// This enumeration defines that a general data export error.
    /// </summary>
    Integration_General_Error = -1600,

    /// <summary>
    /// This enumeration defines the export object contains query parameters.
    /// </summary>
    Integration_Exception_Event = -1601,

    /// <summary>
    /// This enumeration defines the export returned empty list.
    /// </summary>
    Integration_Empty_Record_List = -1602,

    /// <summary>
    /// This enumeration defines the export object contains query parameters.
    /// </summary>
    Integration_Parameter_Error = -1604,

    /// <summary>
    /// This enumeration defines the export object contains query parameters.
    /// </summary>
    Integration_Column_Data_Error = -1605,

    /// <summary>
    /// This enumeration defines that a data export validation error.
    /// </summary>
    Integration_Data_Validation_Error = -1606,

    /// <summary>
    /// This enumeration defines that a data export validation aborted due to too many validation errors.
    /// </summary>
    Integration_Data_Validation_Aborted = -1607,

    /// <summary>
    /// This enumeration defines that a data export project identifier error.
    /// </summary>
    Integration_Trial_Id_Error = -1608,

    /// <summary>
    /// This enumeration defines that a data export form identifier error.
    /// </summary>
    Integration_Form_Id_Error = -1609,

    /// <summary>
    /// This enumeration defines that a CSV data parsing error event.
    /// </summary>
    CSV_Data_Parsing_Error = -1610,

    /// <summary>
    /// This enumeration defines that a CSV data column count error event.
    /// </summary>
    CSV_Data_Column_Count_Error = -1611,


    #endregion

  }//END Evado.Model.EvEventCodes method

} //END namespace Evado.Model
 