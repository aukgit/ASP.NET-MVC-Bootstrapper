﻿/// <reference path="../jQuery/jquery-2.1.3.js" />
/// <reference path="../jQuery/jquery-2.1.3.intellisense.js" />
/// <reference path="../jQuery/jquery.number.js" />
/// <reference path="../jQuery/jquery.unobtrusive-ajax.js" />
/// <reference path="../jQuery/jquery.validate-vsdoc.js" />
/// <reference path="../jQuery/jquery.validate.js" />
/// <reference path="../jQuery/jquery.validate.unobtrusive.js" />
/// <reference path="../jQuery/moment.js" />
/// <reference path="../jQuery/underscore.js" />
/// <reference path="../jQuery/validation.js" />
/// <reference path="../jQuery/Upload/jquery.fileupload.js" />
/// <reference path="../Bootstrap/bootstrap.js" />
/// <reference path="../Bootstrap/bootstrap-select.js" />
/// <reference path="../Bootstrap/bootstrap-table-export.js" />
/// <reference path="../Bootstrap/bootstrap-table-filter.js" />
/// <reference path="../Bootstrap/bootstrap-datetimepicker.js" />
/// <reference path="../Bootstrap/bootstrap-datepicker.js" />
/// <reference path="../Bootstrap/common-tasks-run-every-page.js" />
/// <reference path="../Bootstrap/modernizr-2.8.3.js" />
/// <reference path="../Bootstrap/respond.js" />
/// <reference path="../Bootstrap/star-rating.js" />


/**!
 * Written by Alim Ul Karim
 * Email: devorg.bd{at}gmail.com
 * Dated : 14 Jun 2015
 * Version : 2.2
 * https://www.facebook.com/DevelopersOrganism
 * mailto:info{at}developers-organism.com
 */

