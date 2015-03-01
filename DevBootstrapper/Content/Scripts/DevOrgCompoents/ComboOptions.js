/*
 * Written by Alim Ul Karim
 * Developers Organism
 * https://www.facebook.com/DevelopersOrganism
 * mailto:info@developers-organism.com
*/
function FillComboDepenedOnOther(dependingSelectBoxId, fillSelectBoxId, locationToRetrive, optionalSelectedIdorName) {
    $(dependingSelectBoxId).bind("change", function() {
        // every time the value of the dropdown 1 is changed, do this:
        // console.log('ase' + dependingSelectBoxID);
        var paramId = $(dependingSelectBoxId).val();

        $.getJSON(locationToRetrive + "/" + paramId, function(data) {
            $(fillSelectBoxId).empty(); // clear old values if exist

            var options = "";
            for (i = 0; i < data.length; i++) { // build options
                if ((optionalSelectedIdorName == data[i].id || data[i].text == optionalSelectedIdorName) && (optionalSelectedIdorName != null || optionalSelectedIdorName != "")) {
                    options += ("<option value='" + data[i].id + "' Selected='selected'>" + data[i].text + "</option>");
                } else {
                    options += ("<option value='" + data[i].id + "'>" + data[i].text + "</option>");
                }
            }
            $(fillSelectBoxId).append(options);
        });
    });
}


function FillComboAjaxBased(selectBoxId, locationToRetrive, optionalSelectedIdorName) {
    $.getJSON(locationToRetrive + "/", function(data) {
        $(selectBoxId).empty(); // clear old values if exist
        var options = "";
        for (i = 0; i < data.length; i++) { // build options
            if ((optionalSelectedIdorName == data[i].id || data[i].text == optionalSelectedIdorName) && (optionalSelectedIdorName != null || optionalSelectedIdorName != "")) {
                options += ("<option value='" + data[i].id + "' Selected='selected'>" + data[i].text + "</option>");
            } else {
                options += ("<option value='" + data[i].id + "'>" + data[i].text + "</option>");
            }
        }
        $(selectBoxId).append(options);
    });
}


/*
Check me (checked)
.attr('checked'): checked
.prop('checked'): true
.is(':checked'): true

Check me (not checked)
.attr('checked'): undefined
.prop('checked'): false
.is(':checked'): false

<button id="theBtn" value="taDa" disabled="disabled">Hi There</button>
We can use 
Copy code
$('#theBtn').attr('disabled')    // to return the initial value of this attribute as a string "disabled"
// and
$('#theBtn').prop('disabled')    // to return the current state of this property (true or false)
$('#theBtn').prop('disabled',true)    // to change the property - using a correct data-type
*/
function AddPropertyValue(selectBoxId, locationToRetrive, propertyName) {
    $.getJSON(locationToRetrive + "/" + selectBoxId.value, function(data) {
        $(selectBoxId).prop(propertyName, data);
    });
}

function ShowInformationBaseOnComboInAnotherBoxBind(selectBoxId, locationToRetrive, boxId) {
    $(selectBoxId).change(function() {
        $.getJSON(locationToRetrive + "/" + this.value, function(data) {
            $(boxId).text(data);
        });
    }).triggerHandler("change");
}

function ShowInformationBaseOnComboInAnotherBox(selectBoxId, locationToRetrive, boxId) {
    $.getJSON(locationToRetrive + "/" + selectBoxId.value, function(data) {
        $(boxId).text(data);
    }).triggerHandler("change");

}

