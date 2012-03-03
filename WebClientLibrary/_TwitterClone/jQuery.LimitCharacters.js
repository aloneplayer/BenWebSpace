jQuery.fn.limitCharacters = function (options)
{
    if (this.length == 0) return;

    // Default settings
    var settings = {
        charLimit: 50,
        showRemaining: true,
        remainingClass: 'remainingChars',
        remainingWarnClass: 'remainingCharsWarn'
    };

    if (options)
    {
        $.extend(settings, options);
    }

    this.after("<span id='CharsLeft'></span>");

    this.keyup(function ()
    {
        var len = $(this).val().length;
        if (len > settings.charLimit)
        {
            this.value = this.value.substring(0, settings.charLimit);
        }

        var charsLeft = settings.charLimit - len;

        if (charsLeft < 0)
        {
            charsLeft = 0;
        }

        $('#CharsLeft').text(charsLeft + " characters left");

        if ((settings.charLimit - len) > 10)
        {
            if (!$('#CharsLeft').hasClass(settings.remainingClass))
            {
                $('#CharsLeft').addClass(settings.remainingClass);
            }
            if ($('#CharsLeft').hasClass(settings.remainingWarnClass))
            {
                $('#CharsLeft').removeClass(settings.remainingWarnClass);
            }

        } else
        {

            if (!$('#CharsLeft').hasClass(settings.remainingWarnClass))
            {
                $('#CharsLeft').addClass(settings.remainingWarnClass);
            }

        }

        return this;

    });


}