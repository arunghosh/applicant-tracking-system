var busyImg = '<img src="/Content/Images/busy_h.gif" />';
$(function () {
    $('.datepicker').datepicker();
    //$('.datepicker-dmy').datepicker();
    $('.datepicker').datepicker("option", "dateFormat", "d, M y");
    $(".datepicker").each(function () {
        //var $this = $(this);
        //var utc = new Date($this.val());
        //utc.setMinutes(utc.getMinutes() - utc.getTimezoneOffset());
        //$this.val(utc.toLocaleDateString());
    });

    $('#clndrTab li').click(function () {
        $('#clndrTab li').removeClass('active');
        $(this).addClass('active');
        if ($(this).attr('data-id') == 0) {
            $('#consolLst').show();
            $('#clndrHrinLst').hide();
        } else {
            $('#consolLst').hide();
            $('#clndrHrinLst').show();
        }
    });

    var $head = $('#menu');
    if ($head.length > 0) {
        var previousScroll = 0,
        headerOrgOffset = $head.offset().top;
        $(window).scroll(function () {
            var currentScroll = $(this).scrollTop();
            if (currentScroll > previousScroll) {
                $head.removeClass('fixed');
                $head.hide('slide', { direction: 'up' });
            } else {
                $head.show('slide', { direction: 'up' });
                $head.addClass('fixed');
            }
            previousScroll = currentScroll;
        });
    }

    $('.ntfyMsg').tooltip(
        {
        });
    $('.acc-title').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            toggleExp(this);
        });
    });

    $('.lazy-load').each(function () {
        refreshLazy($(this));
    });

    // New Pop-up for Message/Discssion/Job Post
    $('.new-obj').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            showDialog($this, null);
        })
    });

    $('.note-close').click(function () {
        $(this).parents('.status-note').hide();
    });


    var $nmeSrch = $("#nameSearchCtnr");
    if ($nmeSrch.length === 1) {
        $nmeSrch.find('img').click(function () {
            $nmeSrch.find('form').submit();
        });
        $nmeSrch.find('input').keyup(function (event) {
            if (event.keyCode == 13) {
                $nmeSrch.find('form').submit();
            }
        });
        $nmeSrch.find('input').autocomplete({
            select: function (a, b) {
                $(this).val(b.item.value);
                $nmeSrch.find('form').submit()
            }
        });
    }

    $(document).ajaxError(function (xhr, props) {
        if (props.status === 401) {
            location.reload();
        }
    });
});

function toggleExp(expTitle) {
    var id = $(expTitle).attr('data-exp-target');
    var $target = $('#' + id);
    $expTitle = $(expTitle);
    $expIcon = $expTitle.find('.exp-icon');
    if ($target.css('display') == 'none') {
        $expIcon.removeClass('icon-ch-r');
        $expIcon.addClass('icon-ch-d');
        $expTitle.find('input').val('true');
    }
    else {
        $expIcon.removeClass('icon-ch-d');
        $expIcon.addClass('icon-ch-r');
        $expTitle.find('input').val('false');
    }
    $target.toggle('slide', { direction: 'up' });
}

function refreshLazy($this) {
    if ($this.attr('data-loaded') === undefined) {
        var url = $this.attr('data-url');
        if ($this.attr('data-busy') !== 'false') {
            $this.append('<img src="/Content/images/busy_h.gif" style="margin:10px;"/>')
        }
        $.ajax({
            url: url,
            type: 'Get',
            data: { id: $this.attr('data-id') },
            success: function (result) {
                $this.attr('data-loaded', true);
                $this.html(result);
            }
        });
    }
}

// Display Dialog
function showDialog($this, data) {
    var $ctnr = $("#dialogCtnr");
    showAppBusy($this.attr('data-title'));
    $ctnr.dialog({
        modal: true, autoOpen: false, resizable: false,
        width: 'auto', title: $this.attr('data-title'), draggable: false
    });
    var url = $this.attr('data-url');
    $.ajax({
        url: url,
        type: $this.attr('data-method') === undefined ? "POST" : "GET",
        traditional: true,
        data: { userIds: data },
        success: function (result) {
            hideAppBusy();
            $ctnr.html(result);
            $ctnr.dialog("open");
        }
    });
}

function refreshForm(ctnr) {
    showBusy(ctnr);
    var id = ctnr.attr('data-id');
    var url = ctnr.attr('data-url');
    $.ajax({
        url: url,
        type: 'Get',
        data: { id: id },
        success: function (result) {
            ctnr.html(result);
            clearBusy(ctnr);
        }
    });
    try {
        updateUrl(id);
    } catch (err) { }

}

