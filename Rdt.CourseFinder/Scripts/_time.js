$(function () {
    $('.time').each(function () {
        var $this = $(this);
        var utc = $this.attr('data-utc');
        if (utc === undefined) return "--";
        var arr = utc.split('/');
        var date = new Date(Date.UTC(arr[0], arr[1] - 1, arr[2], arr[3], arr[4]));

        var diffMs = (new Date() - date)
        var diffDays = Math.round(diffMs / 86400000); // days
        var diffHrs = Math.round(diffMs / 3600000); // hours
        var diffMins = Math.round(diffMs / 60000); // minutes
        var time;
        var hrs = date.getHours();
        var fMin = date.getMinutes();
        fMin = fMin > 9 ? fMin : ('0' + fMin);
        if (hrs > 12) {
            time = (hrs - 12) + ":" + fMin + " PM";
        } else if (hrs == 0) {
            time = 12 + ":" + fMin + " AM";
        }
        else {
            time = (hrs < 9 ? ("0" + hrs) : hrs) + ":" + fMin + " AM";
        }

        var format = $this.attr('data-format');

        var display = date.toString().substr(4, 17);

        if (format === 'DayOnly') {
            display = date.toDateString().substr(0, 3);
        }
        if (format === 'WithDay') {
            display = date.toDateString().substr(0, 15);//.replace(new Date().getFullYear(), '');
        }
        if (format === 'DateOnly') {
            display = date.toDateString().substr(4, 15);//.replace(new Date().getFullYear(), '');
        }

        if (format === 'WithTime') {
            display = date.toDateString().replace(new Date().getFullYear(), '') + time;
        }

        if (format === 'Normal') {
            display = date.toDateString().substr(4, 13).replace(new Date().getFullYear(), '') + ' ' + time;
        }

        if (format === 'TimeOnly') {
            display = time;
        }

        if (format === 'Medium') {
            display = date.toDateString().substr(4, 15).replace(new Date().getFullYear(), '');
        }

        if (format === 'Short') {
            if (60 > diffMins) {
                display = diffMins + "m";
            } else if (24 > diffHrs) {
                display = diffHrs + " h";
            } else if (10 > diffDays) {
                display = diffDays + "d";
            } else {
                display = date.toDateString().substr(4, 15).replace(new Date().getFullYear(), '');
            }
        }

        $this.html(display);
    });
});