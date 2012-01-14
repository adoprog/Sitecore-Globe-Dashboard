// Colors set 
var colors = [
        0xd9d9d9, 0xb6b4b5, 0x9966cc, 0x15adff, 0x3e66a3,
        0x216288, 0xff7e7e, 0xff1f13, 0xc0120b, 0x5a1301, 0xffcc02,
        0xedb113, 0x9fce66, 0x7ed3f7,
        0xfe9872, 0x7f3f98, 0xf26522, 0x2bb673, 0xd7df23,
        0xe6b23a, 0x7ed3f7];

// Load traffic types data
trafficServiceUrl = "/GlobeService.svc/GetTrafficTypes"
$.ajax({
    url: trafficServiceUrl,
    type: 'get',
    cache: false,
    contentType: 'application/json; charset=utf-8',
    dataType: 'json',
    success: function (callback) {
        var data = callback.d;
        $.each(data, function (index, trafficType) {
            if (index % 2 == 0) {
                $('#trafficTypes').append('<li>' + trafficType + ' ' + '</li>');
            }
            else {
                $('#trafficTypes').append('<li style="list-style-type: none; background-color:#' + colors[trafficType].toString(16) + '">&nbsp;</li>');
            }
        });
    },
    error: function (xhr, textStatus, errorThrown) {
        alert(xhr.statusText);
    }
});
        
// Initialize DatePicker
$(function () {
    var dates = $("#from, #to").datepicker({
        defaultDate: "+1w",
        changeMonth: true,
        numberOfMonths: 3,
        onSelect: function (selectedDate) {
            var option = this.id == "from" ? "minDate" : "maxDate",
			instance = $(this).data("datepicker"),
			date = $.datepicker.parseDate(
				instance.settings.dateFormat ||
				$.datepicker._defaults.dateFormat,
				selectedDate, instance.settings);
            dates.not(this).datepicker("option", option, date);
        }
    });
});

// Load globe data
$("#LoadData").click(function () {
    if ($("#from").datepicker("getDate") == null || $("#to").datepicker("getDate") == null) {
        alert('Start or end date is not selected.');
        return;
    }
            
    $("#container").html("");
    visitsServiceUrl = "/GlobeService.svc/GetVisits"
    $.ajax({
        url: visitsServiceUrl,
        type: 'post',
        cache: false,
        contentType: 'application/json; charset=utf-8',
        data: '{"from": "' + $("#from").datepicker("getDate").getTime() + '", "to": "' + $("#to").datepicker("getDate").getTime() + '"}',
        dataType: 'json',
        success: function (callback) {
            var globe = DAT.Globe(document.getElementById('container'), function (label) {
                return new THREE.Color(colors[label]);
            });

            globe.addData(callback.d, { format: 'legend' });
            globe.createPoints();
            globe.animate();
        },
        error: function (xhr, textStatus, errorThrown) {
            alert(xhr.statusText);
        }
    });
});