function updateUrl(id) {
    var currUrl = document.URL;
    var urlArr = currUrl.split('/');
    var intRegex = /^\d+$/;
    if (intRegex.test((urlArr[urlArr.length - 1]))) {
        window.history.pushState("", "", "/" + urlArr[urlArr.length - 2] + "/" + id);
    }
    else {
        window.history.pushState("", "", "/" + urlArr[urlArr.length - 1] + "/" + id);
    }
}

$(function () {
    $("input[data-autocomplete-source]").each(function () {
        var target = $(this);
        target.autocomplete({ source: target.attr("data-autocomplete-source") });
    });

    $('body').unbind('click');
    $('body').click(function (event) {
        // Get the focused element:
        var noEle = $(event.target).parents('.ntfy-lst').length + $(event.target).parents('.popover').length + $(event.target).parents('.notify-icon').length;
        var $trgt = $(event.target);
        if (!$trgt.hasClass('stat-lst') && $trgt.parents('.stat-lst').length === 0 && !$trgt.hasClass('stat-chge-btn')
            && $trgt.parents('.stat-chge-btn').length === 0 && !$trgt.hasClass('ui-datepicker') && $trgt.parents('.ui-datepicker').length == 0 && $trgt.parents('.ui-datepicker-header').length == 0) {
            $('.stat-lst').fadeOut();
        }
        if (noEle === 0) {
            $('.popover').hide();
            $('.ntfy-ctnr').hide();
        }

    });

    $("#busyIndicator").hide();
    axbOnResize();
});

function callAjax(form, callBack) {
    $.ajax({
        url: form.action,
        type: form.method,
        data: $(form).serialize(),
        success: function (result) {
            if (callBack !== undefined) {
                callBack(result);
            }
        }
    });
}

function showFullBusy() {
    $('#overBk').show();
    $('#overCnt').show();
}

function showFullBusy() {
    $('#overBk').hide();
    $('#overCnt').hide();
}

function showAppBusy(title) {
    $("#busyIndicator").dialog({
        modal: true, autoOpen: false, resizable: false,
        width: 'auto', title: title, draggable: false
    });
    $("#busyIndicator").dialog('open');
}

function hideAppBusy() {
    $("#busyIndicator").dialog('close');
}



function axbOnResize() {
    var avaiHeight = $(window).height() - $('#header').height() - 55;

    $('#mainContent').css('min-height', avaiHeight + 70);

    $('.msg-thread').slimScroll({
        height: avaiHeight - $('#threadNewMsg').height()
    });

    //$('.msg-thread').height(avaiHeight);

    $('.avail-ht').slimScroll({
        height: avaiHeight
    });

    $('.pop-up-ht').slimScroll({
        height: avaiHeight - 55
    });
}


function showFrmBusy($this) {
    $this.find('.frm-busy').show();
}

function hideFrmBusy($this) {
    $this.find('.frm-busy').hide();
}
function showBusy($this) {
    $this.find('.frm-busy').show();
    $this.css("opacity", "0.7");
}

function clearBusy($this) {
    $this.find('.frm-busy').hide();
    $this.css("opacity", "1");
}

$(".a-sele-item").each(function () {
    var $this = $(this);
    $this.unbind('click');
    $this.click(function () {
        var ctnr = $(".a-frm-ctnr");
        ctnr.html('<img src="/Content/images/busy.gif"/>');
        $(".a-sele-item").removeClass('seleted');
        $this.addClass('seleted');
        var id = $(this).attr('data-id')
        ctnr.attr('data-id', id);
        refreshForm(ctnr);
    });
});

$(function () {
    $('.a-msg-new').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            var $div = $this.next('div');
            var id = $this.attr('data-id');
            $.ajax({
                url: '/Message/NewMessagePop',
                type: 'Get',
                data: { userId: id },
                success: function (result) {
                    $div.html(result);
                    $div.find('.popover').show();
                    clearBusy($this);
                }
            });
        });
    });

    $('.a-add-circle').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            var $div = $this.next('div');
            $div.html('<div class="arrow"></div><img src="/Content/images/busy.gif" class="p20" />');
            var id = $(this).attr('data-id');
            $.ajax({
                url: '/Circle/AddUser',
                type: 'Get',
                data: { userId: id },
                success: function (result) {
                    $div.html(result);
                    $div.find('.popover').show();
                    clearBusy($this);
                }
            });
        });
    });
});