///OptionalInSameLayer means in the same block displayed.
function FillHtmlBasedOnDependableCombo(dependableCombo, locationToRetriveJsonAsValue, defComboValueAtFirst, boxToPutData, addClassToElements, addBeginingHtml, addClosingTagHtml, optionalInSameLayer, optionalItemsSeperator, noRecordsFoundMsg, htmlRowAttr) {
    $(dependableCombo).change(function() {
        var valueToLoad = -1;
        if (this.value == null || this.value == undefined || this.value == "" || this.value == " ") {
            valueToLoad = defComboValueAtFirst;
        } else {
            valueToLoad = this.value;
        }
        //console.log(valueToLoad);

        $.getJSON(locationToRetriveJsonAsValue + "/" + valueToLoad, function(data) {
            $(boxToPutData).empty(); // clear old values if exist
            var sameLayer = false;

            if (optionalInSameLayer != null && optionalInSameLayer != undefined && optionalInSameLayer == true) {
                sameLayer = true;
            }

            if (data == null || data == undefined || data.length == null) {
                if (htmlRowAttr != null && htmlRowAttr != undefined) {
                    generated += "<" + htmlRowAttr + ">";
                }
                if (noRecordsFoundMsg == null || noRecordsFoundMsg == undefined) {
                    noRecordsFoundMsg = "Sorry , records can't be display because of network problem or no records found.";
                }
                generated += addBeginingHtml;
                generated += "<span class = '" + addClassToElements + " bold red' >" + noRecordsFoundMsg + "</span>";
                generated += addClosingTagHtml;
                if (htmlRowAttr != null && htmlRowAttr != undefined) {
                    generated += "</" + htmlRowAttr + ">";
                }
                $(boxToPutData).html(generated);
                return;
            }

            if (addClassToElements == null || addClassToElements == undefined) {
                addClassToElements = "Dependable-Generation";
            }
            var generated = "";
            for (i = 0; i < data.length; i++) { // build options
                if (htmlRowAttr != null && htmlRowAttr != undefined) {
                    generated += "<" + htmlRowAttr + ">";
                }
                if (addBeginingHtml != null && addBeginingHtml != undefined && addClosingTagHtml != null && addClosingTagHtml != undefined) {
                    generated += addBeginingHtml;
                    generated += "<span class = '" + addClassToElements + "' >" + data[i].textue + "</span>";
                    if (sameLayer) {
                        generated += optionalItemsSeperator;
                        generated += "<span class = '" + addClassToElements + "-Optional Optional-Case' >" + data[i].optional + "</span>";
                    }
                    generated += addClosingTagHtml;

                    if ((data[i].optional != null && data[i].optional != undefined) && sameLayer == false) {
                        if (optionalItemsSeperator != null && optionalItemsSeperator != undefined) {
                            generated += addBeginingHtml;
                            generated += optionalItemsSeperator;
                            generated += addClosingTagHtml;
                        }
                        generated += addBeginingHtml;
                        generated += "<span class = '" + addClassToElements + "-Optional' >" + data[i].optional + "</span>";
                        generated += addClosingTagHtml;
                    }
                } else {
                    generated += "<span class = '" + addClassToElements + "' >" + data[i].textue + "</span>";
                    if (sameLayer) {
                        generated += optionalItemsSeperator;
                        generated += "<span class = '" + addClassToElements + "-Optional Optional-Case' >" + data[i].optional + "</span>";
                    }
                    if ((data[i].optional != null && data[i].optional != undefined) && sameLayer == false) {
                        if (optionalItemsSeperator != null && optionalItemsSeperator != undefined) {
                            generated += addBeginingHtml;
                            generated += optionalItemsSeperator;
                            generated += addClosingTagHtml;
                        }
                        generated += "<span class = '" + addClassToElements + "-Optional' >" + data[i].optional + "</span>";
                    }
                }

                if (htmlRowAttr != null && htmlRowAttr != undefined) {
                    generated += "</" + htmlRowAttr + ">";
                }
            }
            $(boxToPutData).html(generated);

        });
    }).triggerHandler("change");
}


//example
function LoadCountryAndDistrict(defCountry, defDistrict) {
    var countryId = "#CountryID";
    var districtId = "#CountryDivisionID";
    var getCountries = "/Country/GetCountries/0";
    var getDistrics = "/Country/GetDistricts";

    if ($(countryId).length > 0) {
        FillComboAjaxBased(countryId, getCountries, defCountry);
    }

    if ($(districtId).length > 0) {
        //get the default country districts
        FillComboAjaxBased(districtId, getDistrics + "/" + defCountry, defDistrict);
        //bind it to the change event.
        FillComboDepenedOnOther(countryId, districtId, getDistrics, defDistrict);
    }
}