
//#region GENEL AYARLAR
$(window).resize(function () {

    var height = $(window).height() - 250;
    for (var i = 0; i < $(".khDataGrid").length; i++) {
        $(".khDataGrid").height(height);
    }

});

//#endregion GENEL AYARLAR

//#region DATAGRID İŞLEMLERİ

Kom.DevExpress = {
    DataSourceByUrl: function ({ url, parameters = [], key = "ID" }) {

        var result = Kom.Ajax.GetDataListReturnFromCommon({
            url,
            parameters
        });

        return {
            store: new DevExpress.data.ArrayStore({
                key: key,
                data: result.responseJSON,
                name: 'data'
            })
        }
    },
    OnToolbarPreparing: function (e) {
        e.toolbarOptions.items.forEach(function (item, i) {

            if (item.name == 'saveButton') {
                item.options.onClick = function () {

                    Kom.AlertBox.AreYouSure({
                        message: 'Are you sure you want to proceed with the registration process?',
                        yesFunc: function () {
                            e.component.saveEditData();
                        }
                    });
                }
            }

        });
    },
    OnClickedDelete: function (e) {

        e.component.cancelEditData();

        if ((e.row.data.ID ?? 0) > 0) {
            Kom.DeleteData({
                tableName: e.element.attr("id").replace('grd', ''),
                id: e.row.data.ID,
                isCommon: e.component.option('isDeleteCommon'),
                sucFunc: function (result) {
                    if (result.state) {
                        e.component.deleteRow(e.row.rowIndex);
                        e.component.saveEditData();
                        //e.component.option('focusedRowIndex', -1);
                    }
                }
            });
        }
    },
    OnClickedSave: function (e) {

        e.component.beginCustomLoading();

        var isDelete = false;
        var model = [];
        e.changes.forEach(function (item) {
            if (item.type == 'update')
                item.data['ID'] = item.key;
            else if (item.type == 'insert')
                item.data['ID'] = 0;
            else if (item.type == 'remove') {
                isDelete = true;
                return false;
            }

            var ChangeData = eval(e.component.option('changeData'));

            if ((ChangeData ?? 0) != 0) {
                $.each(ChangeData, function (key, value) {
                    item.data[key] = value;
                });
            }

            model.push(item.data);
        });

        var BetWeenSave = eval((e.component.option('betweenSave') ?? '')+ '(' + JSON.stringify(e.changes) + ')');

        if ((BetWeenSave ?? true) == false) {
            e.cancel = true;
            return false;
        }

        if (!isDelete) {

            e.cancel = true;

            var tableName = e.element.attr("id").replace('grd', '');
            model = JSON.stringify(model);


            var result = AjaxActionReturn({
                type: 'POST',
                url: '/Common/SaveData?tableName=' + tableName + '&isCommon=' + e.component.option('isSaveCommon'),
                data: { model }
            });

            var response = result.responseJSON;

            if (response.state) {

                e.changes.forEach(function (item, i) {
                    item.data['ID'] = response.dataList[i].ID;
                });
                e.cancel = false;
                e.component.option('focusedRowIndex,' - 1);

                Kom.Notify.Success('The registration process has been successfully completed.');
            }

        }

        e.component.endCustomLoading();
    }
}

//#endregion DATAGRID İŞLEMLERİ