$(function () {
    $("#formSendEmail").submit(function (event) {
        event.preventDefault();
        var form = $(this);
        $.ajax({
            url: form.attr("action"),
            method: "POST",
            data: form.serialize(),
            beforeSend: function () {
                $("#exampleModalCenter").css("display", "flex");
                $("#exampleModalCenter").addClass("justify-content-center");
                $("#exampleModalCenter").addClass("align-items-center");
                $("#exampleModalCenter").html("<div class='spinner-grow text-success m-2' role='status'><span class='sr-only'>Gözləyin...</span></div>");
            },
            success: function (res) {
                if (res.status == false) {
                    form.trigger("reset");
                    location.reload();
                } else {
                    form.trigger("reset");
                    location.reload();

                }
            }
        });
    });

    $(document).on("click", ".DeleteItem", function (event) {
        var deletinput = this;
        event.preventDefault();
        Swal.fire({
            title: 'Are you sure ?',
            text: "You won't be able to revert this!",
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!'
        }).then((result) => {
            if (result.value) {
                $.ajax({
                    url: $(deletinput).attr("href"),
                    success: function (res) {

                        if (res.status == false) {
                            $.toast({
                                heading: 'Failed',
                                text: res.message,
                                showHideTransition: 'slide',
                                icon: 'error'
                            });
                            return;
                        }
                        $(deletinput).parent().parent().fadeOut(500);
                        $.toast({
                            heading: 'Success',
                            text: "Deleting is successfully!",
                            showHideTransition: 'slide',
                            icon: 'success'
                        });

                    },
                    complete: function (res) {


                    }
                })
            }
        })
    });

    $(function () {
        $(".toggle-handle").removeClass("btn-default");
        $(".toggle-handle").addClass("btn-light");
        $(document).on("click", ".toggle", function () {
            var parent = $(this);
            var status = "";
            var minus = "";
            var max = "";
            if (parent.children().hasClass("toogleStatus")) {
                status = "?status=";
                minus = "false";
                max = "true";
            } else {
                status = "?stalk=";
                minus = "1";
                max = "0";
            }
            if ($(this).attr("class") == "toggle btn btn-danger off") {

                $.get(parent.children().data("url") + status + minus, {}, function (data) {
                    if (data) {
                        parent.removeClass("btn-success");
                        parent.add("btn-danger off");
                    }
                }).then(function () {
                    $.toast({
                        heading: 'Uğurlu',
                        text: "Deaktiv edildi !",
                        showHideTransition: 'slide',
                        icon: 'success'
                    });
                });
            } else {
                $.get(parent.children().data("url") + status + max, {}, function (data) {
                    if (data) {
                        parent.removeClass("btn-danger off");
                        parent.addClass("btn-success");
                    }
                }).then(function () {
                    $.toast({
                        heading: 'Success',
                        text: "Aktivated !",
                        showHideTransition: 'slide',
                        icon: 'success'
                    });
                });
            }
        });
    });

    $("#UserGroupId").change(function () {
        var selected = $(this).children("option:selected").val();
        if (selected == 3) {
            $(".UserDepo").removeClass("d-none");
        } else {
            $(".UserDepo").addClass("d-none");
        }
    });

    $("#formSendEmailUser").submit(function (event) {
        event.preventDefault();
        var form = $(this);
        var selected = $("UserGroupId").children("option:selected").val();
        var selectedDepo = $(".UserDepo").children("option:selected").val();
        if (selected == 3 && selectedDepo == null) {
            $.toast({
                heading: 'Uğursuz',
                text: "Anbar seçin !",
                showHideTransition: 'slide',
                icon: 'error'
            });
        } else {
            $.ajax({
                url: form.attr("action"),
                method: "POST",
                data: form.serialize(),
                beforeSend: function () {
                    $("#SendButton").prop('disabled', true);
                },
                success: function (respons) {
                    if (respons.status == "required") {
                        return;
                    };
                    if (respons.status == "isHas") {
                        $("#FieldValidation").toggleClass("field-validation-error");
                        $("#FieldValidation").toggleClass("field-validation-valid");
                        $("#FieldValidation").html("Daxil etdiyiniz email artıq qeydiyyatdan keçib");
                    } else {
                        location.reload();

                    }
                }
            });
        }

    });
    //slug
    String.prototype.removeAcento = function () {
        var text = this.toLowerCase();
        text = text.replace(new RegExp('[ÁÀÂÃ]', 'gi'), 'a');
        text = text.replace(new RegExp('[ÉÈÊ]', 'gi'), 'e');
        text = text.replace(new RegExp('[ÍÌÎ]', 'gi'), 'i');
        text = text.replace(new RegExp('[ÓÒÔÕÖ]', 'gi'), 'o');
        text = text.replace(new RegExp('[ÚÙÛÜ]', 'gi'), 'u');
        text = text.replace(new RegExp('[Ç]', 'gi'), 'c');
        text = text.replace(new RegExp('[Ə]', 'gi'), 'e');
        text = text.replace(new RegExp('[Ş]', 'gi'), 's');
        return text;
    };

    String.prototype.slugify = function () {
        return this.toString().toLowerCase().removeAcento().trim()
            .replace(/\s+/g, '-') // Replace spaces with -
            .replace(/[^\w\-]+/g, '') // Remove all non-word chars
            .replace(/\-\-+/g, '-') // Replace multiple - with single -
            .replace(/^-+/, '') // Trim - from start of text
            .replace(/-+$/, ''); // Trim - from end of text
    };

    $(".slug-source").blur(function () {
        $(".slug").val($(this).val().slugify());
    });
    $(".slug-source-1").blur(function () {
        $(".slug-1").val($(this).val().slugify());
    });
});

