$(function () {
    $("#pageCtnr li").each(function () {
        $(this).click(function () {
            submitSearch($(this).html());
        });
    });

    $("#pageMiniCtnr li").each(function () {
        $(this).click(function () {
            submitSearch($(this).attr('data-page'));
        });
    });

    $('.exp-title').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            toggleFilter(this);
        });
    });

    $("[data-exp-c]").each(function () {
        toggleFilter(this);
    });
});

function toggleFilter(expTitle) {
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

$(function () {
    $("#filterCtnr input").each(function () {
        $(this).removeAttr('disabled');
        $(this).change(function () {
            var $this = $(this);
            if ($this.val() === '_all_') {
                var grpName = $this.attr('data-group');
                $('input[name=' + grpName + ']').removeAttr('checked');
                $this.prop('checked', true);
            } else {
                var grpName = $this.attr('name');
                $('#' + grpName).prop('checked', !$("[name=" + grpName + "]").is(':checked'));
            }
            submitSearch();
        });
    });


    $("#filterCtnr input[type=text]").autocomplete({
        select: function (event, ui) {
            $(this).val(ui.item.value);
            submitSearch();
        }
    });

    $("#filterCtnr input[type=text]").keydown(function (event) {
        if (event.keyCode === 9) {
            $(this).val("");
        }
    });
});

function submitSearch(pageNo) {
    $('#overBk').show();
    $('#overCnt').show();
    pageNo = pageNo === undefined ? 1 : pageNo;
    $("#PageIndex").val(pageNo);
    $("#filterCtnr form").submit();
}

$(function () {

    $('.btn-mail').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            $("#filterCtnr #output").val("mail");
            $("#filterCtnr form").submit();
        })
    });

    $('#filter-summ .reset-btn').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            $('#filterCtnr #CourseName').val('');
            $('#filterCtnr input[type="checkbox"]').prop('checked', false);
            $("#searchCtnr").css({ opacity: 0.5 });
            $("#filterCtnr form").submit();
        });
    });

    $('#filter-summ .close').each(function () {
        var $this = $(this);
        $this.unbind('click');
        $this.click(function () {
            var id = $this.attr('data-id');
            if (id === 'name') {
                $('#filterCtnr #name').val('');
            } else if (id === 'pass') {
                $('#filterCtnr #passport').val('');
            }
            else {
                $('#' + id).prop('checked', false);
            }
            $("#searchCtnr").css({ opacity: 0.5 });
            $("#filterCtnr form").submit();
        });
    });
});
