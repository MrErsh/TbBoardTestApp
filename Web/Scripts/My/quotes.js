$(document).ready(function() {
    var $quotesTable = $("#quotesTable");

    function refreshGrid() {
        $quotesTable
            .jqGrid("setGridParam",
                {
                    datatype: "json",
                    url: GetRoute("Quote",
                        "GetQuotes",
                        {
                            author: $("#author").val(),
                            category: $("#category").val()
                        })
                })
            .trigger("reloadGrid");
    }

    $("#refreshBtn").click(refreshGrid);

    $quotesTable.jqGrid({
        url: GetRoute("Quote", "GetAll"),
        datatype: "json",
        colNames: ["RowId", "Автор", "Текст", "Категория", "Категория"],
        colModel: [
            { name: "RowId", index: "RowId", editable: true, stype: "text", width: 0, hidden: true, key: true },
            {
                name: "Author",
                index: "Author",
                editable: true,
                edittype: "text",
                editrules: { custom: true, custom_func: checkLength(25), required: true },
                width: 200,
                sortable: true
            },
            {
                name: "Content",
                index: "Content",
                editable: true,
                edittype: "textarea",
                editrules: { custom: true, custom_func: checkLength(4000), required: true },
                width: 200,
                sortable: false
            },
            {
                name: "CategoryName",
                index: "CategoryName",
                editable: false,
                width: 200,
                sortable: false
            },
            {
                name: "CategoryId",
                index: "CategoryId",
                editable: true,
                width: -1,
                formatter: "text",
                edittype: "select",
                editrules: { required: true },
                editoptions: { dataUrl: GetRoute("Category", "GetSelect") },
                sortable: false
            }
        ],
        loadonce: false,
        sortname: "Author",
        sortorder: "asc",
        caption: "Цитаты",
        pager: "#quotesPager",
        rowNum: 0,
        autowidth: true
    });
    $("#quotesTable").jqGrid("navGrid",
        "#quotesPager",
        {
            refresh: true,
            add: true,
            del: true,
            edit: true,
            view: true,
            search: false,
            viewtext: "Смотреть",
            viewtitle: "Выбранная запись",
            addtext: "Добавить",
            edittext: "Изменить",
            deltext: "Удалить",
            refreshtext: "Обновить"
        },
        update("edit"),
        update("add"),
        update("del")
    );

    function update(act) {
        return {
            closeAfterAdd: true,
            height: 200,
            width: 400,
            closeAfterEdit: true,
            reloadAfterSubmit: true,
            drag: true,
            onclickSubmit: function(params) {
                var list = $("#quotesTable");
                var selectedRow = list.getGridParam("selrow");
                rowData = list.getRowData(selectedRow);
                if (act === "add")
                    params.url = GetRoute("Quote", "Create");
                else if (act === "del")
                    params.url = GetRoute("Quote", "Delete");
                else if (act === "edit")
                    params.url = GetRoute("Quote", "Edit");
            },
            afterSubmit: function() {
                refreshGrid();
                return [true, "", 0];
            }
        };
    }

    function checkLength(maxLength) {
        return function(value, colname) {
            if (value && value.length > maxLength)
                return [false, colname + ": превышено максимальное количетво символов - " + maxLength + "."];
            else
                return [true, ""];
        };
    }
});