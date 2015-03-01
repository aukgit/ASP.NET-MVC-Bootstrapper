/*
 * Written by Alim Ul Karim
 * Developers Organism
 * https://www.facebook.com/DevelopersOrganism
 * mailto:info@developers-organism.com
 * Dated: 01 Mar 2015
*/

$(function () {
    "use strict";
    var commonTasksToRun = {
        transactionStatusHide: function transactionStatusHide() {
            var $transactionStatus = $(".transaction-status");
            if ($transactionStatus.length > 0) {
                $transactionStatus.delay(3500).fadeOut(2500);
            }
        },
        implementTooltips: function () {
            $(".tooltip-show").tooltip();

        },

        runEveryTask: function () {

            commonTasksToRun.transactionStatusHide();
            commonTasksToRun.implementTooltips();
        }


    }
    //var devBackBtns = $("a.dev-btn-back");
    //if (devBackBtns.length > 0) {
    //    devBackBtns.click(function (e) {
    //        e.preventDefault();
    //        history.back();
    //    });
    //} 
    commonTasksToRun.runEveryTask();


});