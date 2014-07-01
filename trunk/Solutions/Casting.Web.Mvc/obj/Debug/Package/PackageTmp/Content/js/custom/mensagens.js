jQuery(document).ready(function () {
    jQuery('.cont_msg .content .seta' ).click(function () {
		 jQuery(this).parent().toggleClass('content_close');
   });
});

jQuery(document).ready(function () {
    jQuery('.cont_msg .content:last' ).removeClass('content_close');

});

jQuery(document).ready(function() {
   
    jQuery('#btn_responder_tp').click(function(){
        jQuery('html, body').animate({scrollTop:jQuery(document).height()}, 'slow');        
    });

});
