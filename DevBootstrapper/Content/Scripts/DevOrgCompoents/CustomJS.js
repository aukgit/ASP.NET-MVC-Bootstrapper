/// <reference path="../jQuery/jquery-2.1.3.js" />
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
/// <reference path="DevOrgDynamicSelect.js" />
/// <reference path="DevOrgComponent.js" />

/*
 * Written by Alim Ul Karim
 * Developers Organism
 * https://www.facebook.com/DevelopersOrganism
 * mailto:info@developers-organism.com
*/

$(function () {
    //tooltip trigger


    $.devOrg.Constants = {
        registerForm: $("form.register-form"),
        countryComboSelector: ".form-control.selectpicker.country-combo",
        countryDropDownItemsSelector: "div.country-combo ul",
        btnSelector: "button.btn.dropdown-toggle.selectpicker.btn-success.flag-combo",
        userName: "UserName",
        email: "Email",
        phoneNumberSelector: "#Phone",
        // "/Validator/"
        validatorCommonUrl: "/Validator/",
        //"/Validator/Username"
        usernameValidationUrl: "/Validator/Username",
        //"/Validator/Email"        
        emailAddressValidationUrl: "/Validator/Email",
        timeZoneJsonUrl: "/Common/GetTimeZone", // look like this /Common/GetTimeZone/CountryID
        languageJsonUrl: "/Common/GetLanguage" // look like this /Common/GetTimeZone/CountryID
    };

    if ($.devOrg.Constants.registerForm.length > 0) {

        $.devOrg.validateInputFromServer("#" + $.devOrg.Constants.userName,
            $.devOrg.Constants.usernameValidationUrl,
            $.devOrg.Constants.userName,
            true,
            false,
            3);
        $.devOrg.validateInputFromServer("#" + $.devOrg.Constants.email,
            $.devOrg.Constants.emailAddressValidationUrl,
            $.devOrg.Constants.email,
            false,
            false,
            4);

        $.devOrg.enterToNextTextBox(".register-form", false);
        //$.devOrg.uxFriendlySlide("form.register-form", true);
        $.devOrg.countryFlagRefresh($.devOrg.Constants.countryComboSelector,
            $.devOrg.Constants.countryDropDownItemsSelector,
            $.devOrg.Constants.btnSelector);


        //country dependable load
        $.devOrg.smartDependableCombo("select.country-combo",
            ".timezone-main",
            ".timezone-combo-div",
            $.devOrg.Constants.timeZoneJsonUrl,
            "UserTimeZoneID", //name
            "", //id
            "btn-success", //class
            ""
        );
        $.devOrg.smartDependableCombo("select.country-combo",
            ".language-main",
            ".language-combo-div",
            $.devOrg.Constants.languageJsonUrl,
            "CountryLanguageID", //name
            "", //id
            "btn-success", //class
            ""
        );
        $("button.fillit").click(function () {
            $.devOrg.fillRegisterFieldsOnDemo();
        });
        $.devOrg.bootstrapComboSelectbyFindingValue("select.country-combo", "1");

        $.devOrg.countryRelatedToPhone($.devOrg.Constants.countryComboSelector,
          $.devOrg.Constants.countryDropDownItemsSelector,
          $.devOrg.Constants.btnSelector,
          $.devOrg.Constants.phoneNumberSelector);

    }


    var $tables = $("table.bootstrap-table-do");
    if ($tables.length > 0) {
        $tables.bootstrapTable();
    }

    $.devOrg.dynamicSelect.initalize();//load dynamic select

    $("select.selectpicker").selectpicker();
    $.devOrg.bootstrapComboSelectIndex("select.selectpicker", 0);


    ///Implement .rating-5,.rating-10
    $.devOrg.ratingMordernize();
    $.devOrg.bootstrapTabsMordernize(".make-it-tab");


    function workWithMenuPage() {
        var menuPage = $("#menu-item-edit-page");
        if (menuPage.length > 0) {
            var div = $("#hasDropdownDiv");
            div.hide();
            $("#HasDropDown").click(function () {
                if (this.checked) {
                    div.show("slow");
                } else {
                    div.hide("slow");
                }
            });
        }
    }

    workWithMenuPage();


    $("textarea.big-multiline").focus(function () {
        $(this).animate({ 'height': "300px", 'width': "630px", 'max-width': "630px" }, 400);
    }).blur(function () {
        $(this).animate({ 'height': "auto", 'width': "294px", 'max-width': "294px" }, 400);
    });
    //making textarea's elastic
    $("textarea").elastic().trigger("update");

    $(".datetimepicker").datetimepicker({
        pickDate: true, //en/disables the date picker
        pickTime: true, //en/disables the time picker
        useMinutes: true, //en/disables the minutes picker
        useSeconds: true, //en/disables the seconds picker
        useCurrent: true, //when true, picker will set the value to the current date/time     
        minuteStepping: 1, //set the minute stepping
        defaultDate: "", //sets a default date, accepts js dates, strings and moment objects
        disabledDates: [], //an array of dates that cannot be selected
        enabledDates: [], //an array of dates that can be selected
        sideBySide: true //show the date and time picker side by side

    });

    $(".datepicker").datetimepicker({
        pickDate: true, //en/disables the date picker
        pickTime: false, //en/disables the time picker
        useMinutes: false, //en/disables the minutes picker
        useSeconds: false, //en/disables the seconds picker
        useCurrent: true, //when true, picker will set the value to the current date/time     
        minuteStepping: 1, //set the minute stepping
        defaultDate: "", //sets a default date, accepts js dates, strings and moment objects
        disabledDates: [], //an array of dates that cannot be selected
        enabledDates: [], //an array of dates that can be selected

        sideBySide: true //show the date and time picker side by side

    });

    
});