; $.app = $.app || {};
$.app.initialize = function () {
    /// <summary>
    /// Run all modules.
    /// </summary>
    
    var app = $.app;
    app.initHiddenContainer();
    // run controller module
    app.controllers.initialize(); // runs all controllers modules.
};
