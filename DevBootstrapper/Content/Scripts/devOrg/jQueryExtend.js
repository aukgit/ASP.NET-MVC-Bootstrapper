;
/**!
 * Written by Alim Ul Karim
 * Email: devorg.bd{at}gmail.com
 * Dated : 15 Jun 2015
 * Version : 0.1
 * Performance : http://jsperf.com/jquery-vs-fasterjquery
 * https://www.facebook.com/DevelopersOrganism
 * mailto:info{at}developers-organism.com
 * @param {anything} variable 
 * @returns a true/false. 
 */
$.isEmpty = function (variable) {
    /// <summary>
    /// Compare any object to null , unidentified or empty then returns true/false.
    /// </summary>
    /// <param name="variable"> Anything can be possible.</param>
    return variable === undefined || variable === null || variable === '' || variable.length === 0;
}