$(function () {

    function fillInputsName() {
        var int = 0;
        $(".partnerPrice").each(function () {
            $(this).children().each(function () {
                var name = $(this).attr("name");
                if (name == undefined) {
                    name = $(this).children().attr("name");
                    if (name != undefined) {
                        name = name.replace(name.substr(16, 1), int);
                        $(this).children().attr("name", name)
                    }
                } else {
                    name = name.replace(name.substr(16, 1), int)
                    $(this).attr("name", name);
                }
            });
            if ($(this).children().length == 5 || $(this).children().length == 6) {
                int += 1;
            }
        });
    };

    $("#formModalPartner").click(function () {
        var network;
        var networkIndex;
        var networkId;
        $("#v-pills-tab").html("");
        $(".netContent").remove();
        $("#exampleModalCenter").modal('hide');
        $(".select2-selection__choice").each(function (i) {
            network = $(this).attr("title");
            networkIndex = i;
            $(".partnerOptions").each(function (index) {
                if ($(this).html() == network) {
                    networkId = $(this).val();
                    var aTag = $(
                        `<a href="" class="text-danger mr-4 deleteinputPartner" data-id="${networkId}"><i class="fas fa-backspace"></i></a><a class='nav-link deleteinputPartnerBase ${(networkIndex == 0) ? "show active" : ""}' id='nav_home_tab_${networkId}' data-toggle='pill' href='#nav_home_${networkId}' role='tab' aria-controls='nav_home_${networkId}' aria-selected='true'>${network}</a>`
                    );
                    var divInput = $(
                        `<div class="partnerPrice netContent tab-pane fade ${(networkIndex == 0) ? "show active" : ""}" id="nav_home_${networkId}" role="tabpanel" aria-labelledby="nav_home_tab_${networkId}">
                                       <div asp-validation-summary="ModelOnly" class="text-danger"></div>
                                        <input hidden name="ProductPartners[${networkIndex}].UserId" value="${networkId}" />
                                        <input hidden name="ProductPartners[${networkIndex}].Status" value="true" />
                                        <div class="form-group">
                                            <input name="ProductPartners[${networkIndex}].Price" id="ProductPartners[${networkIndex}].Price" class="form-control" placeholder="azn"/>
                                        </div>
                                    </div>`);

                    $("#v-pills-tab").append(aTag).hide().show("fast");
                    $("#v-pills-tabContent").prepend(divInput).hide().show("fast");
                }
            });

        });

    });

    $("#formModalPartnerEdit").click(function () {
        var network;
        var networkIndex;
        var networkId;
        var Id = 0;
        var productId = $("#productId").val();
        $("#v-pills-tab").html("");
        $(".netContent").remove();
        $("#exampleModalCenter").modal('hide');
        $(".select2-selection__choice").each(function (i) {
            network = $(this).attr("title");
            networkIndex = i;
            $(".partnerOptions").each(function (index) {
                if ($(this).html() == network) {
                    networkId = $(this).val();
                    var aTag = $(`<a href="" class="text-danger mr-4 deleteinputPartner" data-id="${networkId}"><i class="fas fa-backspace"></i></a><a class='nav-link deleteinputPartnerBase ${(networkIndex == 0) ? "show active" : ""}' id='nav_home_tab_${networkId}' data-toggle='pill' href='#nav_home_${networkId}' role='tab' aria-controls='nav_home_${networkId}' aria-selected='true'>${network}</a>`);
                    var divInput = $(
                        `<div class="partnerPrice netContent tab-pane fade ${(networkIndex == 0) ? "show active" : ""}" id="nav_home_${networkId}" role="tabpanel" aria-labelledby="nav_home_tab_${networkId}">
                                       <div asp-validation-summary="ModelOnly" class="text-danger"></div>
                                        <input hidden name="ProductPartners[${networkIndex}].UserId" value="${networkId}" />
                                        <input hidden name="ProductPartners[${networkIndex}].Status" value="true" />
                                <input hidden value="${Id}" name="ProductPartners[${networkIndex}].Id" id="ProductPartners[${networkIndex}].Id" />
                                <input hidden value="${productId}" name="ProductPartners[${networkIndex}].ProductId" id="ProductPartners[${networkIndex}].ProductId" />
                                        <div class="form-group">
                                            <input name="ProductPartners[${networkIndex}].Price" id="ProductPartners[${networkIndex}].Price" class="form-control" placeholder="azn" />
                                        </div>
                                    </div>`);

                    $("#v-pills-tab").append(aTag).hide().show("fast");
                    $("#v-pills-tabContent").prepend(divInput).hide().show("fast");

                }
            });

        });
    });

    $(document).on("click", ".deleteinputPartner", function (event) {
        event.preventDefault();
        var th = $(this);
        var inputDelete = $(this).data("id");
        var nextInputDelete = $(this).next().next().data("id");
        if ($(`#nav_home_tab_${inputDelete}`).hasClass("active")) {
            $(`#nav_home_${nextInputDelete}`).addClass("active show");
            $(this).next().next().next().addClass("active");
        }
        $(`#nav_home_tab_${inputDelete}`).fadeOut(300);
        $(`#nav_home_${inputDelete}`).fadeOut(300);
        $(th).fadeOut(300);
        setTimeout(function () {
            $(`#nav_home_tab_${inputDelete}`).remove();
            $(`#nav_home_${inputDelete}`).remove();
            $(th).remove();
            fillInputsName();
        }, 500);
    });

    $("#SetSessionRequestForm").submit(function (event) {
        event.preventDefault();
        var form = $(this);
        $.ajax({
            url: form.attr("action"),
            data: form.serialize(),
            success: function (res) {
                if (res.status == "0") {
                    return;
                }
                $("#ProductRequestList").html(res).hide().fadeIn(300);
                form.trigger("reset");
                $("#select2-ProductId-container").html("seçin");
                $("#CategoryId").children("option:selected").val(null);
                $("#select2-CategoryId-container").html("seçin");
                $("#Tag").html("<option value='null'>seçin</option>");
                $("#select2-ProductId-container").html("seçin");
            }
        });
    });
    $("#SetSessionOrderForm").submit(function (event) {
        event.preventDefault();
        var form = $(this);
        $.ajax({
            url: form.attr("action"),
            data: form.serialize(),
            success: function (res) {
                if (res.status == "0") {
                    return;
                }
                $("#ProductOrderList").html(res).hide().fadeIn(300);
                form.trigger("reset");
                $("#select2-ProductId-container").html("seçin");
                $("#select2-CategoryId-container").html("seçin");
                $("#CategoryId").children("option:selected").val(null);
                $("#ProductId").children("option:selected").val(null);
                $("#ProductId").html(`<option value='${null}'>seçin</option>`);
                $("#Tag").html("<option value='null'>seçin</option>");
                $("#PriceDiv").html(`<label for="Price" class="control-label">Qiymət (AZN)</label><input hidden name="Price" id="Price" value="0"/><input disabled value="0" class="form-control"/>`);
            }
        });
    });
    $(document).on('change', '#ChangeOrderCount', function () {
        var countInput = $(this);
        var productId = countInput.data('productid');
        var tag = countInput.data('tag');
        var url = countInput.data('url');
        var count = countInput.val();
        var formData = {
            productId: productId,
            tag: tag,
            orderCount: count
        };
        $.ajax({
            url: url,
            method: "POST",
            data: formData,
            success: function (res) {
                if (res.status == "0") {
                    $.toast({
                        heading: 'Uğursuz',
                        text: res.message,
                        showHideTransition: 'slide',
                        icon: 'error'
                    });
                    return;
                }
                if (res.status) {
                    $.toast({
                        heading: 'Uğurlu',
                        text: "Məhsul sayı dəyişdirildi",
                        showHideTransition: 'slide',
                        icon: 'success'
                    });
                } else {
                    $.toast({
                        heading: 'Uğursuz',
                        text: "Yenidən çəhd edin !",
                        showHideTransition: 'slide',
                        icon: 'error'
                    });
                }
            }
        });
    });
    $(document).on('change', '.ChangeBackCount', function () {
        var count = $(this).val();
        var t = $(this);
        var id = $(this).data('id');
        var formData = {
            id: id,
            count: count
        };
        $.ajax({
            url: "/panel/redundant/editCount/",
            method: "POST",
            data: formData,
            success: function (res) {
                if (res.status == "0") {
                    $.toast({
                        heading: 'Uğursuz',
                        text: res.message,
                        showHideTransition: 'slide',
                        icon: 'error'
                    });
                    t.val(0);
                    return;
                }
                if (res.status) {
                    $.toast({
                        heading: 'Uğurlu',
                        text: "Geri Qayatarılan məhsul sayı dəyişdirildi",
                        showHideTransition: 'slide',
                        icon: 'success'
                    });
                } else {
                    $.toast({
                        heading: 'Uğursuz',
                        text: "Yenidən çəhd edin !",
                        showHideTransition: 'slide',
                        icon: 'error'
                    });
                }
            }
        });

    });
    $("#formAddParametr").submit(function (event) {
        event.preventDefault();
        var form = $(this);
        $.ajax({
            url: form.attr('action'),
            method: "POST",
            data: form.serialize(),
            success: function (res) {
                if (res.status == "Half") {
                    $.toast({
                        heading: 'Uğursuz',
                        text: "Ödəniş daxil edin !",
                        showHideTransition: 'slide',
                        icon: 'error'
                    });
                    return;
                }
                if (res.status) {
                    $.toast({
                        heading: 'Uğurlu',
                        text: "Əlavə parametrlər daxil olundu !",
                        showHideTransition: 'slide',
                        icon: 'info'
                    });
                    $("#exampleModalCenter").modal('hide');
                } else {
                    $.toast({
                        heading: 'Uğursuz',
                        text: "Əlavə parametrlər daxil edilmədi !",
                        showHideTransition: 'slide',
                        icon: 'error'
                    });
                }

            }
        });

    });

    $(document).on("click", ".editType", function (event) {
        event.preventDefault();
        var type = $(this).attr("data-type");
        var id = $(this).attr("data-id");
        var button = $(this);
        var tab = button.parent().parent().parent().parent().parent();
        var url = button.attr("href");
        var table = $(`<table class="table table-responsive-lg">
                            <thead>
                                <tr>
                                    <th>Status</th>
                                    <th>Sifariş №</th>
                                    <th>Anbar</th>
                                    <th>Anbar rəhbəri</th>
                                    <th>Tarix</th>
                                    <th>Əməliyyatlar</th>
                                </tr>
                            </thead><tbody></tbody></table >`);
        var emptyTable = $(`<div class="alert alert-info emptyTable" role="alert"></div>`);
        $.ajax({
            url: url,
            beforeSend: function () {
                button.attr("disabled", "disabled")
            },
            success: function (res) {
                if (res.status == "errorCount") {
                    $.toast({
                        heading: 'Uğursuz',
                        text: "Anbarda kifayət qədər " + res.product + " yoxdur !",
                        showHideTransition: 'slide',
                        icon: 'error'
                    });
                    return;
                } else if (res.status == "Transport") {
                    $.toast({
                        heading: 'Uğursuz',
                        text: res.message,
                        showHideTransition: 'slide',
                        icon: 'error'
                    });
                    return;
                }
                if (type == 1) {
                    if ($(".acceptedTable tbody").html() == undefined) {
                        $(".acceptedTable").html(table);
                    }
                    $(".acceptedTable tbody").prepend(res).hide().fadeIn(250);
                    if (button.parent().parent().parent().children().length == 1) {
                        tab.html(emptyTable);
                        if (tab.hasClass("completedTable")) {
                            tab.children().html("Hal-hazırda Təsdiqlənmiş İstək mövcud deyil !");
                            tab.children().removeClass("alert-info").addClass("alert-success");
                        } else {
                            tab.children().html("Hal-hazırda Gözləmədə olan İstək mövcud deyil !");
                            tab.children().removeClass("alert-info").addClass("alert-warning");
                        }
                    }
                } else if (type == 0) {
                    if ($(".pendingTable tbody").html() == undefined) {
                        $(".pendingTable").html(table);
                    }
                    $(".pendingTable tbody").prepend(res).hide().fadeIn(250);
                    if (button.parent().parent().parent().children().length == 1) {
                        tab.html(emptyTable);
                        tab.children().html("Hal-hazırda Yoxlamada olan İstək mövcud deyil !");
                    }
                } else if (type == 2) {
                    if ($(".completedTable tbody").html() == undefined) {
                        $(".completedTable").html(table);
                    }
                    $(".completedTable tbody").prepend(res).hide().fadeIn(250);
                    if (button.parent().parent().parent().children().length == 1) {
                        tab.html(emptyTable);
                        tab.children().html("Hal-hazırda Yoxlamada olan İstək mövcud deyil !");
                    }
                } else if (type == 4) {
                    if ($(".completedTable tbody").html() == undefined) {
                        $(".completedTable").html(table);
                    }
                    $(".completedTable tbody").prepend(res).hide().fadeIn(250);
                }
                $.toast({
                    heading: 'Uğurlu',
                    text: "Sifarişin statusu uğurla dəyişdirildi !",
                    showHideTransition: 'slide',
                    icon: 'success'
                });

                button.parent().parent().remove();
            }
        });


    });

});

