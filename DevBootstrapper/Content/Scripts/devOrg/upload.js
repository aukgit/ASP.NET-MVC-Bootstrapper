/// <reference path="../jQuery/jquery-2.1.3.intellisense.js" />
/// <reference path="../jQuery/jquery-2.1.3.js" />

/// <reference path="byId.js" />
/// <reference path="constants.js" />
/// <reference path="devOrg.js" />
/// <reference path="initialize.js" />
/// <reference path="urls.js" />
/// <reference path="selectors.js" />
; $.devOrg = $.devOrg || {};

$.devOrg.upload = {

    uploadedFilesNotificationLabelSelector: "label.file-uploaded-notify-label-",
    editBtnSpinnerSelector: "[data-edit-btn-spinner=true]",
    progressorSpinnerSelector: "[data-progressor-spinner=true]",
    wholeProgressorSelector: ".uploader-progress-info", // old: [data-progressor-div=true].uploader-progress-info

    preUploadedFilesMessage: "files exist.",
    uploadedFilesMessage: "uploaded successfully.",

    failedEvent: "fileuploadfail",
    progressEvent: "fileuploadprogressall",
    doneEvent: "fileuploaddone",
    submitEvent: "fileuploadsubmit",
    fileUploadAddedEvent: "fileuploadadd",

    filesAreAddedForUploadEvent: "fileuploadadd",
    filesAlwaysProcessEvent: "fileuploadprocessalways",

    uploaderInputClass: ".dev-fileupload",

    maxUploadParameter: "data-max-upload-count",
    uploadNumberParameter: "data-uploaded-count",
    uploadPreexistCountParameter: "data-upload-pre-exist-count",
    hasEditButtonParameter: "data-has-edit-btn",

    //Fix those nulls at initialize (done)
    WholeUploadingContainer: null, // which will contain all the uploading elements.
    $form : null,
    formData: null,//$("form input[type='file']").closest("form").serializeArray()
    $uploaderWorkingDiv : null ,//$("form div.uploader")
    $allFileInputTypes: null,//$("form div.uploader>span.fileinput-button>input[type='file']")
    $allSpinners: null, // $("form div.uploader a[data-spinner=spinner].spinner")
    $allWholeProgressor: null, // $("form div.uploader div[data-progressor-div=true].uploader-progress-info")
    $allProgressorValueIdicator: null, // $("form div.uploader div[data-progressor-div=true].uploader-progress-info>a[data-progressor-value=true]")
    $allLabelsToIndicateUploadedFilesNumber: null, // $("form div.uploader>label[data-label-file-uploaded=true]")
    $allEditButtons: null,//$("form div.uploader>a[data-btn=edit].edit-btn")

    $allSuccessIcons: null, // $("form div.uploader>a[data-success-icon=true]")
    $allFailedIcons: null, // $("form div.uploader>a[data-failed-icon=true]")

    $allErrorsRelatedTags: null, //$("form div.uploader>[data-error-related=true]")
    $allULErrorsRelatedTags: null, // $("form div.uploader>ul[data-error-related=true]")


    isInitialized: false, // when fields are initialized it is going to be true.

    initializeFields: function ($container, formSelector) {
        /// <summary>
        /// Going to initialized all the null fields to it's necessary jQuery object.
        /// Only run if not initialized and container is an object.
        /// </summary>
        /// <param name="$container">Uploader containing container.</param>
        /// <param name="formSelector"></param>
        var self = $.devOrg.upload;
        var $form = null,
            $uploaderDivs;
        if (self.isInitialized === false && $container.length > 0) {
            if (!_.isEmpty(formSelector)) {
                $form = $(formSelector);
            } else {
                $form = $container.closest("form");
            }
            self.formData = $form.serializeArray;
            self.$uploaderWorkingDiv = $container.find("div.uploader");
            $uploaderDivs = self.$uploaderWorkingDiv;

            self.$allFileInputTypes = $uploaderDivs.find("input[type='file']");
            self.$allSpinners = $uploaderDivs.find("a.spinner");
            self.$allWholeProgressor = $uploaderDivs.find(self.wholeProgressorSelector);
            self.$allProgressorValueIdicator = self.$allWholeProgressor.find("a[data-progressor-value=true]");
            self.$allLabelsToIndicateUploadedFilesNumber = $uploaderDivs.find("label[data-label-file-uploaded=true]");
            self.$allEditButtons = $uploaderDivs.find("a.edit-btn");

            self.$allSuccessIcons = $uploaderDivs.find("a[data-success-icon=true]");
            self.$allFailedIcons = $uploaderDivs.find("a[data-failed-icon=true]");

            self.$allErrorsRelatedTags = $uploaderDivs.find("[data-error-related=true]");
            self.$allULErrorsRelatedTags = $uploaderDivs.find("ul[data-error-related=true]");

            self.$form = $form;

            self.isInitialized = true;
        }
    },

    initializeHide: function () {
        // only hide edit spinner
        var self = $.devOrg.upload;
        self.$allSpinners.filter(".edit-btn-spinner").hide();
        self.$allWholeProgressor.hide();
        //$.devOrg.upload.$allProgressorValueIdicator.hide();
        self.$allEditButtons.hide();
        self.$allSuccessIcons.hide();
        self.$allFailedIcons.hide();
        self.$allErrorsRelatedTags.hide();
    },
    

    initialize: function ($container, formSelector, acceptedFileSizeInMb, acceptFileTypeRegularExpressionString) {
        /// <summary>
        /// Initialize the upload plugin
        /// </summary>
        /// <param name="container">(only initialized if exist)Which will whole container for all the uploading plugins. Developer can have more than one container in one page if necessary or else put all in one container.</param>
        /// <param name="formSelector">Form selector to find and cache, null can be a choice. If null then it will pull the closet form from the container</param>
        /// <param name="acceptedFileSizeInMb"></param>
        /// <param name="acceptFileTypeRegularExpressionString"></param>
        //var urlExist = false;
        //var kRep = 0;
        var self = $.devOrg.upload;
     
        if (_.isEmpty($container)) {
            // if nothing exist on contain then don't execute anything.
            return;
        } else {
            // initialize all the null fields.
            self.initializeFields($container, formSelector);
        }

        var uploadersLength = 0,
            actualSize = acceptedFileSizeInMb * 1024000,
            id = 0,
            $label = null,
            $editBtn = null,
            $singleUploader = null,
            $uploaderDiv = self.$uploaderWorkingDiv;



        if ($uploaderDiv.length === 0) {
            return;
        }
        var $uploaders = self.$allFileInputTypes.filter($.devOrg.upload.uploaderInputClass);

        uploadersLength = $uploaders.length;
        if (uploadersLength > 0) {

            // hide all necessary objects
            self.initializeHide();

            //$.devOrg.upload.uploaderFixingDataUrlOnInvalidUrls();

            //$.devOrg.upload.editBtnClickEvntBindingNHide();

            for (var i = 0; i < uploadersLength; i++) {
                $singleUploader = $($uploaders[i]);


                /// fix urls if not exist. put from form.
                self.uploaderFixingDataUrlOnInvalidUrls($singleUploader);

                id = $singleUploader.attr("data-id");

                // edit button and preload, show edit button if edit btn is true
                $label = self.getLabelToIndicateUploadedFiles(id);
                self.showEditButtonBasedOnPreUploadNShowMessageOnLabel($label, id);


                // binding with edit button clicked.
                $editBtn = self.getEditButton(id);
                self.uploaderEditBtnClickEvntBinder($editBtn, $label, $singleUploader, id);


            }


            $uploaders.fileupload({
                url: $(this).attr("data-url"),
                dataType: 'json',
                autoUpload: $(this).attr("data-is-auto"),
                //singleFileUploads: true,
                acceptFileTypes: new RegExp(acceptFileTypeRegularExpressionString, 'i'),
                FileSize: actualSize, // in MB
                progressall: function (e, data) {
                    // when in progress

                    //written here so that doesn't have to go back and fort.
                    var $this = $(this);
                    var idAttr = $this.attr("data-id");
                    var progress = parseInt(data.loaded / data.total * 100, 10);

                    self.setProgressorValue(idAttr, progress);
                }
            })
            .on(self.fileUploadAddedEvent, function (e, data) {
                // file upload added event.
                self.onFileUploadAddedEvent(e, data, $(this));
            })
            // when on submit
            .on(self.submitEvent, function (e, data) {
                self.onSubmitEvent(e, data, $(this));
            })
            //.on($.devOrg.upload.progressEvent, function (e, data) {                  

            //    //console.log(progress);
            //})
            // when done
            .on(self.doneEvent, function (e, data) {
                self.onUploadDoneEvent(e, data, $(this));
            })
            // when failed
            .on(self.failedEvent, function (e, data) {
                self.onUploadFailedErrorEvent(e, data, $(this));
            }).prop('disabled', !$.support.fileInput)
                .parent().addClass($.support.fileInput ? undefined : 'disabled');
        }
    },

    getSuccessIcon: function (id) {
        /// <summary>
        /// returns a tag.
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        var self = $.devOrg.upload;

        return self.$allSuccessIcons.filter("[data-id=" + id + "]");
    },
    showAllErrorDisplay: function (id) {
        var $errorSpecficDetailBoxes = $.devOrg.upload.$allErrorsRelatedTags.filter("[data-id=" + id + "]");

        if ($errorSpecficDetailBoxes.is(":hidden")) {
            $errorSpecficDetailBoxes.fadeIn("slow");
        }
    },

    getErrorUL: function (id) {
        /// <summary>
        /// returns ul.
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        return $.devOrg.upload.$allULErrorsRelatedTags.filter("[data-id=" + id + "]");
    },

    addErrorInfoInUL: function ($ul, msg, title) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="$ul"></param>
        /// <param name="msg"></param>
        /// <param name="title"></param>
        $ul.append('<li class="label label-danger block" title="' + title + '">' + msg + '</li>');
    },



    showSuccessIcon: function (id) {
        return $.devOrg.upload.getSuccessIcon(id).show("slow");
    },
    hideSuccessIcon: function (id) {
        return $.devOrg.upload.getSuccessIcon(id).hide();
    },


    getFailedIcon: function (id) {
        /// <summary>
        /// returns a tag.
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        return $.devOrg.upload.$allFailedIcons.filter("[data-id=" + id + "]");
    },

    setSuccessIconMsg: function (id, msg) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>

        var $icon = $.devOrg.upload.getSuccessIcon(id);
        $icon.attr("data-original-title", msg);
        return $icon.attr("title", msg).tooltip();
    },

    setFailedIconMsg: function (id, msg) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        var $icon = $.devOrg.upload.getFailedIcon(id);
        $icon.attr("data-original-title", msg);
        return $icon.attr("title", msg).tooltip();
    },

    showFailedIcon: function (id) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        return $.devOrg.upload.getFailedIcon(id).show("slow");
    },

    hideFailedIcon: function (id) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        return $.devOrg.upload.getFailedIcon(id).hide();
    },

    getLabelToIndicateUploadedFiles: function (id) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns type=""></returns>
        return $.devOrg.upload.$allLabelsToIndicateUploadedFilesNumber.filter("[data-id=" + id + "]");
    },

    getEditButton: function (id) {
        /// <summary>
        /// returns a tag.
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        return $.devOrg.upload.$allEditButtons.filter("[data-id=" + id + "]");
    },
    getEditSpinner: function (id) {
        /// <summary>
        /// returns a tag.
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        /// <returns type=""></returns>
        return $.devOrg.upload.$allSpinners.filter($.devOrg.upload.editBtnSpinnerSelector + "[data-id=" + id + "]");
    },



    getInputFile: function (id) {
        /// <summary>
        /// returns input tag with file
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        /// <returns type=""></returns>
        return $.devOrg.upload.$allFileInputTypes.filter("[data-id=" + id + "]");
    },

    getUploaderSpinner: function (id) {
        /// <summary>
        /// returns the whole 'A' tag / progressor spinner for upload
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        /// <returns type=""></returns>
        return $.devOrg.upload.$allSpinners.filter($.devOrg.upload.editBtnSpinnerSelector + "[data-id=" + id + "]");
    },

    getProgressorValudeIndicator: function (id) {
        /// <summary>
        /// returns A tag inside there will be an span for the value show.
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        /// <returns type=""></returns>
        return $.devOrg.upload.$allProgressorValueIdicator.filter("[data-id=" + id + "]");
    },
    getWholeProgressorDiv: function (id) {
        /// <summary>
        /// returns a div
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        /// <returns type=""></returns>
        return $.devOrg.upload.$allWholeProgressor.filter("[data-id=" + id + "]");
    },





    hideUploadProgressor: function (id) {
        /// <summary>
        /// hides the whole progressor div.
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        /// <returns type=""></returns>
        var $wholeDiv = $.devOrg.upload.getWholeProgressorDiv(id);
        if ($wholeDiv.is(":visible")) {
            $wholeDiv.hide();
        }
    },

    showUploadProgressor: function (id) {
        /// <summary>
        /// show the whole progressor div.
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        /// <returns type=""></returns>
        $.devOrg.upload.getWholeProgressorDiv(id).fadeIn("slow");

    },

    setProgressorValue: function (id, val) {
        /// <summary>
        /// sets the value of the progresssor, it doesn't do the visible thing.
        /// </summary>
        /// <param name="id">data-id</param>
        /// <param name="val">only give the number between 1-100</param>
        var $indicator = $.devOrg.upload.getProgressorValudeIndicator(id);
        if ($indicator.length > 0) {
            $indicator.attr("title", val + "% done.");
            $indicator.attr("data-original-title", val + "% done.");
            return $indicator.find("span").text(val + "%");
        }
        return $indicator;
    },

    setTextInLabel: function ($label, val) {
        /// <summary>
        /// set text to the results display labels. It also contains the uploaded files number.
        /// </summary>
        /// <param name="id">data-id</param>
        /// <param name="val">Any text. Empty text hides the label.</param>
        /// <returns type="$label">Returns the label.</returns>
        if ($label.length > 0) {
            if (val === null || val === undefined || val === "" || val.length == 0) {
                // empty
                $label.hide();
                $label.text("");
            } else {
                if ($label.is(":hidden")) {
                    $label.show("slow");
                }
                $label.text(val);
            }
        }
        return $label;
    },
    setTextInLabelWithUploadNumber: function ($label, val, increaseTheNumberOfUpload) {
        /// <summary>
        /// set text to the results display labels. It also contains the uploaded files number.
        /// Will not update count more than max.
        /// </summary>
        /// <param name="id">data-id</param>
        /// <param name="val">Any text. Empty text hides the label.</param>
        /// <param name="increaseTheNumberOfUpload">increases the value of the upload value</param>
        /// <returns type="">
        /// return updated count value.
        /// </returns>
        var self = $.devOrg.upload;
        var currentUploadCount = parseInt($label.attr(self.uploadNumberParameter));
        var maxUploadCount = parseInt($label.attr(self.maxUploadParameter));
        if (_.isNaN(maxUploadCount)) {
            maxUploadCount = 1;
        }
        if (_.isNull(increaseTheNumberOfUpload) || _.isNaN(increaseTheNumberOfUpload) || _.isUndefined(increaseTheNumberOfUpload)) {
            increaseTheNumberOfUpload = 0;
        }
        var updatedValue = currentUploadCount + increaseTheNumberOfUpload;
        if (updatedValue > maxUploadCount) {
            updatedValue = maxUploadCount;
        }
        if (updatedValue < 0) {
            updatedValue = 0;
        }
        $label.attr(self.uploadNumberParameter, updatedValue)
              .text(updatedValue + " " + val);
        return updatedValue;
    },

    setTextInLabelWithPreUploadNumber: function ($label, val, preuploadedCount) {
        /// <summary>
        /// set text to the results display labels. Sets the pre
        /// </summary>
        /// <param name="id">data-id</param>
        /// <param name="val">Any text. Empty text hides the label.</param>
        /// <param name="preuploadedCount">increases the value of the upload value</param>
        var self = $.devOrg.upload;
        $label.attr(self.preuploadedCount, preuploadedCount)
              .text(preuploadedCount + " " + val);
    },

    showEditButton: function (id) {
        /// <summary>
        /// show the whole progressor div.
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        /// <returns type=""></returns>
        return $.devOrg.upload.getEditButton(id).show("slow");
    },

    hideEditButton: function (id) {
        /// <summary>
        /// show the whole progressor div.
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        /// <returns type=""></returns>
        $.devOrg.upload.getEditButton(id).hide();
    },

    showEditProgressor: function (id) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        /// <returns type=""></returns>
        return $.devOrg.upload.getEditSpinner(id).fadeIn("slow");
    },

    hideEditProgressor: function (id) {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">number of the data-id attribute</param>
        /// <returns type=""></returns>
        return $.devOrg.upload.getEditSpinner(id).hide();
    },



    uploadDeleteBtnClicked: function (e, $deleteBtn, $editBtn, id, sequence, $imageRow, $uploaderLabel, $uploaderInput) {
        var url = $deleteBtn.attr("href");
        var count = 0;
        var self = $.devOrg.upload;
        $.ajax({
            type: "GET",
            dataType: "json",
            url: url,
            success: function (response) {
                if (response) {
                    // removed
                    $imageRow.hide('slow', function () {
                        $imageRow.remove();
                    });
                    count = self.setTextInLabelWithUploadNumber($uploaderLabel, $.devOrg.upload.uploadedFilesMessage, -1);
                    if (count <= 0) {
                        $editBtn.hide();
                    }
                }
            },
            error: function (xhr, status, error) {

            }
        }); // ajax end

    },


    uploadEditBtnClicked: function (e, $editBtn, id, $uploaderLabel, $uploaderInput) {
        e.preventDefault();
        var url = $editBtn.attr("href");
        var self = $.devOrg.upload;

        var $spinner = self.showEditProgressor(id);


        $.ajax({
            type: "POST",
            dataType: "html",
            url: url,
            data: $.devOrg.upload.formData,
            success: function (response) {
                // Remove the processing state     
                //$editList.html(response);
                var $response = $(response);
                $response.modal();
                //inside modal find delete btns and bind it with delete event.

                var deleteBtns = $response.find("a[data-btn='delete']");
                deleteBtns.on('click', function (ew) {
                    ew.preventDefault();
                    var $deleteButton = $(this);
                    var sequence = $deleteButton.attr("data-sequence");

                    var $imageRow = $response.find("div.row[data-id='" + id + "'][data-sequence='" + sequence + "']");

                    self.uploadDeleteBtnClicked(e, $deleteButton, $editBtn, id, sequence, $imageRow, $uploaderLabel, $uploaderInput);
                });


                //$response.find("[data-btn='close']").on('click', modelCloseClicked)
                $response.on('hidden.bs.modal', modelCloseClicked);
                $spinner.hide();
            },
            error: function (xhr, status, error) {
                // Remove the processing state
                $spinner.hide();
                //console.error("Error occurred when drafting app. Err Msg:" + error);
            }
        }); // ajax end

        function modelCloseClicked() {
            $("body").removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('.modal').remove();
        }


    },

    uploaderEditBtnClickEvntBinder: function ($editBtn, $label, $uploaderInput, id) {
        /// <summary>
        /// It's not the actual event but it's the method which binds edit button with click event.
        /// Hides unnecessary edit buttons
        /// </summary>
        /// <param name="$editBtn">
        /// </param>
        /// <param name="$label">
        /// </param>
        /// <param name="$uploaderInput">
        /// </param>
        /// <param name="id">
        /// </param>

        if ($editBtn.length > 0) {
            $editBtn.on('click', function (e) {
                e.preventDefault();
                $.devOrg.upload.uploadEditBtnClicked(e, $editBtn, id, $label, $uploaderInput);
            });
        }
    },

    uploaderFixingDataUrlOnInvalidUrls: function ($uploader) {
        var currentUrl = $uploader.attr("data-url");
        if (_.isEmpty(currentUrl)) {
            var $form = $uploader.closest("form");
            formUrl = $form.attr("action");
            $uploader.attr("data-url", formUrl);
        }
    },

    isPreuploadExist: function ($label) {
        var count = parseInt($label.attr($.devOrg.upload.uploadPreexistCountParameter));
        if (count > 0) {
            return true;
        }
        return false;
    },
    getPreuploadValue: function ($label) {
        var count = parseInt($label.attr($.devOrg.upload.uploadPreexistCountParameter));
        return count;
    },

    getCountOfHowManyFilesUploaded: function (id) {
        var $label = $.devOrg.upload.getLabelToIndicateUploadedFiles(id);
        var count = parseInt($label.attr($.devOrg.upload.uploadNumberParameter));
        return count;
    },
    showEditButtonBasedOnPreUploadNShowMessageOnLabel: function ($label, id) {
        /// <summary>
        /// Fix uploaded counts from preexist count.
        /// </summary>
        /// <param name="$label">
        /// 
        /// </param>
        /// <param name="id">
        /// 
        /// </param>
        /// <returns type="">
        /// return preupload count.
        /// </returns>
        var count = parseInt($label.attr($.devOrg.upload.uploadPreexistCountParameter));
        var hasEditbutton;
        var $editBtn;
        if (count > 0) {
            //if any preload exist then

            // set pre load to load
            $label.attr($.devOrg.upload.uploadNumberParameter, count);

            hasEditbutton = $label.attr($.devOrg.upload.hasEditButtonParameter);

            //show edit buttn
            if (hasEditbutton) {
                $editBtn = $.devOrg.upload.getEditButton(id);
                $editBtn.fadeIn("slow");
            }

            //var $label = $.devOrg.upload.getLabelToIndicateUploadedFiles(id);

            //set msg
            $.devOrg.upload.setTextInLabelWithPreUploadNumber($label, $.devOrg.upload.preUploadedFilesMessage, count);
            return count;
        }
        return 0;
    },
    getId: function ($uploaderItem) {
        return $uploaderItem.attr("data-id");
    },
    onFileUploadAddedEvent: function (e, data, $this) {
        var id = $this.attr("data-id");
        var self = $.devOrg.upload;

        var $label = self.getLabelToIndicateUploadedFiles(id);
        self.showUploadProgressor(id);
        self.setTextInLabel($label, "Uploading...");
    },
    onSubmitEvent: function (e, data, $this) {
        //var id = $this.attr("data-id");
        data.formData = $.devOrg.upload.formData;
    },
    onUploadDoneEvent: function (e, data, $this) {
        var id = $this.attr("data-id");
        //console.log("done" + id);
        var result = data.result;
        var isSingleFileUploader = false;
        var self = $.devOrg.upload;
        var $label = self.getLabelToIndicateUploadedFiles(id);

        var isUploaded = result.isUploaded;
        var uploadedFilesCount = result.uploadedFiles;
        var message = result.message;
        var fileNames = [];
        var $editBtn = null;
        if ($label.is(":hidden")) {
            $label.fadeIn("slow");
        }


        // check if single uploader.
        if (!$this.attr("multiple")) {
            isSingleFileUploader = true;
        }

        if (isSingleFileUploader) {
            if (isUploaded) {
                uploadedFilesCount = 1;
            }
        } else {
            // if multiple then consider edit button to show up.
        }
        if ($this.attr(self.hasEditButtonParameter)) {
            // edit button, show edit button if exist.
            $editBtn = self.getEditButton(id);
            if ($editBtn.is(":hidden")) {
                $editBtn.show("slow");
            }
        }

        var length = data.files.length;
        for (var i = 0; i < length; i++) {
            var file = data.files[i];
            fileNames.push(file.name);
        }

        var filesNamesString = fileNames.join();

        var labelTitle = $label.attr("title");
        if (!_.isEmpty(labelTitle)) {
            labelTitle += ",";
        } else {
            labelTitle = "";
        }
        if (isUploaded) {

            // upload successful
            self.setTextInLabelWithUploadNumber($label, $.devOrg.upload.uploadedFilesMessage, uploadedFilesCount);
            //icons
            var failed = self.hideFailedIcon(id);
            self.showSuccessIcon(id);
            var success = self.setSuccessIconMsg(id, filesNamesString);
            //console.log(success);

            labelTitle += filesNamesString;
            $label.attr("title", labelTitle);
            //console.log($label);

            // hide progressor
            self.hideUploadProgressor(id);
        } else {
            self.onUploadFailedErrorEvent(e, data, $this);
            self.setFailedIconMsg(id, filesNamesString);
        }

    },
    onUploadFailedErrorEvent: function (e, data, $this) {
        var id = $this.attr("data-id");
        var length = data.files.length;
        var files = data.files;
        var file = "";
        var self = $.devOrg.upload;
        var $label = self.getLabelToIndicateUploadedFiles(id);

        // show error headers
        self.showAllErrorDisplay(id);
        var $ul = self.getErrorUL(id);
        if ($ul.length > 0) {
            for (var i = 0; i < length; i++) {
                file = files[i];
                self.addErrorInfoInUL($ul, file.name, file.name + " upload failed.");
            }
        }
        self.showFailedIcon(id);
        self.hideSuccessIcon(id);
        self.setFailedIconMsg(id, "Failed.");
        //reset the upload label.
        self.setTextInLabelWithUploadNumber($label, $.devOrg.upload.uploadedFilesMessage);
        // hide progressor
        self.hideUploadProgressor(id);
    }
}


