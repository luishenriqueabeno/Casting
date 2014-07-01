jQuery(document).ready(function () {
    jQuery(".lista_thumb ul li").click(function() {
jQuery(".foto_grande").hide()
jQuery(this).children(".foto_grande").fadeIn(500)
    });
});