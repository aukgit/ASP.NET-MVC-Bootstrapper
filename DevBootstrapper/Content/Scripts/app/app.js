/**!
 * Written by Alim Ul Karim
 * Email: devorg.bd{at}gmail.com
 * Dated : 10 Aug 2015
 * Version : 2.6
 * https://www.facebook.com/DevelopersOrganism
 * mailto:info{at}developers-organism.com
 */

$.app = {
    isDebugging: true,

    $hiddenContainer: null,
    $hiddenFieldDictionary: [],
    hiddenFieldNamesDictionary: [],

    initHiddenContainer: function () {
        /// <summary>
        /// Initialize hidden container if exist.
        /// </summary>
        /// <returns type="">returns hidden container.</returns>
        var app = $.app,
            $container = app.$hiddenContainer;

        if ($container) {
            return $container;
        } else {
            app.$hiddenContainer = $.byId("hidden-fields-container");
            $container = app.$hiddenContainer;
        }
        return $container;
    },
    isHiddenContainerExist: function () {
        return !$.isEmpty($.app.$hiddenContainer);
    },
    _getHiddenFieldDictionary: function (nameOfHiddenField) {
        /// <summary>
        /// Get dictionary hidden field values.
        /// </summary>
        /// <param name="nameOfHiddenField"></param>
        /// <returns type="return $ type object.">null or jquery obejct.</returns>

        if (nameOfHiddenField) {
            var namesDictionary = $.app.hiddenFieldNamesDictionary;
            for (var i = 0; i < namesDictionary.length; i++) {
                var hiddenName = namesDictionary[i];
                if (hiddenName === nameOfHiddenField) {
                    return $.app.$hiddenFieldDictionary[i];
                }
            }
        }
        return null;
    },
    _addHiddenFieldToDictionary: function ($field) {
        /// <summary>
        /// Only adds the item to the dictionary ($hiddenFieldDictionary, hiddenFieldNamesDictionary)
        /// </summary>
        /// <param name="$field">jQuery object.</param>
        /// <returns type=""></returns>
        var app = $.app;

        app.$hiddenFieldDictionary.push($field);
        app.hiddenFieldNamesDictionary.push($field.attr("name"));
    },
    getHiddenField: function (nameOfHiddenField) {
        /// <summary>
        /// Get the hidden field value, if possible get it from dictionary object.
        /// </summary>
        /// <param name="nameOfHiddenField"></param>
        /// <returns type="return $ type object.">get attribute values $returnedObject.attr() or null</returns>
        var app = $.app;
        if (app.isHiddenContainerExist()) {
            var $container = app.$hiddenContainer,
                $field = app._getHiddenFieldDictionary(nameOfHiddenField);
            if ($field) {
                // not null
                return $field;
            } else {
                // is null the get id from DOM
                $field = $.byId(nameOfHiddenField);
                if ($field.length === 0) {
                    $field = $container.find("[name='" + nameOfHiddenField + "']");
                }
                app._addHiddenFieldToDictionary($field);
                return $field;
            }
        }
        return null;
    },
    setHiddenValue: function (nameOfHiddenField, val) {
        /// <summary>
        /// Get the hidden field value, if possible get it from dictionary object.
        /// </summary>
        /// <param name="nameOfHiddenField"></param>
        /// <returns type="return $ type object.">get attribute values $returnedObject.attr() or null</returns>
        var app = $.app;
        if (app.isHiddenContainerExist()) {
            var $field = app.getHiddenField(nameOfHiddenField);
            if ($field.length > 0) {
                $field.val(val);
                return $field;
            }
        }
        return null;
    }

}