/****************************************************************************************
 *                COPYRIGHT (C) EVADO HOLDING PTY. LTD.  2002 - 2014 ALL RIGHTS RESERVED
 ***************************************************************************************/
var Evado = Evado || {};

(function() {
  var Type_Text = "Text";
  var Type_Num = "Numeric";
  var Type_Date = "Date";
  var Type_Time = "Time";
  var Type_SL = "Selection_List";
  var Type_RBL = "Radio_Button_List";
  var Type_CBL = "Check_Button_List";
  var Type_YesNo = "Yes_No";
  var Null_Date = new Date();
  var timerID = undefined;
  var timerRunning = true;
  var delay = 1000;
  var secs = 0;
  var dateFormat = 'DD MMM YYYY';
  Null_Date.setFullYear(1, 0, 1900);

  function updateComputedFields(){
    console.log("updateComputedFields: STARTED" );
    console.log("computedScript: " + computedScript );
    if ( computedScript == true ) {
    computedFields();
    }

    console.log("updateComputedFields: FINISHED" );
    }

  function onTextValidation(source, oldValue, options) {
    $(source).trigger('uniform:field:valid');
  }


  /* *************************************************************************************
   * NumericValidation method
   *
   * Description:
   *  This method performs a numeric validation of the field content.
   *  The validation, alert and safety ranges are passed as input object attributes.
   *
   * ************************************************************************************/
  function onIndentiferFormat6(source, oldValue, options) {
    options || (options = {focus:true});
    console.log("onIndentiferFormat6: STARTED" );

    if (isNaN(source.value) == true) {
      var message = "This value is not a valid number\r\nE.g. 103256";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }

    if (source.value.length < 6 || source.value.length > 6) {
      var message = "This value is less than 6 digits long.";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }

    $(source).trigger('uniform:field:valid');

    console.log("onIndentiferFormat6: FINISHED" );
  } //END  NumericValidation method
  

  /* *************************************************************************************
   * ontextChange method
   *
   * Description:
   *  This method performs a validation check on text field values.
   *
   * ************************************************************************************/
  function onTextChange( source, oldValue, options) {
    options || (options = {focus:true});
    console.log("onTextChange: STARTED" );

    source.value= source.value.replace("<", "");
    source.value= source.value.replace(">", "");

    $(source).trigger('uniform:field:valid');

    console.log("onTextChange: FINISHED" );
  } //END  onIntegerValidation method

  /* *************************************************************************************
   * onIntegerValidation method
   *
   * Description:
   *  This method performs a numeric validation of the field content.
   *  The validation, alert and safety ranges are passed as input object attributes.
   *
   * ************************************************************************************/
  function onIntegerValidation( source, oldValue, options) {
    options || (options = {focus:true});
    console.log("onIntegerValidation: STARTED" );

    if (isNaN(source.value) == true && source.value != "NA" && source.value != "na") {
      var message = "This value is not a valid integer.\r\nE.g. 27 or 27900 or NA";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }
    var num = Number(source.value);
    var integer = num.toFixed(0);
    if (source.value != integer) {
      var message = "This value is not a valid integer.\r\nE.g. 27 or 27900 or NA";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }

    $(source).trigger('uniform:field:valid');
    
    

    console.log("onIntegerValidation: FINISHED" );
  } //END  onIntegerValidation method


  /* *************************************************************************************
   * NumericValidation method
   *
   * Description:
   *  This method performs a numeric validation of the field content.
   *  The validation, alert and safety ranges are passed as input object attributes.
   *
   * ************************************************************************************/
  function onNumericValidation(source, oldValue ) {
    options || (options = {focus:true});

    console.log("onNumericValidation: STARTED" );

    if (isNaN(source.value) == true && source.value != "NA" && source.value != "na" && source.value != "N/A" && source.value != "n/a") {
      var message = "This value is not a valid number\r\nE.g. 27.590 or 27900 or NA";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }

    $(source).trigger('uniform:field:valid');
    
    

    console.log("onNumericValidation: FINISHED" );
  } //END  NumericValidation method


  /* *************************************************************************************
   * NumericValidation method
   *
   * Description:
   *  This method performs a numeric validation of the field content.
   *  The validation, alert and safety ranges are passed as input object attributes.
   *
   * ************************************************************************************/
  function onRangeValidation( source, oldValue, options)
   {
    options || (options = {focus:true});
    console.log("onRangeValidation: STARTED" );

    var $source = $(source);

    // new
    var message = "";
    var validationLowerLimit = $source.data('minValue');
    var validationUpperLimit = $source.data('maxValue');
    var alertLowerLimit = $source.data('minAlert');
    var alertUpperLimit = $source.data('maxAlert');
    var normalLowerLimit = $source.data('minNorm');
    var normalUpperLimit = $source.data('maxNorm');
    var cssValidation = $source.data('cssValid');
    var cssAlert = $source.data('cssAlert');
    var cssNormal = $source.data('cssNorm');
    
    source.className= 'Default';
    /*
    console.log("source.id "+ source.id );
    console.log("source.value " + source.value );
    console.log("validationLowerLimit " + validationLowerLimit );
    console.log("validationUpperLimit " + validationUpperLimit );
    console.log("alertLowerLimit " + alertLowerLimit );
    console.log("alertUpperLimit " + alertUpperLimit );
    console.log("normalUpperLimit " + normalUpperLimit );
    console.log("normalLowerLimit " + normalLowerLimit );
    console.log("cssValidation " + cssValidation ); 
    console.log("cssAlert " + cssAlert );  
    console.log("cssNormal " + cssNormal)
    */
     //
     // Exit if NA is the value as it indicated not available.
     //
    if (source.value == "NA" || source.value == "N/A") {
      source.value = "NA";
      $(source).trigger('uniform:field:valid');
      return message;
    }

    // test that the value is a number.
    //
    if (isNaN(source.value) == true && source.value != "NA" && source.value != "N/A") {
      var message = "This value is not a valid number\r\nE.g. 27.590 or 27900";
      source.value = oldValue;
      if (options.focus) source.focus();
      if ( cssValidation != null ){
         source.className= cssValidation ; }
      $(source).trigger('uniform:field:invalid', {message: message});

      return message;
    }

    //console.log("Range validating the field" );

    // convert the attributes to numbers
    var fltValue = 1 * source.value;
    var fltValidationLowerLimit = -1000000;
    var fltValidationUpperLimit = 1000000;
    var fltAlertLowerLimit = -1000000;
    var fltAlertUpperLimit = 1000000;
    var fltNormalLowerLimit = -1000000;
    var fltNormalUpperLimit = 1000000;

    
    if ( validationLowerLimit != null && isNaN(validationLowerLimit) == false )
    {
      fltValidationLowerLimit = 1 * validationLowerLimit
    }
    if ( validationUpperLimit != null && isNaN(validationUpperLimit) == false )
    {
      fltValidationUpperLimit = 1 * validationUpperLimit
    }     
    if ( alertLowerLimit != null && isNaN(alertLowerLimit) == false)
    {
      var fltAlertLowerLimit = 1 * alertLowerLimit;
    }
    if ( alertUpperLimit != null && isNaN(alertUpperLimit) == false ) 
    {
      var fltAlertUpperLimit = 1 * alertUpperLimit;
    }
    if ( normalLowerLimit != null && isNaN(normalLowerLimit) == false) 
    {
      var fltNormalLowerLimit = 1 * normalLowerLimit;
    }
    if ( normalUpperLimit != null && isNaN(normalUpperLimit) == false )
    {
      var fltNormalUpperLimit = 1 * normalUpperLimit;
    }
    
    //console.log("COMMENCE: Range validated the field value: " + fltValue );
    //console.log("fltValidationLowerLimit " + fltValidationLowerLimit );
    //console.log("fltValidationUpperLimit " + fltValidationUpperLimit );

    // Check that the value is in the validation range.
    if (-1000000 != fltValidationLowerLimit && 1000000 != fltValidationUpperLimit) 
    {
      //console.log("Validating the field numerical range." );
    
      if ( fltValue < fltValidationLowerLimit 
        || fltValue > fltValidationUpperLimit) 
      {
        message = "Value outside of the validation range \r\n" + "The value must be in the range of " + fltValidationLowerLimit + " - " + fltValidationUpperLimit;
        source.value = oldValue;

        if (options.focus) source.focus();
        if ( cssValidation != null ) source.className=cssValidation ;

        $(source).trigger('uniform:field:invalid', {message: message});
        
        //console.log("EXIT Range numeric validation ." );
        return message;
      }
        
    } //END Validation range.

    
    //console.log("COMMENCE: Alert range validation the field value: " + fltValue );
    //console.log("fltAlertLowerLimit " + fltAlertLowerLimit );
    //console.log("fltAlertUpperLimit " + fltAlertUpperLimit );
    // Check that the value is in the validation range.
    if (-1000000 != fltAlertLowerLimit && 1000000 != fltAlertUpperLimit) 
    {
      //console.log("Validating the field numerical alert range." );

      if (fltValue < fltAlertLowerLimit || fltValue > fltAlertUpperLimit) 
      {
        message = "Value outside of the alert range \r\n" 
        + "The value should be in the range of " 
        + fltAlertLowerLimit + " - " + fltAlertUpperLimit;

        if (options.focus) source.focus();

        if ( cssAlert != null )  source.className=cssAlert ; 
      
        $(source).trigger('uniform:field:invalid', {message: message});
      
        return message;
        
      }//END alert range failed.

    } //END Alert enabled
    
    //console.log("COMMENCE: Normal range validation. the field value: " + fltValue );
    //console.log("fltNormalLowerLimit " + fltNormalLowerLimit );
    //console.log("fltNormalUpperLimit " + fltNormalUpperLimit );

    // Check that the value is in the normal range.
      if (-1000000 != fltNormalLowerLimit && 1000000 != fltNormalUpperLimit) 
      {
      //console.log("Validating the field numerical normal range." );

        if ( fltValue < fltNormalLowerLimit || fltValue > fltNormalUpperLimit) 
        {
          message = "Value outside of the normal range \r\n" 
          + "The value should be in the range of " + fltNormalLowerLimit + " - " + fltNormalUpperLimit;

          if (options.focus) source.focus();

          if ( cssNormal != null ) source.className=cssNormal ; 

          $(source).trigger('uniform:field:invalid', {message: message});

          return message;
        }//END raange failed.

    } //END enable normal range enabled.
    
    

    console.log("onRangeValidation: FINISHED" );

    $(source).trigger('uniform:field:valid', {message: message});

  } //END onRangeValidation method


  /* *************************************************************************************
   * onIntegerValidation method
   *
   * Description:
   *  This method performs a numeric validation of the field content.
   *  The validation, alert and safety ranges are passed as input object attributes.
   *
   * ************************************************************************************/
  function onDateSelectChange( source, fieldid, options) {
    options || (options = {focus:true});
    
    console.log("onDateSelectChange: STARTED" );
    var objdate = document.getElementById(fieldid );
    var objday = document.getElementById(fieldid + "_DAY");
    var objmonth = document.getElementById(fieldid+ "_MTH");
    var objyear = document.getElementById(fieldid+ "_YR");
    var date = "";
    var month = objmonth.value;
    
    console.log( "old date: " + objdate.value );
    console.log( "day: " + objday.value + " Month: " + objmonth.value + " year: " + objyear.value );

    if ( objday.value != "" )
    {
     date += objday.value + " ";
    }
    
    if ( objmonth.value != "" )
    {
     date += objmonth.value + " ";
     }
    
    if ( objyear.value != "" )
    {
     date += objyear.value ;
    }

     
    console.log( "date: " + date );

    objdate.value = date;
    console.log( "new date: " + objdate.value );

    console.log("onDateSelectChange: FINSIHED" );

    
  } //END  onDateSelectChange method
  
  /* *************************************************************************************
   * onIntegerValidation method
   *
   * Description:
   *  This method performs a numeric validation of the field content.
   *  The validation, alert and safety ranges are passed as input object attributes.
   *
   * ************************************************************************************/
  function onTimeSelectChange( source, fieldid ) {
    
    console.log("onTimeSelectChange: STARTED" );
    console.log( "fieldid: " + fieldid );
    var objtime = document.getElementById(fieldid );
    var objhour = document.getElementById(fieldid + "_HR");
    var objmin = document.getElementById(fieldid + "_MIN");
    var objsec = document.getElementById(fieldid + "_SEC");
    var time = "";
    
    //console.log( "time: " + objtime.value + " hr: " + objhour.value + " min: " + objmin.value );

    if ( objhour.value != "" )
    {
     time += objhour.value ;
    }
    
    if ( objmin.value != "" )
    {
     time += ":" + objmin.value;
     }
    
    if ( objsec != null )
    {
      if ( objsec.value != "" )
      {
        time +=  ":" +objsec.value ;
      }
    }

   //console.log( "time: " + time );

    objtime.value = time;

    console.log( "new Time: " + objtime.value );

    console.log("onTimeSelectChange: FINISHED" );
    
  } //END  onDateSelectChange method


  /* *************************************************************************************
   * DateValidation method
   *
   * Description:
   *  This method performs a date validation to ensure that the date is valid and within
   *  an acceptable date range.
   *
   * ************************************************************************************/
  function onDateValidation(source, oldValue, options) {
    options || (options = {focus:true});
    console.log("onDateValidation: STARTED" );

    if (source.value == "") {
      return false;
    }

    if (!moment(source.value, dateFormat).isValid()) {
      var message = "Invalid date format (" + dateFormat + ")";
      oldValue = source.value;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }

    $(source).trigger('uniform:field:valid');
    
    

    console.log("onDateValidation: FINISHED" );

  } //END  DateValidation method


  /* *************************************************************************************
   * onFinishDateValidation method
   *
   * Description:
   *  This method performs a date validation to ensure that the date is valid and within
   *  an acceptable date range.
   *
   * ************************************************************************************/
  function onFinishDateValidation(source, oldValue, options) {
    options || (options = {focus:true});
    
    console.log("onFinishDateValidation: STARTED" );

    if (onDateValidation(source, oldValue) == false) {
      return;
    }

    var startDate = $('#' + (options.startDateId || 'evField_StartDate'));

    if (moment(source.value, dateFormat) < moment(startDate.val(), dateFormat)) {
      var message = "The date is earlier than the start date.";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }
    
    

    console.log("onFinishDateValidation: FINISHED" );
  }


  /* *************************************************************************************
   * onConsentDateValidation method
   *
   * Description:
   *  This method performs a date validation to ensure that the date is valid and within
   *  an acceptable date range.
   *
   * ************************************************************************************/
  function onConsentDateValidation(source, oldValue, options) {
    options || (options = {focus:true});

    console.log("onConsentDateValidation: STARTED" );

    if (onDateValidation(source, oldValue) == false) {
      return;
    }

    var doc = $('#' + (options.doc || 'EvSubject_ConsentDate'));

    if (moment(source.value, dateFormat) < moment(doc.val(), dateFormat)) {
      var message = "The date is earlier than the date of consent.";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return;
    }
    
    

    console.log("onConsentDateValidation: FINISHED" );
  }


  /* *************************************************************************************
   * onBirthDateValidation method
   *
   * Description:
   *  This method performs a date validation to ensure that the date is valid and within
   *  an acceptable date range.
   *
   * ************************************************************************************/
  function onBirthDateValidation(source, oldValue, options) {
    options || (options = {focus:true});
    var message;

    console.log("onBirthDateValidation: STATED" );

    if (message = onDateValidation(source, oldValue)) {
      return message;
    }

    var dob = $('#' + (options.dob || 'EvSubject_DateOfBirth'));

    if (moment(source.value, dateFormat) < moment(dob.val(), dateFormat)) {
      var message = "The date is earlier than the date of birth.";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }
    
    

    console.log("onBirthDateValidation: FINISHED" );
  }



  /* *************************************************************************************
   * onTimeValidation method
   *
   * Description:
   *  This method performs a time structure validation on the field value.
   *
   * ************************************************************************************/
  function onTimeValidation(source, oldValue, options) {
    options || (options = {focus:true});

    console.log("onTimeValidation: STARTED" );

    var time = source.value;
    if (source.value == "") {
      return;
    }

    time = time.replace(" ", ":");
    time = time.replace(" ", ":");
    if (time.indexOf(":") == -1) {
      time += "0000";
      var newtime = time.substring(0, 2) + ":" + time.substring(2, 4);
      if (time.length > 4) {
        newtime += ":" + time.substring(4, 6);
      }
      time = newtime;
    }

    var atime = time.split(':');
    var hour = new Number(atime[0]);
    var min = new Number(atime[1]);
    var sec = 0;
    if (time.length > 5) {
      sec = new Number(atime[2]);
    }

    if (isNaN(hour) == true || isNaN(min) == true || isNaN(sec) == true) {
      var message = "The this is not a valid time structure.";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }

    if (hour < 0 || hour > 23) {
      var message = "The hour is out of range 00 to 23 hours.";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }

    if (min < 0 || min > 59) {
      var message = "The minute is out of range 00 to 59 minutes.";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }

    if (sec < 0 || sec > 59) {
      var message = "The minute is out of range 00 to 59 seconds.";
      source.value = oldValue;
      if (options.focus) source.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
      return message;
    }

    source.value = time;
    oldValue = source.value;
    $(source).trigger('uniform:field:valid');
    
    

    console.log("onTimeValidation: FINISHED" );
  } //END onTimeValidation method


  /* *************************************************************************************
   * SelectionValidation method
   *
   * Description:
   *  This method validates the selection change
   *
   * ************************************************************************************/
  function onSelectionValidation(source, OldValue, options) {
    $(source).trigger('uniform:field:valid');

    console.log("onSelectionValidation: STARTED" );

    var objField = document.getElementById(source.name);

    if( objField != undefined )
    {
    document.getElementById(source.name).value = source.value;

    console.log("updated " + source.name + ".value : " + document.getElementById(source.name).value );
    }

    

    console.log("onSelectionValidation: FINISHED" );
  } //END onSelectionValidation method

  /* *************************************************************************************
   * SelectionValidation method
   *
   * Description:
   *  This method validates the selection change
   *
   * ************************************************************************************/
  function onQuizValidation(source, OldValue, options) {
    $(source).trigger('uniform:field:valid');"";
    var $source = $(source);

    console.log("onQuizValidation: STARTED" );
    var quizValue = $source.data('quizValue');
    var quizAnswer = $source.data('quizAnswer');
    var cssAlert = $source.data('cssAlert');

    console.log("source.name: " + source.name );
    console.log("source.value: " + source.value );
    console.log("quizAnswer: " + quizAnswer );
    console.log("cssAlert: " + cssAlert );

    var answerField = document.getElementById(source.name + "_ANSWER");
    console.log("answerField.value: " + answerField.value );
    
    source.className= 'Default';
    
    // test that the value is a number.
    //
    if ( source.value != '' && source.value != quizValue ) 
    {
      source.className= cssAlert ; 
      $(source).trigger('uniform:field:invalid', {message: quizAnswer});

      answerField.value += source.value +  ";";
      
      console.log("onQuizValidation: FINISHED with ERROR" );
      return;
    }

    

    console.log("onQuizValidation: FINISHED" );
  } //END onQuizValidation method


  /* *************************************************************************************
   * onQueryField method
   *
   * Description:
   *  This method executes when the review clicks the review radio buttons.
   *
   * ************************************************************************************/
  function onQueryField(source, AnnotationFieldId, options) {
    options || (options = {focus:true});

    console.log("onQueryField: STARTED" );

    var varAnnotationField = document.getElementById(AnnotationFieldId);
    if (varAnnotationField.value == "") {
      var message = "To query a field you must first annotate the field then click the Query check box.";
      source.checked = false;
      if (options.focus) varAnnotationField.focus();
      $(source).trigger('uniform:field:invalid', {message: message});
    }

    

    console.log("onQueryField: FINISHED" );
  } //END onQueryField function


  /* *************************************************************************************
   * onCommonRecordCompleted_Clicked method
   *
   * Description:
   *  This method executes when the review clicks the review radio buttons.
   *
   * ************************************************************************************/
  function onCommonRecordCompleted_Clicked(source, options) {
    options || (options = {focus:true});

    console.log("onCommonRecordCompleted_Clicked: STARTED" );

    var varFinishDateField = document.getElementById($(source).data('finishDateField'));
    if (source.checked = "checked") {
      varFinishDateField.disabled = "";
      if (options.focus) varFinishDateField.focus();
    } else {
      varFinishDateField.disabled = "disabled";
    }

    

    console.log("onCommonRecordCompleted_Clicked: FINISHED" );

  } //END onCommonRecordCompleted_Clicked function


  /* *************************************************************************************
   * getVariable method
   *
   * Description:
   *  This method queries the variable arrays to find and return a selected variable value.
   *
   * ************************************************************************************/
  function getVariable(FieldName) {
    var formField = document.getElementById(FieldName);
    if (formField != undefined) {
      return formField.value;
    }
    return "";
  } //END getVariable


  /* *************************************************************************************
   * setVariable method
   *
   * Description:
   *  This method queries the variable arrays to find and return a selected variable value.
   *
   * ************************************************************************************/
  function setVariable(FieldName, Value) {
    setFieldValue(FieldName, Value);
  } //END setFieldValue


  /* *************************************************************************************
   * getFieldValue method
   *
   * Description:
   *  This method queries the variable arrays to find and return a selected variable value.
   *
   * ************************************************************************************/
  function getFieldValue(FieldId) {
    var formField = document.getElementById(FieldId);
    if (formField != undefined) {
      if (isNaN(formField.value) == false && formField.value != "") {
        return new Number(formField.value);
      }
      return formField.value;
    }
    return "";

  } //END getFieldValue


  /* *************************************************************************************
   * setFieldValue method
   *
   * Description:
   *  This method queries the variable arrays to find and return a selected variable value.
   *
   * ************************************************************************************/
  function setFieldValue(FieldName, Value) {
    var fieldName = String(FieldName);
    var value = String(Value);
    var formField = document.getElementById(FieldName);
    if (formField != undefined) {
    formField.value = value;
    }
    return;

  } //END setFieldValue


  /* *************************************************************************************
   * setFieldValue method
   *
   * Description:
   *  This method tests to see if a field value exists.
   *
   * ************************************************************************************/
  function hasFieldValue(FieldName) {
    var formField = document.getElementById(FieldId);
    if (formField != undefined) {
      return true;
    }
    return false;
  } //END setFieldValue


  /* *************************************************************************************
   * getNumeric method
   *
   * Description:
   *  This method convert a text value into a numeric if it is a number.
   *
   * ************************************************************************************/
  function getNumeric(VariableValue) {
    if (isNaN(VariableValue) == true) {
      alert("This value is not a valid number\r\nE.g. 27.590 or 27900");
      return Number.NaN;
    }
    return new Number(VariableValue);

  } //END GetNumeric function


  /* *************************************************************************************
   * getRadioButtonValue method
   *
   * Description:
   *  This method convert a text value into a numeric if it is a number.
   *
   * ************************************************************************************/
  function getRadioButtonValue(FieldName) {
    //alert("getRadioButtonValue method FieldName: " + FieldName);
    for (i = 0; i < 30; i++) {
      var fieldName = FieldName + "_" + i;
      if (document.getElementById(fieldName) != undefined) {
        var formField = document.getElementById(fieldName);
        if (formField.type == "radio") {
          if (formField.checked == true) {
            return formField.value;
          }
        }
      }
    }
    return "";

  } //END getRadioButtonValue function


  /* *************************************************************************************
   * getCheckBoxValue method
   *
   * Description:
   *  This method convert a text value into a numeric if it is a number.
   *
   * ************************************************************************************/
  function getCheckBoxValue(FieldName) {
    var value = "";
    for (i = 0; i < 25; i++) {
      var fieldName = FieldName + "_" + i;
      if (document.getElementById(fieldName) != undefined) {
        var formField = document.getElementById(fieldName);
        if (formField.type == "checkbox") {
          if (formField.checked == true) {
            if (value != "") {
              value += ";";
            }
            value += formField.value;
          }
        }
      }
    }
    return value;

  } //END getCheckBoxValue function


  /* *************************************************************************************
   * getDate method
   *
   * Description:
   *  This method queries convert the variable to a java date object.
   *
   * ************************************************************************************/
  function getDate(VariableValue) {
    return moment(VariableValue, dateFormat);
  } //END getDate function


  /**************************************************************************************
   * validateTableDateRange method
   *
   * Description:
   *  This method sums the values of the passed list of variables variable delimiter is
   *  ' ', ',' or ';'
   *
   * ************************************************************************************/
  function validateTableDateRange(FieldId, FieldName, RowCount, LowerDateColumn, UpperDateColumn, OngoingColumn) {
    for (row = 0; row <= RowCount; row++) {
      //alert("validateTableDateRange function. FieldId: " + FieldId + ", row=" + row);
      var lowerDate = getFieldValue(FieldId + "_" + row + "_" + LowerDateColumn);
      var upperDate = getFieldValue(FieldId + "_" + row + "_" + UpperDateColumn);

      if (lowerDate != "" || upperDate != "") {

        var ongoing = getRadioButtonValue(FieldId + "_" + row + "_" + OngoingColumn);

        if (upperDate.length > 8 && lowerDate == "") {
          return FieldName + " table, date range validation ERROR in " + row + ", you must have a Start to have an End date";
        }

        if (upperDate.length > 8 && ongoing == "Yes") {
          return FieldName + " table, date range validation ERROR in " + row + ", you cannot have a Ongoing and a End date";
        }
        if (upperDate.length < 8 && ongoing == "No") {
          return FieldName + " table, date range validation ERROR in " + row + ", when End date is empty, you must select Ongoing.";
        }
        if (lowerDate.length > 8 && upperDate.length > 8) {
          var date_dif = moment(lowerDate, dateFormat).diff(moment(upperDate, dateFormat), 'days');
          if (date_dif > 0) {
            return FieldName + " table, date range validation ERROR in " + row + ", the Start date must be less that the End date.";
          }
        }
      }
    }
    return FieldName + " table, date ranges validated.";
  }



  /* *************************************************************************************
   * sumTableColumn method
   *
   * Description:
   *  This method sums the values of the passed list of variables variable delimiter is
   *  ' ', ',' or ';'
   *
   * ************************************************************************************/
  function sumTableColumn(FieldName, Column) {
    var nValue = 0;
    for (i = 0; i < 30; i++) {
      var fieldName = FieldName + "_" + i + "_" + Column;
      var formField = document.getElementById(fieldName);
      if (formField != undefined) {
        if (isNaN(formField.value) == false) {
          nValue += new Number(formField.value);
        }
      }
    }
    return nValue;
  }


  /* *************************************************************************************
   * sumCategory method
   *
   * Description:
   *  This method sums the values of the passed list of variables variable delimiter is
   *  ' ', ',' or ';'
   *
   * ************************************************************************************/
  function sumNumericCategory(Category) {
    var nValue = 0;
    var categoryField = document.getElementById("EvField_Category_" + Category);

    if (categoryField != undefined) {
      var arrFieldNames = categoryField.value.split(';');
      for (sumIndex = 0; sumIndex < arrFieldNames.length; sumIndex++) {
        var formField = document.getElementById(arrFieldNames[sumIndex]);
        if (formField != undefined) {
          if (formField.type != "radio") {
            if (isNaN(formField.value) == false) {
              if (isNaN(formField.value) == false) {
                nValue += new Number(formField.value);
              }
            }
          }
        }
      }
    }
    return nValue;
  }


  /* *************************************************************************************
   * sumRadioButtonCategory method
   *
   * Description:
   *  This method sums the values of the passed list of variables variable delimiter is
   *  ' ', ',' or ';'
   *
   * ************************************************************************************/
  function sumRadioButtonCategory(Category) {
    var nValue = 0;
    var categoryField = document.getElementById("EvField_Category_" + Category);
    if (categoryField != undefined) {
      var arrFieldNames = categoryField.value.split(';');
      for (sumIndex = 0; sumIndex < arrFieldNames.length; sumIndex++) {
        var value = getRadioButtonValue(arrFieldNames[sumIndex]);
        if (value != undefined) {
          if (isNaN(value) == false) {
            nValue += new Number(value);
          }
        }
      }
    }
    return nValue;
  }


  /* *************************************************************************************
   * sum method
   *
   * Description:
   *  This method sums the values of the passed list of variables variable delimiter is
   *  ' ', ',' or ';'
   *
   * ************************************************************************************/
  function sum(fieldNames) {
    var stFieldNames = fieldNames.replace(" ", ";");
    stFieldNames = stFieldNames.replace(",", ";");
    var nValue = 0;
    var arrFieldNames = stFieldNames.split(';');
    for (sumIndex = 0; sumIndex < arrFieldNames.length; sumIndex++) {
      var value = getFieldValue(arrFieldNames[sumIndex]);
      if (isNaN(value) == false) {
        nValue += value;
      }
    }
    return nValue;
  }


  /* *************************************************************************************
   * Diff method
   *
   * Description:
   *  this method gets the difference between to variables retrieved by name.
   *
   * ************************************************************************************/
  function diff(fieldName1, fieldName2) {
    var nValue = Number.NaN;
    var variable1 = getFieldValueText(fieldName1);
    var variable2 = getFieldValueText(fieldName2);
    if (isNaN(variable1) == false && isNaN(variable2) == false) {
      nValue = variable1 - variable2;
    }
    return nValue;
  }


  /* *************************************************************************************
   * Diff method
   *
   * Description:
   *  this method gets the difference between to variables retrieved by name.
   *
   * ************************************************************************************/
  function mult(fieldName1, fieldName2) {
    var nValue = Number.NaN;
    var variable1 = getFieldValueText(fieldName1);
    var variable2 = getFieldValueText(fieldName2);
    if (isNaN(variable1) == false && isNaN(variable2) == false) {
      nValue = variable1 * variable2;
    }
    return nValue;
  }


  /* *************************************************************************************
   * Diff method
   *
   * Description:
   *  this method gets the difference between to variables retrieved by name.
   *
   * ************************************************************************************/
  function divide(fieldName1, fieldName2) {
    var nValue = Number.NaN;
    var variable1 = getFieldValueText(fieldName1);
    var variable2 = getFieldValueText(fieldName2);
    if (isNaN(variable1) == false && isNaN(variable2) == false) {
      if (variable2 != 0) {
        nValue = variable1 / variable2;
      }
    }
    return nValue;
  }



  /*
   * Export any functions which need to be made publically available
   */
  Evado.Form = {
    onCustomValidation: onCustomValidation,
    onTextValidation: onTextValidation,
    onIndentiferFormat6: onIndentiferFormat6,
    onTextChange: onTextChange,
    onIntegerValidation: onIntegerValidation,
    onNumericValidation: onNumericValidation,
    onRangeValidation: onRangeValidation,
    onDateSelectChange: onDateSelectChange,
    onTimeSelectChange: onTimeSelectChange,
    onDateValidation: onDateValidation,
    onFinishDateValidation: onFinishDateValidation,
    onConsentDateValidation: onConsentDateValidation,
    onBirthDateValidation: onBirthDateValidation,
    onTimeValidation: onTimeValidation,
    onSelectionValidation: onSelectionValidation,
    onQuizValidation: onQuizValidation,
    onQueryField: onQueryField,
    onCommonRecordCompleted_Clicked: onCommonRecordCompleted_Clicked,

    getVariable: getVariable,
    setVariable: setVariable,
    getFieldValue: getFieldValue,
    hasFieldValue: hasFieldValue,
    getNumeric: getNumeric,
    getRadioButtonValue: getRadioButtonValue,
    getCheckBoxValue: getCheckBoxValue,
    getDate: getDate,
    validateTableDateRange: validateTableDateRange,
    sumTableColumn: sumTableColumn,
    sumNumericCategory: sumNumericCategory,
    sumRadioButtonCategory: sumRadioButtonCategory,
    sum: sum,
    diff: diff,
    mult: mult,
    divide: divide
  };
}());
