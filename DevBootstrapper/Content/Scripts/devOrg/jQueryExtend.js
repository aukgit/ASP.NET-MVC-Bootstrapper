;
/**!
 * Written by Alim Ul Karim
 * Email: devorg.bd{at}gmail.com
 * Dated : 15 Jun 2015
 * Version : 0.1
 * Performance : http://jsperf.com/jquery-vs-fasterjquery
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