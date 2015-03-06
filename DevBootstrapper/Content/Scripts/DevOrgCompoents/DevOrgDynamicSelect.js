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

$.devOrg.dynamicSelect = {
    isDependableAttribute: "data-dependable",
    dependablePropertyNameAttribute: "data-dependable-prop-name",
    propertyNameAttribute: "data-prop",
    dynamicSelectClass: "dynamic-select-load",
    urlAttribute: "data-url",
    dataValueAttribute: "data-value",
    isDynamicSelectElementAttribute: "data-dev-dynamic-select",
    additionalCssAttribute: "data-additional-css",
    liveSearchAttribute: "data-live-search",
    $dynamicSelectContainerDiv: $("div.dynamic-select-container[data-dynamic-select-container=true]"),
    //$allDynamicImmidiaeSelectDivs: null, // will be defined from initialize function
    $dependancySelectsHasNotProcessed: [], //only populated if a dependency combo can't find parent.
    initialize: function (additionalSelector) {
        /// <summary>
        /// select div and push info based on properties
        /// class="dynamic-select-load"
        ///      data-prop="NameOfThePropertyInDatabase"
        ///      data-dev-dynamic-select="true"
        ///      data-url="where the json is"
        ///      data-value="value for the select"
        ///      data-dependable-prop-name=""
        ///      data-dependable="true/false"
        ///      data-load-auto="true/false">
        /// </summary>
        "use strict";
        if (additionalSelector === undefined) {
            additionalSelector = "";
        }

        var selector = "div." + this.dynamicSelectClass + "[" + this.isDynamicSelectElementAttribute + "=true]" + additionalSelector;
        var $dynamicDiv = $(selector);
        // don't use this type of caching because it is not updated when items are appened ** alim ul karim
        //this.$allDynamicImmidiaeSelectDivs = $dynamicDiv; 
        var length = $dynamicDiv.length;



        for (var i = 0; i < length; i++) {
            var $div = $($dynamicDiv[i]);
            var url = $div.attr("data-url");
            if (!_.isEmpty(url)) {
                this.processJsonDynamicSelect($div);
            }
        }
        //length = this.$dependancySelectsHasNotProcessed.length;
        //for (i = 0; i < length; i++) {
        //    $div = this.$dependancySelectsHasNotProcessed[i];
        //    this.processJsonDynamicSelect($div);
        //    length = this.$dependancySelectsHasNotProcessed.length;
        //}

    },

    processJsonDynamicSelect: function ($div) {
        "use strict";


        var isDependable = $div.attr(this.isDependableAttribute);
        var dependablePropName = $div.attr(this.dependablePropertyNameAttribute);

        var url = $div.attr(this.urlAttribute);
        var value = $div.attr(this.dataValueAttribute);
        var liveSearch = $div.attr(this.liveSearchAttribute);
        var additionCss = $div.attr(this.additionalCssAttribute);
        var propName = $div.attr(this.propertyNameAttribute);
        var addAttr = "data-style='" + additionCss + "'" +
                      "data-live-search='" + liveSearch + "'";
        var selectBoxStart = "<select name='" + propName + "' " + addAttr + " class='selectpicker form-control' >";
        var selectBoxEnd = "</select>";

        if (isDependable === "false") {
            // no dependency yet.
            this.getJsonProcessSelectDynamicOptions(url, $div, selectBoxStart, selectBoxEnd, value);

        } else {
            // dependency
            url = this.fixUrlWithSlash(url);
            var selectOfParentDiv = "div." + this.dynamicSelectClass + "[" + this.propertyNameAttribute + "=" + dependablePropName + "]";
            var $parentSelectDiv = $(selectOfParentDiv);
            var $parentSelect = $parentSelectDiv.find("select");
            if ($parentSelect.length > 0) {
                $parentSelect.change(function () {
                    var $currentSelect = $(this);
                    var parentValue = $currentSelect.val();
                    if (parentValue === null) {
                        $currentSelect.val($currentSelect.find("option:first").val());
                        parentValue = $currentSelect.val();
                    }
                    var tempUrl = url + parentValue;
                    $div.html("");
                    $.devOrg.dynamicSelect.getJsonProcessSelectDynamicOptions(tempUrl, $div, selectBoxStart, selectBoxEnd);
                });
            } else {
                this.$dependancySelectsHasNotProcessed.push($div);
            }
        }
    },
    //parentSelectStateChangeFunction: function() {

    //},
    fixUrlWithSlash: function (url) {
        "use strict";
        /// <summary>
        /// if url doesn't contain slash at end
        /// slash will be added
        /// 
        /// </summary>
        /// <param name="url">site.com/ or site.com will return site.com/</param>
        var len = url.length;
        var lastChar = url[len - 1];
        if (lastChar !== "/") {
            url += "/";
        }
        return url;
    },

    getJsonProcessSelectDynamicOptions: function (url, $div, selectStart, selectEnd, value) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="$div"></param>
        /// <param name="selectStart"></param>
        /// <param name="selectEnd"></param>
        /// <param name="value"></param>
        "use strict";
        var propName = $div.attr($.devOrg.dynamicSelect.propertyNameAttribute);

        $.getJSON(url).then(function (jsonData) {
            if (jsonData.length > 0) {
                $div.hide();
                //successfully got  the json
                var options = "";
                for (var i = 0; i < jsonData.length; i++) { // build options
                    if (!_.isEmpty(value) && (value === jsonData[i].id || jsonData[i].display === value)) {
                        options += ("<option value='" + jsonData[i].id + "' Selected='selected'>" + jsonData[i].display + "</option>");
                    } else {
                        options += ("<option value='" + jsonData[i].id + "'>" + jsonData[i].display + "</option>");
                    }
                }

                var compactSelectHtml = selectStart + options + selectEnd;
                $div.html(compactSelectHtml);

                $div.show("slow");
            } else {
                // no data found
                // hide the container
                var selectOfParentDiv = "div.form-row-" + propName + "";
                $.devOrg.dynamicSelect.$dynamicSelectContainerDiv.filter(selectOfParentDiv).hide();
            }
        },
        function (jqXHR, textStatus, err) {
            console.log("Can't retrieved the data from given url : " + url + ". " + textStatus);
        });
    }

}