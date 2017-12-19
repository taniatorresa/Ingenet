var FormFileUpload = function () {
    return {
        //main function to initiate the module
        init: function () {

             // Initialize the jQuery File Upload widget:
            $('#form_sample_3').fileupload({
                disableImageResize: false,
                autoUpload: false,
                disableImageResize: /Android(?!.*Chrome)|Opera/.test(window.navigator.userAgent),
                maxFileSize: 5000000,
                acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
                // Uncomment the following to send cross-domain cookies:
                //xhrFields: {withCredentials: true},                
            });

            // Enable iframe cross-domain access via redirect option:
            $('#form_sample_3').fileupload(
                'option',
                'redirect',
                window.location.href.replace(
                    /\/[^\/]*$/,
                    '/cors/result.html?%s'
                )
            );

            // Upload server status check for browsers with CORS support:
            if ($.support.cors) {
                $.ajax({
                    type: 'HEAD'
                }).fail(function () {
                    $('<div class="alert alert-danger"/>')
                        .text('Upload server currently unavailable - ' +
                                new Date())
                        .appendTo('#form_sample_3');
                });
            }

            // Load & display existing files:
            $('#form_sample_3').addClass('fileupload-processing');
            $.ajax({
                // Uncomment the following to send cross-domain cookies:
                //xhrFields: {withCredentials: true},
                url: $('#form_sample_3').attr("action"),
                dataType: 'json',
                context: $('#form_sample_3')[0]
            }).always(function () {
                $(this).removeClass('fileupload-processing');
            }).done(function (result) {
                $(this).fileupload('option', 'done')
                .call(this, $.Event('done'), {result: result});
            });
        }

    };

}();

jQuery(document).ready(function() {
    FormFileUpload.init();
});