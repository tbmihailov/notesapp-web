
//DatePicker
$(function () {
    alert('test');
    $(".datepicker").datepicker();

    //DateTimePicker
	$('.datetimepicker').datetimepicker({
		addSliderAccess: true,
		sliderAccessArgs: { touchonly: false },
		showOn: "button",
		//buttonImage: "/Images/calendar.gif",
		//buttonImageOnly: true,
		dateFormat: "dd.mm.yy",
		timeFormat: "hh:mm",
	});
});

