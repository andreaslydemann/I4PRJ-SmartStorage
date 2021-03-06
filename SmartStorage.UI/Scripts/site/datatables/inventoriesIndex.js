﻿$(document)
    .ready(function () {

        var table = $("#inventories")
            .DataTable({
                ajax: {
                    url: "/api/Inventories/",
                    dataSrc: ""
                },
                columns: [
                    {
                        data: "name"
                    },
                    {
                        data: "updated",
                        render: function (data) {
                            var date = new Date(data);
                            return date.toLocaleString('da-DK');
                        }
                    },
                    {
                        data: "byUser"
                    },
                    {
                        data: "inventoryId",
                        render: function (data) {
                          return "<button class='btn btn-primary btn-sm js-edit' data-inventory-id=" + data + ">Edit</button> " +
                                "<button class='btn btn-danger btn-sm js-delete' data-inventory-id=" + data + ">Delete</button>";
                        }
                    }
                ]
            });
    });

$("#inventories").on("click", ".js-edit", function () {
    var button = $(this);
    var id = button.attr("data-inventory-id");
    var url = "/Inventories/Edit/" + id;
    window.location.href = url;
});

$("#inventories").on("click", ".js-delete", function () {
    var button = $(this);

    bootbox.confirm("Are you sure you want to delete this inventory?",
        function (result) {
            if (result) {
                $.ajax({
                    url: "/api/inventories/deleteinventory/" + button.attr("data-inventory-id"),
                    method: "DELETE",
                    success: function () {
                        window.location.reload(true);
                    }
                });
            }
        });
});