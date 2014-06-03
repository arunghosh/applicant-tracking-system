$(function () {
    $('.refresh-form').each(function () {
        var $this = $(this);
        $this.unbind('submit');
        $this.submit(function () {
            showFrmBusy($this);
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    if (result.errMsg === null) {
                        refreshForm($this.parents(".a-frm-ctnr"));
                    }
                    else {
                        hideFrmBusy($this);
                        var errMsg = result.errMsg;
                        $this.find(".validation-summary-errors").show().text(errMsg)
                    }
                }
            });
            return false;
        });
    });

    $(function () {
        $('.replace-frm').each(function () {
            var $this = $(this);
            $this.unbind('submit');
            $this.submit(function () {
                //if ($this.valid()) {
                showFrmBusy($this);
                $.ajax({
                    url: this.action,
                    type: this.method,
                    data: $(this).serialize(),
                    success: function (result) {
                        $this.parents(".a-frm-ctnr").html(result);
                        hideFrmBusy($this);
                    }
                });
                //}
                return false;
            });
        });
    });

    $('.replace-stat-frm').each(function () {
        var $this = $(this);
        $this.unbind('submit');
        $this.submit(function () {
            showBusy($this);
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    $this.parents(".a-frm-ctnr").html(result.status);
                    clearBusy($this);
                }
            });
            return false;
        });
    });

    $('.replace-txt-frm').each(function () {
        var $this = $(this);
        $this.unbind('submit');
        $this.submit(function () {
            showBusy($this);
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    $this.find('.rep-txt').html(result);
                    clearBusy($this);
                }
            });
            return false;
        });
    });


    $(function () {
        axbOnResize();
        $('.status-btn').each(function () {
            var $this = $(this);
            $this.unbind('click');
            $this.click(function () {
                var form = $(this).parents("form");
                form.find("[name=Status]").val($(this).attr('data-status'));
                form.submit();
            });
        });
    });
});