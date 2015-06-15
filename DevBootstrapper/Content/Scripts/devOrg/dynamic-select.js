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

/**!
 * Written by Alim Ul Karim
 * Email: devorg.bd{at}gmail.com
 * Dated : 10 Jun 2015
 * Version : 1.0
 * Performance : http://jsperf.com/jquery-vs-fasterjquery
 * https://www.facebook.com/DevelopersOrganism
 * mailto:info{at}developers-organism.com
 */
; $.devOrg = $.devOrg || {};


$.jsonCombo = {
    isDependableAttribute: "data-dependable",
    dependablePropertyNameAttribute: "data-dependable-prop-name",
    propertyNameAttribute: "data-prop",
    dynamicSelectClass: "dynamic-select-load",
    urlAttribute: "data-url",
    dataValueAttribute: "data-value",
    isDynamicSelectElementAttribute: "data-dev-dynamic-select",
    additionalCssAttribute: "data-additional-css",
    liveSearchAttribute: "data-live-search",
    idAttribute: "data-id",
    isHtmlAttribute: "dev-isHtml",
    isStyledAttribute: "data-is-selectpicker",
    $dynamicSelectContainerDiv: $("div.dynamic-select-container[data-dynamic-select-container=true]"),
    $allDynamicImmidiaeSelectDivs: null, // will be defined from initialize function
    $dependancySelectsHasNotProcessed: [], //only populated if a dependency combo can't find parent.
    spineerSelectors: "[data-progress-bar=true]",
    hidableContentSelector: "[data-hidable=true]",
    $hideableContents : null,
    $spinners: null,

    isEmpty: function (variable) {
        return variable === undefined || variable === null || variable === '' || variable.length === 0;
    },
    getDataId: function ($div) {
        /// <summary>
        /// returns data-id='attribute value'
        /// </summary>
        /// <param name="$div"></param>
        return $div.attr($.jsonCombo.idAttribute);
    },
    getSpinner: function (propName) {
        return $.jsonCombo.$spinners.filter("[" + $.jsonCombo.propertyNameAttribute + "='" + propName + "']");
    },
    getIdAsAttribute: function ($div) {
        /// <summary>
        /// returns string as formatted attribute
        /// if id exist then it returns
        /// id = 'idValueGiven'
        /// else returns ''
        /// </summary>
        /// <param name="$div"></param>
        var id = $.jsonCombo.getDataId($div);
        if ($.jsonCombo.isEmpty(id) === false) {
            id = " id='" + id + "' ";
        } else {
            id = "";
        }
        return id;
    },
    initialize: function (additionalSelector) {
        /// <summary>
        /// select div and push info based on html properties
        /// class="dynamic-select-load"
        ///      data-prop="NameOfThePropertyInDatabase"
        ///      data-dev-dynamic-select="true"
        ///      data-url="where the json is"
        ///      data-value="value for the select"
        ///      data-id="id of the select"
        ///      data-dependable-prop-name=""
        ///      data-dependable="true/false"
        ///      data-load-auto="true/false"
        /// </summary>
        "use strict";
        if (additionalSelector === undefined) {
            additionalSelector = "";
        }

        var selector = "div." + $.jsonCombo.dynamicSelectClass + "[" + $.jsonCombo.isDynamicSelectElementAttribute + "=true]" + additionalSelector;
        var $dynamicDiv = $(selector);
        // don't use $.jsonCombo type of caching because it is not updated when items are appended ** alim ul karim
        $.jsonCombo.$allDynamicImmidiaeSelectDivs = $dynamicDiv;
        $.jsonCombo.$spinners = $($.jsonCombo.spineerSelectors);
        $.jsonCombo.$hideableContents = $($.jsonCombo.hidableContentSelector);
        //console.log($.jsonCombo.$spinners);
        //spinner hides first
        if ($.jsonCombo.$spinners.length) {
            $.jsonCombo.$spinners.hide();
        }
        var length = $dynamicDiv.length;


        for (var i = 0; i < length; i++) {
            var $div = $($dynamicDiv[i]);
            var isDependable = $div.attr($.jsonCombo.isDependableAttribute);

            var url = $div.attr(this.urlAttribute);
            if ($.jsonCombo.isEmpty(url) === false && isDependable === 'false') {
                // url exist and needs to be processed
                $.jsonCombo.getJsonProcessSelectDynamicOptions($div, url);
                // dependency will be handled in side the parent when json is reviced in the parent
            }
        }
    },
    isHtmlRequest: function ($div) {
        return $div.attr($.jsonCombo.isHtmlAttribute);
    },
    fixUrlWithSlash: function (url) {
        /// <summary>
        /// if url doesn't contain slash at end
        /// slash will be added
        /// 
        /// </summary>
        /// <param name="url">site.com/ or site.com will return site.com/</param>
        if ($.jsonCombo.isEmpty(url) === false) {
            var len = url.length;
            var lastChar = url[len - 1];
            if (lastChar !== "/") {
                url += "/";
            }
        }
        return url;
    },
    filterDependableDivByPropName: function (depenablePropName) {
        var findChildSelector = "[" + $.jsonCombo.dependablePropertyNameAttribute + "=" + depenablePropName + "]";
        return $.jsonCombo.$allDynamicImmidiaeSelectDivs.filter(findChildSelector);
    },
    filterDivByPropName: function (propName) {
        var findChildSelector = "[" + $.jsonCombo.propertyNameAttribute + "=" + propName + "]";
        return $.jsonCombo.$allDynamicImmidiaeSelectDivs.filter(findChildSelector);
    },
    getUrlFromDynamicSelectDiv: function ($div) {
        var url = $div.attr($.jsonCombo.urlAttribute);
        url = $.jsonCombo.fixUrlWithSlash(url);
        return url;
    },
    selectFirstItemInSelectAndGetValue: function ($currentSelect) {
        var parentValue = $currentSelect.val();
        if (parentValue === null) {
            $currentSelect.val($currentSelect.find("option:first").val());
            parentValue = $currentSelect.val();
        }
        return parentValue;
    },
    getJsonProcessSelectDynamicOptions: function ($div, url) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="$div"></param>
        /// <param name="url">given url to get the json</param>
        "use strict";
        var isHtml = $.jsonCombo.isHtmlRequest($div);
        var requestType = "JSON";
        if (isHtml) {
            requestType = "HTML";
        }

        var value = $div.attr($.jsonCombo.dataValueAttribute);
        var liveSearch = $div.attr($.jsonCombo.liveSearchAttribute);
        var additionCss = $div.attr($.jsonCombo.additionalCssAttribute);
        var propName = $div.attr($.jsonCombo.propertyNameAttribute);
        //container and spinners 
        var selectOfParentDiv = "div.form-row-" + propName +":first";
        //console.log(propName);
        //console.log(selectOfParentDiv);
        var $containerDiv = $(selectOfParentDiv);
        //console.log($containerDiv);
        var $hideableContents = $containerDiv.find($.jsonCombo.hidableContentSelector);
        //console.log($hideableContents);
        var $spinner = null;
        if ($.jsonCombo.$spinners.length > 0) {
            $spinner = $.jsonCombo.getSpinner(propName);
            if ($spinner.length > 0) {
                $spinner.show('slow');
                $hideableContents.hide();
            }
        }
        var elementIdInAttributeFormat = $.jsonCombo.getIdAsAttribute($div); // returns id in attribute format
        var elementId = $.jsonCombo.getDataId($div); // get data-id attribute value
        var isSelectPickerRequired = $div.attr($.jsonCombo.isStyledAttribute);
        var selectPickerClass = isSelectPickerRequired === 'true' ? 'selectpicker' : '';
        var addAttr = "data-style='" + additionCss + "'" +
                      "data-live-search='" + liveSearch + "'" +
                      elementIdInAttributeFormat;
        var selectBoxStart = "<select name='" + propName + "' " + addAttr + " class='" + selectPickerClass + " form-control' >";
        var selectBoxEnd = "</select>";

        $.ajax({
            type: "GET",
            dataType: requestType, //json or html
            url: url,
            success: function (response) {
                var $originalHtmlSelectbox = null;
                if ($spinner.length > 0) {
                    //spinner exist
                    $spinner.hide();
                    $hideableContents.show('slow');
                }
                //console.log(url + " . Data:");
                //console.log(jsonData);
                //
                if (response.length > 0) {
                    //$div.hide();
                    //successfully got  the json
                    var compactSelectHtml = "";
                    if (!isHtml) {
                        //json type
                        var options = new Array(response.length + 5);
                        for (var i = 0; i < response.length; i++) { // build options
                            if ($.jsonCombo.isEmpty(value) === false && (value === response[i].id || response[i].display === value)) {
                                options[i] = ("<option value='" + response[i].id + "' Selected='selected'>" + response[i].display + "</option>");
                            } else {
                                options[i] = ("<option value='" + response[i].id + "'>" + response[i].display + "</option>");
                            }
                        }
                        compactSelectHtml = selectBoxStart + options.join("") + selectBoxEnd;
                        $div.html(compactSelectHtml);
                    } else {
                        // html
                        $div.html(response);
                    }

                    //$div.show("slow");
                    $containerDiv.show('slow');
                    // find any of the dependency if exist
                    if ($.jsonCombo.isEmpty(elementId)) {
                        // id doesn't exist
                        $originalHtmlSelectbox = $div.find("select:first");
                    } else {
                        // id exist
                        $originalHtmlSelectbox = $.byId(elementId);
                        //console.log(elementId);
                        //console.log($parentSelect);
                    }
                    var isItemsExist = $originalHtmlSelectbox.find("option:first").length === 1;
                    //select first item
                    $.jsonCombo.selectFirstItemInSelectAndGetValue($originalHtmlSelectbox);
                    $originalHtmlSelectbox.trigger('change');
                    //console.log(isSelectPickerRequired);
                    //if (isSelectPickerRequired === 'true') {
                    //    // make it styled
                    //$parentSelect.selectpicker();
                    //}
                    var $childDiv = $.jsonCombo.filterDependableDivByPropName(propName);
                    var childUrl = $.jsonCombo.getUrlFromDynamicSelectDiv($childDiv);

                    if ($originalHtmlSelectbox.length === 1 && isItemsExist && $childDiv.length === 1) {
                        //console.log("ase");
                        $originalHtmlSelectbox.change(function () {
                            console.log($originalHtmlSelectbox);
                            var $currentSelect = $(this);
                            var parentValue = $currentSelect.val();
                            var tempUrl = childUrl + parentValue;
                            $childDiv.html("");
                            $.devOrg
                                .dynamicSelect
                                .getJsonProcessSelectDynamicOptions(
                                    $childDiv,
                                    tempUrl);
                        }).trigger('change');
                    }
                } else {
                    // no item retrieved from json 
                    $containerDiv.hide();
                }
            },
            error: function (xhr, status, error) {
                console.log("Error: Can't retrieved the data from given url : " + url + ". Error : " + error);

            }
        });
    }

}
