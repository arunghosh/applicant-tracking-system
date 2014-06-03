

var BaseProfile = function (contoller) {
    var base = "\\Profile\\" + contoller + "\\"
    this.ctnrId = "#" + contoller.toLowerCase() + "Ctnr";
    this.editUrl = base + "Edit";
    this.refreshUrl = base + "Show";
    this.refreshViewId = this.ctnrId;

    this.busyCnt = '<img src="/Content/images/busy.gif" style="position:relative"/>'
    this.param = undefined;
    this.id = undefined;
    new RegisterProfileEdit(this);
};

var ListProfile = function (contoller, itemId) {
    var base = "\\Profile\\" + contoller + "\\"
    this.ctnrId = "#" + contoller.toLowerCase() + "Ctnr_" + itemId;
    this.removeUrl = base + "Remove";
    this.editUrl = base + "Edit";
    this.refreshUrl = base + "List";
    this.refreshViewId = "#" + contoller.toLowerCase() + "ListCtnr";
    this.id = itemId;
    this.busyCnt = '<img src="/Content/images/busy.gif"/>'
    this.param = undefined; // For edit of address
    new RegisterProfileEdit(this);
};

var AddressProfile = function (contoller, action) {
    var base = "\\Profile\\" + contoller + "\\"
    this.ctnrId = "#" + action.toLowerCase() + "Ctnr";
    this.editUrl = base + action + "Edit";
    this.refreshUrl = base + "List";
    this.refreshViewId = "#" + contoller.toLowerCase() + "ListCtnr";
    this.busyCnt = '<img src="/Content/images/busy.gif"/>'
    this.id = undefined;
    this.param = undefined; // For edit of address
    new RegisterProfileEdit(this);
};

var AddProfile = function (contoller) {
    var base = "\\Profile\\" + contoller + "\\"
    this.ctnrId = "#add" + contoller + "Ctnr";
    this.editUrl = base + "Edit";
    this.refreshUrl = base + "List";
    this.refreshViewId = "#" + contoller.toLowerCase() + "ListCtnr";
    this.busyCnt = '<img src="/Content/images/busy.gif"/>'
    this.param = undefined; // For edit of address
    this.id = undefined;
    new RegisterProfileEdit(this);
};

function RegisterProfileEdit(prof) {
    var profile = prof;
    var regSubmit = function () {
        var ctnrId = profile.ctnrId;
        var $cntr = $(ctnrId);
        $cntr.find('form').submit(function () {
            showFrmBusy($cntr);
            $.ajax({
                url: this.action,
                type: this.method,
                data: $(this).serialize(),
                success: function (result) {
                    hideFrmBusy($cntr);
                    if (result.errMsg === null) {
                        closeEdit();
                    }
                    else {
                        var errMsg = result.errMsg;
                        $cntr.find('.validation-summary-errors').hide();
                        $cntr.find('.server-vald-msg').show().text(errMsg);
                    }
                }
            });
            return false;
        });
    }

    var regRemove = function () {
        var $ctnr = $(profile.ctnrId);
        $ctnr.find('.removefrm').each(function () {
            var form = this;
            $(form).find('a').click(function () {
                showFrmBusy($(profile.ctnrId));
                callAjax(form, function (result) {
                    hideFrmBusy($(profile.ctnrId));
                    $ctnr.find(".edit-ctnr").html(result);
                });
            });
        });
    }

    var regClose = function () {
        var ctnrId = profile.ctnrId;
        $(ctnrId + " .edit-close").click(function () {
            closeEdit();
        });
    }

    var closeEdit = function () {
        $.ajax({
            url: profile.refreshUrl,
            type: "Get",
            success: function (result) {
                $(profile.refreshViewId).html(result);
            }
        });
    }
    var regEdit = function () {
        var $ctnr = $(profile.ctnrId);
        $ctnr.find(".edit-icon").click(function () {
            var $edit = $ctnr.find(".edit-ctnr");
            debugger;
            $edit.html(profile.busyCnt);
            $edit.find(".popover").show();
            $.ajax({
                url: profile.editUrl,
                type: 'GET',
                data: { id: profile.id, param: profile.param },
                success: function (editView) {
                    $edit.html(editView);
                    $edit.find(".popover").show();
                    regRemove();
                    regSubmit();
                    regClose();
                    $edit.find(".server-vald-msg").hide();
                }
            });
        });
    }
    regEdit();
};

