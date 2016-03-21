(function () {
    var $menuAndMain = $("#sidebar,#wrapper");
    var $icon = $("#sidebarBtn i.fa");

    $("#sidebarBtn").on("click", function () {
        $menuAndMain.toggleClass("hide-sidebar");
        if ($menuAndMain.hasClass("hide-sidebar")) {
            $icon.addClass("fa-chevron-right");
            $icon.removeClass("fa-chevron-left");
        }
        else {
            $icon.addClass("fa-chevron-left");
            $icon.removeClass("fa-chevron-right");
        }
    });
})();