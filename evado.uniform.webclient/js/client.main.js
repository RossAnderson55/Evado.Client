var Evado = Evado || {};

$(function () {
  var resize = function () {
    var height = $("#page-header-section").height();
    $("#page-body").css({ marginTop: height });

    //console.log("Top margin: " + height);

    var height = $("#form-footer").height();
    $("#page-body").css({ marginBottom: height });

    //console.log("Bottom margin: " + height);

    //var pHeight = $("#page-body").height();
    //$("#pageHeight").value = pHeight;

    //var pWidth = $("#page-body").width();
    //$("#pageWidth").value = pWidth;

    //console.log("wWidth: " + pWidth + ", wHeight: " + pHeight);
  };

  $(window).resize(resize);
  resize();

  //var pHeight = $("#page-body").height();
  //document.getElementById("pageHeight").value = pHeight

  //var pWidth = $("#page-body").width();
  //document.getElementById("pageWidth").value = pWidth;

  //console.log("wWidth: " + pWidth + ", wHeight: " + pHeight);

  $('#pageMenu .nav.nav-pills li a').on('click', function (event) {
    // prevent default anchor behaviour from triggering
    event.preventDefault();

    var btns = $('li', $(this).closest('ul')), thisBtn = $(this).closest('li'), link = $(this).attr('href');

    btns.removeClass('active');

    $('a', btns).each(function (i, btn) {
      if (link != '#')
        $($(btn).attr('href')).hide();
      else
        $($(btn).attr('href')).show();
    });

    // show this section
    thisBtn.addClass('active');
    if (link != '#') $(link).show();
  });

  var init = function () {
    setupValidators();
    initParsley();
  };

  var initParsley = function () {
    var parsleyConfig = {
      errorsContainer: function (pEle) {
        var $err = pEle.$element.closest('.group-row').find('.error-container .cell-error-value span');
        return $err;
      }
    };

    $('#form1').parsley(parsleyConfig);
  };

  var setupValidators = function () {
    window.ParsleyValidator
      .addValidator('integerna', function (value) {
        return value.match(/^-?\d+$|^n\/?a$/i) ? true : false;
      }, 32)
      .addMessage('en', 'integerna', 'This value should be a integer');
  };

  Evado.Uniform = {
    init: init,
    setupValidators: setupValidators
  };

  Evado.Uniform.init();

  $('#form1').on('submit', function (event) {
  });

  $('#form1').on('uniform:field:invalid', function (event, data) {
    var errCell = $(event.target).closest('.group-row').find('.error-container .cell-error-value span')
    errCell.text(data.message);
  });

  $('#form1').on('uniform:field:valid', function (event) {
    var errCell = $(event.target).closest('.group-row').find('.error-container .cell-error-value span')
    errCell.empty();
  });

  window.requiredFieldsIncomplete = [];

  var disableCmdButtonsIfRequired = function (mode, el) {
    if (mode == 'add') {
      requiredFieldsIncomplete.push(el);
    }

    if (mode == 'remove') {
      requiredFieldsIncomplete = _.without(requiredFieldsIncomplete, el);
    }

    if (requiredFieldsIncomplete.length) {
      $('.cmd-button[data-enable-for-mandatory-fields]').attr('disabled', true);
    }
    else {
      $('.cmd-button[data-enable-for-mandatory-fields]').attr('disabled', false);
    }
  };

  $.listen('parsley:field:error', function (event) {
    _.each(event.validationResult, function (vr) {
      if (vr.assert.name === 'required')
        disableCmdButtonsIfRequired('add', event.$element.get(0));
    });
  });

  $.listen('parsley:field:success', function (event) {
    if (event.validationResult) {
      disableCmdButtonsIfRequired('remove', event.$element.get(0));
    }
    else if (!_.findWhere(event.validationResult, function (vr) {
      vr.assert.name === 'required'
    })) {
      disableCmdButtonsIfRequired('remove', event.$element.get(0));
    }
  });

  $('[data-hide-group-if-field-id][data-hide-group-if-field-value]').each(function () {
    var dependantField = $('[name="' + $(this).data('hideGroupIfFieldId') + '"]'),
      that = this;

    dependantField.on('change', function () {
      if (dependantField.is(':radio'))
        var val = dependantField.filter(':checked').val();
      else
        var val = dependantField.val();

      if (val == $(that).data('hideGroupIfFieldValue')) {
        $(that).hide();
      }
      else {
        $(that).show();
      }
    });

    dependantField.trigger('change');
  });

  var fieldsToWatchValue = {};

  $('[data-mandatory-if-field-id][data-mandatory-if-field-value]').each(function () {
    var fieldId = $(this).data('mandatoryIfFieldId');

    if (fieldsToWatchValue[fieldId] == null) {
      fieldsToWatchValue[fieldId] = {};
    }

    fieldsToWatchValue[fieldId][$(this).attr('name')] = $(this).data('mandatoryIfFieldValue');

    // fix parsley issue which is not causing datetime fields to be revalidated on change event
    if ($(this).is('[data-behaviour="datepicker"],[data-behaviour="timepicker"],[data-behaviour="datetimepicker"]')) {
      var that = this;
      $(this).parent().on('change', function () {
        $(that).parsley().validate();
      });
    }
  });

  _.each(fieldsToWatchValue, function (dependantFields, fieldId) {
    var field = $('[name="' + fieldId + '"]');

    field.on('change', function (event) {
      // prevent parsley "Cannot set property 'validatedOnce' of undefined" error
      event.stopImmediatePropagation();

      $('#form1').parsley().destroy();

      _.each(dependantFields, function (mandatoryIfFieldValue, dependantFieldId) {
        var dependantField = $('[name="' + dependantFieldId + '"]');

        if (field.is(':radio'))
          var val = field.filter(':checked').val();
        else
          var val = field.val();

        // add/remove required validator from parsley
        if (val == mandatoryIfFieldValue) {
          dependantField.attr('required', true);
          var node = dependantField.closest('.group-row').find('.cell.title label');
          if (!$('span.required', node).length) node.append('<span class="required"> * </span>');
        }
        else {
          dependantField.attr('required', false);
          dependantField.closest('.group-row').find('.cell.title label span.required').remove();
        }
      });

      initParsley();
      $('#form1').parsley().validate();
    });

    if (field.is(':radio'))
      field.filter(':checked').trigger('change');
    else
      field.trigger('change');
  });
});
