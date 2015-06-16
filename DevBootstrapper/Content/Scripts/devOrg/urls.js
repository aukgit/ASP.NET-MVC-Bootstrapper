/// <reference path="../jQuery/jquery-2.1.4.js" />
/// <reference path="../jQuery/jquery-2.1.4-vsdoc.js" />
/// <reference path="urls.js" />
/// <reference path="byId.js" />
/// <reference path="constants.js" />
/// <reference path="country-phone.js" />
/// <reference path="devOrg.js" />
/// <reference path="initialize.js" />
/// <reference path="jQueryExtend.js" />
/// <reference path="jsonCombo.js" />
/// <reference path="regularExp.js" />
/// <reference path="selectors.js" />
/// <reference path="upload.js" />


;

$.devOrg = $.devOrg || {};

$.devOrg.urls = {
    /*
     * hostUrl will be retrieved from hidden field "#host-url"
     * Contains a slash at the end.
     */
    hostUrl: null,

    validator: "Validator/",
    usernameValidation: "Username",
    emailValidation: "Email",
    timeZoneJson: "Services/GetTimeZone", // look like this /Partials/GetTimeZone/CountryID
    languageJson: "Services/GetLanguage", // look like this /Partials/GetTimeZone/CountryID
    getHostUrl: function () {
        /// <summary>
        /// Retrieve host url from host-url id hidden field
        /// Return host url with a slash at the bottom.
        /// </summary>
        /// <returns type="">Returns the host url.</returns>
        var self = $.devOrg.urls;
        var hostUrl = self.hostUrl;

        if ($.isEmpty(hostUrl)) {
            var dev = $.devOrg,
                selectors = dev.selectors;
            var id = selectors.hostFieldId;
            var $hostUrlHidden = $.byId(id);
            if ($hostUrlHidden.length > 0) {
                var url = $hostUrlHidden.val();
                self.hostUrl = $.returnUrlWithSlash(url);
            }
        }
        return self.hostUrl;
    },

    getAbsUrl: function (givenUrl) {
        /// <summary>
        /// Given url shouldn't have any slash at the begining.
        /// </summary>
        /// <param name="givenUrl">url shouldn't have any slash at the begining.</param>
        /// <returns type="">Return absolute url containing host name and url.</returns>
        var self = $.devOrg.urls;
        var hostUrl = self.hostUrl;
        if (!$.isEmpty(hostUrl)) {
            return hostUrl + givenUrl;
        }
        hostUrl = self.getHostUrl();
        return hostUrl + givenUrl;
    },
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD

    getAbsValidatorUrl: function (url) {
        /// <summary>
        /// Returns absolute url of a validation
        /// </summary>
        /// <param name="url"></param>
        /// <returns type="string">returns absolute url.</returns>
        var self = $.devOrg.urls;

        var urlCombined = self.validator + url;
        return self.getAbsUrl(urlCombined);

    }
=======
>>>>>>> origin/Development

    getAbsValidatorUrl: function (url) {
        /// <summary>
        /// Returns absolute url of a validation
        /// </summary>
        /// <param name="url"></param>
        /// <returns type="string">returns absolute url.</returns>
        var self = $.devOrg.urls;

<<<<<<< HEAD

=======
        var urlCombined = self.validator + url;
        return self.getAbsUrl(urlCombined);
    }
>>>>>>> origin/Development
=======
=======
>>>>>>> origin/Development

    getAbsValidatorUrl: function (url) {
        /// <summary>
        /// Returns absolute url of a validation
        /// </summary>
        /// <param name="url"></param>
        /// <returns type="string">returns absolute url.</returns>
        var self = $.devOrg.urls;

        var urlCombined = self.validator + url;
        return self.getAbsUrl(urlCombined);
    }
<<<<<<< HEAD
>>>>>>> origin/Development
=======
>>>>>>> origin/Development
};