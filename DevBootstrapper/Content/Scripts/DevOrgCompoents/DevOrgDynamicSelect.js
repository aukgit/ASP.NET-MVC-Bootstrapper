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
    dependablePropertyNameAttribute:"data-dependable-prop-name",
    propertyNameAttribute: "data-prop",
    dynamicSelectClass: "dynamic-select-load",
    urlAttribute: "data-url",
    dataValueAttribute: "data-value",
    isDynamicSelectElementAttribute: "data-dev-dynamic-select",

    dynamicSelectLoad: function() {
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
        var selector = "div." + this.dynamicSelectClass + "[" + this.isDynamicSelectElementAttribute + "=true]";
        var $dynamicDiv = $(selector);

        var length = $dynamicDiv.length;
        for (var i = 0; i < length; i++) {
            var $div = $($dynamicDiv[i]);
            var url = $div.attr("data-url");

            if (!_.isEmpty(url)) {
                processJsonDynamicSelect($div);
            }
        }
    },

    processJsonDynamicSelect: function ($div) {
        "use strict";
        var isDependable = $div.attr(this.isDependableAttribute);
        var url = $div.attr(this.urlAttribute);
        if (isDependable === 'false') {
            // no dependency yet.
            $.getJSON()
        }
    }
}