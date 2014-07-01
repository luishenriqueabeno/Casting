jQuery(document).ready(function () {
	jQuery('#resultado').click(function() {
	  jQuery('#cont2').show();
	  jQuery('#cont1').hide();
	});

	jQuery('#votar').click(function() {
	  jQuery('#cont2').show();
	  jQuery('#cont1').hide();
	});
	
	jQuery('#voltar_enqt').click(function() {
	  jQuery('#cont1').show();
	  jQuery('#cont2').hide();
	});
});