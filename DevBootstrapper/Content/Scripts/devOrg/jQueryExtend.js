;
/**!
 * Written by Alim Ul Karim
 * Email: devorg.bd{at}gmail.com
 * Dated : 15 Jun 2015
 * Version : 0.1
 * https://www.facebook.com/DevelopersOrganism
 * mailto:info{at}developers-organism.com
 */
$.isEmpty = function(variable) {
    /// <summary>
    /// Compare any object to null , unidentified or empty then returns true/false.
    /// </summary>
    /// <param name="variable"> Anything can be possible.</param>
    /// <returns type="boolean">True/False</returns>
    return variable === undefined || variable === null || variable === '' || variable.length === 0;
};
/**
 *  @returns array of classes names.
 */
$.getClassesList = function ($jQueryObject) {
    /// <summary>
    /// jQuery element get all classes as an array.
    /// </summary>
    /// <param name="$jQueryObject">Any jQuery object.</param>
    /// <returns type="array">array list of classes.</returns>
    if ($jQueryObject.length === 1) {
        return $jQueryObject.attr("class").split(/\s+/);
    }
    return null;
};

$.getArrayExcept = function (givenArray, excludingArray) {
    /// <summary>
    /// givenArray = ['a','b','c'] , excludingArray=['b','c'], results=['a']
    /// </summary>
    /// <param name="givenArray" type="array">Full list of items (in array format).</param>
    /// <param name="excludingArray" type="array">List of items which needs to be excluded from the list (in array format).</param>
    /// <returns type="array">an array after excluding the items from the given list.</returns>
    "use strict";
    if ($.isEmpty(givenArray)) {
        return [];
    }
    if ($.isEmpty(excludingArray)) {
        return givenArray;
    }

    var len = givenArray.length;
    var results = [];
    for (var i = 0; i < len; i++) {
        if (excludingArray.indexOf(givenArray[i]) === -1) {
            // not found
            results.push(givenArray[i]);
        }
    }
    return results;
}


//validation modification
$.checkValidInputs = function ($inputsCollection, starRatingLabel, invalidStarRatingCss) {
    /// <summary>
    /// Check all the inputs jQuery validations.
    /// Also mark to red when invalid by the default valid method. 
    /// Bootstrap star rating is also validated in custom way.
    /// </summary>
    /// <param name="$inputsCollection" type="jQuery element">All input collection. </param>
    /// <param name="starRatingLabel">Can be null or full html for the label to be injected when star rating is not selected or rated.</param>
    /// <param name="invalidStarRatingCss" type="json with css properties">When null: {'text-shadow': "2px 2px red"}</param>
    /// <returns type="boolean">true/false</returns>
    "use strict";

    var $currentInput = null;
    var length = $inputsCollection.length;
    var labelHtml = starRatingLabel;
    if ($.isEmpty(labelHtml)) {
        labelHtml = "<label class='label label-danger small-font-size'>Please rate first.</label>";
    }

    if ($.isEmpty(invalidStarRatingCss)) {
        invalidStarRatingCss = {
            'text-shadow': "2px 2px red"
        };
    }
    if (length > 0) {
        for (var i = 0; i < length; i++) {
            $currentInput = $($inputsCollection[i]);

            if ($currentInput.hasClass("common-rating")) {
                var $ratingContainer = $currentInput.closest("div.rating-container");
                var $wholeContainer = $ratingContainer.closest("div.star-rating");

                if ($currentInput.val() === "0") {
                    $ratingContainer.css(invalidStarRatingCss);
                    if (!$wholeContainer.attr("data-warned")) {
                        $wholeContainer.append(labelHtml);
                        $wholeContainer.attr("data-warned", "true");
                    }
                    return false;
                } else {
                    // when star rating is valid then 
                    // remove the injected label and make it normal
                    $ratingContainer.css({
                        'text-shadow': "none"
                    });

                    if ($wholeContainer.attr("data-warned")) {
                        // removing injected label.
                        $wholeContainer.find("label").remove();
                        $wholeContainer.attr("data-warned", "false");
                    }
                }
            }
            if (!$currentInput.valid()) {
                return false;
            }
        }
    }
    return true;
}


$.fn.extend({
    getClassesList: function () {
        /// <summary>
        /// jQuery element get all classes as an array.
        /// </summary>
        /// <returns type="array">array list of classes.</returns>
        return $.getClassesList(this);
    },
    isEmpty : function() {
        /// <summary>
        /// Compare any object to null , unidentified or empty then returns true/false.
        /// </summary>
        /// <param name="variable"> Anything can be possible.</param>
        /// <returns type="boolean">True/False</returns>
        return $.isEmpty(this);
    }
});