(function () {
    var $menuAndMain = $("#sidebar,#wrapper");

    $("#sidebarBtn").on("click", function () {
        $menuAndMain.toggleClass("hide-sidebar");
        if ($menuAndMain.hasClass("hide-sidebar")) {
            $(this).text(">>");
        }
        else {
            $(this).text("<<");
        }
    });
})();