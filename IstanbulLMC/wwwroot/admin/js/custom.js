
//#region TANIMLAMALAR

var Loading;

//#endregion TANIMLAMALAR

//#region GENEL İŞLEMLER

Kom = {
    DeleteData: function ({ tableName, id, sucFunc, isCommon = true }) {
        Kom.AlertBox.AreYouSure({
            message: 'Are you sure you want to delete the record?',
            yesFunc: function () {
                AjaxAction({
                    type: 'DELETE',
                    url: '/Common/DeleteData?tableName=' + tableName + '&id=' + id + '&isCommon=' + isCommon,
                    sucFunc: function (result) {
                        sucFunc(result);
                        Kom.Notify.Success('The deletion process has been successfully completed.');
                    }
                })
            }
        });
    },
    SaveData: function ({ tableName, data, sucFunc, isCommon = true }) {
        Kom.AlertBox.AreYouSure({
            message: 'Are you sure you want to proceed with the registration process?',
            yesFunc: function () {
                AjaxAction({
                    type: 'POST',
                    url: '/Common/SaveData?tableName=' + tableName + '&isCommon=' + isCommon,
                    data,
                    sucFunc: function (result) {
                        sucFunc(result);
                        Kom.Notify.Success('The registration process has been successfully completed.');
                    }
                });
            }
        })
    },
    GroupBy: (array, key) => {
        return array.reduce((result, currentValue) => {

            (result[currentValue[key]] = result[currentValue[key]] || []).push(
                currentValue
            );
            return result;
        }, {});
    }
}

function FormBegin() {
    Loading = Kom.MessageBox.Loading().on('shown.bs.modal', () => { });
    
}

function FormSuccess() {
    Kom.Notify.Success('The registration process has been successfully completed.');
    bootbox.hideAll();
    
}

//#endregion GENEL İŞLEMLER

//#region ALERTBOX

function AlertBox({ icon, title, message, okButton = null, cancelButton = null }) {

    if (okButton == null) {
        okButton = { className: "btn-success", text: 'Ok', func: null }
    }

    if (cancelButton == null) {
        cancelButton = { className: "btn-danger", text: 'Cancel', func: null }
    }

    Swal.fire({
        icon,
        title,
        text: message,
        showCloseButton: true,
        showConfirmButton: okButton.func != null,
        showCancelButton: cancelButton.func != null,
        buttonsStyling: false,
        customClass: {
            confirmButton: 'btn ' + (okButton.className ?? 'btn-success'),
            cancelButton: 'm-2 btn ' + (cancelButton.className ?? 'btn-danger')
        },
        confirmButtonText: (okButton.text ?? 'Ok'),
        cancelButtonText: (cancelButton.text ?? 'Cancel'),
        focusConfirm: true,
        allowOutsideClick: false
    }).then(function (result) {
        if (result.isConfirmed) {

            if (okButton.func != null) {
                okButton.func();
            }
        }
        else {
            if (cancelButton.func != null) {
                cancelButton.func();
            }
        }
    });
}

Kom.AlertBox = {
    Success: function ({ title, message, okButton = null, cancelButton = null }) {
        AlertBox({
            icon: 'success',
            title,
            message,
            okButton,
            cancelButton
        });
    },
    Error: function ({ title, message, okButton = null, cancelButton = null }) {
        AlertBox({
            icon: 'error',
            title,
            message,
            okButton,
            cancelButton
        });
    },
    Warning: function ({ title, message, okButton = null, cancelButton = null }) {
        AlertBox({
            icon: 'warning',
            title,
            message,
            okButton,
            cancelButton
        });
    },
    Question: function ({ title, message, okButton = null, cancelButton = null }) {
        AlertBox({
            icon: 'question',
            title,
            message,
            okButton,
            cancelButton
        });
    },
    AreYouSure: function ({ message, yesFunc, noFunc = null }) {
        Kom.AlertBox.Question({
            title: 'Are you sure',
            message,
            okButton: {
                text: 'Yes',
                func: yesFunc
            },
            cancelButton: {
                text: 'No',
                func: noFunc ?? function () { }
            }
        })
    }
}


//#endregion ALERTBOX

//#region MBOX