$.app = {

    countryFlagRefresh: function (countrySelector, dropDownItemsSelector, dropDownBtnSelector) {
        /// <summary>
        /// all Selectors are jQuery Selector Text  only.
        /// selectpicker will be called inside function, no need to call outside.
        /// </summary>
        /// <param name="countrySelector">Select country and make it selectpicker()</param>
        /// <param name="dropDownItemsSelector"></param>
        /// <param name="dropDownBtnSelector"></param>
        "use strict";
        var countryBox = $(countrySelector).selectpicker(); // only select a select element then apply the custom bootstrap selector
        var dropDownItems = $(dropDownItemsSelector); // getting generated dropdown items from the custom bootstrap selector
        var dropDownBtn = $(dropDownBtnSelector); // generated new button from the selectpicker option
        var skippingClassesAnchor = ["flag-country-combo", "flag"];
        var skippingClassesForBtn = ["btn", "dropdown-toggle", "selectpicker", "btn-success", "flag-combo"];
        // console.log(dropDownItems.length);
        countryBox.change(function (e) {
            var listItem = dropDownItems.find("li.selected");
            var anchorItem = listItem.find("a");
            var listOfAllAnchorClasses = anchorItem.getClassesList();
            var listOfAllClassesdropDownBtn = dropDownBtn.getClassesList();
            var flagClass = $.getArrayExcept(listOfAllAnchorClasses, skippingClassesAnchor);
            var btnFlagClass = $.getArrayExcept(listOfAllClassesdropDownBtn, skippingClassesForBtn);
            for (var i = 0; i < btnFlagClass.length; i++) {
                dropDownBtn.removeClass(btnFlagClass[i]);
            }
            dropDownBtn.addClass("fc-" + flagClass[0]);
        });
    },
    countryRelatedToPhone: function (countrySelector, dropDownItemsSelector, dropDownBtnSelector, phoneNumberInputSelector) {
        /// <summary>
        /// countryFlagRefresh must be called first or selectpicker must be called first
        /// all Selectors are jQuery Selector Text  only.
        /// </summary>
        /// <param name="countrySelector"></param>
        /// <param name="dropDownItemsSelector"></param>
        /// <param name="dropDownBtnSelector"></param>
        /// <param name="phoneNumberInputSelector"></param>
        "use strict";
        var countryBox = $(countrySelector);
        var dropDownItems = $(dropDownItemsSelector);
        //var dropDownBtn = $(dropDownBtnSelector);
        var phoneNumberBox = $(phoneNumberInputSelector);
        var previousCallingCode = "";

        function selectChangeState() {
            // console.log("executed");
            var listItem = dropDownItems.find("li.selected");
            var spanText = listItem.find("a > span").text().toString();
            var newCallingCode = $.app.subString(spanText, "(", ")");
            var getWrittenPhoneNumber = phoneNumberBox.val();
            // console.log(listItem);
            newCallingCode = $.app.replaceStartsWith(newCallingCode, "+", "");
            if ((!$.isEmpty(getWrittenPhoneNumber) && !$.isEmpty(previousCallingCode))
                && $.app.isStartsWith(getWrittenPhoneNumber, previousCallingCode)) {
                getWrittenPhoneNumber = $.app.replaceStartsWith(getWrittenPhoneNumber, previousCallingCode, newCallingCode);
            } else {
                getWrittenPhoneNumber = newCallingCode + getWrittenPhoneNumber;
            }
            previousCallingCode = newCallingCode;
            phoneNumberBox.val(getWrittenPhoneNumber);
        }

        countryBox.ready(selectChangeState).change(selectChangeState);
        // phoneNumberBox.keyup(selectChangeState);
        // $("#selectID option")[index].selected = true;
    },

    subString: function (givenString, startSequence, endingSequence) {
        /// <summary>
        /// Give the sub string by searching the start and end sequence.
        /// subString("Hello (World)", "(", ")") returns "World"
        /// </summary>
        /// <param name="givenString">Given string</param>
        /// <param name="startSequence">What find first.</param>
        /// <param name="endingSequence">What is the ending string.</param>
        "use strict";
        if (_.isString(givenString)) {
            var index1 = givenString.indexOf(startSequence);
            if (index1 > -1) {
                var index2 = givenString.indexOf(endingSequence);
                if (index2 > -1) {
                    // exist
                    return givenString.substr(index1 + 1, index2 - index1 - 1);
                }
            }
        }
        return null;
    },

    getComboString: function (comboName, comboClass, comboId, stringOptionItems, additionalAttributes) {
        /// <summary>
        /// returns a select/combo making string
        /// </summary>
        /// <param name="comboName">Name of the combo/select</param>
        /// <param name="comboClass">Class for the combo/select</param>
        /// <param name="comboId">Just pass the id or give null, it will automatically formatted</param>
        /// <param name="stringOptionItems">Option items passed as an string</param>
        /// <param name="additionalAttributes">Add additional attributes with the select, however user have to format it. Eg. id='hello' </param>
        var self = $.app;
        var selectors = self.selectors;
        if (!$.isEmpty(comboId)) {
            comboId = " id='" + comboId + "' ";
        } else {
            comboId = "";
        }
        if ($.isEmpty(comboClass)) {
            comboClass = "";
        }
        if ($.isEmpty(stringOptionItems)) {
            stringOptionItems = "";
        }
        if ($.isEmpty(comboName)) {
            comboName = "";
        } else {
            comboName = " name='" + comboName + "' ";
        }
        var comboString = "<select " + comboName +
                              " class='" + selectors.genericComboClass +
                              " form-control " + comboClass +
                              " '" + comboId +
                              " data-style='" + comboClass + "' " +
                              additionalAttributes +
                              " data-live-search='true'>" +
                              stringOptionItems +
                              " </select>";

        return comboString;
    },
    getComboOptionsStringFromJson: function (jsonItems, extraHtmlWithEachElement, itemClasses) {
        /// <summary>
        /// Generates and append "option" items to the given $select. 
        /// </summary>
        /// <param name="jsonItems">must contain display and id value for every 'option' item.</param>
        /// <param name="extraHtmlWithEachElement">add the extra html content with option display value</param>
        /// <param name="itemClasses">add classes with each option.</param>
        if ($.isEmpty(itemClasses)) {
            itemClasses = "";
        }
        if ($.isEmpty(extraHtmlWithEachElement)) {
            extraHtmlWithEachElement = "";
        }
        if (jsonItems.length > 0) {
            var length = jsonItems.length;
            var options = new Array(length + 5);
            var selected = " selected='selected' ";
            var optionStarting = "<option class='" + itemClasses + "'";
            var optionEnding = "</option>";
            for (var i = 0; i < length; i++) {
                if (i !== 0 && selected !== "") {
                    selected = ""; //only first one will be selected
                }
                options[i] = optionStarting +
                             selected +
                             " value='" +
                             jsonItems[i].id +
                             "'>" +
                             extraHtmlWithEachElement +
                             jsonItems[i].display +
                             optionEnding;
            }
            return options.join("");
        }
        return "";
    },
    getWholeComboStringWithJsonItems: function (jsonItems, comboName, comboClass, comboId, additionalAttributesWithCombo, extraHtmlWithEachElement, eachOptionItemClasses) {
        /// <summary>
        /// Returns a full combo/select based on json items
        /// Developer should inject this into document
        /// </summary>
        /// <param name="comboName">Name of the combo/select</param>
        /// <param name="comboClass">Class for the combo/select</param>
        /// <param name="comboId">Just pass the id or give null, it will automatically formatted</param>
        /// <param name="stringOptionItems">Option items passed as an string</param>
        /// <param name="additionalAttributes">Add additional attributes with the select, however user have to format it. Eg. id='hello' </param>
        /// <param name="jsonItems">must contain display and id value for every 'option' item.</param>
        /// <param name="extraHtmlWithEachElement">add the extra html content with option display value</param>
        /// <param name="itemClasses">add classes with each option.</param>
        var self = $.app;
        var optionsString = self.getComboOptionsStringFromJson(jsonItems, extraHtmlWithEachElement, eachOptionItemClasses);
        var comboString = self.getComboString(comboName, comboClass, comboId, optionsString, additionalAttributesWithCombo);
        return comboString;
    },
    smartDependableCombo: function (parentSelectsjQuerySelector,
                                    mainDivContainerSelector,
                                    innerDivSelectorForPlacingCombo,
                                    receivingJsonUrl,
                                    placingComboName,
                                    placedComboId,
                                    placedComboClass,
                                    placedComboAdditionalAttributes,
                                    placedComboAdditionalClassesWithEachItem,
                                    placedComboAdditionalHtmlWithEachItem) {
        /// <summary>
        /// Create dependable combo based on parent and given url to get json list.
        /// Warning: No combo/select will appear , even the main div will disappear if no item is received from the receivingJsonUrl.
        /// </summary>
        /// <param name="parentSelectsjQuerySelector">Write jQuery selector for only the parent combo/select</param>
        /// <param name="mainDivContainerSelector">Main container div of that select/combo. Reason is to hide all including labels when no items found from url.</param>
        /// <param name="innerDivSelectorForPlacingCombo">Where to place the combo/slect</param>
        /// <param name="receivingJsonUrl">Url to get json list, data must contain at least {display= value to display, id = value to put}</param>
        /// <param name="placingComboName">Name of the select/combo</param>
        /// <param name="placedComboId">Id of the select/combo</param>
        /// <param name="placedComboClass">classes of the select/combo</param>
        /// <param name="placedComboAdditionalAttributes">Add additional attributes with the select, however user have to format it. Eg. id='hello'</param>
        /// <param name="placedComboAdditionalClassesWithEachItem">Add extra classes with every option, only write the class names with space.</param>
        /// <param name="placedComboAdditionalHtmlWithEachItem">Add extra html content with each option item</param>
        var $parentCombo = $(parentSelectsjQuerySelector);
        var self = $.app;

        if ($.isEmpty($parentCombo)) {
            console.error.log("error raised from developers organism component's smartDependableCombo that no parent is detected.");
            return; // nothing exist in parent.
        }
        // row container
        var $mainDiv = $(mainDivContainerSelector);
        // container for  the select
        var $innerDiv = $mainDiv.find(innerDivSelectorForPlacingCombo);

        function hideDiv() {
            if ($mainDiv.length > 0) {
                $mainDiv.hide();
            } else {
                console.error.log("devOrg->smartDependableCombo: main div not found for '" + mainDivContainerSelector + "'");
            }
        }

        hideDiv();

        function removeSelectIfExist() {
            var options = $innerDiv.find("select, div.bootstrap-select");
            if (options.length > 0) {
                options.remove();
            }
        }

        function createCombo(responseJson) {
            //(comboName, comboClass, comboId, additionalAttributes, jsonItems, extraHtmlWithEachElement, itemClasses)
            var comboString = self.getWholeComboStringWithJsonItems(responseJson, placingComboName, placedComboClass, placedComboId, placedComboAdditionalAttributes, placedComboAdditionalHtmlWithEachItem, placedComboAdditionalClassesWithEachItem);
            //var insideDivHtml = $innerDiv.html();
            //var wholeCombo = comboString + insideDivHtml;
            $innerDiv.prepend(comboString);

            var $combo = $innerDiv.find("select");
            $combo.selectpicker();
        }
        //when parent combo/select is changed
        $parentCombo.change(function () {
            /// <summary>
            /// What will happen when 
            /// parent combo/select changes item.
            /// </summary>
            var parentComboValue = $parentCombo.val();
            var actualUrl = receivingJsonUrl + "/" + parentComboValue;
            $.ajax({
                type: "POST",
                dataType: "JSON",
                url: actualUrl,
                success: function (response) {
                    if (response.length === 0) {
                        hideDiv();
                        return;
                    }
                    $innerDiv = $(mainDivContainerSelector + " " + innerDivSelectorForPlacingCombo);
                    // items exist.
                    // $innerDiv is used to check if select exist or not.
                    removeSelectIfExist(); //remove inner options if exist any
                    $mainDiv.show("slow");
                    createCombo(response); // create if necessary and then append options to it.
                },
                error: function (xhr, status, error) {
                    hideDiv();
                }
            });
        });
    },


    bootstrapComboSelectbyFindingValue: function (comboSelector, searchForvalue) {
        /// <summary>
        /// Select bootstrap selectpicker index by searching the item.
        /// </summary>
        /// <param name="comboSelector"></param>
        /// <param name="searchForvalue"></param>
        $(comboSelector).selectpicker("val", searchForvalue).trigger("change");
    },
    bootstrapComboSelectIndex: function (comboSelector, index) {
        /// <summary>
        /// Select bootstrap selectpicker index.
        /// Under the hood search the value for the index and then selects it.
        /// </summary>
        /// <param name="comboSelector">Combo jQuery selector.</param>
        /// <param name="index">Index to select.</param>
        var $combo = $(comboSelector + ">option");
        if ($combo.length > 0 && index <= ($combo.length - 1)) {

            var itemFound = $($combo[index]);
            var value = itemFound.val();
            $.app.bootstrapComboSelectbyFindingValue(comboSelector, value);
        }
    },

    // givenString "Example ( Hello )" 
    // startsWith= "Example" ; returns true.
    isStartsWith: function (givenString, startsWith) {
        if (_.isString(givenString)) {
            var subtringOfGiventext = givenString.substr(0, startsWith.length);
            if (subtringOfGiventext === startsWith) {
                return true;
            }
        }
        return false;
    },

    replaceStartsWith: function (givenString, findStartsWith, replaceString) {
        if (_.isString(givenString) && !$.isEmpty(findStartsWith)) {
            var subtringOfGiventext = givenString.substr(0, findStartsWith.length);
            if (subtringOfGiventext === findStartsWith) {
                var nextStringIndex = findStartsWith.length;
                var otherHalftext = givenString.substr(nextStringIndex, givenString.length - nextStringIndex);
                return replaceString + otherHalftext;
            }
        }
        return givenString;
    },

    enterToNextInput: function ($form, submitAtLast) {
        /// <summary>
        /// Any kind of input(text, password, email, date) and select 
        /// will be focused to the next one.
        /// </summary>
        /// <param name="$form">jQuery form element.</param>
        /// <param name="submitAtLast">True: means submit when reach the last textbox. Usually false would be better choice so that programmer can handle things in other places.</param>
        if ($form.length === 0) {
            return;
        }
        $form.find("input:text:first-child").focus();
        
        //var binders = formSelector + " input[type='text']:visible," +
        //    formSelector + " input[type='password']:visible," +
        //    formSelector + " input[type='numeric']:visible," +
        //    formSelector + " input[type='email']:visible," +
        //    //formSelector + " textarea:visible," +
        //    formSelector + " button.selectpicker[type='button']:visible," +
        //    formSelector + " select:visible";
        var binders = "input[type='text']:visible," +
                     "input[type='password']:visible," +
                     "input[type='numeric']:visible," +
                     "input[type='email']:visible," +
                    //formSelector + " textarea:visible," +
                     "button.selectpicker[type='button']:visible," +
                     "select:visible";
        $form.on("keypress", binders, function (e) {
            // var codeAbove = d.keyCode || d.which;
            // console.log("above code :" + codeAbove);
            var code = e.keyCode || e.which;
            // console.log("inside code :" + code);
            if (code === 13) { // Enter key
                e.preventDefault(); // Skip default behavior of the enter key
                var $binders = $form.find(binders);
                var n = $binders.length;
                var nextIndex = $binders.index(this) + 1;
                if (nextIndex < n) {
                    $(binders)[nextIndex].focus();
                } else {
                    $(binders)[nextIndex - 1].blur();
                    if (submitAtLast === true) {
                        $form.submit();
                    }
                }
            }
        });
    },

    validateTextInputBasedOnRegEx: function (jQuerySelectorforTextBox, stringRegEx, msgOnInvalidPattern) {
        "use strict";
        /// <summary>
        ///     Validate text input while typing with ASP.NET jquery validation.
        ///     Only the attributes with the text. No event is bound.
        /// </summary>
        /// <param name="jQuerySelectorforTextBox">string:jQuery Selector</param>
        /// <param name="stringRegEx">string: Regular expression to validate the textinput</param>
        var $elementFound = $(jQuerySelectorforTextBox);

        $elementFound.attr("data-val-regex-pattern", stringRegEx);
        if (!$.isEmpty(msgOnInvalidPattern)) {
            $elementFound.attr("data-val-regex", msgOnInvalidPattern);
        }
    },

    reSetupjQueryValidate: function (jQueryFormSelector) {
        "use strict";
        /// <summary>
        ///     call after setting new reg ex via validateTextInputBasedOnRegEx
        /// </summary>
        /// <param name="jQueryFormSelector"></param>
        var $form = $(jQueryFormSelector)
            .removeData("validator") /* added by the raw jquery.validate plugin */
            .removeData("unobtrusiveValidation");
        /* added by the jquery unobtrusive plugin */
        $.validator.unobtrusive.parse($form);
    },

    validateTextInputsBasedOnHiddenSpansGiven: function (formSelector) {
        "use strict";
        /// <summary>
        ///     inComplete
        ///     There should be span for each of inputs that needs to be modified or changed.
        ///     hidden span with data-name="same as input"
        ///     data-display, data-reg, data-reg-fail-msg, data-min,data-max,data-range-failed-msg
        ///     data-placeholder, data-class
        /// </summary>
        /// <param name="formSelector">jQuery form selector</param>
        var $form = $(formSelector);
        var dataRegSpanAttr = "data-reg";
        var dataRegMsgSpanAttr = "data-reg-fail-msg";
        var dataMinSpanAttr = "data-min";
        var dataMaxSpanAttr = "data-max";
        var dataRangeMsgSpanAttr = "data-min";
        var dataDisplaySpanAttr = "data-range-failed-msg";
        var dataMinSpanAttr = "data-min";

        if ($form.length > 0) {
            var binders = "input[type='text']," +
                "input[type='password']," +
                "input[type='email']," +
                "input[type='numeric']," +
                "select:visible";
            var $inputs = $form.find(binders);
            for (var i = 0; i < $inputs.length; i++) {
                var $input = $($inputs[i]);
                var $nameOftheInput = $input.attr("name");
                // now find the span based on the name
                var $span = $form.find("span[data-name='" + $nameOftheInput + "']:hidden");
                if ($span.length > 0) {
                    // set the attr to the input.

                    // setting reg ex

                }
            }
        }
    },

    validateInputFromServer: function (jQuerytextBoxSelector, validationUrl, internalValidatorSpanClassName, isAlwaysFocusUntilValid, isDisable, minChars, isSubmitTheWholeForm, onInvalidStringStatementInCrossMark, onValidStringStatementInCheckMark, $formGiven, maxTryLimit) {
        /// <summary>
        ///     Made validation easy on the fly with a server response.
        /// </summary>
        /// <param name="jQuerytextBoxSelector">string: jQuery Selector</param>
        /// <param name="validationURL">string: Url to validate</param>
        /// <param name="internalValidatorSpanClassName">Propertyname (refer to the class of validation span) Indicating class.</param>
        /// <param name="isAlwaysFocusUntilValid">Boolean: Keep the focus until it's valid. By default: false</param>
        /// <param name="isDisable">Boolean: Disable the textbox after being validated. By default : false.</param>
        /// <param name="minChars">number: min chars to send the request</param>
        /// <param name="isSubmitTheWholeForm">Boolean:Rather than submitting the small form submit the whole related closet form.</param>
        /// <param name="onInvalidStringStatementInCrossMark">invalid statement show on the cross mark.</param>
        /// <param name="onValidStringStatementInCheckMark">
        ///     valid statement show on the check mark. By default: fieldDisplayname +
        ///     is available and valid
        /// </param>

        "use strict";

        //if ($.isEmpty(isSubmitTheWholeForm)) {
        //    isSubmitTheWholeForm = false;
        //}

        //if ($.isEmpty(isAlwaysFocusUntilValid)) {
        //    isAlwaysFocusUntilValid = false;
        //}

        //if ($.isEmpty(isDisable)) {
        //    isDisable = false;
        //}

        var sentRequestCount = 0;
        var $userTextbox = $(jQuerytextBoxSelector);
        if ($userTextbox.length > 0) {
            $userTextbox.removeAttr("isDisable");

            if (!isSubmitTheWholeForm) {
                $userTextbox.keyup(function () {
                    $("#validation #id").val($userTextbox.val());
                    // console.log(user);
                }).focus(function () {
                    $("#validation #id").val($userTextbox.val());
                    // console.log(user);
                });
            }

            $userTextbox.blur(function () {
                "use strict";
                if (!isSubmitTheWholeForm) {
                    $("#validation #id").val($userTextbox.val());
                }
                var $passingText = $userTextbox.val();
                if ($.isEmpty($passingText) || $passingText.length < minChars) {
                    // if empty text then don't send.
                    return;
                }
                if ($.isEmpty(onValidStringStatementInCheckMark)) {
                    onValidStringStatementInCheckMark = "is available and valid.";
                }
                if ($.isEmpty(onInvalidStringStatementInCrossMark)) {
                    onInvalidStringStatementInCrossMark = "is not valid or already exist. Your input can't contain ( [ ] ' , * & ? \" ) or space or any other special character for this data-type.";
                }

                // Validation should be a formData underlying the original from with 
                // only antiforgery token and a hidden id field
                // whatever is typed in that selected text box will be pushed into
                // this formData
                var formData;
                if ($formGiven === null || $formGiven === undefined || $formGiven.length == 0) {
                    if (!isSubmitTheWholeForm) {
                        formData = $.byId("validation").serialize();
                    } else {
                        formData = $userTextbox.closest("form").serializeArray();
                    }
                } else {
                    formData = $formGiven.serializeArray();
                }

                //console.log(formData);

                var validatorName = "span.CustomValidation." + internalValidatorSpanClassName;
                var token = $("input[name=__RequestVerificationToken]").val();
                var processingState1 = "glyphicon-refresh";
                var processingState2 = "glyphicon-spin";
                var isHideClass = "hide";
                var colorGreen = "green";
                var colorRed = "red";
                var correctState = "glyphicon-ok";
                var incorrectState = "glyphicon-remove";
                var $validatorBox = $(validatorName);
                var displayName = $validatorBox.attr("data-display");
                var correctStateTitle = displayName + " " + onValidStringStatementInCheckMark;
                var invalidAttrName = "data-invalid";
                var incorrectStateTitle = displayName + " " + onInvalidStringStatementInCrossMark;
                var tooltipName = "a.CustomValidation." + internalValidatorSpanClassName + ".tooltip-show";
                var $tooltipBox = $(tooltipName);

                // console.log($("#validation #id").val());
                $validatorBox.removeClass(incorrectState).removeClass(correctState);


                // if no processing state then add it
                if (!$validatorBox.hasClass(processingState1)) {
                    $validatorBox.addClass(processingState1);
                }

                if (!$validatorBox.hasClass(processingState2)) {
                    $validatorBox.addClass(processingState2);
                }
                if ($validatorBox.hasClass(isHideClass)) {
                    $validatorBox.removeClass(isHideClass);
                }
                $tooltipBox.attr("data-original-title", "Validating " + displayName)
                    .attr("title", "Validating " + displayName);
                // confirming processing state.
                if (maxTryLimit !== null && maxTryLimit !== undefined && sentRequestCount > maxTryLimit) {
                    return;
                }
                $.ajax({
                    type: "POST",
                    dataType: "JSON",
                    url: validationUrl,
                    data: formData,
                    success: function (response) {
                        sentRequestCount = sentRequestCount + 1;
                        // Remove the processing state
                        if ($validatorBox.hasClass(processingState1)) {
                            $validatorBox.removeClass(processingState1);
                        }

                        if ($validatorBox.hasClass(processingState2)) {
                            $validatorBox.removeClass(processingState2);
                        }
                        if ($validatorBox.hasClass(isHideClass)) {
                            $validatorBox.removeClass(isHideClass);
                        }
                        // Remove the processing state
                        if (response == true) {
                            if ($validatorBox.hasClass(incorrectState)) {
                                $validatorBox.removeClass(incorrectState);
                            }
                            if ($validatorBox.hasClass(colorRed)) {
                                $validatorBox.removeClass(colorRed);
                            }
                            $validatorBox.addClass(colorGreen)
                                .addClass(correctState)
                                .attr("title", correctStateTitle);

                            $tooltipBox.attr("title", correctStateTitle)
                                .attr("data-original-title", correctStateTitle);
                            if (isDisable) {
                                $userTextbox.prop("isDisable", true);
                            }

                            $userTextbox.addClass("bold")
                                .addClass("green")
                                .next()
                                .focus();

                            $userTextbox.removeAttr(invalidAttrName);
                        } else {
                            if ($validatorBox.hasClass(colorGreen)) {
                                $validatorBox.removeClass(colorGreen);
                            }
                            if ($validatorBox.hasClass(correctState)) {
                                $validatorBox.removeClass(correctState);
                            }
                            $userTextbox.prop("isDisable", false)
                                .addClass("bold")
                                .addClass("red");

                            $validatorBox.addClass(colorRed)
                                .addClass(incorrectState)
                                .attr("title", incorrectStateTitle);

                            $tooltipBox.attr("title", incorrectStateTitle)
                                .attr("data-original-title", incorrectStateTitle);
                            if (isAlwaysFocusUntilValid === true) {
                                $userTextbox.focus();
                            }
                            $userTextbox.attr(invalidAttrName, "true");
                        }
                        $(".tooltip-show").tooltip();

                    },
                    error: function (xhr, status, error) {
                        // Remove the processing state
                        if ($validatorBox.hasClass(processingState1)) {
                            $validatorBox.removeClass(processingState1);
                        }

                        if ($validatorBox.hasClass(processingState2)) {
                            $validatorBox.removeClass(processingState2);
                        }
                        if ($validatorBox.hasClass(isHideClass)) {
                            $validatorBox.removeClass(isHideClass);
                        }
                        // Remove the processing state
                        if ($validatorBox.hasClass(correctState)) {
                            $validatorBox.removeClass(correctState);
                        }

                        if ($validatorBox.hasClass(colorGreen)) {
                            $validatorBox.removeClass(colorGreen);
                        }
                        $userTextbox.prop("isDisable", false)
                            .addClass("bold")
                            .addClass("red");

                        $validatorBox.addClass(colorRed)
                            .addClass(incorrectState)
                            .attr("title", error);

                        $tooltipBox.attr("title", status)
                            .attr("data-original-title", error);

                        $(".tooltip-show").tooltip();
                        $userTextbox.attr(invalidAttrName, "true");
                    }
                }); // ajax end
            });
        }; // if else end
    },


    fillRegisterFieldsOnDemo: function () {
        /// <summary>
        /// Test Function
        /// </summary>
        var i = 0;
        var controls = $(".form-group");
        var $fields = controls.find("input[type=text]");
        $.each($fields, function () {
            this.value = 1111111111111;
        });

        $fields = controls.find("input[type=password]");
        $.each($fields, function () {
            this.value = "asdf1234@";
        });


        $fields = controls.find("input[type=number]");
        $.each($fields, function () {
            this.value = i++;
        });

        $fields = controls.find("textarea");
        $.each($fields, function () {
            this.value = "1111111111111";
        });

        $fields = controls.find("input[type=email]");
        $.each($fields, function () {
            this.value = "auk.junk@live.com";
        });

        $fields = controls.find("input[type=checkbox]");
        $.each($fields, function () {
            this.prop("checked", true);
        });

    },
    //'.make-it-tab'
    bootstrapTabsMordernize: function (tabSelector) {
        /// <summary>
        /// give jQuery selector to add tab functionality
        /// </summary>
        /// <param name="tabSelector"></param>
        "use strict";
        var bootstrapTabs = $(tabSelector);
        if (bootstrapTabs.length > 0) {
            var tabHidden = $(".tab-content input[type='hidden'][name='tab']");

            if (tabHidden.length > 0) {
                var tabHiddenValue = tabHidden.val();
                if (!$.isEmpty(tabHiddenValue)) {
                    //tab name exist
                    bootstrapTabs.find("li>a[href='" + tabHiddenValue + "']").tab("show");
                } else {
                    //no tab name exist.. select default one.
                    bootstrapTabs.find("li > a:first").tab("show");
                }
            }

            bootstrapTabs.click(function (e) {
                //e.preventDefault();                    
                e.preventDefault();
                $(this).tab("show");

            });

            $("ul" + tabSelector + ".nav-tabs > li > a").on("shown.bs.tab", function (e) {
                var valueOfActive = $(e.target).attr("href");
                // = $(tabSelector + " li.active>a").attr('href');
                tabHidden.val(valueOfActive);
                //window.location.hash = id;
            });
        }
    },
    ratingMordernize: function () {

        var ratingItems = $(".rating-5");

        if (ratingItems.length > 0) {
            ratingItems.rating({
                showClear: false
            });
        }
        ratingItems = $(".rating-10");

        if (ratingItems.length > 0) {
            ratingItems.rating({
                showClear: false,
                starCaptionClasses: {
                    0.5: "label label-danger",
                    1: "label label-danger",
                    1.5: "label label-danger",
                    2: "label label-danger",
                    2.5: "label label-danger",
                    2: "label label-warning",
                    2.5: "label label-warning",
                    3: "label label-warning",
                    3.5: "label label-warning",
                    4: "label label-warning",
                    4.5: "label label-warning",
                    5: "label label-warning",
                    5.5: "label label-info",
                    6: "label label-info",
                    6.5: "label label-info",
                    7: "label label-info",
                    7.5: "label label-primary",
                    8: "label label-primary",
                    8.5: "label label-success",
                    9: "label label-success",
                    9.5: "label label-success",
                    10: "label label-success"
                }
            });
        }
    },

    uxFriendlySlide: function (jQueryformSelector, keepOthersVisible, dontSubmit) {
        /// <summary>
        ///     hides except for the first div with value 0. Add attributes to divs
        ///     [data-dev-slide='number-zero-based'][data-dev-visited='false'] and
        ///     encapsulate inputs. Each click clicked on submit it will verify the inputs if verified next hide ones will be shown
        ///     it will be continuous process until hit the last.
        ///     Always use lower case false
        ///     [data-dev-slide='number-zero-based'][data-dev-visited='false']
        /// </summary>
        /// <param name="jQueryformSelector">jQuery selector for the form</param>
        /// <param name="keepOthersVisible">Should add new hide ones or previous ones hides and load new ones(divs)</param>
        /// <param name="dontSubmit">When none left , do we submit? True: don't submit</param>
        "use strict";
        var $form = $(jQueryformSelector),
            $slides = $form.find("[data-dev-slide][data-dev-visited='false']"),
            //executedOnce = false,
            //binders = "input[type='text']:visible," +
            //    "input[type='password']:visible," +
            //    "input[type='email']:visible," +
            //    "input[type='numeric']:visible," +
            //    "select:visible",
            order = 0,
            totalSliderLength = $slides.length;
             //previousSlideNumber = 0,
             //self = $.app;

        if (totalSliderLength > 0) {
            // exist slides.
            $slides.hide();
            //previousSlideNumber = order;
            $slides.filter("[data-dev-slide='" + (order++) + "'][data-dev-visited='false']").show();
            $form.submit(function (e) {
                e.preventDefault();
                var $nextOne = $slides.filter("[data-dev-slide='" + order + "'][data-dev-visited='false']");
                // if (nextOne.length == 0) {
                //    for (order += 1; nextOne.length == 0 && totalSliderLength >= order; order++) {
                //        nextOne = slideObjects.filter("[data-dev-slide='" + order + "'][data-dev-visited='false']");
                //    }
                // }
                var $previousSlide;
                var $inputBoxes;
                if ($nextOne.length > 0) {
                    $previousSlide = $slides.filter("[data-dev-slide='" + (order - 1) + "']"); // console.log(previousOne);
                    $inputBoxes = $previousSlide.find("input, textarea"); // still exist , prevent submission
                    if ($inputBoxes.length > 0 && $.checkValidInputs($inputBoxes)) {
                        if (!keepOthersVisible) {
                            $previousSlide.hide("slow");
                        }
                        //console.log(inputBoxes);
                        //console.log(binders);
                        if (!$nextOne.prop("data-dev-visited")) {
                            $nextOne.attr("data-dev-visited", "true");
                            $nextOne.show("slow");
                            //console.log(nextOne);
                            order++;
                        }
                    } else {
                        //console.log("no inboxes");
                    }
                } else {
                    // nothing left.
                    // sttil check the validation.
                    $previousSlide = $slides.filter("[data-dev-slide='" + (order - 1) + "']"); // console.log(previousOne);
                    $inputBoxes = $previousSlide.find("input");
                    if ($inputBoxes.length > 0 && $.checkValidInputs($inputBoxes)) {
                        if (!dontSubmit) {
                            this.submit();
                        }
                    }
                }


            });

            // var notVisited = slideObjects.filter("[data-dev-visited='false']");
        }
    }

    
};
