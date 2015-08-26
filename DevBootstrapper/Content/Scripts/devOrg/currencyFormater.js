$.currencyFormater = {
    apply : function ($elements, sign, fractionDigit, bindOnTextChange) {
        /// <summary>
        /// Apply currency formating in a jQuery element.
        /// </summary>
        /// <param name="$elements"></param>
        /// <param name="sign">By default '$'</param>
        /// <param name="fractionDigit">By default 2, for taking 2 digits after decimal.</param>
        /// <param name="bindOnTextChange">True: to bind inputs keypress event to make it formatted when a key is pressed.</param>
        /// <returns type=""></returns>
        if (!sign) {
            sign = "$";
        }
        if (!$.isEmpty($elements)) {
            var formatFunction = $.currencyFormater.getFormat;
            for (var i = 0; i < $elements.length; i++) {
                var $element = $($elements[i]);
                if (bindOnTextChange === true) {
                    // bind
                    $element.on("change", function () {
                        var $this = $(this);
                        var text = $this.val();
                        var attrValue = text.replace(sign, "");
                        $this.attr("data-number-value", attrValue);
                        var number = Number(attrValue);
                        var currencyText = formatFunction(sign, number, fractionDigit);
                        $this.val(currencyText);
                    }).trigger("change");
                } else {
                    var text = $element.val();
                    var attrValue = text.replace(sign, "");
                    $element.attr("data-number-value", attrValue);
                    var number = Number(attrValue);
                    var currencyText = formatFunction(sign, number, fractionDigit);
                    $element.val(currencyText);
                }
            }
           
        }
    },
    getFormat: function (sign, givenNumber, fractionDigit) {
        /// <summary>
        /// Convert a number string to currency format data.
        /// </summary>
        /// <param name="sign">By default '$'</param>
        /// <param name="givenNumber">A number must be given.</param>
        /// <param name="fractionDigit">By default 2, for taking 2 digits after decimal.</param>
        /// <returns type="">Returns currency formatted string value.</returns>
        if (!sign) {
            sign = "$";
        }
        if (!fractionDigit) {
            fractionDigit = 2;
        }
        var getZeros = function(digitPlaces) {
            var finalDigits = "";
            for (var x = 1; x <= digitPlaces; x++) {
                finalDigits += "0";
            }
            return finalDigits;
        }
        var getPaddedZeros = function(fractionPartAsInt) {

            var decimalPart = fractionPartAsInt;
            //console.log(decimalPart);
            for (var k = decimalPart.length; k < fractionDigit; k++) {
                decimalPart += "0";
            }
            return decimalPart;
        };
        var getFraction = function(array) {
            var frac = "0";
            if (array.length >= 2) {
                frac = array[1];
            }
            frac = frac.substring(0, fractionDigit);
            return getPaddedZeros(frac);
        }

        if (typeof givenNumber === "number") {
            givenNumber = givenNumber.toString();
        } else {
            throw new "given input number is not a valid number.";
        }
        if (givenNumber) {
            var numArr = givenNumber.split(".");
            //console.log(numArr);
            var number = numArr[0];
            var fraction = getFraction(numArr);
            var combinedNumber = number + "." + fraction;
            //console.log(number);
            //console.log(fraction);
            return sign + combinedNumber;
        } else {
            return sign + "0" + "." + getZeros(fractionDigit);
        }

    }
};