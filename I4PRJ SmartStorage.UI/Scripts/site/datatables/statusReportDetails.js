﻿$(document)
    .ready(function () {
        var table = $("#status")
            .DataTable({
                ajax: {
                    url: "/api/status/GetStatus/" + document.location.pathname.split('/')[3],
                    dataSrc: ""
                },
                "columnDefs": [
                    { "width": "20%", "targets": 0 },
                    { "width": "20%", "targets": 1 },
                    { "width": "20%", "targets": 2 },
                    { "width": "20%", "targets": 3 },
                    { "width": "20%", "targets": 4 }
                ],
                columns: [
                    {
                        data: "productName"
                    },
                    {
                        data: "categoryName"
                    },
                    {
                        data: "expQuantity"
                    },
                    {
                        data: "curQuantity"
                    },
                    {
                        data: "difference"
                    }
                ]
            });
    });