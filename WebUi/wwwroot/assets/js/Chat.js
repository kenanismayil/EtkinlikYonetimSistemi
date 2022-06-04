//"use strict";
//$(document).ready(function () {
//    var connection = new signalR.HubConnectionBuilder().withUrl("/changeType").build();
//    connection.on("send", function (id, type) {
//        var button;
//        if ($(this).data("type") == type && $(this).data("id") == id) {
//            button = $(this);
//        }
//    });

//    var tab = button.parent().parent().parent().parent().parent();
//    var url = button.attr("href");
//    var table = $(`<table class="table table-responsive-lg">
//                            <thead>
//                                <tr>
//                                    <th>Status</th>
//                                    <th>Sifariş №</th>
//                                    <th>Anbar</th>
//                                    <th>Anbar rəhbəri</th>
//                                    <th>Əməliyyatlar</th>
//                                </tr>
//                            </thead><tbody></tbody></table >`);
//    var emptyTable = $(`<div class="alert alert-info emptyTable" role="alert"></div>`);
//    $.ajax({
//        url: url,
//        success: function (res) {
//            if (res.status == "errorCount") {
//                $.toast({
//                    heading: 'Uğursuz',
//                    text: "Anbarda kifayət qədər " + res.product + " yoxdur !",
//                    showHideTransition: 'slide',
//                    icon: 'error'
//                });
//                return;
//            } else if (res.status == "Transport") {
//                $.toast({
//                    heading: 'Uğursuz',
//                    text: res.message,
//                    showHideTransition: 'slide',
//                    icon: 'error'
//                });
//                return;
//            }
//            if (type == 1) {
//                if ($(".acceptedTable tbody").html() == undefined) {
//                    $(".acceptedTable").html(table);
//                }
//                $(".acceptedTable tbody").prepend(res).hide().fadeIn(250);
//                if (button.parent().parent().parent().children().length == 1) {
//                    tab.html(emptyTable);
//                    if (tab.hasClass("completedTable")) {
//                        tab.children().html("Hal-hazırda Təsdiqlənmiş İstək mövcud deyil !");
//                        tab.children().removeClass("alert-info").addClass("alert-success");
//                    } else {
//                        tab.children().html("Hal-hazırda Gözləmədə olan İstək mövcud deyil !");
//                        tab.children().removeClass("alert-info").addClass("alert-warning");
//                    }
//                }
//            } else if (type == 0) {
//                if ($(".pendingTable tbody").html() == undefined) {
//                    $(".pendingTable").html(table);
//                }
//                $(".pendingTable tbody").prepend(res).hide().fadeIn(250);
//                if (button.parent().parent().parent().children().length == 1) {
//                    tab.html(emptyTable);
//                    tab.children().html("Hal-hazırda Yoxlamada olan İstək mövcud deyil !");
//                }
//            } else if (type == 2) {
//                if ($(".completedTable tbody").html() == undefined) {
//                    $(".completedTable").html(table);
//                }
//                $(".completedTable tbody").prepend(res).hide().fadeIn(250);
//                if (button.parent().parent().parent().children().length == 1) {
//                    tab.html(emptyTable);
//                    tab.children().html("Hal-hazırda Yoxlamada olan İstək mövcud deyil !");
//                }
//            } else if (type == 4) {
//                if ($(".completedTable tbody").html() == undefined) {
//                    $(".completedTable").html(table);
//                }
//                $(".completedTable tbody").prepend(res).hide().fadeIn(250);
//            }
//            $.toast({
//                heading: 'Uğurlu',
//                text: "Sifarişin statusu uğurla dəyişdirildi !",
//                showHideTransition: 'slide',
//                icon: 'success'
//            });

//            button.parent().parent().remove();
//        }
//    });
//});
//$(".editType").each(function () {


//    $(document).on("click", ".editType", function (event) {
//        event.preventDefault();
//        var type = $(this).attr("data-type");
//        var id = $(this).attr("data-id");
//        connection.invoke("send", parseInt(+id), parseInt(type)).catch(function (err) {
//            return console.error(err.toString())
//        });
//    });

//    connection.start().catch(function (err) {
//        return console.error(err.toString());
//    });
//});

//// Load

//$(document).ready(function () {
//    var connectionCreate = new signalR.HubConnectionBuilder().withUrl("/changeType").build();
//    connectionCreate.on("load", function () {
//        location.reload();
//    });
//    connectionCreate.start().catch(function (err) {
//        return console.error(err.toString());
//    });
//});