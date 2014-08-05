(function ($) {
    $("fieldset legend").expandoControl(function (controller) { return controller.nextAll(".expando"); }, { collapse: true, remember: false });

    function overrideDescription() {
        if ($("#Seo_OverrideDescription").prop("checked")) {
            $("label[for='Seo_OverrideDescription']").first().css("display", "none");
            $("label[for='Seo_OverrideDescription']").last().css("display", "inline");
            $("#Seo_DefaultDescription").css("display", "none");
            $("#Seo_Description").css("display", "inline");
        } else {
            $("label[for='Seo_OverrideDescription']").first().css("display", "inline");
            $("label[for='Seo_OverrideDescription']").last().css("display", "none");
            $("#Seo_DefaultDescription").css("display", "inline");
            $("#Seo_Description").css("display", "none");
        }
    }

    function overrideKeywords() {
        if ($("#Seo_OverrideKeywords").prop("checked")) {
            $("label[for='Seo_OverrideKeywords']").first().css("display", "none");
            $("label[for='Seo_OverrideKeywords']").last().css("display", "inline");
            $("#Seo_DefaultKeywords").css("display", "none");
        } else {
            $("label[for='Seo_OverrideKeywords']").first().css("display", "inline");
            $("label[for='Seo_OverrideKeywords']").last().css("display", "none");
            $("#Seo_DefaultKeywords").css("display", "inline");
        }
    }

    function overrideRobots() {
        if ($("#Seo_OverrideRobots").prop("checked")) {
            $("label[for='Seo_OverrideRobots']").first().css("display", "none");
            $("label[for='Seo_OverrideRobots']").last().css("display", "inline");
            $("#Seo_DefaultRobots").css("display", "none");
            $("#Seo_Robots").css("display", "inline");
        } else {
            $("label[for='Seo_OverrideRobots']").first().css("display", "inline");
            $("label[for='Seo_OverrideRobots']").last().css("display", "none");
            $("#Seo_DefaultRobots").css("display", "inline");
            $("#Seo_Robots").css("display", "none");
        }
    }

    $("#Seo_OverrideDescription").on("change", overrideDescription);
    $("#Seo_OverrideKeywords").on("change", overrideKeywords);
    $("#Seo_OverrideRobots").on("change", overrideRobots);

    overrideDescription();
    overrideKeywords();
    overrideRobots();
})(jQuery);