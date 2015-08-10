/// <reference path="../jQuery/jquery-2.1.4.js" />
/// <reference path="../jQuery/jquery-2.1.4-vsdoc.js" />
/// <reference path="byId.js" />
/// <reference path="devOrg.js" />
/// <reference path="jQueryExtend.js" />
/// <reference path="selectors.js" />
/// <reference path="upload.js" />
/// <reference path="constants.js" />
/// <reference path="dynamic-select.js" />
/// <reference path="urls.js" />

/* !Written by Alim Ul Karim
 * Developers Organism
 * Dated : 14 June 2015
 * Version : 1.2
 * https://www.facebook.com/DevelopersOrganism
 * mailto:info@developers-organism.com
*/

; $.app = $.app || {};

$.app.initialize = function () {
    /// <summary>
    /// Initialize everything.
    /// </summary>
    var //self = this,
        dev = $.app,
        uploader = dev.upload,
        constants = dev.constants,
        selectors = dev.selectors,
        urls = dev.urls,
        //regularExpressions = dev.regularExp,
        getUrl = urls.getAbsUrl;

    var $uploadingContainer = $.byId(selectors.UploadingContainerId);
    if ($uploadingContainer.length > 0) {
        uploader.initialize($uploadingContainer,selectors.UploadingFormId, 1, "(\\.|\\/)(gif|jpe?g|png)$");
    }

    var $registerForm = $.byId(selectors.registerFormId);


    if ($registerForm.length > 0) {
        var userName = constants.userName,
            email = constants.email;



        dev.validateInputFromServer(
            "#" + userName,
            getUrl(urls.usernameValidation),
            userName,
            true,
            false,
            3);
        dev.validateInputFromServer(
            "#" + email,
            getUrl(urls.emailValidation),
            email,
            false,
            false,
            4);

        dev.enterToNextInput($registerForm, false);
        //$.app.uxFriendlySlide("form.register-form", true);

        //dev.bootstrapComboSelectbyFindingValue("select.country-combo", "1");


    }

    // make tables look nice with pagination
    var $tables = $("table.bootstrap-table-do");
    if ($tables.length > 0) {
        $tables.bootstrapTable();
    }

    // load dynamic and depended select or combo
    dev.jsonCombo.initialize();


    // make tag component live
    function makeTagLive() {
        var $createdTags = $("div.tag-input-container input[data-provide=tags]").tag();
        if ($createdTags.length > 0) {
            var placeHolder = $createdTags.attr('placeholder');
            $createdTags.siblings("input").attr('placeholder', placeHolder);
        }
    }

    makeTagLive();

    // make all selects to bootstrap select 
    $("select.select-picker").selectpicker();
    $.app.bootstrapComboSelectIndex("select.select-picker", 0);


    ///Implement .rating-5,.rating-10
    $.app.ratingMordernize();
    $.app.bootstrapTabsMordernize(".make-it-tab");


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


    //making textarea's elastic
    $("textarea.elastic").elastic().trigger("update");

    $("div.datetimepicker-start").datetimepicker({
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

    $("div.datepicker-start").datetimepicker({
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
}

$(function () {
    $.app.initialize();
});