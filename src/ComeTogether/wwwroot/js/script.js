(function () {
    var $menuAndMain = $("#sidebar,#wrapper");
    var $icon = $("#sidebarBtn i.fa");

    $("#sidebarBtn").on("click", function () {
        $menuAndMain.toggleClass("hide-sidebar");
        if ($menuAndMain.hasClass("hide-sidebar")) {
            $(this).addClass("fa-chevron-right");
            $(this).removeClass("fa-chevron-left");
        }
        else {
            $(this).addClass("fa-chevron-left");
            $(this).removeClass("fa-chevron-right");
        }
    });
})();