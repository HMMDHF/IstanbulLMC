
$(document).ready(function () {

    for (var i = 0; i < $('grid').length; i++) {
        var grid = $('grid')[i].ej2_instances[0];

        grid.height = $(window).height() - 380;
    }








    //#region Browser Boyutu Değiştiğinde
    $(window).resize(function () {
        for (var i = 0; i < $('grid').length; i++) {
            var grid = $('grid')[i].ej2_instances[0];

            grid.height = $(window).height() - 380;
        }
    });
    //#endregion Browser Boyutu Değiştiğinde


})