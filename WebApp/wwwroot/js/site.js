
jQuery(function ()
{
    jQuery("#Location").autocomplete({
        source: function (request, response) {
            jQuery.getJSON(
                "https://secure.geobytes.com/AutoCompleteCity?key=7c756203dbb38590a66e01a5a3e1ad96&callback=?&q="+request.term,
                function (data) {
                    response(data);
                }
            );
        },
        minLength: 3,
        select: function (event, ui) {
            var selectedObj = ui.item;
            jQuery("#Location").val(selectedObj.value);
            getcitydetails(selectedObj.value);
            return false;
        },
        open: function () {
            jQuery(this).removeClass("ui-corner-all").addClass("ui-corner-top");
        },
        close: function () {
            jQuery(this).removeClass("ui-corner-top").addClass("ui-corner-all");
        }
    });
    jQuery("#Location").autocomplete("option", "delay", 100);
});


var dp = new DayPilot.Scheduler("dp");
dp.startDate = "2020-05-01";
dp.days = 30;
dp.timeHeaders = [
    { groupBy: "Month" },
    { groupBy: "Day", format: "d" }
];
dp.scale = "Day";

dp.init();

dp.resources = [
    {name: "Resource 1", id: "7ef238a0-bdea-4845-2726-08d800bb9ce4"},
    {name: "Resource 2", id: "2"},
    {name: "Resource 3", id: "3"},

];
// dp.events.load("/api/v1.0/date");
//        
dp.events.list = [
    {
    id: "2",
    text: "Mr. g",
    start: "2020-05-06T12:00:00",
    end: "2020-05-11T12:00:00",
    resource: "2",
    "status": "Confirmed",
    "paid": "0"
},
    {
        id: "7ef238a0-bdea-4845-2726-08d800bb9ce4",
        start: "2020-05-06T12:00:00",
        end: "2020-05-11T12:00:00",
        roomId: "b7492e4f-968a-4173-14a3-08d800bb97bc",
        resource: "3",
        
        
    }
];
dp.update();
dp.init();
// dp.dynamicLoading = true;
// dp.onScroll = function (args) {
//     args.async = true;
//     var url = "/api/v1.0/date";
//     DayPilot.Http.ajax({
//         url: url,
//         success: function (a) {
//
//             // var s = JSON.parse(a);
//             // for(let i = 0; i < s.length; i++) {
//             //     console.log(s[i])
//             // }
//             console.log(a.data)
//             args.events = a.data;
//             args.loaded();
//         }
//     });
// };