function MessageBox(type, title, message, func = null) {

    if (func == null) {
        func = function () { };
    }

    bootbox.dialog({
        title: title,
        message: '<div class="text-center"><i class="' + (type == 'error' ? 'ri-spam-2-line text-danger' : '') + ' fs-1"></i><br>' + message + '</div>',
        size: 'small',
        closeButton: false,
        centerVertical: true,
        buttons: {
            ok: {
                label: 'Tamam',
                className: 'btn-sm btn-primary',
                callback: func()
            }
        }
    })
}

Kom.MessageBox = {
    Loading: function () {
        return bootbox.dialog({
            size: 'small',
            centerVertical: true,
            message: '<p class="text-center mb-0"><i class="mdi mdi-spin mdi-dots-circle fs-2 text-info"></i><br>Please Wait..</p>',
            closeButton: false
        });
    },
    Error: function (message, func = null) {
        MessageBox('error', '', message, func);
    },
    AreYouSure: function ({ message, yesFunc = null, noFunc = null }) {
        bootbox.confirm({
            message,
            centerVertical: true,
            buttons: {
                confirm: {
                    label: 'Evet',
                    className: 'btn-success'
                },
                cancel: {
                    label: 'Hayır',
                    className: 'btn-danger'
                }
            },
            callback: function (result) {
                if (result) {
                    if (yesFunc != null) {
                        yesFunc();
                    }
                }
                else {
                    if (noFunc != null) {
                        noFunc();
                    }
                }
            }
        })
    }
}

//#endregion MBOX

//#region NOTIFICATIONS

function Notifications({ icon, title = '', message }) {

    Swal.fire({
        position: 'top-end',
        icon,
        title,
        backdrop: false,
        text: message,
        showConfirmButton: false,
        timer: 3000,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    });
}

Kom.Notify = {
    Success: function (message) {
        Notifications({
            icon: 'success',
            title: 'Success',
            message
        });
    },
    Warning: function (message) {
        Notifications({
            icon: 'warning',
            title: 'Warning',
            message
        });
    },
    Error: function (message) {
        Notifications({
            icon: 'error',
            title: 'Error',
            message
        });
    }
}

//#endregion NOTIFICATIONS

//#region AJAX İŞLEMLERİ

function AjaxError(result) {

    console.log(result);

    var errorDto = result.responseJSON;

    if (errorDto != undefined) {
        Kom.AlertBox.Error({
            title: 'Error',
            message: errorDto.Messages.join('<br>'),
            cancelButton: errorDto.StackTraces.length == 0 ? null : {
                text: 'View Details',
                func: function () {
                    bootbox.dialog({
                        title: 'Error Content',
                        message: errorDto.StackTraces.join('<br>'),
                        size: 'large',
                        closeButton: true,
                        centerVertical: true,
                        buttons: {
                            ok: {
                                label: 'Ok',
                                className: 'btn-primary'
                            }
                        }
                    })
                }
            }
        });
    }
    else {
        Kom.AlertBox.Error({
            title: 'Error',
            message: 'The website is currently unreachable. Please try again later.',
            cancelButton: {
                text: 'Ok',
                func: function () { }
            }
        });
    }
}

function AjaxAction({ type, url, data = null, sucFunc }) {

    Loading = Kom.MessageBox.Loading().on('shown.bs.modal', function () {

        var header = { RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() };

        $.ajax({
            headers: header,
            type: type,
            dataType: 'json',
            url: url,
            data: data,
            complete: function () {
                Loading.modal('hide');
            },
            error: AjaxError,
            success: function (result) {
                Loading.modal('hide');
                sucFunc(result);
            }
        });

    });

}

function AjaxActionReturn({ type, url, data = null }) {

    var header = { RequestVerificationToken: $("input[name='__RequestVerificationToken']").val() };

    return $.ajax({
        headers: header,
        type: type,
        dataType: 'json',
        url: url,
        data: data,
        async: false,
        error: AjaxError
    });

}

Kom.Ajax = {
    GetDataListFromCommon: function ({ url, sucFunc, parameters = [] }) {

        AjaxAction({
            type: 'POST',
            url: '/Common/GetDataList',
            data: { url, parameters: JSON.stringify(parameters) },
            sucFunc
        });
    },
    GetDataListReturnFromCommon: function ({ url, parameters = [] }) {

        return AjaxActionReturn({
            type: 'POST',
            url: '/Common/GetDataList',
            data: { url, parameters: JSON.stringify(parameters) }
        });
    }
}

//#endregion AJAX İŞLEMLERİ